using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PatternTools;
using System.Linq;
using System.IO;

namespace BelaGraph
{
    /// <summary>
    /// Interaction logic for BelaGraphControl.xaml
    /// </summary>
    public partial class BelaGraphControl : UserControl
    {

        /// <summary>
        /// the file sparse matrix is cached here and used for generating reports
        /// </summary>
        public SparseMatrix VennSparseMatrixCache
        {
            get { return vennSparseMatrixCache; }
            set { vennSparseMatrixCache = value; }
        }

        public void SetBottom(UIElement o, double position)
        {
            Canvas.SetLeft(o, position);
            Canvas.SetBottom(o, position);
        }

        public Canvas MyCanvas
        {
            get { return TheGraph;}
            set { TheGraph = value; }
        }

        //Lets expose the canvas

        public SparseMatrix vennSparseMatrixCache = new SparseMatrix();


        List<DataVector> dataVectors = new List<DataVector>();
        List<SolidColorBrush> colors = new List<SolidColorBrush>();
        List<double> xTicks = new List<double>(); //Used for stuff like generating a grid
        List<double> yTicks = new List<double>();

        List<MyVennCircle> vennCircles = new List<MyVennCircle>();

        //These should have accessors
        bool automaticTicks = true;
        double xMin; double xMax;
        double yMin; double yMax;
        double xTick; double yTick;
        int strokeThickness = 2;
        double vennRadiusOfLargestCircleCorrectionFactor = 2.5;

        public double VennRadiusOfLargestCircleCorrectionFactor
        {
            get { return vennRadiusOfLargestCircleCorrectionFactor; }
            set { vennRadiusOfLargestCircleCorrectionFactor = value; }
        }
        
        /// <summary>
        /// Specify fixed number of decimal places in the X axis
        /// </summary>
        public int XRound { get; set; }

        /// <summary>
        /// Specify fixed number of decimal places in the Y axis
        /// </summary>
        public int YRound { get; set; }

        //The mapped extreme values
        double mXMin;
        double mYMin;

        List<string> yLabels = new List<string>();
        List<string> xLabels = new List<string>();

        #region accessors

        public string Title
        {
            set { 
                labelTitle.Content = value;
                ToolTip t = new ToolTip();
                //t.Opened = "whenToolTipOpens";
                t.Content = value;
                labelTitle.ToolTip = t;
            }
        }

        public string XLabel
        {
            set { labelXLabel.Content = value; }
        }

        public int StrokeThickness
        {
            set { strokeThickness = value; }
        }

        public string YLabel
        {
            set {
                    string oldValue = "";
                    
                    if (labelyLabel.Content != null)
                    {
                        oldValue = labelyLabel.Content.ToString();
                    }

                    if (!oldValue.Equals(value))
                    {
                        labelyLabel.Content = value;
                        //Make a correction for the Y Label
                        labelyLabel.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                        Rect measureRect = new Rect(labelyLabel.DesiredSize);
                        labelyLabel.Arrange(measureRect);

                        TranslateTransform tt = new TranslateTransform();
                        tt.Y = -0.5 * measureRect.Width + 15;
                        RotateTransform rt = new RotateTransform();
                        rt.Angle = -90;

                        TransformGroup tg = new TransformGroup();
                        tg.Children.Add(tt);
                        tg.Children.Add(rt);

                        labelyLabel.RenderTransform = tg;
                    }

                }
        }

        /// <summary>
        /// If you want the axis, tick, and labels to be infered automatically
        /// set this to true
        /// </summary>
        public bool AutomaticTicks
        {
            get { return automaticTicks; }
            set { automaticTicks = value; }
        }

        public double XMin
        {
            get { return xMin; }
            set { xMin = value; }
        }

        public double XMax
        {
            get { return xMax; }
            set { xMax = value; }
        }
        public double YMin
        {
            get { return yMin; }
            set { yMin = value; }
        }
        public double YMax
        {
            get { return yMax; }
            set { yMax = value; }
        }

        public double XTick
        {
            get { return xTick; }
            set { xTick = value; }
        }

        public double YTick
        {
            get { return yTick; }
            set { yTick = value; }
        }

        public List<string> XLabels
        {
            get { return xLabels; }
            set { xLabels = value; }
        }

        #endregion

        //----------------
        public BelaGraphControl()
        {
            InitializeComponent();

            //Populate our color aray
            colors.Add(Brushes.Red);
            colors.Add(Brushes.Blue);
            colors.Add(Brushes.Green);
            colors.Add(Brushes.Violet);
            colors.Add(Brushes.Salmon);
            colors.Add(Brushes.AntiqueWhite);
            colors.Add(Brushes.RoyalBlue);
            colors.Add(Brushes.Yellow);
            colors.Add(Brushes.Orchid);
            colors.Add(Brushes.Black);
            colors.Add(Brushes.BlanchedAlmond);
            colors.Add(Brushes.BurlyWood);
            colors.Add(Brushes.Coral);
            colors.Add(Brushes.Cornsilk);
            colors.Add(Brushes.DarkBlue);
            colors.Add(Brushes.DarkGray);
            colors.Add(Brushes.DarkRed);

            System.Drawing.Bitmap logoBitmap = Properties.Resources.BelaGraphLogo;
            BitmapSource bmSource = Imaging.CreateBitmapSourceFromHBitmap(logoBitmap.GetHbitmap(), IntPtr.Zero, new Int32Rect(0, 0, logoBitmap.Width, logoBitmap.Height), BitmapSizeOptions.FromWidthAndHeight(logoBitmap.Width, logoBitmap.Height));
            imageBelaGraphLogo.Source = bmSource;
            imageBelaGraphLogo.ToolTip = "Belagraph is a plot lib. More information at http://pcarvalho.com/belagraph";
        }

        /// <summary>
        /// Sets the graphs title
        /// </summary>

        //Data vector manipulation methods-----
        public void AddDataVector(DataVector dv)
        {
            this.dataVectors.Add(dv);
        }


        /// <summary>
        /// Clears the datavectors, ylabels and xlabels overrides
        /// And hides the belagraph logo
        /// </summary>
        public void ClearDataBuffer()
        {
            dataVectors.Clear();
            yLabels.Clear();
            xLabels.Clear();
            xTicks.Clear();
            yTicks.Clear();
            imageBelaGraphLogo.Visibility = Visibility.Hidden;
            automaticTicks = true;
        }
        //------------------------------------

        #region plottingMethods

