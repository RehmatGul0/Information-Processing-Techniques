namespace k163680_Q2
{
    partial class EmailToXML
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RecipientLabel = new System.Windows.Forms.Label();
            this.RecipientTextBox = new System.Windows.Forms.TextBox();
            this.SubjectLabel = new System.Windows.Forms.Label();
            this.SubjectTextBox = new System.Windows.Forms.TextBox();
            this.BodyLabel = new System.Windows.Forms.Label();
            this.MessageTextArea = new System.Windows.Forms.RichTextBox();
            this.SUBMIT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RecipientLabel
            // 
            this.RecipientLabel.AutoSize = true;
            this.RecipientLabel.Location = new System.Drawing.Point(37, 36);
            this.RecipientLabel.Name = "RecipientLabel";
            this.RecipientLabel.Size = new System.Drawing.Size(22, 13);
            this.RecipientLabel.TabIndex = 0;
            this.RecipientLabel.Text = "TO";
            this.RecipientLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // RecipientTextBox
            // 
            this.RecipientTextBox.Location = new System.Drawing.Point(116, 29);
            this.RecipientTextBox.Name = "RecipientTextBox";
            this.RecipientTextBox.Size = new System.Drawing.Size(389, 20);
            this.RecipientTextBox.TabIndex = 1;
            // 
            // SubjectLabel
            // 
            this.SubjectLabel.AutoSize = true;
            this.SubjectLabel.Location = new System.Drawing.Point(37, 86);
            this.SubjectLabel.Name = "SubjectLabel";
            this.SubjectLabel.Size = new System.Drawing.Size(55, 13);
            this.SubjectLabel.TabIndex = 2;
            this.SubjectLabel.Text = "SUBJECT";
            this.SubjectLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // SubjectTextBox
            // 
            this.SubjectTextBox.Location = new System.Drawing.Point(116, 79);
            this.SubjectTextBox.Name = "SubjectTextBox";
            this.SubjectTextBox.Size = new System.Drawing.Size(389, 20);
            this.SubjectTextBox.TabIndex = 3;
            // 
            // BodyLabel
            // 
            this.BodyLabel.AutoSize = true;
            this.BodyLabel.Location = new System.Drawing.Point(37, 136);
            this.BodyLabel.Name = "BodyLabel";
            this.BodyLabel.Size = new System.Drawing.Size(50, 13);
            this.BodyLabel.TabIndex = 4;
            this.BodyLabel.Text = "Message";
            this.BodyLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // MessageTextArea
            // 
            this.MessageTextArea.Location = new System.Drawing.Point(116, 133);
            this.MessageTextArea.Name = "MessageTextArea";
            this.MessageTextArea.Size = new System.Drawing.Size(389, 164);
            this.MessageTextArea.TabIndex = 5;
            this.MessageTextArea.Text = "";
            // 
            // SUBMIT
            // 
            this.SUBMIT.BackColor = System.Drawing.SystemColors.Window;
            this.SUBMIT.Location = new System.Drawing.Point(323, 326);
            this.SUBMIT.Name = "SUBMIT";
            this.SUBMIT.Size = new System.Drawing.Size(182, 23);
            this.SUBMIT.TabIndex = 6;
            this.SUBMIT.Text = "SUBMIT";
            this.SUBMIT.UseVisualStyleBackColor = false;
            this.SUBMIT.Click += new System.EventHandler(this.SUBMIT_Click);
            // 
            // EmailToXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.SUBMIT);
            this.Controls.Add(this.MessageTextArea);
            this.Controls.Add(this.BodyLabel);
            this.Controls.Add(this.SubjectTextBox);
            this.Controls.Add(this.SubjectLabel);
            this.Controls.Add(this.RecipientTextBox);
            this.Controls.Add(this.RecipientLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EmailToXML";
            this.Text = "EMAIL TO XML";
            this.Load += new System.EventHandler(this.EmailToXML_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RecipientLabel;
        private System.Windows.Forms.TextBox RecipientTextBox;
        private System.Windows.Forms.Label SubjectLabel;
        private System.Windows.Forms.TextBox SubjectTextBox;
        private System.Windows.Forms.Label BodyLabel;
        private System.Windows.Forms.RichTextBox MessageTextArea;
        private System.Windows.Forms.Button SUBMIT;
    }
}

