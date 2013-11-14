using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;

namespace ControlPanelTests
{
    [TestClass()]
    public class ActiveScriptBrowserTest
    {
        private const String cScriptDir = "./TestScriptDir1";
        [TestCleanup()]
        public void Cleanup()
        {
            try
            {
                Directory.Delete(cScriptDir, true);
            }
            catch(DirectoryNotFoundException)
            {

            }
        }

        [TestMethod()]
        public void ActiveScriptBrowserListsDllDirectories()
        {
            ActiveScriptBrowser scriptBrowser = new ActiveScriptBrowser();
            
            Int32 scriptChanges = 0;

            scriptBrowser.ScriptsChanged += delegate()
            {
                scriptChanges++;
            };

            Assert.AreEqual(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName, scriptBrowser.RootDirectory.FullName);
            Assert.AreEqual(0, scriptBrowser.ScriptDirectories.Count);
            Assert.AreEqual(0, scriptChanges);

            Directory.CreateDirectory(cScriptDir);

            Assert.AreEqual(0, scriptBrowser.ScriptDirectories.Count);
            System.Threading.Thread.Sleep(50);
            Assert.AreEqual(1, scriptChanges);

            using(FileStream emptyStream = File.Create(Path.Combine(cScriptDir, "script.dll"))) { }

            Assert.AreEqual(1, scriptBrowser.ScriptDirectories.Count);
            System.Threading.Thread.Sleep(50);
            Assert.AreEqual(2, scriptChanges);
            Assert.AreEqual(new DirectoryInfo(cScriptDir).FullName, scriptBrowser.ScriptDirectories[0].FullName);

            Directory.Delete(cScriptDir, true);
            Assert.AreEqual(0, scriptBrowser.ScriptDirectories.Count);
            System.Threading.Thread.Sleep(50);
            Assert.AreEqual(4, scriptChanges);  //Deleting a directory is a change plus a delete
        }

        [TestMethod()]
        public void ActiveScriptBrowserDoesNotListDirectoriesWithOtherFiles()
        {
            ActiveScriptBrowser scriptBrowser = new ActiveScriptBrowser();

            Directory.CreateDirectory(cScriptDir);

            Assert.AreEqual(0, scriptBrowser.ScriptDirectories.Count);

            using (FileStream emptyStream = File.Create(Path.Combine(cScriptDir, "script.cs"))) { }

            Assert.AreEqual(0, scriptBrowser.ScriptDirectories.Count);

            Directory.Delete(cScriptDir, true);
            Assert.AreEqual(0, scriptBrowser.ScriptDirectories.Count);
        }

        [TestMethod()]
        public void ActiveScriptBrowserRootCannotBeSetToNull()
        {
            ActiveScriptBrowser scriptBrowser = new ActiveScriptBrowser();

            Assert.AreEqual(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName, scriptBrowser.RootDirectory.FullName);

            scriptBrowser.RootDirectory = null;

            Assert.AreEqual(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName, scriptBrowser.RootDirectory.FullName);
        }
    }
}
