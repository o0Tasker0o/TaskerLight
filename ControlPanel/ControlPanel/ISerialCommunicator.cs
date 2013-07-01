using System;

namespace ControlPanel
{
    public interface ISerialCommunicator
    {
        void Connect();
        void Disconnect();
        byte Read();
        void Write(Byte [] buffer);
    }
}
