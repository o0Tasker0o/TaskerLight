using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace ControlPanelTests
{
    [TestClass()]
    public class ActiveScriptEffectGeneratorTest
    {
        private const String cTestScript = "using System;" +
                                           "using System.Drawing;" +
                                           "class TaskerLightScript" +
                                           "{" +
                                           "    private static Color[] mLEDColors = new Color[25];" +
                                           "    public static Color [] TickLighting(long elapsedMS)" +
                                           "    {" +
                                           "        for(int ledIndex = 0; ledIndex < 25; ++ledIndex)" +
                                           "        {" +
                                           "            mLEDColors[ledIndex] = Color.FromArgb((int) (elapsedMS % 255), 0, 0);" +
                                           "        }" +
                                           "        return mLEDColors;" +
                                           "    }" +
                                           "};";
        
        [TestCleanup()]
        public void Cleanup()
        {
            File.Delete("./script.dll");
        }

        [TestMethod()]
        public void ActiveScriptEffectGeneratorNoScriptTest()
        {
            TestSerialCommunicator testSerialCommunicator = new TestSerialCommunicator();

            using (ColourOutputManager colourOutputManager = new ColourOutputManager(testSerialCommunicator))
            {
                ActiveScriptEffectGenerator activeScriptEffectGenerator = new ActiveScriptEffectGenerator(colourOutputManager);

                CollectionAssert.AreEqual(new byte[77], testSerialCommunicator.OutputBuffer);

                activeScriptEffectGenerator.Start();

                Thread.Sleep(150);

                byte[] fadeBytes = BitConverter.GetBytes((UInt16)200);

                for (int bufferIndex = 0; bufferIndex < 75; )
                {
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                }

                Assert.AreEqual(fadeBytes[0], testSerialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeBytes[1], testSerialCommunicator.OutputBuffer[76]);

                activeScriptEffectGenerator.Stop();
            }
        }

        [TestMethod()]
        public void ActiveScriptEffectGeneratorBadScriptTest()
        {
            TestSerialCommunicator testSerialCommunicator = new TestSerialCommunicator();

            using (ColourOutputManager colourOutputManager = new ColourOutputManager(testSerialCommunicator))
            {
                File.WriteAllText("./script.dll", "THIS IS NOT A DLL");

                ActiveScriptEffectGenerator activeScriptEffectGenerator = new ActiveScriptEffectGenerator(colourOutputManager);

                CollectionAssert.AreEqual(new byte[77], testSerialCommunicator.OutputBuffer);

                activeScriptEffectGenerator.Start();

                Thread.Sleep(150);

                byte[] fadeBytes = BitConverter.GetBytes((UInt16)200);

                for (int bufferIndex = 0; bufferIndex < 75; )
                {
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                }

                Assert.AreEqual(fadeBytes[0], testSerialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeBytes[1], testSerialCommunicator.OutputBuffer[76]);

                activeScriptEffectGenerator.Stop();
            }
        }

        [TestMethod()]
        public void ActiveScriptEffectGeneratorWorkingScriptTest()
        {
            TestSerialCommunicator testSerialCommunicator = new TestSerialCommunicator();

            using (ColourOutputManager colourOutputManager = new ColourOutputManager(testSerialCommunicator))
            {
                ActiveScriptCompiler.CompileScript(cTestScript, 
                                                   new DirectoryInfo(Directory.GetCurrentDirectory()));

                ActiveScriptEffectGenerator activeScriptEffectGenerator = new ActiveScriptEffectGenerator(colourOutputManager);

                CollectionAssert.AreEqual(new byte[77], testSerialCommunicator.OutputBuffer);
                
                activeScriptEffectGenerator.Start();

                Thread.Sleep(150);

                byte[] fadeBytes = BitConverter.GetBytes((UInt16) 200);

                for(int bufferIndex = 0; bufferIndex < 75;)
                {
                    Assert.IsTrue(testSerialCommunicator.OutputBuffer[bufferIndex++] < 50);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                }

                Assert.AreEqual(fadeBytes[0], testSerialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeBytes[1], testSerialCommunicator.OutputBuffer[76]);

                Thread.Sleep(200);

                for (int bufferIndex = 0; bufferIndex < 75; )
                {
                    Assert.IsTrue(testSerialCommunicator.OutputBuffer[bufferIndex++] > 200);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                }

                Assert.AreEqual(fadeBytes[0], testSerialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeBytes[1], testSerialCommunicator.OutputBuffer[76]);

                activeScriptEffectGenerator.Stop();
            }
        }
    }
}
