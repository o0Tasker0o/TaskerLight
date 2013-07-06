using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Policy;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace ControlPanelTests
{
    [TestClass()]
    public class ActiveScriptLoaderTest
    {
        AppDomain mScriptAppDomain;

        private const String cScriptSource = "using System;" +
                                             "class TestScript" +
                                             "{" +
                                             "    public static Int32 TestFunction()" +
                                             "    {" +
                                             "        return 0;" +
                                             "    }" +
                                             "}";

        [TestInitialize()]
        public void Initialize()
        {
            AppDomainSetup appSetup = new AppDomainSetup();
            appSetup.ApplicationBase = Directory.GetCurrentDirectory(); ;

            // Set up the Evidence
            Evidence baseEvidence = AppDomain.CurrentDomain.Evidence; 
            Evidence evidence = new Evidence(baseEvidence);

            mScriptAppDomain = AppDomain.CreateDomain("TestScriptDomain",
                                                      evidence,
                                                      appSetup);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            if(null != mScriptAppDomain)
            {
                try
                {
                    //Unplug our active script DLL
                    AppDomain.Unload(mScriptAppDomain);
                }
                catch (AppDomainUnloadedException)
                {
                }
            }
        }

        [TestMethod()]
        public void ActiveScriptLoaderLoadNonExistantScriptName()
        {
            ActiveScriptLoader scriptLoader = CreateActiveScriptLoader();

            scriptLoader.LoadAssembly("./THISDOESNOTEXIST.dll");

            Assert.IsNull(scriptLoader.ExecuteStaticMethod("", "", null));
        }

        [TestMethod()]
        public void ActiveScriptLoaderLoadBadScriptName()
        {
            ActiveScriptLoader scriptLoader = CreateActiveScriptLoader();

            File.WriteAllText("./testScript.dll", "THIS IS BAD CONTENTS");

            scriptLoader.LoadAssembly("./testScript.dll");

            Assert.IsNull(scriptLoader.ExecuteStaticMethod("", "", null));
        }
        
        [TestMethod()]
        public void ActiveScriptLoaderExecuteStaticMethodWithBadParametersTest()
        {
            ActiveScriptCompiler.CompileScript(cScriptSource, new DirectoryInfo("./"));

            ActiveScriptLoader scriptLoader = CreateActiveScriptLoader();

            scriptLoader.LoadAssembly("./script.dll");

            Assert.IsNull(scriptLoader.ExecuteStaticMethod("BADTYPENAME",
                                                           "TestFunction",
                                                           null));

            Assert.IsNull(scriptLoader.ExecuteStaticMethod("TestScript",
                                                           "BADFUNCTIONNAME",
                                                           null));

            Assert.IsNull(scriptLoader.ExecuteStaticMethod("BADTYPENAME",
                                                           "BADFUNCTIONNAME",
                                                           null));
        }

        [TestMethod()]
        public void ActiveScriptLoaderExecuteStaticMethodNoParametersTest()
        {
            ActiveScriptCompiler.CompileScript(cScriptSource, new DirectoryInfo("./"));

            ActiveScriptLoader scriptLoader = CreateActiveScriptLoader();

            scriptLoader.LoadAssembly("./script.dll");

            Int32 result = (Int32) scriptLoader.ExecuteStaticMethod("TestScript",
                                                                    "TestFunction",
                                                                    null);

            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void ActiveScriptLoaderExecuteStaticMethodWithParametersTest()
        {
            String scriptSource = "using System;" +
                                  "class TestScript" +
                                  "{" +
                                  "    public static Int32 TestFunction(Int32 param)" +
                                  "    {" +
                                  "        return param;" +
                                  "    }" +
                                  "}";

            ActiveScriptCompiler.CompileScript(scriptSource, new DirectoryInfo("./"));

            ActiveScriptLoader scriptLoader = CreateActiveScriptLoader();

            scriptLoader.LoadAssembly("./script.dll");

            Int32 inputParameter = 123;
            Object[] parameterArray = new Object[] { inputParameter };
            Int32 result = (Int32) scriptLoader.ExecuteStaticMethod("TestScript",
                                                                    "TestFunction",
                                                                    parameterArray);

            Assert.AreEqual(inputParameter, result);
        }

        private ActiveScriptLoader CreateActiveScriptLoader()
        {
            return (ActiveScriptLoader)mScriptAppDomain.CreateInstanceAndUnwrap(
                                                    typeof(ActiveScriptLoader).Assembly.FullName,
                                                    typeof(ActiveScriptLoader).FullName);
        }
    }
}
