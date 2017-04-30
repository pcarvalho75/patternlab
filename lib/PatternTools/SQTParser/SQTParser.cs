using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace PatternTools.SQTParser
{
    /// <summary>
    /// Depricated.  From now on use SQTParser2
    /// </summary>
    public class SQTParser
    {
        Regex Ssplitter = new Regex("[\r\n]S|[\r]S|[\n]S", RegexOptions.Compiled);
        Regex Msplitter = new Regex("[\r\n]M|[\r]M|[\n]M", RegexOptions.Compiled);
        Regex SplitLine = new Regex("[\r\n]|[\r]|[\n]", RegexOptions.Compiled);
        Regex TabSplitter = new Regex("\t", RegexOptions.Compiled);

        List<SQTScan> theScans = new List<SQTScan>();
        string database = "?";
        public string Database
        {
            get { return database; }
        }

        public List<SQTScan> Scans
        {
            get { return theScans; }
        }
 
        public SQTParser(string fileName)
        {
            int matchesToConsider = 2;

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

            List<SQTScan> scanArray = new List<SQTScan>();
            SQTScan s = new SQTScan();
            
            //Used to make sure that we are only keeping the IDs from the first match
            int lastMatch = 0;
            int lineCounter = 0;

            string[] scans = Ssplitter.Split(sr.ReadToEnd());

            foreach (string scan in scans)
            {
                lineCounter++;

                //try
                //{
                    int matchOneCounter = 0;
                    List<string> line = Regex.Split(scan, "\r\n|\r|\n").ToList(); ;
                    line.RemoveAll(a => a.Equals(""));

                    for (int l = 0; l < line.Count; l++)
                    {
                        string[] cols = TabSplitter.Split(line[l]);
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

                        if (l == 0)
                        {

                            scanArray.Add(s);
                            if (cols.Length < 8)
                            {
                                 Console.WriteLine("Breaking on line " + lineCounter);
                                 break;
                            }
                            s = new SQTScan(int.Parse(cols[1]), 0);
                            s.ChargeState = int.Parse(cols[3]);
                            s.ProbabilityValue = double.Parse(cols[8]);
                            s.MeasuredMH = double.Parse(cols[6]);
                            

                            int numbOfCandidates;
                            if (int.TryParse(cols[9], out numbOfCandidates))
                            {
                                s.NumberOfCandidates = numbOfCandidates;
                            }
                            else
                            {
                                s.NumberOfCandidates = int.MaxValue;
                            }
                        }
                        else if (cols[0].Equals("M"))
                        {
                            int matchNo = int.Parse(cols[1]);
                            lastMatch = matchNo;
                            if (matchNo == 1)
                            {
                                matchOneCounter++;
                                s.PrimaryRank = int.Parse(cols[1]);
                                s.SecondaryRank = int.Parse(cols[2]);
                                s.TheoreticalMH = double.Parse(cols[3]);
                                s.DeltaCN = 0;
                                s.PrimaryScore = double.Parse(cols[5]);
                                s.SecondaryScore = double.Parse(cols[6]);
                                s.PeaksMatched = int.Parse(cols[7]);
                                s.PeaksConsidered = int.Parse(cols[8]);
                                s.PeptideSequence = cols[9].Replace(" ", "");

                            }
                            else if (matchNo > 1)
                            {
                                if (cols.Length == 11)
                                {
                                    s.DeltaCN = double.Parse(cols[4]);
                                }
                            }

                            if (matchNo > matchesToConsider + 1)
                            {
                                break;
                            }


                        }
                        else if (cols[0].Equals("L"))
                        {
                            //Problematic line
                            //L	Reverse_contaminant_gi|625237	15	DFV.TYYQRIFVD.GLI
                            if (lastMatch == 1)
                            {
                                string identificationName = cols[1];
                                //Column 1 holds the Locci
                                if (identificationName.StartsWith("IPI"))
                                {
                                    string[] cols2 = Regex.Split(identificationName, " ");
                                    identificationName = cols2[0];
                                }
                                s.IdentificationNames.Add(identificationName);
                            }
                        }
                        else if (cols[0].Length == 0)
                        {
                            //Skip this line
                        }
                        else
                        {
                            Console.WriteLine("Problematic line in sqt file " + fileName + "\nline content: " + line[l] + "\nLine # " + lineCounter);
                            Console.Beep();
                            //throw new Exception("Unrecognized line");
                        }
                    }

                    s.IdentificationNames = s.IdentificationNames.Distinct().ToList();

                    //This is to make sure that we don't have different peptides tied in first place
                    if (matchOneCounter == 1)
                    {
                        theScans.Add(s);
                    }
                //}
                //catch (Exception e7)
                //{
                //    Console.WriteLine("Problem parsing scan.");
                //    Console.WriteLine(lineCounter);

                //}
            }

            //The first index is bogus so we should remove it
            scanArray.RemoveAll(a => string.IsNullOrEmpty(a.PeptideSequence));
        }
    }

    


}
