using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Text.RegularExpressions;

namespace WikipediaNepaliTranslator
{
    public class NepaliTranslatorHelper
    {
        private static string NepaliWikipediaTranslatorId = "8B88C4ADF2E4CFAC01C159653059F14D35310B41";
        private TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();

        public string TranslateIfEnglish(string inputText)
        {
            if(ContainsEnglishCharacters(inputText))
            {

                string returnString = "Could not connect to http://api.microsofttranslator.com to translate English";
                returnString = MicrosoftTranslator.MsTranslator.Translate(inputText);

                return returnString;
            }
            return inputText;

        }

        public static bool ContainsDevanagariCharacters(string inputstring)
        {
            Regex thisRegex = new Regex(@"[\u0905-\u0963|\u0966-\u096F]");
            //Devanagari
            //const string charSet = @"\u0905-\u0963";
            //const string numSet = @"\u0966-\u096F";
            if (thisRegex.IsMatch(inputstring))
            {

                return true;
            }
            return false;

        }

        public static bool ContainsEnglishCharacters(string inputstring)
        {
            Regex thisRegex = new Regex("[a-zA-Z]");
            if(thisRegex.IsMatch(inputstring))
            {return true;}
            return false;
        }
    }
}
