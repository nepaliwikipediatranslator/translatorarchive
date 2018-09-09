
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WikiFunctions.Plugin;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WikiFunctions;
using WikiFunctions.Parse;
using WikiFunctions.AWBSettings;
using System.Threading;

namespace ExamplePlugin
{
    public sealed class TranslatorExamplePlugin : IAWBPlugin
    {
        #region translator loader
        public const string RuleFileName = "HindiToNepali.txt";
        //public const string KakharaFileName = "Kakhara.txt";
        public const string VerbListFileName = "verblist.txt";
        public const string NounListFileName = "nounlist.txt";
        public const string AdjectiveListFileName = "adjectivelist.txt";
        public const string PronounListFileName = "pronounlist.txt";
        public const string UsersCustomFileName = "Rules.db";
        private PostProcessor pr;
        private RuleEditor ruleEditor;
        private RuleEditor verbEditor;
        private RuleEditor nounEditor;
        private RuleEditor adjectiveEditor;
        private RuleEditor pronounEditor;
        private RuleEditor UsersCustomEditor;

        private SearchBox searchBox;


        private Thread translatorThread;



        private string lockPBVar = "myPBLock";
        #endregion 


        private readonly ToolStripMenuItem pluginenabledMenuItem = new ToolStripMenuItem("Translator");
        private readonly ToolStripMenuItem pluginconfigMenuItem = new ToolStripMenuItem("Configuration");
        private readonly ToolStripMenuItem aboutMenuItem = new ToolStripMenuItem("About the TranslatorExample plugin");
        internal static IAutoWikiBrowser AWB;
        internal static TranslatorExampleSettings Settings = new TranslatorExampleSettings();

        public void Initialise(IAutoWikiBrowser sender)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");

            AWB = sender;

            // Menuitem should be checked when TranslatorExample plugin is active and unchecked when not, and default to not!
            pluginenabledMenuItem.CheckOnClick = true;
            PluginEnabled = Settings.Enabled;

            pluginconfigMenuItem.Click += ShowSettings;
            pluginenabledMenuItem.CheckedChanged += PluginEnabledCheckedChange;
            aboutMenuItem.Click += AboutMenuItemClicked;
            pluginenabledMenuItem.DropDownItems.Add(pluginconfigMenuItem);

            sender.PluginsToolStripMenuItem.DropDownItems.Add(pluginenabledMenuItem);
            sender.HelpToolStripMenuItem.DropDownItems.Add(aboutMenuItem);

            ruleEditor = new RuleEditor();
            ruleEditor.SetFileName(RuleFileName);

            verbEditor = new RuleEditor();
            verbEditor.SetFileName(VerbListFileName);

            nounEditor = new RuleEditor();
            nounEditor.SetFileName(NounListFileName);

            adjectiveEditor = new RuleEditor();
            adjectiveEditor.SetFileName(AdjectiveListFileName);

            pronounEditor = new RuleEditor();
            pronounEditor.SetFileName(PronounListFileName);

            UsersCustomEditor = new RuleEditor();
            UsersCustomEditor.SetFileName(UsersCustomFileName);

        }

        public string Name
        { get { return "TranslatorExample-Plugin"; } }

