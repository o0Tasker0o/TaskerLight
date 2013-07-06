using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace ControlPanel
{
    public class VideoEffectGenerator : EffectGenerator
    {
        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 InitialiseScreenCapture();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 ShutdownScreenCapture();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 StartCapturing();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 StopCapturing();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 GetAverageColour(UInt32 x, UInt32 y,
                                                      UInt32 width, UInt32 height);
        
        public VideoEffectGenerator(ColourOutputManager colourOutputManager) : base(colourOutputManager)
        {
        }

        protected override void ThreadTick()
        {
            mOutputManager.FadeTimeMs = 90;

            InitialiseScreenCapture();
            StartCapturing();

            while (mRunning)
            {
                UInt32 width = 200;
                UInt32 height = 128;

                mOutputManager.SetPixel(0, UInt32ToColor(GetAverageColour(1320, 950, width, height)));
                mOutputManager.SetPixel(1, UInt32ToColor(GetAverageColour(1520, 950, width, height)));
                mOutputManager.SetPixel(2, UInt32ToColor(GetAverageColour(1720, 950, width, height)));

                mOutputManager.SetPixel(3, UInt32ToColor(GetAverageColour(1720, 796, width, height)));
                mOutputManager.SetPixel(4, UInt32ToColor(GetAverageColour(1720, 642, width, height)));
                mOutputManager.SetPixel(5, UInt32ToColor(GetAverageColour(1720, 488, width, height)));
                mOutputManager.SetPixel(6, UInt32ToColor(GetAverageColour(1720, 334, width, height)));
                mOutputManager.SetPixel(7, UInt32ToColor(GetAverageColour(1720, 180, width, height)));

                mOutputManager.SetPixel(8, UInt32ToColor(GetAverageColour(1720, 0, width, height)));
                mOutputManager.SetPixel(9, UInt32ToColor(GetAverageColour(1491, 0, width, height)));
                mOutputManager.SetPixel(10, UInt32ToColor(GetAverageColour(1278, 0, width, height)));
                mOutputManager.SetPixel(11, UInt32ToColor(GetAverageColour(1065, 0, width, height)));
                mOutputManager.SetPixel(12, UInt32ToColor(GetAverageColour(852, 0, width, height)));
                mOutputManager.SetPixel(13, UInt32ToColor(GetAverageColour(639, 0, width, height)));
                mOutputManager.SetPixel(14, UInt32ToColor(GetAverageColour(426, 0, width, height)));
                mOutputManager.SetPixel(15, UInt32ToColor(GetAverageColour(213, 0, width, height)));
                mOutputManager.SetPixel(16, UInt32ToColor(GetAverageColour(0, 0, width, height)));

                mOutputManager.SetPixel(17, UInt32ToColor(GetAverageColour(0, 180, width, height)));
                mOutputManager.SetPixel(18, UInt32ToColor(GetAverageColour(0, 334, width, height)));
                mOutputManager.SetPixel(19, UInt32ToColor(GetAverageColour(0, 488, width, height)));
                mOutputManager.SetPixel(20, UInt32ToColor(GetAverageColour(0, 642, width, height)));
                mOutputManager.SetPixel(21, UInt32ToColor(GetAverageColour(0, 796, width, height)));

                mOutputManager.SetPixel(22, UInt32ToColor(GetAverageColour(0, 950, width, height)));
                mOutputManager.SetPixel(23, UInt32ToColor(GetAverageColour(200, 950, width, height)));
                mOutputManager.SetPixel(24, UInt32ToColor(GetAverageColour(400, 950, width, height)));

                mOutputManager.FlushColours();

                Thread.Sleep(mOutputManager.FadeTimeMs);
            }

            StopCapturing();
            ShutdownScreenCapture();
        }

        private Color UInt32ToColor(UInt32 rgbColour)
        {
            UInt32 blue = rgbColour & 255;
            rgbColour >>= 8;
            UInt32 green = rgbColour & 255;
            rgbColour >>= 8;
            UInt32 red = rgbColour & 255;

            return Color.FromArgb((int) red, (int) green, (int) blue);
        }
    }
}
