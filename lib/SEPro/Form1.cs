using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEProcessor
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string seproFile)
        {
            InitializeComponent();
            Console.WriteLine("Loading ... " + seproFile);
            mainControl1.LoadResults(seproFile);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] stuff = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");
                Console.WriteLine(stuff[1]);
                this.Text += " " + stuff[1];
            }
            catch
            {
                Console.WriteLine("Unable to retrieve version number.");
            }
        }
    }
}