        public void Plot(BackgroundOption bg, bool drawTicksOption, bool drawFrameOnGraphOption, System.Drawing.Font theFontForLabels)
        {
            //Clean our workspace
            TheGraph.Children.Clear();
            canvasX.Children.Clear();
            canvasY.Children.Clear();
            xTicks.Clear();
            yTicks.Clear();
            imageBelaGraphLogo.Visibility = Visibility.Hidden;

            //Local extreme values
            xMin = double.MaxValue;
            yMin = double.MaxValue;
            xMax = double.MinValue;
            yMax = double.MinValue;

            //There is the automatic definition and the user definition
            //Define the extremes!            
            
            //xMin, yMin, xMax, yMax
            //Lets define the Local extremes
            List<double> results = FindExtremePoints(dataVectors);
            xMin = results[0];
            yMin = results[1];
            xMax = results[2];
            yMax = results[3];


            //Create a new list of mapped datavectors
            List<DataVector> mappedVectors = new List<DataVector>(dataVectors.Count);

            foreach (DataVector dv in dataVectors)
            {
                DataVector mappedVector = new DataVector(dv.MyGraphStyle, dv.Name);
                if (dv.MyBrush != null) { mappedVector.MyBrush = dv.MyBrush; }

                if (!dv.MyGraphStyle.Equals(GraphStyle.Pie))
                {
                    //dv.ThePoints.Sort((a, b) => a.Y.CompareTo(b.Y));
                    foreach (PointCav pc in dv.ThePoints)
                    {
                        mappedVector.ThePoints.Add(MapPoint(pc));
                    }
                    mappedVectors.Add(mappedVector);
                }
                else
                {
                    mappedVectors.Add(dv);
                }
            }


            //Lets define the global extremes, user for the plotting
            List<double> results2 = FindExtremePoints(mappedVectors);
            mXMin = results2[0];
            mYMin = results2[1];

            //And make sure the min is not equal to the max
            if (xMin == xMax)
            {
                if (xMin == 0) { xMax = 1; } else { xMin = 0; }
            }

            if (yMin == yMax)
            {
                if (yMax > 0) { yMin = 0; }
                else { yMin = YMax - 1; }
            }
            
            //Draw the graphs
            if (drawTicksOption) { drawTicks(GraphLabel.Numerical, theFontForLabels); }
            if (drawFrameOnGraphOption) { drawFrameOnGraph(Brushes.Beige); }
            if (!bg.Equals(BackgroundOption.None)) {DrawBackground(bg);}

            //------


            //Plot each datavector
            bool pieChartDrawn = false; //We can only draw 1 pie chart
            
            //For styles that require mapping
            foreach (DataVector dv in mappedVectors)
            {
                if (dv.MyGraphStyle == GraphStyle.Pixel) { PlotPixels(dv); }
                if (dv.MyGraphStyle == GraphStyle.Polygon) { PlotPolygon(dv); }
                if (dv.MyGraphStyle == GraphStyle.Bubble) { PlotBubble(dv); }
                if (dv.MyGraphStyle == GraphStyle.Line) { PlotLine(dv); }
                if (dv.MyGraphStyle == GraphStyle.SplineCardinal) { PlotSplineCardinal(dv); }
                if (dv.MyGraphStyle == GraphStyle.SplineAkima) { PlotSplineAkima(dv); }
                if (dv.MyGraphStyle == GraphStyle.Pie && !pieChartDrawn) { PlotPie(dv, theFontForLabels); pieChartDrawn = true; }
            }

            //For other types  --- for now, only the Venn
            List<DataVector> vennDvs = new List<DataVector>();
            foreach (DataVector dv in dataVectors)
            {
                if (dv.MyGraphStyle == GraphStyle.Venn) { vennDvs.Add(dv); }
            }
            if (vennDvs.Count > 0)
            {
                PlotVenn(vennDvs);
            }

        }

        


        #region plot engines

