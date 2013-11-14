using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ControlPanel
{
    public class ApplicationFinder
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

        uint GW_HWNDNEXT = 2;

        private HashSet<String> RegisteredApplications
        {
            get;
            set;
        }

        public ApplicationFinder()
        {
            RegisteredApplications = new HashSet<String>();
        }

        public bool IsApplicationRunning(String processName)
        {
            Process[] foundProcesses = Process.GetProcessesByName(processName);

            return foundProcesses.Length > 0;
        }

        public void RegisterApplication(FileInfo exeFile)
        {
            RegisteredApplications.Add(Path.GetFileNameWithoutExtension(exeFile.FullName));
        }

        public bool RunningRegisteredApplications()
        {
            foreach(String applicationName in RegisteredApplications)
            {
                if(IsApplicationRunning(applicationName))
                {
                    return true;
                }
            }

            return false;
        }

        public Rectangle GetTopmostRegisteredApp()
        {
            List<IntPtr> processHandles = new List<IntPtr>();

            foreach(String processName in RegisteredApplications)
            {
                Process[] processes = Process.GetProcessesByName(processName);

                foreach(Process foundProcess in processes)
                {
                    processHandles.Add(foundProcess.MainWindowHandle);
                }
            }

            Rectangle returnRectangle = new Rectangle(0, 0, 1920, 1080);

            IntPtr topWindow = GetForegroundWindow();
            
            do
            {
                if (processHandles.Contains(topWindow))
                {
                    Point returnRectangleLocation = new Point(0, 0);
                    ClientToScreen(topWindow, ref returnRectangleLocation);
                    GetClientRect(topWindow, out returnRectangle);

                    returnRectangle.Location = returnRectangleLocation;

                    break;
                }

                topWindow = GetWindow(topWindow, GW_HWNDNEXT);
            }
            while (topWindow != IntPtr.Zero);

            return returnRectangle;
        }
    }
}
