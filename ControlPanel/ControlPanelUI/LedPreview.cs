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
            
            refreshTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (null != ColourOutputManager)
            {
                for(UInt16 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(pixelIndex)),
                                             PixelRegions.Instance.GetRegion(pixelIndex));
                }
            }
            else
            {
                for (UInt16 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black),
                                             PixelRegions.Instance.GetRegion(pixelIndex));
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
                for(UInt16 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    if (PixelRegions.Instance.GetRegion(pixelIndex).Contains(e.Location))
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
