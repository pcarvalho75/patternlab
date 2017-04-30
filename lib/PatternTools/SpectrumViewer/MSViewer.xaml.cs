using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PatternTools.MSParser;
using System.Linq;
using System.Windows;
using PatternTools.SpectraPrediction;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using PatternTools.PTMMods;

namespace PatternTools.SpectrumViewer
{
    /// <summary>
    /// Interaction logic for MSViewer.xaml
    /// </summary>
    public partial class MSViewer : UserControl
    {
        //Used for zoom
        double startX;
        List<Ion> IonsInDisplay = new List<Ion>();
        List<Ion> myMS = new List<Ion>();
        double compressionFactor = 0.95;

        public List<ModificationItem> Modifications
        {
            get;
            set;
        }

        public bool IonA {
            set {checkBoxA.IsChecked = value;}
        }

        public bool IonB {
            set {checkBoxB.IsChecked = value;}
        }

        public bool IonC {
            set {checkBoxC.IsChecked = value;}
        }

        public bool IonX {
            set {checkBoxX.IsChecked = value;}
        }

        public bool IonY {
            set {checkBoxY.IsChecked = value;}
        }

        public bool IonZ {
            set {checkBoxZ.IsChecked = value;}
        }

        public bool IonNH3 {
            set {checkBoxNLNH3.IsChecked = value;}
        }

        public bool IonH2O {
            set {checkBoxNLH2O.IsChecked = value;}
        }

        //These variable must always be specified before calling any plotting method
        //We need them to find the precursor neutral losses
        public int ChargeState { get; set; }
        public double TheoreticalMH { get; set; }
        public double RelativeIntensityThreshold {
            get { return double.Parse(textBoxRelativeIntensityThreshold.Text); }
            set {
                textBoxRelativeIntensityThreshold.Text = value.ToString();
            }
        }

        public double SetMS2PPM
        {
            set
            {
                textBoxPPM.Text = value.ToString();
            }
        }

        public string PeptideSequence
        {
            set
            {
                textBoxSequence.Text = value;
            }
        }

        public PatternTools.MSParserLight.MSLight MyMSLight
        {
            //Generate a full MS
            set {
                
                MSParser.MSFull myFullMS = new MSParser.MSFull();
                for (int i = 0; i < value.Intensity.Count; i++)
                {
                    myFullMS.MSData.Add(new Ion(value.MZ[i], value.Intensity[i], value.CromatographyRetentionTime, value.ScanNumber));
                }

                MyMS = myFullMS;

            }
        }

        public PatternTools.MSParser.MSFull MyMS {
            set {
                myMS.Clear();
                PatternTools.MSParser.MSFull anMS = value;

                textBoxSpectrumPeaks.Clear();
                foreach (Ion i in anMS.MSData)
                {
                    textBoxSpectrumPeaks.AppendText(i.MZ + "\t" + i.Intensity + "\n");
                }

                myMS.AddRange(anMS.MSData);

                //And now lets fill up the MS with ilusional ions so we can establish a zoom resolution
                for (double i = anMS.MSData[0].MZ; i < anMS.MSData[anMS.MSData.Count-1].MZ; i += 0.1)
                {
                    myMS.Add(new Ion(i, 0, 0, 0));
                }

                myMS.Sort((a, b) => a.MZ.CompareTo(b.MZ));
            }
        }

        public PatternTools.MSParserLight.MSUltraLight MyMSUltraLight
        {
            //Generate a full MS
            set
            {

                MSParser.MSFull myFullMS = new MSParser.MSFull();
                List<Ion> theIons = value.Ions.Select(a => new Ion(a.Item1, a.Item2, 1, 1)).ToList();
                myFullMS.MSData = theIons;

                MyMS = myFullMS;

            }
        }



        public MSViewer()
        {
            InitializeComponent();
        }

        public void FuncPrintMS()
        {
            if (myMS.Count > 1)
            {
                FuncPrintMS(myMS, myMS.Min(a => a.MZ), myMS.Max(a => a.MZ), false);

                Plot();
            }
        }

        public void FuncPrintMS(double start, double end)
        {
            //Find the minimum and maximum points
            if (myMS.Count > 1)
            {
                FuncPrintMS(myMS, start, end, false);
                Plot();
            }
        }

        private void FuncPrintMS(List<Ion> theMS, double mzStart, double mzEnd)
        {
            FuncPrintMS(myMS, mzStart, mzEnd, false);
            Plot();
        }

