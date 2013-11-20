using System.IO;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControlPanelTests
{
    [TestClass()]
    public class ApplicationFinderTest
    {
        private const String cRunningAppExe = "C:\\Program Files (x86)\\Microsoft Visual Studio 10.0\\Common7\\IDE\\devenv.exe";
        private const String cNonRunningAppExe = "C:\\Windows\\System32\\calc.exe";
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
            Assert.IsTrue(File.Exists(cRunningAppExe));

            ApplicationFinder applicationFinder = new ApplicationFinder();

            Assert.IsTrue(applicationFinder.IsApplicationRunning("devenv"));
        }

        [TestMethod()]
        public void ApplicationFinderWillNotFindNonRunningProcess()
        {
            Assert.IsTrue(File.Exists(cNonRunningAppExe));

            ApplicationFinder applicationFinder = new ApplicationFinder();

            Assert.IsFalse(applicationFinder.IsApplicationRunning("calc"));
        }

        [TestMethod()]
        public void ApplicationFinderCanUnregisterNullExe()
        {
            ApplicationFinder applicationFinder = new ApplicationFinder();

            applicationFinder.UnregisterApplication(null);
        }

        [TestMethod()]
        public void ApplicationFinderCanUnregisterUnknownExe()
        {
            ApplicationFinder applicationFinder = new ApplicationFinder();

            FileInfo calculatorExeFile = new FileInfo(cNonRunningAppExe);

            applicationFinder.UnregisterApplication(calculatorExeFile);
        }

        [TestMethod()]
        public void ApplicationFinderCanFindIfAnyProcessIsRunning()
        {
            Assert.IsTrue(File.Exists(cRunningAppExe));
            Assert.IsTrue(File.Exists(cNonRunningAppExe));

            ApplicationFinder applicationFinder = new ApplicationFinder();

            Assert.IsFalse(applicationFinder.RunningRegisteredApplications());

            FileInfo visualStudioExeFile = new FileInfo(cRunningAppExe);

            applicationFinder.RegisterApplication(visualStudioExeFile);

            Assert.IsTrue(applicationFinder.RunningRegisteredApplications());

            applicationFinder.UnregisterApplication(visualStudioExeFile);

            Assert.IsFalse(applicationFinder.RunningRegisteredApplications());
        }
    }
}
