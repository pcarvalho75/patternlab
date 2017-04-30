using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SEPRPackage;
using PatternTools.SQTParser;
using System.Text.RegularExpressions;
using System.IO;

namespace SEProQ.Acetylation
{
    public partial class ReverseLabelCheck : UserControl
    {
        PatternTools.Deisotoping.SignalGenerator isotopicSignalGenerator = new PatternTools.Deisotoping.SignalGenerator();
        List<ReportItem> reportItems = new List<ReportItem>();

        public ReverseLabelCheck()
        {
            InitializeComponent();
        }

        private void buttonBrowseSepro1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "SEPro (*.sepr)|*.sepr";
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBoxSEProL1.Text = openFileDialog1.FileName;
            }
        }

        private void buttonBrowseSepro2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "SEPro (*.sepr)|*.sepr";
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBoxSEProL2.Text = openFileDialog1.FileName;
            }
        }


        private void buttonGO_Click(object sender, EventArgs e)
        {

            //Load the SEPro files

            reportItems.Clear();
            List<List<ResultPackage>> theConditions = new List<List<ResultPackage>>();
            theConditions.Add(LoadSEProFiles(textBoxSEProL1.Text));
            theConditions.Add(LoadSEProFiles(textBoxSEProL2.Text));

            
            //Normalize for each condition
            if (checkBoxNormalize.Checked)
            {
                foreach (List<ResultPackage> condition in theConditions)
                {
                    foreach (ResultPackage rp in condition)
                    {
                        Console.WriteLine(" Normalizing package");
                        NormalizeQuantitation(rp);
                    }
                }
            }

            //Now lets obtain a list of all the peptides
            List<string> peptides = (from condition in theConditions
                                     from resultpckg in condition
                                     from peptide in resultpckg.MyProteins.AllCleanedPeptideSequences
                                     select peptide).Distinct().ToList();

            Console.WriteLine("Total peptides = " + peptides.Count );

            //Obtain the ratios
            //Works for only 2 conditions
            int quantitatedPeptides = 0;

            List<GraphData> theData = new List<GraphData>(peptides.Count);
            foreach (string peptide in peptides)
            {
                List<PeptideResult> pc1 = (from resultpckg in theConditions[0]
                                           from pep in resultpckg.MyProteins.MyPeptideList
                                           where pep.CleanedPeptideSequence.Equals(peptide)
                                           select pep).ToList();

                List<PeptideResult> pc2 = (from resultpckg in theConditions[1]
                                           from pep in resultpckg.MyProteins.MyPeptideList
                                           where pep.CleanedPeptideSequence.Equals(peptide)
                                           select pep).ToList();

                if (pc1.Count < 2 || pc2.Count < 2)
                {
                    continue;
                }

                


                List<double> q1 = new List<double>();
                foreach (PeptideResult p in pc1)
                {
                    q1.Add(PonderatedRatio(p).Average());
                }

                List<double> q2 = new List<double>();
                foreach (PeptideResult p in pc2)
                {
                    q2.Add(PonderatedRatio(p).Average());
                }

                theData.Add(new GraphData(Math.Log(q1.Average(), 2), Math.Log(q2.Average(), 2)));


                quantitatedPeptides++;
                Console.Write("Q ");
            }

            Console.WriteLine("\nQuantitated peptides: " + quantitatedPeptides);

            //Generate chart
            //And now, print the data to the graph
            chart1.Series[0].Points.Clear();

            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            //chart1.ChartAreas[0].AxisX.Crossing = 0;
            //chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Interval = 1;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";

            
            int removed = theData.RemoveAll(a => double.IsNaN(a.X) || double.IsNaN(a.Y) || double.IsInfinity(a.X) || double.IsInfinity(a.Y));
            Console.WriteLine("Datapoints removed = " + removed);


            foreach (GraphData g in theData)
            {
                chart1.Series[0].Points.AddXY(g.X, g.Y);
            }

            //Finally, update the labels
            labelL1.Text = Math.Round(theData.Average(a => a.X), 3).ToString() + " +- " + Math.Round(PatternTools.pTools.Stdev(theData.Select(a => a.X).ToList(), true), 3);
            labelL2.Text = Math.Round(theData.Average(a => a.Y), 3).ToString() + " +- " + Math.Round(PatternTools.pTools.Stdev(theData.Select(a => a.Y).ToList(), true), 3);


            //labelRSquared.Text = Math.Round(lstsqr.rSquare(),3).ToString();
            //labelRegressorEquation.Text = "y = " + Math.Round(lstsqr.aTerm(),2) + " x^2 + " + Math.Round(lstsqr.bTerm(),2) + " x + " + Math.Round(lstsqr.cTerm(),2);


            dataGridViewPoints.DataSource = theData;
            dataGridViewReport.DataSource = reportItems;


            

            
        }

        private void NormalizeQuantitation(ResultPackage rp)
        {
            //First lets normalize
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            foreach (PeptideResult peptide in rp.MyProteins.MyPeptideList)
            {
                
                X.AddRange(ExtractValues(peptide, 0));
                Y.AddRange(ExtractValues(peptide, 1));
                
            }

            double totalX = X.Sum();
            double totalY = Y.Sum();

            foreach (PeptideResult peptide in rp.MyProteins.MyPeptideList)
            {
                foreach (SQTScan scan in peptide.MyScans)
                {
                    for (int i = 0; i < scan.Quantitation.Count; i++)
                    {
                        scan.Quantitation[i][0] /= totalX;
                        scan.Quantitation[i][1] /= totalY;
                    }
                }
            }
            
        }

        private void Old()
        {
            //Load the SEPro files;




            PatternTools.LstSquQuadRegr lstsqr = new PatternTools.LstSquQuadRegr();

            Console.WriteLine("Loading SEPro1");
            ResultPackage sepro1 = ResultPackage.Load(textBoxSEProL1.Text);

            Console.WriteLine("Loading SEPro2");
            ResultPackage sepro2 = ResultPackage.Load(textBoxSEProL2.Text);

            //Generate a list of all clean peptide sequences
            List<string> cleanPeptideSequences = sepro1.AllPeptideSequences;
            cleanPeptideSequences.AddRange(sepro2.AllPeptideSequences);
            cleanPeptideSequences = cleanPeptideSequences.Distinct().ToList();
            Console.WriteLine(" Peptides loaded: " + cleanPeptideSequences.Count);


            //First lets normalize
            List<GraphData> theData = new List<GraphData>(cleanPeptideSequences.Count);
            double totalXP1 = 0;
            double totalXP2 = 0;
            double totalYP1 = 0;
            double totalYP2 = 0;
            foreach (string peptide in cleanPeptideSequences)
            {
                PeptideResult pep1 = sepro1.MyProteins.MyPeptideList.Find(a => a.CleanedPeptideSequence.Equals(peptide));
                PeptideResult pep2 = sepro2.MyProteins.MyPeptideList.Find(a => a.CleanedPeptideSequence.Equals(peptide));

                CorrectIsotopic(pep1);
                CorrectIsotopic(pep2);


                if (pep1 == null || pep2 == null)
                {
                    continue;
                }

                totalXP1 += ExtractValues(pep1, 0).Sum();
                totalYP1 += ExtractValues(pep1, 1).Sum();
                totalXP2 += ExtractValues(pep2, 0).Sum();
                totalYP2 += ExtractValues(pep2, 1).Sum();
            }

            foreach (string peptide in cleanPeptideSequences)
            {


                PeptideResult pep1 = sepro1.MyProteins.MyPeptideList.Find(a => a.CleanedPeptideSequence.Equals(peptide));
                PeptideResult pep2 = sepro2.MyProteins.MyPeptideList.Find(a => a.CleanedPeptideSequence.Equals(peptide));

                if (pep1 == null || pep2 == null)
                {
                    continue;
                }

                List<double> beforeNorm = ExtractRatios(pep1);
                if (pep1 == null || pep2 == null)
                {
                    continue;
                }

                foreach (SQTScan scan in pep1.MyScans)
                {
                    for (int i = 0; i < scan.Quantitation.Count; i++)
                    {
                        scan.Quantitation[i][0] /= totalXP1;
                        scan.Quantitation[i][1] /= totalYP1;
                    }
                }

                foreach (SQTScan scan in pep2.MyScans)
                {
                    for (int i = 0; i < scan.Quantitation.Count; i++)
                    {
                        scan.Quantitation[i][0] /= totalXP2;
                        scan.Quantitation[i][1] /= totalYP2;
                    }
                }
                List<double> afterNorm = ExtractRatios(pep1);

            }

            //finished Normalizing


            foreach (string peptide in cleanPeptideSequences)
            {
                PeptideResult pep1 = sepro1.MyProteins.MyPeptideList.Find(a => a.CleanedPeptideSequence.Equals(peptide));
                PeptideResult pep2 = sepro2.MyProteins.MyPeptideList.Find(a => a.CleanedPeptideSequence.Equals(peptide));


                if (pep1 == null || pep2 == null)
                {
                    continue;
                }

                List<double> q1 = ExtractRatios(pep1);
                List<double> q2 = ExtractRatios(pep2);

                if (q1.Count == 0 || q2.Count == 0)
                {
                    //Most likely we are here because this peptide is a false positive as it has two different labels in it.
                    Console.Write("x");
                    continue;
                }

                double x = 0;
                if (q1.Count > 0)
                {
                    x = q1.Average();
                }
                //x = q1[0];

                double y = 0;
                if (q2.Count > 0)
                {
                    y = q2.Average();
                }
                //y = q2[0];

                theData.Add(new GraphData(x, y));

                Console.Write(".");
            }

            //And now, print the data to the graph
 
            chart1.Series[0].Points.Clear();

            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;

            foreach (GraphData g in theData)
            {
                lstsqr.AddPoints(g.X, g.Y);
                chart1.Series[0].Points.AddXY(g.X, g.Y);
            }

            //Finally, update the labels
            labelL1.Text = Math.Round(theData.Average(a => a.X), 3).ToString() + " +- " + Math.Round(PatternTools.pTools.Stdev(theData.Select(a => a.X).ToList(), true), 3);
            labelL2.Text = Math.Round(theData.Average(a => a.Y), 3).ToString() + " +- " + Math.Round(PatternTools.pTools.Stdev(theData.Select(a => a.Y).ToList(), true), 3);


            //labelRSquared.Text = Math.Round(lstsqr.rSquare(),3).ToString();
            //labelRegressorEquation.Text = "y = " + Math.Round(lstsqr.aTerm(),2) + " x^2 + " + Math.Round(lstsqr.bTerm(),2) + " x + " + Math.Round(lstsqr.cTerm(),2);


            dataGridViewPoints.DataSource = theData;
        }

        private List<ResultPackage> LoadSEProFiles(string directory)
        {
            List<ResultPackage> theFiles = new List<ResultPackage>();
            string[] SEProFiles = Directory.GetFiles(directory, "*.sepr");

            foreach (string file in SEProFiles)
            {
                Console.WriteLine("Loading file: " + file);
                theFiles.Add(ResultPackage.Load(file));
            }

            return theFiles;
            
        }

        private void CorrectIsotopic(PeptideResult pep)
        {


            int result = (int) Math.Round( (pep.MyScans[0].MeasuredMH - pep.MyScans[0].TheoreticalMH), 0);
            List<double> signal = isotopicSignalGenerator.GetSignal(15, pep.MyScans[0].MeasuredMH, 0);
            foreach (SQTScan s in pep.MyScans)
            {
                int noMods = Regex.Matches(s.PeptideSequenceCleaned, Regex.Escape("(")).Count;
                foreach (List<double> q in s.Quantitation)
                {

                }
            }

            

            Console.Write(".");

            //Count how many deltas are there in the peptide          

        }


        private static List<double> ExtractValues(PeptideResult pep, int dim)
        {
            List<double> q1 = (from sqts in pep.MyScans
                               from quants in sqts.Quantitation
                               select quants[dim]).ToList();

            q1.RemoveAll(a => double.IsInfinity(a) || double.IsNaN(a));

            return q1;
        }


        private static List<double> ExtractRatios(PeptideResult pep)
        {
            List<double> q1 = (from sqts in pep.MyScans
                               from quants in sqts.Quantitation
                               select Math.Log(quants[0] / quants[1], 2)).ToList();

            q1.RemoveAll(a => double.IsInfinity(a) || double.IsNaN(a));

            for (int i = 0; i < q1.Count; i++)
            {
                if (q1[i] > 15)
                {
                    q1[i] = 15;
                }
                else if (q1[i] < -15)
                {
                    q1[i] = -15;
                }
            }

            return q1;
        }

        private List<double> PonderatedRatio(PeptideResult pep)
        {
            List<double> ratios = new List<double>();
            foreach (SQTScan s in pep.MyScans)
            {
                double weightSum = 0;
                double numerator = 0;
                foreach (List<double> quants in s.Quantitation)
                {
                    double w = quants[0] + quants[1];
                    numerator += ((quants[0] / quants[1]) * w);
                    weightSum += w;

                    ReportItem ri = new ReportItem(s.FileName, s.ScanNumber, s.ChargeState, quants[0], quants[1], Math.Log(quants[0] / quants[1], 2), s.PeptideSequence);
                    reportItems.Add(ri);          
                }

                ratios.Add(numerator / weightSum);

                
            }



               

            return ratios;
        }

    }
}
