using System;

namespace ControlPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SerialCommunicator serialCommunicator = new SerialCommunicator();

            serialCommunicator.Connect();

            Console.ReadLine();

            serialCommunicator.Disconnect();
        }
    }
}
