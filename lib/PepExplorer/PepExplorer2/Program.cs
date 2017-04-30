using PepExplorer2.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PepExplorer2
{
    /// <summary>
    /// Program entry point.
    /// If arguments are passed into command line the program executes without GUI otherwise it will call the program form.
    /// </summary>
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            Application.EnableVisualStyles(); 
           

            try
            {

                string[] programInfo = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");

                Console.WriteLine("############################################################################");
                Console.WriteLine("                           PepExplorer " + programInfo[1]                    );
                Console.WriteLine("            Created by Felipe V. Leprevost and Paulo C Carvalho"             );
                Console.WriteLine("############################################################################");
            }
            catch
            {
                Console.WriteLine("PepExplorer");
            }

            if (args.Length == 0)
            {
                MainForm mf = new MainForm();
                Application.Run(mf);
                //Console.WriteLine("No arguments found\n");
                //ProgramMainForm pmf = new ProgramMainForm();
                //pmf.ShowDialog();
                return 0;
            }
            else
            {
                //process args[].
            }

            Console.ReadKey();
            return 0;
        }
    }
}
