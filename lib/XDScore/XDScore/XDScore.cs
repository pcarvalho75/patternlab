using Accord.MachineLearning;
using Accord.Statistics.Distributions.Multivariate;
using PatternTools.SQTParser2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XDScore
{
    public class XDScore
    {
        public List<double> DeltaScores { get; set; }
        List<SQTScan2> scans;
        bool Log;

        public GaussianMixtureModel gmm { get; set; }
        public double GaussianIntersect { get; set; }
        public double CumulativeIntersect { get; set; }
        public MultivariateNormalDistribution GaussianSmallMean {get; set;}
        public MultivariateNormalDistribution GaussianBigMean {get; set;}
        public double ProportionSmallGaussian {get; set;}
        public double ProportionBigGaussian { get; set; }


        public double RetrievePValueForScanNumber(int scanNo, double xdPrime)
        {

            SQTScan2 thisSQT = scans.Find(a => a.ScanNumber == scanNo);

            if (thisSQT == null)
            {
                return -1;
            }
            else
            {
                double xdscore = GetDeltaScore(thisSQT, false);
                return ProbabilityGaussianIntersect(xdPrime, xdscore);
            }

        }

        public XDScore(List<string> SQTFiles, bool log)
        {
            Log = log;
            scans = Parse(SQTFiles);

            int totalScans = scans.Count;
            
            //Now select only those scans of which the first peptide is a phosphp
            int scansRemoved = scans.RemoveAll(a => a.Matches.Count == 0);

            SQTScan2 theS = scans.Find(a => a.ScanNumber == 14202);
            
            scansRemoved += scans.RemoveAll(a => !a.Matches[0].PeptideSequence.Contains("79"));

            Console.WriteLine("Scans removed for not being fosfo peptides = " + scansRemoved);


            int scansRemovedDelta0 = scans.RemoveAll(a => GetDeltaScore(a, true) == double.NegativeInfinity);
            Console.WriteLine("Scans removed for Delta 0  or not having the isobaric counterpart = " + scansRemovedDelta0);

            Console.WriteLine("Scans removed = " + (scansRemoved + scansRemovedDelta0) + "/" + totalScans);


        }

        public void CalculateDeltaScores(int z, int ps, bool training)
        {
            // Parse the files

            if (z == 0)
            {
                DeltaScores = GetDeltaScores(scans.FindAll(a => a.ChargeState > 1), ps, training);
            }
            else
            {
                DeltaScores = GetDeltaScores(scans.FindAll(a => a.ChargeState == z), ps, training);
            }

            gmm = null;
        }

        public void GenerateGMM()
        {
            double[][] mixture = new double[DeltaScores.Count][];
            for (int i = 0; i < DeltaScores.Count; i++)
            {
                mixture[i] = new double[] { DeltaScores[i] };
            }

            gmm = new GaussianMixtureModel(2);

            // If available, initialize with k-means
            //if (kmeans != null) gmm.Initialize(kmeans);

            // Compute the model
            GaussianMixtureModelOptions gmmo = new GaussianMixtureModelOptions();
            gmmo.Logarithm = true;
            gmm.Compute(mixture, 0.000000000000000000000001);



            //Find out who is the big median gaussian and small media gaussian
            double[] meanTmp1 = gmm.Gaussians[0].Mean;
            double[] meanTmp2 = gmm.Gaussians[1].Mean;

            ProportionSmallGaussian = -1;
            ProportionBigGaussian = -1;

            if (meanTmp1[0] < meanTmp2[0])
            {
                GaussianSmallMean = gmm.Gaussians[0].ToDistribution();
                ProportionSmallGaussian = gmm.Gaussians[0].Proportion;

                GaussianBigMean = gmm.Gaussians[1].ToDistribution();
                ProportionBigGaussian = gmm.Gaussians[1].Proportion;


            }
            else
            {
                GaussianSmallMean = gmm.Gaussians[1].ToDistribution();
                ProportionSmallGaussian = gmm.Gaussians[1].Proportion;

                GaussianBigMean = gmm.Gaussians[0].ToDistribution();
                ProportionBigGaussian = gmm.Gaussians[0].Proportion;
            }


            //And now find GMM intersect and the cumulative intersect
            double min = DeltaScores.Min();
            double max = DeltaScores.Max();


            List<double> smallMeanValues = new List<double>();
            List<double> largeMeanValues = new List<double>();

            //Determine intersect of the two Gaussians
            double GMintersect = double.PositiveInfinity;
            double GMdistance2 = double.PositiveInfinity;

            double Cintersect = double.PositiveInfinity;
            double Cdistance2 = double.PositiveInfinity;
            for (double i = GaussianSmallMean.Median[0]; i <= GaussianBigMean.Median[0]; i += 0.02)
            {
                //Gaussian Intersect
                //double GMredG = GaussianSmallMean.ProbabilityDensityFunction(i) * ProportionSmallGaussian;
                //double GMblueG = GaussianBigMean.ProbabilityDensityFunction(i) * ProportionBigGaussian;

                double GMredG = GaussianSmallMean.ProbabilityDensityFunction(i);
                double GMblueG = GaussianBigMean.ProbabilityDensityFunction(i);

                double GMdistanceThis = Math.Abs(GMredG - GMblueG);

                if (GMdistanceThis < Math.Abs(GMdistance2))
                {
                    GMdistance2 = GMdistanceThis;
                    GMintersect = i;
                }

                //Cumulative Intersect
                //double CredG = GaussianSmallMean.ComplementaryDistributionFunction(i) * ProportionSmallGaussian;
                //double CblueG = GaussianBigMean.DistributionFunction(i) * ProportionBigGaussian;

                double CredG = GaussianSmallMean.ComplementaryDistributionFunction(i);
                double CblueG = GaussianBigMean.DistributionFunction(i);

                double CdistanceThis = Math.Abs(CredG - CblueG);

                if (CdistanceThis < Math.Abs(Cdistance2))
                {
                    Cdistance2 = CdistanceThis;
                    Cintersect = i;
                }

            }

            Console.WriteLine("Gaussian Meeting point : " + GMintersect);
            Console.WriteLine("Cumulative Meeting point : " + Cintersect);

            GaussianIntersect = GMintersect;
            CumulativeIntersect = Cintersect;
            
        }

        public double ProbabilityGaussianIntersect(double xdPrime, double xdscore)
        {
            double scoretmp = Math.Log((1 - 0.01) / 0.01) / (xdPrime - GaussianIntersect);
            double score = 1 / (1 + Math.Exp(scoretmp * (xdscore - GaussianIntersect)));
            return score;
        }

        public double ProbabilityCumulativeIntersect(double xdPrime, double xdscore)
        {
            double xdPrimeConsidered = xdPrime;

            if (Log)
            {
                xdPrimeConsidered = Math.Log(xdPrimeConsidered);
            }

            double Theta = Math.Log((1 - 0.01) / 0.01) / (xdPrimeConsidered - CumulativeIntersect);
            double score = 1 / (1 + Math.Exp(Theta * (xdscore - CumulativeIntersect)));
            return score;
        }



        private List<double> GetDeltaScores(List<SQTScan2> myScans, int noPhosphoSites, bool training)
        {
            List<double> deltaScores = new List<double>();

            List<SQTScan2> theScans = myScans;

            if (noPhosphoSites == 0)
            {

                //Calculate an XDScore for each mass spectrum
                deltaScores = myScans.Select(a => GetDeltaScore(a, training)).ToList();
              
            }
            else
            {
                //Calculate an XDScore for each mass spectrum
                Regex seventyNine = new Regex("79");
                List<SQTScan2> tmpScans = myScans.FindAll(a => seventyNine.Matches(a.Matches[0].PeptideSequence).Count == noPhosphoSites);
                deltaScores = tmpScans.Select(a => GetDeltaScore(a, training)).ToList();
                
            }

            deltaScores.RemoveAll(a => a < -1000000);
            return deltaScores;
            
        }

        public double GetDeltaScore(SQTScan2 scn, bool training)
        {
            double primaryScoreFirstMatch = scn.Matches[0].PrimaryScore;
            double deltaScore = -1;


            //Now find the same peptide with a different fosfosite asignment
            //Not cleaning appropriately

            string pepSeq = PatternTools.pTools.CleanPeptide(scn.Matches[0].IDs[0].PeptideSequence, true);
            

            List<PatternTools.SQTParser2.Match> matches = scn.Matches.FindAll(a => PatternTools.pTools.CleanPeptide(a.IDs[0].PeptideSequence, true).Equals(pepSeq) && a.PrimaryRank > 1);

            double second = -1;

            if (matches.Count > 0)
            {
                second = matches[0].PrimaryScore;
            }
            else
            {
                //Lets get the greatest delta
                if (training)
                {
                    return double.NegativeInfinity;
                }
                else
                {
                    scn.Matches.Sort((a, b) => a.PrimaryRank.CompareTo(b.PrimaryRank));
                    second = scn.Matches.Last().PrimaryScore;
                }

            }

            if (primaryScoreFirstMatch - second == 0)
            {
                return double.NegativeInfinity;
            }

            if (Log)
            {
                deltaScore = Math.Log(primaryScoreFirstMatch - second);
            }
            else
            {
                deltaScore = (primaryScoreFirstMatch - second);
            }

            return deltaScore;
        }



        private static List<SQTScan2> Parse(List<string> SQTFiles)
        {
            List<SQTScan2> scans = new List<SQTScan2>();

            //Parse the files
            foreach (string file in SQTFiles)
            {
                Console.WriteLine("Parsing file " + file);
                SQTParser2 sqtParser2 = new SQTParser2();
                scans.AddRange(sqtParser2.Parse(file, 50));
                Console.Write(".");
            }

            return scans;
        }
    }
}
