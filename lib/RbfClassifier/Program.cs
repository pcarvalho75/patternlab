using PatternTools;
using PatternTools.GaussianDiscriminant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RbfClassifier
{
    class Program
    {
        static void Main(string[] args)
        {

            SparseMatrix smtrain = new SparseMatrix(new StreamReader (@"C:\Users\paulo_000\AppData\Roaming\Skype\My Skype Received Files\smTrain.txt"));
            SparseMatrix smtest = new SparseMatrix(new StreamReader(@"C:\Users\paulo_000\AppData\Roaming\Skype\My Skype Received Files\smTest.txt"));

            //SparseMatrix smForTraining = Balance(sm);

            Dictionary<int, int> noClustersDictForM = new Dictionary<int, int>();
            noClustersDictForM.Add(1, 2);
            noClustersDictForM.Add(-1, 2);

            //// instantiating the classifier and training
            RbfClassifier.dRBF rbf = new RbfClassifier.dRBF(true, true);

            rbf.Train(smtrain, noClustersDictForM);
            //rbf.TrainTraditional(sm, 3);

            int fN = 0;
            int fP = 0;
            int pC = 0;
            int nC = 0;
            foreach (sparseMatrixRow smr in smtest.theMatrixInRows)
            {
                double gx = 0;
                int l = rbf.ClassifyByLabel(smr.Values, out gx);

                if (smr.Lable == 1)
                {
                    pC++;
                } else
                {
                    nC++;
                }

                if (l != smr.Lable && smr.Lable == 1)
                {
                    fN++;
                }

                if (l != smr.Lable && smr.Lable == -1)
                {
                    fP++;
                }


            }
            Console.WriteLine("Done");

            //// instantiating the ploting form within the form.
           // ViewerForm f1 = new ViewerForm();
           // f1.Viewer.SparseMatrix = sm;

           // #region //-- Plotting -- //



           // f1.Viewer.PlotDecisionSurface(rbf);
           // f1.Viewer.PlotPoints(false, false);
           // f1.Viewer.PlotCentroids(rbf);



           // f1.MyHistogram.Plot(sm, 30, rbf, false);

           // double gXCutOff = CuttOffThresholdForFDR(sm, 0.05);

           // int entries = sm.theMatrixInRows.Count(a => a.ExtraParam >= gXCutOff);

           // f1.ShowDialog();
           // Console.WriteLine("Done");

           // #endregion //-- -- //

           // #region //-- Bayesian --//

           // //Bayesian ---------------------------------------
           // //PatternTools.GaussianDiscriminant.Discriminant d = new PatternTools.GaussianDiscriminant.Discriminant();
           // //d.MySparseMatrix = sm;
           // //d.Model(false, new List<int>(), 20, false, false);

           // //foreach (sparseMatrixRow row in sm.theMatrixInRows)
           // //{
           // //    List<ClassScoreDictionary> csd = d.Classify(row.Values.ToArray());
           // //    csd.Sort((a, b) => b.MyClass.CompareTo(a.MyClass));
           // //    row.ExtraParam = csd[0].Score - csd[1].Score;

           // //}

           //         //double gXCutOff = CuttOffThresholdForFDR(sm, 0.01);
           //         //int entries = sm.theMatrixInRows.Count(a => a.ExtraParam >= gXCutOff);

           //         //f1.ShowDialog();

           // //Test with bayesian classifier

           // //Bayesian ---------------------------------------
           // PatternTools.GaussianDiscriminant.Discriminant d = new PatternTools.GaussianDiscriminant.Discriminant();
           // d.MySparseMatrix = sm;
           // d.Model(false, new List<int>(), 20, false, false);

           // foreach (sparseMatrixRow row in sm.theMatrixInRows)
           // {
           //     List<ClassScoreDictionary> csd = d.Classify(row.Values.ToArray());
           //     csd.Sort((a, b) => b.MyClass.CompareTo(a.MyClass));
           //     row.ExtraParam = csd[0].Score - csd[1].Score;

           // }

           //// double gXCutOff = CuttOffThresholdForFDR(sm, 0.01);
           // //int entries = sm.theMatrixInRows.Count(a => a.ExtraParam >= gXCutOff);

           // //Dictionary<int, List<List<double>>> kMeansDict = RbfClassifier.dRBF.GenerateDictKmeans(sm, noClustersDict);
           // //mRBF mrbf = new mRBF();
           // //mrbf.mTrain(sm, noClustersDictForM);

           // //int correctPos = 0;
           // //int correctNeg = 0;
           // //int errorCounter = 0;

           // //foreach (sparseMatrixRow row in sm.theMatrixInRows)
           // //{
           // //    Dictionary<int, double> dict = mrbf.ClassifyDictionary(row.Values);
           // //    int thelabel = int.MinValue;
           // //    double gx = double.MinValue;

                

           // //    foreach (KeyValuePair<int, double> kvp in dict)
           // //    {
           // //        if (kvp.Value > gx)
           // //        {
           // //            thelabel = kvp.Key;
           // //            gx = kvp.Value;
           // //        }
           // //    }

           // //    if (thelabel == 1)
           // //    {
           // //        double delta = dict[1] - dict[-1];
           // //        row.ExtraParam = gx;
           // //    }
           // //    else
           // //    {
           // //        row.ExtraParam = double.MinValue;
           // //    }

           // //    if (thelabel == row.Lable)
           // //    {
           // //        if (row.Lable == 1)
           // //        {
           // //            correctPos++;
           // //        }
           // //        else
           // //        {
           // //            correctNeg++;
           // //        }
           // //    }
           // //    else
           // //    {
           // //        errorCounter++;
           // //    }
           // //    //Dictionary<int, double> theres = mrbf.ClassifyDictionary(row.Values);
           // //    //row.ExtraParam = theres[1] - theres[-1];

           // //}

           // //double gXCutOff = CuttOffThresholdForFDR(sm, 0.01);

           // #endregion //-- --//

           // #region //-- Simple RBF --//

           // //for (int i = 2; i <= 7; i++)
           // //{
           // //    for (int j = 2; j <= 7; j++)
           // //    {
           // //        Dictionary<int, int> noClustersDict = new Dictionary<int, int>();
           // //        noClustersDict.Add(1, i);
           // //        noClustersDict.Add(-1, j);                    

           // //        dRBF drbd = new dRBF(true);
           // //        drbd.Train(sm, noClustersDict);


           // //        foreach (sparseMatrixRow row in sm.theMatrixInRows)
           // //        {
           // //            double gx;
           // //            int label = drbd.ClassifyByLabel(row.Values, out gx);

           // //            row.ExtraParam = label;

           // //dRBF drbd = new dRBF(true);
           // //drbd.Train(sm, noClustersDict);

           // //sm.theMatrixInRows.AsParallel().ForAll(a => a.ExtraParam = drbd.Classify(a.Values));

           // //foreach (sparseMatrixRow row in sm.theMatrixInRows)
           // //{
           // //    double gx;
           // //    int label = drbd.ClassifyByLabel(row.Values, out gx);

           // //    row.ExtraParam = gx;

           // //}
           // //double gXCutOff = CuttOffThresholdForFDR(sm, 0.01);




           // //int correctPos = sm.theMatrixInRows.Count(a => a.Lable == (int)a.ExtraParam && a.Lable == 1);
           // //int correctNeg = sm.theMatrixInRows.Count(a => a.Lable == (int)a.ExtraParam && a.Lable == -1);

           // //Console.Write(Math.Round((double)correctNeg / (double)totalNeg, 2) + "\t");
           // #endregion //-- --//

            //Console.WriteLine("Done");
        }

        private static SparseMatrix Balance(SparseMatrix sm)
        {
            List<sparseMatrixRow> positive = sm.theMatrixInRows.FindAll(a => a.Lable == 1);
            List<sparseMatrixRow> negative = sm.theMatrixInRows.FindAll(a => a.Lable == -1);

            List<sparseMatrixRow> toRemove = negative;
            List<sparseMatrixRow> toKeep = positive;
            int noToRemove = Math.Abs(positive.Count - negative.Count);
            if (positive.Count > negative.Count)
            {
                toRemove = positive;
                toKeep = negative;
            }

            int removedItems = 0;
            while (toRemove.Count > noToRemove)
            {
                Random r = new Random(toRemove.Count);
                int index = (int) Math.Floor(r.NextDouble() * (double) toRemove.Count);
                toRemove.RemoveAt(index);
                removedItems++;
            }

            SparseMatrix smNew = new SparseMatrix();
            smNew.theMatrixInRows.AddRange(toRemove);
            smNew.theMatrixInRows.AddRange(toKeep);

            return smNew;            
        }


        class Gambiarra
        {
            public int Label { get; set; }
            public double Gx { get; set; }
            public Gambiarra(int label, double gx)
            {
                Label = label;
                Gx = gx;
            }
        }
        /// <summary>
        /// This method return the g(x) value for the selected cutoff fdr value.
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="acceptableFDR"></param>
        /// <returns></returns>
        private static double CuttOffThresholdForFDR(SparseMatrix sm, double acceptableFDR)
        {
            //ranking the matrix by g(x) value;
            List<Gambiarra> toCalculateFDR = (from row in sm.theMatrixInRows
                                              orderby row.ExtraParam descending
                                              select new Gambiarra(row.Lable, row.ExtraParam)).ToList();

            double negativeCount = (double)toCalculateFDR.Count(a => a.Label == -1);
            double cuttoff = double.MaxValue;

            StreamWriter sr = new StreamWriter("out.txt");
            for (int i = toCalculateFDR.Count - 1; i >= 0; i--)
            {
                double thisFDR = negativeCount / (double)(i + 1);

                if (thisFDR <= acceptableFDR)
                {
                    cuttoff = toCalculateFDR[i].Gx;
                    break;
                }

                sr.WriteLine(toCalculateFDR[i].Gx + "\t" + thisFDR + "\t" + negativeCount + "\t" + (double)toCalculateFDR.Count + 1);


                if (toCalculateFDR[i].Label == -1)
                {
                    negativeCount--;
                }
            }

            sr.Close();

            return cuttoff;

        }


    }
}