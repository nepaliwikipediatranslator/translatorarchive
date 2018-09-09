using System;
using System.IO;
using System.Windows.Forms;

namespace ExamplePlugin
{
    public partial class RuleEditor : Form
    {
        private SearchBox searchBox;


        public RuleEditor()
        {
            InitializeComponent();
        }

        public string FileName { get; set; }

        public void SetFileName(string fileName)
        {
            FileName = fileName;
            if (string.IsNullOrEmpty(FileName))
                return;

            if (!File.Exists(fileName))
            {
                FileStream a = File.Create(fileName);
                a.Close();


                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream nounStream = asm.GetManifestResourceStream("ExamplePlugin." + fileName);

                if (nounStream != null)
                {
                    //Writing in new file
                    StreamWriter swr = new StreamWriter(fileName);
                    string tempString;
                    string inputText = "";
                    StreamReader sr = new StreamReader(nounStream);

                    while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
                    {
                        inputText += (tempString + "\n");
                       
                    }
                    swr.Write(inputText);
                    swr.Close();
                    richTextBox1.Text = inputText;
                }
                 
                
            }
            OpenInputFile(FileName);
           
            searchBox = new SearchBox();
            searchBox.rtb = richTextBox1;
             
        }


        private void RuleEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }



        /// <summary>
        /// Opens input file
        /// </summary>
        /// <param name="inputFileName"></param>
        private void OpenInputFile(string inputFileName)
        {
            string tempString;
            string inputText = "";


            //Fixing Bug: The process cannot access the file 'C:\Users\Rajesh\Documents\Visual Studio 2010\Projects\Nepwikitrans\WikiPediaTranslator\bin\Debug\Rules.db' because it is being used by another process.
            if (!File.Exists(inputFileName))
            {
                FileStream openStream = File.Create(inputFileName);
                openStream.Close();
            }

            if (File.Exists(inputFileName))
            {
                var sr = new StreamReader(inputFileName);
                while ((tempString = sr.ReadLine()) != null)
                {
                    inputText += (tempString + "\n");
                }
                sr.Close();
                richTextBox1.Text = inputText;
            }
        }

        /// <summary>
        /// Saves the text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                var sr = new StreamWriter(FileName);
                sr.Write(richTextBox1.Text);
                sr.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// Activate the find control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchBox == null || searchBox.IsDisposed)
            {
                searchBox = new SearchBox();
                searchBox.rtb = richTextBox1;
            }

            //this.Hide();
            searchBox.Show();
        }

        /// <summary>
        /// this is for hiding the form on pressing escape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //quit form)
            if (e.KeyChar == (char)27)
            {
                // Then escape key was pressed
                Hide();
            }
        }

        private void findAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchBox == null || searchBox.IsDisposed)
            {
                searchBox = new SearchBox();
                searchBox.rtb = richTextBox1;
                searchBox.Show();
            }
            searchBox.FindText(sender, e);
        }
    }
}