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

namespace PatternTools
{
    /// <summary>
    /// Interaction logic for PPMHeatMap.xaml
    /// </summary>
    public partial class HeatMap : UserControl
    {
        public List<List<double>> MyMatrix { get; set; }
        
        public double ScaleMin {
            get;
            set;
        }

        public double ScaleMiddle
        {
            get;
            set;  
        }

        public double ScaleMax
        {
            get;
            set;
        }


        public HeatMap()
        {
            InitializeComponent();
        }

        public void AddPointsToPlot(List<PatternTools.Point> thePoints)
        {

            foreach (PatternTools.Point p in thePoints)
            {
                Ellipse e = new Ellipse();
                e.Width = 10;
                e.Height = 10;
                e.Opacity = 0.8;
                e.Fill = new SolidColorBrush(Colors.Green);
                e.ToolTip = p.Tip;

                e.Stroke = Brushes.DarkGreen;

                canvas1.Children.Add(e);


                //Lets try
                //X is scan number
                //Y is MZ

                double x = canvas1.ActualWidth * p.X;
                double y = canvas1.ActualHeight * p.Y;

                Canvas.SetLeft(e, x);
                Canvas.SetTop(e, y);
            }
        }

        public void Plot(double cWidth, double cHeight)
        {
            Plot(ScaleMin, ScaleMiddle, ScaleMax, cWidth, cHeight);
        }


        public void Plot(double scaleMin, double scaleMiddle, double scaleMax, double cWidth, double cHeight)
        {
            ScaleMin = scaleMin;
            ScaleMiddle = scaleMiddle;
            ScaleMax = scaleMax;

            canvas1.Children.Clear();

            //find the bounds
            List<double> allValues = new List<double>();
            foreach (List<double> list in MyMatrix)
            {
                allValues.AddRange(list);
            }

            double minValue = allValues.Min();
            double maxValue = allValues.Max();


            //Determine rectangle width and height;
            double rectWidth = cWidth / (double)MyMatrix.Count;
            double rectHeight = cHeight / (double)MyMatrix[0].Count;


            //In the plot, x axis is scan number and y axis is mz (higher more)
            //in the matrix, each line holds the same scan number
            for (int x = 0; x < MyMatrix.Count; x++) //the scan number 
            {
                for (int y = 0; y < MyMatrix[x].Count; y++) //the mz
                {
                    Rectangle r = new Rectangle();
                    r.Width = rectWidth;
                    r.Height = rectHeight;
                    double value = MyMatrix[x][y];

                    //Calculate the color
                    Color c = new Color();
                    byte red = 0;
                    byte blue = 0;

                    if (value > scaleMiddle)
                    {
                        blue = 0;

                        double result = (Math.Abs( (value - scaleMiddle)) / (scaleMax - scaleMin)) * 255;
                        if (result > 255)
                        {
                            result = 255;
                        }
                        //blue = (byte)Math.Floor(result);
                        blue = (byte)Math.Floor(result);
                    }
                    else
                    {
                        red = 0;

                        double result = Math.Abs(value - scaleMiddle) / (scaleMax - scaleMin) * 255;
                        if (result > 255)
                        {
                            result = 255;
                        }

                        red = (byte)Math.Floor(result);
                    }

                    c.A = 255;
                    c.R = red;
                    c.G = 0;
                    c.B = blue;

                    SolidColorBrush s = new SolidColorBrush(c);

                    r.Fill = s;
                    r.ToolTip = "X: " + x + " Y: " + y + " Value: " + value;

                    r.Stroke = Brushes.Black;


                    canvas1.Children.Add(r);
                    Canvas.SetLeft(r, x * rectWidth);
                    Canvas.SetTop(r, y * rectHeight);

                }
            }

            this.UpdateLayout();
        }

    }
}
