using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using NepaliWikiPediaTranslator.AWBot;
using NepaliWikiPediaTranslator.Bots;
using WikiFunctions;
using WikiFunctions.API;


namespace NepaliWikiPediaTranslator
{
    public partial class FormMainWindow : Form
    {

       

        //Wikifunctions related
        public LoginForm loggedinUserAWB = null;

        //wikifunctions related

        private PostProcessor pr;
        private RuleEditor ruleEditor;
        private RuleEditor verbEditor;
        private RuleEditor nounEditor;
        private RuleEditor ncblEditor;
        private RuleEditor adjectiveEditor;
        private RuleEditor pronounEditor;
        private RuleEditor UsersCustomEditor;

        private SearchBox searchBox;
        private TemplateCopier templateCopier;
         
        private Thread translatorThread;
        private Thread templateCopyThread;
        private Thread templateTranslatorThread;
         
        
        
        private string lockPBVar = "myPBLock";


        public delegate void InvokeWebBrowser(string content);
        public event InvokeWebBrowser InvokeMyWebBrowserEvent;
        public SessionsManager SessionsManager = new SessionsManager();
        
        



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
            ruleEditor.SetFileName(SessionsManager. RuleFileName);

            verbEditor = new RuleEditor();
            verbEditor.SetFileName(SessionsManager.VerbListFileName);

            nounEditor = new RuleEditor();
            nounEditor.SetFileName(SessionsManager.NounListFileName);

            ncblEditor = new RuleEditor();
            ncblEditor.SetFileName(SessionsManager.NounsCommonInBothLanguageListFileName);

            adjectiveEditor = new RuleEditor();
            adjectiveEditor.SetFileName(SessionsManager.AdjectiveListFileName);

            pronounEditor = new RuleEditor();
            pronounEditor.SetFileName(SessionsManager.PronounListFileName);

            UsersCustomEditor = new RuleEditor();
            UsersCustomEditor.SetFileName(SessionsManager.UsersCustomFileName);

            //this is for dragging and dropping text files
            this.richTextBoxHindi.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragTextFile);
            
            //This is for setting cursor
            this.richTextBoxHindi.DragEnter += new DragEventHandler(this.TextBox_DragEnter);

            this.webBrowser.Navigate("about:blank");

            this.helpToolStripMenuItem.Enabled = File.Exists(SessionsManager.HELPFILENAME);

            this.InvokeMyWebBrowserEvent += new InvokeWebBrowser(C_UpdateWebBrowserFromThreads);

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
             bool loadSuggestionEnabled = false;
            if(richTextBoxNepali.SelectedText.Length>0)
            {
                loadSuggestionEnabled = LoadSuggestions(richTextBoxNepali.SelectedText, SessionsManager.RuleFileName);
            }

