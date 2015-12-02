using System;
using System.Drawing;

namespace ControlPanel
{
    public interface IWallpaperProvider
    {
        event EventHandler WallpaperChanged;
        Bitmap GetScaledWallpaper();
    }
}
