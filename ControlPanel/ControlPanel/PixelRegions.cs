using System;
using System.Drawing;
using System.Collections.Generic;
namespace ControlPanel
{
    public class PixelRegions
    {
        private static PixelRegions mInstance;

        private List<Rectangle> mRegions;

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
    }
}
