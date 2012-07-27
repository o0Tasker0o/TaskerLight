using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ControlPanel
{
    static class SettingsManager
    {
        private static readonly String mFilename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TaskerLight.cnf";

        static Color [] mStaticColours = new Color[25];
        static List<Form1.ProcessIndex> mActiveProcesses = new List<Form1.ProcessIndex>();
        static float mOverSaturation;
        static float mContrast;

        internal static void SetStaticColours(Color[] colours)
        {
            mStaticColours = (Color []) colours.Clone();
        }

        internal static Color [] GetStaticColours()
        {
            return (Color []) mStaticColours.Clone();
        }

        internal static void SetActiveApps(ListView.ListViewItemCollection appItems)
        {
            mActiveProcesses.Clear();

            foreach(ListViewItem appItem in appItems)
            {
                mActiveProcesses.Add((Form1.ProcessIndex) appItem.Tag);
            }
        }

        internal static List<Form1.ProcessIndex> GetActiveApps()
        {
            return mActiveProcesses;
        }

        internal static void SetSaturation(float saturation)
        {
            mOverSaturation = saturation;
        }

        internal static float GetSaturation()
        {
            return mOverSaturation;
        }
        
        internal static void SetContrast(float contrast)
        {
            mContrast = contrast;
        }

        internal static float GetContrast()
        {
            return mContrast;
        }

        internal static void SaveSettings()
        {
            //Save static colour settings
            List<Byte> mSettingsBytes = new List<Byte>();

            for(int i = 0; i < mStaticColours.Length; ++i)
            {
                mSettingsBytes.Add(mStaticColours[i].R);
                mSettingsBytes.Add(mStaticColours[i].G);
                mSettingsBytes.Add(mStaticColours[i].B);
            }

            //Save active application settings
            mSettingsBytes.AddRange(BitConverter.GetBytes(mActiveProcesses.Count));

            foreach(Form1.ProcessIndex activeProcess in mActiveProcesses)
            {
                mSettingsBytes.AddRange(BitConverter.GetBytes(activeProcess.exeName.Length));

                for(int i = 0; i < activeProcess.exeName.Length; ++i)
                {
                    mSettingsBytes.Add(Convert.ToByte(activeProcess.exeName[i]));
                }

                mSettingsBytes.AddRange(BitConverter.GetBytes(activeProcess.topMargin));
                mSettingsBytes.AddRange(BitConverter.GetBytes(activeProcess.bottomMargin));
            }

            mSettingsBytes.AddRange(BitConverter.GetBytes(mOverSaturation));
            mSettingsBytes.AddRange(BitConverter.GetBytes(mContrast));

            File.WriteAllBytes(mFilename,
                               mSettingsBytes.ToArray());
        }

        internal static void LoadSettings()
        {
            try
            {
                Byte[] settingsBytes = File.ReadAllBytes(mFilename);

                int byteIndex = 0;

                for(int i = 0; i < mStaticColours.Length; ++i)
                {
                    mStaticColours[i] = Color.FromArgb(settingsBytes[byteIndex++],
                                                       settingsBytes[byteIndex++],
                                                       settingsBytes[byteIndex++]);
                }

                mActiveProcesses.Clear();

                int activeProcessCount = BitConverter.ToInt32(settingsBytes, byteIndex);
                byteIndex += 4;

                for(int i = 0; i < activeProcessCount; ++i)
                {
                    int appNameLength = BitConverter.ToInt32(settingsBytes, byteIndex);
                    byteIndex += 4;

                    String appName = "";

                    for (int nameIndex = 0; nameIndex < appNameLength; ++nameIndex)
                    {
                        appName += (char)settingsBytes[byteIndex++];
                    }

                    Form1.ProcessIndex processIndex = new Form1.ProcessIndex();
                    processIndex.exeName = appName;

                    processIndex.topMargin = BitConverter.ToInt32(settingsBytes, byteIndex);
                    byteIndex += 4;

                    processIndex.bottomMargin = BitConverter.ToInt32(settingsBytes, byteIndex);
                    byteIndex += 4;

                    mActiveProcesses.Add(processIndex);
                }

                mOverSaturation = BitConverter.ToSingle(settingsBytes, byteIndex);
                byteIndex += 4;

                mContrast = BitConverter.ToSingle(settingsBytes, byteIndex);
                byteIndex += 4;
            }
            catch(FileNotFoundException) { }
        }
    }
}
