using System;
using WikiFunctions.Plugin;

namespace NepaliWikiPediaTranslator
{
    public partial class FormMainWindow:IAutoWikiBrowser
    {

        #region IAutoWikiBrowser Members

        public WikiFunctions.Logging.TraceManager TraceManager
        {
            get { throw new NotImplementedException(); }
        }

        public WikiFunctions.Logging.Uploader.UploadableLogSettings2 LoggingSettings
        {
            get { throw new NotImplementedException(); }
        }

        public bool SkipNoChanges
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public WikiFunctions.Parse.FindandReplace FindandReplace
        {
            get { throw new NotImplementedException(); }
        }

        public WikiFunctions.SubstTemplates SubstTemplates
        {
            get { throw new NotImplementedException(); }
        }

        public string CustomModule
        {
            get { throw new NotImplementedException(); }
        }

        public event GetLogUploadLocationsEvent GetLogUploadLocations;

        #endregion

        #region IAutoWikiBrowserForm Members

        public new System.Windows.Forms.Form Form
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.TextBoxBase EditBox
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.TextBox CategoryTextBox
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.CheckBox BotModeCheckbox
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.CheckBox SkipNoChangesCheckBox
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.Button DiffButton
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.Button PreviewButton
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.Button SaveButton
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.Button SkipButton
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.Button StartButton
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.Button StopButton
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.ComboBox EditSummaryComboBox
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.StatusStrip StatusStrip
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.NotifyIcon NotifyIcon
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.RadioButton SkipNonExistentPages
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.CheckBox ApplyGeneralFixesCheckBox
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.CheckBox AutoTagCheckBox
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.CheckBox RegexTypoFix
        {
            get { throw new NotImplementedException(); }
        }

        public bool PreParseMode
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.ToolStripMenuItem PluginsToolStripMenuItem
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.ToolStripMenuItem InsertTagToolStripMenuItem
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuGeneral
        {
            get { throw new NotImplementedException(); }
        }

        public WikiFunctions.Controls.Lists.ListMaker ListMaker
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.ContextMenuStrip EditBoxContextMenu
        {
            get { throw new NotImplementedException(); }
        }

        public WikiFunctions.Logging.LogControl LogControl
        {
            get { throw new NotImplementedException(); }
        }

        public WikiFunctions.Session TheSession
        {
            get { throw new NotImplementedException(); }
        }

        public void NotifyBalloon(string message, System.Windows.Forms.ToolTipIcon icon)
        {

        }

        public string StatusLabelText
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IAutoWikiBrowserTabs Members

        public System.Windows.Forms.TabPage MoreOptionsTab
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.TabPage OptionsTab
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.TabPage StartTab
        {
            get { return this.tabPageHindi; }
        }

        public System.Windows.Forms.TabPage SkipTab
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.TabPage DabTab
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.TabPage BotTab
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Forms.TabPage LoggingTab
        {
            get { throw new NotImplementedException(); }
        }

        public void AddTabPage(System.Windows.Forms.TabPage tabp)
        {

        }

        public void RemoveTabPage(System.Windows.Forms.TabPage tabp)
        {

        }

        public void HideAllTabPages()
        {

        }

        public void ShowAllTabPages()
        {

        }

        public bool ContainsTabPage(System.Windows.Forms.TabPage tabp)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAutoWikiBrowserCommands Members

        public void ShowHelp(string url)
        {

        }

        public void ShowHelpEnWiki(string article)
        {

        }

        public void Start(IAWBPlugin sender)
        {

        }

        public void Start(string sender)
        {

        }

        public void Stop(IAWBPlugin sender)
        {

        }

        public void Stop(string sender)
        {

        }

        public void Save(IAWBPlugin sender)
        {

        }

        public void Save(string sender)
        {

        }

        public void AddLogItem(bool skipped, WikiFunctions.Logging.AWBLogListener logListener)
        {

        }

        public void TurnOffLogging()
        {

        }

        public void SkipPage(IAWBPlugin sender, string reason)
        {

        }

        public void SkipPage(string sender, string reason)
        {

        }

        public void GetDiff(IAWBPlugin sender)
        {

        }

        public void GetDiff(string sender)
        {

        }

        public void GetPreview(IAWBPlugin sender)
        {

        }

        public void GetPreview(string sender)
        {

        }

        public void AddMainFormClosingEventHandler(System.Windows.Forms.FormClosingEventHandler handler)
        {

        }

        public void StartProgressBar()
        {

        }

        public void StopProgressBar()
        {

        }

        public void AddArticleRedirectedEventHandler(WikiFunctions.ArticleRedirected handler)
        {

        }

        #endregion

        #region IAutoWikiBrowserInfo Members

        public Version AWBVersion
        {
            get { throw new NotImplementedException(); }
        }

        public Version WikiFunctionsVersion
        {
            get { throw new NotImplementedException(); }
        }

        public string AWBVersionString
        {
            get { throw new NotImplementedException(); }
        }

        public string WikiFunctionsVersionString
        {
            get { throw new NotImplementedException(); }
        }

        public string WikiDiffVersionString
        {
            get { throw new NotImplementedException(); }
        }

        public int NumberOfEdits
        {
            get { throw new NotImplementedException(); }
        }

        public int NumberOfNewPages
        {
            get { throw new NotImplementedException(); }
        }

        public int NumberOfIgnoredEdits
        {
            get { throw new NotImplementedException(); }
        }

        public int NumberOfEditsPerMinute
        {
            get { throw new NotImplementedException(); }
        }

        public int NumberOfPagesPerMinute
        {
            get { throw new NotImplementedException(); }
        }

        public int Nudges
        {
            get { throw new NotImplementedException(); }
        }

        public WikiFunctions.ProjectEnum Project
        {
            get { throw new NotImplementedException(); }
        }

        public string LangCode
        {
            get { throw new NotImplementedException(); }
        }

        public bool CheckStatus(bool login)
        {
            throw new NotImplementedException();
        }

        public bool Privacy
        {
            get { throw new NotImplementedException(); }
        }

        public bool Shutdown
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
