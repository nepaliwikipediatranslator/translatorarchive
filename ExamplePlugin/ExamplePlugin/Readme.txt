==================
Idea and algorithm
==================

The idea behind building this application was generated from my experience in building Nepali OCR application in which the final output text of the OCR applicatoin was fed into the postprocessor which would parse the unicode text depending upon the rules.
Though the rules were hardcoded into the application in that application, I later thought of keeping the rules in a separate text file / resource file/ database or at some other places so that we would not need to modify the entire application to add the new rules.

So the first build of this application consisted of a text file called hinditonepali.text in which the "find and replace text" or the so called rules were kept.
The application would search for the specific occurrence of the word and would replace them with the replacement string present in the rule file(hinditonepali.txt).

For example the rule: apple|ball would replace all the "apple"s into "ball"s

Finally the rules grew larger and larger and needed some sort of classification.
It was easy to classify according to the parts of speech: noun/verb and adjectives which occured in significant numbers during translation.
They were placed in separate files called nounlist.txt, verblist.txt and adjectivelist.txt.

Dynamic rules were generated for nouns/verbs and adjectives because most of them followed the parts of speech and the way they were spoken and written.
For example: नियमों were नियमहरु  . The root word would be the same in both the languages: नियम
Hence a rule which would find नियम and remove मों and append हरु would be applicable for most of other nouns.
Thus नियम would be placed in the nounlist and the rule to remove and append would be placed in the rules file(hinditonepali.txt).

Similar rules for verbs and adjectives were added.

#####################
=======
Syntax
========

The purpose of this application is to translate Hindi text to Nepali text so that they could be used in wikipedia.

This application was built by Rajesh Pandey at September 2010 and was modified several times.

The rule file contains all the rules for translating Hindi text to Nepali.
####
To use a verb, one should define that this line contains a verb: 
(hiverb)+ति हैं|(neverb)+छ
(hiverb)+ति है|(neverb)+छ
(hiverb)+वाते हुए|(neverb)+ाउदा खेरि
(hiverb)+ता है|(neverb)+न्छ

####
To use a Noun, one should define that this line contains a Noun: 
eg: the following line demonstrates how to add a noun rule in the file : hinditonepali.txt
(hinoun)+ओं|(nenoun)+हरु

####
besides nouns are placed separately in a file called nounlist.txt
as: 
फेसबुक|फेसबुक
नंबर|नंबर
नीति|नीति
शब्द|शब्द

####
Similarly adjectives are placed in a file called adjectivelist.txt as follows:
सामने|अगाडी
जरूरत|जरूरत
संक्रामक|संक्रामक
मौजूद|मौजूद
वर्जित|वर्जित
देर|अबेर
संकेतक|संकेतक
पूरे|पुरा

####
The rules for adjectives are also kept in the main rule file as follows:
(hiadjective)+ है|(neadjective)+ छ


#################
============================
Debugging and troubleshooting:
============================

Troubles
