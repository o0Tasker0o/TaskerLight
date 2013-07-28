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
    }
}
