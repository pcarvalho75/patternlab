using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace UniQ
{
    /// <summary>
    /// Interaction logic for RatioOfRatio.xaml
    /// </summary>
    public partial class RatioOfRatio : Window
    {

        List<ProtQuant> myProtQuants = new List<ProtQuant>();
        List<ProtQuant> validProts = new List<ProtQuant>();
        Dictionary<string, List<double>> peptideRatioDict;

        public RatioOfRatio()
        {
            InitializeComponent();
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            List<string> peptideFiles = new List<string>();
            

            try
            {
                peptideFiles = Directory.GetFiles(TextBoxDir.Text, "*.txt").ToList();
            } catch (Exception e3)
            {
                MessageBox.Show(e3.Message);
                return;
            }

            myProtQuants = new List<ProtQuant>();

            foreach (string peptideFile in peptideFiles)
            {
                try {
                    KeyValuePair<List<FastaItem>, List<PepQuant>> result = UniQ.UniQTools.ParsePeptideReport(peptideFile);

                    //This could be a useful way to return future pepQuant reports
                    foreach (FastaItem fi in result.Key)
                    {
                        if (!myProtQuants.Exists(a => a.MyFastaItem.SequenceIdentifier.Equals(fi.SequenceIdentifier)))
                        {
                            myProtQuants.Add(new ProtQuant(fi));
                        }
                    }

                    foreach (PepQuant pq in result.Value)
                    {
                        List<ProtQuant> pqs = myProtQuants.FindAll(a => a.MyFastaItem.Sequence.Contains(pq.CleanPeptideSequence));
                        pq.MappableProteins = pqs.Select(a => a.MyFastaItem.SequenceIdentifier).ToList();

                        foreach (ProtQuant protQ in pqs)
                        {
                            protQ.MyPepQuants.Add(pq);
                        }

                        
                    }
                } catch
                {
                    Console.WriteLine("Unable to parse file " + peptideFile);
                }

            }

            GridRow0.IsEnabled = false;

            if (myProtQuants.Count > 0)
            {
                UpdatePanel();
            }
            
        }

        private void UpdatePanel()
        {
            int redundancy = int.MaxValue;
            if ((bool)CheckBoxUnique.IsChecked)
            {
                redundancy = 1;
            }

            if (myProtQuants.Count == 0) { return; }

            peptideRatioDict = new Dictionary<string, List<double>>();
            List<int> theLabels = Regex.Split(TextBoxLabels.Text, " ").Select(a => int.Parse(a)).ToList();


            foreach (ProtQuant protQ in myProtQuants) 
            {
                foreach (PepQuant pq in protQ.MyPepQuants)
                {
                    if (redundancy == 1 && pq.MappableProteins.Count > 1)
                    {
                        continue;
                    }

                    if (!peptideRatioDict.ContainsKey(pq.Sequence))
                    {
                        List<double> ratios = new List<double>();

                        List<Quant> goodQuants = (from q in pq.MyQuants
                                                  where q.MarkerIntensities.Count(b => b == 0) == 0
                                                  select q).ToList();

                        if (goodQuants.Count > 0)
                        {

                            foreach (Quant q in pq.MyQuants)
                            {
                                double numerator = q.MarkerIntensities[theLabels[1]] / q.MarkerIntensities[theLabels[0]];
                                double denominator = q.MarkerIntensities[theLabels[3]] / q.MarkerIntensities[theLabels[2]];
                                double ratio = numerator / denominator;

                                ratios.Add(ratio);
                            }

                            peptideRatioDict.Add(pq.Sequence, ratios);
                            List<double> logFolds = ratios.Select(a => Math.Log(a)).ToList();
                            

                            pq.AVGLogFold = logFolds.Average();

                            double bothTails;
                            double leftTail;
                            double rightTail;

                            alglib.studentttest1(logFolds.ToArray(), logFolds.Count, 0, out bothTails, out leftTail, out rightTail);

                            if (bothTails < 0.01)
                            {
                                bothTails = 0.01;
                            }

                            pq.TTest = bothTails;
                        }
                        else
                        {
                            pq.TTest = 0.01;
                        }

                    }
                }
                

            }


            validProts = (from p in myProtQuants
                          select new ProtQuant(p.MyFastaItem, p.MyPepQuants.FindAll(a => a.MappableProteins.Count <= redundancy))
                                          ).ToList().FindAll(b => b.MyPepQuants.Count >= MinPeptides.Value).ToList();



            var panelData = from protQ in validProts

                            select new
                            {
                                Locus = protQ.MyFastaItem.SequenceIdentifier,
                                SequenceCount = protQ.MyPepQuants.Count,
                                UniquePeptides = protQ.MyPepQuants.Count(a => a.MappableProteins.Count == 1),
                                AverageLogRatioOfRatios = protQ.MyPepQuants.Average(a => a.AVGLogFold),
                                Stouffers = CalculateStouffers(protQ, redundancy),
                                Description = protQ.MyFastaItem.Description
                            };

            LabelProteins.Content = validProts.Count;
            DataGridProteins.ItemsSource = panelData;            
        }

        private double CalculateStouffers(ProtQuant protQ, int peptideRedundancy)
        {

            //lets skip quants that have 0
            List<double> p_Revised = new List<double>();
            List<double> w_Revised = new List<double>();

            foreach (PepQuant pq in protQ.MyPepQuants)
            {
                if (pq.MappableProteins.Count <= peptideRedundancy)
                {
                    if (!double.IsNaN(pq.TTest) && pq.MyQuants.Count > 0)
                    {
                        p_Revised.Add(pq.TTest);
                        w_Revised.Add(pq.MyQuants.Count);
                    }
                }
            }

            if (p_Revised.Count == 0) { return 0.5; }

            double stouffers = PatternTools.pTools.StouffersMethod(p_Revised, w_Revised, 1);
            return stouffers;

        }

        private void CheckBoxUnique_Click(object sender, RoutedEventArgs e)
        {
            UpdatePanel();
        }

        private void MinPeptides_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdatePanel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter report = new StreamWriter(TextBoxDir.Text + "/" + "out.txt");

            int redundancy = int.MaxValue;
            if ((bool)CheckBoxUnique.IsChecked)
            {
                redundancy = 1;
            }

            report.WriteLine("#Identifier\tAverageLogFold\tStouffers\tPeptideCount\tSpectrumCount\tDescription");
            report.WriteLine("#\tAvgLogFold\tSingleSampleTTest\tSpecCounts\tSequence");
            report.WriteLine("#\t\tSpectrum Number\tSignals");


            foreach (ProtQuant pq in validProts)
            {
                int uniquePeps = pq.MyPepQuants.FindAll(a => a.MappableProteins.Count <= redundancy).Count;
                if (uniquePeps == 0) { continue; }

                double stouffers = CalculateStouffers(pq, redundancy);
                if (stouffers < 0.01) { stouffers = 0.01; }

                report.WriteLine(pq.MyFastaItem.SequenceIdentifier + "\t" + Math.Round(pq.MyPepQuants.Average(a => a.AVGLogFold), 4) + stouffers, pq.MyPepQuants.Count, pq.MyPepQuants.Sum(a => a.MyQuants.Count), pq.MyFastaItem.Description );

                foreach (PepQuant pepQuant in pq.MyPepQuants)
                {
                    if (pepQuant.MappableProteins.Count > redundancy) { continue; }

                    report.WriteLine("\t" + Math.Round(pepQuant.AVGLogFold, 4) + "\t" + pepQuant.TTest + "\t" + pepQuant.MyQuants.Count + "\t" + pepQuant.Sequence);

                    foreach (Quant q in pepQuant.MyQuants)
                    {
                        report.WriteLine("\t\t" + q.FileName + "\t" + q.ScanNumber + "\t" + q.Z + "\t" + string.Join("\t", q.MarkerIntensities));
                    }
                }
            }

            

            report.Close();

            MessageBox.Show("Report saved to out.txt on the same input directory.");
            Console.WriteLine("These peptides had missing values so ratios could not be obtained:");
        }
    }


}
