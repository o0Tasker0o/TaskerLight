using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace ControlPanelTests
{
    [TestClass()]
    public class WallpaperEffectGeneratorTest
    {
        private static ISerialCommunicator mSerialCommunicator = Substitute.For<ISerialCommunicator>();
        private static ColourOutputManager mColourOutputManager = new ColourOutputManager(mSerialCommunicator);

        [TestMethod()]
        public void MapsProvidedWallpaperToOutputManager()
        {
            IWallpaperProvider wallpaperProvider = Substitute.For<IWallpaperProvider>();
            wallpaperProvider.GetScaledWallpaper().Returns(GenerateBitmap(Color.White));

            WallpaperEffectGenerator wallpaperGenerator = new WallpaperEffectGenerator(mColourOutputManager, wallpaperProvider);

            byte[] fadeTimeBytes = BitConverter.GetBytes(mColourOutputManager.FadeTimeMs);

            byte[] outputPixels = new byte[77];

            for (int i = 0; i < 75; i++)
            {
                outputPixels[i] = 255;
            }

            outputPixels[75] = fadeTimeBytes[0];
            outputPixels[76] = fadeTimeBytes[1];

            mSerialCommunicator.Received(1).Write(Arg.Is<byte[]>(pix => outputPixels.SequenceEqual(pix)));
        }

        [TestMethod()]
        public void HasDefaultFadeTime()
        {
            IWallpaperProvider wallpaperProvider = Substitute.For<IWallpaperProvider>();
            wallpaperProvider.GetScaledWallpaper().Returns(GenerateBitmap(Color.White));

            WallpaperEffectGenerator wallpaperGenerator = new WallpaperEffectGenerator(mColourOutputManager, wallpaperProvider);

            wallpaperGenerator.Start();

            Thread.Sleep(150);

            Assert.AreEqual(1000, mColourOutputManager.FadeTimeMs);

            wallpaperGenerator.Stop();
        }

        private static Bitmap GenerateBitmap(Color backgroundColor)
        {
            const int cWidth = 9;
            const int cHeight = 7;

            Bitmap bitmap = new Bitmap(cWidth, cHeight);

            for(int x = 0; x < cWidth; x++)
            {
                for(int y = 0; y < cHeight; y++)
                {
                    bitmap.SetPixel(x, y, backgroundColor);
                }
            }

            return bitmap;
        }
    }
}
