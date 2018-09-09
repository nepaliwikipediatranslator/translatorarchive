using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ExamplePlugin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMainWindow formMainWindow = new FormMainWindow();
            

             
            if (args.Count() > 0)
            {

                
                if (!string.IsNullOrEmpty(args[0]))
                {

                    if (File.Exists(args[0]))
                    {
                        MessageBox.Show("Ok loading... openinside");

                        formMainWindow.OpenInputFile(args[0]);

                        MessageBox.Show("Ok loading... end openfile");
                    }
                }
            }
        
            
            Application.Run(formMainWindow);
        }
    }
}
