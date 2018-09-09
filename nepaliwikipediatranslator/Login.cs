using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WikiFunctions.API;

namespace NepaliWikiPediaTranslator
{
    public class Login
    {
        /// <summary>
        /// Source from where articles might be copied
        /// </summary>
        private string _sourceWiki = "en";

        /// <summary>
        /// Destination wiki from where articles might be pasted or translated
        /// </summary>
        private string _destinationWiki = "ne";


        public string UserName { get; set; }
        public string Password { get; set; }

        public bool HasLoggedInOnce { get; set; }
        
        public string SourceWiki {
            get { return _sourceWiki; }
            set
            {
                _sourceWiki = value;
                editor = new ApiEdit("http://" + _sourceWiki + ".wikipedia.org/w");
            }
        }
        public string DestinationWiki
        {
            get { return _destinationWiki; }
            set
            {
                _destinationWiki = value;
                bot = new ApiEdit("http://" + _destinationWiki + ".wikipedia.org/w");
            }
        }

        /// <summary>
        /// This is a stub (rememberme) might be used someday
        /// </summary>
        public bool RememberMe { get; set; }

        public WikiFunctions.API.ApiEdit editor;
        public WikiFunctions.API.ApiEdit bot;

        public Login()
        {
            editor = new ApiEdit("http://"+_sourceWiki+".wikipedia.org/w");
            bot = new ApiEdit("http://" + _destinationWiki + ".wikipedia.org/w");
        }

        /// <summary>
        /// Sets username and password 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void SetUserNameAndPassword(string userName, string password)
        {

            this.UserName = userName;
            this.Password = password;
            HasLoggedInOnce = true;
        }

        
    }
}