        private void PlotVenn(List<DataVector> dvs)
        {

            double greatestAmount = 0;
            int indexOfGreatestCircle = 0;
            double radiusOfLargestCircle = TheGraph.ActualHeight / vennRadiusOfLargestCircleCorrectionFactor;
            double algorithmSpeed = 8;

            List<int> noItemsInEachClass = new List<int>();

            //Step 1, verify which one has the greates amount
            foreach (DataVector dv in dvs)
            {
                List<double> extremes = dv.GetYExtremes();
                dv.ExtraParamDouble = extremes[1]; //We save here the number of items in the class
                if (extremes[1] > greatestAmount) {
                    greatestAmount = extremes[1];
                    indexOfGreatestCircle = dvs.IndexOf(dv);
                }
            }

            //Calculate matrix of overlapping area
            List<List<double>> overlaps = new List<List<double>>();
            List<List<double>> overlapArea = new List<List<double>>();
            vennCircles.Clear();

            double areaOfLargestCircle = Math.PI * Math.Pow(radiusOfLargestCircle, 2);

            for (int i = 0; i < dvs.Count; i++)
            {

                List<double> resultsOverLap = new List<double>();
                List<double> resultsOverLapArea = new List<double>();

                for (int j = 0; j < dvs[i].ThePoints.Count; j++)
                {
                    double overlapPercentage = dvs[i].ThePoints[j].Y / dvs[i].ThePoints[i].Y;
                    resultsOverLap.Add(overlapPercentage);
                    resultsOverLapArea.Add(overlapPercentage * Math.PI * Math.Pow(radiusOfLargestCircle * Math.Sqrt(dvs[i].ExtraParamDouble / greatestAmount),2));

                }
                overlaps.Add(resultsOverLap);
                overlapArea.Add(resultsOverLapArea);

            }

            //Determine location for circle centers
            //Theoretically, lets begin with all circles in the upper left, centralized and rightly justified
            
            for (int i = 0; i < dvs.Count; i++)
            {
                //Estimate the radius
                MyVennCircle mvc = new MyVennCircle();
                mvc.Radius = radiusOfLargestCircle * Math.Sqrt(dvs[i].ExtraParamDouble / greatestAmount);                

                PointCav p = new PointCav();
                mvc.Name = dvs[i].Name;
                p.X = radiusOfLargestCircle - mvc.Radius;// radiusOfLargestCircle - mvc.Radius;
                p.Y = 0;
                mvc.CenterOfCircle = p;
                
                mvc.MyDataVector = dvs[i];
                mvc.OverlapArea = overlapArea[i];
                vennCircles.Add(mvc);

            }

            //Find optimal points for mycircles 0 and mycircles 1
            double errorF = double.MaxValue;
            double iterations = 0;
            //Still having problems with the overlap function
            while (errorF-1 > vennCircles[0].OverlapArea[1] && iterations < 1000)
            {
                iterations++;
                PointCav p = new PointCav();
                p.X = vennCircles[1].CenterOfCircle.X + 2;
                p.Y = vennCircles[1].CenterOfCircle.Y;

                vennCircles[1].CenterOfCircle = p;
                errorF = VennManager.OverlapBetweenCircles(vennCircles[0].CenterOfCircle, vennCircles[1].CenterOfCircle, vennCircles[0].Radius, vennCircles[1].Radius, algorithmSpeed);

            }

            //Find optimal points for mycircle 0 and mycircles 2
            //This will be a good starting point for a genetic algorithm of some how to search a better place for the next circle\

            if (dvs.Count > 2)
            {
                errorF = double.MaxValue;
                iterations = 0;
                while (errorF - 1 >= vennCircles[0].OverlapArea[2] && iterations < 1000)
                {
                    iterations++;
                    PointCav p = new PointCav();
                    p.X = vennCircles[2].CenterOfCircle.X + 2;
                    p.Y = vennCircles[2].CenterOfCircle.Y;

                    vennCircles[2].CenterOfCircle = p;
                    errorF = VennManager.OverlapBetweenCircles(vennCircles[0].CenterOfCircle, vennCircles[2].CenterOfCircle, vennCircles[0].Radius, vennCircles[2].Radius, algorithmSpeed);

                }

                //Now we shall slide circle 2 along a circumference that hold its radius as to the center of circle 0
                //To nicely adjust into circle 1
                //the equation of the path goes, x^2 + y^2 = myCircles[2].CenterOfCircle.X (original)

                double originalPositionX = vennCircles[2].CenterOfCircle.X;
                double originalPositionY = vennCircles[2].CenterOfCircle.Y;

                errorF = double.MaxValue;
                for (double angle = 0; angle > (Math.PI / 2) * (-1); angle -= 0.15)
                {
                    //    //the error function is the sum of the distance of the theoretical overlap area
                    //    //from circlr 2 and circle 1.

                    PointCav p = new PointCav();
                    p.X = originalPositionX * Math.Cos(angle);
                    p.Y = originalPositionX * Math.Sin(angle) * (-1);

                    vennCircles[2].CenterOfCircle = p;

                    errorF = VennManager.OverlapBetweenCircles(vennCircles[1].CenterOfCircle, vennCircles[2].CenterOfCircle, vennCircles[1].Radius, vennCircles[2].Radius, algorithmSpeed);

                    if (errorF <= vennCircles[1].OverlapArea[2])
                    {
                        break;
                    }
                }
            }


            //Draw in all circles....
            int counter = 0;
            foreach (MyVennCircle circle in vennCircles)
            {
                counter++;
                Ellipse myEllipse = new Ellipse();
                //double myRadius = (circle.MyDataVector.ExtraParamDouble / greatestAmount) * (radiusOfLargestCircle);
                double myRadius = circle.Radius;
                myEllipse.Width = myRadius * 2;
                myEllipse.Height = myRadius * 2;

                if (circle.MyDataVector.MyBrush == null)
                {
                    //Lets Choose a random Brush
                    int chosenIndex = pTools.getRandomNumber(colors.Count - 1);
                    circle.MyDataVector.MyBrush = (SolidColorBrush)colors[chosenIndex].Clone();
                    circle.MyDataVector.MyBrush.Opacity = 0.50;
                }

                myEllipse.Fill = circle.MyDataVector.MyBrush;
                myEllipse.Stroke = Brushes.Black;
                myEllipse.StrokeThickness = 2;

                //Add a tooltip
                ToolTip tp = new ToolTip();
                tp.Content = circle.Name;
                myEllipse.ToolTip = tp;

                TheGraph.Children.Add(myEllipse);

                //Radius correction - lets see if this helps
                myEllipse.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Rect measureRect = new Rect(myEllipse.DesiredSize);
                circle.Radius = measureRect.Width/2;
                
                //level out all circles
                Canvas.SetTop(myEllipse, (radiusOfLargestCircle - circle.Radius) + 15 + circle.CenterOfCircle.Y);

                double mySetLeft = (radiusOfLargestCircle - circle.Radius) + circle.CenterOfCircle.X + 15;
                
                Canvas.SetLeft(myEllipse, mySetLeft);

                PointCav mappedCenter = new PointCav();

                mappedCenter.Y = Canvas.GetTop(myEllipse) + circle.Radius;
                mappedCenter.X = Canvas.GetLeft(myEllipse) + circle.Radius;


                
                circle.MappedCircleCenter = mappedCenter;
                
            }

        }


