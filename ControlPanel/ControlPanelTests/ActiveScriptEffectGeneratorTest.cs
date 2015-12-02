using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using NSubstitute;

namespace ControlPanelTests
{
    [TestClass()]
    public class ActiveScriptEffectGeneratorTest
    {
        [TestMethod()]
        public void Constructor()
        {
            ISerialCommunicator serialCommunicator = Substitute.For<ISerialCommunicator>();
            ColourOutputManager colourOutputManager = new ColourOutputManager(serialCommunicator);

            ActiveScriptEffectGenerator effectGenerator = new ActiveScriptEffectGenerator(colourOutputManager);
        }
    }
}
