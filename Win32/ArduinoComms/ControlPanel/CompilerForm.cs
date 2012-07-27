using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace ControlPanel
{
    public partial class CompilerForm : Form
    {
        const String cBasicScript = "using System;\r\n" +
                                    "using System.Drawing;\r\n" +
                                    "\r\n" +
                                    "class TaskerLightScript\r\n" +
                                    "{\r\n" +
                                    "    //An array of colour values. One for each LED in the strip\r\n" +
                                    "    private static Color[] mLEDColors = new Color[25];\r\n" +
                                    "\r\n" +
                                    "    /**\r\n" +
                                    "    * @brief Calculates the colour of the LEDs based on the current time\r\n" +
                                    "    * @param elapsedMS Passed in by the ControlPanel application.\r\n" +
                                    "    *        Represents the number of milliseconds elapsed since the\r\n" +
                                    "    *        application was started.\r\n" +
                                    "    * @return An array of colours representing an RGB value for each LED\r\n" +
                                    "    */\r\n" +
                                    "    public static Color [] TickLighting(long elapsedMS)\r\n" +
                                    "    {\r\n" +
                                    "        //For each LED in the strip\r\n" +
                                    "        for(int ledIndex = 0; ledIndex < 25; ++ledIndex)\r\n" +
                                    "        {\r\n" +
                                    "            //TODO - This function currently just sets all the LEDs to red\r\n" +
                                    "            //       Insert your colour generation algorithms here.\r\n" +
                                    "            mLEDColors[ledIndex] = Color.Red;\r\n" +
                                    "        }\r\n" +
                                    "\r\n" +
                                    "        return mLEDColors;\r\n" +
                                    "    }\r\n" +
                                    "};";

        public CompilerForm(String scriptName = "LightScript", String inputScript = cBasicScript)
        {
            InitializeComponent();

            this.filenameTextbox.Text = scriptName;
            this.codeTextbox.Text = inputScript;
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            // Create a new instance of the C# compiler
            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                Directory.CreateDirectory("./" + filenameTextbox.Text);
                // Create some parameters for the compiler
                CompilerParameters compilerParameters = new CompilerParameters();
                compilerParameters.OutputAssembly = "./" + filenameTextbox.Text + "/Script.dll";
                compilerParameters.GenerateExecutable = false;
                compilerParameters.GenerateInMemory = false;

                compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");

                Directory.CreateDirectory("./" + filenameTextbox.Text);

                String sourceCode = codeTextbox.Text;

                CompilerResults results = compiler.CompileAssemblyFromSource(compilerParameters, sourceCode);

                errorTextbox.Text = "";

                if(results.Errors.Count > 0)
                {
                    Directory.Delete("./" + filenameTextbox.Text);

                    foreach (CompilerError error in results.Errors)
                    {
                        errorTextbox.Text = error.ToString();
                    }
                }
                else
                {
                    File.WriteAllText("./" + filenameTextbox.Text + "/script.cs", codeTextbox.Text);
                }
            }
        }
    }
}
