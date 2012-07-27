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
using System.Diagnostics;

namespace ControlPanel
{
    public partial class Form1 : Form
    {
        #region DLL Imports
        [DllImport("ArduinoCommsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 InitialiseArduinoComms(String comPort);

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

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 TestLeftBorder(Int32 x, Int32 y,
                                                   Int32 width, Int32 height);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 GetLeftBorder();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 TestRightBorder(Int32 x, Int32 y,
                                                    Int32 width, Int32 height);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 GetRightBorder();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(UInt32 uAction, 
                                                       int uParam,
                                                       String lpvParam, 
                                                       int fuWinIni);
        
        private const UInt32 SPI_GETDESKWALLPAPER = 0x73;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetClientRect(IntPtr hWnd, ref Rectangle rect);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindow(IntPtr hWnd, UInt32 wCmd);

        private const UInt32 GW_HWNDNEXT = 0x02;
        #endregion

        //private Color [] mStaticColours;

        internal struct ProcessIndex
        {
            internal String processName;
            internal String exeName;
            internal Process process;
            internal int topMargin;
            internal int bottomMargin;
        };
        
        public Form1()
        {
            InitializeComponent();

            InitialiseArduinoComms("\\\\.\\com" + arduinoComPortUpDown.Value.ToString());

            InitialiseScreenCapture();
            
            SettingsManager.LoadSettings();
            
            foreach(ProcessIndex activeApp in SettingsManager.GetActiveApps())
            {
                AddActiveApp(activeApp);
            }

            //mStaticColours = SettingsManager.GetStaticColours();
            
            this.oversaturationTrackbar.Value = (int) (SettingsManager.Saturation * 100.0f);
            this.contrastTrackbar.Value = (int) (SettingsManager.Contrast * 100.0f);

            switch(SettingsManager.Mode)
            {
                case 0:
                    staticColoursBackgroundRadioButton.Checked = true;
                  break;
                case 2:
                    wallpaperBackgroundModeRadioButton.Checked = true;
                  break;
                case 3:
                    capturedBackgroundModeRadioButton.Checked = true;
                  break;
            }

            staticColoursBackgroundRadioButton_CheckedChanged(this, EventArgs.Empty);
            wallpaperBackgroundModeRadioButton_CheckedChanged(this, EventArgs.Empty);
            capturedBackgroundModeRadioButton_CheckedChanged(this, EventArgs.Empty);
        }

        internal void AddActiveApp(ProcessIndex processIndex)
        {
            FileInfo exeFile = new FileInfo(processIndex.exeName);

            Icon icon = Icon.ExtractAssociatedIcon(processIndex.exeName);
            mAppImageList.Images.Add(icon);

            //ProcessIndex processIndex = new ProcessIndex();
            processIndex.processName = Path.GetFileNameWithoutExtension(exeFile.FullName);
            //processIndex.exeName = exeFile.FullName;

            ListViewItem appItem = new ListViewItem(Path.GetFileNameWithoutExtension(exeFile.FullName));
            appItem.Tag = processIndex;
            appItem.ImageIndex = this.mAppImageList.Images.Count - 1;

            this.activeAppListView.Items.Add(appItem);
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
                                                               SettingsManager.Saturation,
                                                               SettingsManager.Contrast),
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
            List<ProcessIndex> activeProcesses = new List<ProcessIndex>();
            
            for(int appIndex = 0; appIndex < activeAppListView.Items.Count; ++appIndex)
            {
                ProcessIndex processIndex = (ProcessIndex)activeAppListView.Items[appIndex].Tag;
                Process[] processes = Process.GetProcessesByName(processIndex.processName);

                if(processes.Length > 0)
                {
                    processIndex.process = processes[0];
                    activeProcesses.Add(processIndex);
                }

                activeAppListView.Items[appIndex].Tag = processIndex;
            }

            IntPtr foregroundWindow = GetForegroundWindow();

            bool activeProcessFound = false;

            do
            {
                //for(int appIndex = 0; appIndex < activeAppListView.Items.Count; ++appIndex)
                foreach(ProcessIndex runningProcess in activeProcesses)
                {
                    //ProcessIndex processIndex = (ProcessIndex) activeAppListView.Items[appIndex].Tag;
                
                    if(foregroundWindow == runningProcess.process.MainWindowHandle)
                    {
                        //This window is the highest level window of one of our active applications
                        activeProcessFound = true;

                        Rectangle rectangle = new Rectangle();

                        Point position = new Point(0, 0);
                        ClientToScreen(runningProcess.process.MainWindowHandle, ref position);

                        GetClientRect(runningProcess.process.MainWindowHandle, ref rectangle);

                        if(rectangle == Screen.PrimaryScreen.Bounds && !marginsFullscreenCheckbox.Checked)
                        {
                            TestLeftBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                            TestRightBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

                            rectangle.X += GetLeftBorder();
                            rectangle.Width -= GetLeftBorder();
                            rectangle.Width -= GetRightBorder();

                            RegionManager.Instance().Resize(rectangle);
                            break;
                        }

                        rectangle.X = position.X;
                        rectangle.Y = position.Y + runningProcess.topMargin;
                      
                        rectangle.Height -= runningProcess.topMargin;
                        rectangle.Height -= runningProcess.bottomMargin;

                        if(rectangle.X < 0)
                        {
                            rectangle.Width += rectangle.X;
                            rectangle.X = 0;
                        }

                        if(rectangle.X + rectangle.Width > Screen.PrimaryScreen.Bounds.Width)
                        {
                            int excessWidth = (rectangle.X + rectangle.Width) - Screen.PrimaryScreen.Bounds.Width;
                            rectangle.Width -= excessWidth;
                        }

                        if(rectangle.Y + rectangle.Height > Screen.PrimaryScreen.Bounds.Height)
                        {
                            int excessHeight = (rectangle.Y + rectangle.Height) - Screen.PrimaryScreen.Bounds.Height;
                            rectangle.Height -= excessHeight;
                        }

                        TestLeftBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                        TestRightBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

                        rectangle.X += GetLeftBorder();
                        rectangle.Width -= GetLeftBorder();
                        rectangle.Width -= GetRightBorder();

                        RegionManager.Instance().Resize(rectangle);

                        break;
                    }
                }

                foregroundWindow = GetWindow(foregroundWindow, GW_HWNDNEXT);
            } while(foregroundWindow != IntPtr.Zero && !activeProcessFound);

            if(!activeProcessFound)
            {
                Rectangle rectangle = Screen.PrimaryScreen.Bounds;

                TestLeftBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                TestRightBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

                rectangle.X += GetLeftBorder();
                rectangle.Width -= GetLeftBorder();
                rectangle.Width -= GetRightBorder();

                RegionManager.Instance().Resize(rectangle);
            }
            
            for (UInt32 i = 0; i < 25; ++i)
            {
                Rectangle region = RegionManager.Instance().GetRegion(i);
                Color colour = ColourUtil.ConvertToColor(GetAverageColour((UInt32) region.X, (UInt32) region.Y,
                                                                          (UInt32) region.Width, (UInt32) region.Height));

                this.ledPreview1.SetPixel(OutputManager.SetLED(colour, 
                                                               i,
                                                               SettingsManager.Saturation,
                                                               SettingsManager.Contrast),
                                          i);
            }

            OutputManager.FlushColours();

            this.ledPreview1.Refresh();
        }

        private void staticColoursBackgroundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(staticColoursBackgroundRadioButton.Checked)
            {
                for(UInt32 pixelIndex = 0; pixelIndex < SettingsManager.StaticColours.Length; ++pixelIndex)
                {
                    this.ledPreview1.SetPixel(SettingsManager.StaticColours[pixelIndex], pixelIndex);

                    OutputManager.SetLED(SettingsManager.StaticColours[pixelIndex], pixelIndex);
                }

                this.ledPreview1.Refresh();

                OutputManager.FlushColours();

                this.hsvPicker.Visible = true;

                SettingsManager.Mode = 0;
            }
            else
            {
                this.hsvPicker.Visible = false;
            }
        }

