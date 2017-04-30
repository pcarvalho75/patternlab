//using CSMSL;
//using CSMSL.IO.Thermo;
//using CSMSL.Spectral;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PatternTools.XIC
//{
//    public class XICGet40
//    {
//        ThermoRawFile trf = null;
//        List<int> ms1 = null;
//        double minimumIntensity;
//        public FileInfo MyFile { get; set; }


//        public XICGet40(string fileName, double minimumIntensity = 5000)
//        {
//            Console.Write("Preparing for " + fileName);
//            trf = new ThermoRawFile(fileName);
//            trf.Open();
//            ms1 = trf.Where(a => a.MsnOrder == 1).Select(a => a.SpectrumNumber).ToList();
//            this.minimumIntensity = minimumIntensity;
//            MyFile = new FileInfo(fileName);
//            Console.WriteLine("...Ready!");
//        }

//        public List<List<double[]>> GetClusterCandidates(double mz, double ppm)
//        {
//            int counter = 0;
//            int counterIonsFound = 0;

//            double delta = (mz / 1000000) * ppm;
//            double lowerBound = mz - delta;
//            double upperBound = mz + delta;

//            List<double[]> candidateIons = new List<double[]>();

//            for (int i = 0; i < ms1.Count; i++)
//            {
//                List<MZPeak> peaks;
//                MzRange mr = new MzRange(lowerBound, upperBound);

//                if (trf[ms1[i]].MassSpectrum.TryGetPeaks(mr, out peaks))
//                {
//                    double intensity = peaks.Sum(a => a.Intensity);

//                    //if (ms1[i] == 28804)
//                    //{
//                    //    int sn = trf[ms1[i]].SpectrumNumber;
//                    //    List<MZPeak> pl;
//                    //    trf[ms1[i]].MassSpectrum.TryGetPeaks(0, 2500, out pl);

//                    //    StreamWriter sw = new StreamWriter("spec28804.txt");

//                    //    foreach (MZPeak p in pl)
//                    //    {
//                    //        sw.WriteLine(p.MZ + "\t" + p.Intensity);
//                    //    }

//                    //    sw.Close();


//                    //    Console.WriteLine("Halt");
//                    //}

//                    if (intensity >= minimumIntensity)
//                    {
//                        counterIonsFound++;
//                        candidateIons.Add(new double[] { peaks[0].MZ, intensity, ms1[i], trf.GetRetentionTime(ms1[i]), i });
//                    }
//                }

//                counter++;
//            }

//            //We will need to keep track of the scan numbers to know if a cluster ended.
//            if (candidateIons.Count > 0)
//            {
//                candidateIons.Sort((a, b) => a[4].CompareTo(b[4]));

//                List<List<double[]>> clusters = new List<List<double[]>>();
//                List<double[]> thisCluster = new List<double[]>();

//                for (int i = 0; i < candidateIons.Count; i++)
//                {
//                    if (i > 0)
//                    {
//                        //Two ions that are very close from the same spectrum, there intensity should be summed
//                        if (candidateIons[i][4] == candidateIons[i - 1][4])
//                        {
//                            thisCluster.Last()[1] += candidateIons[i][4];
//                            continue;
//                        }

//                        //Time to start a new cluster
//                        if ((candidateIons[i][4] - candidateIons[i - 1][4]) > 1)
//                        {
//                            clusters.Add(thisCluster);
//                            thisCluster = new List<double[]>() { candidateIons[i] };
//                        }
//                        else
//                        {
//                            //their diference should be 1
//                            thisCluster.Add(candidateIons[i]);
//                        }
//                    }
//                    else
//                    {
//                        //Start the first cluster
//                        thisCluster.Add(candidateIons[i]);
//                    }
//                }

//                clusters.Add(thisCluster);

//                //And now lets remove guys that might be in the grass
//                foreach (List<double[]> c in clusters)
//                {
//                    double maxSig = c.Max(a => a[1]);
//                    c.RemoveAll(a => a[1] < maxSig * 0.001);
//                }

//                clusters.RemoveAll(a => a.Count < 2);

//                return clusters;
//            }
//            else
//            {
//                return null;
//            }

//        }

//        /// <summary>
//        /// Returns the list of ions in the format [mz, intensity, scanNo, RetTime]
//        /// This is the one being used
//        /// </summary>
//        public double[,] GetXIC3(double mz, double ppm, int scanNumber)
//        {
//            //List<List<double[]>> clusters = GetClusters(mz, ppm);
//            List<List<double[]>> clusters = GetClusterCandidates(mz, ppm);

//            List<double[]> validCluster = null;

//            if (clusters != null)
//            {
//                if (clusters.Count > 0)
//                {
//                    validCluster = clusters.Find(a => a[0][2] <= scanNumber && a.Last()[2] >= scanNumber);
//                }
//            }


//            if (validCluster != null)
//            {
//                double[,] result = new double[4, validCluster.Count];

//                for (int i = 0; i < validCluster.Count; i++)
//                {
//                    result[0, i] = validCluster[i][0];
//                    result[1, i] = validCluster[i][1];
//                    result[2, i] = validCluster[i][2];
//                    result[3, i] = validCluster[i][3];
//                }

//                return result;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// Returns the list of ions in the format [mz, intensity, scanNo, RetTime]
//        /// </summary>
//        public double[,] GetXIC3(double mz, double ppm, double retentionTime)
//        {
//            List<List<double[]>> clusters = GetClusterCandidates(mz, ppm);
//            List<double[]> validCluster = null;

//            if (clusters != null)
//            {
//                validCluster = clusters.Find(a => a[0][3] <= retentionTime && a.Last()[3] >= retentionTime);
//            }

//            if (validCluster != null)
//            {
//                double[,] result = new double[4, validCluster.Count];

//                for (int i = 0; i < validCluster.Count; i++)
//                {
//                    result[0, i] = validCluster[i][0];
//                    result[1, i] = validCluster[i][1];
//                    result[2, i] = validCluster[i][2];
//                    result[3, i] = validCluster[i][3];
//                }

//                return result;
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}
