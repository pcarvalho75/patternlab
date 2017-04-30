using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PatternTools;
using System.Text.RegularExpressions;
using PatternTools.SQTParser;
using SEPRPackage;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Windows.Resources;
using PatternTools.SpectraPrediction;
using System.Data;

namespace SEProcessor.Result
{

    /// <summary>
    /// Interaction logic for ResultViewer.xaml
    /// </summary>
    /// 
    public partial class ResultViewer : UserControl
    {

        public ResultPackage ResultViewerResultPackage { get; set; }

        List<MyProtein> searchableProteinList = new List<MyProtein>();
        List<PeptideResult> searchablePeptideResult = new List<PeptideResult>();
        List<SQTScan> searchableScanResult = new List<SQTScan>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isReadOnly">Locks the table from deleting rows</param>
        public ResultViewer()
        {
            InitializeComponent();
        }


        public bool SetTablesToReadOnly
        {
            set
            {
                dataGridProteins.IsReadOnly = value;
                dataGridPeptides.IsReadOnly = value;
            }
        }

        public void ResultViewerPopulateTables()
        {

            ResultViewerResultPackage.MyProteins.MyProteinList.Sort((a, b) => a.GroupNo.CompareTo(b.GroupNo));

            searchableProteinList.Clear();
            dataGridProteins.ItemsSource = new List<string>() { };
            dataGridProteins.UpdateLayout();

            searchableProteinList.AddRange(ResultViewerResultPackage.MyProteins.MyProteinList);
            CalculateSAF(searchableProteinList);

            dataGridProteins.ItemsSource = searchableProteinList;
            

            searchablePeptideResult.Clear();
            searchablePeptideResult.AddRange(ResultViewerResultPackage.MyProteins.MyPeptideList);

            dataGridPeptideSelector.ItemsSource = searchablePeptideResult;
            UpdateFDRLabels();


            //Place the lens image
            imageMagnifyingLens.Source = BitmapToImageSource(Properties.Resources.magnifyinglens, System.Drawing.Imaging.ImageFormat.Bmp);


        }

        
        private static BitmapImage BitmapToImageSource(System.Drawing.Bitmap bitmap, System.Drawing.Imaging.ImageFormat imgFormat)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, imgFormat);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }


        private void CalculateSAF(List<MyProtein> searchableProteinList)
        {

            double nsafDenominator = 0;

            foreach (MyProtein prot in searchableProteinList)
            {
                nsafDenominator += (double)prot.Scans.Count / (double)prot.Length;
            }

            foreach (MyProtein prot in searchableProteinList)
            {
                prot.NSAF = ((double)prot.Scans.Count / (double)prot.Length) / nsafDenominator;
            }

        }


        private void UpdateFDRLabels()
        {
            //Update the FDRs
            labelSpectraFDR.Content = ResultViewerResultPackage.MyFDRResult.SpectraFDRLabel;
            labelPeptideFDR.Content = ResultViewerResultPackage.MyFDRResult.PeptideFDRLabel;
            labelProteinFDR.Content = ResultViewerResultPackage.MyFDRResult.ProteinFDRLabel;
            labelUniquePtn.Content = ResultViewerResultPackage.MyProteins.MyProteinList.FindAll(a => a.ContainsUniquePeptide > 0).Count();

            
            string unlabeledDecoy = ResultViewerResultPackage.MyParameters.UnlabeledDecoyTag;

            //for backwards compatibility
            if (string.IsNullOrEmpty(unlabeledDecoy))
            {
                unlabeledDecoy = "Reverse_";
            }

            labelUnlabeledDecoyPtns.Content = ResultViewerResultPackage.MyProteins.MyProteinList.FindAll(a => a.Locus.StartsWith(unlabeledDecoy)).Count() + " / " +
                   (from protein in ResultViewerResultPackage.MyProteins.MyProteinList
                    where protein.Locus.StartsWith(unlabeledDecoy)
                    select protein.GroupNo).Distinct().Count().ToString();

            int maxParsimonyProteinCount = ResultViewerResultPackage.MaxParsimonyList().Count;
            labelMaxParsimony.Content = maxParsimonyProteinCount.ToString();

        }

        public void SaveDTASelectFilteredReport(string fileName)
        {
            ReportGenerator.SaveReportDTASelectFiltered(
                fileName,
                ResultViewerResultPackage
                );
        }

        public void SaveByFileName(string fileName)
        {
            ReportGenerator.SaveReportByFileName(fileName, ResultViewerResultPackage);
        }

        private void dataGridProteins_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                MyProtein p = (MyProtein)dataGridProteins.SelectedItem;
                MyProtein r = ResultViewerResultPackage.MyProteins.MyProteinList.Find(a => a.Locus.Equals(p.Locus));

                List<SQTScan> toDisplay = PatternTools.ObjectCopier.Clone(r.Scans);

                dataGridPeptides.ItemsSource = toDisplay;
            }
            catch (Exception e4)
            {
                Console.WriteLine("Couldn't plot protein : " + e4.Message);
            }
        }

        private void dataGridPeptideSelector_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                PeptideResult pepR = (PeptideResult)dataGridPeptideSelector.SelectedItem; 
                dataGridPeptides.ItemsSource = pepR.MyScans;
            }
            catch (Exception e4)
            {
                Console.WriteLine("Couldn't plot protein : " + e4.Message);
            }

        }


        
        //Show sequence coverage
        private void dataGridProteins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MyProtein p = (MyProtein)dataGridProteins.SelectedItem;
                MyProtein r = searchableProteinList.Find(a => a.Locus.Equals(p.Locus));

                List<MyProtein> proteinsInGroups = ResultViewerResultPackage.MyProteins.MyProteinList.FindAll(a => a.GroupNo == p.GroupNo);

                SequenceExplorer svf = new SequenceExplorer();
                svf.FireUpInterface(proteinsInGroups, p);
                svf.Show();
            }
            catch (Exception e7)
            {
                Console.WriteLine("Unable to plot protein sequence; reason: " + e7.Message);
            }
        }

        private void dataGridProteins_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ////We need to remove all spectra and peptides from the protein object
                Console.WriteLine("Updating result profile.");

                List<MyProtein> proteinsToRemove = new List<MyProtein>();

                foreach (MyProtein p in dataGridProteins.SelectedItems)
                {
                    proteinsToRemove.Add(p);
                    Console.WriteLine("Removing protein: " + p.Locus);
                }

                ResultViewerResultPackage.RemoveProtein(proteinsToRemove);

                UpdateFDRLabels();

                ////We now need to update the wpf table
                List<MyProtein> tmpList = new List<MyProtein>();
                dataGridProteins.ItemsSource = tmpList;
                this.UpdateLayout();
                dataGridProteins.ItemsSource = ResultViewerResultPackage.MyProteins.MyProteinList;

                

            }
        }


        private void radioButtonPeptideView_Checked(object sender, RoutedEventArgs e)
        {
            dataGridProteins.Visibility = System.Windows.Visibility.Hidden;
            dataGridPeptideSelector.Visibility = System.Windows.Visibility.Visible;

            System.Windows.GridLength g = new GridLength(117, GridUnitType.Star);
            grid1.RowDefinitions[2].Height = g;
        }

        private void radioButtonProteinView_Checked(object sender, RoutedEventArgs e)
        {
            if (searchableProteinList.Count == 0)
            {
                Console.WriteLine("Empty protein list.");
                return;
            }
            CalculateSAF(searchableProteinList);
            dataGridProteins.ItemsSource = searchableProteinList;
            dataGridProteins.Visibility = System.Windows.Visibility.Visible;
            System.Windows.GridLength g = new GridLength(117, GridUnitType.Star);
            grid1.RowDefinitions[2].Height = g;
        }

        private void radioButtonProteinMaxParsView_Checked(object sender, RoutedEventArgs e)
        {
            if (ResultViewerResultPackage != null)
            {
                List<MyProtein> maxParsimonyProt = ResultViewerResultPackage.MaxParsimonyList();
                CalculateSAF(maxParsimonyProt);
                dataGridProteins.ItemsSource = maxParsimonyProt;
                dataGridProteins.Visibility = System.Windows.Visibility.Visible;
                System.Windows.GridLength g = new GridLength(117, GridUnitType.Star);
                grid1.RowDefinitions[2].Height = g;
            }
        }

        private void radioButtonScanView_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButtonScanView.IsChecked == true && !object.ReferenceEquals(ResultViewerResultPackage, null))
            {
                dataGridProteins.Visibility = System.Windows.Visibility.Hidden;
                dataGridPeptideSelector.Visibility = System.Windows.Visibility.Hidden;
                dataGridPeptides.ItemsSource = ResultViewerResultPackage.MyProteins.AllSQTScans;
                
                System.Windows.GridLength g = new GridLength(0);
                grid1.RowDefinitions[2].Height = g;
            }

        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {


            Regex r = new Regex(@textBoxSearch.Text, RegexOptions.IgnoreCase);

            dataGridProteins.ItemsSource = searchableProteinList;
            dataGridPeptideSelector.ItemsSource = searchablePeptideResult;

            if ((bool)radioButtonProteinView.IsChecked && searchableProteinList.Count > 0)
            {

                searchableProteinList = (from p in ResultViewerResultPackage.MyProteins.MyProteinList
                                         where (r.IsMatch(p.Description) || r.IsMatch(p.Locus))
                                         select p).ToList();

                dataGridProteins.ItemsSource = searchableProteinList;
            }
            else if ((bool)radioButtonProteinMaxParsView.IsChecked && searchableProteinList.Count > 0)
            {

                searchableProteinList = (from p in searchableProteinList
                                         where (r.IsMatch(p.Description) || r.IsMatch(p.Locus))
                                         select p).ToList();

                dataGridProteins.ItemsSource = searchableProteinList;
            }
            else if ((bool)radioButtonPeptideView.IsChecked && searchablePeptideResult.Count > 0)
            {
                searchablePeptideResult = (from p in ResultViewerResultPackage.MyProteins.MyPeptideList
                                           where (r.IsMatch(p.TheProteinLocci) || r.IsMatch(p.PeptideSequence))
                                           select p).ToList();
                dataGridPeptideSelector.ItemsSource = searchablePeptideResult;


            }
            else if ((bool)radioButtonScanView.IsChecked)
            {

                List<SQTScan> theScans = (from p in ResultViewerResultPackage.MyProteins.AllSQTScans
                                          where (r.IsMatch(p.FileNameWithScanNumberAndChargeState) || r.IsMatch(p.PeptideSequence))
                                          select p).ToList();

                dataGridPeptides.ItemsSource = theScans;


            }
            else if (textBoxSearch.Text.Length == 0)
            {
                dataGridProteins.ItemsSource = ResultViewerResultPackage.MyProteins.MyProteinList;
                searchableProteinList = ResultViewerResultPackage.MyProteins.MyProteinList;

                dataGridPeptideSelector.ItemsSource = ResultViewerResultPackage.MyProteins.MyPeptideList;
                searchablePeptideResult = ResultViewerResultPackage.MyProteins.MyPeptideList;

                List<SQTScan> theScans = ResultViewerResultPackage.MyProteins.AllSQTScans;
                dataGridPeptides.ItemsSource = ResultViewerResultPackage.MyProteins.AllSQTScans;

                return;
            }
            

        }

        private void dataGridPeptides_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SQTScan s = (SQTScan)dataGridPeptides.SelectedItem;

            //This is a temporary patch so the program won't crash when displaying spectra from PTMs.  PTM's are usually displayed using parenthesis.
            if (s.PeptideSequence.Contains("(") || s.PeptideSequence.Contains("*"))
            {
                return;
            }

            if (s.MSLight != null)
            {
                PatternTools.SpectrumViewer.SpectrumViewerForm svf = new PatternTools.SpectrumViewer.SpectrumViewerForm();

                svf.PlotSpectrum(s.MSLight, ResultViewerResultPackage.MyParameters.MS2PPM, s.PeptideSequence, PatternTools.PTMMods.DefaultModifications.TheModifications, false);

                svf.ShowDialog();

            }
        }

        

        //------------------------------------------------------------------------------------

        internal void SaveProteinTable(string fileName)
        {
            ReportGenerator.SaveProteinTable(
                fileName,
                ResultViewerResultPackage
            );
        }

        private void dataGridProteins_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        
    }
}
