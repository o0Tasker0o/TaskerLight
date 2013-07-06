using System;
using System.Threading;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControlPanelTests
{
    [TestClass()]
    public class VideoEffectGeneratorTest
    {
        [TestMethod()]
        public void VideoEffectGeneratorConstructorTest()
        {
            //Video capture DLL needs to be deployed with this test
            /*
            TestSerialCommunicator testSerialCommunicator = new TestSerialCommunicator();

            using(ColourOutputManager colourOutputManager = new ColourOutputManager(testSerialCommunicator))
            {
                VideoEffectGenerator wallpaperGenerator = new VideoEffectGenerator(colourOutputManager);

                CollectionAssert.AreEqual(new byte[77], testSerialCommunicator.OutputBuffer);
                
                wallpaperGenerator.Start();

                Thread.Sleep(50);

                byte[] fadeBytes = BitConverter.GetBytes((UInt16) 90);
                
                Assert.AreEqual(fadeBytes[0], testSerialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeBytes[1], testSerialCommunicator.OutputBuffer[76]);
                
                wallpaperGenerator.Stop();
            }*/
        }
    }
}
