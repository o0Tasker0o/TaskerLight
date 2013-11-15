using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.CodeDom.Compiler;

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
            CompilerResults results = ActiveScriptCompiler.CompileScript(new FileInfo("./source.cs"), null);

            Assert.IsNull(results);
            Assert.IsFalse(File.Exists("./script.dll"));
        }

        [TestMethod()]
        public void CompileScriptNullScriptStringTest()
        {
            CompilerResults results = ActiveScriptCompiler.CompileScript((String) null, null);

            Assert.IsNull(results);
            Assert.IsFalse(File.Exists("./script.dll"));
        }

        [TestMethod()]
        public void CompileScriptNullScriptFileTest()
        {
            CompilerResults results = ActiveScriptCompiler.CompileScript((FileInfo) null, null);

            Assert.IsNull(results);
            Assert.IsFalse(File.Exists("./script.dll"));
        }

        [TestMethod()]
        public void CompileScriptCompileBadScriptTest()
        {
            String scriptSource = "THIS IS NOT VALID";

            CompilerResults results = ActiveScriptCompiler.CompileScript(scriptSource,
                                                                         new DirectoryInfo(Directory.GetCurrentDirectory()));

            Assert.IsNotNull(results);
            Assert.AreNotEqual(0, results.Errors.Count);
            Assert.IsFalse(File.Exists("./script.dll"));
        }

        [TestMethod()]
        public void CompileScriptCompileValidScriptFromFileTest()
        {
            File.WriteAllText("./source.cs",
                              "class CompileClass {}");

            CompilerResults results = ActiveScriptCompiler.CompileScript(new FileInfo("./source.cs"), 
                                                                         new DirectoryInfo(Directory.GetCurrentDirectory()));

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Errors.Count);
            Assert.IsTrue(File.Exists("./script.dll"));
        }

        [TestMethod()]
        public void CompileScriptCompileValidScriptTest()
        {
            String scriptSource = "class CompileClass {}";

            CompilerResults results = ActiveScriptCompiler.CompileScript(scriptSource,
                                                                         new DirectoryInfo(Directory.GetCurrentDirectory()));

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Errors.Count);
            Assert.IsTrue(File.Exists("./script.dll"));
        }
    }
}