        public void PlotVennLabels(Dictionary <int, List<int>> venDict, string C1Name, string C2Name, string C3Name, bool addGlow, bool detailedLabel, SparseMatrixIndexParserV2 smip, System.Drawing.Font myFont)
        {
            //Wire up in case we use dragging events
            TheGraph.PreviewMouseMove += new System.Windows.Input.MouseEventHandler(MyCanvas_PreviewMouseMove);
            TheGraph.PreviewMouseRightButtonUp += new System.Windows.Input.MouseButtonEventHandler(MyCanvas_PreviewMouseRightButtonUp);
            TheGraph.PreviewMouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(MyCanvas_PreviewMouseRightButtonDown);


            //The total label
            List<int> allIndividuals = new List<int>();
            List<int> onlyCircle1 = new List<int>();
            List<int> onlyCircle2 = new List<int>();
            List<int> onlyCircle3 = new List<int>();
            List<int> onlyCircles12 = new List<int>();
            List<int> onlyCircles13 = new List<int>();
            List<int> onlyCircles23 = new List<int>();
            List<int> inAllCircles = new List<int>();

            allIndividuals = vennSparseMatrixCache.allDims();

            if (!venDict.ContainsKey(3))
            {
                //we are dealing with two classes
                foreach (int ind in allIndividuals)
                {
                    if (!venDict[2].Contains(ind))
                    {
                        onlyCircle1.Add(ind);
                    }
                }

                //Fill up for only in 2
                foreach (int ind in allIndividuals)
                {
                    if (!venDict[1].Contains(ind))
                    {
                        onlyCircle2.Add(ind);
                    }
                }

                foreach (int indv1 in venDict[1])
                {
                    if (venDict[2].Contains(indv1))
                    {
                        onlyCircles12.Add(indv1);
                    }
                }
            }
            else
            {
                //We are dealing with 3 classes

                //Fill up for only in 1
                foreach (int ind in allIndividuals)
                {
                    if (!venDict[2].Contains(ind) && !venDict[3].Contains(ind))
                    {
                        onlyCircle1.Add(ind);
                    }
                }

                //Fill up for only in 2
                foreach (int ind in allIndividuals)
                {
                    if (!venDict[1].Contains(ind) && !venDict[3].Contains(ind))
                    {
                        onlyCircle2.Add(ind);
                    }
                }

                //Fill up for only in 3
                foreach (int ind in allIndividuals)
                {
                    if (!venDict[2].Contains(ind) && !venDict[1].Contains(ind))
                    {
                        onlyCircle3.Add(ind);
                    }
                }

                //Fill up for onlycircles12
                foreach (int indv1 in venDict[1])
                {
                    if (venDict[2].Contains(indv1) && !venDict[3].Contains(indv1))
                    {
                        onlyCircles12.Add(indv1);
                    }
                }

                //Fill up for onlycircles13
                foreach (int indv1 in venDict[1])
                {
                    if (venDict[3].Contains(indv1) && !venDict[2].Contains(indv1))
                    {
                        onlyCircles13.Add(indv1);
                    }
                }

                //Fill for onlycircles 23
                foreach (int ind2 in venDict[2])
                {
                    if (venDict[3].Contains(ind2) && !venDict[1].Contains(ind2))
                    {
                        onlyCircles23.Add(ind2);
                    }
                }

                //Fill up for inAllcircles
                foreach (int ind in venDict[1])
                {
                    if (venDict[2].Contains(ind))
                    {
                        if (venDict[3].Contains(ind))
                        {
                            inAllCircles.Add(ind);
                        }
                    }
                }
            }

            PointCav cl1 = FindLabelLocation2(ref TheGraph, vennCircles, new List<bool> { true, false, false });
            PointCav cl2 = FindLabelLocation2(ref TheGraph, vennCircles, new List<bool> { false, true, false });
            PointCav cl3 = FindLabelLocation2(ref TheGraph, vennCircles, new List<bool> { false, false, true });
            PointCav cl12 = FindLabelLocation2(ref TheGraph, vennCircles, new List<bool> { true, true, false });
            PointCav cl13 = FindLabelLocation2(ref TheGraph, vennCircles, new List<bool> { true, false, true });
            PointCav cl23 = FindLabelLocation2(ref TheGraph, vennCircles, new List<bool> { false, true, true });
            PointCav cl123 = FindLabelLocation2(ref TheGraph, vennCircles, new List<bool> { true, true, true });


            Label labelTotal = new Label();

            labelTotal.FontFamily = new System.Windows.Media.FontFamily(myFont.Name);
            labelTotal.FontSize = myFont.Size;

            labelTotal.Content = "Total : " + allIndividuals.Count;
            TheGraph.Children.Add(labelTotal);
            Canvas.SetLeft(labelTotal, 0);
            Canvas.SetBottom(labelTotal, 0);
            
            TheGraph.AllowDrop = true;
            
            if (detailedLabel) {
                DrawLabel(ref TheGraph, cl1, "(1) " + onlyCircle1.Count, addGlow, onlyCircle1, smip, myFont);
                DrawLabel(ref TheGraph, cl2, "(2) " + onlyCircle2.Count, addGlow, onlyCircle2, smip, myFont);
                DrawLabel(ref TheGraph, cl12, "(1&2) " + onlyCircles12.Count, addGlow, onlyCircles12, smip, myFont);

                if (vennCircles.Count > 2)
                {
                    DrawLabel(ref TheGraph, cl3, "(3) " + onlyCircle3.Count, addGlow, onlyCircle3, smip, myFont);
                    DrawLabel(ref TheGraph, cl13, "(1&3) " + onlyCircles13.Count, addGlow, onlyCircles13, smip, myFont);
                    DrawLabel(ref TheGraph, cl123, "(1&2&3) " + inAllCircles.Count, addGlow, inAllCircles, smip, myFont);
                    DrawLabel(ref TheGraph, cl23, "(2&3) " + onlyCircles23.Count, addGlow, onlyCircles23, smip, myFont);
                }
            } else {

                DrawLabel(ref TheGraph, cl1, onlyCircle1.Count.ToString(), addGlow, onlyCircle1, smip, myFont);
                DrawLabel(ref TheGraph, cl2, onlyCircle2.Count.ToString(), addGlow, onlyCircle2, smip, myFont);
                DrawLabel(ref TheGraph, cl12, onlyCircles12.Count.ToString(), addGlow, onlyCircles12, smip, myFont);

                if (vennCircles.Count > 2)
                {
                    DrawLabel(ref TheGraph, cl3, onlyCircle3.Count.ToString(), addGlow, onlyCircle3, smip, myFont);
                    DrawLabel(ref TheGraph, cl13, onlyCircles13.Count.ToString(), addGlow, onlyCircles13, smip, myFont);
                    DrawLabel(ref TheGraph, cl123, inAllCircles.Count.ToString(), addGlow, inAllCircles, smip, myFont);
                    DrawLabel(ref TheGraph, cl23, onlyCircles23.Count.ToString(), addGlow, onlyCircles23, smip, myFont);
                }
            }

            //Add the side Label

            int counter = 0;
            double widthOfLargestLabel = 0;
            int extraSpace = 0;
            foreach (var circle in vennCircles)
            {
                counter++;
                Rectangle labelRect = new Rectangle();
                labelRect.Width = 13;
                labelRect.Height = 13;
                labelRect.Stroke = Brushes.DarkGray;
                labelRect.StrokeThickness = 1;

                Label myLabel = new Label();
                myLabel.FontFamily = new System.Windows.Media.FontFamily(myFont.Name);
                myLabel.FontSize = myFont.Size;

                //Label Color Index
                labelRect.Fill = circle.MyDataVector.MyBrush;
                Canvas.SetTop(labelRect, (counter * myFont.Size) +5 + extraSpace);

                //Label Text
                if (counter == 1)
                {
                    myLabel.Content = "(" + circle.Name + ") " + C1Name + " : " + circle.MyDataVector.ThePoints[counter - 1].Y;
                }
                else if (counter == 2)
                {
                    myLabel.Content = "(" + circle.Name + ") " + C2Name + " : " + circle.MyDataVector.ThePoints[counter - 1].Y;
                }
                else if (counter == 3)
                {
                    myLabel.Content = "(" + circle.Name + ") " + C3Name + " : " + circle.MyDataVector.ThePoints[counter - 1].Y;
                }
                
                Canvas.SetTop(myLabel, (counter * myFont.Size) + extraSpace);

                myLabel.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Rect measureRect = new Rect(myLabel.DesiredSize);

                if (measureRect.Width > widthOfLargestLabel) { widthOfLargestLabel = measureRect.Width; }

                
                Canvas.SetRight(myLabel, 15);
                //Canvas.SetRight(labelRect, measureRect.Width + 15);
                
                TheGraph.Children.Add(myLabel);
                TheGraph.Children.Add(labelRect);
                extraSpace += 5;
            }

            Canvas.SetRight(TheGraph.Children[TheGraph.Children.Count-1], widthOfLargestLabel + 15);
            Canvas.SetRight(TheGraph.Children[TheGraph.Children.Count - 3], widthOfLargestLabel + 15);
            Canvas.SetRight(TheGraph.Children[TheGraph.Children.Count - 5], widthOfLargestLabel + 15);
        }

        private void DrawLabel(ref Canvas TheGraph, PointCav labelCenter, string theText, bool addGlow, List<int> myPopulation, SparseMatrixIndexParserV2 smip, System.Drawing.Font theFont)
        {
            //We need to create a class that inherits from the normal label, so we can pass parameters
            MyLabel thisLabel = new MyLabel();
            thisLabel.Content = theText;
            thisLabel.FontFamily = new System.Windows.Media.FontFamily(theFont.Name);
            thisLabel.FontSize = theFont.Size;
            thisLabel.mySparseMatrixIndexParser = smip;

            //Add the glow effect
            DropShadowEffect dse = new DropShadowEffect();
            dse.ShadowDepth = 0;
            dse.BlurRadius = 8;
            dse.Opacity = 1;
            dse.Color = Colors.White;
            thisLabel.Effect = dse;

            TheGraph.Children.Add(thisLabel);



            thisLabel.MyExtraInfo = myPopulation;

            thisLabel.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            thisLabel.mySparseMatrix = vennSparseMatrixCache;
            Rect measureRect = new Rect(thisLabel.DesiredSize);

            // Add event handlers for double click functionality
            thisLabel.MouseDoubleClick += new MouseButtonEventHandler(thisLabel_MouseDoubleClick);            

            Canvas.SetTop(thisLabel, labelCenter.Y - (measureRect.Height/2) - 5);
            Canvas.SetLeft(thisLabel, labelCenter.X - measureRect.Width/2);
        }

