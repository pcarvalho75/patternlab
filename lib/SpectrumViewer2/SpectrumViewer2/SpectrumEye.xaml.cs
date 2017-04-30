using PatternTools.MSParser;
using PatternTools.MSParserLight;
using PatternTools.SpectraPrediction;
using System;
using System.Collections.Generic;
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

namespace SpectrumViewer2
{
    /// <summary>
    /// Interaction logic for SpectrumEye.xaml
    /// </summary>
    public partial class SpectrumEye : UserControl
    {
        public SpectrumEye()
        {
            InitializeComponent();
        }

        Point startX; //Used for calculating display range when mouse is down.

        List<Ion> IonsInDisplay = new List<Ion>();
        List<Ion> OriginalIons;
        double finishMZ;
        double ppm;
        double startMZ;
        List<PredictedIon> theoreticalSpectrum;

        Rectangle ZoomSelection;
        bool ZoomInProcess = false;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="experimentalIons">List of Ions</param>
        /// <param name="sMZ">Start M/Z</param>
        /// <param name="fMZ">Finish M/Z</param>
        /// <param name="PPM">ppm</param>
        /// <param name="tIons">TheoreticalIons</param>
        public void Plot(List<Ion> experimentalIons, double sMZ, double fMZ, double PPM, List <PredictedIon> tIons = null )
        {
            if (tIons != null)
            {
                //Get a B Series
                List<PredictedIon> bions = tIons.FindAll(a => a.Series == IonSeries.B && a.Number > -1);
                bions.Sort((a, b) => a.Number.CompareTo(b.Number));
                string peptide = string.Join("", bions.FindAll(b=> b.Charge == 1).Select(a => a.FinalAA));



                List<Tuple<string, string, string>> anotations = new List<Tuple<string, string, string>>();

                List<string> splitPeptide = SpectraPredictor.SplitPeptide(peptide);

                for (int i = 0; i < splitPeptide.Count; i++)
                {
                    string a = "";
                    string b = "";

                    if (tIons.Exists(x =>x.MZ >= sMZ && x.MZ <= fMZ && x.Number == i + 1 && x.Matched && (x.Series == IonSeries.X || x.Series == IonSeries.Y || x.Series == IonSeries.Z)))
                    {
                        a = "y1";
                    }

                    if (tIons.Exists(x => x.MZ >= sMZ && x.MZ <= fMZ && x.Number == i + 1 && x.Matched && (x.Series == IonSeries.A || x.Series == IonSeries.B || x.Series == IonSeries.C)))
                    {
                        b = "b2";
                    }

                    anotations.Add(new Tuple<string, string, string>(splitPeptide[i], a, b));
                }

                MySequenceAnotation.Plot(anotations);
            }

            ZoomInProcess = false;
            CanvasPeakDisplay.Children.Clear();
            
            startMZ = sMZ;
            finishMZ = fMZ;
            ppm = PPM;
            theoreticalSpectrum = tIons;

            //Lets keep the original spectrum
            if (OriginalIons == null)
            {
                OriginalIons = experimentalIons;
            }

            IonsInDisplay = experimentalIons.FindAll(a => a.MZ >= startMZ && a.MZ <= finishMZ);

            if (IonsInDisplay.Count == 0)
            {
                //Return to the original spectrum
                PlotOriginal();
            }

            double maxIntensity = IonsInDisplay.Max(a => a.Intensity);

            List<Tuple<double,double>> addedPositions = new List<Tuple<double, double>>(); //Used for printing labels in the peaks
            double minimumHorizonalPixelsBetweenLable = 10;

            IonsInDisplay.Sort((a, b) => b.Intensity.CompareTo(a.Intensity));

            foreach (Ion i in IonsInDisplay)
            {

                Line peak = new Line();
                peak.Stroke = System.Windows.Media.Brushes.Black;
                peak.StrokeThickness = 1;

                double relativeIntensity = Math.Round(i.Intensity / maxIntensity, 3);
                peak.ToolTip = "M/Z: " + i.MZ + "\n";
                peak.ToolTip += "Intensity: " + i.Intensity + "\n";
                peak.ToolTip += "Relative Intensity: " + relativeIntensity + "\n";

                Tuple<double,double> p = ConvertMZToPixel(i, maxIntensity, startMZ, finishMZ);

                peak.X1 = 0;
                peak.X2 = 0;
                peak.Y1 = 0;
                peak.Y2 = p.Item2;

                //Verify if there is a theoretical ion within the ppm tolerance - choose color
                int matching = -1;
                if (!object.Equals(theoreticalSpectrum, null))
                {
                    matching = theoreticalSpectrum.FindIndex(a => Math.Abs(PatternTools.pTools.PPM(a.MZ, i.MZ)) < ppm);
                    if (matching >= 0)
                    {
                        peak.StrokeThickness = 2;

                        if (theoreticalSpectrum[matching].Series == IonSeries.A || theoreticalSpectrum[matching].Series == IonSeries.B || theoreticalSpectrum[matching].Series == IonSeries.C)
                        {
                            peak.Stroke = System.Windows.Media.Brushes.Red;
                        }

                        if (theoreticalSpectrum[matching].Series == IonSeries.X || theoreticalSpectrum[matching].Series == IonSeries.Y || theoreticalSpectrum[matching].Series == IonSeries.Z)
                        {
                            peak.Stroke = System.Windows.Media.Brushes.Blue;
                        }

                        if (theoreticalSpectrum[matching].FinalAA.Contains("-NH3") || theoreticalSpectrum[matching].FinalAA.Contains("-H20"))
                        {
                            peak.Stroke = System.Windows.Media.Brushes.Green;
                        }

                        peak.ToolTip += "Z : " + theoreticalSpectrum[matching].Charge + "\n";
                        peak.ToolTip += "Series: " + theoreticalSpectrum[matching].Series + theoreticalSpectrum[matching].Number.ToString() + "\n";
                        peak.ToolTip += "Sequence Item: " + theoreticalSpectrum[matching].FinalAA;

                    }
                }

                Canvas.SetLeft(peak, p.Item1);
                Canvas.SetBottom(peak, 0);

                CanvasPeakDisplay.Children.Add(peak);

                if (relativeIntensity > 0.02)
                {
                    TextBlock tb = new TextBlock();
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.FontSize = 10;
                    tb.Text = Math.Round(i.MZ, 3).ToString();

                    if (matching > -1)
                    {
                        tb.Text += "\n" + theoreticalSpectrum[matching].Series + theoreticalSpectrum[matching].Number.ToString();
                    }
                    

                    tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    Rect measureRect = new Rect(tb.DesiredSize);

                    
                    double addedPos = p.Item1 - measureRect.Width / 2;
                    Tuple<double, double> thisRange = new Tuple<double, double>(p.Item1 - measureRect.Width / 2, p.Item1 + measureRect.Width / 2);

                    if (!addedPositions.Exists(
                        a =>
                        (Math.Abs(a.Item1 - thisRange.Item1) < minimumHorizonalPixelsBetweenLable)
                        ||
                        (Math.Abs(a.Item2 - thisRange.Item2) < minimumHorizonalPixelsBetweenLable)
                        ||
                        ( (thisRange.Item1 > a.Item1) && (thisRange.Item1 < a.Item2) ) 
                        ||
                        ( (thisRange.Item2 > a.Item1)) && (thisRange.Item2 < a.Item2) )
                        )
                         
                            {
                                CanvasPeakDisplay.Children.Add(tb);
                                Canvas.SetLeft(tb, addedPos);
                                Canvas.SetBottom(tb, p.Item2);
                                addedPositions.Add(thisRange);
                            }
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="maxIntensity"></param>
        /// <param name="startMZ"></param>
        /// <param name="FinishMZ"></param>
        /// <returns>A tuple with the mz and intensity mapped to the canvas</returns>
        private Tuple<double,double> ConvertMZToPixel(Ion i, double maxIntensity, double startMZ, double FinishMZ)
        {
            double compressionFactor = 0.80;

            double deltaMZ = FinishMZ - startMZ;

            double x = ((i.MZ - startMZ) / deltaMZ) *  CanvasPeakDisplay.ActualWidth;
            double y = i.Intensity / maxIntensity * CanvasPeakDisplay.ActualHeight * compressionFactor;

            return new Tuple<double, double>(x, y);
        }

        private void CanvasPeakDisplay_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IonsInDisplay.Count > 1)
            {
                Plot(IonsInDisplay, startMZ, finishMZ, ppm, theoreticalSpectrum);
            }
        }

        private void CanvasPeakDisplay_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ZoomInProcess = true;
            startX = Mouse.GetPosition(CanvasPeakDisplay);

            ZoomSelection = new Rectangle();
            ZoomSelection.Height = CanvasPeakDisplay.ActualHeight;
            ZoomSelection.Width = 0;
            ZoomSelection.Opacity = 0.5;
            ZoomSelection.StrokeThickness = 3;
            ZoomSelection.Stroke = Brushes.DarkGray;
            ZoomSelection.Fill = Brushes.LightGray;
            ZoomSelection.Opacity = 0.3;

            CanvasPeakDisplay.Children.Add(ZoomSelection);
            Canvas.SetLeft(ZoomSelection, startX.X);


            Console.WriteLine(startX);
        }

