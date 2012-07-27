#ifndef ARDUINOCOMMSLIB_H
#define ARDUINOCOMMSLIB_H

#include <Windows.h>

#define DECLDIR __declspec(dllexport)

#define TASKERLIGHT_OK		0x00
#define TASKERLIGHT_ERROR	0x01

struct LEDColour
{
	unsigned char r, g, b;
};

//Exported functions
extern "C"
{
    DECLDIR unsigned int Initialise(void);

	DECLDIR unsigned int Shutdown(void);
	
	DECLDIR unsigned int SetLED(unsigned char red, 
								unsigned char green, 
								unsigned char blue, 
								unsigned int pixelIndex);

	DECLDIR unsigned int FlushColours(void);
}

#endif