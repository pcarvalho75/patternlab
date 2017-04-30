using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using PatternTools.T_ReXS.RegexGenerationTools;
using PatternTools.TReXS.RegexGenerationTools;
using System.Runtime.Serialization.Formatters.Binary;
using PatternTools.FastaParser;
using System.Threading.Tasks;

namespace PatternTools.TReXS
{
    public class SearchEngine
    {
        FastaFileParser fastaParser = new FastaFileParser();
        List<SearchResult> mySearchResults = new List<SearchResult>();
        Regex tabSplitterRegex = new Regex(@"\t", RegexOptions.Compiled);
        AminoacidMasses aa = new AminoacidMasses();


        public void LoadFastaDB(string db, bool eliminateReverse)
        {
            fastaParser.MyItems.Clear();
            fastaParser.ParseFile(new StreamReader(db), true, DBTypes.IDSpaceDescription);
            fastaParser.DBName = db;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="precursorMass">If you dont wish to use precursor information, set the value to -1</param>
        /// <param name="precursorTolerance"></param>
        /// <param name="theRegexes"></param>
        /// <param name="scanNumber"></param>
        /// <param name="measuredPrecursorMZ"></param>
        /// <param name="estimatedPrecursorCharge"></param>
        /// <returns></returns>
        /// 
        public SearchResult Search(double searchLowerBound, double searchUpperBound, List<PathCandidate> pathCandidates, int maxRegexes, int scanNumber, double measuredPrecursorMZ, double estimatedPrecursorCharge)
        {
            if (fastaParser.MyItems.Count == 0)
            {
                throw (new Exception("Please load a FASTA DB prior to search"));
            }
            //Now lets score the sequences
            //First lets set all the counters to zero

            

            foreach (FastaItem seq in fastaParser.MyItems)
            {
                seq.ExtraDouble = 0;

                //We will use this to store the matching regexes
                seq.Objects.Clear();
            }

            //Prepare the regexes
            if (pathCandidates.Count > maxRegexes)
            {
                pathCandidates.RemoveRange(maxRegexes - 1, pathCandidates.Count - maxRegexes);
            }


            //The search is done in 2 steps, we first rank a regex for specificity, then we do the search with the score.

            Parallel.ForEach(pathCandidates, p =>
            //foreach (PathCandidate p in pathCandidates)
            {
                Regex r = new Regex(@p.StringRegexForwardReverse, RegexOptions.Compiled);
                List<FastaItem> theMatches = new List<FastaItem>();
                foreach (FastaItem seq in fastaParser.MyItems)
                {
                    if (seq.MonoisotopicMass >= searchLowerBound)
                    {
                        if (seq.MonoisotopicMass <= searchUpperBound || searchUpperBound == -1)
                        {
                            if (r.IsMatch(seq.Sequence))
                            {
                                theMatches.Add(seq);
                                seq.Objects.Add(p);
                            }
                        }
                    }
                }


                if (theMatches.Count > 0)
                {
                    theMatches.Sort((a, b) => a.ExtraDouble.CompareTo(b.ExtraDouble));
                    double points = 1 / (double)theMatches.Count;

                    for (int i = 0; i < theMatches.Count; i++)
                    {
                        double rankingCorrectionFactor = (double)(i + 1) / (double)theMatches.Count;
                        if (rankingCorrectionFactor < 0.2) { rankingCorrectionFactor = 0.2; }

                        double sequenceCorrectionFactor = 1 / (double)theMatches[i].Sequence.Length;

                        theMatches[i].ExtraDouble += points * sequenceCorrectionFactor * rankingCorrectionFactor;

                    }
                }

            });
            //}

            SearchResult result = new SearchResult(scanNumber, measuredPrecursorMZ, estimatedPrecursorCharge);

            //System.Threading.Parallel.ForEach(fastaParser.MyItems, fi =>
            foreach (FastaItem fi in fastaParser.MyItems)
            {
                if (fi.ExtraDouble > 0)
                {
                    SearchResult.SearchResultCandidate sr = new SearchResult.SearchResultCandidate();
                    sr.Name = fi.SequenceIdentifier;
                    sr.Sequence = fi.Sequence;
                    sr.Score = fi.ExtraDouble;
                    sr.TheoreticalMass = fi.MonoisotopicMass;

                    foreach (object o in fi.Objects)
                    {

                        PathCandidate pc = (PathCandidate)o;
                        PatternTools.T_ReXS.RegexGenerationTools.SearchResult.RegexAndPath rp = new PatternTools.T_ReXS.RegexGenerationTools.SearchResult.RegexAndPath(pc.NodePath, pc.StringRegexForwardReverse);
                        sr.RegexAndPaths.Add(rp);
                    }
                    result.TheResults.Add(sr);
                }
            }
            //);
            return result;
        }




        internal void IncrementalBatchSearch(MSParser.MSFull ms, List<PathCandidate> pathCandidates, RegexGeneration.RegexGenerationParams regexParams)
        {
            //Handle the precursor stuff
            double precursor = -1;
            double precursorCharge = -1;
            
            double lowerSearchBound = -1;
            double upperSearchBound = -1;

            if (ms.ZLines.Count > 0 && regexParams.UsePrecursorInformation)
            {
                string[] zLine = tabSplitterRegex.Split(ms.ZLines[0]);
                precursorCharge = double.Parse(zLine[1]);

                precursor = PatternTools.pTools.DechargeMSPeakToPlus1(ms.ChargedPrecursor, precursorCharge);

                if (precursorCharge <= regexParams.MinTrustedCharge)
                {
                    lowerSearchBound = precursor - regexParams.PrecursorLowerBound;
                    upperSearchBound = precursor + regexParams.PrecursorUpperBound;
                }
                else
                {
                    lowerSearchBound = PatternTools.pTools.DechargeMSPeakToPlus1(ms.ChargedPrecursor, regexParams.MinTrustedCharge);
                    upperSearchBound = -1;
                }
            } 
            else if (regexParams.UsePrecursorInformation && ms.ZLines.Count == 0)
            {
                lowerSearchBound = (regexParams.MinTrustedCharge * ms.ChargedPrecursor) - 500;
                upperSearchBound = -1;
            }
            else if (!regexParams.UsePrecursorInformation)
            {
                lowerSearchBound = ms.MSData[(int)(ms.MSData.Count * 0.8)].MZ;
                upperSearchBound = -1;
            }

            SearchResult sr = Search(lowerSearchBound, upperSearchBound, pathCandidates, regexParams.MaxRegexes, ms.ScanNumber, ms.ChargedPrecursor, precursorCharge);
            sr.TheResults.Sort((a, b) => b.Score.CompareTo(a.Score));
            if (sr.TheResults.Count > 0)
            {
                if (sr.DeltaCN >0)
                {
                    mySearchResults.Add(sr);   
                }
            }
        }


        /// <summary>
        /// Writes the incremental results to a txt file and clears the RAM of existing results.
        /// </summary>
        /// <param name="outputFile"></param>
        internal void FlushResultsToSQTFile(string outputFile, RegexGeneration.RegexGenerationParams regexParams)
        {
            StreamWriter sw = new StreamWriter(outputFile);

            sw.WriteLine("#ScanNo\tDeltaCN\tScore\tName\tMeasuredMZ\tEstPrecursorZ\tSequence");
            sw.WriteLine("Database searched:" + fastaParser.DBName);
            string header = regexParams.PrintHeader();
            sw.WriteLine(header);

            foreach (SearchResult sr in mySearchResults)
            {
                sw.WriteLine("M\t" + sr.ScanNumber + "\t" + Math.Round(sr.DeltaCN, 3) + "\t" + sr.TheResults[0].Score + "\t" + sr.TheResults[0].Name + "\t" + sr.MeasuredPrecursorMZ + "\t" + sr.EstimatedPrecursorCharge + "\t" + sr.TheResults[0].Sequence);

                foreach (PatternTools.T_ReXS.RegexGenerationTools.SearchResult.RegexAndPath rp in sr.TheResults[0].RegexAndPaths)
                {
                    string path = "";
                    foreach (int i in rp.NodePath) {
                        path += i + ":";
                    }
                    path.Remove(path.Length-1, 1);


                    sw.WriteLine("R\t" + rp.TheRegex + "\t" + path);
                }

            }
            sw.Close();

            mySearchResults.Clear();
        }
    }
}
