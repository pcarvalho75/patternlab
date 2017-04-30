using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using PatternTools;

namespace GO
{
    public class TermScoreCalculator
    {
        GOTerms gt;
        ResultParser resultParser;
        List<TermScoreAnalysis> termScoreAnalysisCache = new List<TermScoreAnalysis>();
        List<Term> topMostNodes = new List<Term>();
        Dictionary<string, List<string>> resultsInPopulationDict = new Dictionary<string, List<string>>();


        public void ClearTermScoreAnalysisCache()
        {
            termScoreAnalysisCache.Clear();
            BuildResultsInPopulationCache();
            
        }

        public void BuildResultsInPopulationCache()
        {
            resultsInPopulationDict.Clear();

            //--------Modification cav November 5 2008 to speed up calculations by not having to rebuild the terms found in the population for every term
            //Lets cache the found results within each population,
            //First we detect the root nodes
            //Find the root term and get all nodes below it
            List<string> population = topMostNodes[0].AllNodesBelow;
            for (int i = 0; i < topMostNodes.Count; i++)
            {
                resultsInPopulationDict.Add(topMostNodes[i].nameSpace, new List<string>());
            }

            //Now, we go through each one of the nodes and add it to the according population
            foreach (KeyValuePair<string, double> kvp in resultParser.TheResults)
            {
                List<string> translatedTermList = resultParser.TranslatedResults[kvp.Key];

                foreach (KeyValuePair<string, List<string>> kvpResDict in resultsInPopulationDict)
                {
                    foreach (string translatedTerm in translatedTermList)
                    {

                        if (!kvpResDict.Value.Contains(translatedTerm))
                        {
                            kvpResDict.Value.Add(translatedTerm);
                            break;
                        }

                    }

                }

            }

        }
        
        public TermScoreCalculator(ref GOTerms gt, ref ResultParser rp)
        {
            this.gt = gt;
            resultParser = rp;

            //Find root term for the 3 GO namespaces
            //Molecular function, Celular Component, Biological Process
            List<string> topMostNodesIDs = gt.findTopMostNodes();
            foreach (string s in topMostNodesIDs)
            {
                Term termToAdd = gt.getTermByID(s);
                topMostNodes.Add(termToAdd);
            }

            BuildResultsInPopulationCache();

        }


        //----------------------------

