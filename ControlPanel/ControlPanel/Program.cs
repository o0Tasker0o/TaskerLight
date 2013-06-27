using System;

namespace ControlPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SerialCommunicator serialCommunicator = new SerialCommunicator();

            serialCommunicator.Connect();

            for(int i = 0; i < 25; ++i)
            {
                serialCommunicator.Write((byte) (i * 10));
                serialCommunicator.Write(0);
                serialCommunicator.Write(0);
            }

            Console.WriteLine(serialCommunicator.Read());

            Console.ReadLine();

            serialCommunicator.Disconnect();
        }
    }
}