        private void FuncPrintMS (List<Ion> theMS, double mzStart, double mzEnd, bool overlaySpectra) {

            canvasMS.Children.Clear();
            IonsInDisplay = theMS.FindAll(a => a.MZ >= mzStart && a.MZ <= mzEnd);

            if (IonsInDisplay.Count < 3)
            {
                MessageBox.Show("Not enough ions to display.  Try clicking on the right-mouse button to zoom out");
                return;
            }

            double lowestMZ = IonsInDisplay.Min(a => a.MZ);
            double highestMZ = IonsInDisplay.Max(a => a.MZ);
            double maxIntensity = IonsInDisplay.Max(a => a.Intensity);

            double minimumHorizonalPixelsBetweenLable = 30;
            List<double> addedPositions = new List<double>();
            

            //Find ions that satisfy minimum intensity
            List<Ion> ionsWithMinimumIntensity = IonsInDisplay.FindAll(a => (a.Intensity / maxIntensity) >= double.Parse(textBoxRelativeIntensityThreshold.Text));

            //Find ions that are closest to each of the predicted peaks
            Dictionary<Ion, PredictedIon> ionPredictedIonDic = new Dictionary<Ion, PredictedIon>();
            if (overlaySpectra)
            {
                double acceptablePPM = double.Parse(textBoxPPM.Text);
                
                foreach (PredictedIon p in dataGridPeakViewer.Items)
                {
                    List<Ion> matchedIons = ionsWithMinimumIntensity.FindAll(a => PatternTools.pTools.PPM(a.MZ, p.MZ) < acceptablePPM);

                    foreach (Ion i in matchedIons)
                    {
                        if (!ionPredictedIonDic.ContainsKey(i))
                        {
                            p.Matched = true;
                            ionPredictedIonDic.Add(i, p);
                        }
                        
                    }
                }
            }

            //Lets plot the peaks!
            ionsWithMinimumIntensity.Sort((a, b) => b.Intensity.CompareTo(a.Intensity));
            foreach (Ion i in  ionsWithMinimumIntensity)
            {
                if (i.Intensity == 0) { continue; }

                double x = ConvertMZToPixelX(i.MZ, lowestMZ, highestMZ);
                double height = ConvertIntensityToPixelY(i.Intensity, maxIntensity);

                Rectangle r = new Rectangle();
                r.Height = height;
                
                r.ToolTip = "M/Z: " + i.MZ + "\n";
                r.ToolTip += "Intensity: " + i.Intensity + "\n";
                double relativeIntensity = Math.Round((i.Intensity / maxIntensity) * 100, 0);
                r.ToolTip += "Rel Intensity: " + relativeIntensity + "\n";

                if (ionPredictedIonDic.ContainsKey(i))
                {
                    PredictedIon pIon = ionPredictedIonDic[i];
                    if ((pIon.Series.ToString().Equals("A") || pIon.Series.ToString().Equals("B") || pIon.Series.ToString().Equals("C")) && pIon.Number > 0)
                    {
                        r.Fill = Brushes.Red;
                    }
                    else if ((pIon.Series.ToString().Equals("X") || pIon.Series.ToString().Equals("Y") || pIon.Series.ToString().Equals("Z")) && pIon.Number > 0) {
                        r.Fill = Brushes.Blue;
                    } else {
                        r.Fill = Brushes.Green;
                    }

                    r.ToolTip += "Series: " + ionPredictedIonDic[i].Series + "\n";
                    r.ToolTip += "Series #: " + ionPredictedIonDic[i].Number + "\n";
                    r.ToolTip += "Charge: " + ionPredictedIonDic[i].Charge;
                    r.Width = 2;
                }
                else
                {
                    r.Fill = Brushes.Black;
                    r.Width = 1;
                }
                
                canvasMS.Children.Add(r);
                Canvas.SetLeft(r, x);
                Canvas.SetBottom(r, 0);

                //Add lable
                bool addlable = true;
                foreach (double ap in addedPositions)
                {
                    if (Math.Abs(x - ap) < minimumHorizonalPixelsBetweenLable)
                    {
                        addlable = false;
                    }
                }

                if (addlable && relativeIntensity > 0.02)
                {
                    Label l = new Label();
                    l.Content = Math.Round(i.MZ, 3);
                    l.FontSize = 10;

                    l.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    Rect measureRect = new Rect(l.DesiredSize);

                    canvasMS.Children.Add(l);
                    double addedPos = x - measureRect.Width / 2;
                    Canvas.SetLeft(l, addedPos);
                    Canvas.SetBottom(l, height);
                    addedPositions.Add(x);
                }    
            }

            IonsInDisplay.Sort((a, b) => a.MZ.CompareTo(b.MZ));
        }

