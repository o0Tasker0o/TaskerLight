using System.IO.Ports;

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

        public void Disconnect()
        {
            mArduinoComPort.Close();
        }
    }
}
