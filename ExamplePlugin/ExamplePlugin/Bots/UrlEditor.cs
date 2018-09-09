using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExamplePlugin.Bots
{
    public partial class UrlEditor : Form
    {
        private FormMainWindow formMainWindow;

        public UrlEditor()
        {
            InitializeComponent();
        }

        public UrlEditor(FormMainWindow formMainWindow)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            comboWikiSelector.DataSource = mediaWikiVars;
            this.formMainWindow = formMainWindow;
        }

        private void btnGetSourceCode_Click(object sender, EventArgs e)
        {
            GenerateLink();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            //GenerateLink();
        }

        private void GenerateLink()
        {
            if (this.urlTextBox.Text.ToLower().Contains("wikipedia") )
            {
                try
                {
                    var address = urlTextBox.Text.ToLower().IndexOf("wiki");

                    //MessageBox.Show(address.ToString());

                    var startindex = urlTextBox.Text.ToLower().IndexOf("wikipedia.org/wiki/") + ("wikipedia.org/wiki/").Length;

                    var length = urlTextBox.Text.Length - startindex;
                    var articleName = urlTextBox.Text.Substring(startindex, length);
                    this.Hide();
                    formMainWindow.LoadWikiArticle(articleName);


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString(), ex.Source);
                }


            }
            this.Close();
        }

        private void UrlEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(((int)e.KeyChar).ToString());
            //quit form)
            if (e.KeyChar == (char)27)
            {
                // Then escape key was pressed
                this.Hide();
            }
        }
        private string[] mediaWikiVars = new string[] {
				"ne","hi","en","ace", "af", "ak", "als", "am", "ang", "ab", "ar", "an", "arc",
				"roa-rup", "frp", "arz", "as", "ast", "gn", "av", "ay", "az", "id",
				"ms", "bg", "bm", "zh-min-nan", "nan", "map-bms", "jv", "su", "ba",
				"be", "be-x-old", "bh", "bcl", "bi", "bn", "bo", "bar", "bs", "bpy",
				"br", "bug", "bxr", "ca", "ceb", "ch", "cbk-zam", "sn", "tum", "ny",
				"cho", "chr", "co", "cy", "cv", "cs", "da", "dk", "pdc", "de", "nv",
				"dsb", "na", "dv", "dz", "mh", "et", "el", "eml",  "myv", "es",
				"eo", "ext", "eu", "ee", "fa", "hif", "fo", "fr", "fy", "ff", "fur",
				"ga", "gv", "sm", "gd", "gl", "gan", "ki", "glk", "got", "gu", "ha",
				"hak", "xal", "haw", "he",  "ho", "hsb", "hr", "hy", "io",
				"ig", "ii", "ilo", "ia", "ie", "iu", "ik", "os", "xh", "zu", "is",
				"it", "ja", "ka", "kl", "kr", "pam", "krc", "csb", "kk", "kw", "rw",
				"ky", "rn", "sw", "km", "kn", "ko", "kv", "kg", "ht", "ks", "ku",
				"kj", "lad", "lbe", "la", "lv", "to", "lb", "lt", "lij", "li", "ln",
				"lo", "jbo", "lg", "lmo", "hu", "mk", "mg", "mt", "mi", "cdo",
				"mwl", "ml", "mdf", "mo", "mn", "mr", "mus", "my", "mzn", "nah",
				"fj",  "nl", "nds-nl", "cr", "new", "nap", "ce", "pih", "no",
				"nb", "nn", "nrm", "nov", "oc", "mhr", "or", "om", "ng", "hz", "uz",
				"pa", "pag", "pap", "pi", "pcd", "pms", "nds", "pnb", "pl", "pt",
				"pnt", "ps", "aa", "kaa", "crh", "ty", "ksh", "ro", "rmy", "rm",
				"qu", "ru", "sa", "sah", "se", "sg", "sc", "sco", "sd", "stq", "st",
				"tn", "sq", "si", "scn", "simple", "ss", "sk", "sl", "cu", "szl",
				"so", "ckb", "srn", "sr", "sh", "fi", "sv", "ta", "tl", "kab",
				"roa-tara", "tt", "te", "tet", "th", "ti", "vi", "tg", "tokipona",
				"tp", "tpi", "chy", "ve", "tr", "tk", "tw", "udm", "uk", "ur", "ug",
				"za", "vec", "vo", "fiu-vro", "wa", "vls", "war", "wo", "wuu", "ts",
				"yi", "yo", "diq", "zea", "zh", "zh-tw", "zh-cn", "zh-classical",
				"zh-yue", "bat-smg"
			};

        private void urlTextBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void urlTextBox_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(urlTextBox.Text))
            {
                string wikipediaDomain = (comboWikiSelector.SelectedItem.ToString())+".wikipedia.org/wiki/";
                if (!urlTextBox.Text.ToLower().Contains(wikipediaDomain))
                {
                    urlTextBox.Text = wikipediaDomain + urlTextBox.Text;
                }
            }
        }
    }
}
