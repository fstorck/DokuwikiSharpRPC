namespace RPC_TestUI
{
    partial class MainFrm
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
            this.BTN_Connect = new System.Windows.Forms.Button();
            this.TBX_Url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TBX_Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TBX_User = new System.Windows.Forms.TextBox();
            this.BTN_PutPages = new System.Windows.Forms.Button();
            this.FBD_Files = new System.Windows.Forms.FolderBrowserDialog();
            this.OFD_Upload = new System.Windows.Forms.OpenFileDialog();
            this.BTN_UploadTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BTN_Connect
            // 
            this.BTN_Connect.Location = new System.Drawing.Point(15, 76);
            this.BTN_Connect.Name = "BTN_Connect";
            this.BTN_Connect.Size = new System.Drawing.Size(220, 44);
            this.BTN_Connect.TabIndex = 0;
            this.BTN_Connect.Text = "Connect Test";
            this.BTN_Connect.UseVisualStyleBackColor = true;
            this.BTN_Connect.Click += new System.EventHandler(this.BTN_Connect_Click);
            // 
            // TBX_Url
            // 
            this.TBX_Url.Location = new System.Drawing.Point(47, 12);
            this.TBX_Url.Name = "TBX_Url";
            this.TBX_Url.Size = new System.Drawing.Size(417, 20);
            this.TBX_Url.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL";
            // 
            // TBX_Password
            // 
            this.TBX_Password.Location = new System.Drawing.Point(256, 38);
            this.TBX_Password.Name = "TBX_Password";
            this.TBX_Password.PasswordChar = '*';
            this.TBX_Password.Size = new System.Drawing.Size(208, 20);
            this.TBX_Password.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // TBX_User
            // 
            this.TBX_User.Location = new System.Drawing.Point(47, 38);
            this.TBX_User.Name = "TBX_User";
            this.TBX_User.Size = new System.Drawing.Size(144, 20);
            this.TBX_User.TabIndex = 1;
            // 
            // BTN_PutPages
            // 
            this.BTN_PutPages.Location = new System.Drawing.Point(15, 126);
            this.BTN_PutPages.Name = "BTN_PutPages";
            this.BTN_PutPages.Size = new System.Drawing.Size(220, 44);
            this.BTN_PutPages.TabIndex = 0;
            this.BTN_PutPages.Text = "Put PagesTest";
            this.BTN_PutPages.UseVisualStyleBackColor = true;
            this.BTN_PutPages.Click += new System.EventHandler(this.BTN_PutPages_Click);
            // 
            // OFD_Upload
            // 
            this.OFD_Upload.FileName = "OFD_Upload";
            // 
            // BTN_UploadTest
            // 
            this.BTN_UploadTest.Location = new System.Drawing.Point(244, 126);
            this.BTN_UploadTest.Name = "BTN_UploadTest";
            this.BTN_UploadTest.Size = new System.Drawing.Size(220, 44);
            this.BTN_UploadTest.TabIndex = 0;
            this.BTN_UploadTest.Text = "File Upload Test";
            this.BTN_UploadTest.UseVisualStyleBackColor = true;
            this.BTN_UploadTest.Click += new System.EventHandler(this.BTN_UploadTest_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 262);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBX_User);
            this.Controls.Add(this.TBX_Password);
            this.Controls.Add(this.TBX_Url);
            this.Controls.Add(this.BTN_UploadTest);
            this.Controls.Add(this.BTN_PutPages);
            this.Controls.Add(this.BTN_Connect);
            this.Name = "MainFrm";
            this.Text = "Dokuwiki Connector Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Connect;
        private System.Windows.Forms.TextBox TBX_Url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBX_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TBX_User;
        private System.Windows.Forms.Button BTN_PutPages;
        private System.Windows.Forms.FolderBrowserDialog FBD_Files;
        private System.Windows.Forms.OpenFileDialog OFD_Upload;
        private System.Windows.Forms.Button BTN_UploadTest;
    }
}

