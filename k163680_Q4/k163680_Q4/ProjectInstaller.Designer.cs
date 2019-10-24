namespace k163680_Q4
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NewsFeedServiceInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.NewsFeedService = new System.ServiceProcess.ServiceInstaller();
            // 
            // NewsFeedServiceInstaller
            // 
            this.NewsFeedServiceInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.NewsFeedServiceInstaller.Password = null;
            this.NewsFeedServiceInstaller.Username = null;
            // 
            // NewsFeedService
            // 
            this.NewsFeedService.ServiceName = "RssFeedService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.NewsFeedServiceInstaller,
            this.NewsFeedService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller NewsFeedServiceInstaller;
        private System.ServiceProcess.ServiceInstaller NewsFeedService;
    }
}