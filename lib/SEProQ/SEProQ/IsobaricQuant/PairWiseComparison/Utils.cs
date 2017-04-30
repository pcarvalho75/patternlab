using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEProQ.IsobaricQuant.PairWiseComparison
{
    public static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="onlyUniquePeptides"></param>
        /// <param name="classLabels"></param>
        /// <param name="minQuantsPerPeptide"></param>
        /// <returns>the list ofchannels and the list of peptides</returns>
        public static KeyValuePair<List<int>, List<PepQuant>> ParsePeptideReport(List<string> fileNames, bool onlyUniquePeptides, List<int> classLabels, int minQuantsPerPeptide)
        {
            List<PepQuant> pepsAll = new List<PepQuant>();
            List<int> Channels = new List<int>();

            foreach (string fileName in fileNames)
            {
                List<PepQuant> peps = new List<PepQuant>();
                StreamReader sr = new StreamReader(fileName);
                string line;
                int counter = 0;
                PepQuant pq = null;
                while ((line = sr.ReadLine()) != null)
                {
                    counter++;
                    if (counter > 1)
                    {
                        if (line.StartsWith("Peptide:"))
                        {

                            peps.Add(pq);
                            string[] cols = Regex.Split(line, "\t");

                            //GetSequence
                            string[] colsSeq = Regex.Split(cols[0], ":");

                            //Get redundancy
                            string[] colsRedundancy = Regex.Split(cols[2], ":");

                            pq = new PepQuant(colsSeq[1], classLabels, int.Parse(colsRedundancy[1]));

                        }
                        else
                        {
                            List<string> cols = Regex.Split(line, "\t").ToList();
                            cols.RemoveAt(0);
                            List<double> quants = cols.Select(a => double.Parse(a)).ToList();
                            int qc = quants.Count(a => a > 0);
                            if (qc >= minQuantsPerPeptide)
                            {
                                pq.Quants.Add(quants);
                            }
                        }
                    }
                    else
                    {
                        Channels = Regex.Split(line, "\t").Select(a => int.Parse(a)).ToList();

                    }
                   

                }

                peps.Add(pq);
                peps.RemoveAt(0);
                pepsAll.AddRange(peps);
                sr.Close();
            }

            //Join the stuff
            List<PepQuant> tmp = new List<PepQuant>();
            if (fileNames.Count > 1)
            {
                List<string> peptides = pepsAll.Select(a => a.Sequence).Distinct().ToList();

                foreach (string p in peptides)
                {
                    List<PepQuant> tmpPeps = pepsAll.FindAll(a => a.Sequence.Equals(p));
                }
            }

            if (onlyUniquePeptides)
            {
                pepsAll.RemoveAll(a => a.PeptideRedundancy > 1);
            }
            pepsAll.RemoveAll(a => a.Quants.Count == 0);



            return new KeyValuePair<List<int>, List<PepQuant>>(Channels, pepsAll);
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="onlyUniquePeptides"></param>
        /// <param name="classLabels"></param>
        /// <param name="minQuantsPerPeptide"></param>
        /// <returns>the list ofchannels and the list of peptides</returns>
        public static Dictionary<string, List<double[]>> ParsePeptideReportGeneric(List<string> fileNames)
        {
            Dictionary<string, List<double []>> MyScans = new Dictionary<string,List<double []>>();
            

            foreach (string fileName in fileNames)
            {
                StreamReader sr = new StreamReader(fileName);
                string line;
                int counter = 0;
                string activePeptide = null;

                while ((line = sr.ReadLine()) != null)
                {
                    counter++;
                    if (counter > 1)
                    {
                        if (line.StartsWith("Peptide:"))
                        {
                            string[] cols = Regex.Split(line, "\t");

                            //GetSequence
                            string[] colsSeq = Regex.Split(cols[0], ":");

                            //Get redundancy
                            string[] colsRedundancy = Regex.Split(cols[2], ":");
                            activePeptide = colsSeq[1];

                            if (!MyScans.ContainsKey(activePeptide))
                            {
                                MyScans.Add(activePeptide, new List<double[]>());
                            }

                        }
                        else
                        {
                            List<string> cols = Regex.Split(line, "\t").ToList();
                            cols.RemoveAt(0);
                            double [] quants = cols.Select(a => double.Parse(a)).ToArray();
                            MyScans[activePeptide].Add(quants);
                        }
                    }
                    


                }

                sr.Close();
            }


            return MyScans;
        }
    }
}
