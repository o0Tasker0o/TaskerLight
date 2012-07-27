using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace ControlPanel
{
    public partial class Form1 : Form
    {
        #region DLL Imports
        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 InitialiseArduinoComms();

        [DllImport("ArduinoCommsLib.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern UInt32 ShutdownArduinoComms();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 InitialiseScreenCapture();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 ShutdownScreenCapture();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 StartCapturing(bool useDirectX);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 StopCapturing();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 GetAverageColour(UInt32 x, UInt32 y,
                                                      UInt32 width, UInt32 height);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(UInt32 uAction, 
                                                       int uParam,
                                                       String lpvParam, 
                                                       int fuWinIni);
        
        private const UInt32 SPI_GETDESKWALLPAPER = 0x73;
        #endregion

        private Color [] mStaticColours;

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

            SettingsManager.LoadSettings();

            mStaticColours = SettingsManager.GetStaticColours();

            staticColoursBackgroundRadioButton_CheckedChanged(this, EventArgs.Empty);
            wallpaperBackgroundModeRadioButton_CheckedChanged(this, EventArgs.Empty);
            capturedBackgroundModeRadioButton_CheckedChanged(this, EventArgs.Empty);
        }
        
        internal void LoadWallpaper()
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
                FileStream stream = File.OpenRead(wallpaperFilename);
                originalWallpaper = Bitmap.FromStream(stream);
                stream.Close();
            }
            catch
            {
                return;
            }
            
            using(Graphics g = Graphics.FromImage(scaledWallpaper))
            {
                g.DrawImage(originalWallpaper, 0, 0, 11, 7);
            }

            for(UInt32 i = 0; i < 25; ++i)
            {
                Rectangle region = RegionManager.Instance().GetRegion(i);
                this.ledPreview1.SetPixel(OutputManager.SetLED(scaledWallpaper.GetPixel(region.X, region.Y),
                                                               i,
                                                               1.0f),
                                          i);
            }

            this.ledPreview1.Refresh();

            OutputManager.FlushColours();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ShutdownArduinoComms();

            ShutdownScreenCapture();

            base.OnClosing(e);
        }

        private void screenCaptureTimer_Tick(object sender, EventArgs e)
        {
            float saturation = 1.6f;

            for (UInt32 i = 0; i < 25; ++i)
            {
                Rectangle region = RegionManager.Instance().GetRegion(i);
                Color colour = ColourUtil.ConvertToColor(GetAverageColour((UInt32) region.X, (UInt32) region.Y,
                                                                          (UInt32) region.Width, (UInt32) region.Height));

                this.ledPreview1.SetPixel(OutputManager.SetLED(colour, i, saturation), i);
            }

            OutputManager.FlushColours();

            this.ledPreview1.Refresh();
        }

        private void hsvPicker1_ColourChanged(object sender, EventArgs e)
        {
            this.ledPreview1.ActiveColour = this.hsvPicker1.GetColour();
        }

        private void staticColoursBackgroundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(staticColoursBackgroundRadioButton.Checked)
            {
                for(UInt32 pixelIndex = 0; pixelIndex < mStaticColours.Length; ++pixelIndex)
                {
                    this.ledPreview1.SetPixel(mStaticColours[pixelIndex], pixelIndex);

                    OutputManager.SetLED(mStaticColours[pixelIndex], pixelIndex);
                }

                this.ledPreview1.Refresh();

                OutputManager.FlushColours();

                this.hsvPicker1.Visible = true;
            }
            else
            {
                this.hsvPicker1.Visible = false;
            }
        }
        
        private void wallpaperBackgroundModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(wallpaperBackgroundModeRadioButton.Checked)
            {
                RegionManager.Instance().Resize(new Rectangle(0, 0, 11, 7));
                LoadWallpaper();
                wallpaperTimer.Start();
            }
            else
            {
                wallpaperTimer.Stop();
            }
        }

        private void capturedBackgroundModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(capturedBackgroundModeRadioButton.Checked)
            {
                RegionManager.Instance().Resize();
                StartCapturing(true);
                screenCaptureTimer.Start();
            }
            else
            {
                screenCaptureTimer.Stop();

                StopCapturing();
            }
        }

        private void ledPreview1_MouseUp(object sender, MouseEventArgs e)
        {
            if(staticColoursBackgroundRadioButton.Checked)
            {
                mStaticColours = (Color []) this.ledPreview1.GetPixels().Clone();
                
                UInt32 pixelIndex = 0;

                foreach(Color pixel in mStaticColours)
                {
                    OutputManager.SetLED(pixel, pixelIndex++);
                }

                OutputManager.FlushColours();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SettingsManager.SetStaticColours(mStaticColours);
            SettingsManager.SaveSettings();

            base.OnFormClosing(e);
        }

        private void wallpaperTimer_Tick(object sender, EventArgs e)
        {
            LoadWallpaper();
        }
    }
}
