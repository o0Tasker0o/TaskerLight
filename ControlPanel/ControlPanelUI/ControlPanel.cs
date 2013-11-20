using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlPanel;
using System.IO;

namespace ControlPanelUI
{
    public partial class ControlPanel : Form
    {
        private WallpaperEffectGenerator mWallpaperEffectGenerator;
        private ActiveScriptEffectGenerator mActiveScriptEffectGenerator;
        private VideoEffectGenerator mVideoEffectGenerator;

        private ColourOutputManager mColourOutputManager;
        private SerialCommunicator mSerialCommunicator;
        private ApplicationFinder mApplicationFinder;

        private readonly String cSettingsFilepath;

        public ControlPanel()
        {
            InitializeComponent();

            cSettingsFilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                             "taskerlight settings.xml");

            mSerialCommunicator = new SerialCommunicator();
            mColourOutputManager = new ColourOutputManager(mSerialCommunicator);

            SettingsManager.Load(cSettingsFilepath);

            saturationTrackbar.Value = SettingsManager.OutputSaturation;
            contrastTrackbar.Value = SettingsManager.OutputContrast;

            ledPreview1.ColourOutputManager = mColourOutputManager;

            mApplicationFinder = new ApplicationFinder();

            foreach (String videoApp in SettingsManager.VideoApps)
            {
                AddVideoApp(videoApp);
            }

            mWallpaperEffectGenerator = new WallpaperEffectGenerator(mColourOutputManager);
            mActiveScriptEffectGenerator = new ActiveScriptEffectGenerator(mColourOutputManager);
            mVideoEffectGenerator = new VideoEffectGenerator(mColourOutputManager, mApplicationFinder);

            switch(SettingsManager.Mode)
            {
                case OutputMode.StaticColours:
                    staticColourRadioButton.Checked = true;
                  break;
                case OutputMode.ActiveScript:
                    activeScriptRadioButton.Checked = true;
                  break;
                case OutputMode.Wallpaper:
                    wallpaperRadioButton.Checked = true;
                  break;
                case OutputMode.Video:
                    videoRadioButton.Checked = true;
                  break;
            }

            videoOverlayCheckbox.Checked = SettingsManager.VideoOverlay;
        }

        private void ControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {            
            mWallpaperEffectGenerator.Stop();
            mActiveScriptEffectGenerator.Stop();
            mVideoEffectGenerator.Stop();

            SettingsManager.Save(cSettingsFilepath);
        }

        private void staticColourRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = true;
            activeScriptToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = false;
            hsvPicker1.Visible = staticColourRadioButton.Checked;

