using System;
using System.Drawing;
using System.Linq;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace ControlPanelTests
{
    [TestClass()]
    public class ColourOutputManagerTest
    {
        private static ISerialCommunicator mSerialCommunicator;

        [ClassInitialize()]
        public static void Initialise(TestContext context)
        {
            mSerialCommunicator = Substitute.For<ISerialCommunicator>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            mSerialCommunicator.ClearReceivedCalls();
        }

        [TestMethod()]
        public void ConnectsOnConstruction()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);

            mSerialCommunicator.Received(1).Connect();
        }

        [TestMethod()]
        public void HasDefaultProperties()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);

            Assert.AreEqual(500, colourOutputManager.FadeTimeMs);
            Assert.AreEqual(1.0f, colourOutputManager.ContrastMultiplier);
            Assert.AreEqual(1.0f, colourOutputManager.SaturationMultiplier);
        }

        [TestMethod()]
        public void FlushWritesDefaultPixelArrayToBuffer()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);
            colourOutputManager.FlushColours();

            byte[] fadeTimeBytes = BitConverter.GetBytes(colourOutputManager.FadeTimeMs);

            byte[] outputPixels = new byte[77];
            outputPixels[75] = fadeTimeBytes[0];
            outputPixels[76] = fadeTimeBytes[1];

            mSerialCommunicator.Received(1).Write(Arg.Is<byte[]>(pix => outputPixels.SequenceEqual(pix)));
        }

        [TestMethod()]
        public void FlushReadsHandshakeResponse()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);
            colourOutputManager.FlushColours();

            mSerialCommunicator.Received(1).Read();
        }

        [TestMethod()]
        public void FlushWritesLastSetPixelsToBuffer()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);
            colourOutputManager.SetPixel(0, Color.Red);
            colourOutputManager.SetPixel(1, Color.Lime);
            colourOutputManager.SetPixel(2, Color.Blue);
            colourOutputManager.FlushColours();

            byte[] fadeTimeBytes = BitConverter.GetBytes(colourOutputManager.FadeTimeMs);

            byte[] outputPixels = new byte[77];
            outputPixels[0] = 255;
            outputPixels[1] = 0;
            outputPixels[2] = 0;

            outputPixels[3] = 0;
            outputPixels[4] = 255;
            outputPixels[5] = 0;

            outputPixels[6] = 0;
            outputPixels[7] = 0;
            outputPixels[8] = 255;

            outputPixels[75] = fadeTimeBytes[0];
            outputPixels[76] = fadeTimeBytes[1];

            mSerialCommunicator.Received(1).Write(Arg.Is<byte[]>(pix => outputPixels.SequenceEqual(pix)));
        }

        [TestMethod()]
        public void SetPixelOutsideOfRangeHasNoEffect()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);
            colourOutputManager.SetPixel(25, Color.Red);
            colourOutputManager.FlushColours();

            byte[] fadeTimeBytes = BitConverter.GetBytes(colourOutputManager.FadeTimeMs);

            byte[] outputPixels = new byte[77];

            outputPixels[75] = fadeTimeBytes[0];
            outputPixels[76] = fadeTimeBytes[1];

            mSerialCommunicator.Received(1).Write(Arg.Is<byte[]>(pix => outputPixels.SequenceEqual(pix)));
        }

        [TestMethod()]
        public void GetPixelReturnsSpecifiedPixelColour()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);

            Color setColour = Color.FromArgb(255, 255, 255);

            for (uint pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                colourOutputManager.SetPixel(pixelIndex, setColour);
                Assert.AreEqual(setColour, colourOutputManager.GetPixel(pixelIndex));
            }
        }

        [TestMethod()]
        public void GetPixelOutsideOfBoundsReturnsBlack()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);
            Assert.AreEqual(Color.Black, colourOutputManager.GetPixel(25));
        }

        [TestMethod()]
        public void GetPixelReturnsContrastAdjustedColour()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);

            colourOutputManager.ContrastMultiplier = 0.0f;

            Assert.AreEqual(Color.FromArgb(127, 127, 127), colourOutputManager.GetPixel(0));
        }

        [TestMethod()]
        public void GetPixelReturnsBrightnessColour()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);

            colourOutputManager.SaturationMultiplier = 0.0f;

            colourOutputManager.SetPixel(0, Color.Red);

            Assert.AreEqual(Color.FromArgb(255, 255, 255), colourOutputManager.GetPixel(0));
        }

        [TestMethod()]
        public void TurnLightsOffSetsAllPixelsToBlack()
        {
            ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator);

            for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                colourOutputManager.SetPixel(25, Color.White);
            }

            colourOutputManager.FlushColours();

            mSerialCommunicator.ClearReceivedCalls();

            colourOutputManager.TurnLightsOff();

            byte[] fadeTimeBytes = BitConverter.GetBytes(colourOutputManager.FadeTimeMs);

            byte[] outputPixels = new byte[77];

            outputPixels[75] = fadeTimeBytes[0];
            outputPixels[76] = fadeTimeBytes[1];

            mSerialCommunicator.Received(1).Write(Arg.Is<byte[]>(pix => outputPixels.SequenceEqual(pix)));
        }

        [TestMethod()]
        public void DisconnectsOnDispose()
        {
            using (ColourOutputManager colourOutputManager = new ColourOutputManager(mSerialCommunicator)) { }

            mSerialCommunicator.Received(1).Disconnect();
        }
    }
}
