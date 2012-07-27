#include "ScreenCaptureLib.h"

//Platform includes
#include <Windows.h>
#include <d3d9.h>
#include <D3dx9tex.h>

//Graphics library includes

//Additional system includes

//Utility includes

//Local includes

//Define the string to appear in the top left corner of the window
#define WINDOW_TITLE "Screen Capture Window"

//The name of the window class is the window title plus " Class"
#define WINDOW_CLASS WINDOW_TITLE " Class"

IDirect3DDevice9* g_pd3dDevice;

IDirect3DSurface9* g_pSurface;

HANDLE g_captureThreadRunningLock;
HANDLE g_captureThreadCriticalSectionLock;
bool g_runCaptureThread;

unsigned char *g_pCapturedPixelBits;

bool g_useDirectX;
LPBITMAPINFO g_lpbi;

HDC g_getDC;

DWORD WINAPI CaptureFunction(void* pParams)
{ 
	ResetEvent(g_captureThreadRunningLock);

	while(g_runCaptureThread)
	{
		DWORD tickCount = GetTickCount();
		
		HRESULT result = g_pd3dDevice->GetFrontBufferData(0, g_pSurface);
		
		D3DLOCKED_RECT lockedRect;
	
		g_pSurface->LockRect(&lockedRect,
							 NULL,
							 D3DLOCK_NO_DIRTY_UPDATE | D3DLOCK_NOSYSLOCK | D3DLOCK_READONLY);
		
		ResetEvent(g_captureThreadCriticalSectionLock);

		for( int i=0 ; i < 1050; i++)
		{
			memcpy( (BYTE*) g_pCapturedPixelBits + i * 1680 * 4, 
					(BYTE*) lockedRect.pBits + i * lockedRect.Pitch , 
					1680 * 4);
		}
	
		SetEvent(g_captureThreadCriticalSectionLock);
		
		g_pSurface->UnlockRect();
		
		DWORD millisecond = GetTickCount() - tickCount;
	
		//NB amBX update rate was about 100ms for profiles so for now
		//we'll aim for that update rate
		if(millisecond < 100)
		{
			Sleep(100 - millisecond);
		}
	}
	
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
		case WM_KEYDOWN:
			switch(wp)
			{ 
				case VK_ESCAPE:
					PostQuitMessage(0);
				  break;
				default:
				  break;
			}
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
		OutputDebugString("Initialising screen capture\n");
		//Struct to describe the main window.
		WNDCLASSEX windowClass;

		//Empty the windowClass struct
		memset(&windowClass, 0, sizeof(WNDCLASSEX));

		windowClass.cbSize = sizeof(WNDCLASSEX);	//Store the size of the struct
		windowClass.lpfnWndProc = WndProc;				//The message handling function to be used by the window
		windowClass.style = CS_HREDRAW | CS_VREDRAW;//Redraw the window on horizontal or vertical size changes
		windowClass.lpszClassName = WINDOW_CLASS;	//Assign a name to this window class
		
		//Attempt to register this window class with Windows
		if(!RegisterClassEx(&windowClass))
		{
			//Exit the program
			return 1;
		}

		//Create an, initially invisible, window
		HWND hwnd(CreateWindowEx(NULL, 
								 WINDOW_CLASS, 
								 WINDOW_TITLE,
								 WS_OVERLAPPEDWINDOW, 
								 10, 10,
								 640, 480,
								 0, 
								 0, 
								 NULL, 
								 NULL));
   
		//If now window was created
		if(!hwnd)
		{
			//Exit the program
			return 2;
		}

		LPDIRECT3D9 d3d = Direct3DCreate9(D3D_SDK_VERSION);    // create the Direct3D interface

		D3DPRESENT_PARAMETERS d3dpp;		// create a struct to hold various device information

		ZeroMemory(&d3dpp, sizeof(d3dpp));	// clear out the struct for use
		d3dpp.Windowed = TRUE;				// program windowed, not fullscreen
		d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;// discard old frames
		d3dpp.hDeviceWindow = hwnd;			// set the window to be used by Direct3D*/

		//Create a device class using this information and information from the d3dpp stuct
		d3d->CreateDevice(D3DADAPTER_DEFAULT,
						  D3DDEVTYPE_HAL,
						  hwnd,
						  D3DCREATE_SOFTWARE_VERTEXPROCESSING,
						  &d3dpp,
						  &g_pd3dDevice);
		
		unsigned int captureWidth = GetSystemMetrics(SM_CXSCREEN);
		unsigned int captureHeight = GetSystemMetrics(SM_CYSCREEN);

		g_pCapturedPixelBits = (unsigned char*) malloc(sizeof(unsigned char) * captureWidth * captureHeight * 4);
	
		//TODO this currently uses system memory. This worked cause we were grabbing pixel data for use
		//by the CPU. Now that we're going to be scaling this to another surface, should it use video
		//memory instead?
		HRESULT result = g_pd3dDevice->CreateOffscreenPlainSurface(captureWidth, captureHeight,
																   D3DFMT_A8R8G8B8, 
																   D3DPOOL_SYSTEMMEM, //D3DPOOL_DEFAULT,
																   &g_pSurface, 
																   NULL);
		
		g_captureThreadRunningLock = CreateEvent(NULL,
												 TRUE,
												 TRUE,
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

		return 0;
	}

	DECLDIR unsigned int StartCapturing(bool useDirectX)
	{
		g_useDirectX = useDirectX;

		if(useDirectX)
		{
			g_runCaptureThread = true;
		
			CreateThread( NULL, 
						  0, 
						  CaptureFunction, 
						  NULL, 
						  0, 
						  NULL);
		}
		else
		{
			g_getDC = GetDC(0);

			g_lpbi = (LPBITMAPINFO) (new char[sizeof(BITMAPINFOHEADER) + 256 * sizeof(RGBQUAD)]);
		}

		return 0;
	}

	DECLDIR unsigned int StopCapturing(void)
	{
		g_runCaptureThread = false;

		ReleaseDC(NULL, g_getDC);

		WaitForSingleObject(g_captureThreadRunningLock, 1000);

		return 0;
	}

	DECLDIR unsigned int GetAverageColour(unsigned int x, unsigned int y,
										  unsigned int width, unsigned int height)
	{

		if(g_useDirectX)
		{
			unsigned int r = 0;
			unsigned int g = 0;
			unsigned int b = 0;

			unsigned int count(0);

			for(unsigned int yPos = y; yPos < y + height; ++yPos)
			{
				for(unsigned int xPos = x * 4; xPos < (x + width) * 4; xPos += 4)
				{
					b += ((unsigned char *) g_pCapturedPixelBits)[xPos + (yPos * 1680 * 4)];
					g += ((unsigned char *) g_pCapturedPixelBits)[xPos + (yPos * 1680 * 4) + 1];
					r += ((unsigned char *) g_pCapturedPixelBits)[xPos + (yPos * 1680 * 4) + 2];
				
					++count;
				}
			}
	
			if(count == 0)
			{
				return 0;
			}

			r /= count;
			g /= count;
			b /= count;
		
			unsigned int colour = r;
			colour <<= 8;
			colour |= g;
			colour <<= 8;
			colour |= b;

			return colour;
		}
		else
		{
			DWORD tickCount(GetTickCount());
			HDC bitmapDC = CreateCompatibleDC(0);

			HBITMAP bitmap = CreateCompatibleBitmap(g_getDC, width, height);
	
			SelectObject(bitmapDC, bitmap);
			BitBlt(bitmapDC, 0, 0, width, height, g_getDC, x, y, SRCCOPY);	//BitBlts are expensive!

			HBITMAP OffscrBmp = NULL;
			HDC OffscrDC = NULL;

			if ((OffscrBmp = CreateCompatibleBitmap(bitmapDC, width, height)) == NULL)
			{
				return 0;
			}
	
			if ((OffscrDC = CreateCompatibleDC(bitmapDC)) == NULL)
			{
				return 0;
			}
	
			HBITMAP OldBmp = (HBITMAP) SelectObject(OffscrDC, OffscrBmp);

			BitBlt(OffscrDC, 0, 0, width, height, bitmapDC, 0, 0, SRCCOPY);
		
			ZeroMemory(&g_lpbi->bmiHeader, sizeof(BITMAPINFOHEADER));
			g_lpbi->bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
			SelectObject(OffscrDC, OldBmp);
			if (!GetDIBits(OffscrDC, OffscrBmp, 0, height, NULL, g_lpbi, DIB_RGB_COLORS))
			{
				return 0;
			}
	
			LPVOID lpvb;

			//Create space to store this buffer
			if((lpvb = new char[g_lpbi->bmiHeader.biSizeImage]) == NULL)
			{
				return 0;
			}

			if(!GetDIBits(OffscrDC, OffscrBmp, 0, height, lpvb, g_lpbi, DIB_RGB_COLORS))
			{
				return 0;
			}
	
			unsigned int averageR(0);
			unsigned int averageG(0);
			unsigned int averageB(0);
			unsigned int pixelCount(0);
	
			for(y = 0; y < height; ++y)
			{
				for(x = 0; x < width * 4; x += 4)
				{
					unsigned char b = ((unsigned char*) lpvb)[0 + x + (y * width * 4)];
					unsigned char g = ((unsigned char*) lpvb)[1 + x + (y * width * 4)];
					unsigned char r = ((unsigned char*) lpvb)[2 + x + (y * width * 4)];
					//unsigned char a = ((unsigned char*) lpvBits)[3 + x + (y * width * 4)];

					averageR += r;
					averageG += g;
					averageB += b;

					++pixelCount;
				}
			}
	
			averageR /= pixelCount;
			averageG /= pixelCount;
			averageB /= pixelCount;

			unsigned int colour = averageR;
			colour <<= 8;
			colour |= averageG;
			colour <<= 8;
			colour |= averageB;

			DeleteDC(OffscrDC);
			DeleteObject(OffscrBmp);
	
			DeleteObject(bitmap);
			DeleteDC(bitmapDC);

			return colour;
		}
	}
}