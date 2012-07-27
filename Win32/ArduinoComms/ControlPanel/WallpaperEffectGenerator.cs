using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace ControlPanel
{
    internal class WallpaperEffectGenerator : EffectGenerator
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(UInt32 uAction,
                                                       int uParam,
                                                       String lpvParam,
                                                       int fuWinIni);

        private const UInt32 SPI_GETDESKWALLPAPER = 0x73;

        public WallpaperEffectGenerator(LEDPreview ledPreview) : base(ledPreview)
        {

        }
        
        protected override void ThreadTick()
        {
            RegionManager.Instance().Resize(new Rectangle(0, 0, 11, 7));

            while(mRunning)
            {
                String wallpaperFilename = new String(' ', 256);

                SystemParametersInfo(SPI_GETDESKWALLPAPER,
                                     wallpaperFilename.Length,
                                     wallpaperFilename,
                                     0);

                wallpaperFilename = wallpaperFilename.Substring(0, wallpaperFilename.IndexOf('\0'));

                Bitmap scaledWallpaper = new Bitmap(11, 7);
                Image originalWallpaper;

                try
                {
                    using (FileStream stream = File.OpenRead(wallpaperFilename))
                    {
                        originalWallpaper = Bitmap.FromStream(stream);
                    }
                }
                catch
                {
                    continue;
                }

                using (Graphics g = Graphics.FromImage(scaledWallpaper))
                {
                    g.DrawImage(originalWallpaper, 0, 0, 11, 7);
                }

                originalWallpaper.Dispose();

                for (UInt32 i = 0; i < 25; ++i)
                {
                    Rectangle region = RegionManager.Instance().GetRegion(i);
                    mOutputColours[i] = scaledWallpaper.GetPixel(region.X, region.Y);
                }

                scaledWallpaper.Dispose();

                OutputColours();

                Thread.Sleep(1000);
            }
        }
    }
}
