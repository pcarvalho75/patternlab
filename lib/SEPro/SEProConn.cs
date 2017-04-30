using PatternTools;
using PatternTools.FastaParser;
using PatternTools.SQTParser;
using SEPRPackage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProcessor
{
    public class SEProConn
    {
        public ResultPackage myResults { get; set; }

        public SEProConn(Parameters myParams, List<SQTScan> allScans, FastaFileParser fastaParser, bool proteinLogic, bool includeMS2)
        {
            myParams.FormsPriorForPeptides = false;

            //----------------------------------------------------------

            //Lets throw in some quality filters to remove the junk
            Processor.ApplyQualityFilters(myParams, allScans);


            //Now lets eliminate PSMs of sequences whose decoys match the targets
            int eliminated = Processor.EliminateDecoysPSMSequencesThatMatchTargets(allScans, myParams);
            Console.WriteLine("Decoy scans eliminated for sharing sequences with targets: " + eliminated);


            //Sequence Test----------------------------------------------------------------------------

            PeptideAnalysisPckg analysisFDR = new PeptideAnalysisPckg(allScans, myParams);
            


            //to test the enzyme 
            EnzymeEvaluator ev = new EnzymeEvaluator();
            foreach (SQTScan s in allScans)
            {
                s.NoEnzymeCleavages = ev.NoEnzymaticCleavages(s.PeptideSequence, myParams.MyEnzime).Count;
            }


            ////Lets make the path
            List<Path> myPaths = new List<Path>();

            if (myParams.GroupByNoEnzymaticTermini)
            {

                if (myParams.QFilterMinNoEnzymaticTermini <= 0)
                {
                    Path p0 = new Path("Zero enzymatic termini");
                    p0.MyScans = allScans.FindAll(a => a.NoEnzymeCleavages == 0);
                    if (p0.MyScans.Count > 0)
                    {
                        myPaths.Add(p0);
                    }
                }

                if (myParams.QFilterMinNoEnzymaticTermini <= 1)
                {

                    Path p1 = new Path("One enzymatic termini");
                    p1.MyScans = allScans.FindAll(a => a.NoEnzymeCleavages == 1);
                    if (p1.MyScans.Count > 0)
                    {
                        myPaths.Add(p1);
                    }
                }

                if (myParams.QFilterMinNoEnzymaticTermini <= 2)
                {
                    Path p2 = new Path("Two enzimatic termini");
                    p2.MyScans = allScans.FindAll(a => a.NoEnzymeCleavages == 2);
                    if (p2.MyScans.Count > 0)
                    {
                        myPaths.Add(p2);
                    }
                }
            }
            else
            {
                Path p3 = new Path("No Enzymatic Termini Grouping");
                p3.MyScans = allScans;
                myPaths.Add(p3);
            }


            if (myParams.GroupByChargeState)
            {
                List<Path> tmpPaths = new List<Path>();

                foreach (Path p in myPaths)
                {
                    tmpPaths.Add(new Path(p.Name + "_1", p.MyScans.FindAll(a => a.ChargeState == 1)));
                    tmpPaths.Add(new Path(p.Name + "_2", p.MyScans.FindAll(a => a.ChargeState == 2)));
                    tmpPaths.Add(new Path(p.Name + "_>=3", p.MyScans.FindAll(a => a.ChargeState >= 3)));
                }

                myPaths = tmpPaths;
            }

            foreach (Path p in myPaths)
            {
                if (p.MyScans.Count > 0)
                {
                    //Filtering at spectral level
                    p.CalculateScoresForGeneralClassification(myParams);
                }
            }


            //The unified path array - used below
            KeepTrack trackLevel2 = new KeepTrack("Level 2");
            KeepTrack trackLevel3 = new KeepTrack("Level 3");

            bool quitAnalysis = false;

            Dictionary<int, double> dicPriorsForPeptidesForms = new Dictionary<int, double>();
            if (myParams.FormsPriorForPeptides)
            {
                dicPriorsForPeptidesForms = MainControl.EstablishPriorsForPeptideClassification(allScans, myParams);
            }

            //Process all paths at the spectral level
            foreach (Path pathSpectra in myPaths)
            {
                // this will turn off the filtering at the peptide level, for this, use -1 for peptide FDR
                if (myParams.PeptideFDR == -0.01)
                {
                    return;
                }

                Console.WriteLine("Processing path: " + pathSpectra.Name + " Total scans = " + pathSpectra.MyScans.Count);
                if (pathSpectra.MyScans.Count > 15)
                {
                    //Bayesian Modeling
                    Console.WriteLine("Generating classification model");


                    //Generate spectral classification score
                    try
                    {
                        Classifier.ClassifierScoring.BayesianScoringPSM(pathSpectra.MyScans, myParams, false, false, dicPriorsForPeptidesForms);
                        pathSpectra.MyScans.Sort((a, b) => b.BayesianScore.CompareTo(a.BayesianScore));

                        int myBadExamples = pathSpectra.MyScans.Count - pathSpectra.MyScans.FindAll(a => a.CountNumberForwardNames(myParams.LabeledDecoyTag) > 0).Count;

                        if (pathSpectra.MyScans.Min(a => a.BayesianScore) == 1)
                        {

                            Console.WriteLine("Not enough spectra in decoy or target class to make robust statistics.\nMaybe, try turning off the outlier filter or making bigger groups by decreasing group stringency could yield better results (e.g., use only enzimatic separation or only charge state separation or none).\nError happened for test: " + pathSpectra.Name + "\n\nANALYSIS WILL BE DISCONTINUED.");
                            quitAnalysis = true;
                            break;
                        }
                    }
                    catch (Exception e10)
                    {
                        Console.WriteLine("****Error -> Dynamic Filtering at spectra / peptide level not performed" + e10.Message);
                    }


                    //First lets find out how many good and bad spectra we have at the spectra level
                    Console.WriteLine("Calculating spectra FDR for path: " + pathSpectra.Name);


                    analysisFDR = new PeptideAnalysisPckg(pathSpectra.MyScans, myParams);

                    //Find the point at which we satisfy the FDR
                    if (myParams.SpectraFDR > -0.01)
                    {
                        int cutoffValueSpectra = Processor.FindCutoffSpectralLevel(analysisFDR, myParams, pathSpectra.MyScans);

                        if (cutoffValueSpectra > 0)
                        {
                            pathSpectra.MyScans.RemoveRange(cutoffValueSpectra, pathSpectra.MyScans.Count - cutoffValueSpectra);
                        }

                        analysisFDR = new PeptideAnalysisPckg(pathSpectra.MyScans, myParams);

                        //Keep global tracking
                        trackLevel2.AccumulateResults(analysisFDR);
                    }
                }
            }


            //We will keep these scans to later insert them once we have acerted what are the good proteins in the mixture
            // these are good quality scans where we could still fish out a couple of peptides or scns that got lost.
            List<SQTScan> scansAcceptedAfterSpectralCleaning = (from p in myPaths
                                                                from sqt in p.MyScans
                                                                select sqt).ToList();


            //Process all paths at the peptide level
            //As a first step, we need to establish priors for a given number o charge states
            List<PeptideAnalysisPckg> separatePathAnalysis = new List<PeptideAnalysisPckg>();
            foreach (Path pathPeptide in myPaths)
            {

                PeptideManager pm = new PeptideManager(pathPeptide.MyScans, myParams);
                double thisFDR = pm.FDR;


                if (pm.NoLabeledDecoyPeptides > 25 && thisFDR > myParams.PeptideFDR)
                {
                    if (myParams.CompositeScorePresence)
                    {
                        pm.CalculateSpectralPresenceScore();
                    }

                    //Score the spectra
                    Classifier.ClassifierScoring.BayesianScoringPSM(pathPeptide.MyScans, myParams, myParams.CompositeScorePresence, myParams.FormsPriorForPeptides, dicPriorsForPeptidesForms);

                }

                pm.CleanToMeetFDR(myParams.PeptideFDR);
                pathPeptide.MyScans = pm.MyScans;


                //UpdateStatistics
                PeptideAnalysisPckg thisFDRAnalysis = new PeptideAnalysisPckg(pathPeptide.MyScans, myParams);
                trackLevel3.AccumulateResults(thisFDRAnalysis);
                separatePathAnalysis.Add(thisFDRAnalysis);


            }

            //This should clean up to only confident scans and free up RAM
            allScans = (from pckg in separatePathAnalysis
                        from scn in pckg.AllScans
                        select scn).Distinct().ToList();

            if (quitAnalysis) { return; }



            Console.WriteLine("Starting protein Manager");



            //Prolucid does sometimes report peptides to be unique when they are not.
            //this sub check and corrects for this
            //Needs to be updated if new scan fisher becomes active
            if (true)
            {
                Console.WriteLine("Double-checking search engine's protein mappings");
                //We first need to build a dictionary of all peptides and their mappings
                List<string> allPeptides = (from peppkg in separatePathAnalysis
                                            from sequence in peppkg.CleanedPeptides
                                            select sequence).Distinct().ToList();

                List<SQTScan> allScansInAllPaths = (from peppkg in separatePathAnalysis
                                                    from scn in peppkg.AllScans
                                                    select scn).Distinct().ToList();

                Dictionary<string, List<string>> peptideMappingsDictionary = new Dictionary<string, List<string>>();
                foreach (string peptide in allPeptides)
                {
                    List<string> IDs = (from fItem in fastaParser.MyItems.AsParallel()
                                        where fItem.Sequence.Contains(peptide)
                                        select fItem.SequenceIdentifier).ToList();
                    peptideMappingsDictionary.Add(peptide, IDs);
                }

                //And now correct all the scans
                foreach (SQTScan s in allScansInAllPaths)
                {
                    string seq = PatternTools.pTools.CleanPeptide(s.PeptideSequence, true);
                    s.IdentificationNames = peptideMappingsDictionary[seq];
                }
            }


            Console.WriteLine("Instantiating protein manager");
            ProteinManager myProteins = new ProteinManager(allScans, myParams, new List<string>());
            List<SQTScan> scansThatCouldBeFished = scansAcceptedAfterSpectralCleaning.Except(myProteins.AllSQTScans).ToList();


            //------ 
            Console.WriteLine("Applying protein quality filters");

            if (myParams.QFilterDeltaMassPPM && (myParams.DeltaMassPPMPostProcessing < myParams.QFilterDeltaMassPPMValue))
            {
                //We will remove the proteins with a delta ppm from the average ppm
                double averagePPMOrbitrap = allScans.Average(a => a.PPM_Orbitrap);

                scansThatCouldBeFished.RemoveAll(a => Math.Abs(averagePPMOrbitrap - a.PPM_Orbitrap) > myParams.DeltaMassPPMPostProcessing);
                int ptnsRemoved = myProteins.AllSQTScans.RemoveAll(a => Math.Abs(averagePPMOrbitrap - a.PPM_Orbitrap) > myParams.DeltaMassPPMPostProcessing);
                myProteins.RebuildProteinsFromScans();
            }

            bool needsToRebuildFromScans = false;

            if (myParams.QFilterDiscardProteinsWithNoUniquePeptides)
            {
                int ptnsRemoved = myProteins.MyProteinList.RemoveAll(a => a.ContainsUniquePeptide == 0);
                needsToRebuildFromScans = true;
                Console.WriteLine("Proteins removed for not having unique peptides: " + ptnsRemoved);
            }



            if (myParams.QFilterMinSpecCount > 1)
            {
                int ptnsRemoved = myProteins.MyProteinList.RemoveAll(a => a.Scans.Count < myParams.QFilterMinSpecCount);
                //and now update the peptide count and scan count

                Console.WriteLine("Proteins removed for having less than " + myParams.QFilterMinSpecCount + " spec counts: " + ptnsRemoved);
                needsToRebuildFromScans = true;
            }

            if (myParams.QFilterMinNoStates > 1)
            {
                int ptnsRemoved = myProteins.MyProteinList.RemoveAll(a => a.NoStates < myParams.QFilterMinNoStates);
                Console.WriteLine("Proteins removed for having less than " + myParams.QFilterMinNoStates + " states: " + ptnsRemoved);
                needsToRebuildFromScans = true;
            }

            if (myParams.QFilterProteinsMinNoPeptides > 1)
            {
                int ptnsRemoved = myProteins.MyProteinList.RemoveAll(a => a.SequenceCount < myParams.QFilterProteinsMinNoPeptides);
                needsToRebuildFromScans = true;
                Console.WriteLine("Proteins removed for having less then " + myParams.QFilterMinNoStates + " sequences: " + ptnsRemoved);
            }


            if (myParams.QFilterMinPrimaryScoreForOneHitWonders > 0)
            {
                List<MyProtein> toRemove = new List<MyProtein>();
                foreach (MyProtein p in myProteins.MyProteinList)
                {
                    if (p.SequenceCount == 1 && p.Scans[0].TailScore < myParams.QFilterMinPrimaryScoreForOneHitWonders)
                    {
                        toRemove.Add(p);
                    }
                }

                myProteins.MyProteinList = myProteins.MyProteinList.Except(toRemove).ToList();
                myProteins.RebuildScansFromModifiedProteinList();
            }

            if (needsToRebuildFromScans)
            {
                myProteins.RebuildProteinsFromScans();

                //Eliminate peptides that are not found in the remaining proteins
                myProteins.AllSQTScans = (from prot in myProteins.MyProteinList
                                          from pep in prot.PeptideResults
                                          from scan in pep.MyScans
                                          select scan).Distinct().ToList();
            }




            //Calculate protein coverage
            Console.WriteLine("Calculating protein coverage");
            myProteins.ProteinDB = myParams.AlternativeProteinDB;
            myProteins.CalculateProteinCoverage(fastaParser.MyItems.ToList());


            //Apply the protein discriminant function
            Console.WriteLine("Bayesian cleaning at protein level");
            if (myParams.ProteinFDR > -0.01)
            {
                try
                {
                    myProteins.BayesianCleaningAtProteinLevel();
                }
                catch (Exception e6)
                {
                    Console.WriteLine("****Error -> Protein level filtering disconsidered: " + e6.Message);
                }
            }



            if (proteinLogic)
            {
                Console.WriteLine("Activating protein logic");
                //we now need to find those that map to a protein
                List<string> theLocci = myProteins.MyProteinList.Select(a => a.Locus).Distinct().ToList();

                List<SQTScan> fishedScans = scansThatCouldBeFished.FindAll(a => a.IdentificationNames.Intersect(theLocci).Count() > 0).ToList();
                Console.WriteLine(" " + fishedScans.Count + " scans recovered.");

                myProteins = new ProteinManager(myProteins.AllSQTScans.Concat(fishedScans).ToList(), myParams, theLocci);
                myProteins.CalculateProteinCoverage(fastaParser.MyItems.ToList());

            }


            //Establish protein groups
            myProteins.GroupProteinsHavingCommonPeptides(myParams.GroupPtnsWithXCommonPeptides);

            //--------------------------------------------------------------------
            if (myProteins.AllSQTScans.Count > 0)
            {
                if (includeMS2 && myProteins.AllSQTScans[0].MSLight == null)
                {
                    try
                    {
                        Console.WriteLine("Loading MS2");
                        MainControl.AttributeSpectraToSQT(myProteins.AllSQTScans, myParams.SeachResultDirectoy);
                        Console.WriteLine("Done loading MS2");
                    }
                    catch (Exception e10)
                    {
                        Console.WriteLine("Error :: Unable to include MS2Files in result :" + e10.Message);
                    }
                }
                else if (!includeMS2)
                {
                    foreach (var s in myProteins.AllSQTScans)
                    {
                        s.MSLight = null;
                    }
                }
            }
            //-------------------------------------------------------------------------------------


            //-----------unnormalize the primary score
            if (true)
            {
                foreach (SQTScan s in myProteins.AllSQTScans)
                {
                    s.PrimaryScore = s.TailScore;
                }
            }

            //----------------

            SEPRPackage.FDRStatistics.FDRResult fdrResult = SEPRPackage.FDRStatistics.GenerateFDRStatistics(myProteins, myParams);
            //Update the GUI

            List<MyProtein> unlabeledDecoys = myProteins.MyProteinList.FindAll(a => a.Locus.StartsWith(myParams.UnlabeledDecoyTag));

            //Overfitting calculation
            int success = myProteins.MyProteinList.FindAll(a => a.Locus.StartsWith(myParams.UnlabeledDecoyTag)).Count();
            int n = myProteins.MyProteinList.Count;



            //----
            myResults = new ResultPackage(
                myProteins,
                myParams,
                myParams.AlternativeProteinDB,
                myParams.SeachResultDirectoy,
                false
                );

        }
    }
}
