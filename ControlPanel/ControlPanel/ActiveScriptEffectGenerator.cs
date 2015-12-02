using System;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Threading;

namespace ControlPanel
{
    public class ActiveScriptEffectGenerator : EffectGenerator
    {
        public DirectoryInfo CurrentScriptDirectory
        {
            get;
            set;
        }

        public ActiveScriptEffectGenerator(ColourOutputManager colourOutputManager)
            : base(colourOutputManager)
        {
            CurrentScriptDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        }

        protected override void ThreadTick()
        {
            mOutputManager.FadeTimeMs = 180;

            AppDomainSetup appSetup = new AppDomainSetup();
            appSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

            AppDomain scriptAppDomain = AppDomain.CreateDomain("TaskerLightScriptDomain",
                                                               new Evidence(AppDomain.CurrentDomain.Evidence),
                                                               appSetup);

            ActiveScriptLoader scriptLoader = (ActiveScriptLoader)scriptAppDomain.CreateInstanceAndUnwrap(
                                                    typeof(ActiveScriptLoader).Assembly.FullName,
                                                    typeof(ActiveScriptLoader).FullName);

            scriptLoader.LoadAssembly(Path.Combine(CurrentScriptDirectory.FullName, "script.dll"));
            
            long initialMS = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            while (mRunning)
            {
                long millisecondDifference = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                millisecondDifference -= initialMS;

                Color[] outputPixelColours = (Color []) scriptLoader.ExecuteStaticMethod("TaskerLightScript",
                                                                                         "TickLighting",
                                                                                         new object[] { millisecondDifference });

                if(null != outputPixelColours)
                {
                    for(UInt16 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                    {
                        mOutputManager.SetPixel(pixelIndex, outputPixelColours[pixelIndex]);
                    }
                }

                mOutputManager.FlushColours();

                Thread.Sleep(mOutputManager.FadeTimeMs);
            }

            if(null != scriptAppDomain)
            {
                try
                {
                    AppDomain.Unload(scriptAppDomain);
                }
                catch (AppDomainUnloadedException)
                {
                }
            }
        }
    }
}
