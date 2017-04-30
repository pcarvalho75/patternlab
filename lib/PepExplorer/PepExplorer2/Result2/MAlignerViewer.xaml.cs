using MAligner.MAligner;
using MicroChart;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PatternTools;
using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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

namespace PepExplorer2.Result2
{
    /// <summary>
    /// Interaction logic for MAlignerViewer.xaml
    /// </summary>
    public partial class MAlignerViewer : UserControl
    {
        public ResultPckg2 MyResultPackage { get; set; }
        Aligner mAligner;

        public MAlignerViewer()
        {
            InitializeComponent();

            KeyValuePair<int[,], Dictionary<char, int>> m = MAligner.Utils.LoadSubstitutionMatrixFromString(PepExplorer2.Properties.Resources.PAM30MS);
            mAligner = new Aligner(m);
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            List<object> ProtView = new List<object>();
            
            int reverseCounter = 0;

            List<string> acceptableProtIDs = MyResultPackage.MaxParsimonyProteinList;

            foreach (FastaItem fi in MyResultPackage.MyFasta)
            {
                if ((bool)CheckBoxMaxPars.IsChecked)
                {
                    if (!MyResultPackage.MaxParsimonyProteinList.Contains(fi.SequenceIdentifier))
                    {
                        continue;
                    }
                }

                if (TextBoxSearch.Text.Length > 0 && (bool)RadioDescriptionSearch.IsChecked)
                {
                    if (!fi.Description.Contains(TextBoxSearch.Text))
                    {
                        continue;
                    }
                }

                List<AlignmentResult> alignments = MyResultPackage.Alignments.FindAll(a => a.ProtIDs.Contains(fi.SequenceIdentifier));

                if (alignments.Count < (int)IntegerUpDownMinimumNumberOfAlignments.Value)
                {
                    continue;
                }

                int specCounts = alignments.Sum(a => a.DeNovoRegistries.Count);
                
                if (fi.SequenceIdentifier.StartsWith("Reverse"))
                {
                    reverseCounter++;
                }

                ProtView.Add(new { 
                    ID = fi.SequenceIdentifier,
                    Alignments = alignments.Count,
                    SpecCounts = specCounts,
                    Unique = alignments.FindAll(z => z.ProtIDs.Count == 1).Count,
                    Coverage = Math.Round(fi.Coverage(GetDBSequences(fi)),3),
                    CoverageChart = new KeyValuePair<string, List<string>>(fi.Sequence, GetDBSequences(fi)),
                    Description = fi.Description }
                    );
            }

           var unmapped = MyResultPackage.MyUnmappedRegistries.Select(a =>
                new
                {
                    Sequence = a.Key,
                    SpecCount = a.Value.Count,
                    BestDeNovoScore = a.Value.Select(b => b.DeNovoScore).Max(),
                    DeNovoScore_FileName_ScanNo = string.Join(", ", a.Value.Select(b => b.DeNovoScore + "-" + b.FileName + "- " + b.ScanNumber).ToList())
                });

            DataGridProteins.ItemsSource = ProtView;
            DataGridUnmappedAlignments.ItemsSource = unmapped;


            LabelProteins.Content = reverseCounter + " / " + ProtView.Count + " = " + Math.Round((double)reverseCounter / (double)ProtView.Count, 3);
            LabelAlignments.Content = MyResultPackage.Alignments.Count;

            UpdateScoreDistributionPlot(MyResultPackage.GenerateSparseMatrix());

            //Search Parameters
            TextBlockSearchParameters.Text = "\nSearch Parameters\n\n";
            TextBlockSearchParameters.Text += "Database File: " + MyResultPackage.Arguments.DataBaseFile + "\n";
            TextBlockSearchParameters.Text += "Decoy Label: " + MyResultPackage.Arguments.DecoyLabel + "\n";
            TextBlockSearchParameters.Text += "De novo search engine: " + MyResultPackage.Arguments.DeNovoOption + "\n";
            TextBlockSearchParameters.Text += "De novo result directory: " + MyResultPackage.Arguments.DeNovoResultDirectory + MyResultPackage.Arguments.MPexResultPath + "\n";
            TextBlockSearchParameters.Text += "Minimum de novo score: " + MyResultPackage.Arguments.MinDeNovoScore + "\n";
            TextBlockSearchParameters.Text += "Minimum Identity for searching: " + MyResultPackage.Arguments.MinIdentity + "\n";
            TextBlockSearchParameters.Text += "Required no amino acids for peptide: " + MyResultPackage.Arguments.PeptideMinNumAA + "\n";
            TextBlockSearchParameters.Text += "Substitution Matrix: " + MyResultPackage.Arguments.SubstitutionMatrix + "\n";


        }

