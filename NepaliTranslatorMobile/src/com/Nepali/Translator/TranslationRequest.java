package com.Nepali.Translator;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.BasicHttpContext;
import org.apache.http.protocol.HttpContext;

import android.os.AsyncTask;

public class TranslationRequest extends AsyncTask<String,Void,String>{
	 private static final String SERVICE_URL = "http://reversespeller.com";
		private static final String REMOTE_METHOD_NAME = "Nepali";
		String query;    
	    String reponse;
	    
	    
		@Override
		protected String doInBackground(String... arg0) {
			reponse = ProcessTranslation(query);
			return "Executed";
		}
		
		public String ProcessTranslation(String query2) {
			String sampleURL = SERVICE_URL + "/"+REMOTE_METHOD_NAME+"/"+query;
	         System.out.println(sampleURL);

	         HttpClient httpClient = new DefaultHttpClient();
	         HttpContext localContext = new BasicHttpContext();
	         HttpGet httpGet = new HttpGet(sampleURL);
	         String text = null;
	         try {
	             HttpResponse response = httpClient.execute(httpGet, localContext);
	             System.out.println("Some Response" +response);
	             HttpEntity entity = response.getEntity();
	             text = entity.toString();
	         } catch (Exception e1) {
	             return e1.getLocalizedMessage();
	         }
	         return text;
	     
		}

		protected void SetQuery(String paramQuery){
			this.query = paramQuery;
		}
		
		
		@Override
		protected void onPostExecute(String result){
			if (null != reponse) {
	            System.out.println("Got Data" + reponse);
	            this.reponse = result;
	            
	        }
	        else{
	            //Handle the Error
	        }
		}
		
}
