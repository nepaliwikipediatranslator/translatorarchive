#labels Phase-Requirements,Phase-Support,Featured
#About this translator.
<img src="http://nepaliwikipediatranslator.googlecode.com/svn/wiki/TranslatorGooglePlayImage.JPG">

Introduction 

This project is intended to ease the way people find and replace strings, repeatedly and every time they translate a new article in Wikipedia.
There might be better tools but since I could not find one that was suitable for Nepali Wikipedia. 
While I had to do repeated work I wished to save the find and replace strings so that they could be used in the future. 

Later on I saved them in a notepad. I was working with an article that was imported from Hindi and I was then translating to Nepali. Hence I chose the name HindiToNepali.txt where lexicons / rules/ find and replace keywords/ strings are saved in a flat file. 

This was the easiest thing to do, and later saved a lot of time. I built this application overnight, there is nothing secret but the user who uses this application, presses control+ space and then adds new find and replace keywords.



Details 

Install the setup file 
<br>
For example in है in Hindi might mean छ in Nepali. <br>
Just put <br>
'है|छ' <br>
in one line in the file which can be edited in Notepad present in windows, or even from the application by pressing control+space.

<br>
Then paste some test translation texts in हिन्दी tab and press "Translate" or "Ctrl+J"
Browse to the Nepali tab to see the translated text.

<br>
If you are not satisfied with the results, just add or remove the rules/ find and replace texts: from the file.




 Prerequisites 
Download the setup file, and install it. You might need the dotnetframework 4.0 before you could run the application. I guess the installer downloads the framework from microsoft's download website. 

If it does not, get it from here :

http://www.microsoft.com/downloads/en/details.aspx?FamilyID=9cfb2d51-5ff4-4491-b0e5-b386f32c0992&displaylang=en
