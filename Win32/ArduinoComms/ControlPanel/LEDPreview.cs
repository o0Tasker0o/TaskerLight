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
    public partial class LEDPreview : UserControl
    {        
        private readonly Rectangle [] mLEDRegions;
        private Color[] mLEDColours;
        private Color mActiveColour;

        internal Color ActiveColour
        {
            set { mActiveColour = value; }
        }

        public LEDPreview()
        {
            InitializeComponent();
            mActiveColour = Color.Red;

            const Int32 regionSize = 20;

            mLEDRegions = new Rectangle[25];
            mLEDColours = new Color[25];

            for (int i = 0; i < 25; ++i)
            {
                mLEDColours[i] = Color.Black;
            }

            int x = 180;
            int y = 120;

            mLEDRegions[0] = new Rectangle(x, y, regionSize, regionSize);
            x += regionSize;

            for (int i = 1; i < 8; ++i)
            {
                mLEDRegions[i] = new Rectangle(x, y, regionSize, regionSize);
                y -= regionSize;
            }

            y += regionSize;

            for (int i = 8; i < 17; ++i)
            {
                x -= regionSize;
                mLEDRegions[i] = new Rectangle(x, y, regionSize, regionSize);
            }

            x -= regionSize;

            for (int i = 17; i < 24; ++i)
            {
                mLEDRegions[i] = new Rectangle(x, y, regionSize, regionSize);
                y += regionSize;
            }

            x += regionSize;
            y -= regionSize;
            mLEDRegions[24] = new Rectangle(x, y, regionSize, regionSize);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                e.Graphics.FillRectangle(new SolidBrush(mLEDColours[pixelIndex]), 
                                         mLEDRegions[pixelIndex]);
            }
        }
        
        internal Color [] GetPixels()
        {
            return mLEDColours;
        }

        internal void SetPixel(Color color, UInt32 pixelIndex)
        {
            mLEDColours[pixelIndex] = color;
        }

        private void LEDPreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.None)
            {
                for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    Rectangle currentLED = mLEDRegions[pixelIndex];

                    if (e.X > currentLED.X && e.X < currentLED.X + currentLED.Width &&
                       e.Y > currentLED.Y && e.Y < currentLED.Y + currentLED.Height)
                    {
                        mLEDColours[pixelIndex] = mActiveColour;
                        break;
                    }
                }

                this.Refresh();
            }
        }

        private void LEDPreview_MouseDown(object sender, MouseEventArgs e)
        {
            LEDPreview_MouseMove(sender, e);
        }
    }
}
