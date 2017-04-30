using PatternTools;
using PatternTools.MSParser;
using PatternTools.XIC;
using SEProQ.Cluster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GammaFitter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SEProQ.Viewers.XICViewer xViewer = new SEProQ.Viewers.XICViewer();

            List<Ion> ions = new List<Ion>();
            ions.Add(new Ion(100, 579954.3, 38.65, 1));
            ions.Add(new Ion(100, 8586223.2, 38.77, 1));
            ions.Add(new Ion(100, 27249968.4, 38.894, 1));
            ions.Add(new Ion(100, 27249968.4, 38.894, 1));
            ions.Add(new Ion(100, 772466.5, 39.016, 1));
            ions.Add(new Ion(100, 568579.9, 39.016, 1));
            ions.Add(new Ion(100, 18987161.4, 39.137, 1));

            IonCluster ic = new IonCluster(ions);
            xViewer.Plot(ic);

            //Plot the sNormal
            //Lets fit a gamma distribution
            List<Point> myPoints = ions.Select(a => new Point(a.RetentionTime, a.Intensity)).ToList();
            List<double> sNormalParams = SkewedNormal.FindParameters(myPoints);

            double rms = SkewedNormal.CalculateRMS(myPoints, 0.12, 0, 21.841, 75021724);
            
            Chart c = xViewer.MyChart;
            for (double i = ions.Min(a => a.RetentionTime); i < ions.Max(a => a.RetentionTime); i += 0.01)
            {
                double v = SkewedNormal.X(i, sNormalParams[3], sNormalParams[0], sNormalParams[1], sNormalParams[2]);
                //double v = SkewedNormal.X(i, 0.5, -11.6, 21.631, 114021724);
                //double v = SkewedNormal.X(i, 0.12, 0, 21.841, 75021724);
                //double v = SkewedNormal.StandardNormal(i, 3);
                c.Series[2].Points.AddXY(i, v);
            }


            xViewer.ShowDialog();
        }
    }
}
