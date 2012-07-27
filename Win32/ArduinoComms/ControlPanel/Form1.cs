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
        private static extern UInt32 Initialise();

        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 SetLED(Byte red, Byte green, Byte blue, UInt32 pixelIndex);

        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 FlushColours();

        [DllImport("ArduinoCommsLib.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern UInt32 Shutdown();

        public Form1()
        {
            InitializeComponent();

            if(0 != Initialise())
            {
                Console.WriteLine("ERROR");
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            ((Button) sender).BackColor = Color.FromArgb((int) numericUpDown1.Value,
                                                         (int) numericUpDown2.Value,
                                                         (int) numericUpDown3.Value);

            Byte red = Convert.ToByte(numericUpDown1.Value);
            Byte green = Convert.ToByte(numericUpDown2.Value);
            Byte blue = Convert.ToByte(numericUpDown3.Value);
            UInt32 pixelIndex = Convert.ToUInt32(((Button) sender).Tag);
            SetLED(red, green, blue, pixelIndex);

            FlushColours();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Shutdown();

            base.OnClosing(e);
        }
    }
}
