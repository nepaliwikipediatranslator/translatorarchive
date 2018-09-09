using System;
using System.Drawing;
using System.Windows.Forms;

namespace NepaliWikiPediaTranslator
{
    public partial class SearchBox : Form
    {
        public RichTextBox rtb { get; set; }
        int start = 0;
        int indexOfSearchText = 0;
        public SearchBox()
        {
            InitializeComponent();
            this.ResizeRedraw = false;
            this.searchTextBox.AcceptsReturn = true;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            start = 0;
            indexOfSearchText = 0;
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindText(sender,e);
        }
        public void FindText(object sender, EventArgs e)
        {
            int startindex = 0;

            if (searchTextBox.Text.Length > 0)
                startindex = FindMyText(searchTextBox.Text.Trim(), start, rtb.Text.Length);

            // If string was found in the RichTextBox, highlight it
            if (startindex >= 0)
            {
                // Set the highlight color as red
                rtb.SelectionColor = Color.Red;
                // Find the end index. End Index = number of characters in textbox
                int endindex = searchTextBox.Text.Length;
                // Highlight the search string
                rtb.Select(startindex, endindex);
                // mark the start position after the position of 
                // last search string
                start = startindex + endindex;
            }else
            {
                //Reached at the end of search, no more matches will be found further
                //we will have to restart search to continue now
                start = 0;
                this.indexOfSearchText = 0;
                MessageBox.Show("End of Search, restarting search");
            }
        }
        public int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        {
            // Unselect the previously searched string
            if (searchStart > 0 && searchEnd > 0 && indexOfSearchText >= 0)
            {
                rtb.Undo();
            }

            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index 
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = rtb.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                }
            }
            return retVal;
        }

        private void searchTextBox_Click(object sender, EventArgs e)
        {
            //FindText(sender,e);
        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(((int)e.KeyChar).ToString());
            if (e.KeyChar == (char)13)
            {
                // Then Enter key was pressed
                FindText(sender, e);
            }
            if (e.KeyChar == (char)27)
            {
                // Then escape key was pressed
                this.Hide();
            }
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(((int)e.KeyChar).ToString());
            //quit form)
            if (e.KeyChar == (char)27)
            {
                // Then escape key was pressed
                this.Hide();
            }
        }
    }
}
