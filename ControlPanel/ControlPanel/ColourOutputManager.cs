using System;
using System.Drawing;

namespace ControlPanel
{
    public class ColourOutputManager : IDisposable
    {
        private readonly ISerialCommunicator mSerialCommunicator;
        private readonly Color [] mColourBuffer;

        public UInt16 FadeTimeMs
        {
            get;
            set;
        }

        public float SaturationMultiplier
        {
            get;
            set;
        }

        public float ContrastMultiplier
        {
            get;
            set;
        }

        public ColourOutputManager(ISerialCommunicator serialCommunicator)
        {
            mColourBuffer = new Color[25];
            FadeTimeMs = 500;
            SaturationMultiplier = 1.0f;
            ContrastMultiplier = 1.0f;

            mSerialCommunicator = serialCommunicator;
            mSerialCommunicator.Connect();
        }

        public void Dispose()
        {
            mSerialCommunicator.Disconnect();
        }

        public Color GetPixel(UInt32 pixelIndex)
        {
            if (pixelIndex >= mColourBuffer.Length)
            {
                return Color.Black;
            }

            Color adjustedColour = AdjustSaturation(mColourBuffer[pixelIndex]);
            return AdjustContrast(adjustedColour);
        }

        public void SetPixel(UInt32 pixelIndex, Color pixelColour)
        {
            if(pixelIndex < mColourBuffer.Length)
            {
                mColourBuffer[pixelIndex] = pixelColour;
            }
        }

        public void FlushColours()
        {
            byte[] outputBuffer = new byte[(mColourBuffer.Length * 3) + 2];

            for(int pixelIndex = 0; pixelIndex < mColourBuffer.Length; ++pixelIndex)
            {
                Color adjustedColour = AdjustSaturation(mColourBuffer[pixelIndex]);
                adjustedColour = AdjustContrast(adjustedColour);

                outputBuffer[pixelIndex * 3] = adjustedColour.R;
                outputBuffer[(pixelIndex * 3) + 1] = adjustedColour.G;
                outputBuffer[(pixelIndex * 3) + 2] = adjustedColour.B;
            }

            byte[] fadeTimeBytes = BitConverter.GetBytes(FadeTimeMs);

            outputBuffer[75] = fadeTimeBytes[0];
            outputBuffer[76] = fadeTimeBytes[1];

            mSerialCommunicator.Write(outputBuffer);
            mSerialCommunicator.Read();
        }

        private Color AdjustSaturation(Color inputColour)
        {
            float hue = 0.0f;
            float saturation = 0.0f;
            float value = 0.0f;

            ColourUtilities.ColorToHSV(inputColour, ref hue, ref saturation, ref value);

            return ColourUtilities.HSVToColor(hue, saturation * SaturationMultiplier, value);
        }

        private Color AdjustContrast(Color inputColour)
        {
            Int32 redDifference = inputColour.R - 127;
            Int32 greenDifference = inputColour.G - 127;
            Int32 blueDifference = inputColour.B - 127;

            redDifference = (Int32) (redDifference * ContrastMultiplier);
            greenDifference = (Int32) (greenDifference * ContrastMultiplier);
            blueDifference = (Int32) (blueDifference * ContrastMultiplier);

            Int32 adjustedRed = 127 + redDifference;
            Int32 adjustedGreen = 127 + greenDifference;
            Int32 adjustedBlue = 127 + blueDifference;

            adjustedRed = Math.Max(adjustedRed, 0);
            adjustedRed = Math.Min(adjustedRed, 255);

            adjustedGreen = Math.Max(adjustedGreen, 0);
            adjustedGreen = Math.Min(adjustedGreen, 255);

            adjustedBlue = Math.Max(adjustedBlue, 0);
            adjustedBlue = Math.Min(adjustedBlue, 255);

            return Color.FromArgb(adjustedRed, adjustedGreen, adjustedBlue);
        }

        public void TurnLightsOff()
        {
            for(int colourIndex = 0; colourIndex < mColourBuffer.Length; ++colourIndex)
            {
                mColourBuffer[colourIndex] = Color.Black;
            }

            FlushColours();
        }
    }
}
