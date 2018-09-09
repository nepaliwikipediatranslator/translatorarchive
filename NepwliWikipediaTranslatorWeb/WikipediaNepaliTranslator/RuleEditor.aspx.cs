using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WikipediaNepaliTranslator
{
    public partial class RuleEditor : System.Web.UI.Page
    {
        public string FileName { get; set; }
        private SearchBox searchBox;
        private TextBox richTextBox1 { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetFileName(string fileName)
        {
            FileName = fileName;
            if (string.IsNullOrEmpty(FileName))
                FileName = _Default.RuleFileName;

            OpenInputFile(FileName);
            searchBox = new SearchBox();
            searchBox.rtb = richTextBox1;
        }
        /// <summary>
        /// Opens input file
        /// </summary>
        /// <param name="inputFileName"></param>
        private void OpenInputFile(string inputFileName)
        {
            string tempString;
            string inputText = "";

            if (!File.Exists(inputFileName))
            {
                var fileStream = File.Create(inputFileName);
                fileStream.Close();
            }

            var sr = new StreamReader(inputFileName);
            while ((tempString = sr.ReadLine()) != null)
            {
                inputText += (tempString + "\n");
            }
            sr.Close();
            richTextBox1.Text = inputText;
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                var sr = new StreamWriter(FileName);
                sr.Write(richTextBox1.Text);
                sr.Close();
            }
        }
         
    }
}