        private void wallpaperBackgroundModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(wallpaperBackgroundModeRadioButton.Checked)
            {
                RegionManager.Instance().Resize(new Rectangle(0, 0, 11, 7));
                LoadWallpaper();
                wallpaperTimer.Start();

                SettingsManager.Mode = 2;
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

                SettingsManager.Mode = 3;
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
                SettingsManager.StaticColours = (Color[])this.ledPreview1.GetPixels().Clone();
                
                UInt32 pixelIndex = 0;

                foreach(Color pixel in SettingsManager.StaticColours)
                {
                    OutputManager.SetLED(pixel, pixelIndex++);
                }

                OutputManager.FlushColours();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SettingsManager.SetActiveApps(activeAppListView.Items);

            SettingsManager.SaveSettings();

            base.OnFormClosing(e);
        }

        private void wallpaperTimer_Tick(object sender, EventArgs e)
        {
            LoadWallpaper();
        }

        private void addAppButton_Click(object sender, EventArgs e)
        {
            if(mOpenExeDialog.ShowDialog() == DialogResult.OK)
            {
                ProcessIndex processIndex = new ProcessIndex();
                processIndex.exeName = mOpenExeDialog.FileName;
                AddActiveApp(processIndex);
            }
        }

        private void activeAppListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(activeAppListView.SelectedIndices.Count > 0)
            {
                this.activeAppsMarginGroupbox.Visible = true;
                this.removeAppButton.Visible = true;

                ProcessIndex currentProcess = (ProcessIndex) activeAppListView.Items[activeAppListView.SelectedIndices[0]].Tag;
                this.topMarginUpDown.Value = currentProcess.topMargin;
                this.bottomMarginUpDown.Value = currentProcess.bottomMargin;
            }
            else
            {
                this.activeAppsMarginGroupbox.Visible = false;
                this.removeAppButton.Visible = false;
            }
        }

