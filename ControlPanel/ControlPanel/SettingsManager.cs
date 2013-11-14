using System;
using System.IO;
using System.Xml;

namespace ControlPanel
{
    public class SettingsManager
    {
        public static UInt16 OutputSaturation
        {
            get; set;
        }

        public static UInt16 OutputContrast
        {
            get; set;
        }

        public static void Save(String filename)
        {
            XmlDocument settingsDocument = new XmlDocument();

            XmlNode rootNode = settingsDocument.AppendChild(settingsDocument.CreateElement("TaskerLightSettings"));

            AddChildNode(settingsDocument, "OutputSaturation", OutputSaturation.ToString());
            AddChildNode(settingsDocument, "OutputContrast", OutputContrast.ToString());

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
    }
}
