#include <windows.h>
#include <stdio.h>
#include <stdlib.h>

#include <math.h>

int main()
{
	HANDLE arduinoSerial = CreateFile("COM3",
									  GENERIC_READ | GENERIC_WRITE,
									  0,
									  NULL,
									  OPEN_EXISTING,
									  FILE_ATTRIBUTE_NORMAL,
									  NULL);
	
	if(INVALID_HANDLE_VALUE == arduinoSerial)
    {
        //If not success full display an Error
        if(ERROR_FILE_NOT_FOUND == GetLastError())
		{
            //Print Error if neccessary
            printf("ERROR: Handle was not attached. Reason: COM3 not available.\n");

        }
        else
        {
            printf("ERROR!!!");
        }
    }
	else
	{
		DCB dcbSerialParams = {0};

        //Try to get the current
        if (!GetCommState(arduinoSerial, &dcbSerialParams))
        {
            //If impossible, show an error
            printf("failed to get current serial parameters!");
        }
        else
        {
            //Define serial connection parameters for the arduino board
            dcbSerialParams.BaudRate=CBR_9600;
            dcbSerialParams.ByteSize=8;
            dcbSerialParams.StopBits=ONESTOPBIT;
            dcbSerialParams.Parity=NOPARITY;

            //Set the parameters and check for their proper application
            if(!SetCommState(arduinoSerial, &dcbSerialParams))
            {
                printf("ALERT: Could not set Serial Port parameters");
            }
            else
            {
                //We wait 2s as the arduino board will be reseting
                Sleep(4000);
            }

			unsigned char colourBytes[75];
			unsigned char receiveByte;
			
			DWORD bytesSent = 0;
			DWORD bytesReceived = 0;
			
			for(int i = 0; i < 255; i += 5)
			{
				DWORD startTime = GetTickCount();

				int colourIndex = 0;

				for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
				{					
					colourBytes[colourIndex++] = i;
					colourBytes[colourIndex++] = 255 - i;
					colourBytes[colourIndex++] = 0;
				}

				WriteFile(arduinoSerial, colourBytes, 75, &bytesSent, NULL);
				ReadFile(arduinoSerial, &receiveByte, 1, &bytesReceived, NULL);

				printf("Red = %d\n", i);
				printf("Time taken = %d\n", GetTickCount() - startTime);
			}
        }
	}

	CloseHandle(arduinoSerial);
		
	getchar();

	return 0;
}