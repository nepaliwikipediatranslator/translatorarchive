using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WikipediaNepaliTranslator
{
    /// <summary>
    /// Summary description for M
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class M : System.Web.Services.WebService
    {
        public const string RuleFileName = "HindiToNepali.txt";
        //public const string KakharaFileName = "Kakhara.txt";
        public const string VerbListFileName = "verblist.txt";
        public const string NounListFileName = "nounlist.txt";
        public const string AdjectiveListFileName = "adjectivelist.txt";
        public const string PronounListFileName = "pronounlist.txt";
        public const string NounsCommonInBothLanguageFileName = "NounsCommonInBothLanguage.txt";

        private Log log = new Log();
        DBDataContext db = new DBDataContext();

        private PostProcessor pr;


        string GetTranslatedText(string inputstring)
        {
            pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName, PronounListFileName,NounsCommonInBothLanguageFileName);
            

            string returnString = (string.IsNullOrEmpty(inputstring) ? "" : inputstring);
            pr.LoadTextToTranslate(inputstring);
            returnString = pr.Translate();



            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UrlReferrer != null)
                log.referrer = HttpContext.Current.Request.UrlReferrer.ToString();
            else log.referrer = "";

            log.input = inputstring;
            log.output = returnString;
            log.date = DateTime.Now;
            log.webservice = true;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != null)
                log.ip = HttpContext.Current.Request.UserHostAddress.ToString();
            else log.ip = "";

            AddLogs();
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

                HttpContext.Current.Response.Write("Error writing in db" + e.Message + e.Source + e.StackTrace);
            }


        }


        [WebMethod]
        public string Translate(string q)
        {
            if (string.IsNullOrEmpty(q)) return "";

            NepaliTranslatorHelper helper = new NepaliTranslatorHelper();
            //q = helper.TranslateIfEnglish(q); //Translate to Hindi if it is in English.
            return GetTranslatedText(q); //Translate to Nepali
        }
    }
}
