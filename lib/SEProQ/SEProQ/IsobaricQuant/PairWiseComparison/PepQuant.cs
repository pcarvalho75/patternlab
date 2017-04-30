using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProQ.IsobaricQuant.PairWiseComparison
{
    public class PepQuant
    {
        public string Sequence { get; set; }
        public int PeptideRedundancy { get; set; }
        public List<List<double>> Quants;
        
        /// <summary>
        /// indexes for class 1
        /// </summary>
        List<int> C1 { get; set; }

        /// <summary>
        /// indexes for class 2
        /// </summary>
        List<int> C2 { get; set; }

        public PepQuant(string sequence, List<int> classLabels, int peptideRedundancy)
        {

            if (!classLabels.Contains(1) || !classLabels.Contains(2))
            {
                throw new Exception("This module can only be used for isobaric experiments with two biological conditions.  The classes must be labeled as 1 and 2.");
            }

            Sequence = sequence;
            PeptideRedundancy = peptideRedundancy;
            Quants = new List<List<double>>();
            C1 = new List<int>();
            C2 = new List<int>();

            for (int i = 0; i< classLabels.Count; i++) 
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

            
        }

        public List<double> Folds
        {
            get
            {
                List<double> folds = new List<double>();
                foreach (List<double> qts in Quants)
                {
                    List<double> c1 = new List<double>();
                    List<double> c2 = new List<double>();

                    for (int i = 0; i < qts.Count; i++)
                    {
                        if (C1.Contains(i))
                        {
                            c1.Add(qts[i]);
                        }
                        else if (C2.Contains(i))
                        {
                            c2.Add(qts[i]);
                        }
                        else
                        {
                            //disconsidered marker in channel " + i
                        }
                    }

                    folds.Add(c1.Average() / c2.Average());
                }

                return folds;
            }
        }

        public double TTest
        {
            get
            {
                double bothTails = -1;
                double leftTail = -1;
                double rightTail = -1;

                List<double> folds = Folds.Select(a => Math.Log(a)).ToList();
                //alglib.onesamplesigntest(folds.ToArray(), folds.Count, 0, out bothTails, out leftTail, out rightTail);
                alglib.studentttest1(folds.ToArray(), folds.Count, 0, out bothTails, out leftTail, out rightTail);

                if (bothTails < 0.01)
                {
                    bothTails = 0.01;
                }

                return bothTails;
            }
        }

        public double CorrectedTTest
        {
            get
            {
                double correctedTTest = TTest;

                if (AVGLogFold < 0)
                {
                    correctedTTest = 1 - TTest;
                }

                return correctedTTest;
            }
        }



        public double AVGLogFold
        {
            get
            {
                double avg = Folds.Average();
                return Math.Log(avg);
            }
        }
    }
}