        private void CanvasPeakDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (ZoomInProcess)
            {
                double newPos = Mouse.GetPosition(CanvasPeakDisplay).X;

                double width = Math.Abs(startX.X - newPos);
                ZoomSelection.Width = width;

                if (newPos >=startX.X)
                {
                    Canvas.SetLeft(ZoomSelection, startX.X);
                }
                else
                {
                    Canvas.SetLeft(ZoomSelection, startX.X - width);
                }

                
            }
        }

        private void CanvasPeakDisplay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point endX = Mouse.GetPosition(CanvasPeakDisplay);

            if (endX.X < startX.X)
            {
                Point t;
                t = endX;
                endX = startX;
                startX = t;
            }

            double startMZTemp = ((startX.X / CanvasPeakDisplay.ActualWidth) * (finishMZ - startMZ)) + startMZ;
            double finishMZTemp = ((endX.X / CanvasPeakDisplay.ActualWidth) * (finishMZ - startMZ)) + startMZ;

            Console.WriteLine("StartMz = " + startMZ);
            Console.WriteLine("FinishMz = " + finishMZ);

            Console.WriteLine("StartMzTmp = " + startMZTemp);
            Console.WriteLine("FinishMzTmp = " + finishMZTemp);

            Plot(IonsInDisplay, startMZTemp, finishMZTemp, ppm, theoreticalSpectrum);
        }

        private void CanvasPeakDisplay_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            PlotOriginal();
        }

        private void PlotOriginal()
        {
            Plot(OriginalIons, OriginalIons.Min(a => a.MZ) - 10, OriginalIons.Max(a => a.MZ) + 10, ppm, theoreticalSpectrum);
        }


    }
}
