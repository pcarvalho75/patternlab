using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using PatternTools;
using System.IO;
using System.Collections.Concurrent;
using PatternTools.SQTParser;
using PatternTools.MSParser;
using PatternTools.MSParserLight;
using SEPRPackage;
using PatternTools.PTMMods;

namespace SEProcessor
{
    static class Processor
    {

        public static void ApplyQualityFilters(Parameters myParams, List<SQTScan> theScans)
        {
            Regex trypticTermini = new Regex("[-|K|R]" + Regex.Escape(".") + "[^P]", RegexOptions.Compiled);


            int numberOfElementsRemoved = 0;

            if (theScans != null)
            {

                theScans.RemoveAll(a => a.IdentificationNames == null);
                    
                //Minimum Sequence size
                numberOfElementsRemoved += theScans.RemoveAll(a => a.PeptideSequenceCleaned.Length < myParams.QFilterMinSequenceLength);

                int deltaCNRemoved = 0;
                if (myParams.QFilterDeltaCN)
                {
                    deltaCNRemoved += theScans.RemoveAll(a => a.DeltaCN < myParams.QFilterDeltaCNMin);
                    Console.WriteLine("Removing {0} spectra with deltaCN less than {1}", deltaCNRemoved, myParams.QFilterDeltaCNMin);
                }

                int chargeOneRemoved = 0;

                if (myParams.QFilterDiscardChargeOneMS)
                {
                    chargeOneRemoved = theScans.RemoveAll(a => a.ChargeState == 1);
                    Console.WriteLine("Removed {0} spectra with charge state 1", chargeOneRemoved);
                }

                numberOfElementsRemoved += deltaCNRemoved + chargeOneRemoved;

                //Apply some filters
                if (myParams.QFilterPrimaryScore)
                {
                    int i = theScans.RemoveAll(a => a.PrimaryScore <= myParams.QFilterPrimaryScoreValue);
                    numberOfElementsRemoved += i;
                    //Console.WriteLine(" Primary Score Filter removed: " + i);
                }

                if (myParams.QFilterSecondaryScore)
                {
                    int i = theScans.RemoveAll(a => a.SecondaryScore <= myParams.QFilterSecondaryScoreValue);
                    numberOfElementsRemoved += i;
                    //Console.WriteLine(" Primary Score Filter removed: " + i);
                }

                if (myParams.QFilterMinNoEnzymaticTermini > 0)
                {
                    MatchCollection mc;

                    int i = theScans.RemoveAll(a => (mc = trypticTermini.Matches(a.PeptideSequence)).Count < myParams.QFilterMinNoEnzymaticTermini);

                    numberOfElementsRemoved += i;
                    Console.WriteLine(" Removing peptides with less than {0} enzimatic termini... {1} removed.", myParams.QFilterMinNoEnzymaticTermini, i);
                }

                if (myParams.QFilterDeltaMassPPM)
                {
                    int i = theScans.RemoveAll(a => Math.Abs(a.PPM_Orbitrap) > myParams.QFilterDeltaMassPPMValue);
                    numberOfElementsRemoved += i;
                    Console.WriteLine(" DeltaMass Filter removed: " + i);
                }

                    Console.WriteLine("Total Scans remaining: " + theScans.Count + " elements (" + numberOfElementsRemoved + " removed).");
                }

            Console.WriteLine(".");
        }


        public static List<SQTScan> ParseSQTsToScans(string[] files, Parameters myParams)
        {
            ConcurrentBag<SQTScan> pathBag = new ConcurrentBag<SQTScan>();

            Parallel.ForEach(files, file => 
            //foreach (string file in files)
            {
                Console.WriteLine("Parsing: " + file);
                SQTParser parser = new SQTParser(file);
                
                string fileName = System.IO.Path.GetFileName(file);

                foreach (SQTScan s in parser.Scans)
                {
                    if (s.ChargeState == 0 || s.PeptideSequence == null) { continue; }
                    //Break up the names
                    List<string> newNames = new List<string>(s.IdentificationNames.Count);
                    s.FileName = fileName;
                    pathBag.Add(s);
                } 
            }
            );

            Console.WriteLine("Done SQT->Path\n");

            return pathBag.ToList();
        }     


        //---------------


        /// <summary>
        /// This method will generate a cutoff at the spectral level or at the peptide level.
        /// </summary>
        /// <param name="aStruct"></param>
        /// <param name="myParams"></param>
        /// <param name="scans"></param>
        /// <returns></returns>
        public static int FindCutoffSpectralLevel(PeptideAnalysisPckg aStruct, Parameters myParams, List<SQTScan> scans)
        {
            
            int cutoffValue = -1;

            //Algorithm for finding at spectral level
            double noOfLabeledDecoy = aStruct.NoBadSpectra;
            for (int i = scans.Count - 1; i >= 0; i--)
            {
                if (scans[i].CountNumberForwardNames(myParams.LabeledDecoyTag) == 0)
                {
                    noOfLabeledDecoy--;

                    double FDR = noOfLabeledDecoy / ((double)i);

                    if (FDR <= myParams.SpectraFDR)
                    {
                        cutoffValue = i;
                        break;
                    }
                }

            }
            

            return (cutoffValue);
        }



        internal static int EliminateDecoysPSMSequencesThatMatchTargets(List<SQTScan> allScans, Parameters myParams)
        {
            Console.WriteLine("Cleaning decoy PSMs that share sequence with target PSM sequences");
            List<string> goodPeptides = (from s in allScans
                                         where s.CountNumberForwardNames(myParams.LabeledDecoyTag) > 0
                                         select s.PeptideSequenceCleaned).ToList();
                
            List<SQTScan> badScans = allScans.FindAll(a => a.CountNumberForwardNames(myParams.LabeledDecoyTag) == 0);

            List<SQTScan> toEliminate = (from s in badScans.AsParallel()
                                         where goodPeptides.Contains(s.PeptideSequenceCleaned)
                                         select s).ToList();

            allScans = allScans.Except(toEliminate).ToList();


            return (toEliminate.Count);
                             
        }
    }
}
