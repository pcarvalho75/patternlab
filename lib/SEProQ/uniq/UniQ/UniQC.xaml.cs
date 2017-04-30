using MicroChart;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PatternTools;
using PatternTools.FastaParser;
using PLP;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace UniQ
{
    /// <summary>
    /// Interaction logic for UniQC.xaml
    /// </summary>
    /// 

    public partial class UniQC : UserControl
    {
        static string peptideQuantitationReportFile = null;

        public string MyClassLabelsTextBoxText
        {
            set { TextBoxClassLabel.Text = value; }
        }

        public string MyPeptideQuantitationReportTextBoxText
        {
            set
            {
                TextBoxQuantitationReport.Text = value;
            }
        }

        Dictionary<FastaItem, List<PepQuant>> protPepDict;
        public List<PepQuant> MyPeptides { get; set; }  // Columns : Sequence, SpecCount, pvalue
        private List<PepQuant> myPeptidesTMP { get; set; }


        public List<FastaItem> MyFastaItems { get; set; }


        Dictionary<FastaItem, List<PepQuant>> protPepDictTMP = new Dictionary<FastaItem, List<PepQuant>>();

        List<int> classLabels = null;

        public UniQC()
        {
            InitializeComponent();
       
        }

        private async void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            CHeckBoxShowPeptidesOfFilteredProteinsOnly.IsEnabled = true;

            

            #region Retrieve class lables
            if (TextBoxClassLabel.Text.Length > 0)
            {
                try
                {
                    classLabels = Regex.Split(TextBoxClassLabel.Text, " ").Select(a => int.Parse(a)).ToList();

                    if (classLabels.Select(a => a > 0).Distinct().Count() > 2)
                    {
                        MessageBox.Show("Only two classes are acceptable.");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Please enter valid class labels. Makes sure no extra spaces are included.");
                    return;
                }
            }
            #endregion

            //Some error checking
            if (classLabels == null)
            {
                MessageBox.Show("Error for retrieving class labels.");
                return;
            }


            //Give the wait window
            WaitWindowWPF.Visibility = Visibility.Visible;
            peptideQuantitationReportFile = TextBoxQuantitationReport.Text;
            await ProcessPeptideQuantitationReport();

            //Remove the wait window
            WaitWindowWPF.Visibility = Visibility.Collapsed;

            UpdateGUI();

            TabItemResultBrowser.IsSelected = true;
            MenuItemExportProteinResults.IsEnabled = true;
            MenuItemExporToPLP.IsEnabled = true;
            MenuItemExporToXLS.IsEnabled = true;
        }

        private async Task ProcessPeptideQuantitationReport()
        {
            await Task.Run(() =>  ProcessPeptideQuantitationReport2());

        }

        private void ProcessPeptideQuantitationReport2()
        {
            KeyValuePair<List<FastaItem>, List<PepQuant>> kvp = UniQTools.ParsePeptideReport(peptideQuantitationReportFile);

            //Some error checking
            if (classLabels.Count > kvp.Value[0].MyQuants[0].MarkerIntensities.Count)
            {
                MessageBox.Show("Parameter Min no Quants per reading cannot be greater than number of channels specidied in the class menu.");
                return;
            }

            if (classLabels.Count != kvp.Value[0].MyQuants[0].MarkerIntensities.Count)
            {
                MessageBox.Show("Make sure you properly labeled your class labels followed with spaces as they do not match those from the peptide quantitation report.");
                return;
            }

            UniQTools.ScorePepQuants(classLabels, kvp.Value);

            MyPeptides = kvp.Value;
            MyFastaItems = kvp.Key;

            protPepDict = new Dictionary<FastaItem, List<PepQuant>>();


            //Parallel.ForEach(MyFastaItems, fi =>
            foreach (FastaItem fi in MyFastaItems)
            {
                List<PepQuant> pepts = MyPeptides.FindAll(a => fi.Sequence.Contains(a.CleanPeptideSequence));

                if (pepts.Count > 0)
                {
                    protPepDict.Add(fi, pepts);
                }

                foreach (PepQuant pq in pepts)
                {
                    pq.MappableProteins.Add(fi.SequenceIdentifier);
                }
            }
            //);           

            return;
        }

        private void TextBoxQuantitationReport_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxQuantitationTextChanged();
        }

        private void TextBoxQuantitationTextChanged()
        {
            if (File.Exists(TextBoxQuantitationReport.Text) && !String.IsNullOrEmpty(TextBoxClassLabel.Text))
            {
                ButtonGo.IsEnabled = true;
            }
            else
            {
                ButtonGo.IsEnabled = false;
            }
        }

        private void ButtonBrowsePeptideQuantitationReport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.DefaultExt = "*.txt";
            ofd.Filter = "Text File (*.txt)|*.txt";

            if (ofd.ShowDialog() == true)
            {
                TextBoxQuantitationReport.Text = ofd.FileName;
            }

        }

        public void UpdateGUI()
        {
            //Lets take care of which peptides to consider in our analysis
            if ((bool)CheckBoxConsiderOnlyUniquePeptides.IsChecked)
            {
                myPeptidesTMP = MyPeptides.FindAll(a => a.MappableProteins.Count == 1);
            }
            else
            {
                myPeptidesTMP = MyPeptides.FindAll(a => a.MappableProteins.Count > 0);
            }

            int removedFold = myPeptidesTMP.RemoveAll(a => Math.Abs(a.AVGLogFold) < Math.Round((double)DoubleUpDownPeptideLogFold.Value, 2));

            ///Need to fix here....
            int removedProb1 = myPeptidesTMP.RemoveAll(a => (a.TTest > Math.Round((double)DoubleUpDownPeptidePValueCutoff.Value, 2)));

            List<PepQuant> survival = MyPeptides.FindAll(a => !myPeptidesTMP.Exists(b => b.Sequence.Equals(a.Sequence)));

            protPepDictTMP = new Dictionary<FastaItem, List<PepQuant>>();

            //New way
            foreach (FastaItem fi in MyFastaItems)
            {
                List<PepQuant> pepts = myPeptidesTMP.FindAll(a => a.MappableProteins.Contains(fi.SequenceIdentifier));
                if (pepts.Count > 0)
                {
                    protPepDictTMP.Add(fi, pepts);
                }
            }

            try
            {

                var protDisplay = from kvp in protPepDictTMP
                                  where kvp.Value.Count >= IntegerUpDown.Value
                                  select new
                                  {
                                      ProtID = kvp.Key.SequenceIdentifier,
                                      Daltons = Math.Round(kvp.Key.MonoisotopicMass, 2),
                                      PeptideCount = kvp.Value.Count,
                                      UniquePeptideCount = kvp.Value.Count(a => a.MappableProteins.Count == 1),
                                      SpecCount = protPepDictTMP[kvp.Key].Sum(a => a.MyQuants.Count),
                                      AvgLogFold = Math.Round(kvp.Value.Average(a => a.AVGLogFold), 3),
                                      StouffersPValue = ProteinPValue(kvp.Value),
                                      Coverage = GenerateMicroChart(kvp),
                                      Description = kvp.Key.Description
                                  };

                int noProteins = protDisplay.Count();
                LabelNoProteins.Content = noProteins;

                if (noProteins == 0)
                {
                    MessageBox.Show("No protein satisfies current constraints.");
                    return;
                }

                //Find out how many peptides and spec counts are there
                
                List<PepQuant> validPepQuants = new List<PepQuant>();

                if ((bool)CHeckBoxShowPeptidesOfFilteredProteinsOnly.IsChecked)
                {
                    List<string> IDsProteinsDisplay = protDisplay.Select(a => a.ProtID).ToList();
                    foreach (string pID in IDsProteinsDisplay)
                    {
                        FastaItem fi = MyFastaItems.Find(a => a.SequenceIdentifier.Equals(pID));
                        validPepQuants.AddRange(protPepDictTMP[fi]);
                    }

                    validPepQuants = validPepQuants.Distinct().ToList();
                } else
                {
                    validPepQuants = MyPeptides;
                }

                DataGridAllPeptidesView.ItemsSource = validPepQuants.Select
                    (a => new
                    {
                        Sequence = a.Sequence,
                        Binomial_PValue = Math.Round(a.BinomialProbability, 4),
                        Paired_TTest_Pvalue = Math.Round(a.TTest, 4),
                        AVGLogFold = Math.Round(a.AVGLogFold, 4),
                        Redundancy = a.MappableProteins.Count,
                        SpecCount = a.MyQuants.Count,
                        MissingValues = a.MissingValues,
                        MappableProts = string.Join(" ", (
                                                        from prot in protDisplay
                                                        where a.MappableProteins.Contains(prot.ProtID)
                                                        select "(" + prot.ProtID + "::" + prot.Description + ")").ToList()
                                                        )
                    }
                );


                LabelNoPeptides.Content = validPepQuants.Count();
                LabelSpecCount.Content = validPepQuants.Sum(a => a.MyQuants.Count);
                LabelAvgFilteredPepLogFold.Content = Math.Round(validPepQuants.Average(a => a.AVGLogFold), 3);

                DataGridProteinView.ItemsSource = protDisplay;


                myPeptidesTMP = validPepQuants;

                double correctedP = PatternTools.pTools.BenjaminiHochbergFDR(0.01, protDisplay.Select(a => a.StouffersPValue).ToList(), false);
                LabelCorrectedP.Content = correctedP;

                LabelTotalPeptides.Content = MyPeptides.Count;
            }
            catch (Exception) { return; }
            //Update the graphs
            PlotQuants();

        }

        private KeyValuePair<string, Dictionary<string, double>> GenerateMicroChart(KeyValuePair<FastaItem, List<PepQuant>> kvp)
        {
            Dictionary<string, double> quantData = new Dictionary<string, double>();

            //Find peptides that do not satisfy cutoff criterions to paint them in gray
            List<PepQuant> marginalPeptides = protPepDict[kvp.Key].Except(kvp.Value).ToList();

            foreach (PepQuant pq in marginalPeptides)
            {
                if (!quantData.ContainsKey(pq.CleanPeptideSequence))
                {
                    quantData.Add(pq.CleanPeptideSequence, 0);
                }
            }

            foreach (PepQuant pq in kvp.Value)
            {
                if (!quantData.ContainsKey(pq.CleanPeptideSequence))
                {
                    quantData.Add(pq.CleanPeptideSequence, pq.AVGLogFold);
                }
            }

            KeyValuePair<string, Dictionary<string, double>> theDatum = new KeyValuePair<string, Dictionary<string, double>>(kvp.Key.Sequence, quantData);
            return theDatum;
        }

        private void DataGridProteinView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                int si = DataGridProteinView.SelectedIndex;
                Dictionary<string, object> ao = PatternTools.pTools.AnonymousToDictionary(DataGridProteinView.SelectedItems[0]);
                FastaItem theKey = MyFastaItems.Find(a => a.SequenceIdentifier.Equals(ao["ProtID"].ToString()));
                DataGridPeptideView.ItemsSource = protPepDictTMP[theKey].Select(a => new { Sequence = a.Sequence, Binomial_PValue = Math.Round(a.BinomialProbability, 4), Paired_TTest_Pvalue = Math.Round(a.TTest, 4), AVGLogFold = Math.Round(a.AVGLogFold, 4), Redundancy = a.MappableProteins.Count, SpecCount = a.MyQuants.Count, MissingValues = a.MissingValues });
                LabelSelectedProtein.Content = "Selected protein: " + ao["ProtID"].ToString() + " " + ao["Description"].ToString();
                
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.Message);
            }

        }

        public double ProteinPValue(List<PepQuant> pepQuants)
        {
            List<double> pvalues = new List<double>();
            List<double> w = new List<double>();

            foreach (PepQuant pq in pepQuants)
            {
                double ptmp = pq.TTest;

                if ((pq.BinomialProbability < pq.TTest))
                {
                    ptmp = pq.BinomialProbability;
                }

                if (ptmp < 0.01)
                {
                    ptmp = 0.01;
                }


                if (pq.AVGLogFold > 0)
                {
                    ptmp = 1 - ptmp;
                }

                pvalues.Add(ptmp);

                w.Add(pq.MyQuants.Count);
            }

            double stouffers = PatternTools.pTools.StouffersMethod(pvalues, w, 1);

            if (stouffers > 0.5)
            {
                stouffers = 1 - stouffers;
            }

            return stouffers;


        }



        private void DataGridPeptideView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int si = DataGridProteinView.SelectedIndex;
            Dictionary<string, object> ao = PatternTools.pTools.AnonymousToDictionary(DataGridPeptideView.SelectedItems[0]);

            ShowPeptideWindow(ao);

        }

        private void ShowPeptideWindow(Dictionary<string, object> ao)
        {
            PlotModel MyModel = new PlotModel();

            PepQuant sc = MyPeptides.Find(a => a.Sequence.Equals(ao["Sequence"]));

            //ObservableCollection<MyDataPoint> BarChartData = new ObservableCollection<MyDataPoint>();

            SignalViewer ws = new SignalViewer();

            ws.Title = "Peptide: " + sc.Sequence;
            MyModel.Title = sc.Sequence;

            // --------------------------------------------------------------------------------------------------------

            MyModel.Title = sc.Sequence;
            
            MyModel.LegendOrientation = LegendOrientation.Horizontal;
            MyModel.LegendPlacement = LegendPlacement.Outside;
            MyModel.LegendPosition = LegendPosition.BottomCenter;
      

            DataTable dtDataGridView = new DataTable("PepScans");

            DataSet dataSetBarChart = new DataSet();
            DataTable dtBarChart = new DataTable("PepScans");
            dataSetBarChart.Tables.Add(dtBarChart);

            dtBarChart.Columns.Add("channel", typeof(string));

            dtDataGridView.Columns.Add("File Name", typeof(string));
            dtDataGridView.Columns.Add("Precursor Charge", typeof(int));
            dtDataGridView.Columns.Add("Scan Number", typeof(int));

            List<DataRow> dataRowBarChartList = new List<DataRow>();


            var categoryAxis = new CategoryAxis();
            var valueAxis = new LinearAxis();

            valueAxis.Title = "Intensity";
            valueAxis.Position = AxisPosition.Left;

            MyModel.Axes.Add(valueAxis);

            for (int i = 0; i < sc.MyQuants[0].MarkerIntensities.Count; i++)
            {
                
                if (classLabels[i] >= 0)
                {
                    DataRow newRowBarChart = dtBarChart.NewRow();
                    newRowBarChart["channel"] = "Channel " + (i + 1);
                    dataRowBarChartList.Add(newRowBarChart);
                    dtBarChart.Rows.Add(newRowBarChart);

                    dtDataGridView.Columns.Add(new DataColumn("Channel " + (i + 1).ToString(), typeof(double)));

                    categoryAxis.Labels.Add(new DataColumn("Channel " + (i+1).ToString(), typeof(double)).ToString());
   
                }

                
            }

            MyModel.Axes.Add(categoryAxis);

            bool insertYaxisBarChart = true;
            int counter = 0;
            List<int> categoryIndex = new List<int>();
            List<double> value = new List<double>();


            foreach (Quant q in sc.MyQuants)
            {
                if (insertYaxisBarChart)
                {
                    // Set the Y-Axis value
                    for (int i = 0; i < sc.MyQuants.Count; i++)
                    {
                        dtBarChart.Columns.Add("spec" + i, typeof(double));
                        //ws.BarChart1.ValueField.Add("spec" + i);
                        categoryIndex.Add(i);
                    }

                    insertYaxisBarChart = false;
                }

                DataRow newRow_datagridview = dtDataGridView.NewRow();
                newRow_datagridview["File Name"] = q.FileName;
                newRow_datagridview["Scan Number"] = q.ScanNumber;
                newRow_datagridview["Precursor Charge"] = q.Z;

                var columnSeries = new ColumnSeries();
                columnSeries.Title = q.FileName;

                int negativeClass = 0;

                var solution = classLabels.GroupBy(i => i > 0).SelectMany(g => g).ToArray();
                //classLabels.Sort((a, b) => b.CompareTo(a));

                for (int i = 0; i < q.MarkerIntensities.Count; i++)
                {

                    if (classLabels[i] >= 0)
                    {

                        DataRow newRow_barChart = dataRowBarChartList[i - negativeClass];
                        newRow_barChart["spec" + counter] = q.MarkerIntensities[i];

                        newRow_datagridview[i + 3 - negativeClass] = q.MarkerIntensities[i];
                        //BarChartData.Add(new MyDataPoint(i, q.MarkerIntensities[i], "Row" + " " + counter));
                        columnSeries.Items.Add(new ColumnItem(q.MarkerIntensities[i], (i-negativeClass)));

                    }
                    else
                    {
                        negativeClass++;
                    }

                }

                counter++;

                MyModel.Series.Add(columnSeries);
                dtDataGridView.Rows.Add(newRow_datagridview);
            }

           
            // Set the chart tooltip.  {field} will be replaced by current bar value.
            // Need to improve in this little templating.
            //ws.BarChart1.ToolTipText = "Signal Intensity: {field}";
            // Set the x-axis data field
            //ws.BarChart1.XAxisField = "channel";


            //--------------------------------------------------------------------------------------------------------

            //ws.MyBarChart.HorizontalPropertyName = "X";
            //ws.BarChart1.DataSource = dataSetBarChart;



            //ws.MyBarChart.Items = BarChartData;

            //ws.BarChart1.Generate();
            //ws.MyBarChart.Draw();
            //ws.MyBarChart.Refresh();

            ws.MyDataGridSpec.ItemsSource = dtDataGridView.DefaultView;
            ws.Show();

            ws.MyBarChart.Model = MyModel;

            
        }

        private void PlotQuants()
        {

            //------------------------------------


            BelaGraphPeptideDistribution.ClearDataBuffer();
            BelaGraphPeptideDistribution.XLabel = "-log(p-value)";
            BelaGraphPeptideDistribution.YLabel = "log(fold)";
            BelaGraphPeptideDistribution.XRound = 4;
            BelaGraphPeptideDistribution.YRound = 4;


            SolidColorBrush redBrush = Brushes.Red.Clone();
            redBrush.Opacity = 0.5;

            SolidColorBrush greenBrush = Brushes.Green.Clone();
            greenBrush.Opacity = 0.5;

            SolidColorBrush grayBrush = Brushes.LightGray.Clone();
            grayBrush.Opacity = 0.4;

            BelaGraph.DataVector greenVectorPep = new BelaGraph.DataVector(BelaGraph.GraphStyle.Bubble, "Green dots", greenBrush);
            BelaGraph.DataVector redVectorPep = new BelaGraph.DataVector(BelaGraph.GraphStyle.Bubble, "Red dots", redBrush);
            BelaGraph.DataVector grayVectorPep = new BelaGraph.DataVector(BelaGraph.GraphStyle.Bubble, "Gray dots", grayBrush);

            foreach (PepQuant pq in MyPeptides)
            {

                //Skip Quants composed mainly of zeros or quants that have exactly 0.5 as a p-value
                if (pq.TTest == 0.5) { continue; }

                double avgLogFold = pq.AVGLogFold;
                double pValue = pq.TTest;

                if (avgLogFold < -3) { avgLogFold = -3; }
                if (avgLogFold > 3) { avgLogFold = 3; }

                BelaGraph.PointCav dp = new BelaGraph.PointCav();
                dp.Y = avgLogFold;
                dp.X = Math.Round(1 - pValue, 4);
                dp.ExtraParam = dp.Y.ToString();

                dp.MouseOverTip = "Sequence: " + pq.Sequence + "\nQuants: " + pq.MyQuants.Count + "\nAvg Log Fold: " + avgLogFold + "\nTtest: " + pValue;

                if (myPeptidesTMP.Exists(a => a.Sequence.Equals(pq.Sequence)))
                {
                    if (dp.Y > 0)
                    {
                        greenVectorPep.AddPoint(dp);

                    }
                    else
                    {
                        redVectorPep.AddPoint(dp);
                    }
                }
                else
                {
                    if ((bool)CheckBoxPlotFilteredPeptides.IsChecked)
                    {
                        grayVectorPep.AddPoint(dp);
                    }
                }

            }

            grayVectorPep.ThePoints.Sort((a, b) => b.X.CompareTo(a.X));
            redVectorPep.ThePoints.Sort((a, b) => b.X.CompareTo(a.X));
            greenVectorPep.ThePoints.Sort((a, b) => b.X.CompareTo(a.X));

            BelaGraphPeptideDistribution.AddDataVector(grayVectorPep);
            BelaGraphPeptideDistribution.AddDataVector(redVectorPep);
            BelaGraphPeptideDistribution.AddDataVector(greenVectorPep);


            BelaGraphPeptideDistribution.Plot(BelaGraph.BackgroundOption.YellowXGradient, true, true, new System.Drawing.Font("Courier New", 14));


        }



        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateGUI();
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxQuantitationTextChanged();
        }

        private void DataGridProteinView_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Coverage"))
            {
                //Create a new teamplate column.
                DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                templateColumn.Header = e.Column.Header;
                templateColumn.MinWidth = 160;

                DataTemplate dt = new DataTemplate();
                FrameworkElementFactory fef = new FrameworkElementFactory(typeof(CoverageChart));
                fef.SetBinding(CoverageChart.SequenceAndPeptidesQuantProperty, new Binding("Coverage"));

                dt.VisualTree = fef;
                templateColumn.CellTemplate = dt;
                e.Column = templateColumn;
            }
        }

        private void BelaGraphPeptideDistribution_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BelaGraphPeptideDistribution.Plot(BelaGraph.BackgroundOption.YellowXGradient, true, true, new System.Drawing.Font("Courier New", 14));
        }

        private void IntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                //UpdateGUI();
            }
            catch
            {
                Console.WriteLine("Unable to update GUI");
            }
        }

        private void CheckBoxConsiderOnlyUniquePeptides_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateGUI();
            }
            catch
            {
                Console.WriteLine("Unable to update GUI");
            }
        }

        private void DoubleUpDownPeptideLogFold_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            double d;
            if (double.TryParse(DoubleUpDownPeptideLogFold.Value.ToString(), out d))
            {
                try
                {
                    //UpdateGUI();
                }
                catch
                {
                    Console.WriteLine("Unable to update GUI");
                }
            }
        }

        private void CheckBoxPlotFilteredPeptides_Click(object sender, RoutedEventArgs e)
        {
            PlotQuants();
        }

        private void TabItemPeptideBrowser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int si = DataGridAllPeptidesView.SelectedIndex;
            if (si == -1) return;

            Dictionary<string, object> ao = PatternTools.pTools.AnonymousToDictionary(DataGridAllPeptidesView.SelectedItems[0]);
            ShowPeptideWindow(ao);
        }

        private void DoubleUpDownPeptidePValueCutoff_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            double d;
            if (double.TryParse(DoubleUpDownPeptidePValueCutoff.Value.ToString(), out d))
            {
                try
                {
                    //UpdateGUI();
                }
                catch
                {
                    Console.WriteLine("Unable to update GUI");
                }
            }
        }

        private void MenuItemExportProteinResults_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.DefaultExt = ".txt";
            sfd.Filter = "Protein Report (*.txt)|*.txt";

            Nullable<bool> result = sfd.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // SaveDocument
                StreamWriter sw = new StreamWriter(sfd.FileName);

                sw.WriteLine("#Protein\tProtID\tMW\tSequenceCount\tSpecCount\tAvgLogFold\tStouffersPValue\tCoverage\tDescription");
                sw.WriteLine("#Peptide\tSequence\tBinomial\tT-Test\tAVGLogFold\tRedundancy\tSpecCount");
                sw.WriteLine("#Spectrum\tFileName\tScanNumber\tZ\tMarkerIntensities");
                foreach (KeyValuePair<FastaItem, List<PepQuant>> kvp in protPepDictTMP)
                {
                    if (kvp.Value.Count < IntegerUpDown.Value)
                    {
                        continue;
                    }

                    sw.WriteLine(
                            "Protein\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                            kvp.Key.SequenceIdentifier,
                            Math.Round(kvp.Key.MonoisotopicMass, 2),
                            kvp.Value.Count,
                            protPepDictTMP[kvp.Key].Sum(a => a.MyQuants.Count),
                            Math.Round(kvp.Value.Average(a => a.AVGLogFold), 3),
                            ProteinPValue(kvp.Value),
                            Math.Round(kvp.Key.Coverage(kvp.Value.Select(a => a.CleanPeptideSequence).ToList()), 3),
                            kvp.Key.Description
                        );

                    //Now lets print the peptide info
                    foreach (PepQuant pq in kvp.Value)
                    {
                        sw.WriteLine("Peptide\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}", pq.Sequence, pq.BinomialProbability, pq.TTest, pq.AVGLogFold, pq.MappableProteins.Count, pq.MyQuants.Count);
                        foreach (Quant spec in pq.MyQuants)
                        {
                            sw.WriteLine("Spectrum\t" + spec.FileName + "\t" + spec.ScanNumber + "\t" + spec.Z + "\t" + string.Join("\t", spec.MarkerIntensities));
                        }
                    }
                }

                sw.Close();

                MessageBox.Show("File saved.");
            }
        }

        private void DataGridProteinView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void DataGridAllPeptidesView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void DoubleUpDownQValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                List<double> pvalues = new List<double>();

                foreach (var item in DataGridProteinView.ItemsSource)
                {
                    Dictionary<string, object> dict = PatternTools.pTools.AnonymousToDictionary(item);
                    pvalues.Add((double)dict["StouffersPValue"]);
                }

                double correctedP = PatternTools.pTools.BenjaminiHochbergFDR((double)DoubleUpDownQValue.Value, pvalues, false);
                LabelCorrectedP.Content = correctedP;
            }
            catch { }
        }

        private void TextBoxClassLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(TextBoxQuantitationReport.Text) && !String.IsNullOrEmpty(TextBoxClassLabel.Text))
            {
                ButtonGo.IsEnabled = true;
            }
            else
            {
                ButtonGo.IsEnabled = false;
            }
        }

        

        private void MenuItemExporToPLP_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.DefaultExt = ".txt";
            sfd.Filter = "PatternLab Project (*.plp)|*.plp";

            Nullable<bool> result = sfd.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                SparseMatrixIndexParserV2 smi = new SparseMatrixIndexParserV2();

                int counter = 0;
                List<FastaItem> orderedKeys = new List<FastaItem>();
                foreach (KeyValuePair<FastaItem, List<PepQuant>> kvp in protPepDict)
                {
                    if (kvp.Value.Count > IntegerUpDown.Value)
                    {
                        counter++;
                        SparseMatrixIndexParserV2.Index i = new SparseMatrixIndexParserV2.Index();
                        i.ID = counter;
                        i.Name = kvp.Key.SequenceIdentifier;
                        i.Description = kvp.Key.Description;

                        smi.Add(i);

                        orderedKeys.Add(kvp.Key);
                    }
                }

                SparseMatrix sm = new SparseMatrix();
                sm.ClassDescriptionDictionary = new Dictionary<int, string>();
                List<int> labels = Regex.Split(TextBoxClassLabel.Text, " ").Select(a => int.Parse(a)).ToList();


                //Generate the dictionary
                for (int i = 0; i < labels.Count; i++)
                {
                    if (labels[i] < 0) { continue; }

                    //Create the dictionary for the class
                    sm.ClassDescriptionDictionary.Add(i, (i).ToString());


                    List<int> dims = new List<int>();
                    List<double> values = new List<double>();

                    for (int j = 0; j < orderedKeys.Count; j++)
                    {
                        FastaItem fi = orderedKeys[j];
                        List<PepQuant> thePepQuants = protPepDict[fi];

                        double theIntensitySum = 0;
                        foreach (PepQuant pq in thePepQuants)
                        {
                            theIntensitySum += pq.MyQuants.Sum(a => a.MarkerIntensities[i]);
                        }

                        if (theIntensitySum > 0)
                        {
                            dims.Add(j + 1);
                            values.Add(theIntensitySum);
                        }


                    }

                    sparseMatrixRow smr = new sparseMatrixRow(i, dims, values);
                    sm.theMatrixInRows.Add(smr);

                }

                PatternLabProject plp = new PatternLabProject(sm, smi, "Isobaric Quant Project");
                plp.Save(sfd.FileName);

                MessageBox.Show("PLP file was saved");
                Console.WriteLine("PLP file was saved.");

            }


        }

        private void CHeckBoxShowPeptidesOfFilteredProteinsOnly_Click(object sender, RoutedEventArgs e)
        {
            UpdateGUI();
        }

        private void MenuItemExporToXLS_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "IsobaricAnalyzerToXLS";
            dlg.DefaultExt = ".xls";
            dlg.Filter = "Excel (.xls)|*.xls";

            if (dlg.ShowDialog() == true)
            {
                DataGridProteinView.SelectAllCells();
                DataGridProteinView.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, DataGridProteinView);
                String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                String result = (string)Clipboard.GetData(DataFormats.Text);
                DataGridProteinView.UnselectAllCells();
                System.IO.StreamWriter file = new System.IO.StreamWriter(dlg.FileName);
                file.WriteLine(result.Replace(',', ' '));
                file.Close();

                MessageBox.Show("Exporting DataGrid data to Excel file created");
            }
        }

    }
}
