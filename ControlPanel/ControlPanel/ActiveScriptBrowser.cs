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
                    if (!Directory.Exists(value.FullName))
                    {
                        Directory.CreateDirectory(value.FullName);
                    }
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

                directoriesList.AddRange(FindScriptDirectories(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)));
                directoriesList.AddRange(FindScriptDirectories(RootDirectory));

                return directoriesList;
            }
        }

        private List<DirectoryInfo> FindScriptDirectories(DirectoryInfo rootDirectory)
        {
            List<DirectoryInfo> directoriesList = new List<DirectoryInfo>();

            foreach (DirectoryInfo scriptDirectory in rootDirectory.GetDirectories())
            {
                if (scriptDirectory.GetFiles("script.dll").Count() > 0)
                {
                    directoriesList.Add(scriptDirectory);
                }
            }

            return directoriesList;
        }

        public ActiveScriptBrowser()
        {
            String scriptDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                  "TaskerLight Scripts");
            RootDirectory = new DirectoryInfo(scriptDirectory);
        }
    }
}
