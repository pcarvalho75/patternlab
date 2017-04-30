using System;
using System.Collections.Generic;
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
using System.Linq;
using PatternTools;

namespace WPFMachineLearningCanvas
{
    /// <summary>
    /// Interaction logic for MLCanvas.xaml
    /// </summary>
    public partial class MLCanvas : UserControl
    {
        public MLCanvas()
        {
            InitializeComponent();
        }

        public Canvas MyCanvas
        {
            get { return myCanvas; }
            set { myCanvas = value; }
        }

        private List<PatternTools.Point> leftClass = new List<PatternTools.Point>();
        private List<PatternTools.Point> rightClass = new List<PatternTools.Point>();
        private List<PatternTools.Point> middleClass = new List<PatternTools.Point>();

        //Must be used for a non sparse matrix
        double CanvasWidth;
        double CanvasHeight;

        public int dimForX { get; set; }
        public int dimForY { get; set; }

        List<double> minValueOfDims = new List<double>();
        List<double> maxValueOfDims = new List<double>();

        public List<PatternTools.Point> LeftClass
        {
            get { return leftClass; }
        }

        public List<PatternTools.Point> RightClass
        {
            get { return rightClass; }
        }

        public List<PatternTools.Point> MiddleClass
        {
            get { return middleClass; }
        }

        public void Clear()
        {
            leftClass.Clear();
            rightClass.Clear();
            middleClass.Clear();


            CanvasWidth = 0;
            CanvasHeight = 0;

            minValueOfDims.Clear();
            maxValueOfDims.Clear();

            myCanvas.Children.Clear();

            LabelYMax.Content = " ? ";
            LabelXMax.Content = " ? ";
            LabelYMin.Content = " ? ";
            LabelXMin.Content = " ? ";
        }

        public SparseMatrix SparseMatrix
        {
            set
            {
                CanvasWidth = this.ActualWidth;
                CanvasHeight = this.ActualHeight - 40;  //We need to remove the space used up by the information bar

                //Generate the information for mapping points to the canvas
                if (minValueOfDims.Count == 0)
                {
                    for (int i = 0; i < value.theMatrixInRows[0].Dims.Count; i++)
                    {
                        minValueOfDims.Add(value.theMatrixInRows.Select(a => a.Values[i]).Min());
                        maxValueOfDims.Add(value.theMatrixInRows.Select(a => a.Values[i]).Max());
                    }

                    LabelXMax.Content = Math.Round(maxValueOfDims[dimForX], 2);
                    LabelXMin.Content = Math.Round(minValueOfDims[dimForX], 2);
                    LabelYMax.Content = Math.Round(maxValueOfDims[dimForY], 2);
                    LabelYMin.Content = Math.Round(minValueOfDims[dimForY], 2);
                }

                for (int i = 0; i < value.theMatrixInRows.Count; i++)
                {
                    sparseMatrixRow r = value.theMatrixInRows[i];
                    Brush b = Brushes.Black;

                    PatternTools.Point p = new PatternTools.Point(MapPoint(r.Values[dimForX], r.Values[dimForY]));

                    if (r.Lable == 0)
                    {
                        b = Brushes.Green;
                        middleClass.Add(p);
                    }
                    else if (r.Lable == -1)
                    {
                        b = Brushes.Red;
                        leftClass.Add(p);
                    }
                    else if (r.Lable == 1)
                    {
                        b = Brushes.Blue;
                        rightClass.Add(p);
                    }

                    PaintCircle(p.X, p.Y, b);

                }


            }

            get
            {
                PatternTools.SparseMatrix sm = new PatternTools.SparseMatrix();

                foreach (PatternTools.Point p in leftClass)
                {
                    PatternTools.sparseMatrixRow r = new PatternTools.sparseMatrixRow(-1);
                    r.Dims = new List<int> { 0, 1 };
                    r.Values = new List<double> { p.X, p.Y };
                    sm.theMatrixInRows.Add(r);
                }

                foreach (PatternTools.Point p in middleClass)
                {
                    PatternTools.sparseMatrixRow r = new PatternTools.sparseMatrixRow(0);
                    r.Dims = new List<int> { 0, 1 };
                    r.Values = new List<double> { p.X, p.Y };
                    sm.theMatrixInRows.Add(r);
                }

                foreach (PatternTools.Point p in rightClass)
                {
                    PatternTools.sparseMatrixRow r = new PatternTools.sparseMatrixRow(1);
                    r.Dims = new List<int> { 0, 1 };
                    r.Values = new List<double> { p.X, p.Y };
                    sm.theMatrixInRows.Add(r);
                }

                return sm;
            }
        }


