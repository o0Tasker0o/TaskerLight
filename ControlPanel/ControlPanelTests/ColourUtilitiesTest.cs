using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace ControlPanelTests
{
    [TestClass()]
    public class ColourUtilitiesTest
    {
        [TestMethod()]
        [DeploymentItem("ControlPanel.dll")]
        public void ColorToHSVTest()
        {
            float hue = 0.0f;
            float saturation = 0.0f;
            float value = 0.0f;

            ColourUtilities.ColorToHSV(Color.Black, ref hue, ref saturation, ref value);
            Assert.AreEqual(0.0f, hue);
            Assert.AreEqual(0.0f, saturation);
            Assert.AreEqual(0.0f, value);

            ColourUtilities.ColorToHSV(Color.White, ref hue, ref saturation, ref value);
            Assert.AreEqual(0.0f, hue);
            Assert.AreEqual(0.0f, saturation);
            Assert.AreEqual(1.0f, value);

            ColourUtilities.ColorToHSV(Color.Red, ref hue, ref saturation, ref value);
            Assert.AreEqual(0.0f, hue);
            Assert.AreEqual(1.0f, saturation);
            Assert.AreEqual(1.0f, value);

            ColourUtilities.ColorToHSV(Color.Lime, ref hue, ref saturation, ref value);
            Assert.AreEqual(0.333f, hue, 0.001f);
            Assert.AreEqual(1.0f, saturation);
            Assert.AreEqual(1.0f, value);

            ColourUtilities.ColorToHSV(Color.Blue, ref hue, ref saturation, ref value);
            Assert.AreEqual(0.666f, hue, 0.001f);
            Assert.AreEqual(1.0f, saturation);
            Assert.AreEqual(1.0f, value);
        }

        [TestMethod()]
        [DeploymentItem("ControlPanel.dll")]
        public void HSVToColorTest()
        {
            Color calculated = ColourUtilities.HSVToColor(0.0f, 0.0f, 0.0f);
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 0), calculated);

            calculated = ColourUtilities.HSVToColor(0.0f, 0.0f, 1.0f);
            Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), calculated);

            calculated = ColourUtilities.HSVToColor(0.0f, 1.0f, 1.0f);
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), calculated);

            calculated = ColourUtilities.HSVToColor(0.3333f, 1.0f, 1.0f);
            Assert.AreEqual(Color.FromArgb(255, 0, 255, 0), calculated);

            calculated = ColourUtilities.HSVToColor(0.6666f, 1.0f, 1.0f);
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), calculated);
        }
    }
}
