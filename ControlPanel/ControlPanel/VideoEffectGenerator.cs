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
        private static extern UInt32 GetAverageColour(int left, int right, int top, int bottom);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 GetLeftPadding(int left, int right, int top, int bottom);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 GetRightPadding(int left, int right, int top, int bottom);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 GetTopPadding(int left, int right, int top, int bottom);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 GetBottomPadding(int left, int right, int top, int bottom);

        private ApplicationFinder mApplicationFinder;

        private const UInt16 cMaxPadding = 300;

        public VideoEffectGenerator(ColourOutputManager colourOutputManager, ApplicationFinder appFinder) : base(colourOutputManager)
        {
            mApplicationFinder = appFinder;
        }

        protected override void ThreadTick()
        {
            mOutputManager.FadeTimeMs = 90;

            InitialiseScreenCapture();
            StartCapturing();

            while (mRunning)
            {
                long startTick = DateTime.Now.Ticks;

                Rectangle captureRegion = mApplicationFinder.GetTopmostRegisteredApp();

                UInt32 leftPadding = GetLeftPadding(captureRegion.Left, captureRegion.Right, captureRegion.Top, captureRegion.Bottom);
                UInt32 rightPadding = GetRightPadding(captureRegion.Left, captureRegion.Right, captureRegion.Top, captureRegion.Bottom);
                UInt32 topPadding = GetTopPadding(captureRegion.Left, captureRegion.Right, captureRegion.Top, captureRegion.Bottom);
                UInt32 bottomPadding = GetBottomPadding(captureRegion.Left, captureRegion.Right, captureRegion.Top, captureRegion.Bottom);

                CapPadding(ref leftPadding, ref rightPadding, ref topPadding, ref bottomPadding);

                long x = captureRegion.Left + leftPadding;
                long y = captureRegion.Top + topPadding;
                long width = captureRegion.Width - (leftPadding + rightPadding);
                long height = captureRegion.Height - (topPadding + bottomPadding);
                
                PixelRegions.Instance.CaptureRegion = new Rectangle((int) x, (int) y, (int) width, (int) height);
                
                for(UInt16 regionIndex = 0; regionIndex < 25; ++regionIndex)
                {
                    Rectangle captureSubRegion = PixelRegions.Instance.GetCaptureSubRegion(regionIndex);

                    UInt32 capturedColour = GetAverageColour(captureSubRegion.Left,
                                                             captureSubRegion.Right,
                                                             captureSubRegion.Top,
                                                             captureSubRegion.Bottom);
                    
                    mOutputManager.SetPixel(regionIndex, UInt32ToColor(capturedColour));
                }
                
                mOutputManager.FlushColours();

                long ticksElapsed = DateTime.Now.Ticks - startTick;
                long msElapsed = ticksElapsed / TimeSpan.TicksPerMillisecond;

                int sleepDuration = (int) (mOutputManager.FadeTimeMs - msElapsed) + 20;

                if(sleepDuration > 0)
                {
                    Thread.Sleep(sleepDuration);
                }
            }

            StopCapturing();
            ShutdownScreenCapture();
        }

        private void CapPadding(ref UInt32 leftPadding, 
                                ref UInt32 rightPadding, 
                                ref UInt32 topPadding, 
                                ref UInt32 bottomPadding)
        {
            if(leftPadding > cMaxPadding)
            {
                leftPadding = cMaxPadding;
            }

            if(rightPadding > cMaxPadding)
            {
                rightPadding = cMaxPadding;
            }

            if(topPadding > cMaxPadding)
            {
                topPadding = cMaxPadding;
            }

            if(bottomPadding > cMaxPadding)
            {
                bottomPadding = cMaxPadding;
            }
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
