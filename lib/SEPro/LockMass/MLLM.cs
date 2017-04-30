using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.SQTParser;
using SEPRPackage;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SEProcessor.LockMass
{
    public class MLLM
    {

        //Dictionary <string, MLLMFileInterpolant> interpolantDictionary;
        ConcurrentBag<MLLMFileInterpolant> interpolants;

        public ConcurrentBag<MLLMFileInterpolant> Interpolants
        {
            get { return interpolants; }
        }


        public MLLM (ResultPackage thePackage, Parameters myParams)
        {

            //Learn
            Learn(thePackage.MyProteins.AllSQTScans, myParams);
        }

        private void Learn(List<SQTScan> scans, Parameters myParams)
        {

            //First lets group the Scans by filename
            Dictionary<string, List<SQTScan>> byFileName = (from scan in scans.AsParallel()
                                                            group scan by scan.FileName into theGroup
                                                            select new { theGroup.Key, theGroup }).ToDictionary(a => a.Key, a => a.theGroup.ToList());



            interpolants = new ConcurrentBag<MLLMFileInterpolant>();

            Parallel.ForEach (byFileName, kvp =>
            {
                Console.WriteLine("Learning lockmass function from " + kvp.Key);
                List<SQTScan> learningScans = kvp.Value;

                //Lets eliminate all labeled and unlabeled decoys
                learningScans.RemoveAll(a => a.CountNumberForwardNames(myParams.LabeledDecoyTag) == 0);
                learningScans.RemoveAll(a => a.CountNumberForwardNames(myParams.UnlabeledDecoyTag) == 0);


                //Prepare a training dataset
                List<double> x_MZ = new List<double>(learningScans.Count);
                List<double> y_ScanNumber = new List<double>(learningScans.Count);
                List<double> z_ppm = new List<double>(learningScans.Count);

                //First, lets find ppm bounds for outliers
                for (int i = 0; i < 4; i++)
                {
                    List<double> ppms = (from scan in learningScans
                                         select scan.PPM_Orbitrap).ToList();
                    double ppmAverage = ppms.Average();
                    double ppmSTD = PatternTools.pTools.Stdev(ppms, false);

                    learningScans.RemoveAll(a => a.PPM_Orbitrap > ppmAverage + (6-i) * ppmSTD || a.PPM_Orbitrap < ppmAverage - (6-i) * ppmSTD);

                }


                List<List<double>> trainMatrixTMP = new List<List<double>>(learningScans.Count);
                List<SQTScan> testScans = new List<SQTScan>(learningScans.Count);
                List<List<double>> fullMatrix = new List<List<double>>(learningScans.Count);
                int theCounter = 0;
                foreach (SQTScan scan in learningScans)
                {
                    if (Math.Abs(scan.MeasuredMH - scan.TheoreticalMH) < 0.8)
                    {
                        z_ppm.Add(scan.PPM_Orbitrap);
                        y_ScanNumber.Add(scan.ScanNumber);
                        double mz = GetMZ(scan);
                        x_MZ.Add(mz);

                        theCounter++;
                        List<double> v = new List<double> { { mz }, { scan.ScanNumber }, { scan.PPM_Orbitrap } };
                        fullMatrix.Add(v);
                        if (theCounter < 2)
                        {
                            trainMatrixTMP.Add(v);
                        }
                        else
                        {
                            theCounter = 0;
                            testScans.Add(scan);
                        }
                    }
                }

                double[,] FullMatrix = PatternTools.pTools.ConvertListofListsToDoubleArray(fullMatrix);
                double[,] TrainMatrix = PatternTools.pTools.ConvertListofListsToDoubleArray(trainMatrixTMP);



                //Tune Parameters
                int lowestNW = -1;
                int lowestNQ = -1;
                double lowestAVGRMS = double.MaxValue;
                alglib.idwinterpolant z = new alglib.idwinterpolant();

                Console.WriteLine("Optimizing");

                for (int nw = 10; nw < 55; nw += 2)
                {

                    for (int nq = 5; nq < 20; nq += 2)
                    {

                        alglib.idwbuildnoisy(TrainMatrix, TrainMatrix.GetLength(0), 2, 1, nq, nw, out z);

                        //Study the error
                        List<double> ABSErr = new List<double>(learningScans.Count);
                        foreach (SQTScan scan in testScans)
                        {
                            double[] iv = new double[2];
                            iv[0] = GetMZ(scan);
                            iv[1] = scan.ScanNumber;

                            double result = alglib.idwcalc(z, iv);
                            ABSErr.Add(Math.Abs(scan.PPM_Orbitrap - result));

                        }
                        Console.Write(".");

                        double averageError = ABSErr.Average();
                        if (averageError < lowestAVGRMS)
                        {
                            lowestAVGRMS = averageError;
                            lowestNW = nw;
                            lowestNQ = nq;
                            Console.WriteLine("(ABSErr:" + lowestAVGRMS + " NW:" + nw + " NQ:" + nq + ") ");
                        }
                    }
                }

                //Do the final training
                alglib.idwbuildnoisy(FullMatrix, z_ppm.Count, 2, 1, lowestNQ, lowestNW, out z);
                Console.WriteLine("\nAverage RMS ppm error after optimization: " + lowestAVGRMS);
                interpolants.Add(new MLLMFileInterpolant(kvp.Key, z, z_ppm));
            }
            );
        }

        private double GetMZ(SQTScan scan)
        {
            return (scan.MeasuredMH + ((double)scan.ChargeState - 1) * 1.007276466) / (double)scan.ChargeState;
        }

        public double GetPPM (string fileName, int ScanNumber, double MZ)
        {
            MLLMFileInterpolant theInterpolant = interpolants.First(a => a.MyFile.Equals(fileName));
            return theInterpolant.Interpolate(MZ, ScanNumber, true);
        }

        public void TuneScans(List<SQTScan> scans)
        {
            Console.WriteLine("Tuning Scans");

            foreach (SQTScan scan in scans)
            {
                MLLMFileInterpolant interpolant = interpolants.First(a => a.MyFile.Equals(scan.FileName));
                double estimatedPPMError = interpolant.Interpolate(GetMZ(scan), scan.ScanNumber, true);
                double correctionValue = Math.Abs((estimatedPPMError * scan.MeasuredMH) / 1000000);

                if (estimatedPPMError < 0)
                {
                    scan.MeasuredMH += correctionValue;
                }
                else
                {
                    scan.MeasuredMH -= correctionValue;
                }


            }

            Console.WriteLine("Done tunning scans");
        }
    }
}
