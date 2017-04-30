using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ProteinCoverageViewer.HMMService;

namespace ProteinCoverageViewer
{

    public partial class GlobalProteinCoverage : UserControl
    {

        ProteinData cacheProtein;
        public GlobalProteinCoverage()
        {
            InitializeComponent();
        }


        private void Plot (ProteinData pd, List<HMMResult> hmmResults = null) {
            Plot(pd.Locus, pd.FastaSequence, pd.Peptides, hmmResults);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="LocusName"></param>
        /// <param name="fastaSequence"></param>
        /// <param name="peptides">PeptidesWithPTMs</param>
        /// <param name="hmmResults"></param>
        public void Plot(string LocusName, string fastaSequence, List<string> peptides, List<HMMResult> hmmResults = null)
        {
            chartCoverage.Series[0].Points.Clear();
            chartCoverage.Series[1].Points.Clear();
            chartCoverage.Series[2].Points.Clear();

            //To Fix
            cacheProtein = new ProteinData(LocusName, fastaSequence, peptides);
            
            //Get the locations
            List<PeptidePositionMatch> theMatches = new List<PeptidePositionMatch>();
            foreach (string peptide in peptides)
            {
                //Make sure the peptide is clean
                string cleanedPep = CleanPeptide(peptide, true);
                string pepWithPTM = CleanPeptide(peptide, false);
                MatchCollection location = Regex.Matches(fastaSequence, cleanedPep);
                List<int> thepos = new List<int>();

                foreach (Match m in location)
                {
                    thepos.Add(m.Index);
                }
                thepos = thepos.Distinct().ToList();

                theMatches.Add(new PeptidePositionMatch(thepos, pepWithPTM, cleanedPep.Length));
            }
            theMatches = theMatches.OrderBy(a => a.Position[0]).ThenBy(a => a.Length).ToList();


            // Populate series data
            //First, the protein
            chartCoverage.Series[0].Points.AddXY(0, 0, fastaSequence.Length);
            chartCoverage.Series[0].Points[0].AxisLabel = LocusName;

            //And add a blank petide
            chartCoverage.Series[1].Points.AddXY(0, 0, 0);

            //And add a blank peptide

            //And now the rest
            for (int i = 0; i < theMatches.Count; i++)
            {
                int startPos = theMatches[i].Position[0];
                int length = theMatches[i].Length;

                if (hmmResults == null)
                {
                    //Prints peptides in the protein stripe
                    chartCoverage.Series[1].Points.AddXY(i + 1, startPos, startPos + length);
                    chartCoverage.Series[1].Points.Last().AxisLabel = theMatches[i].Sequence;
                }

                if (true)
                {
                    //Prints in a new layer
                    chartCoverage.Series[1].Points.AddXY(0, startPos, startPos + length);
                }


            }

            if (hmmResults != null)
            {
                for (int i = 0; i < hmmResults.Count; i++)
                {
                    chartCoverage.Series[2].Points.AddXY(i + 1, hmmResults[i].AFromk__BackingField, hmmResults[i].ATok__BackingField);
                    chartCoverage.Series[2].Points.Last().AxisLabel = hmmResults[i].Descriptionk__BackingField;
                }
            }



            //Scroll bar
            chartCoverage.ChartAreas[0].AxisX.ScrollBar.Size = 10;
            chartCoverage.ChartAreas[0].AxisY.Interval = 100;
            chartCoverage.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;


            //For some unknown reasons, Y is X and X is Y
            chartCoverage.ChartAreas[0].AxisY.Maximum = fastaSequence.Length + (int)fastaSequence.Length * 0.05;
            chartCoverage.ChartAreas[0].AxisY.Minimum = 0;
            chartCoverage.ChartAreas[0].AxisY.Title = "Amino Acid Number";

            if (checkBoxAllPeptideLabels.Checked)
            {
                chartCoverage.ChartAreas[0].AxisX.Interval = 1;
            }
            else
            {
                chartCoverage.ChartAreas[0].AxisX.Interval = 0;
            }


            
        }

        static string CleanPeptide(string peptide, bool removeParenthesis)
        {
            string cleanedPeptide = Regex.Replace(peptide, @"^[A-Z|\-|\*]\.", "");
            cleanedPeptide = Regex.Replace(cleanedPeptide, @"\.[A-Z|\-|\*]$", "");
            cleanedPeptide = cleanedPeptide.Replace("*", "");

            if (removeParenthesis)
            {
                cleanedPeptide = Regex.Replace(cleanedPeptide, @"\([0-9|\.]*\)", "");
            }

            return cleanedPeptide;
        }


 

        class PeptidePositionMatch
        {
            public List<int> Position { get; set; }
            public string Sequence { get; set; }
            public int Length { get; set; }

            public PeptidePositionMatch(List<int> pos, string seq, int length)
            {
                Position = pos;
                Sequence = seq;
                Length = length;
            }
        }

        private void checkBoxAllPeptideLabels_CheckedChanged(object sender, EventArgs e)
        {
            Plot(cacheProtein);
        }

        private void buttonDomains_Click(object sender, EventArgs e)
        {
            buttonDomains.Text = "Accessing FioCloud";
            this.Update();
         
            HMMClient hmmClient = new HMMClient();

            try
            {
                List<HMMResult> theDomains = hmmClient.Scan(cacheProtein.FastaSequence).ToList();
                theDomains.RemoveAll(a => a.IEvaluek__BackingField > 0.001 || a.QEValuek__BackingField > 0.00001);

                Plot(cacheProtein, theDomains);
            }
            catch
            {
                MessageBox.Show("Unable to access Fiocloud");
            }

            buttonDomains.Text = "Inferr Domains using FioCloud";
                
        }
    }

    class ProteinData
    {
        public string Locus { get; set; }
        public string FastaSequence { get; set; }
        public List<string> Peptides { get; set; }

        public ProteinData(string locus, string fastaSequence, List<string> peptides)
        {
            Locus = locus;
            FastaSequence = fastaSequence;
            Peptides = peptides;
        }
    }
}
