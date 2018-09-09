using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamplePlugin
{
    public class Word
    {
        private string _wordContent;
        public Word()
        {
            _wordContent = "";
        }
        public Word(string content)
        {
            _wordContent = content;
            
        }

        public string WordContent
        {
            get { return _wordContent; }
        }

        /// <summary>
        /// Returns an array of Words when we pass an array of Words
        /// </summary>
        /// <param name="arrayOfWords"></param>
        /// <returns></returns>
        public static List<Word> AddWords(string[] arrayOfWords)
        {
            List < Word > returnvalue = new List<Word>();

            foreach (string singleWord in arrayOfWords)
            {
                returnvalue.Add(new Word(singleWord));    
            }
            return returnvalue;
        }
    }
}
