using SEPRPackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEProcessor.Result
{
    public partial class SequenceExplorer : Form
    {
        List<MyProtein> MyProteinGroup;
        MyProtein ThisProtein;
        public SequenceExplorer()
        {
            InitializeComponent();
        }

        private void ProteinCoverage3_Load(object sender, EventArgs e)
        {

        }

        public void FireUpInterface(List<MyProtein> theProteinsInGroup, MyProtein p)
        {
            sequenceViewer1.DisplayProtein(p);
            MyProteinGroup = theProteinsInGroup;
            ThisProtein = p;
            
            
            string Locus = p.Locus;
            string FastaSequence = p.Sequence;
            List<string> Peptides = p.PeptideResults.Select(a => a.PeptideSequence).ToList();
            globalProteinCoverage1.Plot(Locus, FastaSequence, Peptides);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2 && groupGraph1.g == null)
            {
                groupGraph1.GenerateGraph(MyProteinGroup, ThisProtein);
            }
        }
    }
}
