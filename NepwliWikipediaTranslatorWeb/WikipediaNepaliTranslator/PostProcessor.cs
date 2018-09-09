using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WikipediaNepaliTranslator
{
    public class PostProcessor
    {

        #region private variables

        private Dictionary<string, string> AllRules = new Dictionary<string, string>();
        private Dictionary<string, string> VerbList = new Dictionary<string, string>();
        private Dictionary<string, string> NounList = new Dictionary<string, string>();
        private Dictionary<string, string> AdjectiveList = new Dictionary<string, string>();
        private Dictionary<string, string> PronounList = new Dictionary<string, string>();
        //private Dictionary<string,string> UsersCustomRuleList = new Dictionary<string, string>(); //User's custom rules
        /// <summary>
        /// Nouns common in both languages (eg: hindi and Nepali)
        /// </summary>
        private List<string> NCBLList = new List<string>(); //Noun rules
        private string originalString = "";

        #endregion

        #region .ctor

        /// <summary>
        /// parameterless constructor
        /// </summary>
        public PostProcessor() { }

        /// <summary>
        /// only loads rule filename
        /// </summary>
        /// <param name="ruleFileName"></param>
        public PostProcessor(string ruleFileName)
        {
            LoadRules(ruleFileName);
        }


        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="ruleFileName"></param>
        /// <param name="verbListFileName"></param>
        public PostProcessor(string ruleFileName, string verbListFileName)
        {
            LoadVerbList(verbListFileName);
            LoadRules(ruleFileName);


        }

        public PostProcessor(string ruleFileName, string verbListFileName, string nounListFileName)
        {
            LoadVerbList(verbListFileName);
            LoadNounList(nounListFileName);
            LoadRules(ruleFileName);


        }

        public PostProcessor(string ruleFileName, string verbListFileName, string nounListFileName, string adjectiveListFileName)
        {
            LoadVerbList(verbListFileName);
            LoadNounList(nounListFileName);
            LoadAdjectiveList(adjectiveListFileName);
            LoadRules(ruleFileName);


        }

        public PostProcessor(string ruleFileName, string verbListFileName, string nounListFileName, string adjectiveListFileName, string pronounListFileName)
        {
            LoadVerbList(verbListFileName);
            LoadNounList(nounListFileName);
            LoadAdjectiveList(adjectiveListFileName);
            LoadPronounList(pronounListFileName);
            LoadRules(ruleFileName);


     }

     public PostProcessor(string ruleFileName, string verbListFileName, string nounListFileName, string adjectiveListFileName, string pronounListFileName, string ncbListFileName)
     {
         //this.verbListFileName = verbListFileName;
         //this.nounListFileName = nounListFileName;
         //this.adjectiveListFileName = adjectiveListFileName;
         //this.pronounListFileName = pronounListFileName;
         //this.ruleFileName = ruleFileName;
         //this.usersCustomFileName = usersCustomFileName;
         //this.ncbListFileName = ncbListFileName;

         LoadNounsCommonInBothLanguageList(ncbListFileName, true);
                  LoadVerbList(verbListFileName);
                  LoadNounList(nounListFileName);
                  LoadAdjectiveList(adjectiveListFileName);
                  LoadPronounList(pronounListFileName);
                  LoadRules(ruleFileName);
                  
     }

        #endregion

        #region methods

        /// <summary>
        /// Loads verb list
        /// </summary>
        /// <param name="verbListFileName"></param>
        private void LoadVerbList(string verbListFileName)
        {
            VerbList = new Dictionary<string, string>();

            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream verbStream = asm.GetManifestResourceStream("WikipediaNepaliTranslator."+verbListFileName);

            //Create one if it doesnot exist
//            if (!File.Exists(verbListFileName))
//              { {var fileStream =  File.Create(verbListFileName);
//              fileStream.Close(); }
//

            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(verbStream);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace("  ", "").Trim();

                inputText += (tempString + "\n");
                //split the text with |
                var splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Count() == 2)
                {
                    if (!VerbList.ContainsKey(splittedText[0]))
                        VerbList.Add(splittedText[0], splittedText[1]);
                }

            }



            sr.Close();
        }



        /// <summary>
        /// Loads Nouns from file
        /// </summary>
        /// <param name="nounListFileName"></param>
        private void LoadNounsCommonInBothLanguageList(string nounsCommonInBothLanguageListFileName)
        {
            NCBLList = new List<string>();
            //Create one if it doesnot exist
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream nounsCommonInBothLanguageStream = asm.GetManifestResourceStream("WikipediaNepaliTranslator." + nounsCommonInBothLanguageListFileName);


            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(nounsCommonInBothLanguageStream);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace("  ", "").Trim();

                inputText += (tempString + "\n");
                //split the text with |
                string splittedText = tempString;
                if (!NCBLList.Contains(splittedText))
                    NCBLList.Add(splittedText);


            }

            sr.Close();
        }

        /// <summary>
        /// Loads Nouns from file
        /// </summary>
        /// <param name="nounListFileName"></param>
        private void LoadNounsCommonInBothLanguageList(string ncblListFileName, bool loadOnlySelectedNounsFromDictionary)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream nounsCommonInBothLanguageStream = asm.GetManifestResourceStream("WikipediaNepaliTranslator." + ncblListFileName);


            string tempString;
            string inputText = "";
            
            NCBLList = new List<string>();

            StreamReader sr = new StreamReader(nounsCommonInBothLanguageStream);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace("  ", "").Trim();

                inputText += (tempString + "\n");
                //split the text with |
                string splittedText = tempString;

                if (!NCBLList.Contains(splittedText))
                {
                    if (originalString.Contains(splittedText))
                    //if the input text contains any noun, load only those nouns in the rule
                    {
                        NCBLList.Add(tempString);
                    }
                }


            }



            sr.Close();
        }


        /// <summary>
        /// Loads Nouns from file
        /// </summary>
        /// <param name="nounListFileName"></param>
        private void LoadNounList(string nounListFileName)
        {
            NounList = new Dictionary<string, string>();
            //Create one if it doesnot exist
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream nounStream = asm.GetManifestResourceStream("WikipediaNepaliTranslator." + nounListFileName);


            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(nounStream);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace("  ", "").Trim();

                inputText += (tempString + "\n");
                //split the text with |
                var splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Count() == 2)
                {
                    if (!NounList.ContainsKey(splittedText[0]))
                        NounList.Add(splittedText[0], splittedText[1]);
                }

            }



            sr.Close();
        }


        /// <summary>
        /// Loads adjective List from file
        /// </summary>
        /// <param name="adjectiveListFileName"></param>
        private void LoadAdjectiveList(string adjectiveListFileName)
        {
            AdjectiveList = new Dictionary<string, string>();
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream adjectiveStream = asm.GetManifestResourceStream("WikipediaNepaliTranslator." + adjectiveListFileName);
            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(adjectiveStream);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace(" ", "").Trim();
                inputText += (tempString + "\n");

                var splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Count() == 2)
                {
                    if (!AdjectiveList.ContainsKey(splittedText[0]))
                        AdjectiveList.Add(splittedText[0], splittedText[1]);
                }
            }



        }


        /// <summary>
        // Loads pronoun List from file
        /// </summary>
        /// <param name="pronounListFileName"></param>
        private void LoadPronounList(string pronounListFileName)
        {
            PronounList = new Dictionary<string, string>();
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream pronounStream = asm.GetManifestResourceStream("WikipediaNepaliTranslator." + pronounListFileName);
            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(pronounStream);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace(" ", "").Trim();
                inputText += (tempString + "\n");

                var splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Count() == 2)
                {
                    if (!PronounList.ContainsKey(splittedText[0]))
                        PronounList.Add(splittedText[0], splittedText[1]);
                }
            }



        }

        /// <summary>
        /// Load rules from the rule file
        /// </summary>
        /// <param name="ruleFileName"></param>
        void LoadRules(string ruleFileName)
        {
            string tempString;
            string inputText = "";

        foreach (string singleString in NCBLList)
        {
            if(!NounList.ContainsKey(singleString))
                NounList.Add(singleString,singleString);
        }
        NCBLList.Clear();
            //Create one if it doesnot exist
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream ruleStream = asm.GetManifestResourceStream("WikipediaNepaliTranslator." + ruleFileName);

            StreamReader sr = new StreamReader(ruleStream);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                inputText += (tempString + "\n");
                //split the text with |
                var splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Count() == 2)
                {

                    if (!AllRules.ContainsKey(splittedText[0]))
                    {
                        if (splittedText[1].Contains("(blank)+")) //Removing unwanted things ((blank) will be trimmed
                        {
                            AllRules.Add(splittedText[0], "");
                        }
                        else
                            if (splittedText[0].Contains("(hiadjective)+") && splittedText[0].Contains("(hinoun)+"))
                            {
                                //combine adjective + noun
                                foreach (KeyValuePair<string, string> adjectiveListPairPart in AdjectiveList)
                                {
                                    var adjectiveKeyToAdd = splittedText[0].Replace("(hiadjective)+", adjectiveListPairPart.Key);
                                    foreach (KeyValuePair<string, string> nounListPart in NounList)
                                    {
                                        var keyToAdd = adjectiveKeyToAdd.Replace("(hinoun)+", nounListPart.Key);
                                        if (!AllRules.ContainsKey(keyToAdd))
                                        {

                                            keyToAdd = keyToAdd.Replace("(hiadjective)+", adjectiveListPairPart.Value);
                                        var valueToAdd = splittedText[1];
                                        valueToAdd = valueToAdd.Replace("(neadjective)+", adjectiveListPairPart.Value);
                                        valueToAdd = valueToAdd.Replace("(neadjective)", adjectiveListPairPart.Value);
                                        valueToAdd = valueToAdd.Replace("(nenoun)+", nounListPart.Value);
                                        valueToAdd = valueToAdd.Replace("(nenoun)", nounListPart.Value);
                                        AllRules.Add(keyToAdd, valueToAdd);
                                        }
                                    }
                                }
                            }
                            else
                                if (splittedText[0].Contains("(hiverb)+"))
                                {

                                    foreach (KeyValuePair<string, string> verbListPairs in VerbList)
                                    {
                                        var keyToAdd = splittedText[0].Replace("(hiverb)+", verbListPairs.Key);
                                        if (!AllRules.ContainsKey(keyToAdd))
                                        {
                                            //keyToAdd.Contains("लगे") && (AllRules.Count>=1072)
                                            AllRules.Add(keyToAdd, splittedText[1].Replace("(neverb)+", verbListPairs.Value));
                                        }
                                    }
                                }
                                else
                                    if (splittedText[0].Contains("(hipronoun)+"))
                                    {
                                        foreach (KeyValuePair<string, string> proNounListPairs in PronounList)
                                        {
                                            var keyToAdd = splittedText[0].Replace("(hipronoun)+", proNounListPairs.Key);
                                            if (!AllRules.ContainsKey(keyToAdd))
                                            {

                                                AllRules.Add(keyToAdd, splittedText[1].Replace("(nepronoun)+", proNounListPairs.Value));
                                            }
                                        }

                                    }
                                    else
                                        if (splittedText[0].Contains("(hinoun)+"))
                                        {
                                            foreach (KeyValuePair<string, string> nounListPairs in NounList)
                                            {
                                                var keyToAdd = splittedText[0].Replace("(hinoun)+", nounListPairs.Key);
                                                if (!AllRules.ContainsKey(keyToAdd))
                                                {

                                                    AllRules.Add(keyToAdd, splittedText[1].Replace("(nenoun)+", nounListPairs.Value));
                                                }
                                            }
                        }
                        else
                            if (splittedText[0].Contains("(rootnoun)+"))
                            {
                                foreach (string nouncommoninBothLanguageListItem in NCBLList)
                                {
                                    string keyToAdd = splittedText[0].Replace("(rootnoun)+", nouncommoninBothLanguageListItem);
                                    if (!AllRules.ContainsKey(keyToAdd))
                                    {

                                        AllRules.Add(keyToAdd, splittedText[1].Replace("(rootnoun)+", nouncommoninBothLanguageListItem));
                                    }
                                }

                            }
                                        else
                                            if (splittedText[0].Contains("(hiadjective)+"))
                                            {
                                                foreach (KeyValuePair<string, string> adjectiveListPairs in AdjectiveList)
                                                {
                                                    var keyToAdd = splittedText[0].Replace("(hiadjective)+", adjectiveListPairs.Key);
                                                    if (!AllRules.ContainsKey(keyToAdd))
                                                    {

                                                        AllRules.Add(keyToAdd, splittedText[1].Replace("(neadjective)+", adjectiveListPairs.Value));
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                var keyToAdd = splittedText[0];
                                                if (!AllRules.ContainsKey(keyToAdd))
                                                {
                                                    AllRules.Add(keyToAdd, splittedText[1]);
                                                }

                                            }

                    }
                }

                else //This block adds support for separating nouns with aakars and eekars, for better parsing
                {




                    if (splittedText.Count() == 3)
                        if (splittedText[0].Contains("(hinounwithsuffix)+")) //handling nouns with suffix: aakar etc
                        {
                            foreach (KeyValuePair<string, string> nounListPairs in NounList)
                            {

                                if (nounListPairs.Key.EndsWith(splittedText[2])) //make sure the third parameter is intact
                                {
                                    var keyToAdd = splittedText[0].Replace("(hinounwithsuffix)+", nounListPairs.Key);
                                    if (!AllRules.ContainsKey(keyToAdd))
                                    {


                                        AllRules.Add(keyToAdd,
                                                     splittedText[1].Replace("(nenoun)+", nounListPairs.Value));
                                    }
                                }
                            }

                        }
                        else

                            if (splittedText[0].Contains("(hiverbwithsuffix)+")) // Input: निभाया जाने लगा था 
                            {    //Output : निभाउन थालिएको थियो  
                                //Rule: (hiverbwithsuffix)+या जाने लगा था|(neverb)+उन थालिएको थियो|ा
                                foreach (KeyValuePair<string, string> verbsListPair in VerbList)
                                {

                                    if (verbsListPair.Key.EndsWith(splittedText[2])) //make sure the third parameter is intact
                                    {
                                        var keyToAdd = splittedText[0].Replace("(hiverbwithsuffix)+", verbsListPair.Key);
                                        if (!AllRules.ContainsKey(keyToAdd))
                                        {


                                            AllRules.Add(keyToAdd,
                                                         splittedText[1].Replace("(neverb)+", verbsListPair.Value));
                                        }
                                    }
                                }

                            }
                }

            }

        Dictionary<string,string> RulesToRemove = new Dictionary<string, string>();
        Dictionary<string,string> identicalRules = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> rule in AllRules)
        {
            if(rule.Key.Contains("{endswithoutvowel}"))
            {
                
                RulesToRemove.Add(rule.Key,rule.Value);
                
            }
            if(rule.Key==rule.Value)
            {
                identicalRules.Add(rule.Key,rule.Value);
            }
        }
        foreach (KeyValuePair<string, string> singleRuleToRemove in RulesToRemove)
        {
            AllRules.Remove(singleRuleToRemove.Key);

            string sourceToSearch = singleRuleToRemove.Key;
            string textToSearch = "{endswithoutvowel}";

            int positionToSearch = sourceToSearch.IndexOf(textToSearch);
            string makeSubString = sourceToSearch.Substring(0, (sourceToSearch.Length - positionToSearch));
            bool endsWithoutAVowel = true;
           
            Regex thisRegex = new Regex(@"[\u0905-\u0939|\u0958-\u095F]\{endswithoutvowel\}");
            //               Matches   (Ka - ha ) or (ka with amkibindu - ya with amkibindu) + {endswithoutvowel}
            if(thisRegex.IsMatch(singleRuleToRemove.Key))
            {
                
                endsWithoutAVowel = true;
                string KeyToAdd = singleRuleToRemove.Key.Replace("{endswithoutvowel}", "");
                if (!AllRules.ContainsKey(KeyToAdd))
                    AllRules.Add(KeyToAdd, singleRuleToRemove.Value);

                //Debug.WriteLine("KeyAdded: Key:" + KeyToAdd + "Value:" + singleRuleToRemove.Value);
                
            }
//            else
//            {
//                Debug.WriteLine("KeyNotAdded => Key:" + singleRuleToRemove.Key + "Value:" + singleRuleToRemove.Value);   
//            }
             
            
        }
        foreach (KeyValuePair<string, string> rule in identicalRules)
        {
            AllRules.Remove(rule.Key);
        }
            //            StreamWriter ruleStreamwriter = new StreamWriter("AllRulesDebugger.txt");
            //            foreach (KeyValuePair<string, string> singleRule in AllRules)
            //            {
            //                ruleStreamwriter.Write(" * " + singleRule.Key + "|" + singleRule.Value+"\n");
            //
            //            }
            //            
            //            ruleStreamwriter.Close();
            //
            sr.Close();

        }






        /// <summary>
        /// Sets the original string 
        /// </summary>
        /// <param name="paramString"></param>
        public void LoadTextToTranslate(string paramString)
        {
            this.originalString = paramString;

        }

        /// <summary>
        /// Translate the string
        /// </summary>
        public string Translate()
        {
            string translatedString = originalString; //creating a copy
            if (string.IsNullOrEmpty(originalString))
            {
                //throw new Exception("Original String is null"); //if there is nothing for translation, throw an exception
                throw new Exception("There is nothing to translate, please compy some text in hindi tab");
                return "";
            }
            if (AllRules.Count() <= 0)
            {
                //throw new Exception("There are no rules for translation"); //if there are no rules for translation, throw an exception
                throw  new Exception(
                    "There are no rules for translation, please press control + space and then add some rules for translations");
                return translatedString;
            }
            else
            {
                foreach (KeyValuePair<string, string> rule in AllRules) //iterate for all rules in this file
                {

                    if (rule.Key.Contains("{regex}"))
                    {

                        try
                        {
                            //Parse regex rules
                            string newKey = rule.Key.Replace("{regex}", "");


                            // matches क-ह
                            Regex aRegex = new Regex(newKey);

                            translatedString = aRegex.Replace(translatedString, rule.Value);
                            //translatedString = translatedString.Replace(newKey + "replacestring", "");


                        }
                        catch (Exception)
                        {

                            //throw;
                        }



                    }
                    else
                        if (rule.Key.Contains("{unicoderegex}"))
                        {
                            //Parse regex rules
                            string newKey = rule.Key.Replace("{unicoderegex}", "");


                            // matches क-ह
                            Regex aRegex = new Regex(@"[\u0901-\u25CC]" + newKey);

                            translatedString = aRegex.Replace(translatedString, "$&replacestring" + rule.Value);
                            translatedString = translatedString.Replace(newKey + "replacestring", "");


                        }
                        else
                        {
                            //Parse normal translations from hinditoNepali.txt
                            translatedString = translatedString.Replace(rule.Key, rule.Value); //replace everything one by one    
                        }


                }
            }
            return translatedString;

        }


        #endregion



    }
}