        public TermScoreAnalysis calculate(Term analyzedTerm, bool restrictToLeafs, bool speedMode)
        {

            //Check if we have it in the cache
            if (!speedMode)
            {
                if (termScoreAnalysisCache.Exists((p) => p.Term.Equals(analyzedTerm.id) && p.ResultRestrictedToLeafs.Equals(restrictToLeafs)))
                {
                    return (termScoreAnalysisCache.Find((p) => p.Term.Equals(analyzedTerm.id)));
                }
            }

            //If we dont have it in the cache, lets work!
            double foldChange = 0;
            double absoluteFoldChange = 0;

            //We will need
            //Step 1: (m) size of population set (all the leafs from the namespace)
            //Step 2: (n) size of study set (all the leafs from the term)
            //Step 3: (mt) Number of genes annotaded to the study term
            //Step 4: (number of draws) how many results were found present in the population
            //Step 5: Calculate the cumulative hypegeo

            //Step 1: Population Set Size

            //We need to find out from which name space the Term belongs to
            //Then, we must find the number of leafs for that specific population
            //This resumes to finding the leafs for the roots of the DAG

            List<string> population;
            List<string> studySet;

            //Step 2: The Study Set
            if (restrictToLeafs)
            {
                studySet = gt.GetLeafs(analyzedTerm);
                population = gt.LeafsOfCategories[analyzedTerm.nameSpace];
            }
            else
            {
                studySet = analyzedTerm.AllNodesBelow;

                //We need to assign population to something

                //Find the root term and get all nodes below it
                population = topMostNodes[0].AllNodesBelow;
                for (int i = 1; i < topMostNodes.Count; i++)
                {
                    if (analyzedTerm.nameSpace.Equals(topMostNodes[i].nameSpace))
                    {
                        population = topMostNodes[i].AllNodesBelow;
                    }
                }
            }


            //Step 3:  Number of results annotaded to the study set
            //Step 4:  Number of results in the population
            List<string> resultsInStudySet = new List<string>();
            //List<string> resultsInPopulation = new List<string>();

            List<string> resultsInPopulation = new List<string>();
            if (resultsInPopulationDict.ContainsKey(analyzedTerm.nameSpace))
            {
                resultsInPopulation = resultsInPopulationDict[analyzedTerm.nameSpace];
            }

            foreach (KeyValuePair<string, double> kvp in resultParser.TheResults)
            {

                List<string> translatedTermList = resultParser.TranslatedResults[kvp.Key];

                foreach (string translatedTerm in translatedTermList)
                {
                    if (studySet.Contains(translatedTerm))
                    {
                        resultsInStudySet.Add(kvp.Key + "(" + Math.Round(kvp.Value, 1) + ")");
                        foldChange += resultParser.TheResults[kvp.Key];
                        absoluteFoldChange += Math.Abs(resultParser.TheResults[kvp.Key]);
                        break;
                    }

                }

                if (resultsInStudySet.Count >= studySet.Count)
                {
                    break;
                }
            }


            //

            //Step 5 Calculate the cumulative hypeGeo
            //N all marbles in the urn
            //D the number of white (success marbles)
            //n number of draws
            //k number of success in your draw


            //We are interested in knowing the probability of seeing (the number of genes annotaded to the study set)-> nt, or more
            //we must sum from nt to the maximum number of possible annotations
            //in a method equivalent to the one sided Fisher exact.

            //Find minimum of (no. genes anotated to term) and (no. genes in study set)
            double cummulativeHypeGeoProb = 0;
            int populationC = population.Count;
            int resultsInPopulationC = resultsInPopulation.Count;
            int studySetC = studySet.Count;

            //In speedMode, lets only consider those with probability superior to 90%
            if (resultsInStudySet.Count == 0)
            {
                cummulativeHypeGeoProb = 0;
            }
            else
            {

                int kc = 0;

                //According to the manuscript, we want to calculate the probability of 
                //observing less than k, therefore we will use < and not <=
                try
                {
                    if (studySetC == populationC)
                    {
                        cummulativeHypeGeoProb = 1;
                    }
                    else
                    {
                        //A necessary correction to make the hypergeometric valid
                        if (studySetC + resultsInPopulationC > populationC && studySetC != populationC)
                        {
                            kc = populationC + resultsInPopulationC - studySetC;
                        }
                        //And we also need to make sure about this
                        int numberOfResultsInStudySet = resultsInStudySet.Count;
                        if (numberOfResultsInStudySet > studySet.Count)
                        {
                            numberOfResultsInStudySet = studySet.Count;
                        }

                        //This procedure substitutes the procedure comented above
                        double denominator = HyperGeometric.hypeGeoDenominator(populationC, studySetC);
                        for (int k = kc; k < numberOfResultsInStudySet; k++)
                        {
                            cummulativeHypeGeoProb += Math.Exp(HyperGeometric.hypeGeoSuminNumerator(k, populationC, resultsInPopulationC, studySetC) - denominator);
                        }
                    }
                }
                catch
                {
                    string answer = "Skipping hypergeometric calculations for term slipping term..\n PopulationC " + populationC;
                    answer += " studySetC " + studySet + " studysetc " + studySetC;
                    answer += "cumulativehypegeo = " + cummulativeHypeGeoProb;
                    answer += "populationc = " + populationC;
                    MessageBox.Show(answer);
                    cummulativeHypeGeoProb = 0;
                }
            }

            //Just to make sure
            double cumulativeHypeGeoCorrected = 1 - cummulativeHypeGeoProb;
            if (cumulativeHypeGeoCorrected < 0.000000001)
            {
                cumulativeHypeGeoCorrected = 0.000000001;
            }

            TermScoreAnalysis tsa = new TermScoreAnalysis();
            tsa.Term = analyzedTerm.id;
            tsa.TermName = analyzedTerm.name;
            tsa.Namespace = analyzedTerm.nameSpace;
            tsa.Description = analyzedTerm.definition;
            tsa.CummulativeHypeGeo = cumulativeHypeGeoCorrected;
            tsa.IdentifiedTermsInPopulation = resultsInPopulation.Count;
            tsa.IdentifiedTermsInStudySet = resultsInStudySet.Count;
            tsa.PopulationSize = population.Count;
            tsa.StudySetSize = studySet.Count;
            tsa.FoldChange = foldChange;
            tsa.AbsoluteFoldChange = absoluteFoldChange;
            tsa.ResultRestrictedToLeafs = restrictToLeafs;

            Dictionary<string, string> identifiedProteins = new Dictionary<string, string>();

            for (int i = 0; i < resultsInStudySet.Count; i++)
            {
                //the IPI
                string theIPI = resultsInStudySet[i];
                string description = "";

                //Clean everything after the last period.
                if (theIPI.StartsWith("IPI"))
                {
                    theIPI = Regex.Replace(theIPI, @"\..*", "");
                }


                //Get description
                ResultParser.ProteinInfo theProteinInfo = resultParser.MyProteins.Find(a => a.ID.Equals( theIPI  ));
                
                try
                {
                    description = theProteinInfo.Description;
                }
                catch 
                {
                    //the protein info is probably null
                    description = "";
                }

                identifiedProteins.Add(resultsInStudySet[i], description);

                tsa.ProteinIDsAndFold += resultsInStudySet[i] + " ";
                if (description.Length > 0) {
                    tsa.ProteinIDsAndFold += "(" + description + ")\n";
                }
            }


            tsa.ProteinIDs = identifiedProteins;

            //Add it to the cache
            lock (termScoreAnalysisCache)
            {
                if (!termScoreAnalysisCache.Exists((p) => p.Term.Equals(analyzedTerm.id)))
                {
                    termScoreAnalysisCache.Add(tsa);
                }
            }

            return (tsa);

        }




        //----------------------------

        public struct TermScoreAnalysis
        {

            public string Term { get; set; }
            //Data about the term
            public string TermName { get; set; }
            public string Namespace { get; set; }
            public string Description { get; set; }
            public double CummulativeHypeGeo { get; set; }
            //-----

            public int IdentifiedTermsInPopulation { get; set; }
            public int IdentifiedTermsInStudySet { get; set; }


            public bool ResultRestrictedToLeafs { get; set; }

            /// <summary>
            /// All fold changes are taken as absolute values
            /// </summary>
            public double AbsoluteFoldChange { get; set; }


            /// <summary>
            /// Differently than the absolute fold change
            /// this value is obtained by summing all fold changes
            /// both positive and negative
            /// </summary>
            public double FoldChange { get; set; }

            /// <summary>
            /// leafs in the namespace
            /// </summary>
            public int PopulationSize { get; set; }

            /// <summary>
            /// leafs in the term
            /// </summary>
            public int StudySetSize {get; set;}

            public string ProteinIDsAndFold {get; set;}

            /// <summary>
            /// ID, description
            /// </summary>
            public Dictionary<string, string> ProteinIDs { get; set; }

        }
    }
}
