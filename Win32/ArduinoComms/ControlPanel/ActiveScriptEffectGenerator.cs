using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace ControlPanel
{
    class ActiveScriptEffectGenerator : EffectGenerator
    {
        private ScriptLoader mScriptLoader;
        private String mScriptDirectory;
        private Int64 mInitialMS;

        public ActiveScriptEffectGenerator(LEDPreview ledPreview) : base(ledPreview)
        {
            mScriptLoader = new ScriptLoader();
        }

        internal void SetCurrentScriptDirectory(String scriptDirectory)
        {
            mScriptDirectory = scriptDirectory;
        }

        protected override void ThreadTick()
        {
            //In order to load and unload the script DLLs at runtime,
            //the DLLs need to be loaded into a seperate appdomain...
            //which we create here
            AppDomain scriptAppDomain = AppDomain.CreateDomain("TaskerLightScriptDomain");

            //Create an instance of the script loader which will give access to the script dll
            mScriptLoader = (ScriptLoader)scriptAppDomain.CreateInstanceAndUnwrap(
                                                typeof(ScriptLoader).Assembly.FullName,
                                                typeof(ScriptLoader).FullName);

            //Load the script DLL into the appdomain
            mScriptLoader.LoadAssembly(mScriptDirectory + "\\script.dll");

            //All the scripts use "number of ticks passed" to time their effects
            //To do this they need to know the number of ticks that represents
            //the time at which they started
            mInitialMS = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            while (mRunning)
            {
                long millisecondDifference = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                millisecondDifference -= mInitialMS;

                mOutputColours = (Color[])mScriptLoader.ExecuteStaticMethod("TaskerLightScript",
                                                                            "TickLighting",
                                                                            millisecondDifference);

                OutputColours();

                mWaitEvent.WaitOne(200);
            }
            
            //If an appdomain has been created containing the active script
            if (null != scriptAppDomain)
            {
                try
                {
                    //Unplug our active script DLL
                    AppDomain.Unload(scriptAppDomain);
                }
                catch (AppDomainUnloadedException)
                {

                }
            }
        }
    }
}
