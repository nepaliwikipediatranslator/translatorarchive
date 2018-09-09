using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.Globalization;
using ExamplePlugin.Bots;


namespace ExamplePlugin
{
    public partial class FormMainWindow : Form
    {
        public const string RuleFileName = "HindiToNepali.txt";
        //public const string KakharaFileName = "Kakhara.txt";
        public const string VerbListFileName = "verblist.txt";
        public const string NounListFileName = "nounlist.txt";
        public const string AdjectiveListFileName = "adjectivelist.txt";
        public const string PronounListFileName = "pronounlist.txt";
        public const string UsersCustomFileName = "Rules.db";
        private ClientLogin loggedinUser = null;

        private PostProcessor pr;
        private RuleEditor ruleEditor;
        private RuleEditor verbEditor;
        private RuleEditor nounEditor;
        private RuleEditor adjectiveEditor;
        private RuleEditor pronounEditor;
        private RuleEditor UsersCustomEditor;

        private SearchBox searchBox;

         
        private Thread translatorThread;
         
        
        
        private string lockPBVar = "myPBLock";



//        public StringBuilder Feedback = new StringBuilder();

        
        /// <summary>
        /// Default constructor: initiating everything here
        /// </summary>
        public FormMainWindow()
        {
            
            InitializeComponent();

           

            //Setting the second tab as the default tab( i.e. hindi tab as default)
            this.tabControl1.SelectedIndex = 1;

            ruleEditor = new RuleEditor();
            ruleEditor.SetFileName(RuleFileName);

            verbEditor = new RuleEditor();
            verbEditor.SetFileName(VerbListFileName);

            nounEditor = new RuleEditor();
            nounEditor.SetFileName(NounListFileName);

            adjectiveEditor = new RuleEditor();
            adjectiveEditor.SetFileName(AdjectiveListFileName);

            pronounEditor = new RuleEditor();
            pronounEditor.SetFileName(PronounListFileName);

            UsersCustomEditor = new RuleEditor();
            UsersCustomEditor.SetFileName(UsersCustomFileName);

            //this is for dragging and dropping text files
            this.richTextBoxHindi.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragTextFile);
            
            //This is for setting cursor
            this.richTextBoxHindi.DragEnter += new DragEventHandler(this.TextBox_DragEnter);

            this.webBrowser.Navigate("about:blank"); 
            
        }


       
        /// <summary>
        /// Exits the application on clicking the exit menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

 

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            openInputFileDialog.ShowDialog();
            string inputFileName = openInputFileDialog.FileName;

            OpenInputFile(inputFileName);
        }

        private void editRuleFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsersCustomEditor == null || UsersCustomEditor.IsDisposed)
                UsersCustomEditor = new RuleEditor();

                
                UsersCustomEditor.Show();
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openInputFileDialog.ShowDialog();
            string inputFileName = openInputFileDialog.FileName;

            OpenInputFile(inputFileName);
        }

        /// <summary>
        /// Clicking on the edit rules menu triggers this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pr==null)
            {
                pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName,
                                       PronounListFileName, UsersCustomFileName);
            }
                if (ruleEditor == null || ruleEditor.IsDisposed)
                    ruleEditor = new RuleEditor();
                ruleEditor.SetFileName(RuleFileName);
                //this.Hide();
                ruleEditor.Show();
            
        }

        /// <summary>
        /// Translating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void translateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            
            this.PerformTranslation();


