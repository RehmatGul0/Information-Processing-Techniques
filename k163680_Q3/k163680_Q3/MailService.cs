using System;
using System.Net.Mail;
using System.ServiceProcess;
using System.Configuration;
using System.Timers;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Security;

namespace k163680_Q3
{
    public partial class MailService3680 : ServiceBase
    {
        private StreamWriter logs;
        public MailService3680()
        {
            InitializeComponent();
            logs = new StreamWriter(Path.Combine(ConfigurationManager.AppSettings.Get("LogFilePath"), "logs.txt"));


        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer time = new System.Timers.Timer();
            time.Start();
            time.Interval = 900000;
            time.Elapsed += time_elapsed;
        }

        protected override void OnStop()
        {
            logs.Dispose();
            logs.Close();
        }
        public void time_elapsed(object sender, ElapsedEventArgs e)
        {
            logs.WriteLine("Mail Sending on " + DateTime.Now.ToString());
            SendEmail();
        }
        public void SendEmail()
        {
            MailMessage mail = new MailMessage();
            MailAddress mailAddress = null;
            DirectoryInfo directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings.Get("EmailFilesPath"));
            FileInfo[] files = directoryInfo.GetFiles("*.xml"); //Getting Text files
            for(int i=0; i<files.Length; i++)
            {
                XmlDocument mailXml = new XmlDocument();
                mailXml.Load(ConfigurationManager.AppSettings.Get("EmailFilesPath") +'/'+files[0].Name);
                JObject mailJson = JObject.Parse(JsonConvert.SerializeXmlNode(mailXml));
                try
                {
                    mail.To.Add((string)mailJson["EmailMessage"]["To"]);
                    mailAddress = new MailAddress(ConfigurationManager.AppSettings.Get("FromMail"));
                    mail.From = mailAddress;
                    mail.Subject = (string)mailJson["EmailMessage"]["Subject"];
                    mail.Body = (string)mailJson["EmailMessage"]["Body"];

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
                catch (Exception ex)
                {
                    logs.WriteLine("Error occured"+ex);
                }
            }
            
        }
    }
}
