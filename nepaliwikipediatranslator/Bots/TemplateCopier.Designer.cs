namespace NepaliWikiPediaTranslator.Bots
{
    partial class TemplateCopier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateCopier));
            this.richTextBoxTemplateList = new System.Windows.Forms.RichTextBox();
            this.CopyTemplateFromEnglishButton = new System.Windows.Forms.Button();
            this.articleInsertButton = new System.Windows.Forms.Button();
            this.articleInsertTextBox = new System.Windows.Forms.TextBox();
            this.articlelistBox = new System.Windows.Forms.ListBox();
            this.parseArticleListButton = new System.Windows.Forms.Button();
            this.CopyTemplateFromHindiButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseArtileListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editListParserRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableTranslationOfTemplatesBeforeSaingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTemplatesFromEnglishWikipediaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTemplatesFromHindiWikipediaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSummaryTextBox = new System.Windows.Forms.TextBox();
            this.lblEditSummary = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.userContributionsButton = new System.Windows.Forms.Button();
            this.richTextBoxLogViewer = new System.Windows.Forms.RichTextBox();
            this.EnableTranslationCheckBox = new System.Windows.Forms.CheckBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.lblTemplateListText = new System.Windows.Forms.Label();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.comboWikiSelector = new System.Windows.Forms.ComboBox();
            this.comboBoxThreadSleepTime = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxTemplateList
            // 
            this.richTextBoxTemplateList.Location = new System.Drawing.Point(503, 67);
            this.richTextBoxTemplateList.Name = "richTextBoxTemplateList";
            this.richTextBoxTemplateList.Size = new System.Drawing.Size(259, 225);
            this.richTextBoxTemplateList.TabIndex = 4;
            this.richTextBoxTemplateList.Text = "";
            // 
            // CopyTemplateFromEnglishButton
            // 
            this.CopyTemplateFromEnglishButton.Location = new System.Drawing.Point(334, 132);
            this.CopyTemplateFromEnglishButton.Name = "CopyTemplateFromEnglishButton";
            this.CopyTemplateFromEnglishButton.Size = new System.Drawing.Size(108, 34);
            this.CopyTemplateFromEnglishButton.TabIndex = 5;
            this.CopyTemplateFromEnglishButton.Text = "Copy Templates from english";
            this.CopyTemplateFromEnglishButton.UseVisualStyleBackColor = true;
            this.CopyTemplateFromEnglishButton.Click += new System.EventHandler(this.CopyTemplatesFromEnglishButton_Click);
            // 
            // articleInsertButton
            // 
            this.articleInsertButton.Location = new System.Drawing.Point(254, 30);
            this.articleInsertButton.Name = "articleInsertButton";
            this.articleInsertButton.Size = new System.Drawing.Size(21, 23);
            this.articleInsertButton.TabIndex = 1;
            this.articleInsertButton.Text = "+";
            this.articleInsertButton.UseVisualStyleBackColor = true;
            this.articleInsertButton.Click += new System.EventHandler(this.articleInsertButton_Click);
            // 
            // articleInsertTextBox
            // 
            this.articleInsertTextBox.Location = new System.Drawing.Point(21, 30);
            this.articleInsertTextBox.Name = "articleInsertTextBox";
            this.articleInsertTextBox.Size = new System.Drawing.Size(227, 20);
            this.articleInsertTextBox.TabIndex = 3;
            this.articleInsertTextBox.TextChanged += new System.EventHandler(this.articleInsertTextBox_TextChanged);
            // 
            // articlelistBox
            // 
            this.articlelistBox.FormattingEnabled = true;
            this.articlelistBox.Location = new System.Drawing.Point(21, 56);
            this.articlelistBox.Name = "articlelistBox";
            this.articlelistBox.Size = new System.Drawing.Size(254, 43);
            this.articlelistBox.TabIndex = 2;
            this.articlelistBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.articlelistBox_MouseDoubleClick);
            // 
            // parseArticleListButton
            // 
            this.parseArticleListButton.Location = new System.Drawing.Point(22, 19);
            this.parseArticleListButton.Name = "parseArticleListButton";
            this.parseArticleListButton.Size = new System.Drawing.Size(130, 36);
            this.parseArticleListButton.TabIndex = 6;
            this.parseArticleListButton.Text = "Parse Article List";
            this.parseArticleListButton.UseVisualStyleBackColor = true;
            this.parseArticleListButton.Click += new System.EventHandler(this.wantedTemplatesbtn_Click);
            // 
            // CopyTemplateFromHindiButton
            // 
            this.CopyTemplateFromHindiButton.Location = new System.Drawing.Point(334, 172);
            this.CopyTemplateFromHindiButton.Name = "CopyTemplateFromHindiButton";
            this.CopyTemplateFromHindiButton.Size = new System.Drawing.Size(108, 38);
            this.CopyTemplateFromHindiButton.TabIndex = 7;
            this.CopyTemplateFromHindiButton.Text = "Copy Template from Hindi";
            this.CopyTemplateFromHindiButton.UseVisualStyleBackColor = true;
            this.CopyTemplateFromHindiButton.Click += new System.EventHandler(this.CopyTemplatesFromHindiButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.templatesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseArtileListToolStripMenuItem,
            this.editListParserRulesToolStripMenuItem,
            this.enableTranslationOfTemplatesBeforeSaingToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // parseArtileListToolStripMenuItem
            // 
            this.parseArtileListToolStripMenuItem.Name = "parseArtileListToolStripMenuItem";
            this.parseArtileListToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.parseArtileListToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.parseArtileListToolStripMenuItem.Text = "Parse Artile List";
            this.parseArtileListToolStripMenuItem.Click += new System.EventHandler(this.parseArtileListToolStripMenuItem_Click);
            // 
            // editListParserRulesToolStripMenuItem
            // 
            this.editListParserRulesToolStripMenuItem.Name = "editListParserRulesToolStripMenuItem";
            this.editListParserRulesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Space";
            this.editListParserRulesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.editListParserRulesToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.editListParserRulesToolStripMenuItem.Text = "Edit list parser rules";
            this.editListParserRulesToolStripMenuItem.Click += new System.EventHandler(this.editListParserRulesToolStripMenuItem_Click);
            // 
            // enableTranslationOfTemplatesBeforeSaingToolStripMenuItem
            // 
            this.enableTranslationOfTemplatesBeforeSaingToolStripMenuItem.CheckOnClick = true;
            this.enableTranslationOfTemplatesBeforeSaingToolStripMenuItem.Name = "enableTranslationOfTemplatesBeforeSaingToolStripMenuItem";
            this.enableTranslationOfTemplatesBeforeSaingToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.enableTranslationOfTemplatesBeforeSaingToolStripMenuItem.Text = "Enable Translation of templates before saing";
            // 
            // templatesToolStripMenuItem
            // 
            this.templatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTemplatesFromEnglishWikipediaToolStripMenuItem1,
            this.copyTemplatesFromHindiWikipediaToolStripMenuItem});
            this.templatesToolStripMenuItem.Name = "templatesToolStripMenuItem";
            this.templatesToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.templatesToolStripMenuItem.Text = "Templates";
            // 
            // copyTemplatesFromEnglishWikipediaToolStripMenuItem1
            // 
            this.copyTemplatesFromEnglishWikipediaToolStripMenuItem1.Name = "copyTemplatesFromEnglishWikipediaToolStripMenuItem1";
            this.copyTemplatesFromEnglishWikipediaToolStripMenuItem1.Size = new System.Drawing.Size(260, 22);
            this.copyTemplatesFromEnglishWikipediaToolStripMenuItem1.Text = "Copy Templates from English Wikipedia";
            this.copyTemplatesFromEnglishWikipediaToolStripMenuItem1.Click += new System.EventHandler(this.copyTemplatesFromEnglishWikipediaToolStripMenuItem1_Click);
            // 
            // copyTemplatesFromHindiWikipediaToolStripMenuItem
            // 
            this.copyTemplatesFromHindiWikipediaToolStripMenuItem.Name = "copyTemplatesFromHindiWikipediaToolStripMenuItem";
            this.copyTemplatesFromHindiWikipediaToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.copyTemplatesFromHindiWikipediaToolStripMenuItem.Text = "Copy Templates from Hindi Wikipedia";
            this.copyTemplatesFromHindiWikipediaToolStripMenuItem.Click += new System.EventHandler(this.copyTemplatesFromHindiWikipediaToolStripMenuItem_Click);
            // 
            // editSummaryTextBox
            // 
            this.editSummaryTextBox.Location = new System.Drawing.Point(22, 91);
            this.editSummaryTextBox.Name = "editSummaryTextBox";
            this.editSummaryTextBox.Size = new System.Drawing.Size(145, 20);
            this.editSummaryTextBox.TabIndex = 9;
            this.editSummaryTextBox.Text = "Copying templates from English Wiki";
            // 
            // lblEditSummary
            // 
            this.lblEditSummary.AutoSize = true;
            this.lblEditSummary.Location = new System.Drawing.Point(22, 73);
            this.lblEditSummary.Name = "lblEditSummary";
            this.lblEditSummary.Size = new System.Drawing.Size(71, 13);
            this.lblEditSummary.TabIndex = 10;
            this.lblEditSummary.Text = "Edit Summary";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripSplitButton1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 350);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel1.Text = "Ready                ";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // userContributionsButton
            // 
            this.userContributionsButton.Location = new System.Drawing.Point(312, 216);
            this.userContributionsButton.Name = "userContributionsButton";
            this.userContributionsButton.Size = new System.Drawing.Size(130, 23);
            this.userContributionsButton.TabIndex = 12;
            this.userContributionsButton.Text = "User Contributions";
            this.userContributionsButton.UseVisualStyleBackColor = true;
            this.userContributionsButton.Visible = false;
            this.userContributionsButton.Click += new System.EventHandler(this.userContributionsButton_Click);
            // 
            // richTextBoxLogViewer
            // 
            this.richTextBoxLogViewer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBoxLogViewer.Location = new System.Drawing.Point(0, 372);
            this.richTextBoxLogViewer.Name = "richTextBoxLogViewer";
            this.richTextBoxLogViewer.Size = new System.Drawing.Size(784, 190);
            this.richTextBoxLogViewer.TabIndex = 4;
            this.richTextBoxLogViewer.Text = "";
            // 
            // EnableTranslationCheckBox
            // 
            this.EnableTranslationCheckBox.AutoSize = true;
            this.EnableTranslationCheckBox.Location = new System.Drawing.Point(22, 117);
            this.EnableTranslationCheckBox.Name = "EnableTranslationCheckBox";
            this.EnableTranslationCheckBox.Size = new System.Drawing.Size(245, 17);
            this.EnableTranslationCheckBox.TabIndex = 13;
            this.EnableTranslationCheckBox.Text = "Enable Translation of Templates before saving";
            this.EnableTranslationCheckBox.UseVisualStyleBackColor = true;
            this.EnableTranslationCheckBox.CheckedChanged += new System.EventHandler(this.EnableTranslationCheckBox_CheckedChanged);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(12, 314);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 14;
            this.lblLog.Text = "Log";
            // 
            // lblTemplateListText
            // 
            this.lblTemplateListText.AutoSize = true;
            this.lblTemplateListText.Location = new System.Drawing.Point(503, 48);
            this.lblTemplateListText.Name = "lblTemplateListText";
            this.lblTemplateListText.Size = new System.Drawing.Size(95, 13);
            this.lblTemplateListText.TabIndex = 15;
            this.lblTemplateListText.Text = "Templates to Copy";
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.comboBoxThreadSleepTime);
            this.groupBoxOptions.Controls.Add(this.comboWikiSelector);
            this.groupBoxOptions.Controls.Add(this.parseArticleListButton);
            this.groupBoxOptions.Controls.Add(this.editSummaryTextBox);
            this.groupBoxOptions.Controls.Add(this.lblEditSummary);
            this.groupBoxOptions.Controls.Add(this.EnableTranslationCheckBox);
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 123);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(282, 169);
            this.groupBoxOptions.TabIndex = 16;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // comboWikiSelector
            // 
            this.comboWikiSelector.FormattingEnabled = true;
            this.comboWikiSelector.Location = new System.Drawing.Point(175, 22);
            this.comboWikiSelector.Name = "comboWikiSelector";
            this.comboWikiSelector.Size = new System.Drawing.Size(107, 21);
            this.comboWikiSelector.TabIndex = 17;
            // 
            // comboBoxThreadSleepTime
            // 
            this.comboBoxThreadSleepTime.FormattingEnabled = true;
            this.comboBoxThreadSleepTime.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100",
            "110",
            "120"});
            this.comboBoxThreadSleepTime.Location = new System.Drawing.Point(175, 50);
            this.comboBoxThreadSleepTime.Name = "comboBoxThreadSleepTime";
            this.comboBoxThreadSleepTime.Size = new System.Drawing.Size(61, 21);
            this.comboBoxThreadSleepTime.TabIndex = 18;
            this.comboBoxThreadSleepTime.SelectedIndexChanged += new System.EventHandler(this.comboBoxThreadSleepTime_SelectedIndexChanged);
            // 
            // TemplateCopier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.lblTemplateListText);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.userContributionsButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CopyTemplateFromHindiButton);
            this.Controls.Add(this.CopyTemplateFromEnglishButton);
            this.Controls.Add(this.richTextBoxLogViewer);
            this.Controls.Add(this.richTextBoxTemplateList);
            this.Controls.Add(this.articleInsertTextBox);
            this.Controls.Add(this.articleInsertButton);
            this.Controls.Add(this.articlelistBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TemplateCopier";
            this.Text = "TemplateCopier";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TemplateCopier_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxTemplateList;
        private System.Windows.Forms.Button CopyTemplateFromEnglishButton;
        private System.Windows.Forms.Button articleInsertButton;
        private System.Windows.Forms.TextBox articleInsertTextBox;
        private System.Windows.Forms.ListBox articlelistBox;
        private System.Windows.Forms.Button parseArticleListButton;
        private System.Windows.Forms.Button CopyTemplateFromHindiButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTemplatesFromEnglishWikipediaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyTemplatesFromHindiWikipediaToolStripMenuItem;
        private System.Windows.Forms.TextBox editSummaryTextBox;
        private System.Windows.Forms.Label lblEditSummary;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button userContributionsButton;
        private System.Windows.Forms.RichTextBox richTextBoxLogViewer;
        private System.Windows.Forms.CheckBox EnableTranslationCheckBox;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label lblTemplateListText;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.ToolStripMenuItem parseArtileListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editListParserRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableTranslationOfTemplatesBeforeSaingToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboWikiSelector;
        private System.Windows.Forms.ComboBox comboBoxThreadSleepTime;

    }
}