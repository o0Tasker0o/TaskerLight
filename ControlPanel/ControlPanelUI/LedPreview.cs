using System;
using System.Drawing;
using System.Windows.Forms;
using ControlPanel;

namespace ControlPanelUI
{
    public partial class LedPreview : UserControl
    {
        private ColourOutputManager mColourOutputManager;
        private bool mAllowInput;

        public Color[] StaticPixelColours
        {
            get;
            private set;
        }

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

            StaticPixelColours = SettingsManager.StaticColours;

            refreshTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (null != ColourOutputManager)
            {
                for(UInt16 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    RectangleF baseRegion = PixelRegions.Instance.GetRegion(pixelIndex);
                    Rectangle scaledRegion = new Rectangle((int) (baseRegion.X * Width),
                                                           (int) (baseRegion.Y * Height),
                                                           (int) (baseRegion.Width * Width),
                                                           (int) (baseRegion.Height * Height));
                    e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(pixelIndex)),
                                             scaledRegion);
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(64, 0, 0, 0)),
                                             scaledRegion);
                }
            }
            else
            {
                for (UInt16 pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
                {
                    RectangleF baseRegion = PixelRegions.Instance.GetRegion(pixelIndex);
                    Rectangle scaledRegion = new Rectangle((int)(baseRegion.X * Width),
                                                           (int)(baseRegion.Y * Height),
                                                           (int)(baseRegion.Width * Width),
                                                           (int)(baseRegion.Height * Height));

                    e.Graphics.FillRectangle(new SolidBrush(Color.Black),
                                             scaledRegion);
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(64, 0, 0, 0)),
                                             scaledRegion);
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
                    RectangleF baseRegion = PixelRegions.Instance.GetRegion(pixelIndex);
                    Rectangle scaledRegion = new Rectangle((int)(baseRegion.X * Width),
                                                           (int)(baseRegion.Y * Height),
                                                           (int)(baseRegion.Width * Width),
                                                           (int)(baseRegion.Height * Height));

                    if (scaledRegion.Contains(e.Location))
                    {
                        StaticPixelColours[pixelIndex] = InputColour;
                        SettingsManager.StaticColours[pixelIndex] = InputColour;
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
