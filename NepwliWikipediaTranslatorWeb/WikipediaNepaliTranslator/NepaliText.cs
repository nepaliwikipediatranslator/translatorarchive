﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikipediaNepaliTranslator
{
    public class NepaliText
    {
        private string _myText;
        public string MyText
        {
            get { return _myText; }
            set { _myText = value; }
        }

        public Dictionary<string, string> Rules;

        public NepaliText() { }

        public void LoadRules()
        {

        }
    }
}