using System.IO.Ports;
using System;

namespace ControlPanel
{
    public class SerialCommunicator
    {
        private SerialPort mArduinoComPort;

        public SerialCommunicator()
        {
            mArduinoComPort = new SerialPort("COM3",
                                             115200,
                                             Parity.None,
                                             8,
                                             StopBits.One);
        }

        public void Connect()
        {
            mArduinoComPort.Open();
        }

        public void Write(Byte buffer)
        {
            mArduinoComPort.Write(new byte[] { buffer }, 0, 1);
        }

        public byte Read()
        {
           return (byte) mArduinoComPort.ReadByte();
        }

        public void Disconnect()
        {
            mArduinoComPort.Close();
        }
    }
}
