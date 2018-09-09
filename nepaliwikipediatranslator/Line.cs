using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NepaliWikiPediaTranslator
{
    public class Line
    {
        private string _lineContent;
        public List<Sentence> SentencesInALine;
        /// <summary>
        /// Constructor
        /// </summary>
        public Line()
        {
            _lineContent = "";
            SentencesInALine = new List<Sentence>();
        }

        /// <summary>
        /// Creating a line
        /// </summary>
        /// <param name="content"></param>
        public Line(string content)
        {
            _lineContent = content;
            string sentenceSeparator = "।";
            string[] splittedSentence = _lineContent.Split(new string[] { sentenceSeparator }, StringSplitOptions.RemoveEmptyEntries);
            
            if(SentencesInALine ==null) SentencesInALine = new List<Sentence>();
            SentencesInALine = Sentence.AddSentences(splittedSentence,sentenceSeparator);
        }


        /// <summary>
        /// Returns line
        /// </summary>
        /// <param name="arrayOfLines"></param>
        /// <returns></returns>
        public static List<Line> AddLines(string[] arrayOfLines)
        {
            List<Line> returnvalue = new List<Line>();

            foreach (string singleLine in arrayOfLines)
            {
                returnvalue.Add(new Line(singleLine));
            }
            return returnvalue;
        }

        
        /// <summary>
        /// Rebuilding line from sentences
        /// </summary>
        /// <returns></returns>
        public string RebuildLineFromSentences()
        {
            StringBuilder wordsToReturn = new StringBuilder();
            foreach (Sentence sentence in SentencesInALine)
            {
                wordsToReturn.Append(sentence.RebuildSentenceFromWords()+sentence.SentenceSeparator);
            }
            return wordsToReturn.ToString();

        }



        
    }
}
