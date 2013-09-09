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

        public ControlPanel()
        {
            InitializeComponent();
                        
            mSerialCommunicator = new SerialCommunicator();
            mColourOutputManager = new ColourOutputManager(mSerialCommunicator);

            SettingsManager.Load("./settings.xml");

            saturationTrackbar.Value = SettingsManager.OutputSaturation;
            contrastTrackbar.Value = SettingsManager.OutputContrast;

            ledPreview1.ColourOutputManager = mColourOutputManager;

            mWallpaperEffectGenerator = new WallpaperEffectGenerator(mColourOutputManager);
            mActiveScriptEffectGenerator = new ActiveScriptEffectGenerator(mColourOutputManager);
            mVideoEffectGenerator = new VideoEffectGenerator(mColourOutputManager);

            mWallpaperEffectGenerator.Start();
        }

        private void ControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {            
            mWallpaperEffectGenerator.Stop();
            mActiveScriptEffectGenerator.Stop();
            mVideoEffectGenerator.Stop();

            SettingsManager.Save("./settings.xml");
        }

        private void staticColourRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = true;
            activeScriptToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = false;

            ledPreview1.AllowInput = staticColourRadioButton.Checked;
            hsvPicker1.Visible = staticColourRadioButton.Checked;
        }

        private void activeScriptRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = false;
            activeScriptToolStripMenuItem.Checked = activeScriptRadioButton.Checked;
            wallpaperToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = false;

            if (activeScriptRadioButton.Checked)
            {
                activeScriptBrowserControl1.Visible = true;
                mActiveScriptEffectGenerator.Start();
            }
            else
            {
                activeScriptBrowserControl1.Visible = false;
                mActiveScriptEffectGenerator.Stop();
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
                mWallpaperEffectGenerator.Start();
            }
            else
            {
                mWallpaperEffectGenerator.Stop();
            }
        }

        private void videoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            staticColoursToolStripMenuItem.Checked = false;
            activeScriptToolStripMenuItem.Checked = false;
            wallpaperToolStripMenuItem.Checked = false;
            videoToolStripMenuItem.Checked = videoRadioButton.Checked;

            if (videoRadioButton.Checked)
            {
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
    }
}
