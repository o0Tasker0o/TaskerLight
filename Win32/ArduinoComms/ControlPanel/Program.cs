using System;
using System.Windows.Forms;
using System.Threading;

namespace ControlPanel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool onlyInstance = false;
            Mutex mutex = new Mutex(true, "TaskerLightMutex", out onlyInstance);

            if(onlyInstance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());

                GC.KeepAlive(mutex);
            }
            else
            {
                MessageBox.Show("Application is already running", "TaskerLight", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
