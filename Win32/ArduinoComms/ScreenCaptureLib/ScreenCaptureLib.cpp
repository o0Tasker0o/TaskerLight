#include "ScreenCaptureLib.h"

//Platform includes
#include <Windows.h>

//Graphics library includes
#include <d3d9.h>
#include <D3dx9tex.h>

//Additional system includes

//Utility includes

//Local includes

//Define the string to appear in the top left corner of the window
#define WINDOW_TITLE "Screen Capture Window"

//The name of the window class is the window title plus " Class"
#define WINDOW_CLASS WINDOW_TITLE " Class"

IDirect3DDevice9* g_pD3DDevice(NULL);
IDirect3DSurface9* g_pSurface(NULL);

HANDLE g_captureThreadRunningLock(NULL);
HANDLE g_captureThreadCriticalSectionLock(NULL);
bool g_runCaptureThread(false);

unsigned char *g_pCapturedPixelBits(NULL);

const unsigned int g_screenWidth(GetSystemMetrics(SM_CXSCREEN));
const unsigned int g_screenHeight(GetSystemMetrics(SM_CYSCREEN));

const unsigned int g_maxBorder(200);
unsigned int g_leftBorder(0);
unsigned int g_rightBorder(0);

HWND g_hwnd(NULL);
WNDCLASSEX g_windowClass;

DWORD WINAPI CaptureFunction(void* pParams)
{ 
	//Don't allow the program to end while this thread is running
	ResetEvent(g_captureThreadRunningLock);

	DWORD tickCount(0);
	DWORD millisecond(0);
	HRESULT result(S_OK);
	D3DLOCKED_RECT lockedRect;
	
	//Run this capture thread continually
	while(g_runCaptureThread)
	{
		tickCount = GetTickCount();
		
		result = g_pD3DDevice->GetFrontBufferData(0, g_pSurface);
		
		if(FAILED(result))
		{
			Sleep(100);
			continue;
		}
	
		result = g_pSurface->LockRect(&lockedRect,
									  NULL,
									  D3DLOCK_NO_DIRTY_UPDATE | D3DLOCK_NOSYSLOCK | D3DLOCK_READONLY);
		
		if(FAILED(result))
		{
			Sleep(100);
			continue;
		}
		
		WaitForSingleObject(g_captureThreadCriticalSectionLock, 500);
		ResetEvent(g_captureThreadCriticalSectionLock);

		memcpy(g_pCapturedPixelBits, lockedRect.pBits, g_screenWidth * g_screenHeight * 4);
			
		SetEvent(g_captureThreadCriticalSectionLock);

		result = g_pSurface->UnlockRect();
		
		if(FAILED(result))
		{
			Sleep(100);
			continue;
		}

		millisecond = GetTickCount() - tickCount;
	
		//NB amBX update rate was about 100ms for profiles so for now
		//we'll aim for that update rate
		if(millisecond < 100)
		{
			Sleep(100 - millisecond);
		}
	}
	
	//The thread is now exiting and program can safely be exited
	SetEvent(g_captureThreadRunningLock);

	return 0;
}

//The function that will handle any messages sent to the window
LRESULT CALLBACK WndProc(HWND hwnd, UINT m, WPARAM wp, LPARAM lp)
{    
	//Check the message code
	switch(m)
	{    
		case WM_CLOSE:
		case WM_DESTROY:
			PostQuitMessage(0);
		  break;
		default:
		  break;
	}

  //Pass remaining messages to default handler.
  return (DefWindowProc(hwnd, m, wp, lp));
}

