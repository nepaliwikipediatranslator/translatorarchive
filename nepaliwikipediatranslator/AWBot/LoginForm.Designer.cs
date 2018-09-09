namespace NepaliWikiPediaTranslator.AWBot
{
    partial class LoginForm
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
            this.WikiProjectList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RememberToken = new System.Windows.Forms.CheckBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WikiProjectList
            // 
            this.WikiProjectList.FormattingEnabled = true;
            this.WikiProjectList.Location = new System.Drawing.Point(153, 71);
            this.WikiProjectList.Name = "WikiProjectList";
            this.WikiProjectList.Size = new System.Drawing.Size(121, 21);
            this.WikiProjectList.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 27);
            this.label2.TabIndex = 22;
            this.label2.Text = "Password: ";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 28);
            this.label1.TabIndex = 21;
            this.label1.Text = "Username:";
            // 
            // RememberToken
            // 
            this.RememberToken.Location = new System.Drawing.Point(20, 71);
            this.RememberToken.Name = "RememberToken";
            this.RememberToken.Size = new System.Drawing.Size(134, 21);
            this.RememberToken.TabIndex = 20;
            this.RememberToken.Text = "Remember the Token";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(100, 44);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(174, 20);
            this.Password.TabIndex = 1;
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(100, 16);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(174, 20);
            this.Username.TabIndex = 0;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(87, 116);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(67, 28);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "&Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Login
            // 
            this.Login.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Login.Location = new System.Drawing.Point(167, 116);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(67, 28);
            this.Login.TabIndex = 3;
            this.Login.Text = "&Login";
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 167);
            this.Controls.Add(this.WikiProjectList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RememberToken);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Login);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox WikiProjectList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox RememberToken;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Login;
    }
}