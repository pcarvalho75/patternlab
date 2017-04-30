using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;
using System.IO;
using SEProcessor.Result;
using PatternTools.SQTParser;
using SEPRPackage;
using System.Text.RegularExpressions;

namespace SEProcessor
{
    public static class ReportGenerator
    {


            public static void SaveReportDTASelectFiltered(string outputFile, ResultPackage resultPackage)
            {

                System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile);

                //Print the head
                PrintHead(resultPackage, sw);


                sw.WriteLine("Locus\tSequence Count\tSpectrum Count\tSequence Coverage\tLength\tMolWt\tDescriptive Name");
                sw.WriteLine("Unique\tFileName\tPrimaryScore\tDeltCN\tM+H+\tCalcM+H+\tZScore\tBayesianScore\tRedundancyAtPtnLevel\tSequence");


                //Just to make sure we list the better ones on top
                resultPackage.MyProteins.MyProteinList.Sort((a, b) => b.SequenceCount.CompareTo(a.SequenceCount));


                //Now lets begin printing
                int noGroups = resultPackage.MyProteins.MyProteinList.Max(a => a.GroupNo);
                for (int i = 1; i <= noGroups; i++)
                {
                    List<MyProtein> theProteins = resultPackage.MyProteins.MyProteinList.FindAll(a => a.GroupNo == i);

                    //Print the headers
                    List<PeptideResult> thePeptideResults = new List<PeptideResult>();
                    foreach (MyProtein result in theProteins)
                    {
                        //Print the headers
                        sw.Write(result.Locus + "\t");

                        //Sequence count
                        sw.Write(result.SequenceCount + "\t");

                        //Spectrum count
                        sw.Write(result.Scans.Count + "\t");

                        //Sequence coverage
                        sw.Write(result.Coverage * 100 + "%\t");

                        //Length
                        sw.Write(result.Length + "\t");

                        //MolWt
                        sw.Write(result.MolWt + "\t");

                        //Descriptive Name
                        sw.Write(result.Description);

                        sw.WriteLine("");
                        thePeptideResults.AddRange(result.PeptideResults);
                    }

                    //Print the peptides
                    thePeptideResults = thePeptideResults.Distinct().ToList();

                    List<string> printedScans = new List<string>();
                    foreach (PeptideResult pepResult in thePeptideResults)
                    {
                        //Extract peptide info
                        foreach (SQTScan s in pepResult.MyScans)
                        {
                            if (printedScans.Contains(s.FileNameWithScanNumberAndChargeState)) { continue; }
                            printedScans.Add(s.FileNameWithScanNumberAndChargeState);
                            PrintScan(sw, s);

                            //Write the end of the line
                            sw.WriteLine("");
                        }
                    }

                }

                //Write the final summary
                sw.WriteLine();
                sw.WriteLine("Spectra FDR: " + resultPackage.MyFDRResult.SpectraFDRLabel);
                sw.WriteLine("Peptide FDR: " + resultPackage.MyFDRResult.PeptideFDRLabel);
                sw.WriteLine("Protein FDR: " + resultPackage.MyFDRResult.ProteinFDRLabel);
                sw.WriteLine("No Potein Groups: " + noGroups);

                sw.Close();
            }

