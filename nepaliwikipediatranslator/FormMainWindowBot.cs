using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WikiFunctions.API;

namespace NepaliWikiPediaTranslator
{
    public partial class FormMainWindow
    {
        private string FormMainWindowUserName = "";
        private string FormMainWindowPassword = "";
        private bool SkipWithOutSaving = false;
        private bool SaveAndMoveNext = false;
        private bool BotMode = true;
        private string ArticleName;
        private ArticleCache  articleContentCache = new ArticleCache();
        private bool _IsEditorBusy = false;
        public string EditSummary { get; set; }
        private string RichTextBoxHindiText { get; set; }

        WikiFunctions.API.ApiEdit bot = new ApiEdit("http://ne.wikipedia.org/w/", false);
        public WikiFunctions.API.ApiEdit editor = new ApiEdit("http://en.wikipedia.org/w/");

        public bool IsEditorBusy
        {
            get { return _IsEditorBusy; }
        }

        public List<string> ListOfArticles { get; set; }
        public string SourceWiki { get; set; }

        public delegate void RobotProgressFeedbackDelgate(int percent);

        public event RobotProgressFeedbackDelgate OnRobotPercentageFeedback;

        public delegate void RobotLoggerDelegate(string message);

        public event RobotLoggerDelegate OnRobotLogFeedBack;

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
            this.OnHindiTextObtained += new SetHindiText(C_SetRichTextBoxHindi);
            //this.OnRobotLogFeedBack +=  new RobotLoggerDelegate();
            //this.OnRobotLogFeedBack+= new NepaliWikiPediaTranslator.Bots.TemplateCopier.RobotLoggerDelegate(NepaliWikiPediaTranslator.Bots.TemplateCopier.RobotLoggerDelegate)
            templateCopyThread.Start();

        }

