using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using WikiFunctions.API;

namespace NepaliWikiPediaTranslator.AWBot
{
    public partial class LoginForm : Form
    {

        String credentialSettingsXml = "AWBcredentials.xml";
        XmlDocument credentialSettings = new XmlDocument();
        CultureInfo currentCulture;
        private SessionsManager sessionsManager;
        //private ApiEdit editor;

//        public LoginForm()
//        {
//
//
//            
//            InitializeComponent();
//            this.WikiProjectList.DataSource = mediaWikiVars;
//            LoadCredentialsFromXML();
//            
//        }
        public LoginForm(SessionsManager  paramsessionsManager)
        {
            
            
            InitializeComponent();
            
            this.WikiProjectList.DataSource = mediaWikiVars;
            this.sessionsManager = paramsessionsManager;
            LoadCredentialsFromXML();

            
        }

        private void Login_Click(object sender, EventArgs e)
        {
            //ApiEdit apieditor= new ApiEdit("http://" + WikiProjectList.SelectedItem.ToString() +".wikipedia.org/w/");
             if(sessionsManager ==null)
             {
                 MessageBox.Show("sessionmanager is null");
                 return;
             }
            
            sessionsManager.LoginData.editor = new ApiEdit("http://" + WikiProjectList.SelectedItem.ToString() +".wikipedia.org/w/");

            try
            {

                sessionsManager.LoginData.editor.Login(User, Password.Text);
                sessionsManager.LoginData.SetUserNameAndPassword(User, Password.Text);
                sessionsManager.LoginData.DestinationWiki = WikiProjectList.SelectedItem.ToString();
                
                if(RememberAuthentication)
                {CreateXmlFileToSaveCredentials();}


            }
            catch (LoginException)
            {
                Console.WriteLine(" failed logging in " );
                MessageBox.Show("Failed logging in");
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
        {//MessageBox.Show((r.ChildNodes[0].Attributes[0].Value) + "\n" + (r.ChildNodes[1].Attributes[0].Value) + "\n" + (r.ChildNodes[2].Attributes[0].Value));
            if (!File.Exists(credentialSettingsXml)){
                
                CreateXmlFileToSaveCredentials();
            }
            else{
                try{
                    ReadCredentialsFromXmlFile();
                }
                catch (XmlException exception){
                   MessageBox.Show(exception.ToString());
                   File.Delete(credentialSettingsXml);
                }
            }
        }

        private void ReadCredentialsFromXmlFile()
        {
            credentialSettings.Load(credentialSettingsXml);
            XmlElement r = credentialSettings.DocumentElement;
            this.Username.Text = (r.ChildNodes[0].Attributes[0].Value);
            this.Password.Text = (r.ChildNodes[1].Attributes[0].Value);
            this.WikiProjectList.SelectedItem = (r.ChildNodes[2].Attributes[0].Value);
        }

        private void CreateXmlFileToSaveCredentials()
        {
            XmlElement r;
            StreamWriter writer = new StreamWriter(credentialSettingsXml);
            if(credentialSettings.DocumentElement==null)
            {
                 r= credentialSettings.CreateElement("Login");
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
                r = credentialSettings.DocumentElement;

                (r.ChildNodes[0].Attributes[0].Value) = this.Username.Text;
                (r.ChildNodes[1].Attributes[0].Value) = this.Password.Text;
                (r.ChildNodes[2].Attributes[0].Value) = this.WikiProjectList.SelectedItem.ToString();
                
            }

            
                XmlWriter xmlWriter = XmlWriter.Create(writer);
                xmlWriter.WriteStartDocument();

                //r.WriteContentTo(xmlWriter);

                r.WriteTo(xmlWriter);
                xmlWriter.WriteEndDocument();

                xmlWriter.Close();
                writer.Close();

            

                
           
        }
    }
}
