using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        internal static Color SetLED(Color ledColor, UInt32 pixelIndex, float saturationMultiplier = 1.0f)
        {
            if(saturationMultiplier != 1.0f)
            {
                float hue = 0.0f;
                float saturation = 0.0f;
                float value = 0.0f;

                ColourUtil.RGBToHSV(ledColor, ref hue, ref saturation, ref value);

                saturation *= saturationMultiplier;

                if(saturation > 1.0f)
                {
                    saturation = 1.0f;
                }

                ledColor = ColourUtil.HSVToRGB(hue, saturation, value);
            }

            SetLED(Convert.ToByte(ledColor.R),
                   Convert.ToByte(ledColor.G),
                   Convert.ToByte(ledColor.B),
                   pixelIndex);

            return ledColor;
        }
    }
}
