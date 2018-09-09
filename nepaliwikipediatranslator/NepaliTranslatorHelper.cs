using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace NepaliWikiPediaTranslator
{
    public class NepaliTranslatorHelper
    {
        private static string NepaliWikipediaTranslatorId = "8B88C4ADF2E4CFAC01C159653059F14D35310B41";
        private TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();

        public string Parse(string EnglishText)
        {
            string returnString = "Could not connect to http://api.microsofttranslator.com to translate English";
            returnString = MicrosoftTranslator.MsTranslator.Translate(EnglishText);
            //HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
            //using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            //{
            //    client.GetAppIdToken("NepaliWikipediaTranslator", 10, 10, 1000);

            //    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] =
            //        httpRequestProperty;
            //    returnString = client.Translate(NepaliWikipediaTranslatorId, EnglishText, "en", "hi", "text/plain", "");
            //}

            return returnString;
        }

    }
}