//            pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName,AdjectiveListFileName,PronounListFileName);
//            pr.LoadTextToTranslate(richTextBoxHindi.Text);
//            this.richTextBoxNepali.Text = pr.Translate();
//            this.tabPageNepali.Focus();
        }

        /// <summary>
        /// Search For text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findNepaliTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindInsideFormMainWindow();
        }



        /// <summary>
        /// Initiate translation process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void translateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.PerformTranslation();
        }

        /// <summary>
        /// Editing Verbs from the editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editVerbListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pr==null)
            {
                pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName,
                                       PronounListFileName, UsersCustomFileName);
            }
                if (verbEditor == null || verbEditor.IsDisposed)
                    verbEditor = new RuleEditor();
                verbEditor.SetFileName(VerbListFileName);
                verbEditor.Show();
            
        }

        /// <summary>
        /// Editing Nouns from the Editor
        /// </summary>
        private void editNounListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pr==null)
            {
                pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName,
                                       PronounListFileName, UsersCustomFileName);
            }
                if (nounEditor == null || nounEditor.IsDisposed)
                    nounEditor = new RuleEditor();
                nounEditor.SetFileName(NounListFileName);
                nounEditor.Show();
            
        }

        /// <summary>
        /// Editing Adjectives from the Editor
        /// </summary>
        private void editAdjectiveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pr==null)
            {
                pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName,
                                       PronounListFileName, UsersCustomFileName);
            }
            if(adjectiveEditor ==null||adjectiveEditor.IsDisposed)
                    adjectiveEditor = new RuleEditor();
                adjectiveEditor.SetFileName(AdjectiveListFileName);

                adjectiveEditor.Show();
            
        }

        /// <summary>
        /// Editing Pronouns from the Editor
        /// </summary>
        private void editPronounListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pr==null)
            {
                pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName,
                                       PronounListFileName, UsersCustomFileName);
            }
                if(pronounEditor ==null||pronounEditor.IsDisposed)
                    pronounEditor = new RuleEditor();
                pronounEditor.SetFileName(PronounListFileName);

                pronounEditor.Show();
            
        }

        /// <summary>
        /// Clicking the translate button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTranslate_Click(object sender, EventArgs e)
        {
//            ThreadStart tsT = new ThreadStart(ThreadedTranslation);
//            Thread thread1 = new Thread(tsT);
//            thread1.Start();


            this.PerformTranslation();
        }

        /// <summary>
        /// This is done for enabling and disabling the translate button while changing the tabs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.tabControl1.SelectedIndex==1)
            {
                this.btnTranslate.Visible = true;
            }
            else
            {
                this.btnTranslate.Visible = false;
            }
        }

      

        /// <summary>
        /// Drag enter function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Drag and drop 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Pressing the F3 and searching again inside rich text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchBox == null || searchBox.IsDisposed)
            {
                searchBox = new SearchBox();
                
                searchBox.Show();
            }

            if (tabPageHindi.ContainsFocus)
                searchBox.rtb = this.richTextBoxHindi;
            else
                searchBox.rtb = this.richTextBoxNepali;
                

            searchBox.FindText(sender,e);
            //this.Hide();
            //searchBox.Show();
        }

        /// <summary>
        /// Clicking on the cut menu
        /// </summary>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

           CutInsideRTB();
        }



        /// <summary>
        /// Clicking on the copy menu
        /// </summary>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyInsideRTB();
        }

        /// <summary>
        /// Clicking on the paste menu
        /// </summary>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteInsideRTB();
        }

        /// <summary>
        /// Clicking on the cut menu
        /// </summary>
        private void toolStripMenuitemCut_Click(object sender, EventArgs e)
        {
            CutInsideRTB();

        }

        /// <summary>
        /// Clicking on the copy menu
        /// </summary>
        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            CopyInsideRTB();
        }

        /// <summary>
        /// Paste inside Rich text box
        /// </summary>
        private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            PasteInsideRTB();
        }

        /// <summary>
        /// Cut inside Rich text box
        /// </summary>
       private void toolStripMenuItemCutNepali_Click(object sender, EventArgs e)
        {
            CutInsideRTB();
        }

        /// <summary>
        /// Copying inside Rich text box
        /// </summary>
        private void toolStripMenuItemCopyNepali_Click(object sender, EventArgs e)
        {
            CopyInsideRTB();

        }

        /// <summary>
        /// Pasting from the context menu
        /// </summary>
        private void toolStripMenuItemPasteNepali_Click(object sender, EventArgs e)
        {
            PasteInsideRTB();
        }

        /// <summary>
        /// Clicking on the find in the menu
        /// </summary>
        private void findNepaliTextToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FindInsideFormMainWindow();
            
        }


        /// <summary>
        /// Clicking on the context menu for translation
        /// </summary>
        /// <param name="sender">
        private void toolStripMenuItemTranslateToNepali_Click(object sender, EventArgs e)
        {
            
            this.PerformTranslation();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PrintNepaliTab();
        }

        private void richTextBoxNepali_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void FormMainWindow_Load(object sender, EventArgs e)
        {
            //Progress bar settings
            toolStripProgressBar1.ProgressBar.Step = 1;
            toolStripProgressBar1.ProgressBar.Minimum = 0;
            toolStripProgressBar1.ProgressBar.Maximum = 100;
            
             
            //

 

        }

        private void browserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            WBrowser browser = new WBrowser(this);
            browser.ShowDialog();
            
        }

        private void loadWikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            LoadWikiArticle("Wikipedia:SandBox");
            

        }

      

        private void speedyDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string > articlesListConfirmedForDeletion = new List<string>();
            if (loggedinUser == null)
            {
                loggedinUser = new ClientLogin();
                loggedinUser.ShowDialog();
            }
             
             
            
            List<string> articlesToDelete = loggedinUser.LogOnBot.GetArticleForSpeedyDeletion("Delete");
            foreach (string articleMarkedForDeletion in articlesToDelete)
            {
               var DeleteOrNot =  MessageBox.Show("Delete" + articleMarkedForDeletion, "Delete confirm", MessageBoxButtons.OKCancel);
                if(DeleteOrNot == DialogResult.OK)
                {
                  articlesListConfirmedForDeletion.Add(articleMarkedForDeletion);   
                }
            }
            loggedinUser.LogOnBot.DeleteConfirmed(articlesListConfirmedForDeletion);


        }

        private void welcomeUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loggedinUser == null)
            {
                loggedinUser = new ClientLogin();
                loggedinUser.ShowDialog();
            }
             
            List<string> newUsersToWelcome = new List<string>();
            if(loggedinUser.IsAuthenticated)
                newUsersToWelcome = loggedinUser.LogOnBot.WelcomeNewUsers();

            var stringBuilder = new StringBuilder();
            foreach (string User in newUsersToWelcome)
            {
                stringBuilder.AppendLine("Welcome : " + User);
            }
            richTextBoxNepali.Text = stringBuilder.ToString();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggedinUser = new ClientLogin();
            loggedinUser.ShowDialog();

        }

        private void publishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                PublishArticle(this.OnlineArticleName,richTextBoxNepali.Text);
            }
        }

        private void publishToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                PublishArticle(this.OnlineArticleName, richTextBoxNepali.Text);
            }
        }

        public void PublishArticle(string articleName, string content)
        {
         if(EditingOnlineArticle)
         {
             this.loggedinUser.LogOnBot.SaveArticle(articleName,content);
         }
     
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrlEditor editor = new UrlEditor(this);
            editor.ShowDialog();
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            foreach (var list in LoadCorrectionList(CorrFileName))
            {
                BotScript.FindAndReplace(list.Key, list.Value, "ne.wikipedia.org", "myusername", "mypassword");    
            }
             */
            //BotScript.FindAndReplace("वाइरस", "भाइरस", "ne.wikipedia.org", "myUsername", "mypassword");    
            
            BotWatcher botWatcher = new BotWatcher();
            botWatcher.ShowDialog();

        }

        private string CorrFileName = "correction.txt";
        private Dictionary<string, string> LoadCorrectionList(string correctionListFileName)
        {
         Dictionary<string,string>   correctionList = new Dictionary<string, string>();
            //Create one if it doesnot exist
         if (!File.Exists(correctionListFileName))
            {
                var fileStream = File.Create(correctionListFileName);
                fileStream.Close();
            }

            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(correctionListFileName);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace("  ", "").Trim();

                inputText += (tempString + "\n");
                //split the text with |
                string[] splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Length == 2)
                {
                    if (!correctionList.ContainsKey(splittedText[0]))
                        correctionList.Add(splittedText[0], splittedText[1]);
                }

            }



            sr.Close();
            return correctionList;
        }

        private void diffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.GetDiff();
            this.tabControl1.SelectedIndex = 2;
        }

        private void FormMainWindow_Resize(object sender, EventArgs e)
        {
            this.tabControl1.Width = this.Width - 10;
        }

        
        
    }
}