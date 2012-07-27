#include <windows.h>

#include <stdio.h>

#include "../ArduinoCommsLib/ArduinoCommsLib.h"

int main()
{
	if(TASKERLIGHT_OK != InitialiseArduinoComms())
	{
		return -1;
	}
	
	unsigned char colour = 0;
	bool ascending(true);

	while(!GetKeyState(VK_SPACE))
	{
		DWORD startTime = GetTickCount();

		for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
		{
			SetLED(colour, 0, 255 - colour, pixelIndex);
		}

		if(ascending)
		{
			colour += 5;
		}
		else
		{
			colour -= 5;
		}

		if(0 == colour || 255 == colour)
		{
			ascending = !ascending;
		}

		FlushColours();

		printf("%d\n", GetTickCount() - startTime);
	}

	ShutdownArduinoComms();

	return 0;
}