using MAligner.MAligner;
using PatternTools.FastaParser;
using PepExplorer2.DataBase;
using PepExplorer2.DeNovo;
using PepExplorer2.Result2;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PepExplorer2
{
    /// <summary>
    /// This class acts as the main engine for the hole programs, this is the start point for all analysis methods.
    /// </summary>
    public class ProgramEngine
    {

        List<DeNovoRegistry> deNovoRegistryList;

        /// <summary>
        /// Main arguments selected by the user.
        /// </summary>
        private ProgramArgs Arguments { get; set; }

        /// <summary>
        /// The result package is the output method for saving and loading results.
        /// </summary>
        public ResultPckg2 MResultPckg { get; set; }

        // <summary>
        // Dictionary represenation of th database.
        // </summary>
        //public Dictionary<string, Sequence> DataBasedictionary { get; set; }

        /// <summary>
        /// Progress Bar control.
        /// </summary>
        public double Progress { get; set; }

        public string PeptideList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProgramEngine()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="args"></param>
        public ProgramEngine(ProgramArgs args )
        {
            Progress = 0;
            Arguments = args;
            PeptideList = "";
        }

        public ResultPckg2 RunFullAnalysisMAlginer()
        {
            DateTime start = DateTime.Now;

            PrepareDeNovoRegistries();

            FastaFileParser ffp = new FastaFileParser();
            Console.WriteLine("Parsing database");
            ffp.ParseFile(new StreamReader(Arguments.DataBaseFile), false, DBTypes.IDSpaceDescription);

            Console.WriteLine("Generating byte sequences");
            ffp.IncludeByteSequences();
            
            MResultPckg = AlignMAlign(ffp.MyItems, deNovoRegistryList);

            Console.Write("Benchmarking: " + (DateTime.Now - start).TotalSeconds + " seconds");

            
            return MResultPckg;
        }


        private void PrepareDeNovoRegistries()
        {
            deNovoRegistryList = new List<DeNovoRegistry>();

            if (PeptideList == null || PeptideList.Equals(""))
            {
                if (Arguments.MPexResultPath.Length > 0) 
                {
                    Console.WriteLine("Loading " + Arguments.MPexResultPath);
                    ResultPckg2 pkg = ResultPckg2.DeserializeResultPackage(Arguments.MPexResultPath);
                    
                    deNovoRegistryList = new List<DeNovoRegistry>();
                    
                    foreach (KeyValuePair<string, List<DeNovoRegistry>> kvp in pkg.MyUnmappedRegistries) {
                        deNovoRegistryList.AddRange(kvp.Value);
                    }
                }
                else 
                {
                    deNovoRegistryList = PreprareDeNovoRegistries();
                }
                //Eliminate denovo registries not having the minimum score
                int removedForNotHavingMinDenovoScore = deNovoRegistryList.RemoveAll(a => a.DeNovoScore < Arguments.MinDeNovoScore);
                Console.WriteLine("Removed elements for not attending the minimum denovo score: " + removedForNotHavingMinDenovoScore);
            }
            else
            {
                List<string> peptides = Regex.Split(PeptideList, "\n").ToList();
                for (int i = 0; i < peptides.Count; i++)
                {
                    string cleanSequence = PatternTools.pTools.CleanPeptide(peptides[i], true);
                    cleanSequence = Regex.Replace(cleanSequence, " ", "");
                    cleanSequence = Regex.Replace(cleanSequence, "\t", "");
                    deNovoRegistryList.Add(new DeNovoRegistry(0, i, 1, cleanSequence, 1, "Provided"));
                }
            }

            //Lets make sure we are satisfying the top hit constraint
            if (Arguments.DeNovoOption == DeNovoOption.PepNovoFull || Arguments.DeNovoOption == DeNovoOption.PepNovo)
            {
                deNovoRegistryList.RemoveAll(a => a.Rank + 1 > Arguments.TopHits);
            }
            else
            {
                deNovoRegistryList.RemoveAll(a => a.Rank > Arguments.TopHits);
            }

            //Finally, lets remove registries that do not satisfy the minimum left criterion
            int removedForNotHavingMinimumLength = deNovoRegistryList.RemoveAll(a => a.CleanSequence.Length < Arguments.PeptideMinNumAA);
            Console.WriteLine("Removed for not attending the minimum number of aa : " + removedForNotHavingMinimumLength);

        }


        /// <summary>
        /// Identifies all denovo output files and list all identified peptides.
        /// </summary>
        /// <returns>A list of DeNovo Registries</returns>
        private List<DeNovoRegistry> PreprareDeNovoRegistries()
        {
            DeNovoParser deNovoParser = new DeNovoParser(Arguments.DeNovoOption, Arguments.DeNovoResultDirectory);
            deNovoParser.ScanDirectory();
            List<DeNovoRegistry> DeNovoRegistryList = deNovoParser.DeNovoRegistryList;
            return DeNovoRegistryList;
        }



        /// <summary>
        /// Multicore alignment method.  It will feed the top alignments into the DenovoRegistryObject
        /// </summary>
        /// <param name="database">A list with a parsed database</param>
        /// <param name="deNovoRegistryList">A lsit with parsed peptide registries</param>
        /// <returns>A list with aligned registries.</returns>
        private ResultPckg2 AlignMAlign(List<FastaItem> database, List<DeNovoRegistry> deNovoRegistryList)
        {

            int removedPeptides = deNovoRegistryList.RemoveAll(a => a.Sequence.Length < Arguments.PeptideMinNumAA);


            Console.WriteLine("Total denovo registries: " + deNovoRegistryList.Count);

            Dictionary<string, List<DeNovoRegistry>> dnovoRegistryDict = (from registry in deNovoRegistryList
                                                                          group registry by registry.CleanSequence into registryGroup
                                                                          select new { Seq = registryGroup.Key, Registries = registryGroup.ToList() }).ToDictionary(a => a.Seq, a => a.Registries);

            Console.WriteLine("Total denovo registries after grouping exact sequences: " + dnovoRegistryDict.Keys.Count);


            //We need to feed the alignment List          
            KeyValuePair<int[,], Dictionary<char, int>> m = MAligner.Utils.LoadSubstitutionMatrixFromString(PepExplorer2.Properties.Resources.PAM30MS);
            
            Aligner mAligner = new Aligner(m);
            int count = 0;

            ConcurrentBag<AlignmentResult> alignments = new ConcurrentBag<AlignmentResult>();
            ConcurrentBag<KeyValuePair<string, List<DeNovoRegistry>>> unmappedRegistries = new ConcurrentBag<KeyValuePair<string, List<DeNovoRegistry>>>();

            Parallel.ForEach(dnovoRegistryDict, kvp =>
            //foreach (KeyValuePair<string, List<DeNovoRegistry>> kvp in dnovoRegistryDict)
            {
                Console.WriteLine("Aligning: " + kvp.Key);
                List<DeNovoR> dRegistries = (from r in kvp.Value
                                             select new DeNovoR(r.ScanNumber, r.DeNovoScore, r.PtmSequence, r.Charge, r.FileName)).ToList();


                byte[] peptideBytes = Encoding.ASCII.GetBytes(kvp.Key);
                
                //Lets take care of palindromic hits that might spoil our result.
                List<FastaItem> validItems = database.FindAll(a => ((double)mAligner.IDScore(peptideBytes, a.SequenceInBytes) / (double)peptideBytes.Length) > Arguments.MinIdentity).ToList();
                if (validItems.Count(a => a.SequenceIdentifier.StartsWith(Arguments.DecoyLabel)) < validItems.Count)
                {
                    validItems.RemoveAll(a => a.SequenceIdentifier.StartsWith(Arguments.DecoyLabel));
                }
                //----------------

                if (validItems.Exists(a => a.SequenceIdentifier.StartsWith(Arguments.DecoyLabel)))
                {
                    Console.WriteLine("Decoy hit included");
                }

                if (validItems.Count > 0)
                {

                    //Now we need to find the best proteins
                    char[] peptide = kvp.Key.ToCharArray();

                    KeyValuePair<List<int>, List<string>> alg = mAligner.GetClosestPeptideInDB(peptide, validItems);


                    //Find the max value and only store the best IDs for this peptide
                    int bestScore = alg.Key.Max();


                    if (alg.Key.Count == validItems.Count)
                    {
                        List<string> protIDs = new List<string>();

                        for (int i = 0; i < validItems.Count; i++)
                        {
                            if (alg.Key[i] == bestScore)
                            {
                                protIDs.Add(validItems[i].SequenceIdentifier);
                            }
                        }

                        alignments.Add(new AlignmentResult(dRegistries, bestScore, protIDs));
                    }
                    else
                    {
                        Console.WriteLine("shouldn't be here, failed in peptide " + kvp.Key);
                    }

                }
                else
                {
                    unmappedRegistries.Add(kvp);
                }

                Interlocked.Increment(ref count);
                Console.WriteLine("Done aligning sequence no {0} ({1})  ", count, kvp.Key);

                //Report progress
                Progress = Math.Round((count / (double)dnovoRegistryDict.Keys.Count), 2) * 100;
            }
            );


            //Lets get only the stuff we used from the DB
            List<string> usedFastaIDs = (from b in alignments
                                        from pID in b.ProtIDs
                                        select pID).Distinct().ToList();

            List<FastaItem> usedFasta = database.FindAll(a => usedFastaIDs.Contains(a.SequenceIdentifier));

            ResultPckg2 resultPckg = new ResultPckg2(usedFasta, Arguments, alignments.ToList(), unmappedRegistries.ToList());

            return resultPckg;
        }



    }
}
