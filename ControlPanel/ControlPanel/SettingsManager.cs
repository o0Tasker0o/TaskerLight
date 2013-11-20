using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;

namespace ControlPanel
{
    public class SettingsManager
    {
        private static Color[] mStaticColours;
        private static List<String> mVideoApps;

        public static UInt16 OutputSaturation
        {
            get; set;
        }

        public static UInt16 OutputContrast
        {
            get; set;
        }

        public static Color[] StaticColours
        {
            get
            {
                if(null == mStaticColours)
                {
                    mStaticColours = new Color[25];
                }

                return mStaticColours;
            }

            set
            {
                mStaticColours = value;
            }
        }

        public static OutputMode Mode
        {
            get;
            set;
        }

        public static List<String> VideoApps
        {
            get
            {
                if (null == mVideoApps)
                {
                    mVideoApps = new List<String>();
                }

                return mVideoApps;
            }

            set
            {
                mVideoApps = value;
            }
        }

        public static bool VideoOverlay
        {
            get;
            set;
        }

        public static void Save(String filename)
        {
            XmlDocument settingsDocument = new XmlDocument();

            XmlNode rootNode = settingsDocument.AppendChild(settingsDocument.CreateElement("TaskerLightSettings"));

            AddChildNode(settingsDocument, "OutputSaturation", OutputSaturation.ToString());
            AddChildNode(settingsDocument, "OutputContrast", OutputContrast.ToString());

            for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                String colourString = StaticColours[pixelIndex].R + ",";
                colourString += StaticColours[pixelIndex].G + ",";
                colourString += StaticColours[pixelIndex].B;
                AddChildNode(settingsDocument, "StaticColours" + pixelIndex, colourString);
            }

            AddChildNode(settingsDocument, "OutputMode", Mode.ToString());


            foreach(String appName in VideoApps)
            {
                AddChildNode(settingsDocument, "VideoApp", appName);
            }

            AddChildNode(settingsDocument, "VideoOverlay", VideoOverlay.ToString());

            settingsDocument.Save(filename);
        }

        public static void Load(String filename)
        {
            XmlDocument settingsXml = new XmlDocument();

            if(!String.IsNullOrEmpty(filename))
            {
                try
                {
                    settingsXml.Load(filename);
                }
                catch (FileNotFoundException)
                {

                }
            }

            OutputSaturation = UInt16.Parse(GetNodeValue(settingsXml, "OutputSaturation", "100"));
            OutputContrast = UInt16.Parse(GetNodeValue(settingsXml, "OutputContrast", "100"));

            for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                String colourString = GetNodeValue(settingsXml, "StaticColours" + pixelIndex, "0,0,0,0");

                String[] colourComponents = colourString.Split(',');

                mStaticColours[pixelIndex] = Color.FromArgb(Int32.Parse(colourComponents[0]),
                                                            Int32.Parse(colourComponents[1]),
                                                            Int32.Parse(colourComponents[2]));
            }

            Mode = (OutputMode) Enum.Parse(typeof(OutputMode), GetNodeValue(settingsXml, "OutputMode", "Wallpaper"));
            VideoApps = GetNodeValues(settingsXml, "VideoApp");

            VideoOverlay = GetNodeValue(settingsXml, "VideoOverlay", "false").Equals("true", StringComparison.InvariantCultureIgnoreCase);
        }

        private static void AddChildNode(XmlDocument settingsDocument, String variableName, String value)
        {
            XmlNode childNode = settingsDocument.CreateElement(variableName);
            childNode.InnerText = value;
            settingsDocument.DocumentElement.AppendChild(childNode);
        }

        private static String GetNodeValue(XmlDocument settingsDocument, String nodeName, String defaultValue)
        {
            XmlNode selectedNode = settingsDocument.SelectSingleNode("//TaskerLightSettings/" + nodeName);

            if (null != selectedNode)
            {
                return selectedNode.InnerText;
            }
            else
            {
                return defaultValue;
            }
        }

        private static List<String> GetNodeValues(XmlDocument settingsDocument, String nodeName)
        {
            XmlNodeList selectedNodes = settingsDocument.SelectNodes("//TaskerLightSettings/" + nodeName);

            List<String> nodeStrings = new List<String>();

            if (null != selectedNodes)
            {
                foreach(XmlNode node in selectedNodes)
                {
                    nodeStrings.Add(node.InnerText);
                }
            }

            return nodeStrings;
        }
    }
}