        static void thisLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MyLabel l = (MyLabel)sender;
            Venn.ReportDisplay rd = new Venn.ReportDisplay(l.MyExtraInfo, l.mySparseMatrix, l.mySparseMatrixIndexParser);
            rd.Show();
        }



        private static PointCav FindLabelLocation2(ref Canvas TheGraph, List<MyVennCircle> mCircles, List<bool> myBools)
        {
            PointCav result = new PointCav(0, 0);
            double greatestSpan = 0;

            for (double y = 0; y < TheGraph.ActualHeight; y += 2)
            {
                double xMin = double.MaxValue;
                double xMax = double.MinValue;

                for (double x = 0; x < TheGraph.ActualWidth; x += 2)
                {
                    //Test if the point is in the required condition
                    List<bool> testBools = new List<bool>();
                    foreach (var b in myBools)
                    {
                        testBools.Add(false);
                    }

                    PointCav testPoint = new PointCav(x, y);

                    //Verify if were in

                    for (int c = 0; c < mCircles.Count; c++)
                    {
                        if (VennManager.CalculateDistance(testPoint, mCircles[c].MappedCircleCenter) < mCircles[c].Radius)
                        {
                            testBools[c] = true;
                        }
                    }

                    bool verify = true; ;
                    for (int b = 0; b < testBools.Count; b++)
                    {
                        if (!myBools[b] == testBools[b])
                        {
                            verify = false;
                        }
                    }

                    if (verify)
                    {
                        if (x < xMin) { xMin = x; }
                        if (x > xMax) { xMax = x; }
                    }

                }

                if ((xMax - xMin) >= greatestSpan)
                {
                    greatestSpan = (xMax - xMin);
                    result.Y = y;
                    result.ExtraParam = xMin.ToString();
                    result.ExtraParam2 = xMax.ToString(); ;
                    result.X = ((xMax - xMin) / 2) + xMin;
                }
            }

            return (result);
        }


        //-------------------------------------

        private void PlotPie(DataVector dv, System.Drawing.Font myFont)
        {
            double radius = (TheGraph.ActualHeight / 2) * 0.85;


            //Display a title
            labelTitle.Visibility = Visibility.Hidden;
            Label TitleLabel = new Label();
            TitleLabel.Content = labelTitle.Content;
            TitleLabel.FontSize = 20;
            TitleLabel.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Rect measureRect = new Rect(TitleLabel.DesiredSize);
            TheGraph.Children.Add(TitleLabel);
            Canvas.SetLeft(TitleLabel, radius-measureRect.Width/2);
            Canvas.SetTop(TitleLabel, -32);

            //Compute the radians for each datapoint
            //First, find the total
            double total = 0;
            foreach (PointCav pc in dv.ThePoints)
            {
                total += pc.X;
            }


            double lastRadsCounter = 0;
            int counter = 0;

            // Sort them by word length.
            dv.ThePoints.Sort((a, b) => b.X.CompareTo(a.X));

            double percentageAcumulated = 0;

            foreach (PointCav pc in dv.ThePoints)
            {
                //Just to make sure
                if (pc.X == 0) { continue; }

                //Now lets begin
                double radsForThisPoint = pc.X * 2 * Math.PI / total;

                //Lets try to draw an arc
                Polygon pg = new Polygon();
                pg.Points.Add(new System.Windows.Point(0, 0));

                double step = 0.002;
                for (double i = lastRadsCounter; i <= lastRadsCounter + radsForThisPoint + step; i += step)
                {
                    pg.Points.Add(new System.Windows.Point(radius * Math.Cos(i), radius * Math.Sin(i)));
                }

                int colorIndex = counter;
                if (counter > colors.Count - 2) { colorIndex = colors.Count - 1; }
                pg.Fill = colors[colorIndex];
                pg.Points.Add(new System.Windows.Point(0, 0));

                //Add a tooltip
                ToolTip tp = new ToolTip();
                tp.Content = pc.MouseOverTip;
                pg.ToolTip = tp;

                TheGraph.Children.Add(pg);
                Canvas.SetLeft(pg, radius);
                Canvas.SetTop(pg, TheGraph.ActualHeight / 2);

                
                //Add a label

                double percentageForPoint = pc.X * 100 / total;
                Rectangle labelRect = new Rectangle();
                labelRect.Width = 12;
                labelRect.Height = 12;
                labelRect.Stroke = Brushes.DarkGray;
                labelRect.StrokeThickness = 1;

                Label myLabel = new Label();
                myLabel.FontSize = myFont.Size;
                myLabel.FontFamily = new System.Windows.Media.FontFamily(myFont.Name);

                Canvas.SetLeft(labelRect, (2 * radius + 15));
                Canvas.SetLeft(myLabel, (2 * radius + 15 + 8));

                if (counter > 10)
                {
                    //The Rectangle
                    labelRect.Fill = Brushes.DarkGray;
                    Canvas.SetTop(labelRect, 11 * 15 + 7);

                    //The text
                    myLabel.Content = "Others: " + Math.Round(100-percentageAcumulated,1) + "%";
                    Canvas.SetTop(myLabel, 11 * 15);

                }
                else
                {

                    //Label Color Index
                    labelRect.Fill = colors[colorIndex];
                    Canvas.SetTop(labelRect, counter * 15 + 7);

                    //Label Text
                    percentageAcumulated += percentageForPoint;
                    myLabel.Content = pc.ExtraParam + " " + Math.Round(percentageForPoint,1) + "%";
                    Canvas.SetTop(myLabel, counter * 15);
                }

                TheGraph.Children.Add(labelRect);
                TheGraph.Children.Add(myLabel);

                //---

                counter++;
                lastRadsCounter += radsForThisPoint;

            }

        }

        
        //----------------------   

