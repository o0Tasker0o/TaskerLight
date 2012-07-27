using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ControlPanel
{
    class RegionManager
    {
        private static RegionManager mInstance;

        private RectangleF [] mRegions;
        private float mXPos, mYPos;
        private float mFullWidth, mFullHeight;
        private float mRegionWidth, mRegionHeight;

        private RegionManager()
        {
            mRegions = new RectangleF[25];

            Resize();

            /*
            for (int i = 0; i < 25; ++i)
            {
                Form testForm = new Form();
                testForm.StartPosition = FormStartPosition.Manual;
                testForm.Location = new Point((int) mRegions[i].Left, (int) mRegions[i].Top);
                testForm.Size = new Size((int) mRegions[i].Width, (int) mRegions[i].Height);
                testForm.FormBorderStyle = FormBorderStyle.None;
                Label x = new Label();
                x.Text = i.ToString();
                testForm.Controls.Add(x);
                testForm.Show();
            }*/
        }

        public static RegionManager Instance()
        {
            if(mInstance == null)
            {
                mInstance = new RegionManager();
            }

            return mInstance;
        }

        private void SetRegions()
        {
            mRegions[0] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 2), mYPos + mFullHeight - mRegionHeight,
                                        mRegionWidth, mRegionHeight);

            mRegions[1] = new RectangleF(mXPos + mFullWidth - mRegionWidth, mYPos + mFullHeight - mRegionHeight,
                                        mRegionWidth, mRegionHeight);

            mRegions[2] = new RectangleF(mXPos + mFullWidth - mRegionWidth, mYPos + mFullHeight - (mRegionHeight * 2),
                                        mRegionWidth, mRegionHeight);

            mRegions[3] = new RectangleF(mXPos + mFullWidth - mRegionWidth, mYPos + mFullHeight - (mRegionHeight * 3),
                                        mRegionWidth, mRegionHeight);

            mRegions[4] = new RectangleF(mXPos + mFullWidth - mRegionWidth, mYPos + mFullHeight - (mRegionHeight * 4),
                                        mRegionWidth, mRegionHeight);

            mRegions[5] = new RectangleF(mXPos + mFullWidth - mRegionWidth, mYPos + mFullHeight - (mRegionHeight * 5),
                                        mRegionWidth, mRegionHeight);

            mRegions[6] = new RectangleF(mXPos + mFullWidth - mRegionWidth, mYPos + mFullHeight - (mRegionHeight * 6),
                                        mRegionWidth, mRegionHeight);

            mRegions[7] = new RectangleF(mXPos + mFullWidth - mRegionWidth, mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[8] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 2), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[9] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 3), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[10] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 4), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[11] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 5), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[12] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 6), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[13] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 7), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[14] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 8), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[15] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 9), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[16] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 10), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[17] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 11), mYPos + mFullHeight - (mRegionHeight * 7),
                                        mRegionWidth, mRegionHeight);

            mRegions[18] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 11), mYPos + mFullHeight - (mRegionHeight * 6),
                                        mRegionWidth, mRegionHeight);

            mRegions[19] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 11), mYPos + mFullHeight - (mRegionHeight * 5),
                                        mRegionWidth, mRegionHeight);

            mRegions[20] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 11), mYPos + mFullHeight - (mRegionHeight * 4),
                                        mRegionWidth, mRegionHeight);

            mRegions[21] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 11), mYPos + mFullHeight - (mRegionHeight * 3),
                                        mRegionWidth, mRegionHeight);

            mRegions[22] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 11), mYPos + mFullHeight - (mRegionHeight * 2),
                                        mRegionWidth, mRegionHeight);

            mRegions[23] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 11), mYPos + mFullHeight - mRegionHeight,
                                        mRegionWidth, mRegionHeight);

            mRegions[24] = new RectangleF(mXPos + mFullWidth - (mRegionWidth * 10), mYPos + mFullHeight - mRegionHeight,
                                         mRegionWidth, mRegionHeight);
        }

        internal void Resize()
        {
            mXPos = 0.0f;
            mYPos = 0.0f;
            mFullWidth = Screen.PrimaryScreen.Bounds.Width;
            mFullHeight = Screen.PrimaryScreen.Bounds.Height;
            mRegionWidth = mFullWidth / 11.0f;
            mRegionHeight = mFullHeight / 7.0f;

            SetRegions();
        }

        internal void Resize(Rectangle rect)
        {
            mXPos = rect.X;
            mYPos = rect.Y;
            mFullWidth = rect.Width;
            mFullHeight = rect.Height;
            mRegionWidth = mFullWidth / 11.0f;
            mRegionHeight = mFullHeight / 7.0f;

            SetRegions();
        }

        internal Rectangle GetRegion(UInt32 index)
        {
            if(index >= mRegions.Length)
            {
                return new Rectangle(0, 0, 0, 0);
            }

            return new Rectangle((int) mRegions[index].X, (int) mRegions[index].Y,
                                 (int) mRegions[index].Width, (int) mRegions[index].Height);
        }
    }
}
