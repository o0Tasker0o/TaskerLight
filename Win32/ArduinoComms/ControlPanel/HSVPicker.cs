using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlPanel
{
    public partial class HSVPicker : UserControl
    {
        Point mSatValPoint;
        int mHueLevel;

        public event EventHandler<EventArgs> ColourChanged;

        public HSVPicker()
        {
            InitializeComponent();

            mHueLevel = this.hueSlider.Height;
            mSatValPoint = new Point(this.Width, 0);
        }

        private void saturationValueBox_Paint(object sender, PaintEventArgs e)
        {
            Pen crossHairPen = new Pen(Color.Gray);

            int crosshairSize = 4;

            e.Graphics.DrawLine(crossHairPen,
                                mSatValPoint.X - crosshairSize, mSatValPoint.Y,
                                mSatValPoint.X + crosshairSize, mSatValPoint.Y);

            e.Graphics.DrawLine(crossHairPen,
                                mSatValPoint.X, mSatValPoint.Y - crosshairSize,
                                mSatValPoint.X, mSatValPoint.Y + crosshairSize);
        }

        private void hueSlider_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.White),
                                0, mHueLevel,
                                this.hueSlider.Width, mHueLevel);

            e.Graphics.DrawLine(new Pen(Color.Black),
                                0, mHueLevel - 1,
                                this.hueSlider.Width, mHueLevel - 1);

            e.Graphics.DrawLine(new Pen(Color.Black),
                                0, mHueLevel + 1,
                                this.hueSlider.Width, mHueLevel + 1);
        }

        private void saturationValueBox_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.None)
            {
                int xPos = e.X;
                int yPos = e.Y;

                if(xPos > this.saturationValueBox.Width)
                {
                    xPos = this.saturationValueBox.Width;
                }
                else if (xPos < 0)
                {
                    xPos = 0;
                }

                if(yPos > this.saturationValueBox.Height)
                {
                    yPos = this.saturationValueBox.Height;
                }
                else if (yPos < 0)
                {
                    yPos = 0;
                }

                mSatValPoint = new Point(xPos, yPos);
                this.saturationValueBox.Refresh();

                this.OnColourChanged(EventArgs.Empty);
            }
        }

        private void saturationValueBox_MouseClick(object sender, MouseEventArgs e)
        {
            saturationValueBox_MouseMove(sender, e);
        }

        private void hueSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.None)
            {
                mHueLevel = e.Y;

                if(mHueLevel > this.hueSlider.Height)
                {
                    mHueLevel = this.hueSlider.Height;
                }
                else if(mHueLevel < 0)
                {
                    mHueLevel = 0;
                }

                float hue = (float)this.mHueLevel / (float)this.hueSlider.Height;

                this.saturationValueBox.BackColor = ColourUtil.HSVToRGB(1.0f - hue, 1.0f, 1.0f);

                this.hueSlider.Refresh();

                this.OnColourChanged(EventArgs.Empty);
            }
        }

        private void hueSlider_MouseClick(object sender, MouseEventArgs e)
        {
            hueSlider_MouseMove(sender, e);
        }

        internal Color GetColour()
        {
            float hue = (float) this.mHueLevel / (float) this.hueSlider.Height;
            float saturation = (float) this.mSatValPoint.Y / (float) this.saturationValueBox.Height;
            float value = (float) this.mSatValPoint.X / (float) this.saturationValueBox.Width;
            return ColourUtil.HSVToRGB(1.0f - hue, 1.0f - saturation, value);
        }

        protected virtual void OnColourChanged(EventArgs e)
        {
            EventHandler<EventArgs> handler = ColourChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
