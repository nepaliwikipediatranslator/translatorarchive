using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WikipediaNepaliTranslator
{
    public partial class _Default : System.Web.UI.Page
    {
        public const string RuleFileName = "HindiToNepali.txt";
        //public const string KakharaFileName = "Kakhara.txt";
        public const string VerbListFileName = "verblist.txt";
        public const string NounListFileName = "nounlist.txt";
        public const string AdjectiveListFileName = "adjectivelist.txt";
        public const string PronounListFileName = "pronounlist.txt";
        public const string NounsCommonInBothLanguageFileName = "NounsCommonInBothLanguage.txt";
        private string currentlyTranslatingText = String.Empty;

        private Log log  = new Log();
        DBDataContext db = new DBDataContext();

        private PostProcessor pr;
        private RuleEditor ruleEditor;
        private RuleEditor verbEditor;
        private RuleEditor nounEditor;
        private RuleEditor adjectiveEditor;
        private RuleEditor pronounEditor;
        private SearchBox searchBox;

        protected void Page_Load(object sender, EventArgs e)
        {
//            ruleEditor = new RuleEditor();
//            ruleEditor.SetFileName(RuleFileName);
//
//            verbEditor = new RuleEditor();
//            verbEditor.SetFileName(VerbListFileName);
//
//            nounEditor = new RuleEditor();
//            nounEditor.SetFileName(NounListFileName);
//
//            adjectiveEditor = new RuleEditor();
//            adjectiveEditor.SetFileName(AdjectiveListFileName);
//
//            pronounEditor = new RuleEditor();
//            pronounEditor.SetFileName(PronounListFileName);
            if(!Page.IsPostBack)
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null &&
                    HttpContext.Current.Request.UrlReferrer != null)
                {
                    referrerfield.Value = HttpContext.Current.Request.UrlReferrer.ToString();
                }
            }

        }
        private void OpenInputFile(string inputFileName)
        {
            string tempString;
            string inputText = "";
            StreamReader sr = new StreamReader(inputFileName);
            while ((tempString = sr.ReadLine()) != null)
            {
                inputText += (tempString + "\n");
            }
            sr.Close();
            this.richTextBoxHindi.InnerText = inputText;

            pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName, PronounListFileName,NounsCommonInBothLanguageFileName);
            pr.LoadTextToTranslate(inputText);


            this.richTextBoxNepali.InnerText = pr.Translate();

            
            
        }

        
        
        protected void translateButton_Click(object sender, EventArgs e)
        {
            currentlyTranslatingText = richTextBoxHindi.InnerText;


            if(!string.IsNullOrEmpty(richTextBoxHindi.InnerText))
            {
                string q = richTextBoxHindi.InnerText;
                

                NepaliTranslatorHelper helper = new NepaliTranslatorHelper();
                q = helper.TranslateIfEnglish(q); //Translate to Hindi if it is in English.
                currentlyTranslatingText = q;
                this.PerformTranslation();
            }
        }

        private void PerformTranslation()
        {
            pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName, PronounListFileName,NounsCommonInBothLanguageFileName);
            pr.LoadTextToTranslate(currentlyTranslatingText);
            this.richTextBoxNepali.InnerText = pr.Translate();



            if (HttpContext.Current!=null && HttpContext.Current.Request != null && HttpContext.Current.Request.UrlReferrer != null)
                log.referrer = HttpContext.Current.Request.UrlReferrer.ToString();
            else log.referrer = "";

            
            log.date = DateTime.Now;
            log.input = currentlyTranslatingText;
            log.output = richTextBoxNepali.InnerText;
            if (HttpContext.Current!=null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != null)
                log.ip = HttpContext.Current.Request.UserHostAddress.ToString();
            else log.ip = "";

            AddLogs();            
        }

        [WebMethod]
        public string m(string q)
        {


            return GetTranslatedText(q);


        }
        [WebMethod]
        public string M(string q)
        {
            return GetTranslatedText(q);

        }

        string GetTranslatedText(string inputstring)
        {
            pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName, PronounListFileName,NounsCommonInBothLanguageFileName);
            string returnString = (string.IsNullOrEmpty(inputstring) ? "" : inputstring);
            pr.LoadTextToTranslate(inputstring);
            returnString = pr.Translate();




            if (referrerfield.Value != null)
                log.referrer = referrerfield.Value;
            else log.referrer = "";

            log.input = inputstring;
            log.output = returnString;
            log.date = DateTime.Now;
            log.webservice = false;

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != null)
                log.ip = HttpContext.Current.Request.UserHostAddress.ToString();
            else log.ip = "";



           // AddLogs();
            return returnString;

        }

        void AddLogs()
        {

            try
            {
                db.Log.InsertOnSubmit(log);
                db.SubmitChanges();

            }
            catch (Exception e)
            {
                
                HttpContext.Current.Response.Write("Error writing in db"+e.Message+e.Source+e.StackTrace);
            }


        }

    }
}
