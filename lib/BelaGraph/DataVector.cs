using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace BelaGraph
{
    public class DataVector
    {
        List<PointCav> thepoints = new List<PointCav>();
        GraphStyle myGraphStyle;
        string name;
        public SolidColorBrush MyBrush {get; set;}
        public double ExtraParamDouble { get; set; }
        public object ExtraParamObject { get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<PointCav> ThePoints
        {
            get { return thepoints; }
            set { thepoints = value; }
        }

        public GraphStyle MyGraphStyle
        {
            get { return myGraphStyle; }
            set { myGraphStyle = value; }
        }

        public DataVector(GraphStyle graphStyle, string name)
        {
            this.myGraphStyle = graphStyle;
            this.name = name;
        }

        public DataVector(GraphStyle graphStyle, string name, SolidColorBrush myBrush)
        {
            this.myGraphStyle = graphStyle;
            this.name = name;
            this.MyBrush = myBrush;
        }

        //-----------------------------

        public void AddPoint(double x, double y)
        {

            PointCav myPoint = new PointCav();
            myPoint.X = x;
            myPoint.Y = y;
            thepoints.Add(myPoint);
        }

        public void AddPoint(double x, double y, string tip)
        {
            PointCav myPoint = new PointCav();
            myPoint.X = x;
            myPoint.Y = y;
            myPoint.MouseOverTip = tip;
            thepoints.Add(myPoint);

        }

        public void AddPoint(PointCav pc)
        {
            thepoints.Add(pc);
        }

        //-----------------------------

        /// <summary>
        /// Returns a list containing the minimum and maximum x values respectively
        /// </summary>
        /// <returns></returns>
        public List<double> GetXExtremes()
        {
            double theMin = double.MaxValue;
            double theMax = double.MinValue;

            foreach (PointCav p in thepoints)
            {
                if (p.X > theMax) { theMax = p.X; }
                if (p.X < theMin) { theMin = p.X; }
            }

            List<double> theAnswer = new List<double>(2);
            theAnswer.Add(theMin);
            theAnswer.Add(theMax);

            return (theAnswer);
        }


        /// <summary>
        /// Returns a list containing the minimum and maximum y values respectively
        /// </summary>
        /// <returns></returns>
        public List<double> GetYExtremes()
        {
            double theMin = double.MaxValue;
            double theMax = double.MinValue;

            foreach (PointCav p in thepoints)
            {
                if (p.Y > theMax) { theMax = p.Y; }
                if (p.Y < theMin) { theMin = p.Y; }
            }

            List<double> theAnswer = new List<double>(2);
            theAnswer.Add(theMin);
            theAnswer.Add(theMax);

            return (theAnswer);

        }
    }


}
