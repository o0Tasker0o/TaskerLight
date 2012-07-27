using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ControlPanel
{
    public partial class Form1 : Form
    {
        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 InitialiseArduinoComms();

        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 SetLED(Byte red, Byte green, Byte blue, UInt32 pixelIndex);

        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 FlushColours();

        [DllImport("ArduinoCommsLib.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern UInt32 ShutdownArduinoComms();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 InitialiseScreenCapture();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 ShutdownScreenCapture();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 StartCapturing();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 GetAverageColour(UInt32 x, UInt32 y,
                                                      UInt32 width, UInt32 height);

        public Form1()
        {
            InitializeComponent();

            if(0 != InitialiseArduinoComms())
            {
                Console.WriteLine("ERROR");
            }

            if(0 != InitialiseScreenCapture())
            {
                Console.WriteLine("ERROR");
            }

            StartCapturing();

            screenCaptureTimer.Start();
        }
        
        protected override void OnClosing(CancelEventArgs e)
        {
            ShutdownArduinoComms();

            ShutdownScreenCapture();

            base.OnClosing(e);
        }

        private void screenCaptureTimer_Tick(object sender, EventArgs e)
        {
            Color colour = ConvertToColor(GetAverageColour(1500, 970,
                                                           80, 80));

            SetLED(Convert.ToByte(colour.R),
                   Convert.ToByte(colour.G),
                   Convert.ToByte(colour.B),
                   0);

            for (UInt32 i = 1; i < 8; ++i)
            {
                colour = ConvertToColor(GetAverageColour(1460, 1050 - (i * 150), 
                                                         80, 80));

                SetLED(Convert.ToByte(colour.R),
                       Convert.ToByte(colour.G),
                       Convert.ToByte(colour.B),
                       i);
            }

            for (UInt32 i = 17; i < 24; ++i)
            {
                colour = ConvertToColor(GetAverageColour(140, (i - 17) * 150,
                                                         80, 80));

                SetLED(Convert.ToByte(colour.R),
                       Convert.ToByte(colour.G),
                       Convert.ToByte(colour.B),
                       i);
            }

            for (UInt32 i = 8; i < 17; ++i)
            {
                colour = ConvertToColor(GetAverageColour(1432 - ((i - 8) * 168), 0,
                                                         80, 80));

                SetLED(Convert.ToByte(colour.R),
                       Convert.ToByte(colour.G),
                       Convert.ToByte(colour.B),
                       i);
            }

            colour = ConvertToColor(GetAverageColour(180, 970,
                                                     80, 80));

            SetLED(Convert.ToByte(colour.R),
                   Convert.ToByte(colour.G),
                   Convert.ToByte(colour.B),
                   24);

            FlushColours();
        }

        Color ConvertToColor(UInt32 colour)
        {
            UInt32 blue = colour & 255;
            colour >>= 8;
            UInt32 green = colour & 255;
            colour >>= 8;
            UInt32 red = colour & 255;

            return Color.FromArgb((int) red, (int) green, (int) blue);
        }
    }
}
