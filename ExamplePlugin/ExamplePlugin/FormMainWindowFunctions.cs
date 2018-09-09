using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using ExamplePlugin.Bots;
using WikiFunctions;

namespace ExamplePlugin
{
    public partial class FormMainWindow
    {
        private StringReader PrintReader;


        
        delegate void SetPercent(int percent); //setpercent is a delegate which will be used to communicate between the postprocessor thread to gett the percentage .. how much the work has been done.. 
        delegate void SetTextCallback(string text); //set textcallback is used to get the translated text 

        private bool EditingOnlineArticle = false;
        private string OnlineArticleName = "";

        private readonly WikiDiff Diff = new WikiDiff();

        public bool SetEditingOnLineArticle
        {
            set { EditingOnlineArticle = value; }
        }
        public string SetOnlineArticleName
        {
            get { return OnlineArticleName; }
            set { OnlineArticleName = value; }
        }

        void SetPercentMethod(int percent)  //this is the method which updates the progressbar after it gets the value from the delegate setpercent
        {
             
            toolStripProgressBar1.ProgressBar.Value = percent;
        }

        /// <summary>
        /// Opens a content of a file and puts in the hindi tab ready for translating/editing 
        /// </summary>
        /// <param name="inputFileName"></param>
        public void OpenInputFile(string inputFileName)
        {
            
            string tempString;
            string inputText = "";
            StreamReader sr = new StreamReader(inputFileName);
            while ((tempString = sr.ReadLine()) != null)
            {
                inputText += (tempString + "\n");
            }
            sr.Close();
            this.richTextBoxHindi.Text = inputText;

        }

        /// <summary>
        /// This method is used for finding text inside the main window
        /// </summary>
        private void FindInsideFormMainWindow()
        {
            if (searchBox == null || searchBox.IsDisposed)
            {
                searchBox = new SearchBox();

            }

            if (tabPageHindi.ContainsFocus)
                searchBox.rtb = this.richTextBoxHindi;
            else
                searchBox.rtb = this.richTextBoxNepali;


            //this.Hide();
            searchBox.Show();
        }

        /// <summary>
        /// Perform translation if everything is okay
        /// Preconditions: 
        /// 1. the rules must be loaded
        /// 2. Noun file, pronoun file, verbs file, adjectives file, and the rules file should be present in the application directory
        /// 3. Postcondition: the above files in (2) should be editable by the application (I mean they should not be read only)
        /// </summary>
        public void PerformTranslation()
        {

            //resetting the progressbar to zero
            toolStripProgressBar1.ProgressBar.Value = toolStripProgressBar1.ProgressBar.Minimum;

            //Loading rules before starting translating
            pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName, PronounListFileName, UsersCustomFileName);
            
            //Getting the source text
            pr.LoadTextToTranslate(richTextBoxHindi.Text);

            
            
            pr.OnFeedback += new PostProcessor.ProgressFeedbackDelgate(C_OnFeedBack); //this event is required for updating the progressbar
            pr.OnTranslationComplete += new PostProcessor.TranslationCompleteDelegate(C_TranslationComplete); //this event is required for updating the result 

            //Lets define a thread before running a memory hungry process, because it takes time and usually hangs the UI, so putting in a thread
            translatorThread = new Thread(new ThreadStart(pr.TranslateVoid)); //inside the thread is the function that will be executed 
            translatorThread.Name = "TranslatorThread"; //Name of the thread
            translatorThread.Start(); //starting translation. 

//TODO: Natural language processing
//The following two lines are something I will work in the future, I was splitting the Document, paragraphs, sentences, and words, for a real natural language processing.
//Will work in this in future when I have more idea and more time.. :) 
            //Document document = new Document(richTextBoxHindi.Text);
            
            //document.SplitIntoAllParagraphs(richTextBoxHindi.Text);
            //this.richTextBoxNepali.Text =  document.ReBuildParagraphs();

//End TODO: Natural language processing
      

            //Changing to the respective tab
            this.tabControl1.SelectedIndex = 0;
            this.tabPageNepali.Focus();

        }


       

     


