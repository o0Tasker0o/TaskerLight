using System;
using System.IO;
using System.Xml;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControlPanelTests
{
    [TestClass()]
    public class SettingsManagerTest
    {
        const String cSettingsFilename = "./settings.xml";

        [TestInitialize()]
        public void SettingsManagerTestInitialise()
        {
            SettingsManager.OutputSaturation = 1;
            SettingsManager.OutputContrast = 2;
        }

        [TestCleanup()]
        public void SettingsManagerTestCleanup()
        {
            File.Delete(cSettingsFilename);
        }

        [TestMethod()]
        public void SettingsManagerCanSaveSettingsToFile()
        {
            SettingsManager.Save(cSettingsFilename);

            Assert.IsTrue(File.Exists(cSettingsFilename));

            XmlDocument settingsXml = new XmlDocument();
            settingsXml.Load(cSettingsFilename);

            Assert.IsNotNull(settingsXml.SelectSingleNode("//TaskerLightSettings"));
            Assert.IsNotNull(settingsXml.SelectSingleNode("//TaskerLightSettings/OutputSaturation"));
            Assert.IsNotNull(settingsXml.SelectSingleNode("//TaskerLightSettings/OutputContrast"));
            Assert.AreEqual("1", settingsXml.SelectSingleNode("//TaskerLightSettings/OutputSaturation").InnerText);
            Assert.AreEqual("2", settingsXml.SelectSingleNode("//TaskerLightSettings/OutputContrast").InnerText);
        }

        [TestMethod()]
        public void SettingsManagerCanLoadSettingsFromFile()
        {
            CreateTestFile(cSettingsFilename);

            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);

            SettingsManager.Load(cSettingsFilename);

            Assert.AreEqual(123, SettingsManager.OutputSaturation);
            Assert.AreEqual(321, SettingsManager.OutputContrast);
        }

        [TestMethod()]
        public void SettingsManagerCanHandleLoadingFromNonExistantFile()
        {
            CreateTestFile("This file does not exist.xml");

            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);

            SettingsManager.Load(cSettingsFilename);

            Assert.AreEqual(50, SettingsManager.OutputSaturation);
            Assert.AreEqual(50, SettingsManager.OutputContrast);
        }

        [TestMethod()]
        public void SettingsManagerCanHandleLoadingFromNullFilename()
        {
            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);

            SettingsManager.Load(null);

            Assert.AreEqual(50, SettingsManager.OutputSaturation);
            Assert.AreEqual(50, SettingsManager.OutputContrast);
        }

        private static void CreateTestFile(String filename)
        {
            XmlDocument settingsDocument = new XmlDocument();

            XmlNode rootNode = settingsDocument.AppendChild(settingsDocument.CreateElement("TaskerLightSettings"));

            XmlNode outputSaturationNode = settingsDocument.CreateElement("OutputSaturation");
            outputSaturationNode.InnerText = "123";
            rootNode.AppendChild(outputSaturationNode);

            XmlNode outputContrastNode = settingsDocument.CreateElement("OutputContrast");
            outputContrastNode.InnerText = "321";
            rootNode.AppendChild(outputContrastNode);

            settingsDocument.Save(filename);
        }
    }
}
