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
                VideoEffectGenerator videoEffectGenerator = new VideoEffectGenerator(colourOutputManager);

                videoEffectGenerator.Start();

                Console.ReadLine();

                videoEffectGenerator.Stop();
            }
        }
    }
}
