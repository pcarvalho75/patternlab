using System;
using System.Collections.Generic;
using System.Text;
using PatternTools;

namespace parserGUI
{
    public class XFoldCalculations
    {

        SparseMatrix sm;

        public XFoldCalculations (SparseMatrix sparseMatrix) {
            this.sm = sparseMatrix;
        }

        /// <summary>
        /// Used for detecting differential expression in pair-wise experiments
        /// To be selected as differentialy expressed, the event should satisfy 
        /// buth criteria. In case of a timecourse, make sure t0 is the positive class
        /// Make sure we have a shattered matrix, before using this method
        /// As for the normalization method; 1 = rowSigma; 2 = TSC; 3 = none
        /// </summary>
        /// 


        public List<XFoldStruct> Xfold(int normalizationMethod, bool useAC, double lowestAcceptablePValue)
        {
            List<sparseMatrixRow> theMatrix = sm.theMatrixInRows;

            List<XFoldStruct> XFoldScores = new List<XFoldStruct>();
            pTools pTool = new pTools();

            //Find out which index belongs to the poitive iv and the negative iv
            int pi = 0; //positive index
            int ni = 1;
            if (theMatrix[0].Lable < 0)
            {
                pi = 1;
                ni = 0;
            }


            if (useAC)
            {
                //Make sure we have a shattered Matrix
                if (theMatrix.Count > 2)
                {
                    Exception myException = new Exception("Are you sure this is a shattered matrix?");
                    throw (myException);
                }

                //Find TSC for + and -
                double tscp = 0; //Total spectral counts positive
                double tscn = 0; //Total spectral counts negative
                double meanp = 0; //mean of the pos vector
                double meann = 0; //mean of the neg vector
                double sigmap = 0; //std of the pos vector
                double sigman = 0; //std of the neg vector

                foreach (sparseMatrixRow r in theMatrix)
                {
                    if (r.Lable > 0)
                    {
                        tscp = r.totalValuesCount();
                        meanp = tscp / r.Dims.Count;
                        sigmap = Math.Sqrt(PatternTools.pTools.variance(r.Values, false));

                    }
                    else
                    {
                        tscn = r.totalValuesCount();
                        meann = tscn / r.Dims.Count;
                        sigman = Math.Sqrt(PatternTools.pTools.variance(r.Values, false));
                    }
                }

                //Eliminate any tagged dims
                sm.EliminateNegativeDims();
                List<int> dims = sm.allDims();

                for (int i = 0; i < dims.Count; i++)
                {
                    //Find out fold Change

                    double thePosValue = theMatrix[pi].getValueForDim(dims[i]);
                    double theNegValue = theMatrix[ni].getValueForDim(dims[i]);
                    double thisFoldChange = 0;
                    double normalizedSpecountPos = thePosValue;
                    double normalizedSpecCountNeg = theNegValue;

                    if (normalizationMethod == 1)
                    {
                        //Row Sigma
                        //MessageBox.Show(meann.ToString()+ " "+ meanp.ToString() + " "+ sigman.ToString()+ " "+ sigmap.ToString()  );
                        normalizedSpecCountNeg = theNegValue / (meann + (3 * sigman));
                        normalizedSpecountPos = thePosValue / (meanp + (3 * sigmap));

                    }
                    else if (normalizationMethod == 2)
                    {
                        //TSC
                        normalizedSpecCountNeg = theNegValue / tscn;
                        normalizedSpecountPos = thePosValue / tscp;
                    }

                    thisFoldChange = (normalizedSpecCountNeg) / (normalizedSpecountPos);


                    //Find out pValue
                    double totalSCL = tscp;
                    double totalSCS = tscn;
                    double theLargeValue = thePosValue;
                    double theSmallValue = theNegValue;

                    if (theNegValue > thePosValue)
                    {
                        totalSCL = tscn;
                        totalSCS = tscp;
                        theLargeValue = theNegValue;
                        theSmallValue = thePosValue;
                    }


                    //double thisPValue = pTool.ACTest(tscp, tscn, thePosValue, theNegValue);
                    double thisPValue = 0.5;

                    try
                    {
                        thisPValue = pTool.ACTest(totalSCL, totalSCS, theLargeValue, theSmallValue);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    XFoldStruct s = new XFoldStruct();
                    s.FoldChange = thisFoldChange;
                    s.pValue = thisPValue;
                    s.QuantitationPositiveClass = thePosValue;
                    s.QuantitationNegativeClass = theNegValue;
                    s.GPI = dims[i];

                    XFoldScores.Add(s);

                }

            }
            else
            {

                //we will use the t test
                //lets use the t test

                //First lets normalize
                SparseMatrix smUnnormalized = sm;

                if (normalizationMethod == 1)
                {
                    sm.rowSigmaNormalization(3);
                }
                else if (normalizationMethod == 2)
                {
                    sm.totalSignalNormalization();
                }
                List<int> labels = sm.ExtractLabels();

                sm.EliminateNegativeDims();
                List<int> dims = sm.allDims();


                if (normalizationMethod != 2)
                {


                    int count = 0;
                    List<double> standardDVs = new List<double>();
                    foreach (int myDim in dims)
                    {

                        count++;
                        List<double> thePosNormalized = sm.ExtractDimValues(myDim, 1, true);
                        List<double> theNegNormalized = sm.ExtractDimValues(myDim, -1, true);
                        List<double> thePosUnNormalized = smUnnormalized.ExtractDimValues(myDim, 1, true);
                        List<double> theNegUnNormalized = smUnnormalized.ExtractDimValues(myDim, -1, true);

                        if (thePosNormalized.Count >= 2)
                        {
                            double stdev = PatternTools.pTools.Stdev(theNegNormalized, true);
                            if (stdev > 0)
                            {
                                standardDVs.Add(stdev);
                            }
                        }

                        if (theNegNormalized.Count >= 2)
                        {
                            double stdev = PatternTools.pTools.Stdev(theNegNormalized, true);
                            if (stdev > 0)
                            {
                                standardDVs.Add(stdev);
                            }
                        }

                        if (count > 500)
                        {
                            break;
                        }
                    }

                    //make the list non redundant
                    List<double> nonredundantSVar = new List<double>();
                    foreach (double n in standardDVs)
                    {
                        if (!nonredundantSVar.Contains(n))
                        {
                            nonredundantSVar.Add(n);
                        }
                    }
                    nonredundantSVar.Sort();
                    standardDVs.Sort();
                }

                foreach (int myDim in dims)
                {

                    List<double> thePosNormalized = sm.ExtractDimValues(myDim, 1, true);
                    List<double> theNegNormalized = sm.ExtractDimValues(myDim, -1, true);
                    List<double> thePosUnNormalized = smUnnormalized.ExtractDimValues(myDim, 1, true);
                    List<double> theNegUnNormalized = smUnnormalized.ExtractDimValues(myDim, -1, true);

                    //fold change;
                    double pvalue = 0;
                    double thisFoldChange = 0;


                    if (labels.Count == 2)
                    {
                        double posAverage = pTools.Average(thePosNormalized);
                        if (thePosNormalized.Count == 0)
                        {
                            posAverage = 1;
                        }
                        thisFoldChange = pTools.Average(theNegNormalized) / posAverage;

                        List<double> pvalues = new List<double>() { 1 };

                        double bothTails;
                        double leftTail;
                        double rightTail;

                        alglib.studentttest2(thePosNormalized.ToArray(), thePosNormalized.Count, theNegNormalized.ToArray(), theNegNormalized.Count, out bothTails, out leftTail, out rightTail);

                        pvalue = bothTails / 2;


                    }
                    else if (labels.Count == 1)
                    {
                        thisFoldChange = pTools.Average(theNegNormalized);

                        double bothTails;
                        double leftTail;
                        double rightTail;

                        alglib.studentttest1(theNegNormalized.ToArray(), theNegNormalized.Count, 1, out bothTails, out leftTail, out rightTail);

                        pvalue = bothTails / 2;
                    }



                    XFoldStruct s = new XFoldStruct();
                    s.FoldChange = thisFoldChange;
                    s.pValue = pvalue;

                    s.QuantitationPositiveClass = PatternTools.pTools.Average(thePosUnNormalized);
                    s.QuantitationNegativeClass = PatternTools.pTools.Average(theNegNormalized);

                    s.GPI = myDim;
                    if (s.QuantitationPositiveClass > 0 && s.QuantitationNegativeClass > 0)
                    {
                        XFoldScores.Add(s);
                    }
                }

            }

            foreach (var x in XFoldScores)
            {
                if (x.pValue < lowestAcceptablePValue)
                {
                    x.pValue = lowestAcceptablePValue;
                }
            }


            return (XFoldScores);

        }

    }
}
