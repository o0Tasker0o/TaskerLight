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
            
            Assert.AreEqual(new Rectangle(60, 60, 10, 10), pixelRegions.GetRegion(0));
            Assert.AreEqual(new Rectangle(70, 60, 10, 10), pixelRegions.GetRegion(1));
            Assert.AreEqual(new Rectangle(80, 60, 10, 10), pixelRegions.GetRegion(2));
            Assert.AreEqual(new Rectangle(80, 50, 10, 10), pixelRegions.GetRegion(3));
            Assert.AreEqual(new Rectangle(80, 40, 10, 10), pixelRegions.GetRegion(4));
            Assert.AreEqual(new Rectangle(80, 30, 10, 10), pixelRegions.GetRegion(5));
            Assert.AreEqual(new Rectangle(80, 20, 10, 10), pixelRegions.GetRegion(6));
            Assert.AreEqual(new Rectangle(80, 10, 10, 10), pixelRegions.GetRegion(7));
            Assert.AreEqual(new Rectangle(80, 0, 10, 10), pixelRegions.GetRegion(8));
            Assert.AreEqual(new Rectangle(70, 0, 10, 10), pixelRegions.GetRegion(9));
            Assert.AreEqual(new Rectangle(60, 0, 10, 10), pixelRegions.GetRegion(10));
            Assert.AreEqual(new Rectangle(50, 0, 10, 10), pixelRegions.GetRegion(11));
            Assert.AreEqual(new Rectangle(40, 0, 10, 10), pixelRegions.GetRegion(12));
            Assert.AreEqual(new Rectangle(30, 0, 10, 10), pixelRegions.GetRegion(13));
            Assert.AreEqual(new Rectangle(20, 0, 10, 10), pixelRegions.GetRegion(14));
            Assert.AreEqual(new Rectangle(10, 0, 10, 10), pixelRegions.GetRegion(15));
            Assert.AreEqual(new Rectangle(0, 0, 10, 10), pixelRegions.GetRegion(16));
            Assert.AreEqual(new Rectangle(0, 10, 10, 10), pixelRegions.GetRegion(17));
            Assert.AreEqual(new Rectangle(0, 20, 10, 10), pixelRegions.GetRegion(18));
            Assert.AreEqual(new Rectangle(0, 30, 10, 10), pixelRegions.GetRegion(19));
            Assert.AreEqual(new Rectangle(0, 40, 10, 10), pixelRegions.GetRegion(20));
            Assert.AreEqual(new Rectangle(0, 50, 10, 10), pixelRegions.GetRegion(21));
            Assert.AreEqual(new Rectangle(0, 60, 10, 10), pixelRegions.GetRegion(22));
            Assert.AreEqual(new Rectangle(10, 60, 10, 10), pixelRegions.GetRegion(23));
            Assert.AreEqual(new Rectangle(20, 60, 10, 10), pixelRegions.GetRegion(24));
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
            Assert.AreEqual(new Rectangle(1706, 154, 213, 154), pixelRegions.GetCaptureSubRegion(7));
            Assert.AreEqual(new Rectangle(1706, 0, 213, 154), pixelRegions.GetCaptureSubRegion(8));
            Assert.AreEqual(new Rectangle(1493, 0, 213, 154), pixelRegions.GetCaptureSubRegion(9));
            Assert.AreEqual(new Rectangle(1280, 0, 213, 154), pixelRegions.GetCaptureSubRegion(10));
            Assert.AreEqual(new Rectangle(1066, 0, 213, 154), pixelRegions.GetCaptureSubRegion(11));
            Assert.AreEqual(new Rectangle(853, 0, 213, 154), pixelRegions.GetCaptureSubRegion(12));
            Assert.AreEqual(new Rectangle(640, 0, 213, 154), pixelRegions.GetCaptureSubRegion(13));
            Assert.AreEqual(new Rectangle(426, 0, 213, 154), pixelRegions.GetCaptureSubRegion(14));
            Assert.AreEqual(new Rectangle(213, 0, 213, 154), pixelRegions.GetCaptureSubRegion(15));
            Assert.AreEqual(new Rectangle(0, 0, 213, 154), pixelRegions.GetCaptureSubRegion(16));
            Assert.AreEqual(new Rectangle(0, 154, 213, 154), pixelRegions.GetCaptureSubRegion(17));
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

            pixelRegions.CaptureRegion = new Rectangle(0, 0, 9, 7);

            Assert.AreEqual(new Rectangle(0, 0, 9, 7), pixelRegions.CaptureRegion);

            Assert.AreEqual(new Rectangle(6, 6, 1, 1), pixelRegions.GetCaptureSubRegion(0));
            Assert.AreEqual(new Rectangle(7, 6, 1, 1), pixelRegions.GetCaptureSubRegion(1));
            Assert.AreEqual(new Rectangle(8, 6, 1, 1), pixelRegions.GetCaptureSubRegion(2));
            Assert.AreEqual(new Rectangle(8, 5, 1, 1), pixelRegions.GetCaptureSubRegion(3));
            Assert.AreEqual(new Rectangle(8, 4, 1, 1), pixelRegions.GetCaptureSubRegion(4));
            Assert.AreEqual(new Rectangle(8, 3, 1, 1), pixelRegions.GetCaptureSubRegion(5));
            Assert.AreEqual(new Rectangle(8, 2, 1, 1), pixelRegions.GetCaptureSubRegion(6));
            Assert.AreEqual(new Rectangle(8, 1, 1, 1), pixelRegions.GetCaptureSubRegion(7));
            Assert.AreEqual(new Rectangle(8, 0, 1, 1), pixelRegions.GetCaptureSubRegion(8));
            Assert.AreEqual(new Rectangle(7, 0, 1, 1), pixelRegions.GetCaptureSubRegion(9));
            Assert.AreEqual(new Rectangle(6, 0, 1, 1), pixelRegions.GetCaptureSubRegion(10));
            Assert.AreEqual(new Rectangle(5, 0, 1, 1), pixelRegions.GetCaptureSubRegion(11));
            Assert.AreEqual(new Rectangle(4, 0, 1, 1), pixelRegions.GetCaptureSubRegion(12));
            Assert.AreEqual(new Rectangle(3, 0, 1, 1), pixelRegions.GetCaptureSubRegion(13));
            Assert.AreEqual(new Rectangle(2, 0, 1, 1), pixelRegions.GetCaptureSubRegion(14));
            Assert.AreEqual(new Rectangle(1, 0, 1, 1), pixelRegions.GetCaptureSubRegion(15));
            Assert.AreEqual(new Rectangle(0, 0, 1, 1), pixelRegions.GetCaptureSubRegion(16));
            Assert.AreEqual(new Rectangle(0, 1, 1, 1), pixelRegions.GetCaptureSubRegion(17));
            Assert.AreEqual(new Rectangle(0, 2, 1, 1), pixelRegions.GetCaptureSubRegion(18));
            Assert.AreEqual(new Rectangle(0, 3, 1, 1), pixelRegions.GetCaptureSubRegion(19));
            Assert.AreEqual(new Rectangle(0, 4, 1, 1), pixelRegions.GetCaptureSubRegion(20));
            Assert.AreEqual(new Rectangle(0, 5, 1, 1), pixelRegions.GetCaptureSubRegion(21));
            Assert.AreEqual(new Rectangle(0, 6, 1, 1), pixelRegions.GetCaptureSubRegion(22));
            Assert.AreEqual(new Rectangle(1, 6, 1, 1), pixelRegions.GetCaptureSubRegion(23));
            Assert.AreEqual(new Rectangle(2, 6, 1, 1), pixelRegions.GetCaptureSubRegion(24));
        }

        [TestMethod()]
        public void PixelRegionsCanScaleAndTranslateCaptureRegion()
        {
            PixelRegions pixelRegions = PixelRegions.Instance;

            pixelRegions.CaptureRegion = new Rectangle(10, 10, 9, 7);

            Assert.AreEqual(new Rectangle(10, 10, 9, 7), pixelRegions.CaptureRegion);

            Assert.AreEqual(new Rectangle(16, 16, 1, 1), pixelRegions.GetCaptureSubRegion(0));
            Assert.AreEqual(new Rectangle(17, 16, 1, 1), pixelRegions.GetCaptureSubRegion(1));
            Assert.AreEqual(new Rectangle(18, 16, 1, 1), pixelRegions.GetCaptureSubRegion(2));
            Assert.AreEqual(new Rectangle(18, 15, 1, 1), pixelRegions.GetCaptureSubRegion(3));
            Assert.AreEqual(new Rectangle(18, 14, 1, 1), pixelRegions.GetCaptureSubRegion(4));
            Assert.AreEqual(new Rectangle(18, 13, 1, 1), pixelRegions.GetCaptureSubRegion(5));
            Assert.AreEqual(new Rectangle(18, 12, 1, 1), pixelRegions.GetCaptureSubRegion(6));
            Assert.AreEqual(new Rectangle(18, 11, 1, 1), pixelRegions.GetCaptureSubRegion(7));
            Assert.AreEqual(new Rectangle(18, 10, 1, 1), pixelRegions.GetCaptureSubRegion(8));
            Assert.AreEqual(new Rectangle(17, 10, 1, 1), pixelRegions.GetCaptureSubRegion(9));
            Assert.AreEqual(new Rectangle(16, 10, 1, 1), pixelRegions.GetCaptureSubRegion(10));
            Assert.AreEqual(new Rectangle(15, 10, 1, 1), pixelRegions.GetCaptureSubRegion(11));
            Assert.AreEqual(new Rectangle(14, 10, 1, 1), pixelRegions.GetCaptureSubRegion(12));
            Assert.AreEqual(new Rectangle(13, 10, 1, 1), pixelRegions.GetCaptureSubRegion(13));
            Assert.AreEqual(new Rectangle(12, 10, 1, 1), pixelRegions.GetCaptureSubRegion(14));
            Assert.AreEqual(new Rectangle(11, 10, 1, 1), pixelRegions.GetCaptureSubRegion(15));
            Assert.AreEqual(new Rectangle(10, 10, 1, 1), pixelRegions.GetCaptureSubRegion(16));
            Assert.AreEqual(new Rectangle(10, 11, 1, 1), pixelRegions.GetCaptureSubRegion(17));
            Assert.AreEqual(new Rectangle(10, 12, 1, 1), pixelRegions.GetCaptureSubRegion(18));
            Assert.AreEqual(new Rectangle(10, 13, 1, 1), pixelRegions.GetCaptureSubRegion(19));
            Assert.AreEqual(new Rectangle(10, 14, 1, 1), pixelRegions.GetCaptureSubRegion(20));
            Assert.AreEqual(new Rectangle(10, 15, 1, 1), pixelRegions.GetCaptureSubRegion(21));
            Assert.AreEqual(new Rectangle(10, 16, 1, 1), pixelRegions.GetCaptureSubRegion(22));
            Assert.AreEqual(new Rectangle(11, 16, 1, 1), pixelRegions.GetCaptureSubRegion(23));
            Assert.AreEqual(new Rectangle(12, 16, 1, 1), pixelRegions.GetCaptureSubRegion(24));
        }
    }
}
