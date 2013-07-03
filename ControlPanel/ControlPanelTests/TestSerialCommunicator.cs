using System;
using ControlPanel;

namespace ControlPanelTests
{
    public class TestSerialCommunicator : ISerialCommunicator
    {
        public bool IsConnected
        {
            get;
            private set;
        }

        public byte[] OutputBuffer
        {
            get;
            private set;
        }

        public UInt32 ReadAmount
        {
            get;
            private set;
        }

        public TestSerialCommunicator()
        {
            IsConnected = false;

            OutputBuffer = new byte[77];
        }
        
        public void Connect()
        {
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
        }

        public void Write(byte[] buffer)
        {
            OutputBuffer = new byte[buffer.Length];

            for(int bufferIndex = 0; bufferIndex < buffer.Length; ++bufferIndex)
            {
                OutputBuffer[bufferIndex] = buffer[bufferIndex];
            }
        }

        public byte Read()
        {
            ReadAmount++;
            return 0;
        }
    }
}
