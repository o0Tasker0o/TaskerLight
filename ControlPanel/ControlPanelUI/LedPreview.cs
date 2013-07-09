using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlPanel;
using System.Threading;

namespace ControlPanelUI
{
    public partial class LedPreview : UserControl
    {
        private ColourOutputManager mColourOutputManager;
        private Rectangle[] mPixelRegions;
        private bool mAllowInput;

        public ColourOutputManager ColourOutputManager
        {
            private get
            {
                return mColourOutputManager;
            }
            set
            {
                mColourOutputManager = value;
            }
        }

        public bool AllowInput
        {
            get
            {
                return mAllowInput;
            }
            set
            {
                mAllowInput = value;

                if(mAllowInput)
                {
                    ColourOutputManager.FadeTimeMs = 1000;
                    refreshTimer.Interval = 1000;
                }
                else
                {
                    refreshTimer.Interval = 100;
                }
            }
        }

        public Color InputColour
        {
            get;
            set;
        }

        public LedPreview()
        {
            InitializeComponent();

            AllowInput = false;

            mPixelRegions = new Rectangle[25];
            mPixelRegions[0] = new Rectangle(60, 60, 10, 10);
            mPixelRegions[1] = new Rectangle(70, 60, 10, 10);
            mPixelRegions[2] = new Rectangle(80, 60, 10, 10);
            mPixelRegions[3] = new Rectangle(80, 50, 10, 10);
            mPixelRegions[4] = new Rectangle(80, 40, 10, 10);
            mPixelRegions[5] = new Rectangle(80, 30, 10, 10);
            mPixelRegions[6] = new Rectangle(80, 20, 10, 10);
            mPixelRegions[7] = new Rectangle(80, 10, 10, 10);
            mPixelRegions[8] = new Rectangle(80, 0, 10, 10);
            mPixelRegions[9] = new Rectangle(70, 0, 10, 10);
            mPixelRegions[10] = new Rectangle(60, 0, 10, 10);
            mPixelRegions[11] = new Rectangle(50, 0, 10, 10);
            mPixelRegions[12] = new Rectangle(40, 0, 10, 10);
            mPixelRegions[13] = new Rectangle(30, 0, 10, 10);
            mPixelRegions[14] = new Rectangle(20, 0, 10, 10);
            mPixelRegions[15] = new Rectangle(10, 0, 10, 10);
            mPixelRegions[16] = new Rectangle(0, 0, 10, 10);
            mPixelRegions[17] = new Rectangle(0, 10, 10, 10);
            mPixelRegions[18] = new Rectangle(0, 20, 10, 10);
            mPixelRegions[19] = new Rectangle(0, 30, 10, 10);
            mPixelRegions[20] = new Rectangle(0, 40, 10, 10);
            mPixelRegions[21] = new Rectangle(0, 50, 10, 10);
            mPixelRegions[22] = new Rectangle(0, 60, 10, 10);
            mPixelRegions[23] = new Rectangle(10, 60, 10, 10);
            mPixelRegions[24] = new Rectangle(20, 60, 10, 10);

            refreshTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (null != ColourOutputManager)
            {
                for(UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(pixelIndex)),
                                             mPixelRegions[pixelIndex]);
                }
            }
            else
            {
                for (UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black),
                                             mPixelRegions[pixelIndex]);
                }
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Refresh();

            if(AllowInput)
            {
                ColourOutputManager.FlushColours();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (AllowInput)
            {
                for(UInt32 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    if (mPixelRegions[pixelIndex].Contains(e.Location))
                    {
                        ColourOutputManager.SetPixel(pixelIndex, InputColour);
                    }
                }

                Refresh();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if(e.Button != MouseButtons.None)
            {
                OnMouseDown(e);
            }
        }
    }
}
