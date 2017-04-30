using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace PatternTools.SQTParser2
{
    public class SQTParser2
    {
        Regex Ssplitter = new Regex("[\r\n]S|[\r]S|[\n]S", RegexOptions.Compiled);
        Regex Msplitter = new Regex("[\r\n]M|[\r]M|[\n]M", RegexOptions.Compiled);
        Regex SplitLine = new Regex("[\r\n]|[\r]|[\n]", RegexOptions.Compiled);
        Regex TabSplitter = new Regex("\t", RegexOptions.Compiled);

        string database = "?";
        public string Database
        {
            get { return database; }
        }

        public List<SQTScan2> Parse (string fileName, int matchesToConsider = 2)
        {
            if (matchesToConsider < 2)
            {
                throw new Exception("Minimum of 2 matches must be considered in the SQT parser.");
            }

            //Scan Example
            //scan number, scan number, charge state, time used for search, computer name, measured M+H, total ion intensity, probability value, number of singleSearchCandidates, 
            //Match, primary rank, secondary rank, calculated M+H, delta CN, Primary Score, Secondary Score, peaks matched, peaks considered, peptide sequence, Manual Validation Status
            //S	198	198	2	172	blacknote	1491.6589	39274362.53	0.0129	60284
            //M	1	5	1491.6505	0.0000	1.4029	10.368	8	23	F.GAGLGGGYGGGFGSGFSS.S	U
            //L	Reverse_contaminant_KERATIN08 PositionInAASequence
            //L	Reverse_contaminant_KERATIN05 PositionInAASequence
            //M	2	21	1491.6789	0.0982	1.2651	6.559	5	15	S.LNQDSMLHYLEE.I	U
            //L	Reverse_YLR398C PositionInAASequence
            //M	155	1	1491.7039	0.5606	0.6164	11.649	8	17	F.DLKSDNSVKNNSGN.N	U
            //L	Reverse_YKL112W PositionInAASequence

            StreamReader sr = new StreamReader(fileName);

            List<SQTScan2> scanArray = new List<SQTScan2>();
            SQTScan2 s = new SQTScan2();

            //Used to make sure that we are only keeping the IDs from the first match
            int lineCounter = 0;

            string line = null;

            while ((line = sr.ReadLine()) != null) { 

                lineCounter++;

                string[] cols = TabSplitter.Split(line);
                if (cols.Length == 1) { continue; }

                if (cols[0].Equals("H"))
                {
                    if (cols[1].Equals("Database"))
                    {
                        database = cols[2];
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (cols[0].StartsWith("S"))
                {
                    scanArray.Add(s);
                    s = new SQTScan2();
                    s.ScanNumber = int.Parse(cols[1]);
                    s.ChargeState = int.Parse(cols[3]);
                    s.ProbabilityValue = double.Parse(cols[8]);
                    s.MeasuredMH = double.Parse(cols[6]);
                    s.NumberOfCandidates = int.Parse(cols[9]);
                    s.FileName = fileName;

                }
                else if (cols[0].Equals("M"))
                {
                    Match m = new Match();
                    m.PrimaryRank = int.Parse(cols[1]);
                    m.SecondaryRank = int.Parse(cols[2]);
                    m.TheoreticalMH = double.Parse(cols[3]);
                    m.DeltaCN = double.Parse(cols[4]);
                    m.PrimaryScore = double.Parse(cols[5]);
                    m.SecondaryScore = double.Parse(cols[6]);
                    m.PeaksMatched = int.Parse(cols[7]);
                    m.PeaksConsidered = int.Parse(cols[8]);
                    m.PeptideSequence = cols[9].Substring(2, cols[9].Length - 4);
                    s.Matches.Add(m);
                }
                else if (cols[0].Equals("L"))
                {
                    ID id;

                    if (cols.Length == 4)
                    {
                        //ProLuCID
                        id = new ID(cols[1], cols[3]);
                    }
                    else
                    {
                        //Comet
                        id = new ID(cols[1], "NA");
                    }
                    s.Matches.Last().IDs.Add(id);
                }
                else if (cols[0].Length == 0)
                {
                    //Skip this line
                }
                else
                {
                    Console.WriteLine("Problematic line in sqt file " + fileName + "\nline content: " + line + "\nLine # " + lineCounter);
                    Console.Beep();
                    //throw new Exception("Unrecognized line");
                }

            }

            //Add the last scan
            scanArray.Add(s);

            //The first index is bogus so we should remove it
            scanArray.RemoveAt(0);

            //And now calculate DeltaCN for everyone


            return scanArray;

        }
    }


}
