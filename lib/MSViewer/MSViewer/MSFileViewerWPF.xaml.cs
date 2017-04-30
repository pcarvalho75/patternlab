using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using PatternTools;
using PatternTools.MSParser;
using PatternTools.MSParserLight;
using PatternTools.PTMMods;
using PatternTools.RawReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MSViewer
{
    /// <summary>
    /// Interaction logic for MSFileViewerWPF.xaml
    /// </summary>
    public partial class MSFileViewerWPF : UserControl
    {
        List<MSUltraLight> MyMS2 = new List<MSUltraLight>();
        List<MSUltraLight> MyMS1 = new List<MSUltraLight>();

        public MSFileViewerWPF()
        {
            InitializeComponent();
        }

        private async void MenuItemLoadFile_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            ofd.DefaultExt = ".raw";
            ofd.Filter = "Supported Mass Spectrum files (MS1, MS2, MGF, Thermo  RAW)|*.ms2;*.ms1;*.mgf;*.RAW";


            // Get the selected file name and display in a TextBox 
            if ((bool)ofd.ShowDialog())
            {
                await LoadFile(ofd.FileName);

            }

        }

        public async Task LoadFile(string fileName)
        {
            WaitWindowWPF.Visibility = Visibility.Visible;

            FileInfo theFile = new FileInfo(fileName);
            await Task.Run(() => LoadFile(theFile));

            MenuItemOperations.IsEnabled = true;


            groupBoxLoadedSpectra.Header = "# MS2 :: " + MyMS2.Count + " File: " + theFile.Name;
            UpdateDataGrid();
            RefreshLCTopPlot();
            RefreshLCFrontPlot();
            RefreshMS2DispositionPlot();

            WaitWindowWPF.Visibility = Visibility.Collapsed;
        }

        private void RefreshMS2DispositionPlot()
        {
            PlotModel myPlot = new PlotModel();

            var scatterSeries1 = new ScatterSeries();

            scatterSeries1.SelectionMode = OxyPlot.SelectionMode.Single;
            myPlot.SelectionColor = OxyColors.Red;

            scatterSeries1.MarkerSize = 3;
            scatterSeries1.MarkerStroke = OxyColors.DarkGreen;
            scatterSeries1.MarkerFill = OxyColors.LightGreen;


            scatterSeries1.MarkerType = MarkerType.Circle;

            var linearAxis1 = new LinearAxis();
            linearAxis1.Title = "Retention time";
            linearAxis1.Position = AxisPosition.Bottom;
            myPlot.Axes.Add(linearAxis1);


            var linearAxis2 = new LinearAxis();
            linearAxis2.Title = "m/z";
            linearAxis2.Position = AxisPosition.Left;
            myPlot.Axes.Add(linearAxis2);


            foreach (MSUltraLight ms in MyMS2)
            {
                ScatterPoint s = new ScatterPoint(ms.CromatographyRetentionTime, (double)ms.Precursors[0].Item1);
                s.Tag = ms.ScanNumber;
                scatterSeries1.Points.Add(s);
            }

            scatterSeries1.MouseDown += (s, e) =>
            {
                int index =(int) Math.Round(e.HitTestResult.Index);
                Console.WriteLine("Index of nearest point  " + index);
                scatterSeries1.SelectItem(index);
                myPlot.InvalidatePlot(false);

                int scanNo = (int)scatterSeries1.Points[index].Tag;

                //And now select the spectrum in the table

                var theDataInDataGridView = (IEnumerable<dynamic>)this.dataGridMassSpectra.ItemsSource;
                int tableIndex = -1;
                foreach (var v in theDataInDataGridView)
                {
                    tableIndex++;
                    Dictionary<string, object> a = PatternTools.pTools.AnonymousToDictionary(v);
                    if ((int)a["ScanNumber"] == scanNo)
                    {
                        break;
                    }
                }

                dataGridMassSpectra.ScrollIntoView(dataGridMassSpectra.Items[tableIndex]);
                dataGridMassSpectra.UpdateLayout();
                dataGridMassSpectra.SelectedIndex = tableIndex;

            };

            myPlot.Series.Add(scatterSeries1);


            MyPlotMS2disposition.Model = myPlot;
        }

        private void dataGridMassSpectra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dictionary<string, object> tableValues = PatternTools.pTools.AnonymousToDictionary(dataGridMassSpectra.SelectedItem);
            int scanNumber = (int)tableValues["ScanNumber"];
            ShowSpectrum(scanNumber);

            //Select the dot in the MS2 viewer
            ScatterSeries ss = (ScatterSeries)MyPlotMS2disposition.Model.Series[0];
            ScatterPoint point = ss.Points.Find(a => (int)a.Tag == scanNumber);
            int indexToSelect = ss.Points.IndexOf(point);

            bool isSelected = ss.IsItemSelected(indexToSelect);
            if (!isSelected)
            {
                MyPlotMS2disposition.Model.Series[0].SelectItem(indexToSelect);
                ss.SelectItem(indexToSelect);
                MyPlotMS2disposition.Model.InvalidatePlot(false);
            }
        }

        private void RefreshLCFrontPlot()
        {
            var plotModel = new PlotModel();

            var linearAxis1 = new LinearAxis();
            linearAxis1.Title = "Intensity";
            linearAxis1.Position = AxisPosition.Left;
            plotModel.Axes.Add(linearAxis1);

            var linearAxis2 = new LinearAxis();
            linearAxis2.Title = "Minutes";
            linearAxis2.Position = AxisPosition.Bottom;
            plotModel.Axes.Add(linearAxis2);



            var linearBarSeries = new LinearBarSeries();
            linearBarSeries.FillColor = OxyColor.FromArgb(69, 76, 175, 80);
            linearBarSeries.StrokeThickness = 1;
            linearBarSeries.StrokeColor = OxyColor.FromArgb(255, 76, 175, 80);


            plotModel.Series.Add(linearBarSeries);

            if (MyMS1.Count == 0)
            {
                return;
            }

            double intensitySum = 0;

            foreach (MSUltraLight ms in MyMS1)
            {
                double spectrumCurrent = ms.Ions.Sum(a => a.Item2);
                intensitySum += spectrumCurrent;
                linearBarSeries.Points.Add(new DataPoint(ms.CromatographyRetentionTime, spectrumCurrent));
            }

            LabelTICMS1.Content = intensitySum.ToString("e4", CultureInfo.InvariantCulture);
            LabelNoMS1.Content = MyMS1.Count;
            LabelAvgTIC.Content = Math.Round(intensitySum / (double)MyMS1.Count, 2).ToString("e4", CultureInfo.InvariantCulture);


            MyPlotLCMSFront.Model = plotModel;

        }

        private void RefreshLCTopPlot()
        {
            if (MyMS1.Count == 0 || !(bool)checkBoxGenerateLCMSTOP.IsChecked)
            {
                return;
            }
            //Make plot
            PlotModel MyModel = new PlotModel();
            var heatMapSeries1 = new HeatMapSeries();

            var linearColorAxis1 = new LinearColorAxis();
            linearColorAxis1.HighColor = OxyColors.Gray;
            linearColorAxis1.LowColor = OxyColors.Black;
            linearColorAxis1.Position = AxisPosition.Right;
            MyModel.Axes.Add(linearColorAxis1);



            heatMapSeries1.Interpolate = false;
            //This heatmap will range up to 2000 mz, with a resolution  of 0.5
            int cromFactor = 5;
            int mzFactor = 2;

            int cromMax = (int) (Math.Round(MyMS1.Last().CromatographyRetentionTime) * cromFactor + 1);

            //FInd MZMax and MZMin
            double mzMin = (from ms in MyMS1
                         from i in ms.Ions
                         select i.Item1).Min();

            double mzMax = (from ms in MyMS1
                            from i in ms.Ions
                            select i.Item1).Max();

            int mzSpace = (int)Math.Round((mzMax - mzMin) * mzFactor);

            heatMapSeries1.Data = new Double[cromMax, mzSpace];

            foreach (MSUltraLight ms in MyMS1)
            {
                int cromRetTime = (int) (ms.CromatographyRetentionTime * cromFactor);
                foreach (Tuple<float, float> i in ms.Ions)
                {
                    int x = (int)(i.Item1  - mzMin) * mzFactor;
                    double r = Math.Pow(i.Item2, (double)DoubleUpDownTopLCAtenuationFactor.Value);

                    if (r > heatMapSeries1.Data[cromRetTime, x])
                    {
                        heatMapSeries1.Data[cromRetTime, x] = r;
                    }
                    
                }

            }

            heatMapSeries1.X0 = 0;
            heatMapSeries1.X1 = (int)MyMS2.Last().CromatographyRetentionTime;
            heatMapSeries1.Y0 = 0;
            heatMapSeries1.Y1 = 2000;

            MyModel.Series.Add(heatMapSeries1);

            MyPlotLCMS.Model = MyModel;
        }



        public void LoadFile(FileInfo file)
        {
           
            
            if (file.Extension.Contains("mgf"))
            {
                MyMS2 = PatternTools.MSParser.MGFParser.ParseMGFFile(file.FullName);
            }
            else if (file.Extension.Contains("ms2"))
            {
                MyMS2 =  PatternTools.MSParserLight.ParserUltraLightMS2.ParseSpectra(file.FullName);
            }
            else
            {

                PatternTools.MSParserLight.ParserUltraLightRAW parser = new ParserUltraLightRAW();
                MyMS1 = parser.ParseFile(file.FullName, -1, 1);
                MyMS2 = parser.ParseFile(file.FullName, -1, 2);
            }

        }

        private void UpdateDataGrid()
        {

            var query = from ms in MyMS2

                        select new
                        {
                            ScanNumber = ms.ScanNumber,
                            ChargedPrecursor = ms.Precursors[0].Item1,
                            Charge = ms.Precursors[0].Item2,
                        };

            dataGridMassSpectra.ItemsSource = query;


        }

        private void ShowSpectrum(int scanNumber)
        {
            MSUltraLight theMS = MyMS2.Find(a => a.ScanNumber == scanNumber);
            List<Ion> ions = theMS.Ions.Select(a => new Ion(a.Item1, a.Item2, 0, scanNumber)).ToList();
            SpectrumEye1.Plot(ions, ions[0].MZ, ions.Last().MZ, 0);

            //MyMSViewer.Modifications = new List<ModificationItem>();
            //MyMSViewer.PeptideSequence = "P";
            //MyMSViewer.MyMS = new MSFull(theMS);
            //MyMSViewer.FuncPrintMS(theMS.Ions.Min(a => a.Item1), theMS.Ions.Max(a => a.Item1));

            TextBoxMSPeaks.Text = PatternTools.MSParserLight.MSUltraLightPrinter.PrintSpectrum(theMS);
        }


        private void DoubleUpDownTopLCAtenuationFactor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            RefreshLCTopPlot();

        }

        private void MenuItemViewOnlyTheseSpectra_Click(object sender, RoutedEventArgs e)
        {
            WaitWindowWPF.Visibility = Visibility.Visible;

            List<string> items = Regex.Split(TextBoxOperations.Text, "[ |\n]").ToList();
            items.RemoveAll(a => a.Length == 0);
            List<int> scns = items.Select(a => int.Parse(a)).ToList();
            MyMS2.RemoveAll(a => !scns.Contains(a.ScanNumber));

            UpdateDataGrid();
            //RefreshLCFrontPlot();
            //RefreshLCTopPlot();

            WaitWindowWPF.Visibility = Visibility.Collapsed;

        }

        private void MenuItemDeleteScanNumbers_Click(object sender, RoutedEventArgs e)
        {
            WaitWindowWPF.Visibility = Visibility.Visible;

            List<string> items = Regex.Split(TextBoxOperations.Text, "[ |\n]").ToList();
            items.RemoveAll(a => a.Length == 0);
            List<int> scns = items.Select(a => int.Parse(a)).ToList();
            MyMS2.RemoveAll(a => scns.Contains(a.ScanNumber));
            UpdateDataGrid();

            UpdateDataGrid();
            //RefreshLCFrontPlot();
            //RefreshLCTopPlot();

            WaitWindowWPF.Visibility = Visibility.Collapsed;
        }

        private void MenuItemWait_Click(object sender, RoutedEventArgs e)
        {
            WaitWindowWPF.Visibility = Visibility.Visible;
        }

        private void checkBoxGenerateLCMSTOP_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBoxGenerateLCMSTOP.IsChecked)
            {
                RefreshLCTopPlot();
            }
        }

        private async void  MenuItemSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();


            if ((bool)sfd.ShowDialog())
            {
                WaitWindowWPF.Visibility = Visibility.Visible;
                await Task.Run
                    (
                        () => PatternTools.MSParserLight.MSUltraLightPrinter.PrintMS2List(MyMS2, sfd.FileName)
                    );

                WaitWindowWPF.Visibility = Visibility.Collapsed;
                
                MessageBox.Show("File Saved");
            }
        }
    }
}
