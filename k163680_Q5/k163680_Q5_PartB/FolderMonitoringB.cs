using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace k163680_Q5_PartB
{
    public partial class FolderMonitorServiceB : ServiceBase
    {
        private System.Timers.Timer time;
        private FileSystemWatcher watcher;
        private List<FileInformation> filesModified;
        private StreamWriter logs;
        public FolderMonitorServiceB()
        {
            logs = new StreamWriter(Path.Combine(ConfigurationManager.AppSettings.Get("Path"), "logs.txt"));
            time = new System.Timers.Timer();
            watcher = new FileSystemWatcher();
            filesModified = new List<FileInformation>();
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
                time.Interval = 900000;
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
            sendMail();
        }

        public void sendMail()
        {
            StringBuilder body = new StringBuilder();
            foreach(FileInformation fileInfo in filesModified)
            {
                body.Append("File Name is " + fileInfo.getName() + " and size is " + fileInfo.getSize() + " bytes..");
            }
            MailMessage mail = new MailMessage();
            MailAddress mailAddress = null;
            mail.To.Add(ConfigurationManager.AppSettings.Get("ToMail"));
            mailAddress = new MailAddress(ConfigurationManager.AppSettings.Get("FromMail"));
            mail.From = mailAddress;
            mail.Subject = "Directory Change Detection Notification";
            mail.Body = body.ToString();

            SecureString password = new SecureString();
            foreach (char passwordChar in ConfigurationManager.AppSettings.Get("password"))
                password.AppendChar(passwordChar);


            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings.Get("FromMail"), password);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);

            logs.WriteLine("Mail Send Successfully");

        }
        private void OnChanged(object source, FileSystemEventArgs evnt)
        {
            if (File.Exists(evnt.FullPath))
            {
                FileInfo file = new FileInfo(evnt.FullPath);
                filesModified.Add(new FileInformation(evnt.Name, evnt.FullPath,file.Length));
            }
            // Specify what is done when a file is changed, created, or deleted.
            logs.WriteLine($"File: {evnt.FullPath} {evnt.ChangeType}");
        }

        private void OnRenamed(object source, RenamedEventArgs evnt)
        {
            if (File.Exists(evnt.FullPath))
            {
                FileInfo file = new FileInfo(evnt.FullPath);
                filesModified.Add(new FileInformation(evnt.Name, evnt.FullPath, file.Length));
            }
            // Specify what is done when a file is renamed.
            logs.WriteLine($"File: {evnt.OldFullPath} renamed to {evnt.FullPath}");
        }
    }
}
