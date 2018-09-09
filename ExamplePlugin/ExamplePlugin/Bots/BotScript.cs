// Write your own bot scripts and functions in this file.
// Run "Compile & Run.bat" file - it will compile this file as executable and launch it.
// Compiled XML autodocumentation for DotNetWikiBot namespace is available as "Documentation.chm".

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DotNetWikiBot;

namespace ExamplePlugin.Bots
{
    public class BotScript : Bot
    {
        private Site site;
        //I need these delegates to give feedback for the progressbar , the progressbar will be updated once it is completed.
        public delegate void ProgressFeedbackDelgateForBot(int percent);
        public event ProgressFeedbackDelgateForBot OnFeedbackForBot;

        //I need these delegates to the main window with the translated string..., the translated text will be provided to the main thread once it is completed
        public delegate void TranslationCompleteDelegateForBot(string message);
        public event TranslationCompleteDelegateForBot OnBotProcessComplete;

        public delegate void CallBackDelegateForBot(string message);
        public static event CallBackDelegateForBot OnBotConsoleWrite;
        public static RichTextBox botScriptRichTextBox;

        public void SetSite(Site paramSite)
        {
            // Write your own function here
            this.site = paramSite;
        }
        public BotScript(Site _site)
        {
            site = _site;
        }
       

        public BotScript(){}
        

        public bool IsAuthenticated
        {
            get { return this.site != null; }
             
        }
        
        public Site GetSite
        {
            get { return this.site; }
        }

        /// <summary>
        /// Login("http://ne.wikipedia.org", "UserName", "Password");
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Login(string url, string username, string password)
        {
            { site = new Site(url, username, password); }
        }

        public string GetContent(string pageName)
        {
            Page p = new Page(site, pageName);
            p.LoadEx();

            if (p.Exists())
                return p.text;
            else return "Page Not Found";
        }
        /// The entry point function. Start coding here.
        public void RunTest()
        {
            // Here are some code examples:
            if(site ==null)
            {site = new Site("http://ne.wikipedia.org", "testUser", "testPassword");}
            //Site site = new Site("http://mywikisite.com", "YourBotLogin", "YourBotPassword");
            //Site site = new Site("http://sourceforge.net/apps/mediawiki/YourProjectName/",
            //"YourSourceForgeLogin", "YourSourceForgePassword");

		
            Page p = new Page(site, "Wikipedia:Sandbox");
            p.LoadEx();

            if (p.Exists())
                Console.WriteLine(p.text);
            p.SaveToFile("dir\\file.txt");
            p.LoadFromFile("dir\\file.txt");
            p.ResolveRedirect();
            site.ShowNamespaces();
            Console.WriteLine(p.GetNamespace());
            p.text = "new text";
            Bot.editComment = "saving test";
            Bot.isMinorEdit = true;
            p.Save();

            /**/

            /*
		string[] arr = {"Art", "Poetry", "Cinematography", "Camera", "Image"};
		PageList pl = new PageList(site, arr);
		pl.LoadEx();
		pl.FillFromAllPages("Sw", 0, true, 100);
		pl.FillFromFile("dir\\text.txt");
		pl.FillFromCategory("Category:Cinematography");
		pl.FillFromAllPageLinks("Cinematography");
		pl.FillFromLinksToPage("Cinematography");
		pl.RemoveEmpty();
		pl.RemoveDisambigs();
		pl.ResolveRedirects();
		Console.WriteLine(pl[2].text);
		pl[1].text = "#REDIRECT [[Some Page]]";
		pl.FilterNamespaces(new int[] {0,3});
		pl.RemoveNamespaces(new int[] {2,4});
		pl.SaveTitlesToFile("dir\\text.txt");
		pl.Clear();
		Bot.editComment = "my edit comment";
		isMinorEdit = true;
		pl.Save();
		/**/
        }

        public List<string> GetArticleForSpeedyDeletion(string categoryToDelete)
        {
         //   Site enWP = new Site("http://ne.wikipedia.org", "YOURUSERNAMEGOESHERE", "YOURPASSWORDGOESHERE");
            PageList pl = new PageList(site);
            PageList plSave = new PageList(site);
            // Pages to delete
            pl.FillFromCategory(categoryToDelete);//Donot include category prefix
            pl.LoadEx();

            List<string> articleList = new List<string>();


            
            
            foreach (Page i in pl)
            {
                articleList.Add(i.title);
            }
            return articleList;

            

        }
        public void DeleteConfirmed(List<string> deleteList)
        {
            if(site==null)
                throw new Exception("not logged in");
            foreach (string articleMarkedForDeletion in deleteList)
            {
                 
                Page page  = new Page(site,articleMarkedForDeletion);

                page.Delete("This article was marked for deletion!! ");
            }
//            foreach (Page i in pl)
//                if (i.text.Contains("delete"))
//                {
//                    Console.Write("Would you like to delete " + i.title + " (Y/N)? ");
//                    if (Console.ReadLine().ToUpper() == "Y")
//                    {
//                        i.Delete("Category Marked for Deletion");
//                    }
//                }
        }

        public List<string> WelcomeNewUsers()
        {
            List<string> usersNewlyWelcomed = new List<string>();
            PageList pl = new PageList(site);
            PageList plSave = new PageList(site);
            pl.FillFromCustomLog("विशेष:Log/newusers", "", "", 100);
            pl.Load();

            var count = 0;
            foreach (Page i in pl)
            {
                usersNewlyWelcomed.Add(i.title);
                count++;
                //if(!i.Exists()){
                if (i.text.Trim() == "")
                {
                    //i.text = "{{welcome}} -~~~~~";
                    //plSave.Add(i);
                    usersNewlyWelcomed.Add(i.title);
                    count++;
                }
                if(count>10)
                    break;
                //}
            //plSave.SaveSmoothly(5, "Welcome!", false);
            
            }
            return usersNewlyWelcomed;
        }

