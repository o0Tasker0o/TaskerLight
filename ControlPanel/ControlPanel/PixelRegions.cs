using System;
using System.Drawing;
using System.Collections.Generic;
namespace ControlPanel
{
    public class PixelRegions
    {
        private static PixelRegions mInstance;

        private List<RectangleF> mRegions;
        private List<RectangleF> mCaptureSubRegions;

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
            mRegions = new List<RectangleF>();
            mRegions.Add(new RectangleF(0.648f, 0.653f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.748f, 0.653f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.849f, 0.653f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.849f, 0.553f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.849f, 0.453f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.849f, 0.353f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.849f, 0.253f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.849f, 0.153f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.849f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.748f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.648f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.547f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.447f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.346f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.246f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.145f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.045f, 0.051f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.045f, 0.153f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.045f, 0.253f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.045f, 0.353f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.045f, 0.453f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.045f, 0.553f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.045f, 0.653f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.145f, 0.653f, 0.105f, 0.104f));
            mRegions.Add(new RectangleF(0.246f, 0.653f, 0.105f, 0.104f));

            CaptureRegion = new Rectangle(0, 0, 1920, 1080);

            mCaptureSubRegions = new List<RectangleF>();
            mCaptureSubRegions.Add(new RectangleF(0.667f, 0.857f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.778f, 0.857f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.889f, 0.857f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.889f, 0.714f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.889f, 0.572f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.889f, 0.428f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.889f, 0.286f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.889f, 0.142f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.889f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.778f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.667f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.555f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.444f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.333f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.222f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.111f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.000f, 0.00f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.000f, 0.142f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.000f, 0.286f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.000f, 0.428f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.000f, 0.572f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.000f, 0.714f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.000f, 0.857f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.111f, 0.857f, 0.111f, 0.143f));
            mCaptureSubRegions.Add(new RectangleF(0.222f, 0.857f, 0.111f, 0.143f));
        }

        public UInt16 RegionCount
        {
            get
            {
                return (UInt16) mRegions.Count;
            }
        }

        public RectangleF GetRegion(UInt16 regionIndex)
        {
            if(regionIndex >= mRegions.Count)
            {
                return new RectangleF(0, 0, 0, 0);
            }

            return mRegions[regionIndex];
        }

        public Rectangle GetCaptureSubRegion(UInt16 regionIndex)
        {
            RectangleF scaledSubRegion = mCaptureSubRegions[regionIndex];

            scaledSubRegion.Location = new PointF(CaptureRegion.Left + (scaledSubRegion.Left * CaptureRegion.Width),
                                                  CaptureRegion.Top + (scaledSubRegion.Top * CaptureRegion.Height));
            scaledSubRegion.Width = (scaledSubRegion.Width * CaptureRegion.Width);
            scaledSubRegion.Height = (scaledSubRegion.Height * CaptureRegion.Height);

            return new Rectangle((int) scaledSubRegion.X, 
                                 (int) scaledSubRegion.Y, 
                                 (int) scaledSubRegion.Width, 
                                 (int) scaledSubRegion.Height);
        }
    }
}
