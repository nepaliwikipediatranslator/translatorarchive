namespace ExamplePlugin.Bots
{
    partial class BotWatcher
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
            this.richTextBoxLogger = new System.Windows.Forms.RichTextBox();
            this.btnFindAndReplace = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLogger = new System.Windows.Forms.Panel();
            this.progressBarBotWatcher = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.panelLogger.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxLogger
            // 
            this.richTextBoxLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLogger.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxLogger.Name = "richTextBoxLogger";
            this.richTextBoxLogger.ReadOnly = true;
            this.richTextBoxLogger.Size = new System.Drawing.Size(469, 316);
            this.richTextBoxLogger.TabIndex = 0;
            this.richTextBoxLogger.Text = "";
            // 
            // btnFindAndReplace
            // 
            this.btnFindAndReplace.Location = new System.Drawing.Point(12, 69);
            this.btnFindAndReplace.Name = "btnFindAndReplace";
            this.btnFindAndReplace.Size = new System.Drawing.Size(75, 40);
            this.btnFindAndReplace.TabIndex = 1;
            this.btnFindAndReplace.Text = "Find and Replace";
            this.btnFindAndReplace.UseVisualStyleBackColor = true;
            this.btnFindAndReplace.Click += new System.EventHandler(this.btnFindAndReplace_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(12, 40);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(585, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // panelLogger
            // 
            this.panelLogger.Controls.Add(this.richTextBoxLogger);
            this.panelLogger.Location = new System.Drawing.Point(116, 40);
            this.panelLogger.Name = "panelLogger";
            this.panelLogger.Size = new System.Drawing.Size(469, 316);
            this.panelLogger.TabIndex = 4;
            // 
            // progressBarBotWatcher
            // 
            this.progressBarBotWatcher.Location = new System.Drawing.Point(374, 1);
            this.progressBarBotWatcher.Name = "progressBarBotWatcher";
            this.progressBarBotWatcher.Size = new System.Drawing.Size(211, 23);
            this.progressBarBotWatcher.TabIndex = 5;
            // 
            // BotWatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 408);
            this.Controls.Add(this.progressBarBotWatcher);
            this.Controls.Add(this.panelLogger);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnFindAndReplace);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BotWatcher";
            this.Text = "BotWatcher";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelLogger.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxLogger;
        private System.Windows.Forms.Button btnFindAndReplace;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panelLogger;
        private System.Windows.Forms.ProgressBar progressBarBotWatcher;
    }
}