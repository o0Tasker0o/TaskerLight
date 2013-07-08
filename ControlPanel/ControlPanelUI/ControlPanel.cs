using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlPanel;

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
        }

        private void activeScriptRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (activeScriptRadioButton.Checked)
            {
                mActiveScriptEffectGenerator.Start();
            }
            else
            {
                mActiveScriptEffectGenerator.Stop();
            }
        }

        private void wallpaperRadioButton_CheckedChanged(object sender, EventArgs e)
        {
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
            if (videoRadioButton.Checked)
            {
                mVideoEffectGenerator.Start();
            }
            else
            {
                mVideoEffectGenerator.Stop();
            }
        }
    }
}
