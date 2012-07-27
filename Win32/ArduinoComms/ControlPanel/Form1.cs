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
        #endregion

        #region Error codes defined in ArduinoComms header     
        const uint TASKERLIGHT_OK	    = 0x00;
        const uint TASKERLIGHT_ERROR    = 0x01;
        #endregion

        internal struct ProcessIndex
        {
            internal String processName;
            internal String exeName;
            internal Process process;
            internal int topMargin;
            internal int bottomMargin;
        };

        #region Member Variables
        private CompilerForm mCompilerForm;
        private AppDomain mScriptAppDomain;
        private ScriptLoader mLoader;
        private long mInitialMS;
        private WallpaperEffectGenerator wallpaperEffectGenerator;
        private VideoEffectGenerator mVideoEffectGenerator;
        #endregion

        public Form1()
        {
            InitializeComponent();

            AttemptConnection();
        }

        private void AttemptConnection()
        {
            if(TASKERLIGHT_OK == InitialiseArduinoComms("\\\\.\\com" + arduinoComPortUpDown.Value.ToString()))
            {
                connectButton.Visible = false;

                ledPreview1.Visible = true;
                tabControl1.Visible = true;

                StartupRoutine();
            }
            else
            {
                connectButton.Visible = true;

                ledPreview1.Visible = false;
                tabControl1.Visible = false;
            }
        }

        private void StartupRoutine()
        {
            ledPreviewTimer.Start();
            mCompilerForm = new CompilerForm();

            SettingsManager.LoadSettings();
            
            foreach(ProcessIndex activeApp in SettingsManager.GetActiveApps())
            {
                AddActiveApp(activeApp);
            }
                        
            this.oversaturationTrackbar.Value = (int) (SettingsManager.Saturation * 100.0f);
            this.contrastTrackbar.Value = (int) (SettingsManager.Contrast * 100.0f);

            wallpaperEffectGenerator = new WallpaperEffectGenerator(this.ledPreview1);
            mVideoEffectGenerator = new VideoEffectGenerator(this.ledPreview1, this.activeAppListView.Items);

            switch(SettingsManager.Mode)
            {
                case 0:
                    staticColoursBackgroundRadioButton.Checked = true;
                  break;
                case 1:
                    wallpaperBackgroundModeRadioButton.Checked = true;
                  break;
                case 2:
                    wallpaperBackgroundModeRadioButton.Checked = true;
                  break;
                case 3:
                    capturedBackgroundModeRadioButton.Checked = true;
                  break;
            }


            staticColoursBackgroundRadioButton_CheckedChanged(this, EventArgs.Empty);
            activeSceneModeRadioButton_CheckedChanged(this, EventArgs.Empty);
            wallpaperBackgroundModeRadioButton_CheckedChanged(this, EventArgs.Empty);
            capturedBackgroundModeRadioButton_CheckedChanged(this, EventArgs.Empty);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            wallpaperEffectGenerator.Stop();
            mVideoEffectGenerator.Stop();

            SettingsManager.SetActiveApps(activeAppListView.Items);

            SettingsManager.SaveSettings();

            ShutdownArduinoComms();
            
            base.OnFormClosing(e);
        }
                
        #region Static Colour Functions
        private void hsvPicker_ColourChanged(object sender, EventArgs e)
        {
            this.ledPreview1.ActiveColour = this.hsvPicker.Colour;
        }

        private void ledPreview1_MouseUp(object sender, MouseEventArgs e)
        {
            //If the mode is set to static colours
            if(staticColoursBackgroundRadioButton.Checked)
            {
                //Copy the current static colours for saving on exit
                SettingsManager.StaticColours = (Color[]) this.ledPreview1.GetPixels().Clone();

                UInt32 pixelIndex = 0;

                //Set the LED colours to the static colours
                foreach(Color pixel in SettingsManager.StaticColours)
                {
                    OutputManager.SetLED(pixel, pixelIndex++);
                }

                //Send the LED colours to the LED strip
                OutputManager.FlushColours();
            }
        }
        #endregion

        #region Script Functions
        private void StartActiveScript(String scriptDirectory)
        {
            //In order to load and unload the script DLLs at runtime,
            //the DLLs need to be loaded into a seperate appdomain...
            //which we create here
            mScriptAppDomain = AppDomain.CreateDomain("TaskerLightScriptDomain");

            //Create an instance of the script loader which will give access to the script dll
            mLoader = (ScriptLoader) mScriptAppDomain.CreateInstanceAndUnwrap(
                                                typeof(ScriptLoader).Assembly.FullName,
                                                typeof(ScriptLoader).FullName);

            //Load the script DLL into the appdomain
            mLoader.LoadAssembly(scriptDirectory + "\\script.dll");

            //All the scripts use "number of ticks passed" to time their effects
            //To do this they need to know the number of ticks that represents
            //the time at which they started
            mInitialMS = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            //Start the script polling timer
            activeAppTimer.Start();
        }

        private void StopActiveScript()
        {
            //Stop updating the active script
            activeAppTimer.Stop();

            //If an appdomain has been created containing the active script
            if(null != mScriptAppDomain)
            {
                try
                {
                    //Unplug our active script DLL
                    AppDomain.Unload(mScriptAppDomain);
                }
                catch(AppDomainUnloadedException)
                {

                }
            }
        }

        private void newScriptSelected(object sender, EventArgs e)
        {
            //Stop the previous active script
            StopActiveScript();

            //Start the newly selected script
            StartActiveScript(((RadioButton) sender).Tag.ToString());
        }

        private void newScriptButton_Click(object sender, EventArgs e)
        {
            //Show a dialog containing a new active script to be compiled
            mCompilerForm.ShowDialog();

            //A new script may have been added to the start of the library
            //so start the first active scene found in the list
            activeSceneModeRadioButton_CheckedChanged(this, EventArgs.Empty);
        }

        private void editScriptButton_Click(object sender, EventArgs e)
        {
            //For each script listed on the display
            foreach (RadioButton scriptButton in scriptPanel.Controls)
            {
                //If the script is selected as the current script
                if(scriptButton.Checked)
                {
                    //Stop running the selected script
                    StopActiveScript();

                    //Load the script up from its source code file
                    String loadedScriptCode = File.ReadAllText(scriptButton.Tag.ToString() + "\\script.cs");

                    //Show the source code in the compiler dialog
                    CompilerForm compilerForm = new CompilerForm(scriptButton.Text, loadedScriptCode);
                    compilerForm.ShowDialog();

                    //Start showing the edited script
                    StartActiveScript(scriptButton.Tag.ToString());

                    break;
                }
            }
        }
        #endregion
        
        #region Mode RadioButton Change Functions
        private void staticColoursBackgroundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.staticColoursToolStripMenuItem.Checked = staticColoursBackgroundRadioButton.Checked;

            if(staticColoursBackgroundRadioButton.Checked)
            {
                for (UInt32 pixelIndex = 0; pixelIndex < SettingsManager.StaticColours.Length; ++pixelIndex)
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
        
        private void activeSceneModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.activeSceneToolStripMenuItem.Checked = this.activeSceneModeRadioButton.Checked;
            scriptPanel.Visible = activeSceneModeRadioButton.Checked;

            if(activeSceneModeRadioButton.Checked)
            {
                scriptPanel.Controls.Clear();

                int yPos = 2;

                bool colourBackground = false;
                bool firstScript = true;

                foreach(String directory in Directory.GetDirectories("./"))
                {
                    DirectoryInfo di = new DirectoryInfo(directory);
                    RadioButton scriptButton = new RadioButton();
                    scriptButton.CheckedChanged += new EventHandler(newScriptSelected);
                    scriptButton.Tag = di.FullName;
                    scriptButton.Text = di.Name;
                    scriptButton.TextAlign = ContentAlignment.MiddleLeft;
                    scriptButton.Location = new Point(5, yPos);
                    scriptButton.Size = new Size(305, scriptButton.Height);
                    
                    if(firstScript)
                    {
                        scriptButton.Checked = true;
                        firstScript = false;
                    }

                    if(colourBackground)
                    {
                        scriptButton.BackColor = Color.FromArgb(215, 215, 215);
                    }
                    colourBackground = !colourBackground;

                    scriptPanel.Controls.Add(scriptButton);

                    yPos += 24;
                }

                SettingsManager.Mode = 1;
            }
            else
            {
                activeAppTimer.Stop();

                if(null != mScriptAppDomain)
                {
                    AppDomain.Unload(mScriptAppDomain);
                }
            }

            this.newScriptButton.Visible = activeSceneModeRadioButton.Checked;
            this.editScriptButton.Visible = activeSceneModeRadioButton.Checked;
        }
        
        private void wallpaperBackgroundModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(wallpaperBackgroundModeRadioButton.Checked)
            {
                wallpaperEffectGenerator.Start();

                SettingsManager.Mode = 2;
            }
            else
            {
                wallpaperEffectGenerator.Stop();
            }
        }

        private void capturedBackgroundModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.activeSceneToolStripMenuItem.Checked = this.capturedBackgroundModeRadioButton.Checked;

            if(capturedBackgroundModeRadioButton.Checked)
            {
                mVideoEffectGenerator.Start();

                SettingsManager.Mode = 3;
            }
            else
            {
                mVideoEffectGenerator.Stop();
            }
        }
        #endregion

        #region Timer Tick Functions
        private void activeAppTimer_Tick(object sender, EventArgs e)
        {
            long millisecondDifference = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            millisecondDifference -= mInitialMS;

            Color[] colours = new Color[25];
            colours = (Color[])mLoader.ExecuteStaticMethod("TaskerLightScript",
                                                           "TickLighting",
                                                           millisecondDifference);

            for (UInt32 i = 0; i < 25; ++i)
            {
                Rectangle region = RegionManager.Instance().GetRegion(i);

                this.ledPreview1.SetPixel(OutputManager.SetLED(colours[i], i), i);
            }

            this.ledPreview1.Refresh();
            OutputManager.FlushColours();
        }

        #endregion

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
            }
        }

        internal void AddActiveApp(ProcessIndex processIndex)
        {
            FileInfo exeFile = new FileInfo(processIndex.exeName);

            Icon icon = Icon.ExtractAssociatedIcon(processIndex.exeName);
            mAppImageList.Images.Add(icon);

            processIndex.processName = Path.GetFileNameWithoutExtension(exeFile.FullName);

            ListViewItem appItem = new ListViewItem(Path.GetFileNameWithoutExtension(exeFile.FullName));
            appItem.Tag = processIndex;
            appItem.ImageIndex = this.mAppImageList.Images.Count - 1;

            this.activeAppListView.Items.Add(appItem);
        }

        private void addAppButton_Click(object sender, EventArgs e)
        {
            if(mOpenExeDialog.ShowDialog() == DialogResult.OK)
            {
                ProcessIndex processIndex = new ProcessIndex();
                processIndex.exeName = mOpenExeDialog.FileName;
                AddActiveApp(processIndex);
                mVideoEffectGenerator.SetActiveApps(activeAppListView.Items);
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

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
            {
                Hide();
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        #region Settings Functions
        private void arduinoComPortUpDown_ValueChanged(object sender, EventArgs e)
        {
            ShutdownArduinoComms();
            InitialiseArduinoComms("\\\\.\\com" + arduinoComPortUpDown.Value.ToString());
        }

        private void oversaturationTrackbar_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.Saturation = oversaturationTrackbar.Value / 100.0f;
        }

        private void contrastTrackbar_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.Contrast = contrastTrackbar.Value / 100.0f;
        }
        #endregion

        private void staticColoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.staticColoursBackgroundRadioButton.Checked = true;

            activeSceneToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
            videoCaptureToolStripMenuItem.Checked = false;
        }

        private void activeSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.activeSceneModeRadioButton.Checked = true;

            staticColoursToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
            videoCaptureToolStripMenuItem.Checked = false;
        }

        private void wallpaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.wallpaperBackgroundModeRadioButton.Checked = true;

            staticColoursToolStripMenuItem.Checked = false;
            activeSceneToolStripMenuItem.Checked = false;
            videoCaptureToolStripMenuItem.Checked = false;
        }

        private void videoCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.capturedBackgroundModeRadioButton.Checked = true;

            staticColoursToolStripMenuItem.Checked = false;
            activeSceneToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.WindowState = FormWindowState.Minimized;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            AttemptConnection();
        }

        private void ledPreviewTimer_Tick(object sender, EventArgs e)
        {
            ledPreview1.Refresh();
        }
    }
}
