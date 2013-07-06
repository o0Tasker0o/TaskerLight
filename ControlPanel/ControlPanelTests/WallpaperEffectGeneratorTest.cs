using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace ControlPanelTests
{
    [TestClass()]
    public class WallpaperEffectGeneratorTest
    {
        [TestMethod()]
        [DeploymentItem("ControlPanel.exe")]
        public void WallpaperEffectGeneratorConstructorTest()
        {
            TestSerialCommunicator testSerialCommunicator = new TestSerialCommunicator();

            using(ColourOutputManager colourOutputManager = new ColourOutputManager(testSerialCommunicator))
            {
                WallpaperEffectGenerator wallpaperGenerator = new WallpaperEffectGenerator(colourOutputManager);

                CollectionAssert.AreEqual(new byte[77], testSerialCommunicator.OutputBuffer);

                wallpaperGenerator.Start();

                Thread.Sleep(100);

                byte[] fadeBytes = BitConverter.GetBytes((UInt16) 1000);

                Assert.AreEqual(fadeBytes[0], testSerialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeBytes[1], testSerialCommunicator.OutputBuffer[76]);

                wallpaperGenerator.Stop();
            }
        }
    }
}
