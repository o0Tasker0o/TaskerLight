using System;
using System.Drawing;

namespace ControlPanel
{
    public class ColourOutputManager : IDisposable
    {
        private ISerialCommunicator mSerialCommunicator;
        private Color [] mColourBuffer;

        public UInt16 FadeTimeMs
        {
            get;
            set;
        }

        public ColourOutputManager(ISerialCommunicator serialCommunicator)
        {
            mColourBuffer = new Color[25];
            FadeTimeMs = 500;

            mSerialCommunicator = serialCommunicator;
            mSerialCommunicator.Connect();
        }

        public void Dispose()
        {
            mSerialCommunicator.Disconnect();
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
                outputBuffer[pixelIndex * 3] = mColourBuffer[pixelIndex].R;
                outputBuffer[(pixelIndex * 3) + 1] = mColourBuffer[pixelIndex].G;
                outputBuffer[(pixelIndex * 3) + 2] = mColourBuffer[pixelIndex].B;
            }

            byte[] fadeTimeBytes = BitConverter.GetBytes(FadeTimeMs);

            outputBuffer[75] = fadeTimeBytes[0];
            outputBuffer[76] = fadeTimeBytes[1];

            mSerialCommunicator.Write(outputBuffer);
            mSerialCommunicator.Read();
        }
    }
}