        private void UpdateScoreDistributionPlot(SparseMatrix sparseMatrix)
        {
            //Make a plot
            PlotModel MyModel = new PlotModel();

            var scatterSeriesBlue = new ScatterSeries();
            scatterSeriesBlue.MarkerStroke = OxyColors.DarkBlue;
            scatterSeriesBlue.MarkerFill = OxyColors.LightBlue;
            scatterSeriesBlue.MarkerType = MarkerType.Circle;
            scatterSeriesBlue.Title = "Target";


            var scatterSeriesRed = new ScatterSeries();
            scatterSeriesRed.MarkerStroke = OxyColors.DarkRed;
            scatterSeriesRed.MarkerFill = OxyColors.LightSalmon;
            scatterSeriesRed.MarkerType = MarkerType.Circle;
            scatterSeriesRed.Title = "Decoy";

            var linearAxisY = new LinearAxis();
            linearAxisY.MinorGridlineStyle = LineStyle.Dot;
            linearAxisY.Title = "De novo score";
            linearAxisY.Position = AxisPosition.Left;

            var linearAxisX = new LinearAxis();
            linearAxisX.MinorGridlineStyle = LineStyle.Dot;
            linearAxisX.Title = "(Similarity score) / (Sequence length)";
            linearAxisX.Position = AxisPosition.Bottom;

            MyModel.Axes.Add(linearAxisY);
            MyModel.Axes.Add(linearAxisX);



            foreach (sparseMatrixRow smr in sparseMatrix.theMatrixInRows)
            {

                //similarity, de novo
                ScatterPoint sp = new ScatterPoint(smr.Values[0], smr.Values[1]);

                if (smr.Lable == 1)
                {
                    scatterSeriesBlue.Points.Add(sp);
                }
                else
                {
                    scatterSeriesRed.Points.Add(sp);
                }

                //e.ToolTip += smr.FileName + "\n";
                //e.ToolTip += "x (Similarity): " + smr.Values[0] + " y (de novo): " + smr.Values[1];

            }

            MyModel.Series.Add(scatterSeriesBlue);
            MyModel.Series.Add(scatterSeriesRed);

            MyPlot.Model = MyModel;
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.DefaultExt = ".mpex";
            sfd.Filter = "MAligner PepExplorer File (*.mpex)|*.mpex"; 

            Nullable<bool> result = sfd.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // SaveDocument
                MyResultPackage.SerializeResultPackage(sfd.FileName);
                MessageBox.Show("File saved.");
            }
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".mpex";

            ofd.Filter = "MAligner PepExplorer File (*.mpex)|*.mpex";

