using System;
using System.IO;
using System.Xml;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

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

            for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                SettingsManager.StaticColours[pixelIndex] = Color.FromArgb(1 * pixelIndex, 2 * pixelIndex, 3 * pixelIndex);
            }
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

            for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                Assert.AreEqual(1 * pixelIndex + "," + 2 * pixelIndex + "," + 3 * pixelIndex, 
                                settingsXml.SelectSingleNode("//TaskerLightSettings/StaticColours" + pixelIndex).InnerText);
            }
        }

        [TestMethod()]
        public void SettingsManagerCanLoadSettingsFromFile()
        {
            CreateTestFile(cSettingsFilename);

            SettingsManager.StaticColours = new Color[25];

            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);

            SettingsManager.Load(cSettingsFilename);

            Assert.AreEqual(123, SettingsManager.OutputSaturation);
            Assert.AreEqual(321, SettingsManager.OutputContrast);

            for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                Assert.AreEqual(Color.FromArgb(1 * pixelIndex, 2 * pixelIndex, 3 * pixelIndex),
                                SettingsManager.StaticColours[pixelIndex]);
            }
        }

        [TestMethod()]
        public void SettingsManagerCanHandleLoadingFromNonExistantFile()
        {
            CreateTestFile("This file does not exist.xml");

            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);

            SettingsManager.Load(cSettingsFilename);

            Assert.AreEqual(100, SettingsManager.OutputSaturation);
            Assert.AreEqual(100, SettingsManager.OutputContrast);
            Color[] expectedColours = new Color[25];

            for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                expectedColours[pixelIndex] = Color.FromArgb(255, 0, 0, 0);
            }

            CollectionAssert.AreEqual(expectedColours, SettingsManager.StaticColours);
        }

        [TestMethod()]
        public void SettingsManagerCanHandleLoadingFromNullFilename()
        {
            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);

            SettingsManager.Load(null);

            Assert.AreEqual(100, SettingsManager.OutputSaturation);
            Assert.AreEqual(100, SettingsManager.OutputContrast);
            Color[] expectedColours = new Color[25];

            for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                expectedColours[pixelIndex] = Color.FromArgb(255, 0, 0, 0);
            }

            CollectionAssert.AreEqual(expectedColours, SettingsManager.StaticColours);
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

            for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                XmlNode staticColourNode = settingsDocument.CreateElement("StaticColours" + pixelIndex);
                staticColourNode.InnerText = 1 * pixelIndex + "," + 2 * pixelIndex + "," + 3 * pixelIndex;
                rootNode.AppendChild(staticColourNode);
            }

            settingsDocument.Save(filename);
        }
    }
}