        public void PlotBubble(DataVector dv, double eRadius = 10, string bubbleName = "none")
        {
            
            foreach (PointCav pc in dv.ThePoints)
            {
                Ellipse myEllipse = new Ellipse();
                myEllipse.Name = bubbleName;

                myEllipse.Height = eRadius;
                myEllipse.Width = eRadius;

                
                //Lets check if the user defined a color, else we should define it
                if (dv.MyBrush == null)
                {
                    //Lets Choose a random Brush
                    int chosenIndex = pTools.getRandomNumber(colors.Count - 1);
                    dv.MyBrush = (SolidColorBrush)colors[chosenIndex].Clone();
                    dv.MyBrush.Opacity = 0.65;
                }

                myEllipse.Fill = dv.MyBrush;
                myEllipse.Stroke = Brushes.Black;
                myEllipse.StrokeThickness = 1;

                //Set the tooltip
                if (!string.IsNullOrEmpty(pc.MouseOverTip))
                {
                    ToolTip tp = new ToolTip();
                    tp.Content = pc.MouseOverTip;
                    ToolTipService.SetInitialShowDelay(myEllipse, 0);
                    ToolTipService.SetHasDropShadow(myEllipse, true);
                    ToolTipService.SetShowDuration(myEllipse, 30000);
                    ToolTipService.SetBetweenShowDelay(myEllipse, 0);
                    myEllipse.ToolTip = (tp);
                }


                TheGraph.Children.Add(myEllipse);

                if (dv.ThePoints.Count == 1)
                {
                    Canvas.SetLeft(myEllipse, (pc.X) - (myEllipse.Width) / 2);
                }
                else
                {
                    Canvas.SetLeft(myEllipse, (pc.X - mXMin) - (myEllipse.Width) / 2);

                }
                Canvas.SetTop(myEllipse, (pc.Y - mYMin) - (myEllipse.Height)/2);

            }

        }


        //----------------------   

        private void PlotLine(DataVector dv)
        {

            Polyline pn = new Polyline();

            if (!string.IsNullOrEmpty(dv.Name))
            {
                ToolTip tp = new ToolTip();
                tp.Content = dv.Name;
                ToolTipService.SetInitialShowDelay(pn, 0);
                ToolTipService.SetHasDropShadow(pn, true);
                ToolTipService.SetShowDuration(pn, 30000);
                ToolTipService.SetBetweenShowDelay(pn, 0);
                pn.ToolTip = (tp);
            }

            foreach (PointCav pc in dv.ThePoints)
            {
                System.Windows.Point p = new System.Windows.Point();

                p.X = pc.X;
                p.Y = pc.Y;

                pn.Points.Add(p);
            }
            //Lets Choose a random Brush

            int chosenIndex = pTools.getRandomNumber(colors.Count - 1);
            SolidColorBrush thebrush = (SolidColorBrush)colors[chosenIndex].Clone();

            thebrush.Opacity = 0.9;
            pn.Stroke = thebrush;
            pn.StrokeThickness = strokeThickness;

            TheGraph.Children.Add(pn);

        }

        //---------------------------------

        private void PlotSplineCardinal(DataVector dv)
        {
            CardinalSplineShape css = new CardinalSplineShape();

            if (!string.IsNullOrEmpty(dv.Name))
            {
                ToolTip tp = new ToolTip();
                tp.Content = dv.Name;
                ToolTipService.SetInitialShowDelay(css, 0);
                ToolTipService.SetHasDropShadow(css, true);
                ToolTipService.SetShowDuration(css, 30000);
                ToolTipService.SetBetweenShowDelay(css, 0);
                css.ToolTip = (tp);
            }
            PointCollection pcolection = new PointCollection();


            foreach (PointCav pc in dv.ThePoints)
            {
                System.Windows.Point p = new System.Windows.Point();

                p.X = pc.X;
                p.Y = pc.Y;
                pcolection.Add(p);
            }
            //Lets Choose a random Brush

            css.Points = pcolection;

            int chosenIndex = pTools.getRandomNumber(colors.Count - 1);
            SolidColorBrush thebrush = (SolidColorBrush)colors[chosenIndex].Clone();

            thebrush.Opacity = 0.9;
            css.Stroke = thebrush;
            css.StrokeThickness = strokeThickness;

            TheGraph.Children.Add(css);
        }
        //--------------------------------------

        private void PlotSplineAkima(DataVector dv)
        {
            double n = 500;

            //Generate the akima spline
            double[] x = dv.ThePoints.Select(a => a.X).ToArray();
            double[] y = dv.ThePoints.Select(a => a.Y).ToArray();
            alglib.spline1dinterpolant akima;
            alglib.spline1dbuildakima(x, y, out akima);

            DataVector dvAkima = new DataVector(GraphStyle.Line, dv.Name);

            List<PointCav> akimaPoints = new List<PointCav>();
            for (double i = dv.ThePoints[0].X; i <= dv.ThePoints.Last().X; i += (dv.ThePoints.Last().X - dv.ThePoints[0].X) / n)
            {
                dvAkima.AddPoint(i, alglib.spline1dcalc(akima, i));
            }

            PlotLine(dvAkima);




        }

        // -------------------------------------

        private void PlotPolygon(DataVector dv)
        {
            Polygon myPolygon = new Polygon();

            //Lets Choose a random Brush
            int chosenIndex = pTools.getRandomNumber(colors.Count - 1);

            foreach (PointCav pc in dv.ThePoints)
            {
                myPolygon.Points.Add(new System.Windows.Point(pc.X, pc.Y));
            }

            SolidColorBrush thebrush = (SolidColorBrush)colors[chosenIndex].Clone();
            thebrush.Opacity = 0.70;

            myPolygon.Fill = thebrush;
            myPolygon.Stroke = Brushes.Black;
            myPolygon.StrokeThickness = 1;

            TheGraph.Children.Add(myPolygon);

            Canvas.SetLeft(myPolygon, -mXMin);
            Canvas.SetTop(myPolygon, -mYMin);
            
            Console.WriteLine("C");
        }

        private void PlotPixels(DataVector dv)
        {

            //Lets Choose a random Brush
            int chosenIndex = pTools.getRandomNumber(colors.Count - 1);
            SolidColorBrush thebrush = (SolidColorBrush)colors[chosenIndex].Clone();

            foreach (PointCav pc in dv.ThePoints)
            {
                Rectangle r = new Rectangle();
                r.Width = 4;
                r.Height = 4;
                r.Fill = thebrush;
                TheGraph.Children.Add(r);
                Canvas.SetLeft(r, pc.X);
                Canvas.SetTop(r, pc.Y);
            }
        }

        #endregion plot egines

        #endregion

        //-------
        #region AxisAndScalingMethods

        public PointCav MapPoint(PointCav pc)
        {
            //And make sure the min is not equal to the max
            if (xMin == xMax)
            {
                if (xMin == 0) { xMax = 1; } else { xMin = 0; }
            }

            if (yMin == yMax)
            {
                if (yMax > 0) { yMin = 0; }
                else { yMin = YMax - 1; }
            }

            PointCav mappedPoint = new PointCav();

            mappedPoint.X = ((pc.X-xMin) / (xMax - xMin)) * TheGraph.ActualWidth;
            mappedPoint.Y = TheGraph.ActualHeight - (((pc.Y-yMin) / (yMax - yMin)) * TheGraph.ActualHeight);

            //This extraparemeter is mapped only accoeding to x; it premisses that the x.width = y.width to be true!
            //mappedPoint.extraParam = (pc.extraParam / (xMax - xMin)) * TheGraph.ActualWidth;
            mappedPoint.MouseOverTip = pc.MouseOverTip;

            return (mappedPoint);
        }