        private void removeAppButton_Click(object sender, EventArgs e)
        {
            this.activeAppListView.Items.RemoveAt(this.activeAppListView.SelectedIndices[0]);
        }

        private void topMarginUpDown_ValueChanged(object sender, EventArgs e)
        {
            ProcessIndex processIndex = (ProcessIndex) activeAppListView.Items[this.activeAppListView.SelectedIndices[0]].Tag;
            processIndex.topMargin = (int) topMarginUpDown.Value;
            activeAppListView.Items[this.activeAppListView.SelectedIndices[0]].Tag = processIndex;
        }

        private void bottomMarginUpDown_ValueChanged(object sender, EventArgs e)
        {
            ProcessIndex processIndex = (ProcessIndex)activeAppListView.Items[this.activeAppListView.SelectedIndices[0]].Tag;
            processIndex.bottomMargin = (int) bottomMarginUpDown.Value;
            activeAppListView.Items[this.activeAppListView.SelectedIndices[0]].Tag = processIndex;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if(FormWindowState.Minimized == WindowState)
            {
                Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void oversaturationTrackbar_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.Saturation = oversaturationTrackbar.Value / 100.0f;
        }

        private void contrastTrackbar_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.Contrast = contrastTrackbar.Value / 100.0f;
        }

        private void hsvPicker_ColourChanged(object sender, EventArgs e)
        {
            this.ledPreview1.ActiveColour = this.hsvPicker.Colour;
        }

        private void arduinoComPortUpDown_ValueChanged(object sender, EventArgs e)
        {
            ShutdownArduinoComms();
            InitialiseArduinoComms("\\\\.\\com" + arduinoComPortUpDown.Value.ToString());
        }
    }
}
