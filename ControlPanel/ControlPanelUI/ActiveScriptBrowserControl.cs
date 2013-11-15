using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ControlPanel;

namespace ControlPanelUI
{
    public partial class ActiveScriptBrowserControl : UserControl
    {
        private delegate void UpdateScriptCallback();

        public delegate void ScriptSelectionChangedEventHandler(DirectoryInfo scriptDirectory);
        public event ScriptSelectionChangedEventHandler ScriptSelectionChanged;

        private ActiveScriptBrowser mScriptBrowser;

        public DirectoryInfo SelectedScriptDirectory
        {
            get;
            private set;
        }

        protected virtual void OnScriptSelectionChanged(object sender, EventArgs e)
        {
            if(ScriptSelectionChanged != null)
            {
                SelectedScriptDirectory = (DirectoryInfo) ((RadioButton) sender).Tag;
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
            if(InvokeRequired)
            {
                Invoke(new UpdateScriptCallback(UpdateScriptList));
            }
            else
            {
                Controls.Clear();

                int yPosition = 0;

                foreach (DirectoryInfo scriptDirectory in mScriptBrowser.ScriptDirectories)
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
}
