using System;

namespace ControlPanel
{
    public interface ISerialCommunicator
    {
        bool Connect();
        void Disconnect();
        byte Read();
        void Write(Byte [] buffer);
    }
}