            public static void SaveProteinTable(string outputFile, ResultPackage resultPackage)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile);

                PrintHead(resultPackage, sw);

                resultPackage.MyProteins.MyProteinList.Sort((a,b) => a.GroupNo.CompareTo(b.GroupNo));
                sw.WriteLine("#Identified Proteins :{0}, Identified Peptides :{1}", resultPackage.MyProteins.MyProteinList.Count, resultPackage.MyProteins.MyPeptideList.Count);
                sw.WriteLine("#Locus\tGroup\tLength\tMolWt (MH)\tSequenceCount\tSpectrumCount\tCoverage\tProteinScore (i.e., sum of XCorr)\tDescription");
                sw.WriteLine("#\tPeptideSequence\tSpecCount\tRedundancy\tMaximumPrimaryScore\tFileName.ScanNumber.ChargeState-At Maximum Primary Score");
                foreach (MyProtein p in resultPackage.MyProteins.MyProteinList)
                {
                    sw.WriteLine(p.Locus + "\t" + p.GroupNo + "\t" + p.Sequence.Length + "\t" + p.MolWt + "\t" + p.SequenceCount + "\t" + p.SpectrumCount + "\t" + Math.Round(p.Coverage,2) + "\t" + Math.Round(p.ProteinScore,3) + "\t" + p.Description);

                    foreach (PeptideResult pep in p.PeptideResults)
                    {
                        pep.MyScans.Sort((a, b) => b.PrimaryScore.CompareTo(a.PrimaryScore));
                        sw.WriteLine("\t" + pep.PeptideSequence + "\t" + pep.NoMyScans + "\t" + pep.MyMapableProteins.Count + "\t" + pep.MyScans[0].PrimaryScore + "\t" + pep.MyScans[0].FileName + "." + pep.MyScans[0].ScanNumber + "." + pep.MyScans[0].ChargeState);
                    }
                }

                sw.Close();

            }

            public static void SaveReportByFileName(string fileName, ResultPackage resultPackage)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName);

                //Print the head
                PrintHead(resultPackage, sw);
                sw.WriteLine("#Scan Line: Unique\tFileName\tPrimaryScore\tDeltCN\tM+H+\tCalcM+H+\tZScore\tBayesianScore\tRedundancyAtPtnLevel\tSequence");

                //Obtain a list of unique file Names
                List<string> fileNames = (from peptide in resultPackage.MyProteins.MyPeptideList
                                          from scan in peptide.MyScans
                                          select scan.FileName).Distinct().ToList();



                fileNames.Sort();

                foreach (var fname in fileNames)
                {
                    sw.WriteLine("\nFileName: " + fname);

                    List<MyProtein> myProtein = (from protein in resultPackage.MyProteins.MyProteinList
                                                 from scan in protein.Scans
                                                 where scan.FileName.Equals(fname)
                                                 select protein).Distinct().ToList();
                    
                    foreach (MyProtein prot in myProtein)
                    {
                        sw.WriteLine("\tLocus:" + prot.Locus + "\tGroup No: " + prot.GroupNo + "(" + prot.MyGroupType + ")\t" + prot.Description);

                        List<SQTScan> scans = (from scan in prot.Scans
                                               where scan.FileName.Equals(fname)
                                               select scan).Distinct().ToList();

                        foreach (SQTScan scan in scans)
                        {
                            sw.Write("\t\t");
                            PrintScan(sw, scan);
                            sw.WriteLine("");
                        }
                    }

                    Console.WriteLine("\n");
                }

                sw.Close();

            }

            private static void PrintHead(ResultPackage resultPackage, System.IO.StreamWriter sw)
            {
                string versionNo = "NT";

                try
                {
                    string[] stuff = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");
                    versionNo = stuff[1];
                }
                catch { }
                sw.WriteLine("Search Engine Processor v" + versionNo);
                sw.WriteLine("Input directory: {0}", resultPackage.SQTDirectory);
                sw.WriteLine("Database searched: {0}", resultPackage.Database);
                sw.WriteLine("");

                sw.WriteLine("{0}\tComposite score considers primary score", resultPackage.MyParameters.CompositeScorePrimaryScore);
                sw.WriteLine("{0}\tComposite score considers secondary score", resultPackage.MyParameters.CompositeScoreSecondaryScore);
                sw.WriteLine("{0}\tComposite score considers deltaCN", resultPackage.MyParameters.CompositeScoreDeltaCN);
                sw.WriteLine("{0}\tComposite score considers peaks matched", resultPackage.MyParameters.CompositeScorePeaksMatched);
                sw.WriteLine("{0}\tCompostie score considers deltaMass", resultPackage.MyParameters.CompositeScoreDeltaMassPPM);
                sw.WriteLine("{0}\tSpectra max FDR", resultPackage.MyParameters.SpectraFDR);
                sw.WriteLine("{0}\tPeptide max FDR", resultPackage.MyParameters.PeptideFDR);
                sw.WriteLine("{0}\tProtein max FDR", resultPackage.MyParameters.ProteinFDR);
                sw.WriteLine("{0}\tQualityFilterMinSequenceLength", resultPackage.MyParameters.QFilterMinSequenceLength);

                string qdDeltaPPM = "False";
                if (resultPackage.MyParameters.QFilterDeltaMassPPM)
                {
                    qdDeltaPPM = resultPackage.MyParameters.QFilterDeltaMassPPMValue.ToString();
                }

                sw.WriteLine("{0}\tQualityFilterDeltaMassPPM", qdDeltaPPM);

                double primScore = 0;
                if (resultPackage.MyParameters.QFilterPrimaryScore)
                {
                    primScore = resultPackage.MyParameters.QFilterPrimaryScoreValue;
                }
                sw.WriteLine("{0}\tQualityFilterPrimaryScore", primScore);

                double secScore = 0;
                if (resultPackage.MyParameters.QFilterSecondaryScore)
                {
                    secScore = resultPackage.MyParameters.QFilterSecondaryScoreValue;
                }

                sw.WriteLine("{0}\tQualityFilterSecondaryScore", secScore);
                sw.WriteLine("{0}\tQualityFilterDiscardNonTryptic", resultPackage.MyParameters.QFilterMinNoEnzymaticTermini);
                sw.WriteLine("{0}\tQualityFilterIncludeOnlyLociWithUniquePeptide", resultPackage.MyParameters.QFilterDiscardProteinsWithNoUniquePeptides);
                sw.WriteLine("{0}\tQualityFilterIncludeOnlyLociWithTwoOrMorePeptides", resultPackage.MyParameters.QFilterProteinsMinNoPeptides);
                sw.WriteLine("");
            }

            private static void PrintScan(System.IO.StreamWriter sw, SQTScan s)
            {
                //Print Is unique
                if (s.IsUnique)
                {
                    sw.Write("*\t");
                }
                else
                {
                    sw.Write("\t");
                }

                //Print the filename
                sw.Write(s.FileNameWithScanNumberAndChargeState + "\t");

                //Print Primary Score
                sw.Write(s.PrimaryScore + "\t");

                //Print DeltaCN
                sw.Write(s.DeltaCN + "\t");

                //M+H+
                sw.Write(s.MeasuredMH + "\t");

                //CalcM+H+
                sw.Write(s.TheoreticalMH + "\t");

                //ZScore
                sw.Write(s.ProbabilityValue + "\t");

                //BayesianScore
                sw.Write(s.BayesianScore + "\t");

                //Redundancy
                sw.Write(s.IdentificationNames.Count + "\t");

                //Sequence
                sw.Write(s.PeptideSequence);
            }
    }
}
