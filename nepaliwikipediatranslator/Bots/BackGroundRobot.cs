using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using WikiFunctions.API;

namespace NepaliWikiPediaTranslator.Bots
{
   public class BackGroundRobot
    {

       public BackGroundRobot(){}

        private string FormMainWindowUserName = "";
        private string FormMainWindowPassword = "";
        private bool SkipWithOutSaving = false;
        private bool SaveAndMoveNext = false;
        private bool BotMode = true;
        private string ArticleName;
        private ArticleCache articleContentCache = new ArticleCache();
        //private bool _IsEditorBusy = false;
        public string EditSummary { get; set; }
        private string RichTextBoxHindiText { get; set; }
        public bool EnableTranslation { get; set; }
        private string m_destinationWiki = "ne";


        private WikiFunctions.API.ApiEdit bot = new ApiEdit("http://ne.wikipedia.org/w/", false);
        private WikiFunctions.API.ApiEdit editor = new ApiEdit("http://en.wikipedia.org/w/");

        private string m_sourceWiki = "en";

        private Thread templateCopyThread;
        private Thread templateTranslatorThread;

        private PostProcessor pr;


        delegate void SetHindiText(string message); // delegate to Change the content of the richtextbox 
        event SetHindiText OnHindiTextObtained; //event that is triggered when the Template Copying thread completes

        public delegate void RobotProgressFeedbackDelgate(int percent);
        public event RobotProgressFeedbackDelgate OnRobotPercentageFeedback;

        public delegate void RobotLoggerDelegate(string message);
        public event RobotLoggerDelegate OnRobotLogFeedBack;

       public delegate void ArticleCacheProviderDelegate(ArticleCache articleCache);
       public event ArticleCacheProviderDelegate OnArticleCacheReady;

       public delegate void ArticleProcessedDelegate(ArticleCache articleCache);
       public event ArticleProcessedDelegate OnArticleProcessed;

       private int m_ThreadSleepTime = 1000;

       public int SetThreadSleepTime{
           get { return m_ThreadSleepTime; }
           set { m_ThreadSleepTime = value; }
            }
        public List<string> ListOfArticles { get; set; }
       public string SourceWiki
       {
           get { return m_sourceWiki; }
           set
           {
               editor = new ApiEdit("http://"+value+".wikipedia.org/w/");
               m_sourceWiki = value;
           }

       }

       public string DestinationWiki
       {
           get { return m_destinationWiki; }
           set
           {
               bot = new ApiEdit("http://" + value + ".wikipedia.org/w/");
               m_destinationWiki = value;
           }
       }


        /// <summary>
        /// sets username/password from some other forms. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void SetFormMainWindowUserNameAndPassword(string username, string password)
        {
            this.FormMainWindowUserName = username;
            this.FormMainWindowPassword = password;
        }


        public void CopyTemplates()
        {

            templateCopyThread = new Thread(new ThreadStart(CopyTemplatesMethod));
            templateCopyThread.Name = "Template Copy thread";
            //this.OnHindiTextObtained += new SetHindiText(C_SetRichTextBoxHindi);
            templateCopyThread.Start();

        }

        public void TranslateTemplateThread()
        {
            
            articleContentCache.TranslatedArticle_Content = "";
            articleContentCache.IsTranslatedArticle = false;
            pr = new PostProcessor(SessionsManager.RuleFileName, SessionsManager.VerbListFileName, SessionsManager.NounListFileName, SessionsManager.AdjectiveListFileName, SessionsManager.PronounListFileName, SessionsManager.UsersCustomFileName);
            pr.LoadTextToTranslate(articleContentCache.Article_WikiContent);
            pr.OnTranslationComplete += new PostProcessor.TranslationCompleteDelegate(TranslationOfThreadCompleted);

            templateTranslatorThread = new Thread(new ThreadStart(pr.TranslateVoid)); //inside the thread is the function that will be executed 
            templateTranslatorThread.Name = "TemplateTranslatorThread"; //Name of the thread
            templateTranslatorThread.Start(); //starting translation. 



        }

