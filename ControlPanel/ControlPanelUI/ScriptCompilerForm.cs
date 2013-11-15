using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Windows.Forms;
using ControlPanel;

namespace ControlPanelUI
{
    public partial class ScriptCompilerForm : Form
    {
        private readonly String cScriptRootDirectory;

        public ScriptCompilerForm()
        {
            InitializeComponent();

            cScriptRootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                "TaskerLight Scripts");
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            DirectoryInfo outputDirectory = new DirectoryInfo(Path.Combine(cScriptRootDirectory, outputDirectoryTextbox.Text));
            Directory.CreateDirectory(outputDirectory.FullName);

            CompilerResults results = ActiveScriptCompiler.CompileScript(codeTextbox.Text, outputDirectory);

            outputTextbox.Text = "";
            
            foreach(String resultText in results.Output)
            {
                outputTextbox.AppendText(resultText + "\n");
            }

            if (results.Errors.Count == 0)
            {
                outputTextbox.AppendText("Compilation Successful");
            }
        }
    }
}
