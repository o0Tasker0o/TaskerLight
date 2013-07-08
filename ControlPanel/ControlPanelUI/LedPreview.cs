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

        public LedPreview()
        {
            InitializeComponent();

            refreshTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if(null != ColourOutputManager)
            {
                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(0)),
                                         new Rectangle(60, 60, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(1)),
                                         new Rectangle(70, 60, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(2)),
                                         new Rectangle(80, 60, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(3)),
                                         new Rectangle(80, 50, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(4)),
                                         new Rectangle(80, 40, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(5)),
                                         new Rectangle(80, 30, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(6)),
                                         new Rectangle(80, 20, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(7)),
                                         new Rectangle(80, 10, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(8)),
                                         new Rectangle(80, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(9)),
                                         new Rectangle(70, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(10)),
                                         new Rectangle(60, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(11)),
                                         new Rectangle(50, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(12)),
                                         new Rectangle(40, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(13)),
                                         new Rectangle(30, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(14)),
                                         new Rectangle(20, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(15)),
                                         new Rectangle(10, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(16)),
                                         new Rectangle(0, 0, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(17)),
                                         new Rectangle(0, 10, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(18)),
                                         new Rectangle(0, 20, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(19)),
                                         new Rectangle(0, 30, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(20)),
                                         new Rectangle(0, 40, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(21)),
                                         new Rectangle(0, 50, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(22)),
                                         new Rectangle(0, 60, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(23)),
                                         new Rectangle(10, 60, 10, 10));

                e.Graphics.FillRectangle(new SolidBrush(ColourOutputManager.GetPixel(24)),
                                         new Rectangle(20, 60, 10, 10));
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
