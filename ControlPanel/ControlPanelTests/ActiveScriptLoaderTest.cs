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
        const String mScriptName = "testScript";
        AppDomain mScriptAppDomain;

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
            String scriptSource = "using System;" +
                                  "class TestScript" +
                                  "{" +
                                  "    public static Int32 TestFunction()" +
                                  "    {" +
                                  "        return 0;" +
                                  "    }" +
                                  "}";

            CompileScript(scriptSource);

            ActiveScriptLoader scriptLoader = CreateActiveScriptLoader();

            scriptLoader.LoadAssembly("./" + mScriptName + ".dll");

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
            String scriptSource = "using System;" +
                                  "class TestScript" +
                                  "{" +
                                  "    public static Int32 TestFunction()" +
                                  "    {" +
                                  "        return 0;" +
                                  "    }" +
                                  "}";

            CompileScript(scriptSource);

            ActiveScriptLoader scriptLoader = CreateActiveScriptLoader();

            scriptLoader.LoadAssembly("./" + mScriptName + ".dll");

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

            CompileScript(scriptSource);

            ActiveScriptLoader scriptLoader = CreateActiveScriptLoader();

            scriptLoader.LoadAssembly("./" + mScriptName + ".dll");

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

        private void CompileScript(String sourceCode)
        {
            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                // Create some parameters for the compiler
                CompilerParameters compilerParameters = new CompilerParameters();
                compilerParameters.OutputAssembly = "./" + mScriptName + ".dll";
                compilerParameters.GenerateExecutable = false;
                compilerParameters.GenerateInMemory = false;

                compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
                
                CompilerResults results = compiler.CompileAssemblyFromSource(compilerParameters, sourceCode);
            }
        }
    }
}
