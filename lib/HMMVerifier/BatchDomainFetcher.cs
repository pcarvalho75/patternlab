using HMMVerifier.HMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HMMVerifier
{
    class FastaSeqence
    {
        public string Locus { get; set; }
        public string Sequence { get; set; }

        public FastaSeqence() { }

        public FastaSeqence(string locus, string sequence)
        {
            Locus = locus;
            Sequence = sequence;
        }
    }

    public class BatchDomainFetcher
    {
        HMMClient hmm = new HMMClient();


        public static int Percent { get; set; }
        public static List<HMMResult> Results { get; set;}

        public BatchDomainFetcher()
        {
            Percent = 0;
            Results = new List<HMMResult>();
        }

        public void Fetch(Dictionary<string, string> locusFastaDict, bool compress, int step, int iEvalue, int eValue)
        {

            if (compress) {
                locusFastaDict = CompressFastaDictionary(locusFastaDict);
            }

            int stepCounter = 0;
            int totalCounter = 0;

            Results = new List<HMMResult>(locusFastaDict.Keys.Count);
            Dictionary<string, string> toAnalyze = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> kvp in locusFastaDict)
            {
                totalCounter++;
                toAnalyze.Add(kvp.Key, kvp.Value);
                if (stepCounter == step)
                {
                    List<HMMResult> hmmResults = hmm.FilteredScans(toAnalyze, iEvalue, eValue).ToList();
                    Results.AddRange(hmmResults);
                    toAnalyze = new Dictionary<string, string>();
                    Percent = (int)(((double)totalCounter / (double)locusFastaDict.Keys.Count) * 100);
                    Console.WriteLine("Progress:: {0} / {1} = {2}", totalCounter, locusFastaDict.Keys.Count, Percent + "%");
                    stepCounter = 0;
                }

                
                stepCounter++;
            }

            if (toAnalyze.Count > 0)
            {
                Results.AddRange(hmm.Scans(toAnalyze).ToList());
            }
            Percent = 100;
        }

        public void Fetch(string dataInFastaFormat, bool compress, int step)
        {
            Dictionary<string, string> fastaDict = ParseFasta(dataInFastaFormat, compress);
            Fetch(fastaDict, false, step, 1, 1);
        }


        private Dictionary<string, string> ParseFasta(string fastaFile, bool compress)
        {
            Percent = 0;

            string[] theLines = Regex.Split(fastaFile, "\n");

            List<FastaSeqence> theSequences = new List<FastaSeqence>(theLines.Length);

            FastaSeqence fs = new FastaSeqence();
            for (int i = 0; i < theLines.Length; i++)
            {
                if (theLines[i].StartsWith("#")) { continue; }

                string cleanedLine = Regex.Replace(theLines[i], "\n", "");
                if (theLines[i].StartsWith(">"))
                {
                    string theKey = Regex.Replace(cleanedLine, ">", "");
                    theSequences.Add(fs);
                    fs = new FastaSeqence(theKey, "");
                }
                else
                {
                    fs.Sequence += cleanedLine;
                }
            }

            theSequences.Add(fs);
            theSequences.RemoveAt(0);

            Dictionary<string, string> theDict = theSequences.ToDictionary(a => a.Locus, a => a.Sequence);

            if (compress)
            {
                theDict = CompressFastaDictionary(theDict);
            }
            
            return theDict;

        }

        private static Dictionary<string, string> CompressFastaDictionary(Dictionary<string, string> theSequencesDict)
        {
            List<FastaSeqence> theSequences = new List<FastaSeqence>(theSequencesDict.Keys.Count);

            foreach (KeyValuePair<string, string> kvp in theSequencesDict)
            {
                theSequences.Add(new FastaSeqence(kvp.Key, kvp.Value));
            }

            List<FastaSeqence> toRemove = new List<FastaSeqence>(theSequences.Count);

            theSequences.Sort((a, b) => b.Sequence.Length.CompareTo(a.Sequence.Length));

            for (int i = theSequences.Count - 1; i >= 0; i--)
            {
                string thisSeq = theSequences[i].Sequence.ToString();

                for (int j = i - 1; j >= 0; j--)
                {
                    if (theSequences[j].Sequence.Contains(theSequences[i].Sequence))
                    {
                        toRemove.Add(theSequences[i]);
                        break;
                    }
                }
            }

            theSequences = theSequences.Except(toRemove).ToList();

            Dictionary<string, string> compressedDictionary = theSequences.ToDictionary(a => a.Locus, a => a.Sequence);

            Console.WriteLine(toRemove.Count + " subset sequences removed");

            return compressedDictionary;
            
        }


    }
}
