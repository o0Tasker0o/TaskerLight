#ifndef SCREENCAPTURELIB_H
#define SCREENCAPTURELIB_H

#define DECLDIR __declspec(dllexport)

extern "C"
{
    DECLDIR unsigned int InitialiseScreenCapture(void);

	DECLDIR unsigned int ShutdownScreenCapture(void);

	DECLDIR unsigned int StartCapturing(void);

	DECLDIR unsigned int StopCapturing(void);

	DECLDIR unsigned int GetAverageColour(int left, int right, int top, int bottom);
	
	DECLDIR unsigned int GetLeftPadding(int left, int right, int top, int bottom);
	DECLDIR unsigned int GetRightPadding(int left, int right, int top, int bottom);
	DECLDIR unsigned int GetTopPadding(int left, int right, int top, int bottom);
	DECLDIR unsigned int GetBottomPadding(int left, int right, int top, int bottom);
}

#endif