using System;
using System.Drawing;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControlPanelTests
{
    [TestClass()]
    public class ColourOutputManagerTest
    {
        [TestMethod()]
        public void ColourOutputManagerWriteColoursTest()
        {
            TestSerialCommunicator serialCommunicator = new TestSerialCommunicator();
            Assert.IsFalse(serialCommunicator.IsConnected);

            ColourOutputManager colourOutputManager;

            using(colourOutputManager = new ColourOutputManager(serialCommunicator))
            {
                Assert.IsTrue(serialCommunicator.IsConnected);

                for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    colourOutputManager.SetPixel(pixelIndex, Color.FromArgb((Int32) pixelIndex, 0, 0));
                }

                CollectionAssert.AreEqual(new byte[77], serialCommunicator.OutputBuffer);
                Assert.AreEqual(500, colourOutputManager.FadeTimeMs);

                Assert.AreEqual(0U, serialCommunicator.ReadAmount);

                colourOutputManager.FadeTimeMs = 123;

                colourOutputManager.FlushColours();

                for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Assert.AreEqual(pixelIndex, serialCommunicator.OutputBuffer[pixelIndex * 3]);
                    Assert.AreEqual(0, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 1]);
                    Assert.AreEqual(0, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 2]);
                }

                byte [] fadeTimeBytes = BitConverter.GetBytes(colourOutputManager.FadeTimeMs);

                Assert.AreEqual(fadeTimeBytes[0], serialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeTimeBytes[1], serialCommunicator.OutputBuffer[76]);
                Assert.AreEqual(1U, serialCommunicator.ReadAmount);
            }

            Assert.IsFalse(serialCommunicator.IsConnected);
        }

        [TestMethod()]
        public void ColourOutputManagerBadIndexTest()
        {
            TestSerialCommunicator serialCommunicator = new TestSerialCommunicator();

            using(ColourOutputManager colourOutputManager = new ColourOutputManager(serialCommunicator))
            {
                colourOutputManager.SetPixel(26, Color.Black);

                CollectionAssert.AreEqual(new byte[77], serialCommunicator.OutputBuffer);
                Assert.AreEqual(0U, serialCommunicator.ReadAmount);
            }
        }
    }
}
