using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace ControlPanel
{
    static class SettingsManager
    {
        private static readonly String mFilename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TaskerLight.cnf";

        static Color [] mStaticColours = new Color[25];

        internal static void SetStaticColours(Color[] colours)
        {
            mStaticColours = (Color []) colours.Clone();
        }

        internal static Color [] GetStaticColours()
        {
            return (Color []) mStaticColours.Clone();
        }

        internal static void SaveSettings()
        {
            List<Byte> mSettingsBytes = new List<Byte>();

            for(int i = 0; i < mStaticColours.Length; ++i)
            {
                mSettingsBytes.Add(mStaticColours[i].R);
                mSettingsBytes.Add(mStaticColours[i].G);
                mSettingsBytes.Add(mStaticColours[i].B);
            }

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
            }
            catch(FileNotFoundException) { }
        }
    }
}
