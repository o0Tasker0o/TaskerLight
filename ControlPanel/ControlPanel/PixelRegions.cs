using System;
using System.Drawing;
using System.Collections.Generic;
namespace ControlPanel
{
    public class PixelRegions
    {
        private static PixelRegions mInstance;

        private List<Rectangle> mRegions;
        private List<Rectangle> mCaptureSubRegions;

        public Rectangle CaptureRegion
        {
            get;
            set;
        }

        public static PixelRegions Instance
        {
            get
            {
                if(null == mInstance)
                {
                    mInstance = new PixelRegions();
                }

                return mInstance;
            }
        }

        private PixelRegions() 
        {
            mRegions = new List<Rectangle>();
            mRegions.Add(new Rectangle(60, 60, 10, 10));
            mRegions.Add(new Rectangle(70, 60, 10, 10));
            mRegions.Add(new Rectangle(80, 60, 10, 10));
            mRegions.Add(new Rectangle(80, 50, 10, 10));
            mRegions.Add(new Rectangle(80, 40, 10, 10));
            mRegions.Add(new Rectangle(80, 30, 10, 10));
            mRegions.Add(new Rectangle(80, 20, 10, 10));
            mRegions.Add(new Rectangle(80, 10, 10, 10));
            mRegions.Add(new Rectangle(80, 0, 10, 10));
            mRegions.Add(new Rectangle(70, 0, 10, 10));
            mRegions.Add(new Rectangle(60, 0, 10, 10));
            mRegions.Add(new Rectangle(50, 0, 10, 10));
            mRegions.Add(new Rectangle(40, 0, 10, 10));
            mRegions.Add(new Rectangle(30, 0, 10, 10));
            mRegions.Add(new Rectangle(20, 0, 10, 10));
            mRegions.Add(new Rectangle(10, 0, 10, 10));
            mRegions.Add(new Rectangle(0, 0, 10, 10));
            mRegions.Add(new Rectangle(0, 10, 10, 10));
            mRegions.Add(new Rectangle(0, 20, 10, 10));
            mRegions.Add(new Rectangle(0, 30, 10, 10));
            mRegions.Add(new Rectangle(0, 40, 10, 10));
            mRegions.Add(new Rectangle(0, 50, 10, 10));
            mRegions.Add(new Rectangle(0, 60, 10, 10));
            mRegions.Add(new Rectangle(10, 60, 10, 10));
            mRegions.Add(new Rectangle(20, 60, 10, 10));

            CaptureRegion = new Rectangle(0, 0, 1920, 1080);

            mCaptureSubRegions = new List<Rectangle>();
            mCaptureSubRegions.Add(new Rectangle(6, 6, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(7, 6, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(8, 6, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(8, 5, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(8, 4, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(8, 3, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(8, 2, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(8, 1, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(8, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(7, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(6, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(5, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(4, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(3, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(2, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(1, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(0, 0, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(0, 1, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(0, 2, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(0, 3, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(0, 4, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(0, 5, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(0, 6, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(1, 6, 1, 1));
            mCaptureSubRegions.Add(new Rectangle(2, 6, 1, 1));
        }

        public UInt16 RegionCount
        {
            get
            {
                return (UInt16) mRegions.Count;
            }
        }

        public Rectangle GetRegion(UInt16 regionIndex)
        {
            if(regionIndex >= mRegions.Count)
            {
                return new Rectangle(0, 0, 0, 0);
            }

            return mRegions[regionIndex];
        }

        public Rectangle GetCaptureSubRegion(UInt16 regionIndex)
        {
            Rectangle scaledSubRegion = mCaptureSubRegions[regionIndex];

            scaledSubRegion.Location = new Point(CaptureRegion.Left + ((scaledSubRegion.Left * CaptureRegion.Width) / 9),
                                                 CaptureRegion.Top + ((scaledSubRegion.Top * CaptureRegion.Height) / 7));
            scaledSubRegion.Width = (scaledSubRegion.Width * CaptureRegion.Width) / 9;
            scaledSubRegion.Height = (scaledSubRegion.Height * CaptureRegion.Height) / 7;
            
            return scaledSubRegion;
        }
    }
}