        private double ConvertMZToPixelX(double mz, double lowestMZ, double highestMZ)
        {
            return ((mz - lowestMZ) / (highestMZ - lowestMZ)) * canvasMS.ActualWidth * compressionFactor;
        }

        private double ConvertIntensityToPixelY(double intensity, double maxIntensity)
        {
            return intensity / maxIntensity * canvasMS.ActualHeight * compressionFactor;
        }

        private double ConvertPixelXtoMZ(double pxValue, double highestMZ, double lowestMZ)
        {
            double result = (pxValue) / (canvasMS.ActualWidth * compressionFactor);
            result = result * (highestMZ - lowestMZ);
            result += lowestMZ;
            return (result);
        }

        private void canvasMS_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IonsInDisplay.Count > 1)
            {
                FuncPrintMS(myMS, IonsInDisplay[0].MZ, IonsInDisplay[IonsInDisplay.Count - 1].MZ);
                Plot();
            }
        }

        private void canvasMS_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startX = e.GetPosition(this).X - MyGrid.ColumnDefinitions[0].ActualWidth;
        }

        private void canvasMS_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (myMS.Count == 0) { return; }
            double endX = e.GetPosition(this).X - MyGrid.ColumnDefinitions[0].ActualWidth;

            if (endX < startX)
            {
                double t;
                t = endX;
                endX = startX;
                startX = t;
            }

            //Find minimum and maximum mz points of ions in display
            IonsInDisplay.Sort((a, b) => a.MZ.CompareTo(b.MZ));

            double lowerBoundMZ = ConvertPixelXtoMZ(startX, IonsInDisplay[IonsInDisplay.Count - 1].MZ, IonsInDisplay[0].MZ);
            double upperBountMz = ConvertPixelXtoMZ(endX, IonsInDisplay[IonsInDisplay.Count - 1].MZ, IonsInDisplay[0].MZ);


            FuncPrintMS(myMS, lowerBoundMZ, upperBountMz);
            Plot();
        }

        private void canvasMS_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            FuncPrintMS();
            Plot();
        }

        private void buttonCID_Click(object sender, RoutedEventArgs e)
        {
            SetToCIDMode();
        }

        public void SetToCIDMode()
        {
            checkBoxA.IsChecked = false;
            checkBoxB.IsChecked = true;
            checkBoxY.IsChecked = true;
            checkBoxX.IsChecked = false;
            checkBoxZ.IsChecked = false;
            checkBoxC.IsChecked = false;
            checkBoxNLH2O.IsChecked = true;
            checkBoxNLNH3.IsChecked = true;
            checkBoxIsMonoisotopic.IsChecked = true;
            Plot();
        }


        public void SetToETDMode()
        {
            checkBoxA.IsChecked = false;
            checkBoxB.IsChecked = false;
            checkBoxY.IsChecked = false;
            checkBoxX.IsChecked = false;
            checkBoxZ.IsChecked = true;
            checkBoxC.IsChecked = true;
            checkBoxNLH2O.IsChecked = true;
            checkBoxNLNH3.IsChecked = true;
            checkBoxIsMonoisotopic.IsChecked = true;
            Plot();
        }

        private void buttonPlot_Click(object sender, RoutedEventArgs e)
        {
            Plot();

            //Evaluate the spectrum quality
            PatternTools.SpectraPrediction.SpectralPredictionParameters spm = new SpectralPredictionParameters(
                true,
                true,
                true,
                true,
                true,
                true,
                false,
                false,
                1,
                true,
                Modifications
                );
                        
            PatternTools.SpectraPrediction.SpectraPredictor sp = new SpectraPredictor(spm);
            List<PredictedIon> theoretical = sp.PredictPeaks(textBoxSequence.Text, ChargeState, TheoreticalMH);

            List<Ion> filteredList = myMS.FindAll(a => a.Intensity > double.Parse(textBoxRelativeIntensityThreshold.Text));

            List<SpectralMatchEvaluator.TheTests> evaluationTests = new List<SpectralMatchEvaluator.TheTests>() { SpectralMatchEvaluator.TheTests.AllTests };
            PatternTools.SpectralMatchEvaluator.SpectrumComparisonResult spectrumEvaluationResult = 
                PatternTools.SpectralMatchEvaluator.Compare.Do(
                evaluationTests,
                theoretical,
                filteredList,
                double.Parse(textBoxPPM.Text),
                textBoxSequence.Text.Length,
                RelativeIntensityThreshold
                );

            PatternTools.SpectralMatchEvaluator.Compare.PrintResults(spectrumEvaluationResult);

        }


        public void Plot(List<PredictedIon> peaks = null)
        {
            myMS.Sort((a, b) => a.MZ.CompareTo(b.MZ));

            if (peaks == null)
            {
                SpectralPredictionParameters spp = new SpectralPredictionParameters
                    ((bool)checkBoxA.IsChecked,
                    (bool)checkBoxB.IsChecked,
                    (bool)checkBoxC.IsChecked,
                    (bool)checkBoxX.IsChecked,
                    (bool)checkBoxY.IsChecked,
                    (bool)checkBoxZ.IsChecked,
                    (bool)checkBoxNLH2O.IsChecked,
                    (bool)checkBoxNLNH3.IsChecked,
                    3,
                    (bool)checkBoxIsMonoisotopic.IsChecked,
                    Modifications
                    );

                if (Modifications == null)
                {
                    spp.MyModifications = new List<ModificationItem>();
                }
                PatternTools.SpectraPrediction.SpectraPredictor sp = new PatternTools.SpectraPrediction.SpectraPredictor(spp);

                if (textBoxSequence.Text.Length > 0)
                {
                    peaks = sp.PredictPeaks(textBoxSequence.Text, ChargeState, TheoreticalMH);
                    if (!(bool)checkBoxZ1.IsChecked)
                    {
                        peaks.RemoveAll(a => a.Charge == 1 && a.Series != IonSeries.Precursor);
                    }
                    if (!(bool)checkBoxZ2.IsChecked)
                    {
                        peaks.RemoveAll(a => a.Charge == 2 && a.Series != IonSeries.Precursor);
                    }
                    if (!(bool)checkBoxZ3.IsChecked)
                    {
                        peaks.RemoveAll(a => a.Charge == 3 && a.Series != IonSeries.Precursor);
                    }
                } else
                {
                    peaks = new List<PredictedIon>();
                }
            }

            dataGridPeakViewer.ItemsSource = peaks;

            try
            {
                if (textBoxSequence.Text.Length == 0)
                {
                    FuncPrintMS(myMS, IonsInDisplay[0].MZ, IonsInDisplay[IonsInDisplay.Count - 1].MZ, false);
                }
                else
                {
                    FuncPrintMS(myMS, IonsInDisplay[0].MZ, IonsInDisplay[IonsInDisplay.Count - 1].MZ, true);
                }
            }
            catch (Exception eee)
            {
                Console.WriteLine(eee.Message);
            }

            double matches = peaks.FindAll(a => a.Matched == true).Count;
            double total = peaks.Count;
            labelPeaksProduced.Content = matches + " / " + total + " = " + Math.Round(matches/total,1) * 100 + "%";


            // Plot the denovo
            canvasDenovo.Children.Clear();
            if ((bool)checkBoxB.IsChecked)
            {
                labelABC.Content = "B:";
                PlotSequence(peaks, IonSeries.B, 13, Brushes.Red);
            }
            else if ((bool)checkBoxC.IsChecked)
            {
                labelABC.Content = "C:";
                PlotSequence(peaks, IonSeries.C, 13, Brushes.Red);
            }
            else if ((bool)checkBoxA.IsChecked)
            {
                labelABC.Content = "A:";
                PlotSequence(peaks, IonSeries.A, 13, Brushes.Red);
            }
            else
            {
                labelABC.Content = "";
            }

            if ((bool)checkBoxY.IsChecked)
            {
                labelXYZ.Content = "Y:";
                PlotSequence(peaks, IonSeries.Y, 40, Brushes.Blue);
            }
            else if ((bool)checkBoxZ.IsChecked)
            {
                labelXYZ.Content = "Z:";
                PlotSequence(peaks, IonSeries.Z, 40, Brushes.Blue);
            }
            else if ((bool)checkBoxX.IsChecked)
            {
                labelXYZ.Content = "X:";
                PlotSequence(peaks, IonSeries.X, 40, Brushes.Blue);
            }
            else
            {
                labelXYZ.Content = "";
            }

            //Display peptide of theoretical spectrum
            Label peptideSequenceLabel = new Label();
            peptideSequenceLabel.FontSize = 10;
            peptideSequenceLabel.Content = textBoxSequence.Text + " (" + textBoxSequence.Text.Count() + ")";
            peptideSequenceLabel.FontWeight = FontWeights.Light;
            peptideSequenceLabel.Foreground = Brushes.Gray;

            canvasMS.Children.Add(peptideSequenceLabel);
            Canvas.SetLeft(peptideSequenceLabel, -2);
            Canvas.SetTop(peptideSequenceLabel, 0);

        }

        private void PlotSequence(List<PredictedIon> peaks, IonSeries ionType, double yHeight, SolidColorBrush theBrush) {

            if (IonsInDisplay.Count == 0)
            {
                IonsInDisplay = myMS;

                if (myMS.Count == 0)
                {
                    return;
                }
            }

            double lowestMass = IonsInDisplay.Min(a => a.MZ);
            double largestMass = IonsInDisplay.Max(a => a.MZ);

            List<PredictedIon> peaksOfInterest = peaks.FindAll(a => a.Number >=0 && a.Series.Equals(ionType));
            peaksOfInterest.Sort((a, b) => a.MZ.CompareTo(b.MZ));

            string upperLabel = "";
            string lowerLabel = "";
            int matchCounter = 0;
            double lastX = ConvertMZToPixelX(lowestMass, lowestMass, largestMass);
            for (int i = 0; i < peaksOfInterest.Count; i++)
            {

                if (peaksOfInterest[i].Matched || i == peaksOfInterest.Count - 1)
                {
                    matchCounter++;
                    upperLabel += peaksOfInterest[i].FinalAA;
                    lowerLabel += peaksOfInterest[i].Number;

                    //Plot
                    double startX = lastX;
                    double endX = ConvertMZToPixelX(peaksOfInterest[i].MZ, lowestMass, largestMass);

                    ArrowHeads.ArrowEnds thisArrowEnds = ArrowHeads.ArrowEnds.Both;
                    if (matchCounter == 1 && peaksOfInterest[i].Number > i)
                    {
                        thisArrowEnds = ArrowHeads.ArrowEnds.End;
                    }

                    if (i == peaksOfInterest.Count - 1)
                    {
                        endX = ConvertMZToPixelX(largestMass, lowestMass, largestMass);
                        if (peaksOfInterest[i].MZ > largestMass)
                        {
                            thisArrowEnds = ArrowHeads.ArrowEnds.Start;
                        }
                    }

                    PatternTools.ArrowHeads.ArrowDeNovo.ArrowDeNovoDraw(upperLabel, lowerLabel, theBrush, canvasDenovo, startX, endX, yHeight, thisArrowEnds);

                    //And now clear for the next labels
                    upperLabel = "";
                    lowerLabel = "";
                    lastX = endX;
                }
                else
                {
                    upperLabel += peaksOfInterest[i].FinalAA + " ";
                    lowerLabel += peaksOfInterest[i].Number + ", ";
                }

            }

        }

        private void buttonSaveImage_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog g = new SaveFileDialog();
            g.DefaultExt = ".png";
            g.Filter = "PNG (*.png) |*.png";
            double scale = double.Parse(textBoxImageScale.Text);
            g.ShowDialog();

            if (g.FileName != "")
            {
                int width = (int)(MyGrid.ActualWidth * scale);
                int height = (int)(MyGrid.ActualHeight * scale);

                double originalWidth = MyGrid.ActualWidth;
                double originalHeight = MyGrid.ActualHeight;

                System.IO.FileStream fs = new System.IO.FileStream(g.FileName, System.IO.FileMode.Create);
                RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 1 / 300, 1 / 300, PixelFormats.Pbgra32);



                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext context = visual.RenderOpen())
                {
                    Size s = new Size(originalWidth, originalHeight);
                    System.Windows.Point p = new System.Windows.Point(0, 0);
                    Rect r = new Rect(p, s);
                    VisualBrush brush = new VisualBrush(MyGrid);
                    context.DrawRectangle(brush, null, r);
                }

                visual.Transform = new ScaleTransform(width / MyGrid.ActualWidth, height / MyGrid.ActualHeight);


                bmp.Render(visual);
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fs);
                fs.Close();
            }
        }

        private void checkBoxB_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBoxB.IsChecked)
            {
                checkBoxA.IsChecked = false;
                checkBoxC.IsChecked = false;
            }
        }
    }
}