            if(pr==null)
            {
                pr = new PostProcessor(SessionsManager.RuleFileName, SessionsManager.VerbListFileName, SessionsManager.NounListFileName, SessionsManager.AdjectiveListFileName,
                                       SessionsManager.PronounListFileName, SessionsManager.UsersCustomFileName);
            }
                if (ruleEditor == null || ruleEditor.IsDisposed)
                    ruleEditor = new RuleEditor();
                ruleEditor.SetFileName(SessionsManager.RuleFileName);
                //this.Hide();
              if (loadSuggestionEnabled) ruleEditor.LoadSuggestion(richTextBoxNepali.SelectedText);
                ruleEditor.Show();
            
        }

        /// <summary>
        /// Translating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void translateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            
            this.PerformTranslationWithGuiUpdates();


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
            this.PerformTranslationWithGuiUpdates();
        }

        /// <summary>
        /// Editing Verbs from the editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editVerbListToolStripMenuItem_Click(object sender, EventArgs e)
        {
             bool loadSuggestionEnabled = false;
            if(richTextBoxNepali.SelectedText.Length>0)
            {
                loadSuggestionEnabled = LoadSuggestions(richTextBoxNepali.SelectedText, SessionsManager.VerbListFileName);
            }

            if(pr==null)
            {
                pr = new PostProcessor(SessionsManager.RuleFileName, SessionsManager.VerbListFileName, SessionsManager.NounListFileName, SessionsManager.AdjectiveListFileName,
                                       SessionsManager.PronounListFileName, SessionsManager.UsersCustomFileName);
            }
                if (verbEditor == null || verbEditor.IsDisposed)
                    verbEditor = new RuleEditor();
                verbEditor.SetFileName(SessionsManager.VerbListFileName);
              if (loadSuggestionEnabled) verbEditor.LoadSuggestion(richTextBoxNepali.SelectedText);
                verbEditor.Show();
            
        }

        /// <summary>
        /// Editing Nouns from the Editor
        /// </summary>
        private void editNounListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool loadSuggestionEnabled = false;
            if(richTextBoxNepali.SelectedText.Length>0)
            {
                loadSuggestionEnabled = LoadSuggestions(richTextBoxNepali.SelectedText, SessionsManager.NounListFileName);
            }

            if(pr==null)
            {
                pr = new PostProcessor(SessionsManager.RuleFileName, SessionsManager.VerbListFileName, SessionsManager.NounListFileName, SessionsManager.AdjectiveListFileName,
                                       SessionsManager.PronounListFileName, SessionsManager.UsersCustomFileName);
            }
            
                if (nounEditor == null || nounEditor.IsDisposed)
                    nounEditor = new RuleEditor();
                nounEditor.SetFileName(SessionsManager.NounListFileName);

                if (loadSuggestionEnabled) nounEditor.LoadSuggestion(richTextBoxNepali.SelectedText);
            
            nounEditor.Show();
            
        }

        private bool LoadSuggestions(string suggestionText,string partOfSpeechType)
        {
            //partOfSpeechType = Noun Transitive, Noun Intransitive, Verb Causative, Verb Incausative, Adjective, Pronoun, simpleReplacement
          if(MessageBox.Show(string.Format("Do you want to add {0} in {1}", suggestionText, partOfSpeechType), "Add Rule",MessageBoxButtons.OKCancel) == DialogResult.OK)
          {
              return true;
          }
            return false;
        }

        /// <summary>
        /// Editing Adjectives from the Editor
        /// </summary>
        private void editAdjectiveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool loadSuggestionEnabled = false;
            if(richTextBoxNepali.SelectedText.Length>0)
            {
                loadSuggestionEnabled = LoadSuggestions(richTextBoxNepali.SelectedText, SessionsManager.AdjectiveListFileName);
            }

            if(pr==null)
            {
                pr = new PostProcessor(SessionsManager.RuleFileName, SessionsManager.VerbListFileName, SessionsManager.NounListFileName, SessionsManager.AdjectiveListFileName,
                                       SessionsManager.PronounListFileName, SessionsManager.UsersCustomFileName);
            }
            if(adjectiveEditor ==null||adjectiveEditor.IsDisposed)
                    adjectiveEditor = new RuleEditor();
            adjectiveEditor.SetFileName(SessionsManager.AdjectiveListFileName);
              if (loadSuggestionEnabled) adjectiveEditor.LoadSuggestion(richTextBoxNepali.SelectedText);
                adjectiveEditor.Show();
            
        }

        /// <summary>
        /// Editing Pronouns from the Editor
        /// </summary>
        private void editPronounListToolStripMenuItem_Click(object sender, EventArgs e)
        {
             bool loadSuggestionEnabled = false;
            if(richTextBoxNepali.SelectedText.Length>0)
            {
                loadSuggestionEnabled = LoadSuggestions(richTextBoxNepali.SelectedText, SessionsManager.PronounListFileName);
            }

            if(pr==null)
            {
                pr = new PostProcessor(SessionsManager.RuleFileName, SessionsManager.VerbListFileName, SessionsManager.NounListFileName, SessionsManager.AdjectiveListFileName,
                                       SessionsManager.PronounListFileName, SessionsManager.UsersCustomFileName);
            }
                if(pronounEditor ==null||pronounEditor.IsDisposed)
                    pronounEditor = new RuleEditor();
                pronounEditor.SetFileName(SessionsManager.PronounListFileName);

                if (loadSuggestionEnabled) pronounEditor.LoadSuggestion(richTextBoxNepali.SelectedText);
            
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


            this.PerformTranslationWithGuiUpdates();
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
        private void findAgain(object sender, EventArgs e)
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

       private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
       {
           SelectAllInsideRTB();
       }

       private void selectAllMenuItem_RichTextBoxNepali_Click(object sender, EventArgs e)
       {
           SelectAllInsideRTB();
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
            
            this.PerformTranslationWithGuiUpdates();
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

       

        private void loadWikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            LoadWikiArticle("Wikipedia:SandBox");
            

        }


       

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loggedinUserAWB == null)
            {
                loggedinUserAWB = new LoginForm(SessionsManager);
                loggedinUserAWB.ShowDialog();
            }
            UrlEdit awbUrlEditor = new UrlEdit(this);
            awbUrlEditor.ShowDialog();
            
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveText();

        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.SaveText();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchHelpFile();
        }

        private void translateFromEnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformEnglishTranslation();
        }

        private void enableCopyPasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableClipBoardCopyPaste = !EnableClipBoardCopyPaste;
            enableCopyPasteToolStripMenuItem.Checked = EnableClipBoardCopyPaste;
        }

        private void aWBLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {

            loggedinUserAWB = new LoginForm(SessionsManager);
            loggedinUserAWB.ShowDialog();


        }

        private void findAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findAgain(sender,e);
        }

        private void loadBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBot();
        }

        void LoadBot()
        {
            this.templateCopier = new TemplateCopier(this);
            templateCopier.OnArticleCacheReady+= new TemplateCopier.ArticleCacheProviderDelegate(C_OnArticleCacheReady);
            templateCopier.Show();
        }



        public void C_UpdateWebBrowserFromThreads(string message)
        {
            webBrowser.Navigate("about:blank");
            webBrowser.Document.OpenNew(true);
            webBrowser.Document.Write(message);
            
        }

        public void C_OnArticleCacheReady(ArticleCache articleCache)
        {


            string text =
            ("<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">"
                                              + "<html><head>" +
                                              WikiDiff.DiffHead() + @"</head><body>"
                                              + WikiDiff.TableHeader +
                                              Diff.GetDiff(articleCache.Article_WikiContent, articleCache.TranslatedArticle_Content, 2) +
                                              "</table></body></html>");
            


            
            try
            {

                if(InvokeMyWebBrowserEvent!=null)
                {
                    this.Invoke(InvokeMyWebBrowserEvent,text);
                }
                

            }
            catch (Exception ex)
            {

                ErrorHandler.Handle(ex);
            }

            


            //GetDifferences(articleCache.Article_WikiContent, articleCache.TranslatedArticle_Content);


        }

        private void saveArticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAndMoveNext = true;
            
        }

        private void btnSaveArticle_Click(object sender, EventArgs e)
        {
            this.SaveAndMoveNext = true;
            
        }

        private void btnSkipSaving_Click(object sender, EventArgs e)
        {
            this.SkipWithOutSaving = true;
            
        }

        private void richTextBoxHindi_TextChanged(object sender, EventArgs e)
        {
            this.RichTextBoxHindiText = this.richTextBoxHindi.Text;
        }


        private void FormMainWindow_Activated(object sender, EventArgs e)
        {
            if (EnableClipBoardCopyPaste)
            {
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                {
                    string clipBoardText = Clipboard.GetText();
                    if (clipBoardText.Equals(richTextBoxNepali.Text) | clipBoardText.Equals(richTextBoxHindi.Text))
                    {

                    }
                    else
                    {
                        this.richTextBoxHindi.Text = clipBoardText;
                        this.PerformTranslationWithGuiUpdates();
                    }
                }
            }

        }

        private void FormMainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (translatorThread != null)
            { if(translatorThread.IsAlive)
                translatorThread.Abort();
            }
            if(templateCopyThread!=null)
            {
                if(templateCopyThread.IsAlive)
                    templateCopyThread.Abort(); 
            }
            
            
            Application.Exit();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(SessionsManager);
            loginForm.Show();

        }

        private void editNCBLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool loadSuggestionEnabled = false;
            if (richTextBoxNepali.SelectedText.Length > 0)
            {
                loadSuggestionEnabled = LoadSuggestions(richTextBoxNepali.SelectedText, SessionsManager.NounsCommonInBothLanguageListFileName);
            }

            if (pr == null)
            {
                pr = new PostProcessor(SessionsManager.RuleFileName, SessionsManager.VerbListFileName, SessionsManager.NounListFileName, SessionsManager.AdjectiveListFileName,
                                       SessionsManager.PronounListFileName, SessionsManager.UsersCustomFileName,SessionsManager.NounsCommonInBothLanguageListFileName);
            }

            if (ncblEditor == null || ncblEditor.IsDisposed)
                nounEditor = new RuleEditor();
            ncblEditor.SetFileName(SessionsManager.NounsCommonInBothLanguageListFileName);

            if (loadSuggestionEnabled) ncblEditor.LoadSuggestion(richTextBoxNepali.SelectedText);

            ncblEditor.Show();
        }

        

        
       
    }
}