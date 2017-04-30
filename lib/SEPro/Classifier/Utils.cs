using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;
using System.IO;
using PatternTools.SQTParser;
using SEPRPackage;

namespace SEProcessor.Classifier
{
    public static class Utils
    {

        public static void GenerateSparseMatrix(List<SQTScan> myScans, Parameters myParams, List<sparseMatrixRow> theRows, bool considerPresence, bool considerForms)
        {
            //Find out how many dims we are dealing with
            List<int> dims = new List<int>();
            int dimCounter = 0;
            if (myParams.CompositeScoreDeltaCN)
            {
                dimCounter++;
                dims.Add(dimCounter);
            }

            if (myParams.CompositScoreDigestion)
            {
                dimCounter++;
                dims.Add(dimCounter);
            }

            if (myParams.CompositeScorePeaksMatched)
            {
                dimCounter++;
                dims.Add(dimCounter);
            }

            if (myParams.CompositeScoreDeltaMassPPM)
            {
                dimCounter++;
                dims.Add(dimCounter);
            }

            if (myParams.CompositeScorePresence && considerPresence)
            {
                dimCounter++;
                dims.Add(dimCounter);
            }

            //if (myParams.CompositeScoreForms && considerForms)
            //{
            //    dimCounter++;
            //    dims.Add(dimCounter);
            //}

            if (myParams.CompositeScorePrimaryScore)
            {
                dimCounter++;
                dims.Add(dimCounter);
            }

            if (myParams.CompositeScoreSecondaryScore)
            {
                dimCounter++;
                dims.Add(dimCounter);
            }

            if (myParams.CompositeScoreSecondaryRank)
            {
                dimCounter++;
                dims.Add(dimCounter);
            }



            foreach (SQTScan s in myScans)
            {

                double label = 1;
                //if (s.NumberForwardNames == 0)
                if (s.CountNumberForwardNames(myParams.LabeledDecoyTag) == 0)
                {
                    label = -1;
                }

                s.Bayes_InputVector = new List<double>(dims.Count);

                if (myParams.CompositeScoreDeltaCN)
                {
                    s.Bayes_InputVector.Add(s.Bayes_DeltaCNScore);
                }

                if (myParams.CompositScoreDigestion)
                {
                    s.Bayes_InputVector.Add(s.Bayes_EnzymeEfficiencyScore);
                }

                if (myParams.CompositeScorePeaksMatched)
                {
                    s.Bayes_InputVector.Add(s.Bayes_NumPeaksMatchedScore);
                }

                if (myParams.CompositeScoreDeltaMassPPM)
                {
                    s.Bayes_InputVector.Add(s.Bayes_PPMScore);
                }

                if (myParams.CompositeScorePresence && considerPresence)
                {
                    s.Bayes_InputVector.Add(s.Bayes_PresenceScore);
                }

                if (myParams.CompositeScorePrimaryScore)
                {
                    s.Bayes_InputVector.Add(s.Bayes_PrimaryScore);
                }

                if (myParams.CompositeScoreSecondaryScore)
                {
                    s.Bayes_InputVector.Add(s.Bayes_SecondaryScore);
                }

                if (myParams.CompositeScoreSecondaryRank)
                {
                    s.Bayes_InputVector.Add(s.Bayes_SecondaryRankScore);
                }

                theRows.Add(new sparseMatrixRow((int)label, dims, s.Bayes_InputVector.ToList()));

            }
        }
    }
}
