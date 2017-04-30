using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;
using System.IO;
using PatternTools.SQTParser;
using SEPRPackage;
using System.Text.RegularExpressions;

namespace SEPRPackage
{
    public class PeptideAnalysisPckg
    {
        public List<SQTScan> GoodScans { get; set; }
        public List<SQTScan> BadScans { get; set; }
        public double NoGoodSpectra { get { return (double)GoodScans.Count; } }
        public double NoBadSpectra { get { return (double)BadScans.Count; } }
        public double NoGoodPeptides { get { return (double)MyGoodPeptideDic.Keys.Count; } }
        public double NoBadPeptides { get { return (double)MyBadPeptideDic.Keys.Count; } }
        public Dictionary<string, double> MyGoodPeptideDic { get; set; }
        public Dictionary<string, double> MyBadPeptideDic { get; set; }

        public List<SQTScan> AllScans
        {
            get { return GoodScans.Concat(BadScans).Distinct().ToList(); }
        }

        public List<string> CleanedPeptides
        {
            get
            {
                List<string> thePeptides = (from s in AllScans
                                            select PatternTools.pTools.CleanPeptide(s.PeptideSequenceCleaned, true)).Distinct().ToList();

                return thePeptides;
            }
        }


        public PeptideAnalysisPckg(List<SQTScan> myScans, Parameters myParams)
        {


            //Make sure there are no illegal characters in name
            Regex cleanName1 = new Regex("\n", RegexOptions.Compiled);
            Regex cleanName2 = new Regex("\r", RegexOptions.Compiled);

            foreach (SQTScan scan in myScans)
            {
                for (int i = 0; i < scan.IdentificationNames.Count; i++)
                {
                    scan.IdentificationNames[i] = cleanName1.Replace(scan.IdentificationNames[i], string.Empty);
                    scan.IdentificationNames[i] = cleanName2.Replace(scan.IdentificationNames[i], string.Empty);
                }

            }


            myScans.ForEach(a => a.PPM_Orbitrap = Math.Round(a.PPM_Orbitrap, 5));
            myScans.ForEach(a => a.BayesianScore = Math.Round(a.BayesianScore, 5));
            myScans.ForEach(a => a.PrimaryScore = Math.Round(a.PrimaryScore, 5));

            GoodScans = myScans.FindAll(a => a.CountNumberForwardNames(myParams.LabeledDecoyTag) > 0);
            BadScans = myScans.FindAll(a => a.CountNumberForwardNames(myParams.LabeledDecoyTag) == 0);



            var goodPeptideDict = (from s in GoodScans.AsParallel()
                                   group s by s.PeptideSequenceCleaned into peptideSequence
                                   select new { CleanedPeptideSequence = peptideSequence.Key, Scans = peptideSequence });

            var badPeptideDict = (from s in BadScans.AsParallel()
                                  group s by s.PeptideSequenceCleaned into peptideSequence
                                  select new { CleanedPeptideSequence = peptideSequence.Key, Scans = peptideSequence });



            MyGoodPeptideDic = goodPeptideDict.ToDictionary(a => a.CleanedPeptideSequence, a => (double)a.Scans.Count());
            MyBadPeptideDic = badPeptideDict.ToDictionary(a => a.CleanedPeptideSequence, a => (double)a.Scans.Count());

        }





        public double TotalSpectra
        {
            get
            {
                return NoGoodSpectra + NoBadSpectra;
            }
        }

        public double TotalPeptides
        {
            get
            {
                return NoGoodPeptides + NoBadPeptides;
            }
        }



        public void PrintStatistics(string statisticsStitle)
        {
            Console.WriteLine("\n**" + statisticsStitle);
            Console.WriteLine("No Target spectra: " + NoGoodSpectra);
            Console.WriteLine("No Labeled Decoy spectra: " + NoBadSpectra);
            Console.WriteLine("No Target peptides: " + NoGoodPeptides);
            Console.WriteLine("No Labeled Decoy peptides: " + NoBadPeptides);
            Console.WriteLine("----------------------\n");
        }

        /// <summary>
        /// Used for debugging among other purposes
        /// </summary>
        /// <param name="fileName"></param>
        public void PrintGoodPeptideListToFile(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine("estimated no keys: " + MyGoodPeptideDic.Keys.Count);
            List<string> myKeys = MyGoodPeptideDic.Keys.ToList();
            myKeys.Sort();
            
            foreach (string k in myKeys)
            {
                sw.WriteLine(k);
            }
            sw.Close();

        }


        public string SpectraFDR
        {
            get
            {
                string report = NoBadSpectra + " / " + TotalSpectra + " = " + (NoBadSpectra / TotalSpectra).ToString("N3");
                return (report);
            }
        }

        public string PeptideFDR
        {
            get
            {
                string report = NoBadPeptides + " / " + TotalPeptides + " = " + (NoBadPeptides / TotalPeptides).ToString("N3");
                return (report);
            }
        }


    }
}