            Nullable<bool> result = ofd.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                LoadResult(ofd.FileName);
            }
        }

        public void LoadResult(string fileName)
        {
            // OpenDocument
            MyResultPackage = ResultPckg2.DeserializeResultPackage(fileName);
            UpdateDisplay();
        }

        private void DataGridProteins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object theSelection = DataGridProteins.SelectedItem;
            Dictionary<string, object> dict = theSelection.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(theSelection, null));

            FastaItem thisProtein = MyResultPackage.MyFasta.Find(a => a.SequenceIdentifier.Equals(dict["ID"]));

            List<AlignmentResult> alns = MyResultPackage.Alignments.FindAll(a => a.ProtIDs.Contains(dict["ID"]));
       
            DisplayPeptides(alns, thisProtein);

        }

        private void DisplayPeptides(List<AlignmentResult> alns, FastaItem thisProtein = null)
        {
            List<object> displayItems = new List<object>();
            foreach (AlignmentResult aln in alns)
            {
                string dbPep = "NA";

                if (thisProtein != null)
                {
                    dbPep = mAligner.GetClosestPeptideInASequence(PatternTools.pTools.CleanPeptide(aln.DeNovoRegistries[0].PtmSequence, true).ToCharArray(), thisProtein.Sequence.ToCharArray());
                } else if (thisProtein == null && aln.ProtIDs.Count > 0)
                {
                    List<string> pepsInDB = new List<string>();

                    foreach (string p in aln.ProtIDs)
                    {
                        FastaItem aProt = MyResultPackage.MyFasta.Find(a => a.SequenceIdentifier.Equals(p));
                        pepsInDB.Add(mAligner.GetClosestPeptideInASequence(PatternTools.pTools.CleanPeptide(aln.DeNovoRegistries[0].PtmSequence, true).ToCharArray(), aProt.Sequence.ToCharArray()));
                    }

                    pepsInDB = pepsInDB.Distinct().ToList();

                    dbPep = string.Join(", ", pepsInDB);
                }

                
                foreach (DeNovoR dnvr in aln.DeNovoRegistries)
                {
                    byte[] peptideBytes = Encoding.ASCII.GetBytes(PatternTools.pTools.CleanPeptide(dnvr.PtmSequence, true));
                    int IDScore = -1;

                    if (thisProtein != null)
                    {
                        IDScore = mAligner.IDScore(peptideBytes, thisProtein.SequenceInBytes);
                    }

                    displayItems.Add(new
                    {
                        FileName = dnvr.FileName,
                        ScanNo = dnvr.ScanNumber,
                        Z = dnvr.Charge,
                        Redundancy = aln.ProtIDs.Count,
                        DeNovoScore = dnvr.DeNovoScore,
                        DeNovoSeq = dnvr.PtmSequence,
                        DBSeq = dbPep,
                        Similarity = aln.SimilarityScore,
                        //Identity = IDScore + " (" + Math.Round(((double)IDScore / (double)PatternTools.pTools.CleanPeptide(dnvr.PtmSequence, true).Length), 3) + ")"
                    });

                }
            }

            DataGridAln.ItemsSource = displayItems;
        }

        private List<string> GetDBSequences(FastaItem fi)
        {
            List<AlignmentResult> alns = MyResultPackage.Alignments.FindAll(a => a.ProtIDs.Contains(fi.SequenceIdentifier));

            List<string> dnvPeptides = (from a in alns
                                        from d in a.DeNovoRegistries
                                        select PatternTools.pTools.CleanPeptide(d.PtmSequence, true)).Distinct().ToList();


            List<string> sequencePeptides = (from dnv in dnvPeptides
                                             select mAligner.GetClosestPeptideInASequence(dnv.ToArray(), fi.Sequence.ToCharArray())).Distinct().ToList();

            return sequencePeptides;

        }

        private void ButtonPeptideSearch_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)RadioPeptideSearch.IsChecked)
            {
                List<AlignmentResult> results = MyResultPackage.Alignments.FindAll(a => a.DeNovoRegistries[0].PtmSequence.Contains(TextBoxSearch.Text));

                if (results.Count > 0)
                {
                    DisplayPeptides(results);
                } else 
                {
                    MessageBox.Show("No peptides were found with the following sequence: " + TextBoxSearch.Text);
                }
                
            }
            else if ((bool)RadioDescriptionSearch.IsChecked)
            {
                UpdateDisplay();

            } else
            {
                throw new Exception("Please select a search mode.");
            }
        }

        private void DataGridProteins_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("CoverageChart"))
            {
                //Create a new teamplate column.
                DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                templateColumn.Header = e.Column.Header;
                templateColumn.MinWidth = 160;

                DataTemplate dt = new DataTemplate();
                FrameworkElementFactory fef = new FrameworkElementFactory(typeof(CoverageChart));

                fef.SetBinding(CoverageChart.SequenceAndPeptidesProperty, new Binding("CoverageChart"));

                dt.VisualTree = fef;
                templateColumn.CellTemplate = dt;
                e.Column = templateColumn;
            }
        }

        private void MenuItemExportResult_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".txt";

            sfd.Filter = "Text File (*.txt)|*.txt";

            Nullable<bool> result = sfd.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);
                sw.WriteLine("#PROT\tID\tAlignments\tSpecCounts\tUnique\tCoverage\tDescription");
                sw.WriteLine("#ALN\tFileName\tScanNo\tZ\tRedundancy\tDeNovoScore\tDeNovoSeq\tDBSeq\tSimilarityScore\tIdentity");
                foreach (var prot in DataGridProteins.ItemsSource) 
                {
                    Dictionary<string, object> protDic = pTools.AnonymousToDictionary(prot);

                    sw.WriteLine("PROT\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}", protDic["ID"], protDic["Alignments"], protDic["SpecCounts"], protDic["Unique"], protDic["Coverage"], protDic["Description"]);

                    List<AlignmentResult> alns = MyResultPackage.Alignments.FindAll(a => a.ProtIDs.Contains(protDic["ID"]));
                    FastaItem thisProtein = MyResultPackage.MyFasta.Find(a => a.SequenceIdentifier.Equals(protDic["ID"]));

                    foreach (AlignmentResult aln in alns)
                    {
                        
                        string dbPep = mAligner.GetClosestPeptideInASequence(PatternTools.pTools.CleanPeptide(aln.DeNovoRegistries[0].PtmSequence, true).ToCharArray(), thisProtein.Sequence.ToCharArray());
                        foreach (DeNovoR dnvr in aln.DeNovoRegistries)
                        {
                            byte[] peptideBytes = Encoding.ASCII.GetBytes(PatternTools.pTools.CleanPeptide(dnvr.PtmSequence, true));
                            int IDScore = mAligner.IDScore(peptideBytes, thisProtein.SequenceInBytes);

                            string idScore = IDScore + " (" + Math.Round(((double)IDScore / (double)PatternTools.pTools.CleanPeptide(dnvr.PtmSequence, true).Length), 3) + ")";
                            sw.WriteLine("ALN\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", dnvr.FileName, dnvr.ScanNumber, dnvr.Charge, aln.ProtIDs.Count, dnvr.DeNovoScore, dnvr.PtmSequence, dbPep, aln.SimilarityScore, idScore);
                        }
                    }
                }

                sw.Close();
                Console.WriteLine("Results Exported.");
            }
        }

        private void DataGridProteins_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }

        private void MenuItemExportUnmappedAlignmentsToBlast_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.DefaultExt = ".fasta";
            sfd.Filter = "FASTA (*.fasta)|*.fasta";

            Nullable<bool> result = sfd.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // SaveDocument
                MyResultPackage.SaveUnmappedRegistries(sfd.FileName);
                MessageBox.Show("File saved.");
            }

        }

        private void DataGridProteins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object theSelection = DataGridProteins.SelectedItem;
            Dictionary<string, object> dict = theSelection.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(theSelection, null));

            FastaItem thisProtein = MyResultPackage.MyFasta.Find(a => a.SequenceIdentifier.Equals(dict["ID"]));

            List<AlignmentResult> alns = MyResultPackage.Alignments.FindAll(a => a.ProtIDs.Contains(dict["ID"]));

            //We need to find the alignment positions of each alignment with this protein
            List<Tuple<List<int>, string>> toPlot = new List<Tuple<List<int>, string>>();

            //We need to feed the alignment List          
            KeyValuePair<int[,], Dictionary<char, int>> m = MAligner.Utils.LoadSubstitutionMatrixFromString(PepExplorer2.Properties.Resources.PAM30MS);
            Aligner mAligner = new Aligner(m);

            foreach (AlignmentResult aln in alns)
            {
                string cleanPeptideSequence = PatternTools.pTools.CleanPeptide(aln.DeNovoRegistries[0].PtmSequence, true);
                List<int> alnScores = mAligner.Align(cleanPeptideSequence.ToCharArray(), thisProtein.Sequence.ToCharArray()).ToList();
                int maxNo = alnScores.Max();
                List<int> pos = new List<int>();
                
                for (int i = 0; i < alnScores.Count; i++)
                {
                    if (alnScores[i] == maxNo)
                    {
                        pos.Add(i);
                    }
                }

                toPlot.Add(new Tuple<List<int>, string>(pos, cleanPeptideSequence));    
            }


            Forms.ProteinCoverageViewForm pcvf = new Forms.ProteinCoverageViewForm();
            pcvf.MyProteinCoverageView.Plot(thisProtein, toPlot);
            pcvf.ShowDialog();

        }
    }
}
