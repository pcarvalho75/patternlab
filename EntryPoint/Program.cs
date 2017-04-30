using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntryPoint
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region Setting Language
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (!Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\International", "LocaleName", null).ToString().ToLower().Equals("en-us"))
            {
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "Locale", "00000409");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "LocaleName", "en-US");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sCountry", "Estados Unidos");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sCurrency", "$");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sDate", "/");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sDecimal", ".");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sGrouping", "3;0");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sLanguage", "ENU");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sList", ",");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sLongDate", "dddd, MMMM dd, yyyy");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sMonDecimalSep", ".");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sMonGrouping", "3;0");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sMonThousandSep", ",");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sNativeDigits", "0123456789");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sNegativeSign", "-");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sPositiveSign", "");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "M/d/yyyy");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sThousand", ",");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sTime", ":");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sTimeFormat", "h:mm:ss tt");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sShortTime", "h:mm tt");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sYearMonth", "MMMM, yyyy");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iCalendarType", "1");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iCountry", "1");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iCurrDigits", "2");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iCurrency", "0");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iDate", "0");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iDigits", "2");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "NumShape", "1");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iFirstDayOfWeek", "6");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iFirstWeekOfYear", "0");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iLZero", "1");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iMeasure", "1");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iNegCurr", "0");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iNegNumber", "1");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iPaperSize", "1");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iTime", "0");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iTimePrefix", "0");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "iTLZero", "0");
                MessageBox.Show("PatternLab will be restarted to set the default culture to EN-us.  We recommend configuring your computer with these parameters as default on the Control Panel -> Region not to get this warning again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Environment.Exit(0);
                System.Windows.Forms.Application.Exit();
            }
            #endregion

            if (args.Length == 0)
            {
                Console.WriteLine("Loading main GUI");
                Application.Run(new PatternLab.MainEntry());
                return;
            }
            else if (args.Length == 1)
            {
                FileInfo fi = new FileInfo(args[0]);
                Console.WriteLine("File extension: " + fi.Extension);

                if (fi.Extension.Contains("sepr"))
                {                  
                    Application.Run(new SEProcessor.Form1(fi.FullName));
                    return;
                }
                else if (fi.Extension.Contains("mpex"))
                {
                    Application.Run(new PepExplorer2.Result2.MAlginerrForm(fi.FullName));
                    return;
                }
                
            }
        }
    }
}
