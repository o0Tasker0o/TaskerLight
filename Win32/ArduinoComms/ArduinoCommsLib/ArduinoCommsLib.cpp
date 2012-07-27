#include "ArduinoCommsLib.h"

#include <stdio.h>

#define NUMBER_OF_LEDS 25

struct LEDColour
{
	unsigned char r, g, b;
};

HANDLE g_arduinoHandle(NULL);
LEDColour g_Pixels[NUMBER_OF_LEDS];

extern "C"
{
    DECLDIR unsigned int InitialiseArduinoComms(char *pComPort)
	{
		//Open the arduino's serial port for reading and writing
		g_arduinoHandle = CreateFile(pComPort,
									 GENERIC_READ | GENERIC_WRITE,
									 0,
									 NULL,
									 OPEN_EXISTING,
									 FILE_ATTRIBUTE_NORMAL,
									 NULL);
	
		//If you can't read from or write to the Arduino in 500ms then
		//something has probably gone wrong
		COMMTIMEOUTS timeouts;		
		timeouts.ReadTotalTimeoutConstant = 500;
		timeouts.ReadTotalTimeoutMultiplier = 1;
		timeouts.WriteTotalTimeoutConstant = 500;
		timeouts.WriteTotalTimeoutMultiplier = 1;

		SetCommTimeouts(g_arduinoHandle, &timeouts);

		//If you were unable to open comms with the Arduino
		if(INVALID_HANDLE_VALUE == g_arduinoHandle)
		{
			MessageBox(NULL, 
					   "Unable to open Arduino comms", 
					   "IO Error", 
					   MB_OK | MB_ICONERROR);

			return TASKERLIGHT_ERROR;
		}
		else //You are now connected to the Arduino
		{
			DCB dcbSerialParams = {0};

			//Try to get the current
			if (!GetCommState(g_arduinoHandle, &dcbSerialParams))
			{
				MessageBox(NULL, 
						   "Unable get serial properties", 
						   "Init Error", 
						   MB_OK | MB_ICONERROR);

				return TASKERLIGHT_ERROR;
			}
			else
			{
				//Define serial connection parameters for the Arduino board
				dcbSerialParams.BaudRate=CBR_115200;
				dcbSerialParams.ByteSize=8;
				dcbSerialParams.StopBits=ONESTOPBIT;
				dcbSerialParams.Parity=NOPARITY;

				//Set the parameters and check for their proper application
				if(!SetCommState(g_arduinoHandle, &dcbSerialParams))
				{
					MessageBox(NULL, 
							   "Unable set serial properties", 
							   "Init Error", 
							   MB_OK | MB_ICONERROR);

					return TASKERLIGHT_ERROR;
				}
				else
				{
					//We wait 3s as the Arduino resets whenever a connection
					//is made. This gives it time to boot
					Sleep(3000);
				}
			}
		}

		return TASKERLIGHT_OK;
	}

	DECLDIR unsigned int ShutdownArduinoComms(void)
	{
		CloseHandle(g_arduinoHandle);

		return TASKERLIGHT_OK;
	}
	
	DECLDIR unsigned int SetLED(unsigned char red, 
								unsigned char green, 
								unsigned char blue, 
								unsigned int pixelIndex)
	{
		if(pixelIndex < NUMBER_OF_LEDS)
		{
			g_Pixels[pixelIndex].r = red;
			g_Pixels[pixelIndex].g = green;
			g_Pixels[pixelIndex].b = blue;

			return TASKERLIGHT_OK;
		}

		return TASKERLIGHT_ERROR;
	}

	DECLDIR unsigned int FlushColours(void)
	{
		DWORD bytesSent(0);
		DWORD bytesReceived(0);

		//Send the pixel colours to the Arduino
		WriteFile(g_arduinoHandle, g_Pixels, NUMBER_OF_LEDS * 3, &bytesSent, NULL);
		
		//If not all the RGB values of the LEDs were written to the Arduino
		if(NUMBER_OF_LEDS * 3 != bytesSent)
		{
			return TASKERLIGHT_ERROR;
		}

		unsigned char response(1);

		//Wait until the Arduino responds
		ReadFile(g_arduinoHandle, &response, 1, &bytesReceived, NULL);

		//If the Arduino did not respond correctly or just didn't respond
		if(0 != response || 1 != bytesReceived)
		{
			return TASKERLIGHT_ERROR;
		}

		return TASKERLIGHT_OK;
	}
}