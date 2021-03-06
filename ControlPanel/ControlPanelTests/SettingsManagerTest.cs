﻿using System;
using System.IO;
using System.Xml;
using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;

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
            SettingsManager.Mode = OutputMode.ActiveScript;
            SettingsManager.VideoApps = new List<String>();
            SettingsManager.VideoOverlay = false;

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
            SettingsManager.VideoApps.Add("app name 1");
            SettingsManager.VideoApps.Add("app name 2");

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
            
            Assert.AreEqual("ActiveScript", settingsXml.SelectSingleNode("//TaskerLightSettings/OutputMode").InnerText);

            Assert.AreEqual("app name 1", settingsXml.SelectNodes("//TaskerLightSettings/VideoApp")[0].InnerText);
            Assert.AreEqual("app name 2", settingsXml.SelectNodes("//TaskerLightSettings/VideoApp")[1].InnerText);
            Assert.AreEqual("False", settingsXml.SelectSingleNode("//TaskerLightSettings/VideoOverlay").InnerText);
        }

        [TestMethod()]
        public void SettingsManagerCanLoadSettingsFromFile()
        {
            CreateTestFile(cSettingsFilename);

            SettingsManager.StaticColours = new Color[25];

            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);
            Assert.AreEqual(OutputMode.ActiveScript, SettingsManager.Mode);

            SettingsManager.Load(cSettingsFilename);

            Assert.AreEqual(123, SettingsManager.OutputSaturation);
            Assert.AreEqual(321, SettingsManager.OutputContrast);

            for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                Assert.AreEqual(Color.FromArgb(1 * pixelIndex, 2 * pixelIndex, 3 * pixelIndex),
                                SettingsManager.StaticColours[pixelIndex]);
            }

            Assert.AreEqual(OutputMode.ActiveScript, SettingsManager.Mode);

            List<String> videoApps = new List<String>();
            videoApps.Add("app name 1");
            videoApps.Add("app name 2");

            CollectionAssert.AreEqual(videoApps, SettingsManager.VideoApps);

            Assert.AreEqual(true, SettingsManager.VideoOverlay);
        }

        [TestMethod()]
        public void SettingsManagerCanHandleLoadingFromNonExistantFile()
        {
            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);
            Assert.AreEqual(OutputMode.ActiveScript, SettingsManager.Mode);

            SettingsManager.Load("This file does not exist.xml");

            Assert.AreEqual(100, SettingsManager.OutputSaturation);
            Assert.AreEqual(100, SettingsManager.OutputContrast);
            Color[] expectedColours = new Color[25];

            for (int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                expectedColours[pixelIndex] = Color.FromArgb(255, 0, 0, 0);
            }

            CollectionAssert.AreEqual(expectedColours, SettingsManager.StaticColours);
            Assert.AreEqual(OutputMode.Wallpaper, SettingsManager.Mode);
            CollectionAssert.AreEqual(new List<String>(), SettingsManager.VideoApps);
        }

        [TestMethod()]
        public void SettingsManagerCanHandleLoadingFromNullFilename()
        {
            Assert.AreEqual(1, SettingsManager.OutputSaturation);
            Assert.AreEqual(2, SettingsManager.OutputContrast);
            Assert.AreEqual(OutputMode.ActiveScript, SettingsManager.Mode);

            SettingsManager.Load(null);

            Assert.AreEqual(100, SettingsManager.OutputSaturation);
            Assert.AreEqual(100, SettingsManager.OutputContrast);
            Color[] expectedColours = new Color[25];

            for(int pixelIndex = 0; pixelIndex < 25; ++pixelIndex)
            {
                expectedColours[pixelIndex] = Color.FromArgb(255, 0, 0, 0);
            }

            CollectionAssert.AreEqual(expectedColours, SettingsManager.StaticColours);
            Assert.AreEqual(OutputMode.Wallpaper, SettingsManager.Mode);
            CollectionAssert.AreEqual(new List<String>(), SettingsManager.VideoApps);
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

            XmlNode outputNode = settingsDocument.CreateElement("OutputMode");
            outputNode.InnerText = "ActiveScript";
            rootNode.AppendChild(outputNode);

            XmlNode registeredAppNode1 = settingsDocument.CreateElement("VideoApp");
            registeredAppNode1.InnerText = "app name 1";
            rootNode.AppendChild(registeredAppNode1);

            XmlNode registeredAppNode2 = settingsDocument.CreateElement("VideoApp");
            registeredAppNode2.InnerText = "app name 2";
            rootNode.AppendChild(registeredAppNode2);

            XmlNode videoOverlayNode = settingsDocument.CreateElement("VideoOverlay");
            videoOverlayNode.InnerText = "True";
            rootNode.AppendChild(videoOverlayNode);

            settingsDocument.Save(filename);
        }
    }
}
