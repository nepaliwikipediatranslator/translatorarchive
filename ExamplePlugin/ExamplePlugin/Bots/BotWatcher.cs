using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DotNetWikiBot;

namespace ExamplePlugin.Bots
{
     

    public partial class BotWatcher : Form
    {
        delegate void SetPercent(int percent); //setpercent is a delegate which will be used to communicate between the postprocessor thread to gett the percentage .. how much the work has been done.. 
        delegate void SetTextCallback(string text); //set textcallback is used to get the translated text 

        private Thread botThread;
        private BotScript pr;

        void SetPercentMethod(int percent)  //this is the method which updates the progressbar after it gets the value from the delegate setpercent
        {
            progressBarBotWatcher.Value = percent;
        }

        public BotWatcher()
        {
            InitializeComponent();
            pr = new BotScript();
        }
        public BotWatcher(Site site)
        {
            InitializeComponent();
            pr = new BotScript(site);
        }

        /// <summary>
        /// This method informs the progressbar to update, with the appropriate value to update
        /// </summary>
        /// <param name="percent"></param>
        private void C_OnFeedBack(int percent)
        {
            //this.progressBar1.Value = percent;

            //progressBar1.Invoke(new SetPercent(SetPercentMethod), new object[] { percent });
            Debug.Write(" " + percent + "%");

            //cross thread - so you don't get the cross theading exception
            if (progressBarBotWatcher.InvokeRequired)
            {
                progressBarBotWatcher.Invoke(new SetPercent(SetPercentMethod), new object[] { percent });
            }

        }
        /// <summary>
        /// This method is called when the translation is complete
        /// It should have been as simple as : private void C_TranslationComplete(string message){this.richTextBoxNepali.Text = message;}
        /// But I kept on receiving error from the compiler and did the lengthy process, hope it will be thread safe now.. 
        /// </summary>
        /// <param name="message"></param>
        private void C_TranslationComplete(string message)
        {
            //Microsoft thing Grr..
            if (this.richTextBoxLogger.InvokeRequired)
            {
                //I don't know why but microsoft said: "How to: Make Thread-Safe Calls to Windows Forms Controls" => to use a delegate and use if/else to do this thing.. 
                //looks like some complicated thing .. but the thing we will be doing is inside else.. 
                SetTextCallback d = new SetTextCallback(C_TranslationComplete);
                this.Invoke(d, new object[] { message });

                //this.richTextBoxNepali.Text = message;

            } //End microsoft thing (warning) grrr..
            else
            {
                this.richTextBoxLogger.Text = message; //This is what I actually wanted to do
            }




        }
        public void RunBotWatcher()
        {

            progressBarBotWatcher.Value = progressBarBotWatcher.Minimum; //resetting the progressbar to zero


            pr.OnFeedbackForBot += new BotScript.ProgressFeedbackDelgateForBot(C_OnFeedBack); //this event is required for updating the progressbar
            //pr.OnBotProcessComplete += new BotScript.TranslationCompleteDelegateForBot(C_TranslationComplete); //this event is required for updating the result 
            //pr.OnBotProcessComplete += MyThreader.IO.Console.WriteLine;
            


            //Lets define a thread before running a memory hungry process, because it takes time and usually hangs the UI, so putting in a thread
            botThread = new Thread(new ThreadStart(pr.FindAndReplaceThread)); //inside the thread is the function that will be executed 
            botThread.Name = "BotThread"; //Name of the thread
            botThread.Start(); //starting translation. 

            
             

        }

        private void btnFindAndReplace_Click(object sender, EventArgs e)
        {
            
            if(pr ==null || pr.GetSite==null)
            {
                ClientLogin clientLogin = new ClientLogin();
                clientLogin.ShowDialog();
                if(clientLogin.LogOnBot!=null)
                pr = clientLogin.LogOnBot;
                pr.SetSite(clientLogin.logonBotSite);

            }
            //pr.FindAndReplaceThread();
            RunBotWatcher();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ClientLogin clientLogin  = new ClientLogin();
            clientLogin.ShowDialog();
            if(clientLogin.LogOnBot!=null)
                pr = clientLogin.LogOnBot;

        }
    }
}
