using System;
using System.Drawing;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

            using(ColourOutputManager colourOutputManager = new ColourOutputManager(serialCommunicator))
            {
                Assert.IsTrue(serialCommunicator.IsConnected);

                for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Color inputColour = Color.FromArgb((Int32) pixelIndex, 0, 0);
                    colourOutputManager.SetPixel(pixelIndex, inputColour);
                    Assert.AreEqual(inputColour, colourOutputManager.GetPixel(pixelIndex));
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

        [TestMethod()]
        public void ColourOutputManagerOverSaturationTest()
        {
            TestSerialCommunicator serialCommunicator = new TestSerialCommunicator();
            
            using(ColourOutputManager colourOutputManager = new ColourOutputManager(serialCommunicator))
            {
                Assert.AreEqual(1.0f, colourOutputManager.SaturationMultiplier);

                for(UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    colourOutputManager.SetPixel(pixelIndex, Color.FromArgb(255, 128, 128));
                    Assert.AreEqual(Color.FromArgb(255, 128, 128), colourOutputManager.GetPixel(pixelIndex));
                }

                colourOutputManager.FlushColours();

                for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Assert.AreEqual(255, serialCommunicator.OutputBuffer[pixelIndex * 3]);
                    Assert.AreEqual(128, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 1]);
                    Assert.AreEqual(128, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 2]);
                }

                colourOutputManager.SaturationMultiplier = 2.0f;
                Assert.AreEqual(2.0f, colourOutputManager.SaturationMultiplier);

                for(UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    colourOutputManager.SetPixel(pixelIndex, Color.FromArgb(255, 128, 128)); 
                    Assert.AreEqual(Color.FromArgb(255, 1, 1), colourOutputManager.GetPixel(pixelIndex));
                }

                colourOutputManager.FlushColours();

                for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Assert.AreEqual(255, serialCommunicator.OutputBuffer[pixelIndex * 3]);
                    Assert.AreEqual(1, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 1]);
                    Assert.AreEqual(1, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 2]);
                }

                colourOutputManager.SaturationMultiplier = 0.0f;
                Assert.AreEqual(0.0f, colourOutputManager.SaturationMultiplier);

                for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    colourOutputManager.SetPixel(pixelIndex, Color.FromArgb(255, 0, 0));
                    Assert.AreEqual(Color.FromArgb(255, 255, 255), colourOutputManager.GetPixel(pixelIndex));
                }

                colourOutputManager.FlushColours();

                for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Assert.AreEqual(255, serialCommunicator.OutputBuffer[pixelIndex * 3]);
                    Assert.AreEqual(255, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 1]);
                    Assert.AreEqual(255, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 2]);
                }
            }
        }

        [TestMethod()]
        public void ColourOutputManagerOverContrastTest()
        {
            TestSerialCommunicator serialCommunicator = new TestSerialCommunicator();

            using (ColourOutputManager colourOutputManager = new ColourOutputManager(serialCommunicator))
            {
                Assert.AreEqual(1.0f, colourOutputManager.ContrastMultiplier);

                for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    colourOutputManager.SetPixel(pixelIndex, Color.FromArgb(126, 127, 128));
                    Assert.AreEqual(Color.FromArgb(126, 127, 128), colourOutputManager.GetPixel(pixelIndex));
                }

                colourOutputManager.FlushColours();

                for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Assert.AreEqual(126, serialCommunicator.OutputBuffer[pixelIndex * 3]);
                    Assert.AreEqual(127, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 1]);
                    Assert.AreEqual(128, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 2]);
                }

                colourOutputManager.ContrastMultiplier = 2.0f;
                Assert.AreEqual(2.0f, colourOutputManager.ContrastMultiplier);

                for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    colourOutputManager.SetPixel(pixelIndex, Color.FromArgb(126, 127, 128));
                    Assert.AreEqual(Color.FromArgb(125, 127, 129), colourOutputManager.GetPixel(pixelIndex));
                }

                colourOutputManager.FlushColours();

                for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Assert.AreEqual(125, serialCommunicator.OutputBuffer[pixelIndex * 3]);
                    Assert.AreEqual(127, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 1]);
                    Assert.AreEqual(129, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 2]);
                }

                colourOutputManager.ContrastMultiplier = 0.0f;
                Assert.AreEqual(0.0f, colourOutputManager.ContrastMultiplier);

                for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    colourOutputManager.SetPixel(pixelIndex, Color.FromArgb(126, 127, 128)); 
                    Assert.AreEqual(Color.FromArgb(127, 127, 127), colourOutputManager.GetPixel(pixelIndex));
                }

                colourOutputManager.FlushColours();

                for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Assert.AreEqual(127, serialCommunicator.OutputBuffer[pixelIndex * 3]);
                    Assert.AreEqual(127, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 1]);
                    Assert.AreEqual(127, serialCommunicator.OutputBuffer[(pixelIndex * 3) + 2]);
                }
            }
        }
    }
}
