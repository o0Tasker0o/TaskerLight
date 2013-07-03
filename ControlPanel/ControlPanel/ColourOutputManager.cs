using System;
using System.Drawing;

namespace ControlPanel
{
    public class ColourOutputManager : IDisposable
    {
        private ISerialCommunicator mSerialCommunicator;
        private Color [] mColourBuffer;

        public ColourOutputManager(ISerialCommunicator serialCommunicator)
        {
            mColourBuffer = new Color[25];

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

            outputBuffer[75] = 244;
            outputBuffer[76] = 1;

            mSerialCommunicator.Write(outputBuffer);
            mSerialCommunicator.Read();
        }
    }
}