            if(staticColourRadioButton.Checked)
            {
                SettingsManager.Mode = OutputMode.StaticColours;

                StartStaticColourMode();
            }
        }

        private void StartStaticColourMode()
        {
            if(!(mApplicationFinder.RunningRegisteredApplications() && videoOverlayCheckbox.Checked))
            {
                ledPreview1.AllowInput = true;

                ledPreview1.RefreshInterval = 1000;
                mColourOutputManager.FadeTimeMs = 1000;

                for (UInt16 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    mColourOutputManager.SetPixel(pixelIndex, ledPreview1.StaticPixelColours[pixelIndex]);
                }
            }
        }

        private void activeScriptRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            deleteScriptButton.Visible = activeScriptRadioButton.Checked;
            addScriptButton.Visible = activeScriptRadioButton.Checked;
            staticColoursToolStripMenuItem.Checked = false;
            activeScriptToolStripMenuItem.Checked = activeScriptRadioButton.Checked;
            wallpaperToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = false;

            if (activeScriptRadioButton.Checked)
            {
                SettingsManager.Mode = OutputMode.ActiveScript;

                activeScriptBrowserControl1.Visible = true;
                StartActiveScriptMode();
            }
            else
            {
                activeScriptBrowserControl1.Visible = false;
                mActiveScriptEffectGenerator.Stop();
            }
        }

        private void StartActiveScriptMode()
        {
            if(!(mApplicationFinder.RunningRegisteredApplications() && videoOverlayCheckbox.Checked))
            {
                ledPreview1.AllowInput = false;

                ledPreview1.RefreshInterval = 180;
                mActiveScriptEffectGenerator.Start();
            }
        }

        private void wallpaperRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = false;
            activeScriptToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = wallpaperRadioButton.Checked;
            videoToolStripMenuItem.Checked = false;

            if (wallpaperRadioButton.Checked)
            {
                SettingsManager.Mode = OutputMode.Wallpaper;

                StartWallpaperMode();
            }
            else
            {
                mWallpaperEffectGenerator.Stop();
            }
        }

        private void StartWallpaperMode()
        {
            if (!(mApplicationFinder.RunningRegisteredApplications() && videoOverlayCheckbox.Checked))
            {
                ledPreview1.AllowInput = false;

                ledPreview1.RefreshInterval = 1000;
                mWallpaperEffectGenerator.Start();
            }
        }

        private void videoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = false;
            activeScriptToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = videoRadioButton.Checked;

            addVideoAppButton.Visible = videoRadioButton.Checked;
            deleteVideoAppButton.Visible = videoRadioButton.Checked;
            videoAppListView.Visible = videoRadioButton.Checked;

            if (videoRadioButton.Checked)
            {
                SettingsManager.Mode = OutputMode.Video;

                ledPreview1.RefreshInterval = 100;
                ledPreview1.AllowInput = false;
                mVideoEffectGenerator.Start();
            }
            else
            {
                mVideoEffectGenerator.Stop();
            }
        }

        private void activeScriptBrowserControl1_ScriptSelectionChanged(DirectoryInfo scriptDirectory)
        {
            mActiveScriptEffectGenerator.Stop();
            mActiveScriptEffectGenerator.CurrentScriptDirectory = scriptDirectory;
            mActiveScriptEffectGenerator.Start();

            deleteScriptButton.Enabled = !scriptDirectory.FullName.StartsWith(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void hsvPicker1_ColourChanged(object sender, EventArgs e)
        {
            ledPreview1.InputColour = hsvPicker1.Colour;
        }

        private void saturationTrackbar_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.OutputSaturation = (UInt16) saturationTrackbar.Value;
            mColourOutputManager.SaturationMultiplier = (float) saturationTrackbar.Value / 100.0f;
        }

        private void contrastTrackbar_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.OutputContrast = (UInt16) contrastTrackbar.Value;
            mColourOutputManager.ContrastMultiplier = (float) contrastTrackbar.Value / 100.0f;
        }

        private void ControlPanel_Resize(object sender, EventArgs e)
        {
            if(FormWindowState.Minimized == WindowState)
            {
                Hide();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void staticColoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeScriptToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = false;

            staticColourRadioButton.Checked = true;
        }

        private void activeScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = false;

            activeScriptRadioButton.Checked = true;
        }

        private void wallpaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = false;
            activeScriptToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = false;

            wallpaperRadioButton.Checked = true;
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = false;
            activeScriptToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;

            videoRadioButton.Checked = true;
        }

        private void addScriptButton_Click(object sender, EventArgs e)
        {
            ScriptCompilerForm compilerForm = new ScriptCompilerForm();

            compilerForm.ShowDialog();
        }

        private void deleteScriptButton_Click(object sender, EventArgs e)
        {
            mActiveScriptEffectGenerator.Stop();
            mActiveScriptEffectGenerator.CurrentScriptDirectory = null;

            activeScriptBrowserControl1.SelectedScriptDirectory.Delete(true);
        }

        private void deleteVideoAppButton_Click(object sender, EventArgs e)
        {
            String exeName = (String) videoAppListView.SelectedItems[0].Tag;

            mApplicationFinder.UnregisterApplication(new FileInfo(exeName));

            videoAppListView.SelectedItems[0].Remove();
        }

        private void addVideoAppButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openExeDialog = new OpenFileDialog();

            openExeDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            openExeDialog.Filter = "Executables (*.exe)|*.exe";

            if (openExeDialog.ShowDialog() == DialogResult.OK)
            {
                AddVideoApp(openExeDialog.FileName);

                SettingsManager.VideoApps.Add(openExeDialog.FileName);
            }
        }

        private void AddVideoApp(String exeFilename)
        {
            Icon icon = Icon.ExtractAssociatedIcon(exeFilename);

            videoAppIconList.Images.Add(icon);

            ListViewItem appItem = new ListViewItem(Path.GetFileNameWithoutExtension(exeFilename));
            appItem.Tag = exeFilename;
            appItem.ImageIndex = this.videoAppIconList.Images.Count - 1;

            videoAppListView.Items.Add(appItem);

            mApplicationFinder.RegisterApplication(new FileInfo(exeFilename));
        }

        private void videoAppListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            deleteVideoAppButton.Enabled = videoAppListView.SelectedItems.Count > 0;
        }

        private void videoAppPollTimer_Tick(object sender, EventArgs e)
        {
            if(mApplicationFinder.RunningRegisteredApplications())
            {
                switch (SettingsManager.Mode)
                {
                    case OutputMode.StaticColours:
                        ledPreview1.AllowInput = false;
                        break;
                    case OutputMode.ActiveScript:
                        mActiveScriptEffectGenerator.Stop();
                        break;
                    case OutputMode.Wallpaper:
                        mWallpaperEffectGenerator.Stop();
                        break;
                }

                ledPreview1.RefreshInterval = 100;
                mVideoEffectGenerator.Start();
            }
            else
            {
                StopVideoOverlay();
            }
        }

        private void videoOverlayCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.VideoOverlay = videoOverlayCheckbox.Checked;

            if(videoOverlayCheckbox.Checked)
            {
                videoAppPollTimer.Start();
            }
            else
            {
                videoAppPollTimer.Stop();
                StopVideoOverlay();
            }
        }

        private void StopVideoOverlay()
        {
            switch (SettingsManager.Mode)
            {
                case OutputMode.StaticColours:
                    mVideoEffectGenerator.Stop();
                    StartStaticColourMode();
                    break;
                case OutputMode.ActiveScript:
                    mVideoEffectGenerator.Stop();
                    StartActiveScriptMode();
                    break;
                case OutputMode.Wallpaper:
                    mVideoEffectGenerator.Stop();
                    StartWallpaperMode();
                    break;
            }
        }
    }
}
