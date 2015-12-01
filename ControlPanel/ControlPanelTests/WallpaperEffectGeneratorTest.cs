using System.Drawing;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace ControlPanelTests
{
    [TestClass()]
    public class WallpaperEffectGeneratorTest
    {
        [TestMethod()]
        public void WallpaperEffectGeneratorConstructorTest()
        {
            IWallpaperProvider wallpaperProvider = Substitute.For<IWallpaperProvider>();
            wallpaperProvider.GetScaledWallpaper().Returns(new Bitmap(9, 7));

            TestSerialCommunicator testSerialCommunicator = new TestSerialCommunicator();

            using(ColourOutputManager colourOutputManager = new ColourOutputManager(testSerialCommunicator))
            {
                WallpaperEffectGenerator wallpaperGenerator = new WallpaperEffectGenerator(colourOutputManager, wallpaperProvider);

                AssertColourValuesMatchWallpaper(testSerialCommunicator.OutputBuffer);
            }
        }

        private void AssertColourValuesMatchWallpaper(byte[] colourValues)
        {
            for (int index = 0; index < 75; ++index)
            {
                Assert.AreEqual(0, colourValues[index]);
            }
        }
    }
}