        //--------------------------------------

        private void drawFrameOnGraph(System.Windows.Media.SolidColorBrush b)
        {
            Rectangle myFrame = new Rectangle();
            myFrame.Width = TheGraph.ActualWidth;
            myFrame.Height = TheGraph.ActualHeight;
            myFrame.Stroke = Brushes.LightGray;
            myFrame.StrokeThickness = 1;
            myFrame.Fill = b;
            TheGraph.Children.Add(myFrame);
        }

        //----------------------------

        /// <summary>
        /// Can only be called after calculating the ticjs
        /// </summary>
        /// <param name="b"></param>
        public void DrawBackground(BackgroundOption b)
        {
            if (b.Equals(BackgroundOption.HorizontalBars))
            {
                for (int i = 1; i < yTicks.Count; i++)
                {
                    Rectangle myRectangle = new Rectangle();
                    myRectangle.Width = TheGraph.ActualWidth;
                    myRectangle.Height = yTicks[0] - yTicks[1];
                    myRectangle.Stroke = Brushes.LightGray;
                    myRectangle.StrokeThickness = 1;
                    myRectangle.Fill = Brushes.LightGray;
                    if (i % 2 == 0)
                    {
                        TheGraph.Children.Add(myRectangle);
                        Canvas.SetBottom(myRectangle, yTicks[i]);
                    }
                    
                }

            } else if (b.Equals(BackgroundOption.YellowXGradient)) {

                LinearGradientBrush rBrush = new LinearGradientBrush();
                rBrush.StartPoint = new System.Windows.Point(0, 0);
                rBrush.EndPoint = new System.Windows.Point(0, 1);

                Rectangle rectangleBackground = new Rectangle();
                rectangleBackground.Width = TheGraph.ActualWidth;
                rectangleBackground.Height = TheGraph.ActualHeight;

                GradientStopCollection gscollection = new GradientStopCollection();
                gscollection.Add(new GradientStop(Colors.Yellow, 0));
                gscollection.Add(new GradientStop(Colors.White, 0.45));
                gscollection.Add(new GradientStop(Colors.White, 0.55));
                gscollection.Add(new GradientStop(Colors.Yellow, 1));
                rBrush.GradientStops = gscollection;

                rectangleBackground.Fill = rBrush;

                TheGraph.Children.Add(rectangleBackground);
            
            
            } else if (b.Equals(BackgroundOption.Checkers)) {
                for (int i = 1; i < yTicks.Count; i++)// The rows
                {
                    for (int j = 0; j < xTicks.Count-1; j++)
                    {
                        Rectangle myRectangle = new Rectangle();
                        myRectangle.Width = xTicks[1] - xTicks[0];
                        myRectangle.Height = yTicks[0] - yTicks[1];
                        myRectangle.Stroke = Brushes.DarkGray;
                        myRectangle.StrokeThickness = 1;
                        myRectangle.Fill = Brushes.LightGray;
                        

                        // on even rows we print even rects, on odd rows we print odd rect
                        if (i % 2 == 0)
                        {
                            if (j % 2 == 0)
                            {
                                TheGraph.Children.Add(myRectangle);
                                Canvas.SetBottom(myRectangle, yTicks[i]);
                                Canvas.SetLeft(myRectangle, xTicks[j]);
                            }

                        }
                        else
                        {
                            if (j % 2 != 0)
                            {
                                TheGraph.Children.Add(myRectangle);
                                Canvas.SetBottom(myRectangle, yTicks[i]);
                                Canvas.SetLeft(myRectangle, xTicks[j]);
                            }
                        }

                    }                    
                }
            }

        }

        //--------------------------------------

        private void drawTicks(GraphLabel gl, System.Drawing.Font myFont )
        {
            //Inferr the Ticks
            if (automaticTicks)
            {
                xTick = (xMax - xMin) / 5;
                yTick = (yMax - yMin) / 5;
            }

            //Or if we have a label override
            if (xLabels.Count>0) {
                xTick = (xMax - xMin) / (xLabels.Count-1);
            }

            //Correction for negative numbers
            double correctionX = 0;
            if (xMin < 0)
            {
                PointCav cP = new PointCav();
                cP.X = xMin;
                cP.Y = 0;
                cP = MapPoint(cP);
                correctionX = Math.Abs(cP.X);
            }

            //Draw the xTicks
            int tickCounter = 0;
            for (double i = xMin; i <= xMax*1.01; i += xTick)
            {
                tickCounter++;

                //Map the tick
                PointCav pc = new PointCav();
                pc.X = i;
                pc.Y = 0;
                pc = MapPoint(pc);

                //Draw the Tick
                Line l = new Line();
                l.X1 = pc.X;
                l.X2 = pc.X;
                l.Y1 = 0;
                l.Y2 = 10;
                l.Stroke = Brushes.LightGray;
                l.StrokeThickness = 1;
                canvasX.Children.Add(l);
                xTicks.Add(pc.X);
                Canvas.SetTop(l, 0);

                //Draw the label
                string theLabel = "No Label";
                Label myLabel = new Label();
                myLabel.FontSize = myFont.Size;
                myLabel.FontFamily = new FontFamily(myFont.Name);

                if (xLabels.Count > 0) {
                    try
                    {
                        theLabel = xLabels[int.Parse((tickCounter - 1).ToString())];
                    }
                    catch (FormatException) 
                    {
                        theLabel = "0";
                    }
                } else {
                    if (!XRound.Equals(null))
                    {
                        theLabel = Math.Round(i, XRound).ToString();
                    }
                    else
                    {
                        theLabel = i.ToString();
                    }
                }

                myLabel.Content = theLabel;
                canvasX.Children.Add(myLabel);

                //Measure it
                myLabel.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Rect measureRect = new Rect(myLabel.DesiredSize);
                myLabel.Arrange(measureRect);

                //Place it in Canvas
                Canvas.SetLeft(myLabel, (pc.X - (measureRect.Width)/2) + correctionX);       
                Canvas.SetTop(myLabel, 10);

            }

            //Correction for negative numbers
            double correctionY = 0;
            if (yMin < 0)
            {
                PointCav cPy = new PointCav();
                cPy.X = 0;
                cPy.Y = yMax;
                cPy = MapPoint(cPy);
                correctionY = Math.Abs(cPy.Y);
            }

            //Draw the yTicks
            for (double i = yMin; i <= yMax*1.01; i += yTick)
            {
                //Map the tick
                PointCav pc = new PointCav();
                pc.X = 0;
                pc.Y = i;
                pc = MapPoint(pc);

                //Draw the Tick
                Line l = new Line();
                l.X1 = 0;
                l.X2 = 10;
                l.Y1 = pc.Y;
                l.Y2 = pc.Y;
                l.Stroke = Brushes.LightGray;
                l.StrokeThickness = 1;
                canvasY.Children.Add(l);
                Canvas.SetRight(l, 0);
                Canvas.SetTop(l, -correctionY);
                yTicks.Add(pc.Y);

                //Draw the label
                if (gl.Equals(GraphLabel.Numerical))
                {
                    Label myLabel = new Label();
                    myLabel.FontSize = myFont.Size;
                    myLabel.FontFamily = new System.Windows.Media.FontFamily(myFont.Name);

                    string theLabel = i.ToString();
                    //The label cant get more than 8 char!

                    if (!YRound.Equals(null))
                    {
                        double shrinkedLabel = Math.Round(double.Parse(theLabel), YRound);
                        theLabel = shrinkedLabel.ToString();
                    }

                    myLabel.Content = theLabel;
                    myLabel.HorizontalAlignment = HorizontalAlignment.Right;
                    canvasY.Children.Add(myLabel);

                    //Measure it
                    myLabel.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    Rect measureRect = new Rect(myLabel.DesiredSize);
                    myLabel.Arrange(measureRect);

                    Canvas.SetLeft(myLabel, 90-measureRect.Width - 10);
                    Canvas.SetTop(myLabel, (pc.Y - 12) - correctionY);
                }
            }
        }