        public void SaveArticle(string articleName,string content)
        {
            if (site == null)
                throw new Exception("not logged in");
            Page p = new Page(site,articleName);
            p.Save(content);
        }

        private static void BotWriterEventHandler(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString()+" "+ e.ToString());
            
        }

        public static void FindAndReplace(string findText, string replaceText, string wikipediaDomain, string userName, string passWord)
        {
             if(botScriptRichTextBox == null) botScriptRichTextBox = new RichTextBox();
              
            // Firstly make Site object, specifying site's URL and your bot account
            Site enWiki = new Site(wikipediaDomain, userName, passWord);

            botScriptRichTextBox.TextChanged += new EventHandler(BotWriterEventHandler);
            // Then make Page object, specifying site and page title in constructor
//            Page p = new Page(enWiki, "Art");
            // Load actual page text from live wiki
//            p.Load();
            // Add "Visual arts" category link to "Art" page's text
//            p.AddToCategory("Visual arts");
            // Save "Art" article's text back to live wiki with specified comment
//            p.Save("comment: category link added", true);

            // Make empty PageList object, representing collection of pages
            PageList pl = new PageList(enWiki);
            // Fill it with 100 pages, where "nuclear disintegration" is mentioned
             pl.FillFromGoogleSearchResults(findText, 100);
            //pl.FillFromSearchResults(findText,100);
            // Load texts and metadata of all found pages from live wiki
            pl.LoadEx();
            // Now suppose, that we must correct some typical mistake in all our pages
            foreach (Page i in pl)
                // In each page we will replace one phrase with another
                i.text = i.text.Replace(findText, replaceText);
            // Finally we'll save all changed pages to wiki with 5 seconds interval			
            pl.SaveSmoothly(5, ("comment: mistake autocorrection "+findText+" with "+replaceText), true);

            // Now clear our PageList so we could re-use it
            pl.Clear();
            // Fill it with all articles in "Astronomy" category and it's subcategories
             pl.FillFromCategoryTree("Astronomy");
            // Download and save all PageList's articles to specified local XML file
            pl.SaveXMLDumpToFile("Dumps\\ArticlesAboutAstronomy.xml");		
        }


        public void FindAndReplaceThread()
        {
            string findText = "अप्रैल";
            string replaceText = "अप्रिल";
            string wikipediaDomain="ne.wikipedia.org";
            string userName="RajeshBot";
            string passWord="windows1";
            // Firstly make Site object, specifying site's URL and your bot account
            if(site == null)
            {
                throw new Exception("Not logged in ");
            }
            Site enWiki = this.site;
           // Site enWiki = new Site(wikipediaDomain, userName, passWord);

            // Then make Page object, specifying site and page title in constructor
            //            Page p = new Page(enWiki, "Art");
            // Load actual page text from live wiki
            //            p.Load();
            // Add "Visual arts" category link to "Art" page's text
            //            p.AddToCategory("Visual arts");
            // Save "Art" article's text back to live wiki with specified comment
            //            p.Save("comment: category link added", true);

            // Make empty PageList object, representing collection of pages
            PageList pl = new PageList(enWiki);
            // Fill it with 100 pages, where "nuclear disintegration" is mentioned
            pl.FillFromGoogleSearchResults(findText, 100);
            //pl.FillFromSearchResults(findText,100);
            // Load texts and metadata of all found pages from live wiki
            pl.LoadEx();
            // Now suppose, that we must correct some typical mistake in all our pages
            var thisCounter = 0;
            var maxValue = pl.Count();
            var percentComplete = 0;
            foreach (Page i in pl)// In each page we will replace one phrase with another
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

                if (OnFeedbackForBot != null)
                    OnFeedbackForBot(percentComplete);
                    
                i.text = i.text.Replace(findText, replaceText);
                


            }
            // Finally we'll save all changed pages to wiki with 5 seconds interval			
            pl.SaveSmoothly(5, ("comment: mistake autocorrection " + findText + " with " + replaceText), true);

            // Now clear our PageList so we could re-use it
            pl.Clear();
            // Fill it with all articles in "Astronomy" category and it's subcategories
            pl.FillFromCategoryTree("Astronomy");
            // Download and save all PageList's articles to specified local XML file
            pl.SaveXMLDumpToFile("Dumps\\ArticlesAboutAstronomy.xml");

            if (OnBotProcessComplete != null)
            {
                OnBotProcessComplete("Completed");
            }
        }
    }
}
namespace MyThreader.IO
{
    public class BotConsole  
    {
        private StringRedir RedirConsole;
        private TextWriter ConsoleWriter;
        public RichTextBox rtbOutput = new RichTextBox();

        public BotConsole()
        {
            // Here we redirect Console.WriteLine to a RichTextBox control.
            ConsoleWriter = Console.Out; // Save the current console TextWriter.
            RedirConsole = new StringRedir(ref rtbOutput);
            Console.SetOut(RedirConsole); // Set console output to the StringRedir class. 
        }
    }
    public class StringRedir : StringWriter
    { // Redirecting Console output to RichTextBox
        private RichTextBox outBox;
        delegate void Writeline(string x);

        public StringRedir(ref RichTextBox textBox)
        {
             
            outBox = textBox;
        }

        public override void Write(string value)
        {
            // Centralize output in ONE function : this one
            outBox.Text += value;
            outBox.Refresh();
        }

        public override void WriteLine(string value)
        {
            Write(value + "\n");
        }

        public override void Write(string format, params object[] args)
        {
            Write(string.Format(format, args));
        }

        public override void WriteLine(string format, params object[] args)
        {
            Write(string.Format(format, args) + "\n");
        }

    }
}