using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;
using System.Threading.Tasks;
using SEProcessor.Result;
using PatternTools.SQTParser;
using SEPRPackage;

namespace SEProcessor.Classifier
{
    public static class ClassifierScoring
    {
        //Optionally returns the training sparse matrix
        public static SparseMatrix BayesianScoringPSM(List<SQTScan> myScans, Parameters myParams, bool considerPresence, bool considerForms, Dictionary<int, double> peptidePriors)
        {

            int minNoExamplesForClassModel = 4;

            PatternTools.GaussianDiscriminant.Discriminant gd = new PatternTools.GaussianDiscriminant.Discriminant();
            List<sparseMatrixRow> theRows = new List<sparseMatrixRow>();
            Utils.GenerateSparseMatrix(myScans, myParams, theRows, considerPresence, considerForms);
            //we will just train accepting there are no outliers

            List<int> unstableDims = PatternTools.GaussianDiscriminant.StabilityVerifier.Verify(theRows);
            gd.MySparseMatrix.theMatrixInRows = theRows;


            gd.Model(false, new List<int>(), minNoExamplesForClassModel, true, false);


            if (myParams.QFilterMahalanobisDistance)
            {

                List<sparseMatrixRow> outlierRows = new List<sparseMatrixRow>();
                //Measure the MH distance for all vectors and eliminate the ones with MH greater than specified value
                foreach (sparseMatrixRow smr in gd.MySparseMatrix.theMatrixInRows)
                {
                    double md = gd.MahalanobisDistance(smr);
                    if (md > myParams.QFilterMahalanobisDistanceValue)
                    {
                        outlierRows.Add(smr);
                    }
                }



                List<PatternTools.sparseMatrixRow> positiveRows = gd.MySparseMatrix.theMatrixInRows.FindAll(a => a.Lable == 1);
                List<PatternTools.sparseMatrixRow> negativeRows = gd.MySparseMatrix.theMatrixInRows.FindAll(a => a.Lable == -1);
                int outliersPositive = outlierRows.FindAll(a => a.Lable == 1).Count;
                int outliersNegative = outlierRows.FindAll(a => a.Lable == -1).Count;

                Console.WriteLine("\n===== Outlier detection with Mahalanobis Distance > {0} =====", myParams.QFilterMahalanobisDistanceValue);
                Console.WriteLine("= Target class : " + outliersPositive + " / " + positiveRows.Count);
                Console.WriteLine("= Labeled Decoy class : " + outliersNegative + " / " + negativeRows.Count);
                Console.WriteLine("=============================================================\n");


                //Make sure we have enough juice so to afford eliminating outliers
                if ((negativeRows.Count - outliersNegative) > minNoExamplesForClassModel + 1)
                {

                    //delete previous classification model
                    foreach (sparseMatrixRow smr in outlierRows)
                    {
                        gd.MySparseMatrix.theMatrixInRows.Remove(smr);
                    }
                    List<sparseMatrixRow> cleanedMatrix = PatternTools.ObjectCopier.Clone(gd.MySparseMatrix.theMatrixInRows);
                    unstableDims.AddRange(PatternTools.GaussianDiscriminant.StabilityVerifier.Verify(theRows));
                    unstableDims.Sort((a, b) => b.CompareTo(a));


                    //generate a new model based on cloned and cleaned sparse matrix
                    gd = new PatternTools.GaussianDiscriminant.Discriminant();
                    gd.MySparseMatrix.theMatrixInRows = cleanedMatrix;
                    gd.Model(false, new List<int>(), minNoExamplesForClassModel, true, false);
                    Console.WriteLine("Done cleaning outliers and generating clean model");
                }

            }

            if (gd.ClassLableClassPackageDic.Keys.Count == 1)
            {
                //throw new System.ArgumentException("Not enough trainning datapoints to generate spectra / peptide classification model");
                //There are no negatives! all are positives
                foreach (SQTScan s in myScans)
                {
                    //We do not provide maximum Bayesian Score so we can let other good spectra provide good examples for the protein classifier
                    s.BayesianScore = 1;
                    s.BayesianClass = 1;
                }
            }
            else
            {

                gd.ClassLableClassPackageDic[-1].Prior = 0.5;
                gd.ClassLableClassPackageDic[1].Prior = 0.5;

                //Parallel.ForEach(thePath.MyScans, s =>
                foreach (SQTScan s in myScans)
                {
                    //The result is ordered by class number

                    if (gd.MySparseMatrix.theMatrixInRows[0].Values.Count < s.Bayes_InputVector.Count)
                    {
                        foreach (int i in unstableDims)
                        {
                            s.Bayes_InputVector.RemoveAt(i - 1);
                        }
                    }

                    if (myParams.FormsPriorForPeptides  && considerForms)
                    {
                        //calculate a penalty for the negative class


                        gd.ClassLableClassPackageDic[-1].Prior = peptidePriors[s.NoForms];
                        gd.ClassLableClassPackageDic[1].Prior = 1-peptidePriors[s.NoForms];
                    }

                    var results = gd.Classify(s.Bayes_InputVector.ToArray());

                    double BayesianDiference = results[0].Score - results[1].Score;

                    

                    s.BayesianScore = BayesianDiference;

                    s.BayesianClass = 1;
                    if (results[0].Score < results[1].Score)
                    {
                        s.BayesianClass = -1;
                    }

                }
                //);

                double BayesianMin = myScans.Min(a => a.BayesianScore);
                double BayesianMax = myScans.Max(a => a.BayesianScore);
                double BayesianDif = BayesianMax - BayesianMin;

                foreach (SQTScan s in myScans)
                {
                    s.BayesianScore = (s.BayesianScore - BayesianMin) / BayesianDif;
                }

                Console.WriteLine("Class modeled");


            }

            return gd.MySparseMatrix;
        }

        

        

    }
}
