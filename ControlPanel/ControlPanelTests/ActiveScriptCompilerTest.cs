using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ControlPanelTests
{
    [TestClass()]
    public class ActiveScriptCompilerTest
    {
        [TestCleanup()]
        public void Cleanup()
        {
            File.Delete("./script.dll");
        }

        [TestMethod()]
        public void CompileScriptNullTargetTest()
        {
            ActiveScriptCompiler.CompileScript(new FileInfo("./source.cs"), null);

            Assert.IsFalse(File.Exists("./script.dll"));
        }

        [TestMethod()]
        public void CompileScriptCompileBadScriptTest()
        {
            String scriptSource = "THIS IS NOT VALID";

            ActiveScriptCompiler.CompileScript(scriptSource,
                                               new DirectoryInfo(Directory.GetCurrentDirectory()));

            Assert.IsFalse(File.Exists("./script.dll"));
        }

        [TestMethod()]
        public void CompileScriptCompileValidScriptFromFileTest()
        {
            File.WriteAllText("./source.cs",
                              "class CompileClass {}");

            ActiveScriptCompiler.CompileScript(new FileInfo("./source.cs"), 
                                               new DirectoryInfo(Directory.GetCurrentDirectory()));

            Assert.IsTrue(File.Exists("./script.dll"));
        }

        [TestMethod()]
        public void CompileScriptCompileValidScriptTest()
        {
            String scriptSource = "class CompileClass {}";

            ActiveScriptCompiler.CompileScript(scriptSource,
                                               new DirectoryInfo(Directory.GetCurrentDirectory()));

            Assert.IsTrue(File.Exists("./script.dll"));
        }
    }
}
