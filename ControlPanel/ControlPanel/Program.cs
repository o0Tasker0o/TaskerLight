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
                int sleepTime = 1500;

                for(int repeat = 0; repeat < 50; ++repeat)
                {
                    for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                    {
                        colourOutputManager.SetPixel(pixelIndex, Color.Red);
                    }

                    colourOutputManager.FlushColours();
                    Thread.Sleep(sleepTime);

                    for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                    {
                        colourOutputManager.SetPixel(pixelIndex, Color.Green);
                    }

                    colourOutputManager.FlushColours();
                    Thread.Sleep(sleepTime);

                    for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                    {
                        colourOutputManager.SetPixel(pixelIndex, Color.Blue);
                    }

                    colourOutputManager.FlushColours();
                    Thread.Sleep(sleepTime);
                }
            }
        }
    }
}
