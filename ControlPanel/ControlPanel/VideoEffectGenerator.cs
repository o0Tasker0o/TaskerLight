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
            mOutputManager.FadeTimeMs = 110;

            InitialiseScreenCapture();
            StartCapturing();

            while (mRunning)
            {
                for(UInt16 regionIndex = 0; regionIndex < 25; ++regionIndex)
                {
                    Rectangle captureSubRegion = PixelRegions.Instance.GetCaptureSubRegion(regionIndex);

                    UInt32 capturedColour = GetAverageColour((UInt32) captureSubRegion.Left,
                                                             (UInt32) captureSubRegion.Top,
                                                             (UInt32) captureSubRegion.Width,
                                                             (UInt32) captureSubRegion.Height);

                    mOutputManager.SetPixel(regionIndex, UInt32ToColor(capturedColour));
                }
                
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
