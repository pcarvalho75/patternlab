using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UniQ
{
    public static class UniQTools
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="onlyUniquePeptides"></param>
        /// <param name="classLabels"></param>
        /// <param name="minQuantsPerPeptide"></param>
        /// <returns>the list ofchannels and the list of peptides</returns>
        /// 
        public static KeyValuePair<List<FastaItem>, List<PepQuant>> ParsePeptideReport(string fileName)
        {
            List<PepQuant> pepsAll = new List<PepQuant>();

            bool fastaData = false;
            List<FastaItem> MyFastaItems = new List<FastaItem>();

            List<PepQuant> peps = new List<PepQuant>();
            StreamReader sr = new StreamReader(fileName);
            string line;
            int counter = 0;
            PepQuant pq = null;
            while ((line = sr.ReadLine()) != null)
            {
                counter++;
                //Console.WriteLine("Parsing line: " + counter);

                if (line.Equals("#Fasta Items"))
                {
                    fastaData = true;
                } else if (line.StartsWith("#Channels"))
                {
                    continue;
                }
                else if (fastaData)
                {
                    //Parse fasta data
                    if (line.StartsWith(">"))
                    {
                        string[] cols = Regex.Split(line, " ");
                        string protID = cols[0].ToString();
                        protID = protID.Remove(0, 1);

                        string protDesc = "";

                        for (int i = 0; i < cols.Length; i++)
                        {
                            if (i == 0) { continue; }
                            if (i == 1)
                            {
                                protDesc += cols[1];
                            } else
                            {
                                protDesc += " " + cols[i];
                            }
                        }

                        FastaItem fi = new FastaItem(protID, protDesc);
                        MyFastaItems.Add(fi);
                    }
                    else
                    {
                        MyFastaItems.Last().Sequence += line;
                    }

                }

                else
                {

                    if (line.StartsWith("Peptide:"))
                    {

                        peps.Add(pq);
                        string[] cols = Regex.Split(line, "\t");

                        //GetSequence
                        string[] colsSeq = Regex.Split(cols[0], ":");

                        pq = new PepQuant(colsSeq[1]);

                    }
                    else
                    {
                        //quant line
                        List<string> cols = Regex.Split(line, "\t").ToList();

                        List<string> cols2 = Regex.Split(cols[0], Regex.Escape(".")).ToList();


                        int scanNumber = int.Parse(cols2[cols2.Count - 2]);
                        int z = int.Parse(cols2.Last());
                        string filename = Regex.Replace(cols[0], Regex.Escape(".") + scanNumber + Regex.Escape(".") + scanNumber + Regex.Escape(".") + z, "");

                        //Get the signals
                        cols.RemoveAt(0);

                        try {
                            List<double> signals = cols.Select(a => double.Parse(a)).ToList();

                            //This fix is required because sometimes the purity correction may yield negative numbers.
                            for (int i = 0; i < signals.Count; i++)
                            {
                                if (signals[i] < 0)
                                {
                                    signals[i] = 0;
                                }
                            }

                            int qc = signals.Count(a => a > 0);

                            if (!signals.Contains(double.NaN))
                            {
                                pq.MyQuants.Add(new Quant(z, filename, scanNumber, signals));
                            }
                        
                        } catch
                        {
                            Console.WriteLine("Failed parsing line: " + counter);
                        }

                    }

                }
            }

            peps.Add(pq);
            peps.RemoveAt(0);
            pepsAll.AddRange(peps);
            sr.Close();

            pepsAll.RemoveAll(a => a.MyQuants.Count == 0);

            return new KeyValuePair<List<FastaItem>, List<PepQuant>>(MyFastaItems, pepsAll);

        }



        /// <summary>
        /// This will update them with their Fold, T-Test corrected T-Test and Binomial
        /// </summary>
        /// <param name="classLabels"></param>
        /// <param name="PepQuants"></param>
        public static void ScorePepQuants(List<int> classLabels, List<PepQuant> PepQuants)
        {
            #region Bionomial

            //Find all positives in the dataset
            //Find how many positive values and 0 values we have in the item list      
            double priorSuccessProbability = 0.0;

            //Obtain number of signals from all isobaric markers from all spectra
            int n = PepQuants.Select(a => a.MyQuants.Count).Sum() * classLabels.ToList().Count(a => a > 0);

            //Obtain number of quants with value greater than 0
            double tp = (from pq in PepQuants
                         from item in pq.MyQuants
                         select SuccessCounter(item.MarkerIntensities, classLabels)).Sum();

            priorSuccessProbability = tp / n;
      
            int noClasses = classLabels.Count(a => a > 0);

            List<int> C1 = new List<int>();
            List<int> C2 = new List<int>();

            for (int i = 0; i < classLabels.Count; i++)
            {
                if (classLabels[i] == 1)
                {
                    C1.Add(i);
                }
                else if (classLabels[i] == 2)
                {
                    C2.Add(i);
                }
                else if (classLabels[i] == -1)
                {
                    //Lets ignore this guy
                }
                else
                {
                    throw new Exception("This method currently supports only two classes and they must be labeled as 1 and 2.");
                }
            }
            
            foreach (PepQuant pq in PepQuants)
            {
              
                    int localSuccessCounter = pq.MyQuants.Sum(a => SuccessCounter(a.MarkerIntensities, classLabels));
                    int n_Local = noClasses * pq.MyQuants.Count;
                    pq.BinomialProbability = alglib.binomialcdistribution(localSuccessCounter - 1, n_Local, priorSuccessProbability);

                #region folds
                List<double> folds = new List<double>();
                List<double> c1, c2;
                double bothTails = -1, leftTail = -1, rightTail = -1;

                double missingValues = 0;
                foreach (Quant q in pq.MyQuants)
                {
                    ObtainMarkerIntensities(C1, C2, q, out c1, out c2);

                    missingValues += c1.RemoveAll(a => a == 0);
                    missingValues += c2.RemoveAll(a => a == 0);

                    if (c1.Count > 0 && c2.Count > 0)
                    {
                        folds.Add(c1.Average() / c2.Average());
                    }

                    if (pq.MyQuants.Count == 1)
                    {
                        if (c1.Count > 1 && c2.Count > 1)
                        {
                            alglib.studentttest2(c1.ToArray(), c1.Count, c2.ToArray(), c2.Count, out bothTails, out leftTail, out rightTail);
                            pq.TTest = bothTails;
                        }
                        else
                        {
                            pq.TTest = 0.5;
                        }
                    }               
                }

                pq.MissingValues = (int)missingValues;

                pq.Folds = folds;

                #endregion

                #region PairedTTest

                if (folds.Count > 1)
                {
                    double[] f = folds.Select(a => Math.Log(a)).ToArray();
                    alglib.studentttest1(f, f.Length, 0, out bothTails, out leftTail, out rightTail);

                    if (bothTails < 0.001)
                    {
                        bothTails = 0.001;
                    }

                    pq.TTest = bothTails;
                }
                
            
                #endregion

                #region AvgFold
                if (folds.Count == 0)
                {
                    pq.AVGLogFold = 0;
                }
                else
                {
                    List<double> lnFolds = folds.Select(a => Math.Log(a)).ToList();
                    pq.AVGLogFold = lnFolds.Average();
                }


                #endregion
            }
            #endregion

            //Now a final correction for the binomial p-values
            foreach (PepQuant pq in PepQuants)
            {
                if (pq.AVGLogFold > 0)
                {
                    pq.BinomialProbability = 1 - pq.BinomialProbability;
                }
            }

            PepQuants.Sort((a, b) => a.BinomialProbability.CompareTo(b.BinomialProbability));

        }


        private static void ObtainMarkerIntensities(List<int> C1, List<int> C2, Quant q, out List<double> c1, out List<double> c2)
        {
            c1 = new List<double>();
            c2 = new List<double>();

            for (int i = 0; i < q.MarkerIntensities.Count; i++)
            {
                if (C1.Contains(i))
                {
                    c1.Add(q.MarkerIntensities[i]);
                }
                else if (C2.Contains(i))
                {
                    c2.Add(q.MarkerIntensities[i]);
                }
                else
                {
                    //disconsidered marker in channel " + i
                }
            }
        }
   
        static int SuccessCounter(List<double> i ,List<int> classLabels)
        {

            int sucess = 0;

            for (int x = 0; x < i.Count; x++)
            {

                if ((classLabels[x] == 1) && (i[x] == 0))
                {
                    sucess++;
                } 
                else if ((classLabels[x] == 2) && (i[x] != 0))
                {
                    sucess++;
                }
            }

            return sucess;

        }
     
    }
}
