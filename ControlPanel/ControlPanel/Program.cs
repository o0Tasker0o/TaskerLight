using System;

namespace ControlPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SerialCommunicator serialCommunicator = new SerialCommunicator();

            serialCommunicator.Connect();

            int sleepTime = 1500;
            
            for(int repeat = 0; repeat < 50; ++repeat)
            {
                byte[] buffer = new byte[76];

                for(int i = 0; i < 75; )
                {
                    buffer[i++] = 255;
                    buffer[i++] = 0;
                    buffer[i++] = 0;
                }

                buffer[75] = (byte) (repeat + 1);

                serialCommunicator.Write(buffer);
                serialCommunicator.Read();
                System.Threading.Thread.Sleep(sleepTime);
                
                for (int i = 0; i < 75; )
                {
                    buffer[i++] = 0;
                    buffer[i++] = 255;
                    buffer[i++] = 0;
                }
                
                serialCommunicator.Write(buffer);
                serialCommunicator.Read();
                System.Threading.Thread.Sleep(sleepTime);
                
                for (int i = 0; i < 75; )
                {
                    buffer[i++] = 0;
                    buffer[i++] = 0;
                    buffer[i++] = 255;
                }
                
                serialCommunicator.Write(buffer);
                serialCommunicator.Read();
                System.Threading.Thread.Sleep(sleepTime);

                Console.WriteLine(repeat + 1);
            }

            serialCommunicator.Disconnect();
        }
    }
}
