using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ControlPanel
{
    internal static class OutputManager
    {
        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 SetLED(Byte red, Byte green, Byte blue, UInt32 pixelIndex);

        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern UInt32 FlushColours();

        internal static Color SetLED(Color ledColor, UInt32 pixelIndex, float saturationMultiplier = 1.0f, float contrastMultiplier = 1.0f)
        {
            if(saturationMultiplier != 1.0f)
            {
                ledColor = ColourUtil.AdjustSaturation(ledColor, saturationMultiplier);
            }

            if(contrastMultiplier != 1.0f)
            {
                ledColor = ColourUtil.AdjustContrast(ledColor, contrastMultiplier);
            }

            SetLED(Convert.ToByte(ledColor.R),
                   Convert.ToByte(ledColor.G),
                   Convert.ToByte(ledColor.B),
                   pixelIndex);

            return ledColor;
        }
    }
}