        /// <summary>
        /// Running the process in a thread so that it would not mess with the GUI 
        /// </summary>
        public void ThreadedTranslation()
        {
            //TranslatedText = pr.Translate();
            PerformTranslation();


        }

        /// <summary>
        /// Dragging file event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragTextFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Code to read the contents of the text file
                    if (File.Exists(fileLoc))
                    {
                        using (TextReader tr = new StreamReader(fileLoc))
                        {
                            richTextBoxHindi.Text = "";
                            //MessageBox.Show(tr.ReadToEnd());
                            richTextBoxHindi.LoadFile(fileLoc, RichTextBoxStreamType.PlainText);
                        }
                    }

                }
            }


        }


        /// <summary>
        /// Cut function inside Rich text box
        /// </summary>
        private void CutInsideRTB()
        {
            if (tabPageHindi.ContainsFocus)
            {
                if (richTextBoxHindi.SelectionLength > 0)
                {
                    richTextBoxHindi.Cut();
                }
            }
            if (tabPageNepali.ContainsFocus)
            {
                if (richTextBoxNepali.SelectionLength > 0)
                {
                    richTextBoxNepali.Cut();
                }
            }
        }

        /// <summary>
        /// Copy function for rich text box
        /// </summary>
        private void CopyInsideRTB()
        {
            if (tabPageHindi.ContainsFocus)
            {
                if (richTextBoxHindi.SelectionLength > 0)
                {
                    richTextBoxHindi.Copy();
                }
            }
            if (tabPageNepali.ContainsFocus)
            {
                if (richTextBoxNepali.SelectionLength > 0)
                {
                    richTextBoxNepali.Copy();
                }
            }
            if (tabWebBrowser.ContainsFocus)
            {
                string text = webBrowser.Document.ActiveElement.InnerText;
                if(!string.IsNullOrEmpty(text))
                    Clipboard.SetText(text);
                //MessageBox.Show("Copied");
            }
        }

        /// <summary>
        /// Paste menu
        /// </summary>
        private void PasteInsideRTB()
        {
            if (tabPageHindi.ContainsFocus)
            {
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                {

                    richTextBoxHindi.Paste();

                }
            }
            if (tabPageNepali.ContainsFocus)
            {
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                {
                    richTextBoxNepali.Paste();
                }
            }
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        private void ExitApplication()
        {
            //Do some household works before exitting
            //Save temp data
            Application.Exit();
        }

        /// <summary>
        /// Prints the nepali tab
        /// </summary>
        private void PrintNepaliTab()
        {
            PrintDialog printDialog1 = new PrintDialog();

            PrintDocument DocumentToPrint = new PrintDocument();

            printDialog1.Document = DocumentToPrint;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {

                
                PrintReader = new StringReader(richTextBoxNepali.Text);
                DocumentToPrint.PrintPage += new PrintPageEventHandler(DocumentToPrint_PrintPage);
                DocumentToPrint.Print();

            }

           

        }

        /// <summary>
        /// Prints page (a generic function for printing)
        /// </summary>
        private void DocumentToPrint_PrintPage(object sender, PrintPageEventArgs e)
        {

            #region Code To Print (From http://www.c-sharpcorner.com/winforms/texteditor.asp)

            float LinesPerPage = 0;

            float YPosition = 0;

            int Count = 0;

            float LeftMargin = e.MarginBounds.Left;

            float TopMargin = e.MarginBounds.Top;



            string Line = null;

            Font PrintFont = this.richTextBoxNepali.Font;

            SolidBrush PrintBrush = new SolidBrush(Color.Black);



            LinesPerPage = e.MarginBounds.Height / PrintFont.GetHeight(e.Graphics);



            while (Count < LinesPerPage && ((Line = PrintReader.ReadLine()) != null))
            {

                YPosition = TopMargin + (Count * PrintFont.GetHeight(e.Graphics));

                e.Graphics.DrawString(Line, PrintFont, PrintBrush, LeftMargin, YPosition, new StringFormat());

                Count++;

            }



            if (Line != null)
            {

                e.HasMorePages = true;

            }

            else
            {

                e.HasMorePages = false;

            }



            PrintBrush.Dispose();

            #endregion

        }

        /// <summary>
        /// This method informs the progressbar to update, with the appropriate value to update
        /// </summary>
        /// <param name="percent"></param>
        private void C_OnFeedBack (int percent)
        {
            //this.progressBar1.Value = percent;
            
            //progressBar1.Invoke(new SetPercent(SetPercentMethod), new object[] { percent });
            Debug.Write(" "+percent +"%");

            //cross thread - so you don't get the cross theading exception
            
            if(toolStripProgressBar1.ProgressBar.InvokeRequired)
            {
                toolStripProgressBar1.ProgressBar.Invoke(new SetPercent(SetPercentMethod), new object[] {percent});
            }
           // toolStripStatusLabel1.Text = "Translating...                    ";

        }

        /// <summary>
        /// This method is called when the translation is complete
        /// It should have been as simple as : private void C_TranslationComplete(string message){this.richTextBoxNepali.Text = message;}
        /// But I kept on receiving error from the compiler and did the lengthy process, hope it will be thread safe now.. 
        /// </summary>
        /// <param name="message"></param>
        private void C_TranslationComplete(string message)
        {
            //Microsoft thing Grr..
            if(this.richTextBoxNepali.InvokeRequired)
            {
                //I don't know why but microsoft said: "How to: Make Thread-Safe Calls to Windows Forms Controls" => to use a delegate and use if/else to do this thing.. 
                //looks like some complicated thing .. but the thing we will be doing is inside else.. 
                SetTextCallback d = new SetTextCallback(C_TranslationComplete);
                this.Invoke(d, new object[] { message });

                //this.richTextBoxNepali.Text = message;
                
            } //End microsoft thing (warning) grrr..
            else
            {
                this.richTextBoxNepali.Text = message; //This is what I actually wanted to do
                GetDiff();
                toolStripStatusLabel1.Text = "Done                                 ";

            }
            

            
            
        }

        /// <summary>
        /// Loads wiki article from the internet
        /// </summary>
        /// <param name="articleName"></param>
        public void LoadWikiArticle(string articleName)
        {
            if (loggedinUser == null)
            {
                loggedinUser = new ClientLogin();
                loggedinUser.ShowDialog();
            }
            if (loggedinUser.IsAuthenticated)
            {
                this.richTextBoxHindi.Text = loggedinUser.LogOnBot.GetContent(articleName);
                this.SetOnlineArticleName = articleName;
                this.SetEditingOnLineArticle = true;
            }
            else
            {
                SetEditingOnLineArticle = false;
                loggedinUser.ShowDialog();
            }


        }

        private void GetDiff()
        {
             

            try
            {
                webBrowser.BringToFront();
                if (webBrowser.Document == null)
                    return;

                webBrowser.Document.OpenNew(false);
                if (richTextBoxNepali.Text == richTextBoxHindi.Text)
                {
                    webBrowser.Document.Write(
                        @"<h2 style='padding-top: .5em;
padding-bottom: .17em;
border-bottom: 1px solid #aaa;
font-size: 150%;'>No changes</h2><p>Press the ""Source"" button to skip to write in Hindi and click on translate to Nepali to translate.</p>");
                }
                else
                {
                    // when less than 10 edits show user help info on double click to undo etc.
                    webBrowser.Document.Write("<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">"
                                              + "<html><head>" +
                                              WikiDiff.DiffHead() + @"</head><body>" 
                                              + WikiDiff.TableHeader+
                                              Diff.GetDiff(richTextBoxHindi.Text, richTextBoxNepali.Text, 2) +
                                              "</table>" + /*"<!--<script language='Javascript'>
// Scroll part of the way into the table, disabled due to other interface problems
diffNode=document.getElementById('wikiDiff');
var diffTopY = 0;
while(diffNode) {
    diffTopY += diffNode.offsetTop;
    diffNode = diffNode.offsetParent;
}
window.scrollTo(0, diffTopY);
</script>-->"*/"</body></html>");
                }

                 
                 

                 
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex);
            }
        }

    }
}
