﻿using System.IO;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControlPanelTests
{
    [TestClass()]
    public class ApplicationFinderTest
    {
        [TestMethod()]
        public void ApplicationFinderFindsVisualStudio()
        {
            ApplicationFinder applicationFinder = new ApplicationFinder();

            Assert.IsTrue(applicationFinder.IsApplicationRunning("devenv"));
        }

        [TestMethod()]
        public void ApplicationFinderDoesNotFindNonExistantApp()
        {
            ApplicationFinder applicationFinder = new ApplicationFinder();

            Assert.IsFalse(applicationFinder.IsApplicationRunning("ThisIsNotAnApplication"));
        }
        
        [TestMethod()]
        public void ApplicationFinderCanFindProcessFromExe()
        {
            Assert.IsTrue(File.Exists("C:\\Program Files (x86)\\Microsoft Visual Studio 10.0\\Common7\\IDE\\devenv.exe"));

            ApplicationFinder applicationFinder = new ApplicationFinder();

            Assert.IsFalse(applicationFinder.RunningRegisteredApplications());

            FileInfo visualStudioExeFile = new FileInfo("C:\\Program Files (x86)\\Microsoft Visual Studio 10.0\\Common7\\IDE\\devenv.exe");

            applicationFinder.RegisterApplication(visualStudioExeFile);

            Assert.IsTrue(applicationFinder.RunningRegisteredApplications());
        }

        [TestMethod()]
        public void ApplicationFinderWillNotFindNonRunningProcess()
        {
            Assert.IsTrue(File.Exists("C:\\Windows\\System32\\calc.exe"));

            ApplicationFinder applicationFinder = new ApplicationFinder();

            Assert.IsFalse(applicationFinder.RunningRegisteredApplications());

            FileInfo controlPanelExeFile = new FileInfo("C:\\Windows\\System32\\calc.exe");

            applicationFinder.RegisterApplication(controlPanelExeFile);

            Assert.IsFalse(applicationFinder.RunningRegisteredApplications());
        }
    }
}