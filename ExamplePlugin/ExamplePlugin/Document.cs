using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamplePlugin
{
    /// <summary>
    /// This class is meant to handle the document layout
    /// It will contain paragraphs
    /// </summary>
    class Document
    {
        private string _documentContent="";
        public List<Paragraph> AllParagraphs = new List<Paragraph>();

        /// <summary>
        /// Splits paragraphs
        /// </summary>
        /// <param name="text"></param>
        public void SplitIntoAllParagraphs(string text)
        {
            string[] splittedParagraphs = text.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            for(int i=0;i<splittedParagraphs.Length;i++)
            {
                AllParagraphs.Add(new Paragraph(splittedParagraphs[i]));   
            }
            
        }

        /// <summary>
        /// TODO: Rebuild Paragraphs
        /// </summary>
        public string ReBuildParagraphs()
        {
            StringBuilder paragraphToReturn = new StringBuilder();
            foreach (Paragraph allParagraph in AllParagraphs)
            {
                paragraphToReturn.Append(allParagraph.RebuildParagraphFromLines());
            }       
            return paragraphToReturn.ToString();
        }
    }
}
