#ifndef SCREENCAPTURELIB_H
#define SCREENCAPTURELIB_H

#include <Windows.h>

#define DECLDIR __declspec(dllexport)

//Exported functions
extern "C"
{
    DECLDIR unsigned int InitialiseScreenCapture(void);

	DECLDIR unsigned int ShutdownScreenCapture(void);

	DECLDIR unsigned int StartCapturing(bool useDirectX);

	DECLDIR unsigned int StopCapturing(void);

	DECLDIR unsigned int GetAverageColour(unsigned int x, unsigned int y,
										  unsigned int width, unsigned int height);
}

#endif