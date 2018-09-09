using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NepaliWikiPediaTranslator
{
    /// <summary>
    /// This class will contain paragraphs
    /// Each paragraphs will contain lines separated by full stops in English or purnabirams "।" in Nepali 
    /// </summary>
    public class Paragraph
    {
        private string _paragraphContent="";
        /// <summary>
        /// Constructor
        /// </summary>
        public Paragraph()
        {
            _paragraphContent = "";
        }

        /// <summary>
        /// setting a paragraph
        /// </summary>
        /// <param name="content"></param>
        public Paragraph(string content)
        {
            _paragraphContent = content;
            string[] splittedLines = _paragraphContent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            AllLines = Line.AddLines(splittedLines);

        }
         
        public List<Line> AllLines = new List<Line>();


        private string _topSeparator;
        
        public string RebuildParagraphFromLines()
        {
            StringBuilder lineToReturn = new StringBuilder();
            foreach (Line line in AllLines)
            {
                lineToReturn.Append(line.RebuildLineFromSentences()+"\n");
            }
            return lineToReturn.ToString();
        }

    }
}
