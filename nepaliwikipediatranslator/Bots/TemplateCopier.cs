using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NepaliWikiPediaTranslator.AWBot;
using WikiFunctions;
using WikiFunctions.API;
using WikiFunctions.Lists.Providers;

namespace NepaliWikiPediaTranslator.Bots
{
    public partial class TemplateCopier : Form
    {

        private FormMainWindow formMainWindow;  //direct reference to of the main window. bad coding style .. //Need to find the drawbacks
        private List<string> articleList = new List<string>(); //ArticleList to process
        private List<string> dataSourceForListItem = new List<string>();

        private delegate void SetProgressBarPercent(int percent); //setpercent is a delegate which will be used to communicate between the postprocessor thread to gett the percentage .. how much the work has been done.. 
//        public delegate void RobotLoggerDelegate(string message); //to log the message in the richtextbox
//
//        public event RobotLoggerDelegate OnRobotLogFeedback; 

        public delegate void ArticleCacheProviderDelegate(ArticleCache articleCache); //to tell that one of the article has been processed
        public event ArticleCacheProviderDelegate OnArticleCacheReady;

        
        
        BackGroundRobot bRobot = new BackGroundRobot();  //Initialize background robot

        RuleEditor ruleEditor = new RuleEditor();
        
        /// <summary>
        /// Gets usually invoked from FormMainwindow, hence we don't have any issues yet. 
        /// </summary>
        /// <param name="paramformMainWindow"></param>
        public TemplateCopier(FormMainWindow paramformMainWindow)
        {
            InitializeComponent();
            this.formMainWindow = paramformMainWindow;

            //brobot
            bRobot = new BackGroundRobot();
           //ResetBot();
            ruleEditor.SetFileName(SessionsManager.TemplateParserFileName);
            comboWikiSelector.DataSource = mediaWikiVars;
        }

        private void articleInsertButton_Click(object sender, EventArgs e)
        {
            AddArticle(articleInsertTextBox.Text);
        }
        void AddArticle(string articleName)
        {
            if(string.IsNullOrEmpty(articleName))
                return;
            articleList.Add(articleName);
            
            articlelistBox.DataSource = null;
            //articlelistBox.DataBindings.Clear();
            articlelistBox.DataSource = articleList;

            articleInsertTextBox.Text = "";
//            articlelistBox.Refresh();
            this.articlelistBox.Refresh();
        }

        private void articleInsertTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void articlelistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*
            if(formMainWindow.editor==null)
            {
                LoginForm loginForm = new LoginForm(formMainWindow.SessionsManager);
                loginForm.ShowDialog();
                articlelistBox_MouseDoubleClick(sender,e);
            }
            if(formMainWindow.editor.User==null)
            {
                LoginForm loginForm = new LoginForm(formMainWindow.SessionsManager);
                loginForm.ShowDialog();
                articlelistBox_MouseDoubleClick(sender, e);
            }
            if(!formMainWindow.editor.User.IsLoggedIn)
            {
                LoginForm loginForm = new LoginForm(formMainWindow.SessionsManager);
                loginForm.ShowDialog();
                articlelistBox_MouseDoubleClick(sender, e);
            }

            //MessageBox.Show(this.articlelistBox.SelectedItems[this.articlelistBox.SelectedIndex].ToString());
            var articleName = this.articlelistBox.SelectedItems[this.articlelistBox.SelectedIndex].ToString();
            
            if(string.IsNullOrEmpty(articleName)) return;

            if (formMainWindow.loggedinUserAWB == null)
            {
                formMainWindow.loggedinUserAWB = new LoginForm(formMainWindow.SessionsManager);
            }

            
                try
                {
                    //this.Hide();
                    // var articleContent = formMainWindow.editor.Open(articleName);
                    formMainWindow.LoadWikiArticle("Template:"+articleName);


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString(), ex.Source);
                    formMainWindow.loggedinUserAWB = null;
                }


            
            

