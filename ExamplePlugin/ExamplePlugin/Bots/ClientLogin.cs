using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DotNetWikiBot;

namespace ExamplePlugin.Bots
{
    public partial class ClientLogin : Form
    {
        String credentialSettingsXml = "credentials.xml";
        XmlDocument credentialSettings = new XmlDocument();
        CultureInfo currentCulture;


        private BotScript logonBot = new BotScript();

        public bool IsAuthenticated
        {
            get
            {
                if(logonBot==null) return false;
                return this.logonBot.IsAuthenticated;
            }
        }

        public Site logonBotSite
        {
            get
            {
               return this.logonBot.GetSite;

            }
        }
         
        public BotScript LogOnBot
        {
            get { return this.logonBot; }
        }
         

        public ClientLogin()
        {
            InitializeComponent();
            this.WikiProjectList.DataSource = mediaWikiVars;
        }
        

        private void Login_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(WikiProjectList.SelectedItem.ToString())|string.IsNullOrEmpty(User)|string.IsNullOrEmpty(Password.Text))
                return;
            try
            {
                logonBot.Login("http://" + WikiProjectList.SelectedItem.ToString() + ".wikipedia.org", User, Password.Text);
            }
            catch (WikiBotException ex)
            {

                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK);
            }
            catch(Exception ea)
            {
                MessageBox.Show("Error",ea.ToString());
            }
            
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// returns the user name 
        /// </summary>
        public string User
        {
            get
            {
                return this.Username.Text;
            }
        }

        /// <summary>
        /// indicates if the checkbox to remember the 
        /// authentication token is shown. Remembering the 
        /// token is then up to the calling application
        /// </summary>
        public bool ShowRememberAuthentication
        {
            get
            {
                return this.RememberToken.Visible;
            }
            set
            {
                this.RememberToken.Visible = value;
            }
        }
        /// <summary>
        /// returns if the checkbox to remember the authentication
        /// was checked or not
        /// </summary>
        public bool RememberAuthentication
        {
            get
            {
                return this.RememberToken.Checked;
            }
        }

        #region mediawikivars
        private string[] mediaWikiVars = new string[] {
				"ne","hi","en","ace", "af", "ak", "als", "am", "ang", "ab", "ar", "an", "arc",
				"roa-rup", "frp", "arz", "as", "ast", "gn", "av", "ay", "az", "id",
				"ms", "bg", "bm", "zh-min-nan", "nan", "map-bms", "jv", "su", "ba",
				"be", "be-x-old", "bh", "bcl", "bi", "bn", "bo", "bar", "bs", "bpy",
				"br", "bug", "bxr", "ca", "ceb", "ch", "cbk-zam", "sn", "tum", "ny",
				"cho", "chr", "co", "cy", "cv", "cs", "da", "dk", "pdc", "de", "nv",
				"dsb", "na", "dv", "dz", "mh", "et", "el", "eml",  "myv", "es",
				"eo", "ext", "eu", "ee", "fa", "hif", "fo", "fr", "fy", "ff", "fur",
				"ga", "gv", "sm", "gd", "gl", "gan", "ki", "glk", "got", "gu", "ha",
				"hak", "xal", "haw", "he",  "ho", "hsb", "hr", "hy", "io",
				"ig", "ii", "ilo", "ia", "ie", "iu", "ik", "os", "xh", "zu", "is",
				"it", "ja", "ka", "kl", "kr", "pam", "krc", "csb", "kk", "kw", "rw",
				"ky", "rn", "sw", "km", "kn", "ko", "kv", "kg", "ht", "ks", "ku",
				"kj", "lad", "lbe", "la", "lv", "to", "lb", "lt", "lij", "li", "ln",
				"lo", "jbo", "lg", "lmo", "hu", "mk", "mg", "mt", "mi", "cdo",
				"mwl", "ml", "mdf", "mo", "mn", "mr", "mus", "my", "mzn", "nah",
				"fj",  "nl", "nds-nl", "cr", "new", "nap", "ce", "pih", "no",
				"nb", "nn", "nrm", "nov", "oc", "mhr", "or", "om", "ng", "hz", "uz",
				"pa", "pag", "pap", "pi", "pcd", "pms", "nds", "pnb", "pl", "pt",
				"pnt", "ps", "aa", "kaa", "crh", "ty", "ksh", "ro", "rmy", "rm",
				"qu", "ru", "sa", "sah", "se", "sg", "sc", "sco", "sd", "stq", "st",
				"tn", "sq", "si", "scn", "simple", "ss", "sk", "sl", "cu", "szl",
				"so", "ckb", "srn", "sr", "sh", "fi", "sv", "ta", "tl", "kab",
				"roa-tara", "tt", "te", "tet", "th", "ti", "vi", "tg", "tokipona",
				"tp", "tpi", "chy", "ve", "tr", "tk", "tw", "udm", "uk", "ur", "ug",
				"za", "vec", "vo", "fiu-vro", "wa", "vls", "war", "wo", "wuu", "ts",
				"yi", "yo", "diq", "zea", "zh", "zh-tw", "zh-cn", "zh-classical",
				"zh-yue", "bat-smg"
			};
        #endregion

        private void LoadCredentialsFromXML()
        {

            if (!File.Exists(credentialSettingsXml))
            {
                XmlElement r = credentialSettings.CreateElement("Login");
                credentialSettings.AppendChild(r);
                XmlElement el;

                el = credentialSettings.CreateElement("LoginUserName");
                el.SetAttribute("UserName", this.User);
                r.AppendChild(el);

                el = credentialSettings.CreateElement("LoginPassword");
                el.SetAttribute("Password", Password.Text);
                r.AppendChild(el);

                el = credentialSettings.CreateElement("HomeWiki");
                el.SetAttribute("Domain", WikiProjectList.SelectedItem.ToString());
                r.AppendChild(el);

                
            }
            else
            {
                credentialSettings.Load(credentialSettingsXml);
                XmlElement r = credentialSettings.DocumentElement;
                this.Username.Text = (r.ChildNodes[0].Attributes[0].Value);
                this.Password.Text = (r.ChildNodes[1].Attributes[0].Value);
                this.WikiProjectList.SelectedItem = (r.ChildNodes[2].Attributes[0].Value);
                //favoritesPanel.Visible = (r.ChildNodes[3].Attributes[0].Value.Equals("True"));
            }
             
        }

    }
}
