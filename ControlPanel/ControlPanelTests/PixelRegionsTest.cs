using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace ControlPanelTests
{
    [TestClass()]
    public class PixelRegionsTest
    {
        [TestMethod()]
        public void PixelRegionsIsSingletonTest()
        {
            PixelRegions pixelRegions1 = PixelRegions.Instance;
            PixelRegions pixelRegions2 = PixelRegions.Instance;

            Assert.AreEqual(pixelRegions1, pixelRegions2);
        }

        [TestMethod()]
        public void PixelRegionsInitiallyHas25RegionsTest()
        {
            PixelRegions pixelRegions = PixelRegions.Instance;

            Assert.AreEqual(25, pixelRegions.RegionCount);
            
            Assert.AreEqual(new RectangleF(0.648f, 0.653f, 0.105f, 0.104f), pixelRegions.GetRegion(0));
            Assert.AreEqual(new RectangleF(0.748f, 0.653f, 0.105f, 0.104f), pixelRegions.GetRegion(1));
            Assert.AreEqual(new RectangleF(0.849f, 0.653f, 0.105f, 0.104f), pixelRegions.GetRegion(2));
            Assert.AreEqual(new RectangleF(0.849f, 0.553f, 0.105f, 0.104f), pixelRegions.GetRegion(3));
            Assert.AreEqual(new RectangleF(0.849f, 0.453f, 0.105f, 0.104f), pixelRegions.GetRegion(4));
            Assert.AreEqual(new RectangleF(0.849f, 0.353f, 0.105f, 0.104f), pixelRegions.GetRegion(5));
            Assert.AreEqual(new RectangleF(0.849f, 0.253f, 0.105f, 0.104f), pixelRegions.GetRegion(6));
            Assert.AreEqual(new RectangleF(0.849f, 0.153f, 0.105f, 0.104f), pixelRegions.GetRegion(7));
            Assert.AreEqual(new RectangleF(0.849f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(8));
            Assert.AreEqual(new RectangleF(0.748f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(9));
            Assert.AreEqual(new RectangleF(0.648f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(10));
            Assert.AreEqual(new RectangleF(0.547f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(11));
            Assert.AreEqual(new RectangleF(0.447f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(12));
            Assert.AreEqual(new RectangleF(0.346f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(13));
            Assert.AreEqual(new RectangleF(0.246f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(14));
            Assert.AreEqual(new RectangleF(0.145f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(15));
            Assert.AreEqual(new RectangleF(0.045f, 0.051f, 0.105f, 0.104f), pixelRegions.GetRegion(16));
            Assert.AreEqual(new RectangleF(0.045f, 0.153f, 0.105f, 0.104f), pixelRegions.GetRegion(17));
            Assert.AreEqual(new RectangleF(0.045f, 0.253f, 0.105f, 0.104f), pixelRegions.GetRegion(18));
            Assert.AreEqual(new RectangleF(0.045f, 0.353f, 0.105f, 0.104f), pixelRegions.GetRegion(19));
            Assert.AreEqual(new RectangleF(0.045f, 0.453f, 0.105f, 0.104f), pixelRegions.GetRegion(20));
            Assert.AreEqual(new RectangleF(0.045f, 0.553f, 0.105f, 0.104f), pixelRegions.GetRegion(21));
            Assert.AreEqual(new RectangleF(0.045f, 0.653f, 0.105f, 0.104f), pixelRegions.GetRegion(22));
            Assert.AreEqual(new RectangleF(0.145f, 0.653f, 0.105f, 0.104f), pixelRegions.GetRegion(23));
            Assert.AreEqual(new RectangleF(0.246f, 0.653f, 0.105f, 0.104f), pixelRegions.GetRegion(24));
        }

        [TestMethod()]
        public void PixelRegionsReturnNullForBadIndicesTest()
        {
            PixelRegions pixelRegions = PixelRegions.Instance;

            Assert.AreEqual(25, pixelRegions.RegionCount);

            Assert.AreEqual(new Rectangle(0, 0, 0, 0), pixelRegions.GetRegion(25));
        }

        [TestMethod()]
        public void PixelRegionsHasDefaultCaptureRegion()
        {
            PixelRegions pixelRegions = PixelRegions.Instance;

            //Test fails because PixelRegions is a singleton
            //Assert.AreEqual(1920, 1080, pixelRegions.CaptureRegion);

            pixelRegions.CaptureRegion = new Rectangle(0, 0, 1920, 1080);

            Assert.AreEqual(new Rectangle(1280, 925, 213, 154), pixelRegions.GetCaptureSubRegion(0));
            Assert.AreEqual(new Rectangle(1493, 925, 213, 154), pixelRegions.GetCaptureSubRegion(1));
            Assert.AreEqual(new Rectangle(1706, 925, 213, 154), pixelRegions.GetCaptureSubRegion(2));
            Assert.AreEqual(new Rectangle(1706, 771, 213, 154), pixelRegions.GetCaptureSubRegion(3));
            Assert.AreEqual(new Rectangle(1706, 617, 213, 154), pixelRegions.GetCaptureSubRegion(4));
            Assert.AreEqual(new Rectangle(1706, 462, 213, 154), pixelRegions.GetCaptureSubRegion(5));
            Assert.AreEqual(new Rectangle(1706, 308, 213, 154), pixelRegions.GetCaptureSubRegion(6));
            Assert.AreEqual(new Rectangle(1706, 153, 213, 154), pixelRegions.GetCaptureSubRegion(7));
            Assert.AreEqual(new Rectangle(1706, 0, 213, 154), pixelRegions.GetCaptureSubRegion(8));
            Assert.AreEqual(new Rectangle(1493, 0, 213, 154), pixelRegions.GetCaptureSubRegion(9));
            Assert.AreEqual(new Rectangle(1280, 0, 213, 154), pixelRegions.GetCaptureSubRegion(10));
            Assert.AreEqual(new Rectangle(1065, 0, 213, 154), pixelRegions.GetCaptureSubRegion(11));
            Assert.AreEqual(new Rectangle(852, 0, 213, 154), pixelRegions.GetCaptureSubRegion(12));
            Assert.AreEqual(new Rectangle(639, 0, 213, 154), pixelRegions.GetCaptureSubRegion(13));
            Assert.AreEqual(new Rectangle(426, 0, 213, 154), pixelRegions.GetCaptureSubRegion(14));
            Assert.AreEqual(new Rectangle(213, 0, 213, 154), pixelRegions.GetCaptureSubRegion(15));
            Assert.AreEqual(new Rectangle(0, 0, 213, 154), pixelRegions.GetCaptureSubRegion(16));
            Assert.AreEqual(new Rectangle(0, 153, 213, 154), pixelRegions.GetCaptureSubRegion(17));
            Assert.AreEqual(new Rectangle(0, 308, 213, 154), pixelRegions.GetCaptureSubRegion(18));
            Assert.AreEqual(new Rectangle(0, 462, 213, 154), pixelRegions.GetCaptureSubRegion(19));
            Assert.AreEqual(new Rectangle(0, 617, 213, 154), pixelRegions.GetCaptureSubRegion(20));
            Assert.AreEqual(new Rectangle(0, 771, 213, 154), pixelRegions.GetCaptureSubRegion(21));
            Assert.AreEqual(new Rectangle(0, 925, 213, 154), pixelRegions.GetCaptureSubRegion(22));
            Assert.AreEqual(new Rectangle(213, 925, 213, 154), pixelRegions.GetCaptureSubRegion(23));
            Assert.AreEqual(new Rectangle(426, 925, 213, 154), pixelRegions.GetCaptureSubRegion(24));
        }

        [TestMethod()]
        public void PixelRegionsCanScaleCaptureRegion()
        {
            PixelRegions pixelRegions = PixelRegions.Instance;

            pixelRegions.CaptureRegion = new Rectangle(0, 0, 18, 14);

            Assert.AreEqual(new Rectangle(0, 0, 18, 14), pixelRegions.CaptureRegion);

            Assert.AreEqual(new Rectangle(12, 11, 1, 2), pixelRegions.GetCaptureSubRegion(0));
            Assert.AreEqual(new Rectangle(14, 11, 1, 2), pixelRegions.GetCaptureSubRegion(1));
            Assert.AreEqual(new Rectangle(16, 11, 1, 2), pixelRegions.GetCaptureSubRegion(2));
            Assert.AreEqual(new Rectangle(16, 9, 1, 2), pixelRegions.GetCaptureSubRegion(3));
            Assert.AreEqual(new Rectangle(16, 8, 1, 2), pixelRegions.GetCaptureSubRegion(4));
            Assert.AreEqual(new Rectangle(16, 5, 1, 2), pixelRegions.GetCaptureSubRegion(5));
            Assert.AreEqual(new Rectangle(16, 4, 1, 2), pixelRegions.GetCaptureSubRegion(6));
            Assert.AreEqual(new Rectangle(16, 1, 1, 2), pixelRegions.GetCaptureSubRegion(7));
            Assert.AreEqual(new Rectangle(16, 0, 1, 2), pixelRegions.GetCaptureSubRegion(8));
            Assert.AreEqual(new Rectangle(14, 0, 1, 2), pixelRegions.GetCaptureSubRegion(9));
            Assert.AreEqual(new Rectangle(12, 0, 1, 2), pixelRegions.GetCaptureSubRegion(10));
            Assert.AreEqual(new Rectangle(9, 0, 1, 2), pixelRegions.GetCaptureSubRegion(11));
            Assert.AreEqual(new Rectangle(7, 0, 1, 2), pixelRegions.GetCaptureSubRegion(12));
            Assert.AreEqual(new Rectangle(5, 0, 1, 2), pixelRegions.GetCaptureSubRegion(13));
            Assert.AreEqual(new Rectangle(3, 0, 1, 2), pixelRegions.GetCaptureSubRegion(14));
            Assert.AreEqual(new Rectangle(1, 0, 1, 2), pixelRegions.GetCaptureSubRegion(15));
            Assert.AreEqual(new Rectangle(0, 0, 1, 2), pixelRegions.GetCaptureSubRegion(16));
            Assert.AreEqual(new Rectangle(0, 1, 1, 2), pixelRegions.GetCaptureSubRegion(17));
            Assert.AreEqual(new Rectangle(0, 4, 1, 2), pixelRegions.GetCaptureSubRegion(18));
            Assert.AreEqual(new Rectangle(0, 5, 1, 2), pixelRegions.GetCaptureSubRegion(19));
            Assert.AreEqual(new Rectangle(0, 8, 1, 2), pixelRegions.GetCaptureSubRegion(20));
            Assert.AreEqual(new Rectangle(0, 9, 1, 2), pixelRegions.GetCaptureSubRegion(21));
            Assert.AreEqual(new Rectangle(0, 11, 1, 2), pixelRegions.GetCaptureSubRegion(22));
            Assert.AreEqual(new Rectangle(1, 11, 1, 2), pixelRegions.GetCaptureSubRegion(23));
            Assert.AreEqual(new Rectangle(3, 11, 1, 2), pixelRegions.GetCaptureSubRegion(24));
        }

        [TestMethod()]
        public void PixelRegionsCanScaleAndTranslateCaptureRegion()
        {
            PixelRegions pixelRegions = PixelRegions.Instance;

            pixelRegions.CaptureRegion = new Rectangle(10, 10, 18, 14);

            Assert.AreEqual(new Rectangle(10, 10, 18, 14), pixelRegions.CaptureRegion);

            Assert.AreEqual(new Rectangle(22, 21, 1, 2), pixelRegions.GetCaptureSubRegion(0));
            Assert.AreEqual(new Rectangle(24, 21, 1, 2), pixelRegions.GetCaptureSubRegion(1));
            Assert.AreEqual(new Rectangle(26, 21, 1, 2), pixelRegions.GetCaptureSubRegion(2));
            Assert.AreEqual(new Rectangle(26, 19, 1, 2), pixelRegions.GetCaptureSubRegion(3));
            Assert.AreEqual(new Rectangle(26, 18, 1, 2), pixelRegions.GetCaptureSubRegion(4));
            Assert.AreEqual(new Rectangle(26, 15, 1, 2), pixelRegions.GetCaptureSubRegion(5));
            Assert.AreEqual(new Rectangle(26, 14, 1, 2), pixelRegions.GetCaptureSubRegion(6));
            Assert.AreEqual(new Rectangle(26, 11, 1, 2), pixelRegions.GetCaptureSubRegion(7));
            Assert.AreEqual(new Rectangle(26, 10, 1, 2), pixelRegions.GetCaptureSubRegion(8));
            Assert.AreEqual(new Rectangle(24, 10, 1, 2), pixelRegions.GetCaptureSubRegion(9));
            Assert.AreEqual(new Rectangle(22, 10, 1, 2), pixelRegions.GetCaptureSubRegion(10));
            Assert.AreEqual(new Rectangle(19, 10, 1, 2), pixelRegions.GetCaptureSubRegion(11));
            Assert.AreEqual(new Rectangle(17, 10, 1, 2), pixelRegions.GetCaptureSubRegion(12));
            Assert.AreEqual(new Rectangle(15, 10, 1, 2), pixelRegions.GetCaptureSubRegion(13));
            Assert.AreEqual(new Rectangle(13, 10, 1, 2), pixelRegions.GetCaptureSubRegion(14));
            Assert.AreEqual(new Rectangle(11, 10, 1, 2), pixelRegions.GetCaptureSubRegion(15));
            Assert.AreEqual(new Rectangle(10, 10, 1, 2), pixelRegions.GetCaptureSubRegion(16));
            Assert.AreEqual(new Rectangle(10, 11, 1, 2), pixelRegions.GetCaptureSubRegion(17));
            Assert.AreEqual(new Rectangle(10, 14, 1, 2), pixelRegions.GetCaptureSubRegion(18));
            Assert.AreEqual(new Rectangle(10, 15, 1, 2), pixelRegions.GetCaptureSubRegion(19));
            Assert.AreEqual(new Rectangle(10, 18, 1, 2), pixelRegions.GetCaptureSubRegion(20));
            Assert.AreEqual(new Rectangle(10, 19, 1, 2), pixelRegions.GetCaptureSubRegion(21));
            Assert.AreEqual(new Rectangle(10, 21, 1, 2), pixelRegions.GetCaptureSubRegion(22));
            Assert.AreEqual(new Rectangle(11, 21, 1, 2), pixelRegions.GetCaptureSubRegion(23));
            Assert.AreEqual(new Rectangle(13, 21, 1, 2), pixelRegions.GetCaptureSubRegion(24));
        }
    }
}
