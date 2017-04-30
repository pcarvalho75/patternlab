using PatternTools;
using PatternTools.MSParser;
using PatternTools.SQTParser;
using PatternTools.XIC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQuant.Quants
{
    [Serializable]
    public class Quant
    {
        //public string Sequence { get; set; }

        /// <summary>
        /// /// Returns the list of ions in the format [mz, intensity, scanNo, RetTime]
        /// </summary>
        /// 
        public double[,] MyIons { get; set; }
        public int ScanNoMS2 { get; set; }
        public double PrecursorMZ { get; set; }
        public int Z { get; set; }
        public double QuantArea { get; set; }

        public List<IonLight> GetIonsLight ()
        {
            List<IonLight> theseIons = new List<IonLight>(MyIons.GetLength(1));

                for (int y = 0; y < MyIons.GetLength(1); y++)
                {
                    double mz = MyIons[0,y];
                    double intensity = MyIons[1,y];
                    double scanNo = MyIons[2,y];
                    double RetTime = Math.Round(MyIons[3,y],3);
                    theseIons.Add(new IonLight(mz, intensity, RetTime, (int)scanNo));
                }
                return theseIons;
        }

        public double AverageIntensity(int topX)
        {
            double [] d = new double[MyIons.GetLength(1)];

            for (int y = 0; y < MyIons.GetLength(1); y++)
            {
                d[y] = MyIons[1, y];
            }

            List<double> dd = d.ToList();
            if (dd.Count > topX)
            {
                dd.Sort((a, b) => b.CompareTo(a));
                dd.RemoveRange(topX - 1, dd.Count - topX);
            }

            return dd.Average();
        }



        /// <summary>
        /// Used for serialization purposes only
        /// </summary>
        public Quant() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="ionCluster"></param>
        /// <param name="ScanNo">If this was not identified by a scan number please enter -1</param>
        /// <param name="z"></param>
        public Quant(
            //string sequence,
            double[,] ionCluster,
            int ScanNo,
            int z,
            double precursorMZ
            )
        {
            //Sequence = sequence;
            MyIons = ionCluster;
            ScanNoMS2 = ScanNo;
            Z = z;
            PrecursorMZ = precursorMZ;
            if (MyIons.GetLength(1) > 2)
            {
                QuantArea = Math.Round(PatternTools.pTools.TrapezoidalIntegration(ionCluster, 3, 1), 1);
            }
            else
            {
                QuantArea = -1;
            }

        }
    }
}
