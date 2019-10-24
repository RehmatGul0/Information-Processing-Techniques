using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace k163680_Q5_PartA
{
    public partial class FolderMonitoring : ServiceBase
    {
        private System.Timers.Timer time;
        private FileSystemWatcher watcher;
        private List<FileInfo> filesModified;
        private StreamWriter logs;

        public FolderMonitoring()
        {
            logs = new StreamWriter(Path.Combine(ConfigurationManager.AppSettings.Get("Path"), "logs.txt"));
            time = new System.Timers.Timer();
            watcher = new FileSystemWatcher();
            filesModified = new List<FileInfo>();
            InitializeComponent();
            watcher.Path = ConfigurationManager.AppSettings.Get("MonitorPath");
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.EnableRaisingEvents = true;
        }

        protected override void OnStart(string[] args)
        {
            logs.WriteLine("Service Has been started");
            try
            {
                time.Start();
                time.Interval = 60000;
                time.Elapsed += timeElapsed;
                watcher.Created += OnChanged;
                watcher.Renamed += OnRenamed;
            }
            catch
            {
                logs.WriteLine("Error");
            }
         
        }
        protected override void OnStop()
        {
            logs.Dispose();
            logs.Close();
        }
        public void timeElapsed(object sender, ElapsedEventArgs e)
        {
            logs.WriteLine("Monitoring service at {0}", DateTime.Now);
            if(time.Interval <= 3600000)
                time.Interval += 120000;
            monitorFolder();
        }

        public void monitorFolder()
        {
            try
            {
                string destPath;
                foreach (FileInfo fileInfo in filesModified)
                {
                    destPath = System.IO.Path.Combine(ConfigurationManager.AppSettings.Get("CopyFolderPath"), fileInfo.getName());
                    if (File.Exists(fileInfo.getFullPath()))
                    {
                        System.IO.File.Copy(fileInfo.getFullPath(), destPath, true);
                    }
                }
                logs.WriteLine("Successfully Copied {0} files..");
                filesModified.Clear();
            }
            catch
            {
                logs.WriteLine("Error Copying files");
            }
            
        }
        private void OnChanged(object source, FileSystemEventArgs evnt)
        {
            filesModified.Add(new FileInfo(evnt.Name, evnt.FullPath));
            // Specify what is done when a file is changed, created, or deleted.
            logs.WriteLine($"File: {evnt.FullPath} {evnt.ChangeType}");
        }

        private void OnRenamed(object source, RenamedEventArgs evnt)
        {
            filesModified.Add(new FileInfo(evnt.Name, evnt.FullPath));
            // Specify what is done when a file is renamed.
            logs.WriteLine($"File: {evnt.OldFullPath} renamed to {evnt.FullPath}");
        }
    }
}