        private void TranslationOfThreadCompleted(string message)
        {
           
            articleContentCache.TranslatedArticle_Content = message;
            articleContentCache.IsTranslatedArticle = true;
            //resume thread
            //englisharticle= message
            //resume thread;
        }

        public void CopyTemplatesMethod()
        {
            #region resetting things in articlecache
            articleContentCache.ArticleName = "";
            articleContentCache.Article_WikiContent = "";
            #endregion

            List<string> articleList = this.ListOfArticles;
            string sourceWiki = SourceWiki.Trim();
            if (editor == null)
            {
                editor = new ApiEdit("http://" + sourceWiki + ".wikipedia.org/w/", false);
                editor.Login(FormMainWindowUserName, FormMainWindowPassword);
            }

            if (editor.User == null)
                editor.Login(FormMainWindowUserName, FormMainWindowPassword);

            if (editor.User.Name == null)
                editor.Login(FormMainWindowUserName, FormMainWindowPassword);


            if (bot == null)
            {
                bot = new ApiEdit("http://"+m_destinationWiki+".wikipedia.org/w/", false);
                bot.Login(FormMainWindowUserName, FormMainWindowPassword);
            }
            if (bot.User == null)
            {
                bot.Login(FormMainWindowUserName, FormMainWindowPassword);
            }
            if (bot.User.Name == null)
            {
                bot.Login(FormMainWindowUserName, FormMainWindowPassword);
            }




            int _counter = 0;
            int maxValue = articleList.Count;
            var completedArticlePercentage = 0;
            foreach (string articleName in articleList)
            {
                _counter++;

                try{
                #region progressbar data
                if (maxValue > 0)
                {
                    completedArticlePercentage = ((_counter * 100) / maxValue);
                }
                else
                {
                    completedArticlePercentage = 5;
                }
                if (OnRobotPercentageFeedback != null)
                {
                    OnRobotPercentageFeedback(completedArticlePercentage);
                }

                #endregion

                string TemplateName = "Template:" + articleName;


                Debug.WriteLine(_counter + ")ArticleName:" + articleName);
                if (OnRobotLogFeedBack != null)
                {
                    OnRobotLogFeedBack("\n----------------------------------------------\n");
                    OnRobotLogFeedBack("Opening article:" + articleName + " from " + editor.ApiURL);
                }
                string englishArticle = editor.Open(TemplateName, false);

                //clearing cache
                articleContentCache.ClearCache();
                articleContentCache.ArticleName = TemplateName;
                articleContentCache.Article_WikiContent = englishArticle;



                if (!string.IsNullOrEmpty(articleContentCache.Article_WikiContent))
                {
                    if (OnRobotLogFeedBack != null)
                    {
                        OnRobotLogFeedBack("Opening article:" + articleContentCache.ArticleName + " from " + bot.ApiURL);
                    }
                    string nepaliArticle = bot.Open(articleContentCache.ArticleName, false); //Don't know what to do for content "nepaliArticle"

                    //if(sourceWiki.ToLower().Trim()=="hi"){
                    //this.richTextBoxHindi.Text = englishArticle;
                    //richTextBoxHindi.Invoke(this.OnHindiTextObtained, englishArticle);

                    if (EnableTranslation)
                    {
                        this.TranslateTemplateThread();
                        if (OnRobotLogFeedBack != null)
                        {
                            if (templateTranslatorThread != null)
                                OnRobotLogFeedBack("\nThread started : " + templateTranslatorThread.Name);

                        }

                        //Thread Joining here
                        if (templateTranslatorThread != null)
                        {
                            templateTranslatorThread.Join();
                        }
                        //end thread join


                        if (OnRobotLogFeedBack != null)
                        {
                            OnRobotLogFeedBack("\nText to translate : " + articleContentCache.Article_WikiContent);
                        }


                        if (OnRobotLogFeedBack != null)
                        {
                            OnRobotLogFeedBack("\ntranslated text : " + articleContentCache.TranslatedArticle_Content);
                        }

                        if (OnArticleCacheReady != null)
                        {
                            if (OnRobotLogFeedBack != null) OnRobotLogFeedBack("Sending article cache");
                            OnArticleCacheReady(articleContentCache);
                        }

                        while (this.templateTranslatorThread.IsAlive)
                        {
                            //MessageBox.Show("Editor is busy");
                            if (OnRobotLogFeedBack != null)
                            {
                                OnRobotLogFeedBack("\n Editor is busy");
                            }
                            Thread.Sleep(5000);
                        }
                    }

                    

                    if (!bot.URL.ToLower().Contains("http://en.wikipedia.org/w/") || (!bot.URL.ToLower().Contains("http://" + sourceWiki + ".wikipedia.org/w/"))) //Don't mess with en wikipedia and sourceWikipedia 
                    {//Write protect for english wikipedia
                        string editSummary = string.IsNullOrEmpty(EditSummary)
                                                 ? "Copy template from " + sourceWiki
                                                 : EditSummary;
                        editSummary = editSummary + " from  " + editor.URL + "index.php?oldid=" + editor.Page.RevisionID.ToString()+" [[:"+sourceWiki+":" +editor.Page.Title+"]]";
                        bool _templateSaveSuccess = true;
                        if (!bot.Page.Exists)
                        {

                            if (OnRobotLogFeedBack != null)
                            {
                                OnRobotLogFeedBack("Going to save "+bot.Page.Title);
                            }
                            string _nepaliArticleToSave = articleContentCache.Article_WikiContent;
                            if (articleContentCache.IsTranslatedArticle)
                                _nepaliArticleToSave = articleContentCache.TranslatedArticle_Content;

                            if(OnArticleCacheReady!=null){
                                OnArticleCacheReady(articleContentCache);
                            }

                            SaveInfo saveInfo = bot.Save(_nepaliArticleToSave, editSummary, false, WatchOptions.NoChange);

                            if (OnRobotLogFeedBack != null)
                            {
                                OnRobotLogFeedBack("Saved : " + bot.Page.Title + saveInfo.ResponseXml.InnerText);
                            }
                            if (!TemplateName.EndsWith("/doc"))
                            {
                                string templateDocName = TemplateName.Trim() + "/doc";
                                string englishTemplateDoc = editor.Open(templateDocName);
                                if (!string.IsNullOrEmpty(englishTemplateDoc))
                                {
                                    string nepaliArticleDoc = bot.Open(templateDocName, false);
                                    if (!bot.Page.Exists)
                                    {
                                        SaveInfo docSaveInfo = bot.Save(englishTemplateDoc, editSummary, false,
                                                                        WatchOptions.NoChange);
                                        if (OnRobotLogFeedBack != null)
                                        {
                                            OnRobotLogFeedBack("\n Found a template doc, Copied Template doc as well : " + docSaveInfo.ResponseXml);
                                        }
                                    }
                                }
                            }
                        }
                    }







                }
                if(OnArticleProcessed!=null )
                {
                    OnArticleProcessed(articleContentCache);
                }
                Thread.Sleep(m_ThreadSleepTime); //Let Wiki Breathe

            }catch(Exception e){
                    WikiFunctions.ErrorHandler.Handle(e);
                }

            }


        }

       //Quit all the threads because we want to exit the application
       public void AbortThreads()
       {
           if(this.templateTranslatorThread!=null){
               if(this.templateTranslatorThread.IsAlive)
               this.templateTranslatorThread.Abort();

           }
           if(this.templateCopyThread!=null){
               if(this.templateCopyThread.IsAlive)
                this.templateCopyThread.Abort();
           }
           
       }
    }
}
