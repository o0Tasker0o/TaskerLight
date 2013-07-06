using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace ControlPanel
{
    public class WallpaperEffectGenerator : EffectGenerator
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(UInt32 uAction,
                                                       int uParam,
                                                       String lpvParam,
                                                       int fuWinIni);

        private const UInt32 SPI_GETDESKWALLPAPER = 0x73;

        public WallpaperEffectGenerator(ColourOutputManager colourOutputManager) : base(colourOutputManager)
        {
        }

        protected override void ThreadTick()
        {
            mOutputManager.FadeTimeMs = 1000;

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
                    g.DrawImage(originalWallpaper, 0, 0, 9, 7);
                }

                originalWallpaper.Dispose();

                mOutputManager.SetPixel(0, scaledWallpaper.GetPixel(6, 6));
                mOutputManager.SetPixel(1, scaledWallpaper.GetPixel(7, 6));
                mOutputManager.SetPixel(2, scaledWallpaper.GetPixel(8, 6));

                mOutputManager.SetPixel(3, scaledWallpaper.GetPixel(8, 5));
                mOutputManager.SetPixel(4, scaledWallpaper.GetPixel(8, 4));
                mOutputManager.SetPixel(5, scaledWallpaper.GetPixel(8, 3));
                mOutputManager.SetPixel(6, scaledWallpaper.GetPixel(8, 2));
                mOutputManager.SetPixel(7, scaledWallpaper.GetPixel(8, 1));

                mOutputManager.SetPixel(8, scaledWallpaper.GetPixel(8, 0));
                mOutputManager.SetPixel(9, scaledWallpaper.GetPixel(7, 0));
                mOutputManager.SetPixel(10, scaledWallpaper.GetPixel(6, 0));
                mOutputManager.SetPixel(11, scaledWallpaper.GetPixel(5, 0));
                mOutputManager.SetPixel(12, scaledWallpaper.GetPixel(4, 0));
                mOutputManager.SetPixel(13, scaledWallpaper.GetPixel(3, 0));
                mOutputManager.SetPixel(14, scaledWallpaper.GetPixel(2, 0));
                mOutputManager.SetPixel(15, scaledWallpaper.GetPixel(1, 0));
                mOutputManager.SetPixel(16, scaledWallpaper.GetPixel(0, 0));

                mOutputManager.SetPixel(17, scaledWallpaper.GetPixel(0, 1));
                mOutputManager.SetPixel(18, scaledWallpaper.GetPixel(0, 2));
                mOutputManager.SetPixel(19, scaledWallpaper.GetPixel(0, 3));
                mOutputManager.SetPixel(20, scaledWallpaper.GetPixel(0, 4));
                mOutputManager.SetPixel(21, scaledWallpaper.GetPixel(0, 5));

                mOutputManager.SetPixel(22, scaledWallpaper.GetPixel(0, 6));
                mOutputManager.SetPixel(23, scaledWallpaper.GetPixel(1, 6));
                mOutputManager.SetPixel(24, scaledWallpaper.GetPixel(2, 6));

                mOutputManager.FlushColours();

                Thread.Sleep(mOutputManager.FadeTimeMs);
            }
        } 
    }
}
