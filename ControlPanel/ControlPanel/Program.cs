using System;
using System.Drawing;
using System.Threading;

namespace ControlPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using(ColourOutputManager colourOutputManager = new ColourOutputManager(new SerialCommunicator()))
            {
                colourOutputManager.FadeTimeMs = 1000;

                WallpaperEffectGenerator wallpaperEffectGenerator = new WallpaperEffectGenerator(colourOutputManager);

                wallpaperEffectGenerator.Start();

                Console.ReadLine();

                wallpaperEffectGenerator.Stop();
            }
        }
    }
}
