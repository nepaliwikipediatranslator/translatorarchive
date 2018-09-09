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

import org.apache.http.Header;
import org.apache.http.client.methods.HttpPost;
import org.ksoap2.*;
import org.ksoap2.serialization.*;
import org.ksoap2.transport.*;
import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import android.annotation.SuppressLint;
import android.database.CursorJoiner.Result;



public class SoapTranslationHelper {

	 private static final String SERVICE_URL = "http://reversespeller.com/Translator/M.asmx";
		private static final String NAMESPACE = "http://tempuri.org/";
		private static final String URL = 
				"http://reversespeller.com/Translator/M.asmx";		
		
		private static final String SOAP_ACTION = "http://tempuri.org/Translate";
		private static final String METHOD_NAME = "Translate";
		
		
		private static final String inputText = "मेरा नाम कमल है।";
		public static final String q = "testq";
		
	 
		private static final String REMOTE_METHOD_NAME = "translate";
		String query;    
	    String reponse;
	    
	    protected void SetQuery(String paramQuery){
			this.query = paramQuery;
		}
	    
	    @SuppressLint("NewApi") protected String Translate() throws UnsupportedEncodingException{
				String stringToTranslate = query;
				if(!stringToTranslate.isEmpty()){
				
				try {
				SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);
				
				PropertyInfo info = new PropertyInfo();
				info.name = "q";
				info.type = PropertyInfo.STRING_CLASS;
				
				//request.addProperty("q", inputText);				
				request.addProperty(info,query);				
				SoapSerializationEnvelope envelope = 
					new SoapSerializationEnvelope(SoapEnvelope.VER11); 
				envelope.dotNet = true;

				envelope.setOutputSoapObject(request);
				HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
				Object result;	
					androidHttpTransport.call(SOAP_ACTION, envelope);
					SoapObject resultsRequestSOAP = (SoapObject) envelope.bodyOut;					
					result=  (Object)envelope.getResponse();
					return result.toString();
					
				}
				catch (Exception e) {
					System.out.println("Exception while communicating to server");
					return "Exception while communicating to translation server";
				}
			}
				return "";	        
	    	
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
