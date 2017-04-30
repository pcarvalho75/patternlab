using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProteinCoverageViewer
{
    /// <summary>
    /// Interaction logic for MicroChargeCoverageControl.xaml
    /// </summary>
    public partial class MicroChargeCoverageControl : UserControl
    {
        public MicroChargeCoverageControl()
        {
            InitializeComponent();
        }

        public void Plot(string sequence, List<string> peptides) 
        {
            foreach (string pep in peptides)
            {

                foreach (Match m in Regex.Matches(sequence, pep))
                {
                    
                }

            }
        }
    }
}
