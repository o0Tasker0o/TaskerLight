using System;
using System.Threading;
using System.Drawing;

namespace ControlPanel
{
    internal abstract class EffectGenerator
    {
        private Thread mTickThread;
        private LEDPreview mLEDPreview;
        protected bool mRunning;
        protected Color[] mOutputColours;

        public EffectGenerator(LEDPreview ledPreview)
        {
            mLEDPreview = ledPreview;

            mOutputColours = new Color[25];
        }
        
        internal void Start()
        {
            if(!mRunning)
            {
                mRunning = true;
                mTickThread = new Thread(ThreadTick);
                mTickThread.Start();
            }
        }

        internal void Stop()
        {
            if (mRunning)
            {
                mRunning = false;
                mTickThread.Join();
            }
        }

        protected abstract void ThreadTick();

        protected void OutputColours()
        {
            if (mRunning)
            {
                for (UInt32 pixelIndex = 0; pixelIndex < SettingsManager.StaticColours.Length; ++pixelIndex)
                {
                    mLEDPreview.SetPixel(mOutputColours[pixelIndex], pixelIndex);
                    OutputManager.SetLED(mOutputColours[pixelIndex],
                                            pixelIndex,
                                            SettingsManager.Saturation,
                                            SettingsManager.Contrast);
                }

                OutputManager.FlushColours();
            }
        }
    }
}
