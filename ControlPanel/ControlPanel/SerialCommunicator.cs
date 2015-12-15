using System;
using System.IO;
using System.IO.Ports;

namespace ControlPanel
{
    public class SerialCommunicator : ISerialCommunicator
    {
        private readonly SerialPort mArduinoComPort;
        private bool mIsValid;

        public SerialCommunicator(string comPort)
        {
            mIsValid = false;
            mArduinoComPort = new SerialPort(comPort,
                                             115200,
                                             Parity.None,
                                             8,
                                             StopBits.One);
        }

        public bool Connect()
        {
            try
            {
                mArduinoComPort.Open();
                mIsValid = true;
            }
            catch (IOException)
            {
            }
            catch (ArgumentException)
            {
            }

            System.Threading.Thread.Sleep(1500);

            return mIsValid;
        }

        public void Write(Byte [] buffer)
        {
            if (mIsValid)
            {
                mArduinoComPort.Write(buffer, 0, buffer.Length);
            }
        }

        public byte Read()
        {
            if (!mIsValid)
            {
                return 0;
            }

            return (byte) mArduinoComPort.ReadByte();
        }

        public void Disconnect()
        {
            mArduinoComPort.Close();
        }
    }
}
