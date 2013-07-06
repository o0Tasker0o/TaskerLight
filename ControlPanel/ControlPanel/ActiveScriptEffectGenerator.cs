using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Security.Policy;
using System.Drawing;

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
            CurrentScriptDirectory = new DirectoryInfo("./");
            colourOutputManager.FadeTimeMs = 200;
        }

        protected override void ThreadTick()
        {
            AppDomainSetup appSetup = new AppDomainSetup();
            appSetup.ApplicationBase = Directory.GetCurrentDirectory();

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
