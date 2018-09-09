using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamplePlugin
{
    public class Sentence
    {
        private string _sentenceContent;
        public List<Word> WordsInASentence;
        public string SentenceSeparator;
        public string SentenceTab;
        /// <summary>
        /// Constructor
        /// </summary>
        public Sentence()
        {
            _sentenceContent = "";
            WordsInASentence = new List<Word>();
            SentenceSeparator = "";
            SentenceTab = "";
        }

        /// <summary>
        /// Creating a line
        /// </summary>
        /// <param name="content"></param>
        public Sentence(string content)
        {
            _sentenceContent = content;
            string[] splittedWords = _sentenceContent.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            
            if(WordsInASentence ==null) WordsInASentence = new List<Word>();
            WordsInASentence = Word.AddWords(splittedWords);
        }


        /// <summary>
        /// Returns line
        /// </summary>
        /// <param name="arrayOfSentences"></param>
        /// <returns></returns>
        public static List<Sentence> AddSentences(string[] arrayOfSentences, string separator)
        {
            List<Sentence> returnvalue = new List<Sentence>();

            foreach (string singleSentence in arrayOfSentences)
            {
                var sentenceToAdd = new Sentence(singleSentence);
                sentenceToAdd.SentenceSeparator = separator;
                returnvalue.Add(sentenceToAdd);
            }
            return returnvalue;
        }

        
        public string RebuildSentenceFromWords()
        {
            StringBuilder sbWordsToReturn = new StringBuilder();
            int i = 0;
            foreach (Word word in WordsInASentence)
            {
                if (i > 0) { sbWordsToReturn.Append(" ");  } //This is for adding a space before starting a new sentence.

                sbWordsToReturn.Append(word.WordContent);
                
                i++;
            }
            return sbWordsToReturn.ToString();

        }


    }
}
