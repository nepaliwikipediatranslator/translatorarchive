package com.Nepali.Translator;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.BasicHttpContext;
import org.apache.http.protocol.HttpContext;

public class TranslationHelper {

	 private static final String SERVICE_URL = "http://reversespeller.com";
		private static final String REMOTE_METHOD_NAME = "Nepali";
		String query;    
	    String reponse;
	    
	    protected void SetQuery(String paramQuery){
			this.query = paramQuery;
		}
	    
	    protected String Translate() throws UnsupportedEncodingException{
	    	//query = URLEncoder.encode(query,"UTF-8");
	    	String sampleURL = SERVICE_URL + "/"+REMOTE_METHOD_NAME+"/"+query;
	         System.out.println(sampleURL);

	         HttpClient httpClient = new DefaultHttpClient();
	         HttpContext localContext = new BasicHttpContext();
	         System.out.println("creating new httpget");
	         
	         //String restUrl = URLEncoder.encode(sampleURL, "UTF-8");
	         
	         //HttpGet httpGet = new HttpGet(restUrl);
	         HttpGet httpGet = new HttpGet(sampleURL);
	         String translatedString = null;
	         try {
	        	 System.out.println("creating response");
	             HttpResponse response = httpClient.execute(httpGet, localContext);
	             System.out.println("Some Response" +response);
	             HttpEntity entity = response.getEntity();
	             
	             translatedString = ParseContent(entity);
	         } catch (Exception e1) {
	             System.out.println(e1.toString());
	         }
	         return translatedString;
	    	
	    }
	    
	    protected String ParseContent(HttpEntity entity){	
	    	if(entity.getContentLength()==0){
	    		return "Nothing returned from server";
	    	}
	    	StringBuilder sb = new StringBuilder();
	    	try {
	    	    BufferedReader reader = 
	    	           new BufferedReader(new InputStreamReader(entity.getContent()), 65728);
	    	    String line = null;

	    	    while ((line = reader.readLine()) != null) {
	    	        sb.append(line);
	    	    }
	    	}
	    	catch (IOException e) { e.printStackTrace(); }
	    	catch (Exception e) { e.printStackTrace(); }

	    	System.out.println("finalResult " + sb.toString());
	    	return sb.toString();
	    }
}
