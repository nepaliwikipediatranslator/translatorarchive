using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ExamplePlugin
{
    class PostProcessor : IPostProcessor
    {

        #region private variables

        private Dictionary<string, string> AllRules = new Dictionary<string, string>(); //Rules from the main rule file
        private Dictionary<string, string> VerbList = new Dictionary<string, string>(); //Verb rules 
        private Dictionary<string, string> NounList = new Dictionary<string, string>(); //Noun rules
        private Dictionary<string, string> AdjectiveList = new Dictionary<string, string>(); //Adjective rules
        private Dictionary<string, string> PronounList = new Dictionary<string, string>(); //Pronoun rules
        private Dictionary<string, string> UsersCustomRuleList = new Dictionary<string, string>(); //User's custom rules
        string ruleFileName, verbListFileName, nounListFileName, adjectiveListFileName, pronounListFileName, usersCustomFileName;

        //I need these delegates to give feedback for the progressbar , the progressbar will be updated once it is completed.
        public delegate void ProgressFeedbackDelgate(int percent);
        public event ProgressFeedbackDelgate OnFeedback;

        //I need these delegates to the main window with the translated string..., the translated text will be provided to the main thread once it is completed
        public delegate void TranslationCompleteDelegate(string message);
        public event TranslationCompleteDelegate OnTranslationComplete;



        private string originalString = ""; //Just in case , a backup of the original string

        //Generate CSV file
        private bool GenerateCsvForGoogle = false; //set this to true if you want the debug the rules and see it in a csv file 
        //Generate CSV file                                

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
            this.ruleFileName = ruleFileName;
            //LoadRules(ruleFileName);
        }


        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="ruleFileName"></param>
        /// <param name="verbListFileName"></param>
        public PostProcessor(string ruleFileName, string verbListFileName)
        {
            this.verbListFileName = verbListFileName;
            this.ruleFileName = ruleFileName;
            //         LoadVerbList(verbListFileName);
            //         LoadRules(ruleFileName);


        }

        public PostProcessor(string ruleFileName, string verbListFileName, string nounListFileName)
        {
            this.verbListFileName = verbListFileName;
            this.nounListFileName = nounListFileName;
            this.ruleFileName = ruleFileName;

            //         LoadVerbList(verbListFileName);
            //         LoadNounList(nounListFileName);
            //         LoadRules(ruleFileName);


        }

        public PostProcessor(string ruleFileName, string verbListFileName, string nounListFileName, string adjectiveListFileName)
        {
            this.verbListFileName = verbListFileName;
            this.nounListFileName = nounListFileName;
            this.adjectiveListFileName = adjectiveListFileName;
            this.ruleFileName = ruleFileName;

            //         LoadVerbList(verbListFileName);
            //         LoadNounList(nounListFileName);
            //         LoadAdjectiveList(adjectiveListFileName);
            //         LoadRules(ruleFileName);


        }

        public PostProcessor(string ruleFileName, string verbListFileName, string nounListFileName, string adjectiveListFileName, string pronounListFileName)
        {
            this.verbListFileName = verbListFileName;
            this.nounListFileName = nounListFileName;
            this.adjectiveListFileName = adjectiveListFileName;
            this.pronounListFileName = pronounListFileName;
            this.ruleFileName = ruleFileName;
            //
            //         LoadVerbList(verbListFileName);
            //         LoadNounList(nounListFileName);
            //         LoadAdjectiveList(adjectiveListFileName);
            //         LoadPronounList(pronounListFileName);
            //         LoadRules(ruleFileName);
        }

        public PostProcessor(string ruleFileName, string verbListFileName, string nounListFileName, string adjectiveListFileName, string pronounListFileName, string usersCustomFileName)
        {
            this.verbListFileName = verbListFileName;
            this.nounListFileName = nounListFileName;
            this.adjectiveListFileName = adjectiveListFileName;
            this.pronounListFileName = pronounListFileName;
            this.ruleFileName = ruleFileName;
            this.usersCustomFileName = usersCustomFileName;

            //         LoadVerbList(verbListFileName);
            //         LoadNounList(nounListFileName);
            //         LoadAdjectiveList(adjectiveListFileName);
            //         LoadPronounList(pronounListFileName);
            //         LoadRules(ruleFileName);
            //         LoadUsersCustomRules(usersCustomFileName);
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
            //Create one if it doesnot exist
            if (!File.Exists(verbListFileName))
            {
                var fileStream = File.Create(verbListFileName);
                fileStream.Close();
            }

            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(verbListFileName);
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
        /// Loads verb list
        /// </summary>
        /// <param name="verbListFileName"></param>
        /// <param name="loadOnlySelectedVerbFromDictionary">Loads only selected verb</param>
        private void LoadVerbList(string verbListFileName, bool loadOnlySelectedVerbFromDictionary)
        {
            if (string.IsNullOrEmpty(this.originalString))
            {
                LoadVerbList(verbListFileName);
                return;
            }
            VerbList = new Dictionary<string, string>();
            //Create one if it doesnot exist
            if (!File.Exists(verbListFileName))
            {
                var fileStream = File.Create(verbListFileName);
                fileStream.Close();
            }

            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(verbListFileName);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace("  ", "").Trim();

                inputText += (tempString + "\n");
                //split the text with |
                var splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Count() == 2)
                {
                    if (!VerbList.ContainsKey(splittedText[0]))
                    {
                        if (this.originalString.Contains(splittedText[0])) //Load only those verbs which are present in the text to translate
                        {
                            VerbList.Add(splittedText[0], splittedText[1]);
                        }
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
            if (!File.Exists(nounListFileName))
            {
                var fileStream = File.Create(nounListFileName);
                fileStream.Close();

            }

            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(nounListFileName);
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
        /// Loads Nouns from file
        /// </summary>
        /// <param name="nounListFileName"></param>
        private void LoadNounList(string nounListFileName, bool loadOnlySelectedNounsFromDictionary)
        {
            if (string.IsNullOrEmpty(this.originalString))
            {
                LoadNounList(nounListFileName);
                return;
            }
            NounList = new Dictionary<string, string>();
            //Create one if it doesnot exist
            if (!File.Exists(nounListFileName))
            {
                var fileStream = File.Create(nounListFileName);
                fileStream.Close();

            }

            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(nounListFileName);
            while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
            {
                tempString = tempString.Replace("  ", "").Trim();

                inputText += (tempString + "\n");
                //split the text with |
                var splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Count() == 2)
                {
                    if (!NounList.ContainsKey(splittedText[0]))
                    {
                        if (this.originalString.Contains(splittedText[0])) //if the input text contains any noun, load only those nouns in the rule
                        {
                            NounList.Add(splittedText[0], splittedText[1]);
                        }
                    }
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
            if (!File.Exists(adjectiveListFileName))
            {
                var fileStream = File.Create(adjectiveListFileName);
                fileStream.Close();

            }
            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(adjectiveListFileName);
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
            if (!File.Exists(pronounListFileName))
            {

                var fileStream = File.Create(pronounListFileName);
                fileStream.Close();
            }
            string tempString;
            string inputText = "";

            StreamReader sr = new StreamReader(pronounListFileName);
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

            //Create one if it doesnot exist
            if (!File.Exists(ruleFileName))
            {
                var fileStream = File.Create(ruleFileName);
                fileStream.Close();
            }

            StreamReader sr = new StreamReader(ruleFileName);
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

            //Now remove extra rules from it
            //lets parse endswithoutvowel
            Dictionary<string, string> RulesToRemove = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> rule in AllRules)
            {
                if (rule.Key.Contains("{endswithoutvowel}"))
                {

                    RulesToRemove.Add(rule.Key, rule.Value);

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
                if (thisRegex.IsMatch(singleRuleToRemove.Key))
                {

                    endsWithoutAVowel = true;
                    string KeyToAdd = singleRuleToRemove.Key.Replace("{endswithoutvowel}", "");
                    if (!AllRules.ContainsKey(KeyToAdd))
                        AllRules.Add(KeyToAdd, singleRuleToRemove.Value);

                    //                Debug.WriteLine("KeyAdded: Key:" + KeyToAdd + "Value:" + singleRuleToRemove.Value);

                }
                //            else
                //            {
                //                Debug.WriteLine("KeyNotAdded => Key:" + singleRuleToRemove.Key + "Value:" + singleRuleToRemove.Value);   
                //            }


            }

            //Generate Rules for debugging. 
            if (this.GenerateCsvForGoogle) { WriteFile(AllRules); }

            sr.Close();


            //Do some work
            //            if (OnFeedback != null)
            //                OnFeedback("Finished Loading Rules", 1);


        }

        void LoadUsersCustomRules(string usersCustomFileName)
        {
            UsersCustomRuleList = new Dictionary<string, string>();
            if (!File.Exists(usersCustomFileName))
            {
                var fileStream = File.Create(usersCustomFileName);
                fileStream.Close();
            }
            else
            {
                string tempString;
                string inputText = "";

                StreamReader sr = new StreamReader(usersCustomFileName);
                while (!string.IsNullOrEmpty(tempString = sr.ReadLine()))
                {
                    tempString = tempString.Replace(" ", "").Trim();
                    inputText += (tempString + "\n");

                    var splittedText = tempString.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (splittedText.Count() == 2)
                    {
                        if (!AllRules.ContainsKey(splittedText[0]))
                            AllRules.Add(splittedText[0], splittedText[1]);
                    }
                }
            }
        }

        public void WriteFile(Dictionary<string, string> allRules)
        {
            List<StreamWriter> dynamicStreamWriter = new List<StreamWriter>();

            StreamWriter ruleStreamwriter0 = new StreamWriter("AllRulesDebugger.csv");
            StreamWriter ruleStreamwriter1 = new StreamWriter("AllRulesDebugger1.csv");
            StreamWriter ruleStreamwriter2 = new StreamWriter("AllRulesDebugger2.csv");
            StreamWriter ruleStreamwriter3 = new StreamWriter("AllRulesDebugger3.csv");
            ruleStreamwriter0.Write("hi,ne,pos,description\n");
            ruleStreamwriter1.Write("hi,ne,pos,description\n");
            ruleStreamwriter2.Write("hi,ne,pos,description\n");
            ruleStreamwriter3.Write("hi,ne,pos,description\n");
            int i = 0;
            int streamWriterCount = -1;
            foreach (KeyValuePair<string, string> singleRule in allRules)
            {
                if (i % 10000 == 0)
                {
                    streamWriterCount++;
                    dynamicStreamWriter.Add(new StreamWriter("AllRulesDebugger" + i + ".csv"));
                    dynamicStreamWriter[streamWriterCount].Write("en,ne,pos,description\n");

                }
                if (!singleRule.Value.Contains("hi") && !singleRule.Value.Contains("ne"))
                {

                    i++;

                    //writing dynamically

                    //dynamicStreamWriter[streamWriterCount].Write("\"" + singleRule.Key + "\",\"" + singleRule.Value + "\", , \n");
                    dynamicStreamWriter[streamWriterCount].Write(singleRule.Key + "|" + singleRule.Value + "\n");


                    //writing dynamically

                    //                                if(i<13000)
                    //                                {
                    //                                    ruleStreamwriter0.Write("\"" + singleRule.Key + "\",\"" + singleRule.Value + "\", , \n");    
                    //                                }
                    //                                if(i>13000 && i<28000)
                    //                                {
                    //                                    ruleStreamwriter1.Write("\"" + singleRule.Key + "\",\"" + singleRule.Value + "\", , \n");
                    //                                }
                    //                                if(i>28000 && i<42000)
                    //                                {
                    //                                    ruleStreamwriter2.Write("\"" + singleRule.Key + "\",\"" + singleRule.Value + "\", , \n");
                    //                                }
                    //                                if(i>42000)
                    //                                {
                    //                                    ruleStreamwriter3.Write("\"" + singleRule.Key + "\",\"" + singleRule.Value + "\", , \n");
                    //                                }
                }

            }

            ruleStreamwriter0.Close();
            ruleStreamwriter1.Close();
            ruleStreamwriter2.Close();
            ruleStreamwriter3.Close();
            foreach (StreamWriter streamWriter in dynamicStreamWriter)
            {
                streamWriter.Close();
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

            /*
            StreamReader firstStreamReader = new StreamReader("default0.xml");
            StreamReader SecondStreamReader = new StreamReader("default1.xml");
            StreamWriter objectfilewriter = new StreamWriter("default.xml");
            int ccount = 0;
            objectfilewriter.Write(firstStreamReader.ReadToEnd());
            foreach (KeyValuePair<string, string> singleRule in allRules)
            {
                ccount++;
                if(string.Compare(singleRule.Key.ToString(),singleRule.Value.ToString()) != 0 && ccount>80000)
                {
                    var string1 =
                    @"
            <Replacement>"+"\n";
                string1+=@"<Find>"+singleRule.Key+"</Find>"+"\n";
                string1 += "<Replace>" + singleRule.Value + "</Replace>"+"\n";
               //  string1+=@"<Find>वालिबल</Find>"+"\n";
               //  string1 += "<Replace>भलिबलc</Replace>"+"\n";
                string1+=@"<Comment />
                <IsRegex>false</IsRegex>
                <Enabled>true</Enabled>
                <Minor>false</Minor>
                <BeforeOrAfter>false</BeforeOrAfter>
                <RegularExpressionOptions>None</RegularExpressionOptions>
            </Replacement > ";
                objectfilewriter.Write(string1.ToString());
               
                }
                
                
            }
           

            objectfilewriter.Write(SecondStreamReader.ReadToEnd());
            objectfilewriter.Close();
             * 
             */
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
        /// I had a method called Translate which returned string earlier, since I had to work with threads, it wanted me to use a void function
        /// And after that it wanted me to return the translated string to be returned through a delegate .. 
        /// 
        /// OnFeedback event is triggered for updating the progressbar 
        /// OnTranslationComplete event is triggered for returning the translated string.
        /// 
        /// Haven't faced any bugs in threads yet.. though I didn't do any serious threading before.. lets see.. 
        /// </summary>
        public void TranslateVoid()
        {


            LoadVerbList(this.verbListFileName, true);
            LoadNounList(this.nounListFileName, true);
            LoadAdjectiveList(this.adjectiveListFileName);
            LoadPronounList(this.pronounListFileName);
            LoadRules(this.ruleFileName);
            LoadUsersCustomRules(this.usersCustomFileName);

            string translatedString = originalString; //creating a copy
            if (string.IsNullOrEmpty(originalString))
            {
                //throw new Exception("Original String is null"); //if there is nothing for translation, throw an exception
                MessageBox.Show("There is nothing to translate, please copy some text in hindi tab");

            }
            if (AllRules.Count() <= 0)
            {
                //throw new Exception("There are no rules for translation"); //if there are no rules for translation, throw an exception
                MessageBox.Show(
                    "There are no rules for translation, please press control + space and then add some rules for translations");
            }
            else
            {
                var thisCounter = 0;
                var maxValue = AllRules.Count();
                var percentComplete = 0;
                foreach (KeyValuePair<string, string> rule in AllRules) //iterate for all rules in this file
                {

                    thisCounter++;
                    if (maxValue > 0)
                    {
                        percentComplete = ((thisCounter * 100) / maxValue);
                    }
                    else
                    {
                        percentComplete = 5;
                    }

                    if (OnFeedback != null)
                        OnFeedback(percentComplete);

                    // Do some more work
                    //                    if (OnFeedback != null && AllRules.Count>0)
                    //                    {
                    //
                    //                        int currentStatus = (thisCounter/AllRules.Count)*100;
                    //                        OnFeedback(thisCounter.ToString(), currentStatus);
                    //
                    //                    }

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

                    if(rule.Key.Contains("\\r")){
                        Trace.Write("\n\n\n Found \\r"); 
                        Trace.Write("\n Found " + rule.Key + " : "+rule.Value +"\n");
                    }
                }
            }

            if (OnTranslationComplete != null)
            {
                OnTranslationComplete(translatedString);
            }
            MessageBox.Show("Complete");

        }




        #endregion





    }
}

/*
 * 
 * nouns=>while making plural: 
//nouns which already contain या or ा ि
// ि or ी = यों 
// ा = ओं
//eg: 
//सुरक्षाों को|सुरक्षाहरु लाइ
//मां ों को| आमाहरु लाइ
//प्रजातिों को|प्रजातिहरु लाइ
//बिल्लिों को|बिरालोहरु लाइ
//उत्तराधिकारिों को|उत्तराधिकारिहरु लाइ
//कास्त्रोों को|कास्ट्रोहरु लाइ
//नेताों को|नेताहरु लाइ
//जन्तुों को|जन्तुहरु लाइ
//हादसाों को|घटनाहरु लाइ
//कंधेओं को|काँधहरु लाइ 
//फार्मओं को|फारमहरु लाइ 
//
//
//
//प्रकाशनएँ को|प्रकाशनहरु लाइ
//आवश्यकताएँ को|आवश्यकताहरु लाइ
//वेबसाइटएँ को|वेबसाइटहरु लाइ
//अंदाजाएँ को|अन्दाजहरु लाइ
//डिजाइनएँ को|डिजाइनहरु लाइ
//देरीएँ को|ढिलाइहरु लाइ
//फ़ायदाएँ को|फाइदाहरु लाइ
//निर्माताएँ को|निर्माताहरु लाइ
//नियंत्रकएँ को|नियन्त्रकहरु लाइ
//वोल्टेजएँ को|भोल्टेजहरु लाइ
//प्रणालिएँ को|प्रणालिहरु लाइ
//संस्करणएँ को|संस्करणहरु लाइ
//डेस्कटॉपएँ को|डेस्कटपहरु लाइ
//सोवियतएँ को|सोभियतहरु लाइ
//जर्मनीएँ को|जर्मनीहरु लाइ
//आस्ट्रियाएँ को|अस्ट्रियाहरु लाइ
//फ्रांसएँ को|फ्रान्सहरु लाइ
//वाहनएँ को|वाहनहरु लाइ
//योजनाएँ को|योजनाहरु लाइ
//
//
//
//
//
//रचनायें को|रचनाहरु लाइ
//सलाहयें को|सल्लाहहरु लाइ
//इसयें को|यसहरु लाइ
//शाखायें को|शाखाहरु लाइ
//भाषायें को|भाषाहरु लाइ
//पेशाबयें को|पिशाबहरु लाइ


 * 
 */
