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

            mWallpaperEffectGenerator = new WallpaperEffectGenerator(mColourOutputManager);
            mActiveScriptEffectGenerator = new ActiveScriptEffectGenerator(mColourOutputManager);
            mVideoEffectGenerator = new VideoEffectGenerator(mColourOutputManager);

            mWallpaperEffectGenerator.Start();
        }

        private void modeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            mWallpaperEffectGenerator.Stop();
            mActiveScriptEffectGenerator.Stop();
            mVideoEffectGenerator.Stop();

            if(wallpaperRadioButton.Checked)
            {
                mWallpaperEffectGenerator.Start();
            }
            else if(activeScriptRadioButton.Checked)
            {
                mActiveScriptEffectGenerator.Start();
            }
            else if(videoRadioButton.Checked)
            {
                mVideoEffectGenerator.Start();
            }
        }

        private void ControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            mWallpaperEffectGenerator.Stop();
            mActiveScriptEffectGenerator.Stop();
            mVideoEffectGenerator.Stop();
        }
    }
}