        public void PaintDecisionSurface (IPLDiscriminator discriminator)
        {

            if (CanvasWidth == 0)
            {
                throw new Exception("The canvas height and width must be previously specified before printing a decision surface");
            }

            SolidColorBrush bBrush = new SolidColorBrush(Colors.Blue);
            SolidColorBrush rBrush = new SolidColorBrush(Colors.Red);
            SolidColorBrush gBrush = new SolidColorBrush(Colors.Green);

            bBrush.Opacity = 0.45;
            rBrush.Opacity = 0.45;
            gBrush.Opacity = 0.45;

            for (double x = 0; x < MyCanvas.ActualWidth; x += 5)
            {
                for (double y = 0; y < MyCanvas.ActualHeight; y += 5)
                {

                    int theClass = discriminator.SimpleClassification(MapSurfaceToOriginalSpace(x, y ));


                    if (theClass == 1)
                    {
                        PaintSquare(5, x, y, bBrush, 0);

                    }
                    else if (theClass == -1)
                    {
                        //Color the square in red
                        PaintSquare(5, x, y, rBrush, 0);
                    }
                    else if (theClass == 0)
                    {
                        //Color the square in red
                        PaintSquare(5, x, y, gBrush, 0);
                    }

                }

            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point mouseCurrent = e.GetPosition(null);
            leftClass.Add(new PatternTools.Point(mouseCurrent.X, mouseCurrent.Y));

            PaintCircle(mouseCurrent.X, mouseCurrent.Y, Brushes.Blue);

        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Media.Effects.OuterGlowBitmapEffect theEffect = new System.Windows.Media.Effects.OuterGlowBitmapEffect();
            theEffect.GlowColor = Colors.White;
            theEffect.Opacity = 0.7;
            theEffect.GlowSize = 10;
            theEffect.Noise = 0.5;

            System.Windows.Point mouseCurrent = e.GetPosition(null);
            rightClass.Add(new PatternTools.Point(mouseCurrent.X, mouseCurrent.Y));

            PaintCircle(mouseCurrent.X, mouseCurrent.Y, Brushes.Red);
        }

        public void PaintCircle(double x, double y, Brush b)
        {
            //Add an ellipse
            Ellipse myEllipse = new Ellipse();
            myEllipse.Width = 10;
            myEllipse.Height = 10;
            myEllipse.Fill = b;
            myEllipse.ToolTip = "(" + x + "," + y + ")";

            myEllipse.Stroke = Brushes.Gray;
            myEllipse.Opacity = 0.6;

            myCanvas.Children.Add(myEllipse);
            Canvas.SetTop(myEllipse, y);
            Canvas.SetLeft(myEllipse, x);
        }

        public void PaintSquare(double size, double x, double y, Brush b, double gx)
        {
            System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
            r.Width = size;
            r.Height = size;
            r.Fill = b;
            r.ToolTip = "(" + x + "," + y + ") = " + gx;
            myCanvas.Children.Add(r);
            Canvas.SetTop(r, y);
            Canvas.SetLeft(r, x);

        }

        public double[] MapPointToSurface(double[] p)
        {
            double correctionFactorX = (p[0] - minValueOfDims[dimForX]) / (maxValueOfDims[dimForX] - minValueOfDims[dimForX]);
            double x = correctionFactorX * CanvasWidth;

            //Y
            double correctionFactorY = (p[1] - minValueOfDims[dimForY]) / (maxValueOfDims[dimForY] - minValueOfDims[dimForY]);
            double y = correctionFactorY * CanvasHeight;

            return new double[] { x, y };
        }

        public double[] MapPoint(double X, double Y)
        {
            return MapPointToSurface(new double[] { X, Y });
        }

        public double [] MapSurfaceToOriginalSpace(double x, double y)
        {
            double cfX = ( (x / CanvasWidth))  * ((maxValueOfDims[dimForX] - minValueOfDims[dimForX])) + +minValueOfDims[dimForX];
            double cfY = ( (y / CanvasHeight)) * ((maxValueOfDims[dimForY] - minValueOfDims[dimForY])) + +minValueOfDims[dimForY];

            //double X = x * correctionFactorX;
            //double Y = y * correctionFactorY;
            //return new double[] { X, Y };

            return new double[] { cfX, cfY };
        }

    }
}
