namespace NepaliWikiPediaTranslator.AWBot
{
    partial class UrlEdit
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
            this.comboWikiSelector = new System.Windows.Forms.ComboBox();
            this.btnGetSourceCode = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboWikiSelector
            // 
            this.comboWikiSelector.FormattingEnabled = true;
            this.comboWikiSelector.Location = new System.Drawing.Point(18, 11);
            this.comboWikiSelector.Name = "comboWikiSelector";
            this.comboWikiSelector.Size = new System.Drawing.Size(107, 21);
            this.comboWikiSelector.TabIndex = 6;
            // 
            // btnGetSourceCode
            // 
            this.btnGetSourceCode.Location = new System.Drawing.Point(236, 37);
            this.btnGetSourceCode.Name = "btnGetSourceCode";
            this.btnGetSourceCode.Size = new System.Drawing.Size(75, 23);
            this.btnGetSourceCode.TabIndex = 5;
            this.btnGetSourceCode.Text = "Get Source";
            this.btnGetSourceCode.UseVisualStyleBackColor = true;
            this.btnGetSourceCode.Click += new System.EventHandler(this.btnGetSourceCode_Click);
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(145, 11);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(402, 20);
            this.urlTextBox.TabIndex = 4;
            this.urlTextBox.Leave += new System.EventHandler(this.urlTextBox_Leave);
            // 
            // UrlEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 71);
            this.Controls.Add(this.comboWikiSelector);
            this.Controls.Add(this.btnGetSourceCode);
            this.Controls.Add(this.urlTextBox);
            this.Name = "UrlEdit";
            this.Text = "UrlEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboWikiSelector;
        private System.Windows.Forms.Button btnGetSourceCode;
        private System.Windows.Forms.TextBox urlTextBox;
    }
}