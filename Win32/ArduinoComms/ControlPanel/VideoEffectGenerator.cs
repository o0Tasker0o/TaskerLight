using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ControlPanel
{
    class VideoEffectGenerator : EffectGenerator
    {
        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 InitialiseScreenCapture();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 ShutdownScreenCapture();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 StartCapturing(bool useDirectX);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 StopCapturing();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 GetAverageColour(UInt32 x, UInt32 y,
                                                      UInt32 width, UInt32 height);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 TestLeftBorder(Int32 x, Int32 y,
                                                   Int32 width, Int32 height);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 GetLeftBorder();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 TestRightBorder(Int32 x, Int32 y,
                                                    Int32 width, Int32 height);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 GetRightBorder();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 TestTopBorder(Int32 x, Int32 y,
                                                  Int32 width, Int32 height);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 GetTopBorder();

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 TestBottomBorder(Int32 x, Int32 y,
                                                     Int32 width, Int32 height);

        [DllImport("ScreenCaptureLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 GetBottomBorder();

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetClientRect(IntPtr hWnd, ref Rectangle rect);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindow(IntPtr hWnd, UInt32 wCmd);

        private const UInt32 GW_HWNDNEXT = 0x02;

        private ListView.ListViewItemCollection mActiveAppsList;
        private bool mUseMarginsFullscreen;

        public VideoEffectGenerator(LEDPreview ledPreview,
                                    ListView.ListViewItemCollection activeApps)
            : base(ledPreview)
        {
            SetActiveApps(activeApps);
        }

        internal void SetActiveApps(ListView.ListViewItemCollection activeApps)
        {
            mActiveAppsList = activeApps;
        }

        internal void UseMarginsFullscreen(bool useMargins)
        {
            mUseMarginsFullscreen = useMargins;
        }

        protected override void ThreadTick()
        {
            InitialiseScreenCapture();

            RegionManager.Instance().Resize();
            StartCapturing(true);

            while (mRunning)
            {
                List<Form1.ProcessIndex> activeProcesses = new List<Form1.ProcessIndex>();

                for (int appIndex = 0; appIndex < mActiveAppsList.Count; ++appIndex)
                {
                    Form1.ProcessIndex processIndex = (Form1.ProcessIndex)mActiveAppsList[appIndex].Tag;
                    Process[] processes = Process.GetProcessesByName(processIndex.processName);

                    if (processes.Length > 0)
                    {
                        processIndex.process = processes[0];
                        activeProcesses.Add(processIndex);
                    }

                    mActiveAppsList[appIndex].Tag = processIndex;
                }

                IntPtr foregroundWindow = GetForegroundWindow();

                bool activeProcessFound = false;

                do
                {
                    foreach (Form1.ProcessIndex runningProcess in activeProcesses)
                    {
                        if (foregroundWindow == runningProcess.process.MainWindowHandle)
                        {
                            //This window is the highest level window of one of our active applications
                            activeProcessFound = true;

                            Rectangle rectangle = new Rectangle();

                            Point position = new Point(0, 0);
                            ClientToScreen(runningProcess.process.MainWindowHandle, ref position);

                            GetClientRect(runningProcess.process.MainWindowHandle, ref rectangle);

                            if (rectangle == Screen.PrimaryScreen.Bounds && !mUseMarginsFullscreen)
                            {
                                TestLeftBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                                TestRightBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                                TestTopBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                                TestBottomBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

                                rectangle.X += GetLeftBorder();
                                rectangle.Width -= GetLeftBorder();
                                rectangle.Width -= GetRightBorder();

                                rectangle.Y += GetTopBorder();
                                rectangle.Height -= GetTopBorder();
                                rectangle.Height -= GetBottomBorder();

                                RegionManager.Instance().Resize(rectangle);
                                break;
                            }

                            rectangle.X = position.X;
                            rectangle.Y = position.Y + runningProcess.topMargin;

                            rectangle.Height -= runningProcess.topMargin;
                            rectangle.Height -= runningProcess.bottomMargin;

                            if (rectangle.X < 0)
                            {
                                rectangle.Width += rectangle.X;
                                rectangle.X = 0;
                            }

                            if (rectangle.X + rectangle.Width > Screen.PrimaryScreen.Bounds.Width)
                            {
                                int excessWidth = (rectangle.X + rectangle.Width) - Screen.PrimaryScreen.Bounds.Width;
                                rectangle.Width -= excessWidth;
                            }

                            if (rectangle.Y + rectangle.Height > Screen.PrimaryScreen.Bounds.Height)
                            {
                                int excessHeight = (rectangle.Y + rectangle.Height) - Screen.PrimaryScreen.Bounds.Height;
                                rectangle.Height -= excessHeight;
                            }

                            TestLeftBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                            TestRightBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

                            rectangle.X += GetLeftBorder();
                            rectangle.Width -= GetLeftBorder();
                            rectangle.Width -= GetRightBorder();

                            RegionManager.Instance().Resize(rectangle);

                            break;
                        }
                    }

                    foregroundWindow = GetWindow(foregroundWindow, GW_HWNDNEXT);
                } while (foregroundWindow != IntPtr.Zero && !activeProcessFound);

                if (!activeProcessFound)
                {
                    Rectangle rectangle = Screen.PrimaryScreen.Bounds;

                    TestLeftBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                    TestRightBorder(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

                    rectangle.X += GetLeftBorder();
                    rectangle.Width -= GetLeftBorder();
                    rectangle.Width -= GetRightBorder();

                    RegionManager.Instance().Resize(rectangle);
                }

                for (UInt32 i = 0; i < 25; ++i)
                {
                    Rectangle region = RegionManager.Instance().GetRegion(i);
                    mOutputColours[i] = ColourUtil.ConvertToColor(GetAverageColour((UInt32)region.X, (UInt32)region.Y,
                                                                                   (UInt32)region.Width, (UInt32)region.Height));
                }

                OutputColours();

                Thread.Sleep(100);
            }

            StopCapturing();
            ShutdownScreenCapture();
        }
    }
}
