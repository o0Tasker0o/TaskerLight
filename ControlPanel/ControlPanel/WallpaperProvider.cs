using System;
using System.Drawing;
using System.IO;

namespace ControlPanel
{
    public class WallpaperProvider : IWallpaperProvider
    {
        public event EventHandler WallpaperChanged;

        private readonly String mWallpaperFilename;

        public WallpaperProvider()
        {
            String roamingDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            String wallpaperDirectory = Path.Combine(roamingDir, "Microsoft\\Windows\\Themes");
            mWallpaperFilename = Path.Combine(wallpaperDirectory, "TranscodedWallpaper");

            FileSystemWatcher fsw = new FileSystemWatcher(wallpaperDirectory);

            fsw.NotifyFilter = NotifyFilters.LastWrite;

            fsw.EnableRaisingEvents = true;

            fsw.Changed += WallpaperChangedCallback;
        }

        public Bitmap GetScaledWallpaper()
        {
            Bitmap scaledWallpaper = new Bitmap(11, 7);
            Image originalWallpaper;

            try
            {
                using (FileStream stream = File.OpenRead(mWallpaperFilename))
                {
                    originalWallpaper = Image.FromStream(stream);
                }
            }
            catch
            {
                return null;
            }

            using (Graphics g = Graphics.FromImage(scaledWallpaper))
            {
                g.DrawImage(originalWallpaper, 0, 0, 9, 7);
            }

            originalWallpaper.Dispose();

            return scaledWallpaper;
        }

        private void WallpaperChangedCallback(object sender, FileSystemEventArgs e)
        {
            if(WallpaperChanged != null)
            {
                WallpaperChanged(sender, e);
            }
        }
    }
}
