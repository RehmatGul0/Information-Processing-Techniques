using System;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.IO;

namespace k163680_Q2
{
    public partial class EmailToXML : Form
    {
        Random random = new Random();
        public EmailToXML()
        {
            InitializeComponent();
        }

        private void EmailToXML_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
     
        private void SUBMIT_Click(object sender, EventArgs e)
        {

            if (!IsValidEmail(this.RecipientTextBox.Text))
            {
                this.RecipientTextBox.Text = "";
            }
            else if(this.RecipientTextBox.Text != "" && this.SubjectTextBox.Text!="" && this.MessageTextArea.Text!="")
            {
                //using random number + file count in the directory for unique email file name
                using (XmlWriter writer = XmlWriter.Create(ConfigurationManager.AppSettings.Get("Path")+"/"+random.Next()+""+ Directory.GetFiles(ConfigurationManager.AppSettings.Get("Path"), "*", SearchOption.TopDirectoryOnly).Length + ".xml"))
                {
                    writer.WriteStartElement("EmailMessage");
                    writer.WriteElementString("To", this.RecipientTextBox.Text);
                    writer.WriteElementString("Subject", this.SubjectTextBox.Text);
                    writer.WriteElementString("Body", this.MessageTextArea.Text);
                    writer.WriteEndElement();
                    writer.Flush();
                }
                this.RecipientTextBox.Text = "";
                this.SubjectTextBox.Text = "";
                this.MessageTextArea.Text = "";
            }

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
