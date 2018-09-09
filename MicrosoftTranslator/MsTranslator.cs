using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace MicrosoftTranslator
{
    public class MsTranslator
    {
        public static  string Translate(string englishText)
        {
            AdmAccessToken admToken;
            string headerValue;
            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            //AdmAuthentication admAuth = new AdmAuthentication("clientId", "client secret");
            AdmAuthentication admAuth = new AdmAuthentication("NepaliWikipediaTranslator", "vUo0WWvT6CQvaQO1CuFCvU/gkstlhy2TXYxqOj4aOPY=");
            try
            {
                admToken = admAuth.GetAccessToken();
                DateTime tokenReceived = DateTime.Now;
                // Create a header with the access_token property of the returned token
                headerValue = "Bearer " + admToken.access_token;
               return TranslateMethod(headerValue,englishText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                return englishText;
            }
        }
        public static void TranslateAddMethod(string englishText)
        {
            AdmAccessToken admToken;
            string headerValue;
            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            //AdmAuthentication admAuth = new AdmAuthentication("clientId", "client secret");
            AdmAuthentication admAuth = new AdmAuthentication("NepaliWikipediaTranslator", "vUo0WWvT6CQvaQO1CuFCvU/gkstlhy2TXYxqOj4aOPY=");
            try
            {
                admToken = admAuth.GetAccessToken();
                DateTime tokenReceived = DateTime.Now;
                // Create a header with the access_token property of the returned token
                headerValue = "Bearer " + admToken.access_token;
                AddTranslationMethod(headerValue,englishText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }
        public static void TranslationDetection(string englishText)
        {
            AdmAccessToken admToken;
            string headerValue;
            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            //AdmAuthentication admAuth = new AdmAuthentication("clientID", "client secret");//NepaliWikipediaTranslator
            AdmAuthentication admAuth = new AdmAuthentication("NepaliWikipediaTranslator", "vUo0WWvT6CQvaQO1CuFCvU/gkstlhy2TXYxqOj4aOPY=");
            try
            {
                admToken = admAuth.GetAccessToken();
                // Create a header with the access_token property of the returned token
                headerValue = "Bearer " + admToken.access_token;
                DetectMethod(headerValue,englishText);

            }
            catch (WebException e)
            {
                ProcessWebException(e);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }
        private static void DetectMethod(string authToken, string textToDetect)
        {
            //Keep appId parameter blank as we are sending access token in authorization header.
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Detect?text=" + textToDetect;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string languageDetected = (string)dcs.ReadObject(stream);
                    Console.WriteLine(string.Format("Language detected:{0}", languageDetected));
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
        private static void ProcessWebException(WebException e)
        {
            Console.WriteLine("{0}", e.ToString());
            // Obtain detailed error information
            string strResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)e.Response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.ASCII))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            Console.WriteLine("Http status code={0}, error message={1}", e.Status, strResponse);
        }
        private static void AddTranslationMethod(string authToken, string sourcetextforadd)
        {
            // Add TranslatorService as a service reference, Address:http://api.microsofttranslator.com/V2/Soap.svc
            TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();
            //Set Authorization header before sending the request
            HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Method = "POST";
            httpRequestProperty.Headers.Add("Authorization", authToken);

            // Creates a block within which an OperationContext object is in scope.
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                //string sourcetextforadd = "una importante contribución a la rentabilidad de la empresa";
                try
                {
                    //Keep appId parameter blank as we are sending access token in authorization header.
                    client.AddTranslation("", sourcetextforadd, "an important contribution to the company profitability.", "en", "hi", 1, "text/plain", "general", "TestUserID", null);
                    Console.WriteLine("Your translation for {0} has been added", sourcetextforadd);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while adding translation. " + ex.Message);
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }
        private static string TranslateMethod(string authToken,string sourceText)
        {
            // Add TranslatorService as a service reference, Address:http://api.microsofttranslator.com/V2/Soap.svc
            TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();
            //Set Authorization header before sending the request
            HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Method = "POST";
            httpRequestProperty.Headers.Add("Authorization", authToken);

            // Creates a block within which an OperationContext object is in scope.
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                //string sourceText = "<UL><LI>Use generic class names. <LI>Use pixels to express measurements for padding and margins. <LI>Use percentages to specify font size and line height. <LI>Use either percentages or pixels to specify table and container width.   <LI>When selecting font families, choose browser-independent alternatives.   </LI></UL>";
                string translationResult;
                //Keep appId parameter blank as we are sending access token in authorization header.
                translationResult = client.Translate("", sourceText, "en", "hi", "text/html", "general");
                //Console.WriteLine("Translation for source {0} from {1} to {2} is", sourceText, "en", "de");
                //Console.WriteLine(translationResult);
                //Console.WriteLine("Press any key to continue...");
                //Console.ReadKey(true);
                return translationResult;
            }
        }
    

    }
}
