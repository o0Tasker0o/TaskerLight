using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ControlPanel
{
    public class ActiveScriptBrowser
    {
        public delegate void ScriptsChangedEventHandler();
        public event ScriptsChangedEventHandler ScriptsChanged;

        FileSystemWatcher mScriptDirectoryWatcher;

        protected virtual void OnScriptsChanged(object sender, FileSystemEventArgs e)
        {
            if (ScriptsChanged != null)
            {
                ScriptsChanged();
            }
        }

        public DirectoryInfo RootDirectory
        {
            get
            {
                return new DirectoryInfo(mScriptDirectoryWatcher.Path);
            }
            set
            {
                if(null != value)
                {
                    mScriptDirectoryWatcher = new FileSystemWatcher(value.FullName);

                    mScriptDirectoryWatcher.Changed += new FileSystemEventHandler(OnScriptsChanged);
                    mScriptDirectoryWatcher.Created += new FileSystemEventHandler(OnScriptsChanged);
                    mScriptDirectoryWatcher.Deleted += new FileSystemEventHandler(OnScriptsChanged);

                    mScriptDirectoryWatcher.EnableRaisingEvents = true;
                }
            }
        }

        public List<DirectoryInfo> ScriptDirectories
        {
            get
            {
                List<DirectoryInfo> directoriesList = new List<DirectoryInfo>();

                foreach(DirectoryInfo scriptDirectory in RootDirectory.GetDirectories())
                {
                    if(scriptDirectory.GetFiles("script.dll").Count() > 0)
                    {
                        directoriesList.Add(scriptDirectory);
                    }
                }

                return directoriesList;
            }
        }

        public ActiveScriptBrowser()
        {
            RootDirectory = new DirectoryInfo("./");
        }
    }
}