            */
        }

        private void CopyTemplatesFromEnglishButton_Click(object sender, EventArgs e)
        {
            string[] articleArray = richTextBoxTemplateList.Text.Split(new string[] { "\n" },
                                                                       StringSplitOptions.RemoveEmptyEntries);
            articleList = articleArray.ToList();
            toolStripProgressBar1.ProgressBar.Value = toolStripProgressBar1.ProgressBar.Minimum;

            formMainWindow.OnRobotPercentageFeedback += new FormMainWindow.RobotProgressFeedbackDelgate(C_OnFeedBack);
            formMainWindow.OnRobotLogFeedBack+= new FormMainWindow.RobotLoggerDelegate(C_RobotLogFeedBack);


            ArticleListBox_DataBind();
            CopyTemplateFromEnglishWikipedia(articleList);
        }

        #region Copying Template from English WIkipedia

        private void LoginBeforeCopyingTemplate()
        {
            if (formMainWindow.editor == null)
            {
                LoginForm loginForm = new LoginForm(formMainWindow.SessionsManager);
                loginForm.ShowDialog();
                return;
            }
            if (formMainWindow.editor.User == null) {
                LoginForm loginForm = new LoginForm(formMainWindow.SessionsManager); 
                loginForm.ShowDialog();
                return;
                }
            if (!formMainWindow.editor.User.IsLoggedIn) {
                LoginForm loginForm = new LoginForm(formMainWindow.SessionsManager); 
                loginForm.ShowDialog();
                return;
            }
            if (string.IsNullOrEmpty(formMainWindow.editor.User.Name))
            {
                LoginForm loginForm = new LoginForm(formMainWindow.SessionsManager); 
                loginForm.ShowDialog();
                return;
            }
            
        }

        private void CopyTemplateFromEnglishWikipedia(List<string> articleList)
        {
            /*
             * LoginBeforeCopyingTemplate();
             * 
             */

            ResetBot();

            string _sourceWiki = "en";
            bRobot.DestinationWiki = comboWikiSelector.SelectedValue.ToString().Trim();


            bRobot.ListOfArticles = articleList;
            bRobot.SourceWiki = _sourceWiki;
            bRobot.EditSummary = this.editSummaryTextBox.Text;
            bRobot.OnRobotLogFeedBack += new BackGroundRobot.RobotLoggerDelegate(C_RobotLogFeedBack);
            bRobot.OnRobotPercentageFeedback+= new BackGroundRobot.RobotProgressFeedbackDelgate(C_OnFeedBack);
            //bRobot.OnArticleCacheReady += new BackGroundRobot.ArticleCacheProviderDelegate(C_OnArticleCacheReady);
            bRobot.OnArticleProcessed+= new BackGroundRobot.ArticleProcessedDelegate(C_OnArticleProcessed);
            bRobot.EnableTranslation = EnableTranslationCheckBox.Checked;

            ArticleListBox_DataBind();

            bRobot.CopyTemplates();


            /*
             * 
             * 
            formMainWindow.ListOfArticles = articleList;
            formMainWindow.SourceWiki = _sourceWiki;
            formMainWindow.EditSummary = this.editSummaryTextBox.Text;
            formMainWindow.CopyTemplates();
             * 
             */





        }
        private void CopyTemplateFromHindiWikipedia(List<string> articleList)
        {
            ResetBot();
            /**
            LoginBeforeCopyingTemplate();
            string _sourceWiki = "hi";
            formMainWindow.ListOfArticles = articleList;
            formMainWindow.EditSummary = this.editSummaryTextBox.Text;
            formMainWindow.CopyTemplates();
             */
            
            string _sourceWiki = "hi";
            bRobot.DestinationWiki = comboWikiSelector.SelectedValue.ToString().Trim();
            bRobot.ListOfArticles = articleList;
            bRobot.SourceWiki = _sourceWiki;
            bRobot.EditSummary = this.editSummaryTextBox.Text;

            
            bRobot.OnRobotLogFeedBack += new BackGroundRobot.RobotLoggerDelegate(C_RobotLogFeedBack);
            bRobot.OnRobotPercentageFeedback += new BackGroundRobot.RobotProgressFeedbackDelgate(C_OnFeedBack);
          
            //  bRobot.OnArticleCacheReady += new BackGroundRobot.ArticleCacheProviderDelegate(C_OnArticleCacheReady);
            bRobot.EnableTranslation = EnableTranslationCheckBox.Checked;

            ArticleListBox_DataBind();
            bRobot.CopyTemplates();

        }

        #endregion 

        /// <summary>
        /// On clicking this textbox, it will parse with the regular expression rules and remove text other than the articles
        /// Regular expressions has to be provided in a text file (initially the name was : "templateparser.txt"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wantedTemplatesbtn_Click(object sender, EventArgs e)
        {
            PrepareTemplateListByParsingTheTextBox();
        }


        #region Parse and Prepare TemplateList
        /// <summary>
        /// Note: two files will be used: "Templateparser.txt" 
        /// Templateparser.txt will contain the regular expressions on how to get text from recentchanges pages
        /// or from any other sources, 
        /// template_temp is a blank page, which might be required just in case, to feed the postprocessor before translating
        /// May be it should be just fine by providing the first parameter. 
        /// TODO: will work on this later to change the postprocessor
        /// </summary>
        private void PrepareTemplateListByParsingTheTextBox()
        {
            if (!string.IsNullOrEmpty(richTextBoxTemplateList.Text))
            {
                PostProcessor pr = new PostProcessor(SessionsManager.TemplateParserFileName,
                                                     SessionsManager.BlankFileName, SessionsManager.BlankFileName,
                                                     SessionsManager.BlankFileName, SessionsManager.BlankFileName,
                                                     SessionsManager.BlankFileName);
                pr.LoadTextToTranslate(richTextBoxTemplateList.Text);
                pr.OnTranslationComplete += new PostProcessor.TranslationCompleteDelegate(C_TranslateComplete);
                pr.TranslateVoid();
            }
            //Required : C_TranslationComplete method
        }

        /// <summary>
        /// This method is invoked at the end of the process by the delegate.
        /// </summary>
        /// <param name="translatedText"></param>
        private void C_TranslateComplete(string translatedText)
        {
            richTextBoxTemplateList.Text = translatedText;
        }

        #endregion

        private void CopyTemplatesFromHindiButton_Click(object sender, EventArgs e)
        {
            string[] articleArray = richTextBoxTemplateList.Text.Split(new string[] { "\n" },
                                                           StringSplitOptions.RemoveEmptyEntries);
            articleList = articleArray.ToList();
            toolStripProgressBar1.ProgressBar.Value = toolStripProgressBar1.ProgressBar.Minimum;
            formMainWindow.OnRobotPercentageFeedback += new FormMainWindow.RobotProgressFeedbackDelgate(C_OnFeedBack);
            formMainWindow.OnRobotLogFeedBack += new FormMainWindow.RobotLoggerDelegate(C_RobotLogFeedBack);


            ArticleListBox_DataBind();
            CopyTemplateFromHindiWikipedia(articleList);

        }

        private void copyTemplatesFromEnglishWikipediaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string[] articleArray = richTextBoxTemplateList.Text.Split(new string[] { "\n" },
                                                           StringSplitOptions.RemoveEmptyEntries);
            articleList = articleArray.ToList();
           
            /*
            //Need to Set articlelist
            toolStripProgressBar1.ProgressBar.Value = toolStripProgressBar1.ProgressBar.Minimum;
            formMainWindow.OnRobotPercentageFeedback+= new FormMainWindow.RobotProgressFeedbackDelgate(C_OnFeedBack);
            */
            ArticleListBox_DataBind();

            CopyTemplateFromEnglishWikipedia(articleList);

        }

        private void copyTemplatesFromHindiWikipediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] articleArray = richTextBoxTemplateList.Text.Split(new string[] { "\n" },
                                                           StringSplitOptions.RemoveEmptyEntries);
            articleList = articleArray.ToList();

            /*
            toolStripProgressBar1.ProgressBar.Value = toolStripProgressBar1.ProgressBar.Minimum;
            formMainWindow.OnRobotPercentageFeedback += new FormMainWindow.RobotProgressFeedbackDelgate(C_OnFeedBack);
            */

            ArticleListBox_DataBind();
            CopyTemplateFromHindiWikipedia(articleList);

        }
        /// <summary>
        /// This method informs the progressbar to update, with the appropriate value to update
        /// </summary>
        /// <param name="percent"></param>
        private void C_OnFeedBack(int percent)
        {
            //this.progressBar1.Value = percent;

            //progressBar1.Invoke(new SetPercent(SetPercentMethod), new object[] { percent });
            Debug.Write(" " + percent + "% ");

            //cross thread - so you don't get the cross theading exception

            if (toolStripProgressBar1.ProgressBar.InvokeRequired)
            {
                toolStripProgressBar1.ProgressBar.Invoke(new SetProgressBarPercent(SetPercentMethod), new object[] { percent });
            }

            if(statusStrip1.InvokeRequired)
            {
                if(percent <=10)
                {
                    toolStripStatusLabel1.Text = "Copying template...              ";    
                }
                
                if (percent == 100)
                {
                    toolStripStatusLabel1.Text = "Completed                       ";
                }
            }
            
            

        }

        /// <summary>
        /// Prints the log in a text box, with whatever log the thread transmits.
        /// </summary>
        /// <param name="message"></param>
        private void C_RobotLogFeedBack(string message)
        {
            Debug.Write("\r Log: "+message+"\r");
            if(richTextBoxLogViewer.InvokeRequired)
            {
                BackGroundRobot.RobotLoggerDelegate d = new BackGroundRobot.RobotLoggerDelegate(C_RobotLogFeedBack);
                this.Invoke(d, new object[] { message });
                
                //richTextBoxLogViewer.AppendText("\n"+message);
            }
            else
            {
                richTextBoxLogViewer.AppendText("\n" + message);
            }
        }

        /// <summary>
        /// Perform the following task when when the translation of the article(template) has been completed 
        /// </summary>
        /// <param name="articleCache"></param>
        private void C_OnArticleCacheReady(ArticleCache articleCache)
        {
            if(formMainWindow.InvokeRequired)
            {
                //formMainWindow.GetDiff(articleCache.TranslatedArticle_Content,articleCache.Article_WikiContent);
                //do nothing 
                //MessageBox.Show("Invoke required");
               
                formMainWindow.GetDifferences(articleCache.TranslatedArticle_Content, articleCache.Article_WikiContent);
            }else
                formMainWindow.GetDifferences(articleCache.TranslatedArticle_Content, articleCache.Article_WikiContent);
        }

        /// <summary>
        /// When an article has been processed and gets notified, may be we can remove an article from the list or from textbox,
        /// Tell that one item from the list has been processed.
        /// </summary>
        /// <param name="articleCache"></param>
        private void C_OnArticleProcessed(ArticleCache articleCache)
        {
            if (dataSourceForListItem != null)
            {



                if (dataSourceForListItem.Count > 0)
                    dataSourceForListItem.RemoveAt(0);
                
                if(articlelistBox.InvokeRequired)
                {
                    articlelistBox.DataSource = null;
                    articlelistBox.DataSource = dataSourceForListItem;
                }
            }

            if(articleCache!=null)
            {
                if (statusStrip1.InvokeRequired)
                {

                    toolStripStatusLabel1.Text = "Processed " + articleCache.ArticleName + "       ";
                    OnArticleCacheReady(articleCache);
                    
                }
            }

        }

        /// <summary>
        /// Gets a percentage : required for progressbar
        /// this is the method which updates the progressbar after it gets the value from the delegate setpercent
        /// </summary>
        /// <param name="percent"></param>
        void SetPercentMethod(int percent)  
        {

            toolStripProgressBar1.ProgressBar.Value = percent;
        }

        private void userContributionsButton_Click(object sender, EventArgs e)
        {
            WikiFunctions.API.ApiEdit api = new ApiEdit("http://ne.wikipedia.org/w/", false);
            api.Login("user","password");

            List<string> userContribs = new List<string>();
            UserContribUserDefinedNumberListProvider listProvider = new UserContribUserDefinedNumberListProvider();
            listProvider.Limit = 15000;

            List<Article> allList;
            allList = listProvider.ApiMakeList(
                "http://ne.wikipedia.org/w/index.php?limit=500&tagFilter=&title=Special%3AContributions&contribs=user&target=Nirmal+Dulal&namespace=10&year=&month=-1",
                0);
            userContribs = allList.Select(p => p.Name+"/doc").ToList();
            StringBuilder sb = new StringBuilder();
            

            //adds doc
            foreach (Article article in allList)
            {
                
                sb.AppendLine(article.Name + "/doc");
            }
            richTextBoxTemplateList.Text = sb.ToString();



            //this.richTextBoxTemplateList.AppendText(userContribs);
        }

        private void TemplateCopier_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Aborting all threads before quitting
            this.bRobot.AbortThreads();
        }

        private void EnableTranslationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Enabling automatic translation of templates
            bRobot.EnableTranslation = EnableTranslationCheckBox.Checked;
        }

        private void ArticleListBox_DataBind()
        {
            if(this.articleList!=null)
            {
                if(this.articleList.Count>0)
                {
                    dataSourceForListItem = articleList.ToList();
                    articlelistBox.DataSource = dataSourceForListItem;
                }
            }
            //if (this.articleList == null) MessageBox.Show("it is null");
            //if (this.articleList.Count <= 0) MessageBox.Show("count is zero");
        }

        private void ResetBot()
        {
            bRobot = new BackGroundRobot();
            bRobot.SetThreadSleepTime = int.Parse(comboBoxThreadSleepTime.SelectedValue + "000");
            //if the user has ever logged in get the username and password 
            if(formMainWindow.SessionsManager.LoginData.HasLoggedInOnce)
            {
                //username and passwords are saved in that file
                bRobot.SetFormMainWindowUserNameAndPassword(
                    formMainWindow.SessionsManager.LoginData.UserName,
                    formMainWindow.SessionsManager.LoginData.Password
                    );

            }
            else
            {
                LoginForm loginForm = new LoginForm(formMainWindow.SessionsManager);
                loginForm.ShowDialog();
                ResetBot();
            }
        }

        private void parseArtileListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrepareTemplateListByParsingTheTextBox();
        }

        private void editListParserRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ruleEditor == null || ruleEditor.IsDisposed)
                ruleEditor = new RuleEditor();
            ruleEditor.SetFileName(SessionsManager.TemplateParserFileName);
            ruleEditor.Show();


        }
        private string[] mediaWikiVars = new string[] {
				"ne","hi","new", "bh"
			};

        private void comboBoxThreadSleepTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            bRobot.SetThreadSleepTime = int.Parse(comboBoxThreadSleepTime.SelectedValue+"000");
        }
    }
}