extern "C"
{
    DECLDIR unsigned int InitialiseScreenCapture(void)
	{
		//Empty the windowClass struct
		memset(&g_windowClass, 0, sizeof(WNDCLASSEX));

		g_windowClass.cbSize = sizeof(WNDCLASSEX);		//Store the size of the struct
		g_windowClass.lpfnWndProc = WndProc;			//The message handling function to be used by the window
		g_windowClass.style = CS_HREDRAW | CS_VREDRAW;	//Redraw the window on horizontal or vertical size changes
		g_windowClass.lpszClassName = WINDOW_CLASS;		//Assign a name to this window class
		
		//Attempt to register this window class with Windows
		if(!RegisterClassEx(&g_windowClass))
		{
			//Exit the program
			return 1;
		}

		//Create an, initially invisible, window
		g_hwnd = CreateWindowEx(NULL, 
								WINDOW_CLASS, 
								WINDOW_TITLE,
								WS_OVERLAPPEDWINDOW, 
								10, 10,
								640, 480,
								0, 
								0, 
								NULL, 
								NULL);
   
		//If now window was created
		if(!g_hwnd)
		{
			//Exit the program
			return 2;
		}

		LPDIRECT3D9 d3d = Direct3DCreate9(D3D_SDK_VERSION);    // create the Direct3D interface

		D3DPRESENT_PARAMETERS d3dpp;		// create a struct to hold various device information

		ZeroMemory(&d3dpp, sizeof(d3dpp));	// clear out the struct for use
		d3dpp.Windowed = TRUE;				// program windowed, not fullscreen
		d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;// discard old frames
		d3dpp.hDeviceWindow = g_hwnd;		// set the window to be used by Direct3D*/

		//Create a device class using this information and information from the d3dpp stuct
		d3d->CreateDevice(D3DADAPTER_DEFAULT,
						  D3DDEVTYPE_HAL,
						  g_hwnd,
						  D3DCREATE_SOFTWARE_VERTEXPROCESSING,
						  &d3dpp,
						  &g_pD3DDevice);
		
		g_pCapturedPixelBits = (unsigned char*) malloc(sizeof(unsigned char) * g_screenWidth * g_screenHeight * 4);
	
		//TODO this currently uses system memory. This worked cause we were grabbing pixel data for use
		//by the CPU. Now that we're going to be scaling this to another surface, should it use video
		//memory instead?
		HRESULT result = g_pD3DDevice->CreateOffscreenPlainSurface(g_screenWidth, g_screenHeight,
																   D3DFMT_A8R8G8B8, 
																   D3DPOOL_SYSTEMMEM, //D3DPOOL_DEFAULT,
																   &g_pSurface, 
																   NULL);
		
		g_captureThreadRunningLock = CreateEvent(NULL,
												 TRUE,
												 FALSE,
												 "Capture Thread Lock");
				
		g_captureThreadCriticalSectionLock = CreateEvent(NULL,
														 TRUE,
														 TRUE,
														 "Critical Section Lock");

		g_runCaptureThread = false;

		return 0;
	}

	DECLDIR unsigned int ShutdownScreenCapture(void)
	{
		StopCapturing();

		free(g_pCapturedPixelBits);
		
		SendMessage(g_hwnd, WM_CLOSE, NULL, NULL);

		UnregisterClass(WINDOW_CLASS, g_windowClass.hInstance);

		return 0;
	}

	DECLDIR unsigned int StartCapturing(bool useDirectX)
	{
		g_runCaptureThread = true;

		CreateThread( NULL, 
						0, 
						CaptureFunction, 
						NULL, 
						0, 
						NULL);

		return 0;
	}

	DECLDIR unsigned int StopCapturing(void)
	{
		g_runCaptureThread = false;
		
		WaitForSingleObject(g_captureThreadRunningLock, 1000);

		return 0;
	}

	DECLDIR unsigned int GetAverageColour(unsigned int x, unsigned int y,
										  unsigned int width, unsigned int height)
	{
		unsigned int r = 0;
		unsigned int g = 0;
		unsigned int b = 0;

		unsigned int count(0);
			
		//Don't allow any other threads to access the captured pixel data
		WaitForSingleObject(g_captureThreadCriticalSectionLock, 500);
		ResetEvent(g_captureThreadCriticalSectionLock);

		//Go through each BGRA byte in the given region
		for(unsigned int yPos = y; yPos < y + height; ++yPos)
		{
			for(unsigned int xPos = x * 4; xPos < (x + width) * 4; xPos += 4)
			{
				//Add the total colour of this region
				b += g_pCapturedPixelBits[xPos + (yPos * g_screenWidth * 4)];
				g += g_pCapturedPixelBits[xPos + (yPos * g_screenWidth * 4) + 1];
				r += g_pCapturedPixelBits[xPos + (yPos * g_screenWidth * 4) + 2];

				++count;
			}
		}
				
		//Give up access to the captured pixel data
		SetEvent(g_captureThreadCriticalSectionLock);
				
		//If no pixels were read, return Black
		if(count == 0)
		{
			return 0;
		}

		//Calculate the average colour
		r /= count;
		g /= count;
		b /= count;
		
		//Return an integer containing the RGB colour
		unsigned int colour = r;
		colour <<= 8;
		colour |= g;
		colour <<= 8;
		colour |= b;

		return colour;
	}

	DECLDIR void TestLeftBorder(int x, int y, int width, int height)
	{	
		unsigned char r, g, b;

		x += g_leftBorder;

		bool allBlack = true;

		WaitForSingleObject(g_captureThreadCriticalSectionLock, 500);
		ResetEvent(g_captureThreadCriticalSectionLock);

		for(int yPos = y; yPos < y + height; yPos += 2)
		{

			b = g_pCapturedPixelBits[(x * 4) + (yPos * g_screenWidth * 4)];
			g = g_pCapturedPixelBits[(x * 4) + (yPos * g_screenWidth * 4) + 1];
			r = g_pCapturedPixelBits[(x * 4) + (yPos * g_screenWidth * 4) + 2];

			if(r != 0 || g != 0 || b != 0)
			{
				allBlack = false;
				break;
			}
		}	
		
		SetEvent(g_captureThreadCriticalSectionLock);

		if(allBlack)
		{
			if(g_leftBorder < g_maxBorder)
			{
				++g_leftBorder;
			}
		}
		else
		{
			if(g_leftBorder > 0)
			{
				--g_leftBorder;
			}
		}
	}
	
	DECLDIR void TestRightBorder(int x, int y, int width, int height)
	{	
		unsigned char r, g, b;

		x += width - 1;
		x -= g_rightBorder;

		bool allBlack = true;

		WaitForSingleObject(g_captureThreadCriticalSectionLock, 500);
		ResetEvent(g_captureThreadCriticalSectionLock);

		for(int yPos = y; yPos < y + height; yPos += 2)
		{

			b = g_pCapturedPixelBits[(x * 4) + (yPos * g_screenWidth * 4)];
			g = g_pCapturedPixelBits[(x * 4) + (yPos * g_screenWidth * 4) + 1];
			r = g_pCapturedPixelBits[(x * 4) + (yPos * g_screenWidth * 4) + 2];

			if(r != 0 || g != 0 || b != 0)
			{
				allBlack = false;
				break;
			}
		}	
			
		SetEvent(g_captureThreadCriticalSectionLock);

		if(allBlack)
		{
			if(g_rightBorder < g_maxBorder)
			{
				++g_rightBorder;
			}
		}
		else
		{
			if(g_rightBorder > 0)
			{
				--g_rightBorder;
			}
		}
	}

	DECLDIR int GetLeftBorder(void)
	{
		return g_leftBorder;
	}

	DECLDIR int GetRightBorder(void)
	{
		return g_rightBorder;
	}
}