        public string WikiName
        {
            get
            {
                return "[[WP:TranslatorExample|TranslatorExample]] Plugin version " +
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
        private void C_TranslationComplete(string message) {
            //MessageBox.Show("Translation Complete");
            //Debug.

        }
        public string ProcessArticle(IAutoWikiBrowser sender, IProcessArticleEventArgs eventargs)
        {
            //MessageBox.Show("Procsssing Article");
            //If menu item is not checked, then return
            if (!PluginEnabled)
            {
                eventargs.Skip = false;
                return eventargs.ArticleText;
            }
            string text = eventargs.ArticleText;
            string removed = "", replaced = "";


            pr = new PostProcessor(RuleFileName, VerbListFileName, NounListFileName, AdjectiveListFileName, PronounListFileName, UsersCustomFileName);
            pr.LoadTextToTranslate(text);

           // translatorThread = new Thread(new ThreadStart(pr.TranslateVoid));
            text = pr.Translate();
            //translatorThread.Name = "TranslatorThread"; //Name of the thread
            //translatorThread.Start(); //starting translation. 
            //pr.OnTranslationComplete += new PostProcessor.TranslationCompleteDelegate(C_TranslationComplete);
            //MessageBox.Show("Translation Complete");
            #region abcd

            //

            //foreach (KeyValuePair<string, string> p in Settings.Categories)
            //{
            //    bool noChange;

            //    if (p.Value.Length == 0)
            //    {
            //        text = Parsers.RemoveCategory(p.Key, text, out noChange);
            //        if (!noChange)
            //        {
            //            if (!string.IsNullOrEmpty(removed))
            //            {
            //                removed += ", ";
            //            }

            //            removed += Variables.Namespaces[Namespace.Category] + p.Key;
            //        }
            //    }
            //    else
            //    {
            //        text = Parsers.ReCategoriser(p.Key, p.Value, text, out noChange);
            //        if (!noChange)
            //        {
            //            if (!string.IsNullOrEmpty(replaced))
            //            {
            //                replaced += ", ";
            //            }

            //            replaced += Variables.Namespaces[Namespace.Category]
            //             + p.Key + FindandReplace.Arrow + Variables.Namespaces[Namespace.Category] + p.Value;
            //        }
            //    }
            //    if (!noChange)
            //    {
            //        text = Regex.Replace(text, "<includeonly>[\\s\\r\\n]*\\</includeonly>", "");
            //    }
            //}

            //string editSummary = "";
            //if (Settings.AppendToEditSummary)
            //{
            //    if (!string.IsNullOrEmpty(replaced))
            //        editSummary = "replaced: " + replaced.Trim();

            //    if (!string.IsNullOrEmpty(removed))
            //    {
            //        if (!string.IsNullOrEmpty(editSummary))
            //            editSummary += ", ";

            //        editSummary += "removed: " + removed.Trim();
            //    }
            //}
            //eventargs.EditSummary = editSummary;

            //eventargs.Skip = (text == eventargs.ArticleText) && Settings.Skip;
            #endregion
            return text;
        }

        public void LoadSettings(object[] prefs)
        {
            if (prefs == null) return;

            foreach (object o in prefs)
            {
                PrefsKeyPair p = o as PrefsKeyPair;
                if (p == null) continue;

                switch (p.Name.ToLower())
                {
                    case "enabled":
                        PluginEnabled = Settings.Enabled = (bool)p.Setting;
                        break;
                    case "skip":
                        Settings.Skip = (bool)p.Setting;
                        break;
                    case "appendtoeditsummary":
                        Settings.AppendToEditSummary = (bool)p.Setting;
                        break;
                }
                //Settings.Categories = (Dictionary<string, string>)pkp.Setting;
            }
        }

        public object[] SaveSettings()
        {
            Settings.Enabled = PluginEnabled;
            
            return new PrefsKeyPair[] {
            new PrefsKeyPair("Enabled", Settings.Enabled),
            new PrefsKeyPair("Skip", Settings.Skip),
            new PrefsKeyPair("AppendToEditSummary", Settings.AppendToEditSummary) };
        }

        public void Reset()
        {
            //set default settings
            Settings = new TranslatorExampleSettings();
            PluginEnabled = false;
        }

        public void Nudge(out bool Cancel) { Cancel = false; }
        public void Nudged(int Nudges) { }

        private static void ShowSettings(object sender, EventArgs e)
        { new FormMainWindow().Show(); }

        private bool PluginEnabled
        {
            get { return pluginenabledMenuItem.Checked; }
            set { pluginenabledMenuItem.Checked = value; }
        }

        private void PluginEnabledCheckedChange(object sender, EventArgs e)
        {
            Settings.Enabled = PluginEnabled;
            if (PluginEnabled)
                AWB.NotifyBalloon("TranslatorExample plugin enabled", ToolTipIcon.Info);
            else
                AWB.NotifyBalloon("TranslatorExample plugin disabled", ToolTipIcon.Info);
        }

        private static void AboutMenuItemClicked(Object sender, EventArgs e)
        {
            new AboutBox().Show();
        }
    }

    [Serializable]
    internal sealed class TranslatorExampleSettings
    {
        public bool Enabled;
        public Dictionary<string, string> Categories = new Dictionary<string, string>();
        public bool Skip = true;
        public bool AppendToEditSummary = true;
    }

    internal sealed class AboutBox : WikiFunctions.Controls.AboutBox
    {
        protected override void Initialise()
        {
            lblVersion.Text = "Version " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            textBoxDescription.Text = GPLNotice;
            Text = "Translator Plugin";
        }
    }
}

