﻿using System;
using System.IO.Ports;

namespace ControlPanel
{
    public class SerialCommunicator : ISerialCommunicator
    {
        private readonly SerialPort mArduinoComPort;

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

            System.Threading.Thread.Sleep(1500);
        }

        public void Write(Byte [] buffer)
        {
            mArduinoComPort.Write(buffer, 0, buffer.Length);
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
