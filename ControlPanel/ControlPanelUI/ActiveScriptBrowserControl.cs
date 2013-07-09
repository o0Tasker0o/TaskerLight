using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ControlPanel;

namespace ControlPanelUI
{
    public partial class ActiveScriptBrowserControl : UserControl
    {
        public delegate void ScriptSelectionChangedEventHandler(DirectoryInfo scriptDirectory);
        public event ScriptSelectionChangedEventHandler ScriptSelectionChanged;

        private ActiveScriptBrowser mScriptBrowser;

        protected virtual void OnScriptSelectionChanged(object sender, EventArgs e)
        {
            if(ScriptSelectionChanged != null)
            {
                ScriptSelectionChanged((DirectoryInfo) ((RadioButton) sender).Tag);
            }
        }

        public ActiveScriptBrowserControl()
        {
            InitializeComponent();

            mScriptBrowser = new ActiveScriptBrowser();
            mScriptBrowser.ScriptsChanged += UpdateScriptList;

            UpdateScriptList();
        }

        private void UpdateScriptList()
        {
            Controls.Clear();

            int yPosition = 0;

            foreach(DirectoryInfo scriptDirectory in mScriptBrowser.ScriptDirectories)
            {
                RadioButton scriptButton = new RadioButton();
                scriptButton.Text = scriptDirectory.Name;
                scriptButton.Location = new Point(6, yPosition);
                scriptButton.CheckedChanged += new EventHandler(OnScriptSelectionChanged);
                scriptButton.Tag = scriptDirectory;

                Controls.Add(scriptButton);

                yPosition += 20;
            }
        }
    }
}
