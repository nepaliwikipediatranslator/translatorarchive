package com.Nepali.Translator;


import android.annotation.SuppressLint;
import android.app.Activity;


import android.net.ParseException;

import android.os.AsyncTask;
import android.os.Bundle;

import android.view.View;
import android.view.View.OnClickListener;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.widget.Button;

import android.widget.ProgressBar;
import android.widget.TextView;
import com.Nepali.Translator.R;
public class NepaliTranslatorActivity extends Activity implements AnimationListener {
	
	private static final String inputText = "मेरा नाम कमल है।";
	public static final String q = "testq";

	 private ProgressBar mProgress;
	 private TextView richTextBox1;
	 private TextView richTextBox2;
	 
	 @Override    
	    public void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.main);        
	        System.out.println("started");        
	        richTextBox1 = (TextView)findViewById(R.id.richTextBox1);
	        richTextBox2 = (TextView)findViewById(R.id.richTextBox2);
	        richTextBox1.setText(inputText);        
			Button button = (Button) findViewById(R.id.btnTranslate);
			
	        mProgress = (ProgressBar) findViewById(R.id.progress);
			button.setOnClickListener(btnTranslate_Click);
			
	    }
	 private OnClickListener btnTranslate_Click = new OnClickListener() {		 
		    @SuppressLint("NewApi") public void onClick(View v) {
				System.out.println("button clicked");	        
				
				richTextBox2.setText("Connecting...");		
				
				String stringToTranslate = richTextBox1.getText().toString();
				if(!stringToTranslate.isEmpty()){
					System.out.println("starting string is not empty");
				try {
					String translatedString = "";
					//new TranslationRequest().execute(stringToTranslate,translatedString);
					//System.out.println("Executing thread");
					new LookupTask().execute(stringToTranslate,translatedString);
					//System.out.println("executed thread");
					
				} catch (Exception e) {
					e.printStackTrace();
					richTextBox2.setText("error printing"+e.getMessage());
				}
				}
				
			
		    }
		};
		
		private class LookupTask extends AsyncTask<String, String, String> {

			 @Override
		        protected void onPreExecute() {				 	
				 	mProgress.setVisibility(View.VISIBLE);		            
		        }
			 
			@Override
			protected String doInBackground(String... args) {
				String query = args[0];
				String parsedText = null;
				try{
				if(query==null){
					TextView tView = (TextView)findViewById(R.id.richTextBox1);
					query = tView.getText().toString();
				}
				if(query!=null){
					publishProgress(query);
					//TranslationHelper helper = new TranslationHelper();
					SoapTranslationHelper helper = new SoapTranslationHelper();
					helper.SetQuery(query);
					parsedText = helper.Translate();				

				}
				///System.out.println("Inside Try block");
			}catch(ParseException e){
				System.out.println(e.toString());
			}catch(Exception e){
				System.out.println(e.toString());
			}
				System.out.println("returning parsed text: " + parsedText);
				return parsedText;
		}
			@Override
	        protected void onPostExecute(String parsedText) {
	            mProgress.setVisibility(View.INVISIBLE);	            
	            richTextBox2.setText(parsedText);         
	            ///System.out.println("Inside async task, onpost execute");
	        }
			
			
		}
	@Override
	public void onAnimationEnd(Animation arg0) {
		mProgress.setVisibility(View.VISIBLE);	
		
	}
	@Override
	public void onAnimationRepeat(Animation arg0) {

		
	}
	@Override
	public void onAnimationStart(Animation arg0) {

		
	}	 
	 
}
