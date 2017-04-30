using PatternTools.MSParserLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools.XIC
{
    public class XICGet5
    {
        List<MSUltraLight> TheMS1;
        double minimumIntensity;
        public FileInfo MyFile { get; set; }


        public XICGet5(string fileName, double minimumIntensity = 5000)
        {
            Console.Write("Preparing for " + fileName);
            ParserUltraLightRAW parser = new ParserUltraLightRAW();
            TheMS1 = parser.ParseFile(fileName, -1, 1, null);
            this.minimumIntensity = minimumIntensity;
            MyFile = new FileInfo(fileName);
            Console.WriteLine("...Ready!");
        }

        public List<List<double[]>> GetClusterCandidates(double mz, double ppm)
        {
            int counterIonsFound = 0;

            double delta = (mz / 1000000) * ppm;
            double lowerBound = mz - delta;
            double upperBound = mz + delta;

            List<double[]> candidateIons = new List<double[]>();

            int ms1Counter = 0;
            foreach (MSUltraLight ms1 in TheMS1)
            {
                ms1Counter++;


                List<Tuple<float,float>> pks = ms1.Ions.FindAll(a => a.Item1 >= lowerBound && a.Item1 <= upperBound);
                double intensity = pks.Sum(a => a.Item2);

                //if (ms1.ScanNumber == 28804)
                //{
                //    Console.WriteLine("Halt");
                //    StreamWriter sw = new StreamWriter("OEspectro" + Math.Round(mz, 0) + ".txt");
                //    foreach (var p in ms1.Ions)
                //    {
                //        sw.WriteLine(p.Item1 + "\t" + p.Item2);
                //    }

                //    sw.Close();
                //}

                if (intensity >= minimumIntensity)
                {
                    counterIonsFound++;
                    candidateIons.Add(new double[] { mz, intensity, ms1.ScanNumber, ms1.CromatographyRetentionTime, ms1Counter });
                }
            }

            //We will need to keep track of the scan numbers to know if a cluster ended.
            if (candidateIons.Count > 0)
            {
                //candidateIons.Sort((a, b) => a[4].CompareTo(b[4]));

                List<List<double[]>> clusters = new List<List<double[]>>();
                List<double[]> thisCluster = new List<double[]>();

                for (int i = 0; i < candidateIons.Count; i++)
                {
                    if (i > 0)
                    {
                        //Two ions that are very close from the same spectrum, there intensity should be summed
                        if (candidateIons[i][4] == candidateIons[i - 1][4])
                        {
                            thisCluster.Last()[1] += candidateIons[i][4];
                            continue;
                        }

                        //Time to start a new cluster
                        if ((candidateIons[i][4] - candidateIons[i - 1][4]) > 1)
                        {
                            clusters.Add(thisCluster);
                            thisCluster = new List<double[]>() { candidateIons[i] };
                        }
                        else
                        {
                            //their diference should be 1
                            thisCluster.Add(candidateIons[i]);
                        }
                    }
                    else
                    {
                        //Start the first cluster
                        thisCluster.Add(candidateIons[i]);
                    }
                }

                clusters.Add(thisCluster);

                //And now lets remove guys that might be in the grass
                foreach (List<double[]> c in clusters)
                {
                    double maxSig = c.Max(a => a[1]);
                    c.RemoveAll(a => a[1] < maxSig * 0.001);
                }

                clusters.RemoveAll(a => a.Count < 2);

                return clusters;
            }
            else
            {
                return null;
            }

        }

        

        /// <summary>
        /// Returns the list of ions in the format [mz, intensity, scanNo, RetTime]
        /// This is the one being used
        /// </summary>
        public double[,] GetXIC3(double mz, double ppm, int scanNumber)
        {
            //List<List<double[]>> clusters = GetClusters(mz, ppm);
            List<List<double[]>> clusters = GetClusterCandidates(mz, ppm);

            List<double[]> validCluster = null;

            if (clusters != null)
            {
                if (clusters.Count > 0)
                {
                    validCluster = clusters.Find(a => a[0][2] <= scanNumber && a.Last()[2] >= scanNumber);
                }
            }


            if (validCluster != null)
            {
                double[,] result = new double[4, validCluster.Count];

                for (int i = 0; i < validCluster.Count; i++)
                {
                    result[0, i] = validCluster[i][0];
                    result[1, i] = validCluster[i][1];
                    result[2, i] = validCluster[i][2];
                    result[3, i] = validCluster[i][3];
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the list of ions in the format [mz, intensity, scanNo, RetTime]
        /// </summary>
        public double[,] GetXIC3(double mz, double ppm, double retentionTime)
        {
            List<List<double[]>> clusters = GetClusterCandidates(mz, ppm);
            List<double[]> validCluster = null;

            if (clusters != null)
            {
                validCluster = clusters.Find(a => a[0][3] <= retentionTime && a.Last()[3] >= retentionTime);
            }

            if (validCluster != null)
            {
                double[,] result = new double[4, validCluster.Count];

                for (int i = 0; i < validCluster.Count; i++)
                {
                    result[0, i] = validCluster[i][0];
                    result[1, i] = validCluster[i][1];
                    result[2, i] = validCluster[i][2];
                    result[3, i] = validCluster[i][3];
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
