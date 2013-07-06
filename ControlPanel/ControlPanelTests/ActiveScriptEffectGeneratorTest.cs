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
                                           "    private static bool mShowRed = true;" +
                                           "    public static Color [] TickLighting()" +
                                           "    {" +
                                           "        for(int ledIndex = 0; ledIndex < 25; ++ledIndex)" +
                                           "        {" +
                                           "            mLEDColors[ledIndex] = mShowRed ? Color.Red : Color.Blue;" +
                                           "        }" +
                                           "        mShowRed = !mShowRed;" +
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
        public void ActiveScriptEffectGeneratorRedBlueScriptTest()
        {
            TestSerialCommunicator testSerialCommunicator = new TestSerialCommunicator();

            using (ColourOutputManager colourOutputManager = new ColourOutputManager(testSerialCommunicator))
            {
                CompileScript(cTestScript);

                ActiveScriptEffectGenerator activeScriptEffectGenerator = new ActiveScriptEffectGenerator(colourOutputManager);

                CollectionAssert.AreEqual(new byte[77], testSerialCommunicator.OutputBuffer);
                
                activeScriptEffectGenerator.Start();

                Thread.Sleep(150);

                byte[] fadeBytes = BitConverter.GetBytes((UInt16) 200);

                for(int bufferIndex = 0; bufferIndex < 75;)
                {
                    Assert.AreEqual(255, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                }

                Assert.AreEqual(fadeBytes[0], testSerialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeBytes[1], testSerialCommunicator.OutputBuffer[76]);

                Thread.Sleep(200);

                for (int bufferIndex = 0; bufferIndex < 75; )
                {
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(0, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                    Assert.AreEqual(255, testSerialCommunicator.OutputBuffer[bufferIndex++]);
                }

                Assert.AreEqual(fadeBytes[0], testSerialCommunicator.OutputBuffer[75]);
                Assert.AreEqual(fadeBytes[1], testSerialCommunicator.OutputBuffer[76]);

                activeScriptEffectGenerator.Stop();
            }
        }

        private void CompileScript(String sourceCode)
        {
            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                // Create some parameters for the compiler
                CompilerParameters compilerParameters = new CompilerParameters();
                compilerParameters.OutputAssembly = "./script.dll";
                compilerParameters.GenerateExecutable = false;
                compilerParameters.GenerateInMemory = false;

                compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");

                CompilerResults results = compiler.CompileAssemblyFromSource(compilerParameters, sourceCode);
            }
        }
    }
}
