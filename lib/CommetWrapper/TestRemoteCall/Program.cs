using CometWrapper;
using PatternTools.FastaParser;
using PatternTools.SQTParser2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRemoteCall
{
    class Program
    {
        static void Main(string[] args)
        {
            int minNoPeptides = 2;
            double minAcceptableXCorr = 1.7;
            string sequenceDB = @"C:\Users\pcarvalho\Desktop\XIC\BSA\PFUsmall.T-R";
            string massSpectraFile = @"C:\Users\pcarvalho\Desktop\XIC\BSA\1\20111024_BSA_40fmol_01.RAW";


            CometWrapper.cometParams cp = new CometWrapper.cometParams();

            cp.ClearMZRangeMax = 0;
            cp.ClearMZRangeMin = 0;

            cp.Enzyme = 1; //Trypsin
            cp.EnzymeSpecificity = 2;
            cp.FragmentBinOffset = 0.4;
            cp.FragmentBinTolerance = 1.0005;
            cp.IonsA = false;
            cp.IonsB = true;
            cp.IonsC = false;
            cp.IonsNL = true;
            cp.IonsX = false;
            cp.IonsY = true;
            cp.IonsZ = false;

            cp.MaxVariableModsPerPeptide = 3;
            cp.MissedCleavages = 4;
            
            
            Modification m1 = new Modification("Carb", (decimal)57.02146, "C");

            //Modification m2 = new Modification("M", (decimal)15.9949, "M");
            //m2.isDiff = true;

            //For cross-linker dead end, include as a variable mod, and then as a variable mod for n-terminal

            cp.MyModificationItems = new List<CometWrapper.Modification>() {m1};

            cp.PrecursorMassTolerance = 40;
            cp.SearchMassRangeMax = 5500;
            cp.SearchMassRangeMin = 550;
            cp.SequenceDatabase = sequenceDB;
            cp.TheoreticalFragIons = 1;

            List<SQTScan2> myResults = CometWrapper.RemoteCall.CallComet(cp, massSpectraFile);

            
            
            int removedPrimary = myResults.RemoveAll(a => a.Matches[0].PrimaryScore < minAcceptableXCorr);

            var proteinIDs = (from result in myResults
                              from ID id in result.Matches[0].IDs
                              group id by id.Locus into myLocusGroups
                              select new {ID = myLocusGroups.Key, Count = myLocusGroups.Count()});

            List<string> peptides = (from result in myResults
                                     select result.Matches[0].PeptideSequence).Distinct().ToList();

            PatternTools.FastaParser.FastaFileParser ffp = new PatternTools.FastaParser.FastaFileParser();
            ffp.ParseFile(new StreamReader(cp.SequenceDatabase), true, PatternTools.FastaParser.DBTypes.IDSpaceDescription);

            List<FastaItem> myFasta = new List<FastaItem>();

            foreach (var prot in proteinIDs)
            {
                if (prot.Count >= minNoPeptides)
                {
                    myFasta.Add(ffp.MyItems.Find(a => a.SequenceIdentifier.Equals(prot.ID)));
                }

            }

            Console.WriteLine("Done");

        }
    }
}
