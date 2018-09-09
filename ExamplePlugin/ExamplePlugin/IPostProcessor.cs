using System;
namespace ExamplePlugin
{
    interface IPostProcessor
    {
        //Loads text to translate 
        void LoadTextToTranslate(string paramString);

        //These are the delegates which will inform whether the task has been done .
       // event PostProcessor.ProgressFeedbackDelgate OnFeedback;
       // event PostProcessor.TranslationCompleteDelegate OnTranslationComplete;
        
        //This is the memory hungry process 
        void TranslateVoid();

        //This is used to write the rules in a flat file for debugging or providing to the Google
        //Exporting to CSV or in a flat file will be done using this function
        void WriteFile(System.Collections.Generic.Dictionary<string, string> allRules);
    }
}
