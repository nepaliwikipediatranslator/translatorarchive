using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NepaliWikiPediaTranslator
{
    public class SessionsManager
    {
        public const string RuleFileName = "HindiToNepali.txt";
        //public const string KakharaFileName = "Kakhara.txt";
        public const string VerbListFileName = "verblist.txt";
        public const string NounListFileName = "nounlist.txt";
        public const string NounsCommonInBothLanguageListFileName = "NounsCommonInBothLanguage.txt";
        public const string AdjectiveListFileName = "adjectivelist.txt";
        public const string PronounListFileName = "pronounlist.txt";
        public const string UsersCustomFileName = "Rules.db";
        public const string CorrectionFileName = "correction.txt";
        public const string HELPFILENAME = "Manual.pdf";

        /// <summary>
        /// This file is used for parsing and obtaining templatenames, articlenames from various sources like:
        /// RecentChanges/WantedTemplates/UserContributions etc. 
        /// This file contains regular expressions (Format will be similar to this.RuleFileName)
        /// </summary>
        public const string TemplateParserFileName = "TemplateParser.txt";

        /// <summary>
        /// a zero sized file, should not contain nothing. 
        /// </summary>
        public const string BlankFileName = "temp_temp";

        public Login LoginData = new Login();

        public SessionsManager()
        {
            
        }
    }

}