        public void TranslateTemplateThread()
        {
            _IsEditorBusy = true;
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
            _IsEditorBusy = false;
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
            if(editor ==null){
                editor = new ApiEdit("http://" + sourceWiki + ".wikipedia.org/w/", false);
                editor.Login(FormMainWindowUserName, FormMainWindowPassword);
            }

            if (editor.User==null)
                editor.Login(FormMainWindowUserName, FormMainWindowPassword);

            if(editor.User.Name==null)
                editor.Login(FormMainWindowUserName, FormMainWindowPassword);

            
            if(bot==null){
                bot = new ApiEdit("http://ne.wikipedia.org/w/", false);
                bot.Login(FormMainWindowUserName,FormMainWindowPassword);
            }
            if(bot.User==null){
                bot.Login(FormMainWindowUserName, FormMainWindowPassword);}
            if(bot.User.Name ==null){
                bot.Login(FormMainWindowUserName, FormMainWindowPassword);}

            
            

            int _counter = 0;
            int maxValue = articleList.Count;
            var completedArticlePercentage = 0;
            foreach (string articleName in articleList)
            {
                _counter++;
                
                #region progressbar data
                if (maxValue > 0)
                {
                    completedArticlePercentage = ((_counter * 100) / maxValue);
                }
                else
                {
                    completedArticlePercentage = 5;
                }
                if(OnRobotPercentageFeedback!=null)
                {
                    OnRobotPercentageFeedback(completedArticlePercentage);
                }

                #endregion

                string TemplateName = "Template:" + articleName;
                

                Debug.WriteLine(_counter +")ArticleName:"+articleName);
                if(OnRobotLogFeedBack!=null)
                {
                    OnRobotLogFeedBack("\n----------------------------------------------\n");
                    OnRobotLogFeedBack("Opening article:" + articleName + " from " + editor.ApiURL);
                }
                string englishArticle = editor.Open(TemplateName,false);

                //clearing cache
                articleContentCache .ClearCache();
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

                    //if (sourceWiki.ToLower().Trim() == "hi"){
                    this.TranslateTemplateThread();
                    if (OnRobotLogFeedBack != null)
                    {
                        if(templateTranslatorThread!=null)
                        OnRobotLogFeedBack("\nThread started : " + templateTranslatorThread.Name);

                    }

                    //Thread Joining here
                    if(templateTranslatorThread!=null)
                    {templateTranslatorThread.Join();}
                    //end thread join


                    if (OnRobotLogFeedBack != null){
                        OnRobotLogFeedBack("\nText to translate : " + articleContentCache.Article_WikiContent);
                    }
                    

                    if (OnRobotLogFeedBack != null)
                    {
                        OnRobotLogFeedBack("\ntranslated text : " + articleContentCache.TranslatedArticle_Content);
                    }
                    //}

                    while(this.IsEditorBusy)
                    {
                        //MessageBox.Show("Editor is busy");
                        if (OnRobotLogFeedBack != null)
                        {
                            OnRobotLogFeedBack("\n Editor is busy");
                        }
                        Thread.Sleep(5000);
                    }
                        
                    if (!bot.URL.ToLower().Contains("http://en.wikipedia.org/w/") || (!bot.URL.ToLower().Contains("http://" + sourceWiki + ".wikipedia.org/w/"))) //Don't mess with en wikipedia and sourceWikipedia 
                    {//Write protect for english wikipedia
                        string editSummary = string.IsNullOrEmpty(EditSummary)
                                                 ? "Copy template from "+sourceWiki
                                                 : EditSummary;
                        
                        bool _templateSaveSuccess = true;
                        if(!bot.Page.Exists){

                            if (OnRobotLogFeedBack != null)
                            {
                                OnRobotLogFeedBack("\n Going to save it");
                            }
                            string _nepaliArticleToSave = articleContentCache.Article_WikiContent;
                            if (articleContentCache.IsTranslatedArticle)
                                _nepaliArticleToSave = articleContentCache.TranslatedArticle_Content;
                                
                            SaveInfo saveInfo = bot.Save(_nepaliArticleToSave, editSummary, false, WatchOptions.NoChange);

                            if (OnRobotLogFeedBack != null)
                            {
                                OnRobotLogFeedBack("\nSaved : " + saveInfo.ResponseXml);
                            }
                            if(!TemplateName.EndsWith("/doc"))
                            {
                                string templateDocName = TemplateName.Trim() + "/doc";
                                string englishTemplateDoc = editor.Open(templateDocName);
                                if(!string.IsNullOrEmpty(englishTemplateDoc))
                                {
                                    string nepaliArticleDoc = bot.Open(templateDocName, false);
                                    if(!bot.Page.Exists)
                                    {
                                        SaveInfo docSaveInfo = bot.Save(englishTemplateDoc, editSummary, false,
                                                                        WatchOptions.NoChange);
                                        if (OnRobotLogFeedBack != null)
                                        {
                                            OnRobotLogFeedBack("\nSaved Template doc: " + docSaveInfo.ResponseXml);
                                        }
                                    }
                                }
                            }
                        }
                    }    

                        
                        

                   
                    
                    
                }

            }


        }
    }
    public class ArticleCache
    {
        public ArticleCache()
        {
            ArticleName = "";
            Article_WikiContent = "";
            TranslatedArticle_Content = "";
            IsTranslatedArticle = false;
        }

        public string ArticleName { get; set; }
        public string Article_WikiContent { get; set; }
        public string TranslatedArticle_Content { get; set; }

        //Checks whether the article has been ever translated 
        public bool IsTranslatedArticle { get; set; }

        //Checks whether the translated article and the original article are same 
        public bool IsTranslatedArticleSameAsOriginal
        {
            get { return (string.Compare(TranslatedArticle_Content, Article_WikiContent) == 0); }
        }

        /// <summary>
        /// Copies an article cache
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void ShallowCopy(ArticleCache source,ArticleCache destination)
        {
            if(source==null) return;
            if(destination==null) destination= new ArticleCache();
            destination.ArticleName = source.ArticleName;
            destination.Article_WikiContent = source.Article_WikiContent;
            destination.TranslatedArticle_Content = source.TranslatedArticle_Content;
            destination.IsTranslatedArticle = source.IsTranslatedArticle;
        }
        public void ClearCache()
        {
            ArticleName = "";
            Article_WikiContent = "";
            TranslatedArticle_Content = "";
            IsTranslatedArticle = false;
        }
    }
}