        #endregion


        #region Misc


        /// <summary>
        /// returns xMin, yMin, xMax, yMax respectively
        /// </summary>
        /// <returns></returns>
        private static List<double> FindExtremePoints(List<DataVector> dataVectors)
        {

            List<double> results = new List<double>(4);

            //xMin [0]
            results.Add(double.MaxValue);

            //yMin [1]
            results.Add(double.MaxValue);

            //xMax [2]
            results.Add(double.MinValue);

            //yMax [3]
            results.Add(double.MinValue);

            foreach (DataVector dv in dataVectors)
            {
                List<double> xXtremes = dv.GetXExtremes();
                List<double> yXtremes = dv.GetYExtremes();

                if (xXtremes[0] < results[0]) { results[0] = xXtremes[0]; }
                if (xXtremes[1] > results[2]) { results[2] = xXtremes[1]; }
                if (yXtremes[0] < results[1]) { results[1] = yXtremes[0]; }
                if (yXtremes[1] > results[3]) { results[3] = yXtremes[1]; }
            }

            return (results);
        }

        public void SaveGraph(string fileName)
        {

            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)this.ActualWidth,
                (int)this.ActualHeight, 1 / 300, 1 / 300, PixelFormats.Pbgra32);
            bmp.Render(this);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(fs);
            fs.Close();

        }

        #endregion



        #region DragEventHandlers

        void window1_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape && _isDragging)
            {
                DragFinished(true);
            }
        }

        void MyCanvas_PreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_isDown)
            {
                DragFinished(false);
                e.Handled = true;
            }
        }

        private void DragFinished(bool cancelled)
        {
            System.Windows.Input.Mouse.Capture(null);
            if (_isDragging)
            {
                AdornerLayer.GetAdornerLayer(_overlayElement.AdornedElement).Remove(_overlayElement);

                if (cancelled == false)
                {
                    Canvas.SetTop(_originalElement, _originalTop + _overlayElement.TopOffset);
                    Canvas.SetLeft(_originalElement, _originalLeft + _overlayElement.LeftOffset);
                }
                _overlayElement = null;

            }
            _isDragging = false;
            _isDown = false;
        }

        void MyCanvas_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_isDown)
            {
                if ((_isDragging == false) && ((Math.Abs(e.GetPosition(TheGraph).X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(e.GetPosition(TheGraph).Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                {
                    DragStarted();
                }
                if (_isDragging)
                {
                    DragMoved();
                }
            }
        }

        private void DragStarted()
        {
            _isDragging = true;
            _originalLeft = Canvas.GetLeft(_originalElement);
            _originalTop = Canvas.GetTop(_originalElement);

            _overlayElement = new SimpleCircleAdorner(_originalElement);
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(_originalElement);
            layer.Add(_overlayElement);

        }

        private void DragMoved()
        {
            System.Windows.Point CurrentPosition = System.Windows.Input.Mouse.GetPosition(TheGraph);

            _overlayElement.LeftOffset = CurrentPosition.X - _startPoint.X;
            _overlayElement.TopOffset = CurrentPosition.Y - _startPoint.Y;

        }

        void MyCanvas_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Source == TheGraph)
            {
            }
            else
            {
                _isDown = true;
                _startPoint = e.GetPosition(TheGraph);
                _originalElement = e.Source as UIElement;
                TheGraph.CaptureMouse();
                e.Handled = true;
            }
        }

        private System.Windows.Point _startPoint;
        private double _originalLeft;
        private double _originalTop;
        private bool _isDown;
        private bool _isDragging;
        private UIElement _originalElement;
        private SimpleCircleAdorner _overlayElement;

    }

    // Adorners must subclass the abstract base class Adorner.
    public class SimpleCircleAdorner : Adorner
    {
        // Be sure to call the base class constructor.
        public SimpleCircleAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            VisualBrush _brush = new VisualBrush(adornedElement);

            _child = new Rectangle();
            _child.Width = adornedElement.RenderSize.Width;
            _child.Height = adornedElement.RenderSize.Height;


            DoubleAnimation animation = new DoubleAnimation(0.3, 1, new Duration(TimeSpan.FromSeconds(1)));
            animation.AutoReverse = true;
            animation.RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever;
            _brush.BeginAnimation(System.Windows.Media.Brush.OpacityProperty, animation);

            _child.Fill = _brush;
        }

        // A common way to implement an adorner's rendering behavior is to override the OnRender
        // method, which is called by the layout subsystem as part of a rendering pass.
        protected override void OnRender(DrawingContext drawingContext)
        {
            // Get a rectangle that represents the desired size of the rendered element
            // after the rendering pass.  This will be used to draw at the corners of the 
            // adorned element.
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);

            // Some arbitrary drawing implements.
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
            renderBrush.Opacity = 0.2;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
            double renderRadius = 5.0;

            // Just draw a circle at each corner.
            drawingContext.DrawRectangle(renderBrush, renderPen, adornedElementRect);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _child;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        public double LeftOffset
        {
            get
            {
                return _leftOffset;
            }
            set
            {
                _leftOffset = value;
                UpdatePosition();
            }
        }

        public double TopOffset
        {
            get
            {
                return _topOffset;
            }
            set
            {
                _topOffset = value;
                UpdatePosition();

            }
        }

        private void UpdatePosition()
        {
            AdornerLayer adornerLayer = this.Parent as AdornerLayer;
            if (adornerLayer != null)
            {
                adornerLayer.Update(AdornedElement);
            }
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            GeneralTransformGroup result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(_leftOffset, _topOffset));
            return result;
        }

        private Rectangle _child = null;
        private double _leftOffset = 0;
        private double _topOffset = 0;


        #endregion


    }

    public enum GraphLabel
    {
        Numerical,
        Text
    }

    public enum BackgroundOption
    {
        Checkers,
        HorizontalBars,
        YellowXGradient,
        None,
    }
}
