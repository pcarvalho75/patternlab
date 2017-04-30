using PatternTools;
using PatternTools.FastaParser;
using PatternTools.MSParserLight;
using PatternTools.RawReader;
using PatternTools.SQTParser;
using SEProcessor.LockMass;
using SEPRPackage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Xml.Serialization;

namespace SEProcessor
{
    public partial class MainControl : UserControl
    {
        Regex trypticTermini = new Regex("[-|K|R]" + Regex.Escape(".") + "[^P]", RegexOptions.Compiled);
        MLLM mllm; //Machine learning lock mass

        public MainControl()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();
            resultViewer1.SetTablesToReadOnly = false;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            //For benchmarking
            DateTime beginTime = DateTime.Now;

            buttonGo.Text = "Working!";

            Parameters myParams = GenerateParametersFromGUI();
            myParams.FormsPriorForPeptides = false;

            // Load the protein database--------------------------------

            //Lets load the fastaDB

            //If we have the database, lets load it!
            FastaFileParser fastaParser = new FastaFileParser();
            try
            {
                Console.WriteLine("Parsing fasta DB");
                fastaParser.ParseFile(new StreamReader(myParams.AlternativeProteinDB), false, DBTypes.IDSpaceDescription);
            }
            catch (Exception e5)
            {
                MessageBox.Show("Problems parsing the protein DB " + myParams.AlternativeProteinDB + "\n" + e5.Message);
                DoneMainProcessing(beginTime);
                return;
            }

            //----------------------------------------------------------


            List<string> directoriesToFilter = new List<string>();

            if (!Directory.Exists(textBoxSQTDirectory.Text))
            {
                MessageBox.Show("The filter directory does not exist.");
                DoneMainProcessing(beginTime);
                return;
            }

            if (!checkBoxBatchProcessing.Checked)
            {
                directoriesToFilter.Add(textBoxSQTDirectory.Text);
                string[] files = Directory.GetFiles(textBoxSQTDirectory.Text, "*.sqt");
                if (files.Length == 0)
                {
                    MessageBox.Show("No .sqt files found");
                    DoneMainProcessing(beginTime);
                    return;
                }

                //Check if there are sepro files and prompt the user to delete them
                string[] seproFiles = Directory.GetFiles(textBoxSQTDirectory.Text, "*.sepr");
                DeleteSEProFiles(seproFiles);
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(textBoxSQTDirectory.Text);
                List<FileInfo> sqtFiles = di.GetFiles("*.sqt", SearchOption.AllDirectories).ToList();
                directoriesToFilter = sqtFiles.Select(a => a.Directory.FullName).Distinct().ToList();
                if (directoriesToFilter.Count == 0)
                {
                    MessageBox.Show("No .sqt files found");
                    DoneMainProcessing(beginTime);
                    return;
                }
                //Check if there are sepro files and prompt the user to delete them
                string[] seproFiles = Directory.GetFiles(textBoxSQTDirectory.Text, "*.sepr", SearchOption.AllDirectories);
                DeleteSEProFiles(seproFiles);
            }

            foreach (string filterDir in directoriesToFilter)
            {

                //Parse
                //Sets all labels to ?
                ResetLabels();

                List<SQTScan> allScans = Processor.ParseSQTsToScans(Directory.GetFiles(filterDir, "*.sqt"), myParams);

                if (checkBoxMLLM.Checked)
                {
                    if (resultViewer1.ResultViewerResultPackage == null)
                    {
                        MessageBox.Show("A first layer of filtering with stringent spectra criteria must be performed before appling machine learning lock mass.");
                        DoneMainProcessing(beginTime);
                        return;
                    }

                    if (resultViewer1.ResultViewerResultPackage.LockMass == true)
                    {
                        MessageBox.Show("The machine learning lock mass cannot be applied to data that were previously processed using machine learning lock mass");
                        DoneMainProcessing(beginTime);
                        return;

                    }

                    mllm = new MLLM(resultViewer1.ResultViewerResultPackage, myParams);
                    mllm.TuneScans(allScans);
                    Console.WriteLine("Finished applying lockmass");
                }

                tabControlMain.SelectedIndex = 1;
                this.Update();

                //Parse the sqt files to the paths
                toolStripStatusLabel.Text = "Parsing";


                PeptideAnalysisPckg analysisFDR = new PeptideAnalysisPckg(allScans, myParams);
                labelQ0Spec.Text = analysisFDR.SpectraFDR;
                labelQ0Pept.Text = analysisFDR.PeptideFDR;
                this.Update();

                //FormsScore
                Dictionary<int, double> dicPriorsForPeptidesForms = new Dictionary<int, double>();
                if (myParams.FormsPriorForPeptides)
                {
                    dicPriorsForPeptidesForms = EstablishPriorsForPeptideClassification(allScans, myParams);
                }



                //Lets throw in some quality filters to remove the junk!
                Console.WriteLine("Applying quality filters");
                Processor.ApplyQualityFilters(myParams, allScans);


                //Now lets eliminate PSMs of sequences whose decoys match the targets
                int eliminated = Processor.EliminateDecoysPSMSequencesThatMatchTargets(allScans, myParams);
                Console.WriteLine("Decoy scans eliminated for sharing sequences with targets: " + eliminated);


                //Sequence Test----------------------------------------------------------------------------

                analysisFDR = new PeptideAnalysisPckg(allScans, myParams);
                labelQ1Spec.Text = analysisFDR.SpectraFDR;
                labelQ1Pept.Text = analysisFDR.PeptideFDR;


                toolStripStatusLabel.Text = "Calculating scores";
                this.Update();


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
                    tabControlDistributionGraph.SelectedTab = tabPage2EnzymaticTermini;

                    if (myParams.QFilterMinNoEnzymaticTermini <= 0)
                    {
                        Path p0 = new Path("Zero enzymatic termini");
                        p0.MyScans = allScans.FindAll(a => a.NoEnzymeCleavages == 0);
                        if (p0.MyScans.Count > 0)
                        {
                            //p0.CalculateScoresForGeneralClassification(myParams);
                            myPaths.Add(p0);
                        }
                    }

                    if (myParams.QFilterMinNoEnzymaticTermini <= 1)
                    {

                        Path p1 = new Path("One enzymatic termini");
                        p1.MyScans = allScans.FindAll(a => a.NoEnzymeCleavages == 1);
                        if (p1.MyScans.Count > 0)
                        {
                            //p1.CalculateScoresForGeneralClassification(myParams);
                            myPaths.Add(p1);
                        }
                    }

                    if (myParams.QFilterMinNoEnzymaticTermini <= 2)
                    {
                        Path p2 = new Path("Two enzimatic termini");
                        p2.MyScans = allScans.FindAll(a => a.NoEnzymeCleavages == 2);
                        if (p2.MyScans.Count > 0)
                        {
                            //p2.CalculateScoresForGeneralClassification(myParams);
                            myPaths.Add(p2);
                        }
                    }
                }
                else
                {
                    tabControlDistributionGraph.SelectedTab = tabPageNoEnzymaticSeparation;
                    Path p3 = new Path("No Enzymatic Termini Grouping");
                    p3.MyScans = allScans;
                    //p3.CalculateScoresForGeneralClassification(myParams);
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
                        p.CalculateScoresForGeneralClassification(myParams);
                    }
                }


                //The unified path array - used below
                KeepTrack trackLevel2 = new KeepTrack("Level 2");
                KeepTrack trackLevel3 = new KeepTrack("Level 3");

                bool quitAnalysis = false;

                //Process all paths at the spectral level
                foreach (Path pathSpectra in myPaths)
                {
                    if (myParams.PeptideFDR == -0.01)
                    {
                        MessageBox.Show("You must set an FDR for peptide level.");
                        DoneMainProcessing(beginTime);
                        return;
                    }

                    Console.WriteLine("Processing path: " + pathSpectra.Name + " Total scans = " + pathSpectra.MyScans.Count);
                    if (pathSpectra.MyScans.Count > 15)
                    {
                        //Bayesian Modeling
                        toolStripStatusLabel.Text = "Generating Bayesian model";
                        this.Update();
                        Console.WriteLine("Generating classification model");


                        //Generate spectral classification score
                        try
                        {
                            Classifier.ClassifierScoring.BayesianScoringPSM(pathSpectra.MyScans, myParams, false, false, dicPriorsForPeptidesForms);
                            pathSpectra.MyScans.Sort((a, b) => b.BayesianScore.CompareTo(a.BayesianScore));

                            int myBadExamples = pathSpectra.MyScans.Count - pathSpectra.MyScans.FindAll(a => a.CountNumberForwardNames(myParams.LabeledDecoyTag) > 0).Count;

                            if (pathSpectra.MyScans.Min(a => a.BayesianScore) == 1)
                            {

                                MessageBox.Show("Not enough spectra in decoy or target class to make robust statistics.\nMaybe, try turning off the outlier filter or making bigger groups by decreasing group stringency could yield better results (e.g., use only enzimatic separation or only charge state separation or none).\nError happened for test: " + pathSpectra.Name + "\n\nANALYSIS WILL BE DISCONTINUED.");
                                DoneMainProcessing(beginTime);
                                quitAnalysis = true;
                                break;
                            }
                        }
                        catch (Exception e10)
                        {
                            Console.WriteLine("****Error -> Dynamic Filtering at spectra / peptide level not performed" + e10.Message);
                            groupBoxQ2.BackColor = Color.Red;
                            groupBoxQ3.BackColor = Color.Red;
                        }

                        //Geneate Histogam
                        try
                        {
                            if (!myParams.GroupByChargeState)
                            {
                                GenerateSeparabilityHistogram(pathSpectra, myParams);
                                this.Update();
                            }
                        }
                        catch (Exception esh)
                        {
                            Console.WriteLine("Failed to generate separability histogram; reason: " + esh.Message);
                        }
                        //------------


                        //First lets find out how many good and bad spectra we have at the spectra level
                        Console.WriteLine("Calculating spectra FDR for path: " + pathSpectra.Name);
                        toolStripStatusLabel.Text = "Calculating spectra FDR";
                        this.Update();

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
                            labelQ2Spec.Text = analysisFDR.SpectraFDR;
                            labelQ2Pept.Text = analysisFDR.PeptideFDR;

                            //Keep global tracking
                            trackLevel2.AccumulateResults(analysisFDR);


                            this.Update();
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
                    labelQ3Spec.Text = thisFDRAnalysis.SpectraFDR;
                    labelQ3Pept.Text = thisFDRAnalysis.PeptideFDR;
                    trackLevel3.AccumulateResults(thisFDRAnalysis);


                    separatePathAnalysis.Add(thisFDRAnalysis);

                    this.Update();

                }

                //This should clean up to only confident scans and free up RAM
                allScans = (from pckg in separatePathAnalysis
                            from scn in pckg.AllScans
                            select scn).Distinct().ToList();

                if (quitAnalysis) { return; }

                //Now lets head for the proteins
                //Grouping
                toolStripStatusLabel.Text = "Grouping";

                //Update the labels
                labelQ2Spec.Text = trackLevel2.SpectraFDR;
                labelQ2Pept.Text = trackLevel2.PeptideFDR;
                labelQ3Spec.Text = trackLevel3.SpectraFDR;
                labelQ3Pept.Text = trackLevel3.PeptideFDR;
                this.Update();



                Console.WriteLine("Starting protein Manager");



                //Prolucid does sometimes report peptides to be unique when they are not.
                //this sub check and corrects for this
                //Needs to be updated if new scan fisher becomes active
                
                if (true)
                //if (checkBoxDoubleCheckPeptideUniqueness.Checked)
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
                    DateTime start = DateTime.Now;

                    //We will remove the proteins with a delta ppm from the average ppm
                    if (allScans.Count == 0)
                    {
                        MessageBox.Show("No scans passed the filtering process.  0 matches.");
                        Console.WriteLine("No scans passed the filtering process.");
                        return;
                    }

                    double averagePPMOrbitrap = allScans.Average(a => a.PPM_Orbitrap);

                    scansThatCouldBeFished.RemoveAll(a => Math.Abs(averagePPMOrbitrap - a.PPM_Orbitrap) > myParams.DeltaMassPPMPostProcessing);
                    int ptnsRemoved = myProteins.AllSQTScans.RemoveAll(a => Math.Abs(averagePPMOrbitrap - a.PPM_Orbitrap) > myParams.DeltaMassPPMPostProcessing);
                    myProteins.RebuildProteinsFromScans();

                    string theTime = Math.Ceiling((DateTime.Now - start).TotalMilliseconds).ToString() + " ms.";
                    Console.WriteLine("Post-processing detla PPM filter applied " + ptnsRemoved + "  scans removed :" + theTime);
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
                        if (p.SequenceCount == 1 && p.Scans[0].PrimaryScore < myParams.QFilterMinPrimaryScoreForOneHitWonders)
                        {
                            toRemove.Add(p);
                        }
                    }

                    myProteins.MyProteinList = myProteins.MyProteinList.Except(toRemove).ToList();
                    myProteins.RebuildScansFromModifiedProteinList();
                }

                if (myParams.QFilterMinPrimaryScoreForNonOneHitWonders > 0)
                {
                    List<MyProtein> toRemove = new List<MyProtein>();
                    foreach (MyProtein p in myProteins.MyProteinList)
                    {
                        if (p.SequenceCount > 1 && !p.Scans.Exists(mkmkm => mkmkm.TailScore > myParams.QFilterMinPrimaryScoreForNonOneHitWonders))
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
                //Clean coverage data
                foreach (FastaItem fastaItemMy in fastaParser.MyItems)
                {
                    fastaItemMy.ResetCoverage();
                }

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
                        groupBoxQ4.BackColor = Color.Red;
                    }
                }



                if (checkBoxProteinLogic.Checked)
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
                    if (checkBoxIncludeMS2.Checked && myProteins.AllSQTScans[0].MSLight == null)
                    {
                        try
                        {
                            Console.WriteLine("Loading MS2");
                            AttributeSpectraToSQT(myProteins.AllSQTScans, filterDir);
                            Console.WriteLine("Done loading MS2");
                        }
                        catch (Exception e10)
                        {
                            Console.WriteLine("Error :: Unable to include MS2Files in result :" + e10.Message);
                        }
                    }
                    else if (!checkBoxIncludeMS2.Checked)
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

                labelQ4Spec.Text = fdrResult.SpectraFDRLabel;
                labelQ4Pept.Text = fdrResult.PeptideFDRLabel;
                labelQ4Prot.Text = fdrResult.ProteinFDRLabel;
                labelPtnNR.Text = myProteins.TheGroups.Count.ToString();
                labelPtnUnique.Text = myProteins.MyProteinList.FindAll(a => a.ContainsUniquePeptide > 0).Count().ToString();

                List<MyProtein> unlabeledDecoys = myProteins.MyProteinList.FindAll(a => a.Locus.StartsWith(myParams.UnlabeledDecoyTag));
                labelUnlabeledDecoyPtnsAndGroups.Text = unlabeledDecoys.Count.ToString() + " / " + unlabeledDecoys.Select(a => a.GroupNo).Distinct().Count().ToString();

                //Overfitting calculation
                int success = myProteins.MyProteinList.FindAll(a => a.Locus.StartsWith(myParams.UnlabeledDecoyTag)).Count();
                int n = myProteins.MyProteinList.Count;
                double cumulative = 0;
                FileInfo fi = new FileInfo(myParams.AlternativeProteinDB);

                if (fi.Extension.Equals(".T-PR-MR") || fi.Extension.Equals(".T-S-S1") || fi.Extension.Equals(".T-S0-S1") && myParams.ProteinFDR > 0 && myParams.ProteinFDR <= 1)
                {
                    double p = myParams.ProteinFDR;
                    cumulative = alglib.binomialcdistribution(success, n, p);
                }
                else
                {
                    cumulative = double.NaN;
                }

                if (cumulative < 0.05)
                {
                    labelProteinOverfitting.BackColor = Color.Red;
                }
                else
                {
                    labelProteinOverfitting.BackColor = Color.Blue;
                }

                labelProteinOverfitting.Text = (Math.Round(cumulative, 3)).ToString("F2");

                //----
                resultViewer1.ResultViewerResultPackage = new ResultPackage(
                    myProteins,
                    myParams,
                    myParams.AlternativeProteinDB,
                    filterDir,
                    checkBoxMLLM.Checked
                    );

                resultViewer1.ResultViewerPopulateTables();

                //-------------------------------------------

                ///Result Table
                toolStripStatusLabel.Text = "Updating result table";
                this.Update();
                ///--------------------------------
                ///

                //We need to automatically save the file
                if (checkBoxBatchProcessing.Checked)
                {
                    DirectoryInfo di = new DirectoryInfo(filterDir);
                    string saveFileName = filterDir + "/" + di.Name + ".sepr";
                    Console.WriteLine("Batch Mode: saving " + saveFileName);
                    resultViewer1.ResultViewerResultPackage.SQTDirectory = filterDir;
                    resultViewer1.ResultViewerResultPackage.Save(saveFileName);
                    ResetLabels();
                }

            }



            //For benchmark purposes
            DoneMainProcessing(beginTime);
        }

        private static void DeleteSEProFiles(string[] seproFiles)
        {
            if (seproFiles.Length > 0)
            {
                if (MessageBox.Show("There are SEPro files in the current filter directory.\n\n" + string.Join("\n", seproFiles) + "\n\nShould I delete them?", "Delete existing files", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (string file in seproFiles)
                    {
                        Console.WriteLine("Deleting " + file);
                        File.Delete(file);
                    }
                }
            }
        }


        public static Dictionary<int, double> EstablishPriorsForPeptideClassification(List<SQTScan> allScans, Parameters myParams)
        {

            Dictionary<string, List<SQTScan>> pepDict = new Dictionary<string, List<SQTScan>>();

            pepDict = (from s in allScans.AsParallel()
                       group s by s.PeptideSequenceCleaned into peptideSequence
                       select new { CleanedPeptideSequence = peptideSequence.Key, Scans = peptideSequence }).ToDictionary(a => a.CleanedPeptideSequence, a => a.Scans.ToList());

            Dictionary<int, List<int>> dictFormsScanDict = new Dictionary<int, List<int>>();

            foreach (KeyValuePair<string, List<SQTScan>> kvp in pepDict)
            {
                //Find out how many charge states
                int charges = (from c in kvp.Value
                               select c.ChargeState).Distinct().ToList().Count;

                if (dictFormsScanDict.ContainsKey(charges))
                {
                    dictFormsScanDict[charges].Add(kvp.Value[0].CountNumberForwardNames(myParams.LabeledDecoyTag));
                }
                else
                {
                    dictFormsScanDict.Add(charges, new List<int>() { kvp.Value[0].CountNumberForwardNames(myParams.LabeledDecoyTag) });
                }

                foreach (SQTScan s in kvp.Value)
                {
                    s.NoForms = charges;
                }
            }

            //Now, foreach charge state, we calculate an FDR
            Dictionary<int, double> dicChargeFDR = new Dictionary<int, double>();

            foreach (KeyValuePair<int, List<int>> kvp in dictFormsScanDict)
            {
                double fdr = (double)kvp.Value.Count(a => a == 0) / (double)kvp.Value.Count;

                if (fdr < 0.35) { fdr = 0.35; }
                if (fdr > 0.75) { fdr = 0.75; }
                dicChargeFDR.Add(kvp.Key, fdr);
            }

            return dicChargeFDR;

        }




        private void DoneMainProcessing(DateTime beginTime)
        {
            buttonGo.Text = "Go!";
            toolStripStatusLabel.Text = "Idle : Processing time: " + Math.Ceiling((DateTime.Now - beginTime).TotalMilliseconds).ToString() + " ms.";
            Console.Write("Done");
            if (checkBoxBatchProcessing.Checked)
            {
                MessageBox.Show("Done processing all subdirectories.  You can now navigate to each one and load the coresponding SEPro file.");
            }
        }

        private void GenerateSeparabilityHistogram(Path thePath, Parameters myParams)
        {

            Chart theChart;

            if (thePath.Name.Equals("No Enzymatic Termini Grouping"))
            {
                theChart = chartNoEnzymaticGrouping;
            }
            else if (thePath.Name.Equals("Zero enzymatic termini"))
            {
                theChart = chartZeroEnzymaticTermini;
            }
            else if (thePath.Name.Equals("One enzymatic termini"))
            {
                theChart = chartOneEnzymaticTermini;
            }
            else
            {
                theChart = chartTwoEnzymaticTermini;
            }



            int intervals = 80;
            double[] forwardCounter = new double[intervals + 1];
            double[] labeledDecoyCounter = new double[intervals + 1];
            double step = 1 / (double)intervals;


            List<double> forwardScores = new List<double>();
            foreach (SQTScan s in thePath.MyScans)
            {
                //Find out the bucket number
                int bucketNo = (int)Math.Floor(s.BayesianScore / step);

                int theIndex = s.IdentificationNames.FindIndex(a => !a.StartsWith(myParams.LabeledDecoyTag));

                if (theIndex > -1)
                {
                    forwardCounter[bucketNo]++;
                }
                else
                {
                    labeledDecoyCounter[bucketNo]++;
                }
            }

            theChart.Series["Series1"].Points.Clear();
            theChart.Series["Series2"].Points.Clear();
            theChart.Titles.Clear();

            theChart.Series["Series1"].LegendText = "Target";
            theChart.Series["Series2"].LegendText = "Labeled Decoys";

            for (int i = 0; i < forwardCounter.Length; i++)
            {
                theChart.Series["Series1"].Points.Add(new DataPoint(i, forwardCounter[i]));
                theChart.Series["Series2"].Points.Add(new DataPoint(i, labeledDecoyCounter[i]));
            }

            theChart.Titles.Add(thePath.Name);


            //----------------------
        }

        private void ResetLabels()
        {
            groupBoxQ0.BackColor = Color.Transparent;
            groupBoxQ1.BackColor = Color.Transparent;
            groupBoxQ2.BackColor = Color.Transparent;
            groupBoxQ3.BackColor = Color.Transparent;
            groupBoxQ4.BackColor = Color.Transparent;

            labelQ0Spec.Text = "?";
            labelQ0Pept.Text = "?";
            labelQ1Pept.Text = "?";
            labelQ1Spec.Text = "?";
            labelQ2Pept.Text = "?";
            labelQ2Spec.Text = "?";
            labelQ3Pept.Text = "?";
            labelQ3Spec.Text = "?";
        }


        private List<FastaItem> GetGoodFasta(ProteinManager myProteins, List<FastaItem> fullFastaDB)
        {

            ConcurrentBag<FastaItem> myItems = new ConcurrentBag<FastaItem>();

            Parallel.ForEach(myProteins.MyProteinList, p =>
            //foreach (Result.ProteinResult p in allProteins)
            {
                myItems.Add(fullFastaDB.Find(a => a.SequenceIdentifier.Contains(p.Locus)));
            }
            );

            return myItems.ToList();
        }

        public static void AttributeSpectraToSQT(List<SQTScan> list, string path)
        {
            ConcurrentBag<PatternTools.MSParserLight.MSLight> goodSpectra = new ConcurrentBag<PatternTools.MSParserLight.MSLight>();


            var scansByFile = (from s in list
                               group s by s.FileName into g
                               select new { fileName = g.Key, scans = g });

            Dictionary<string, List<SQTScan>> scanDict = scansByFile.ToDictionary(a => a.fileName, a => a.scans.ToList());

            Console.WriteLine("Retrieving MS2 spectra");

            Parallel.ForEach(scanDict, kvp =>
            //foreach (KeyValuePair<string, List<PatternTools.SQTParser.SQTScan>> kvp in scanDict)
            {
                Console.WriteLine("Obtaining MS2 from " + kvp.Key);

                
                //Retrieve the MS2 for the file
                string ms2FileName = Regex.Replace(kvp.Key, @"\.sqt", ".ms2");
                string ms2FullFileName = path + "/" + ms2FileName;
                string rawFileName = Regex.Replace(kvp.Key, @"\.sqt", ".RAW");
                string rawFullFileName = path + "/" + rawFileName;
                string mzmlFileName = Regex.Replace(kvp.Key, @"\.sqt", ".mzML");
                string mzmlFullFileName = path + "/" + mzmlFileName;

                List<PatternTools.MSParserLight.MSLight> theSpectra = new List<PatternTools.MSParserLight.MSLight>();
                
                if (File.Exists(ms2FullFileName))
                {
                    theSpectra = msParserLightExtract(ref kvp, ms2FullFileName);
                } 
                else if (File.Exists(mzmlFullFileName)) 
                {
                    theSpectra = msParserLightExtract(ref kvp, mzmlFullFileName);
                } 
                else if (File.Exists(rawFullFileName))
                {

                    List<int> scanNumbers = kvp.Value.Select(a => a.ScanNumber).Distinct().ToList();
                    scanNumbers.Sort();

                    RawReaderParams rParams = new RawReaderParams();
                    rParams.ExtractMS1 = false;
                    rParams.ExtractMS2 = true;
                    rParams.UseThermoMonoIsotopicPrediction = true;

                    Reader r = new Reader(rParams);

                    theSpectra = r.GetSpectra(rawFullFileName, scanNumbers, false);

                    foreach (MSLight ms in theSpectra)
                    {
                        int index = kvp.Value.FindIndex(a => a.ScanNumber == ms.ScanNumber);

                        if (index > -1)
                        {
                            kvp.Value[index].MSLight = ms;
                        }
                    }
                }


                

            }
            );
        }

        private static List<MSLight> msParserLightExtract(ref KeyValuePair<string, List<SQTScan>> kvp, string ms2FullFileName)
        {
            List<PatternTools.MSParserLight.MSLight> theSpectra;
            theSpectra = PatternTools.MSParserLight.ParserLight.ParseLightMS2(ms2FullFileName);
            kvp.Value.Sort((a, b) => a.ScanNumber.CompareTo(b.ScanNumber));

            int j = 0;
            int i = 0;

            while (i < kvp.Value.Count)
            {

                if (kvp.Value[i].ScanNumber == theSpectra[j].ScanNumber)
                {
                    kvp.Value[i].MSLight = theSpectra[j];
                    i++;
                    j++;
                }
                else if (kvp.Value[i].ScanNumber < theSpectra[j].ScanNumber)
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }
            return theSpectra;
        }

        //---------------------------------------------------------------------------


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Parameters GenerateParametersFromGUI()
        {
            Parameters p = new Parameters();

            p.LabeledDecoyTag = textBoxLabeledDecoyTag.Text;
            p.UnlabeledDecoyTag = textBoxUnlabeledDecoyTag.Text;

            //Protein grouping
            p.GroupPtnsWithXCommonPeptides = 1;
            p.EliminateInPtns = true;

            p.GroupByNoEnzymaticTermini = checkBoxGroupByNoEnzimaticTermini.Checked;
            p.GroupByChargeState = checkBoxGroupByChargeState.Checked;

            p.MS2PPM = (double)numericUpDownMS2PPM.Value;
            p.MyEnzime = (Enzyme)Enum.Parse(typeof(Enzyme), comboBoxEnzyme.SelectedItem.ToString());
            //p.SeqFilterStaticMod = (Modification)Enum.Parse(typeof(Modification), comboBoxSeqFilterStaticMod.SelectedItem.ToString());
            //p.SeqFilterVariableMod = (Modification)Enum.Parse(typeof(Modification), comboBoxSeqFilterVariableMod.SelectedItem.ToString());
            p.SequenceFiltersEnabled = checkBoxEnableSequenceFilters.Checked;

            //One hit wonders
            p.SeqQFilterRelativeIntensityThreshold = (double)numericUpDownSeqFilterIonIntensityThreshold.Value;
            p.SeqQFilterACount = (int)numericUpDownSeqFilterACount.Value;
            p.SeqQFilterBCount = (int)numericUpDownSeqFilterBCount.Value;
            p.SeqQFilterCCount = (int)numericUpDownSeqFilterCCount.Value;
            p.SeqQFilterXCount = (int)numericUpDownSeqFilterXCount.Value;
            p.SeqQFilterYCount = (int)numericUpDownSeqFilterYCount.Value;
            p.SeqQFilterZCount = (int)numericUpDownSeqFilterZCount.Value;
            p.SeqQFilterCAXCount = (int)numericUpDownSeqFilterCAX.Value;
            p.SeqQFilterCBYCount = (int)numericUpDownSeqFilterCBY.Value;
            p.SeqQFilterCCZCount = (int)numericUpDownSeqFilterCCZ.Value;
            //----

            p.SpectraFDR = (double)numericUpDownSpectraFDR.Value / 100;
            p.PeptideFDR = (double)numericUpDownPeptideFDR.Value / 100;
            p.ProteinFDR = (double)numericUpDownProteinFDR.Value / 100;
            p.CompositeScorePrimaryScore = checkBoxPrimaryScore.Checked;
            p.CompositeScoreSecondaryScore = checkBoxSecondaryScore.Checked;
            p.CompositeScoreDeltaCN = checkBoxDeltaCN.Checked;
            p.CompositeScorePeaksMatched = checkBoxPeaksMatched.Checked;
            p.CompositeScoreDeltaMassPPM = checkBoxQualityDeltaMass.Checked;
            p.CompositScoreDigestion = checkBoxDigestionScore.Checked;
            p.CompositeScoreSecondaryRank = checkBoxCompositeScoreSecondaryRank.Checked;
            p.CompositeScorePresence = checkBoxCompositeScorePresence.Checked;
            p.QFilterMinSequenceLength = (int)numericUpDownMinSequenceLength.Value;
            p.QFilterDiscardChargeOneMS = checkBoxFilterEliminateChargeOne.Checked;
            p.QFilterDeltaMassPPM = checkBoxFilterDeltaMass.Checked;
            p.QFilterDeltaMassPPMValue = (double)numericUpDownDeltaMassPPM.Value;
            p.QFilterPrimaryScore = checkBoxFilterPrimaryScore.Checked;
            p.QFilterPrimaryScoreValue = (double)numericUpDownFilterPrimaryScore.Value;
            p.QFilterMinNoEnzymaticTermini = (int)numericUpDownMinNoEnzymaticTermini.Value;
            p.QFilterDiscardProteinsWithNoUniquePeptides = checkBoxFilterDiscardProteinsWithNoUniquePeptides.Checked;
            p.QFilterProteinsMinNoPeptides = (int)numericUpDownMinNoPeptides.Value;
            p.QFilterMinNoStates = (int)numericUpDownPtnMinNoStates.Value;
            p.QFilterDeltaCN =  checkBoxFilterDeltaCN.Checked;
            p.QFilterDeltaCNMin = (double)numericUpDownMinDeltaCN.Value;
            p.AlternativeProteinDB = textBoxAlternativeProteinDB.Text;
            p.SeachResultDirectoy = textBoxSQTDirectory.Text;
            p.QFilterMinSpecCount = (int)numericUpDownMinNoSpecCounts.Value;
            p.DeltaMassPPMPostProcessing = (double)numericUpDownDeltaMassPPMPostProcessing.Value;
            p.QFilterMinPrimaryScoreForOneHitWonders = (double)numericUpDownMinPrimaryScoreForOneHitWonders.Value;
            p.QFilterMinPrimaryScoreForNonOneHitWonders = (double)numericUpDownMinPrimaryScoreForNonOneHitWonders.Value;
            return p;
        }


        private void buttonBrowseSqtDir_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = false;
            if (folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxSQTDirectory.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void checkBoxFilterDeltaMass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFilterDeltaMass.Checked)
            {
                numericUpDownDeltaMassPPM.Enabled = true;
                numericUpDownDeltaMassPPMPostProcessing.Enabled = true;
            }
            else
            {
                numericUpDownDeltaMassPPM.Enabled = false;
                numericUpDownDeltaMassPPMPostProcessing.Enabled = false;
            }
        }

        private void buttonBrowseForAlternativeDB_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Load SE Processor parameter file";
            openFileDialog1.Filter = "All items (*.*)|*.*";
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxAlternativeProteinDB.Text = openFileDialog1.FileName;
            }
        }


        private void checkBoxFilterPrimaryScore_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFilterPrimaryScore.Checked)
            {
                numericUpDownFilterPrimaryScore.Enabled = true;
            }
            else
            {
                numericUpDownFilterPrimaryScore.Enabled = false;
            }
        }

        public void LoadResults(string fileName)
        {
            resultViewer1.ResultViewerResultPackage = new ResultPackage();
            resultViewer1.ResultViewerResultPackage = ResultPackage.Load(fileName);
            resultViewer1.ResultViewerPopulateTables();
            UpdateGUIwithParameters(resultViewer1.ResultViewerResultPackage.MyParameters);
            tabControlMain.SelectedTab = tabPageResultBrowser;
            cHPPNextProtAnalysisonlyForHumanSamplesToolStripMenuItem.Enabled = true;
            Console.WriteLine("Object deserialized!");

        }


        private void UpdateGUIwithParameters(Parameters p)
        {
            if (!p.QFilterDeltaCN)
            {
                numericUpDownMinDeltaCN.Enabled = false;
            }

            textBoxLabeledDecoyTag.Text = p.LabeledDecoyTag;
            textBoxUnlabeledDecoyTag.Text = p.UnlabeledDecoyTag;

            //Protein grouping params

            if (p.GroupPtnsWithXCommonPeptides == 0) 
            { 
                p.GroupPtnsWithXCommonPeptides = 1;
            }

            checkBoxGroupByNoEnzimaticTermini.Checked = p.GroupByNoEnzymaticTermini;
            checkBoxGroupByChargeState.Checked = p.GroupByChargeState;

            numericUpDownMS2PPM.Value = (decimal)p.MS2PPM;


            int enzymeIndex = comboBoxEnzyme.Items.IndexOf(p.MyEnzime.ToString());
            comboBoxEnzyme.Text = "";
            comboBoxEnzyme.SelectedText = p.MyEnzime.ToString();
            comboBoxEnzyme.SelectedItem = enzymeIndex;

            //comboBoxSeqFilterStaticMod.SelectedItem = p.SeqFilterStaticMod.ToString();


            //One hit wonders
            numericUpDownSeqFilterIonIntensityThreshold.Value = (decimal)p.SeqQFilterRelativeIntensityThreshold;
            numericUpDownSeqFilterACount.Value = p.SeqQFilterACount;
            numericUpDownSeqFilterBCount.Value = p.SeqQFilterBCount;
            numericUpDownSeqFilterCCount.Value = p.SeqQFilterCCount;
            numericUpDownSeqFilterXCount.Value = p.SeqQFilterXCount;
            numericUpDownSeqFilterYCount.Value = p.SeqQFilterYCount;
            numericUpDownSeqFilterZCount.Value = p.SeqQFilterZCount;
            numericUpDownSeqFilterCAX.Value = p.SeqQFilterCAXCount;
            numericUpDownSeqFilterCBY.Value = p.SeqQFilterCBYCount;
            numericUpDownSeqFilterCCZ.Value = p.SeqQFilterCCZCount;
            //----

            numericUpDownSpectraFDR.Value = (decimal)p.SpectraFDR * 100;
            numericUpDownPeptideFDR.Value = (decimal)p.PeptideFDR * 100;
            numericUpDownProteinFDR.Value = (decimal)p.ProteinFDR * 100;
            checkBoxPrimaryScore.Checked = p.CompositeScorePrimaryScore;
            checkBoxSecondaryScore.Checked = p.CompositeScoreSecondaryScore;
            checkBoxDeltaCN.Checked = p.CompositeScoreDeltaCN;
            checkBoxPeaksMatched.Checked = p.CompositeScorePeaksMatched;
            checkBoxQualityDeltaMass.Checked = p.CompositeScoreDeltaMassPPM;
            checkBoxDigestionScore.Checked = p.CompositScoreDigestion;
            checkBoxCompositeScoreSecondaryRank.Checked = p.CompositeScoreSecondaryRank;
            checkBoxCompositeScorePresence.Checked = p.CompositeScorePresence;
            checkBoxFilterEliminateChargeOne.Checked = p.QFilterDiscardChargeOneMS;
            checkBoxFilterDeltaMass.Checked = p.QFilterDeltaMassPPM;
            numericUpDownDeltaMassPPM.Value = (decimal)p.QFilterDeltaMassPPMValue;
            checkBoxFilterPrimaryScore.Checked = p.QFilterPrimaryScore;      
            numericUpDownMinNoEnzymaticTermini.Value = p.QFilterMinNoEnzymaticTermini;
            checkBoxFilterDiscardProteinsWithNoUniquePeptides.Checked = p.QFilterDiscardProteinsWithNoUniquePeptides;     
            textBoxAlternativeProteinDB.Text = p.AlternativeProteinDB;
            textBoxSQTDirectory.Text = p.SeachResultDirectoy;

            numericUpDownMinNoSpecCounts.Value = p.QFilterMinSpecCount;
            numericUpDownDeltaMassPPMPostProcessing.Value = (decimal)p.DeltaMassPPMPostProcessing;
            numericUpDownPtnMinNoStates.Value = (int)p.QFilterMinNoStates;
            numericUpDownFilterPrimaryScore.Value = (decimal)p.QFilterPrimaryScoreValue;
            numericUpDownMinNoPeptides.Value = p.QFilterProteinsMinNoPeptides;
            numericUpDownMinSequenceLength.Value = p.QFilterMinSequenceLength;

            checkBoxFilterDeltaCN.Checked = p.QFilterDeltaCN;
            numericUpDownMinDeltaCN.Value = (decimal)p.QFilterDeltaCNMin;
            numericUpDownMinPrimaryScoreForOneHitWonders.Value = (decimal)p.QFilterMinPrimaryScoreForOneHitWonders;
            numericUpDownMinPrimaryScoreForNonOneHitWonders.Value = (decimal)p.QFilterMinPrimaryScoreForNonOneHitWonders;

            Enzyme enz = p.MyEnzime;
            for (int i = 0; i < comboBoxEnzyme.Items.Count; i++)
            {
                string iString = comboBoxEnzyme.Items[i].ToString();
                if (enz.ToString().Equals(iString))
                {
                    comboBoxEnzyme.SelectedIndex = i;

                }
            }
                
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            Console.WriteLine("Search Engine Processor initiating....");
            tabControlMain.TabPages.RemoveAt(3);

            try
            {
                string[] stuff = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");
                Console.WriteLine(stuff[1]);
                this.Text += " " + stuff[1];
            }
            catch
            {
                Console.WriteLine("Unable to retrieve version number.");
            }

            try
            {
                string[] myAppData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                Console.WriteLine("Loading results... ");
                LoadResults(myAppData[0]);
                tabControlMain.SelectedTab = tabPageResultBrowser;
                this.Text += " - " + myAppData[0];
            }
            catch
            {
                //Program not being loaded by a result file, lets try to load the default params

                try
                {

                    Parameters myParams = new Parameters();
                    StringReader s = new StringReader(SEProcessor.Properties.Settings.Default.FilterSettingsHL);

                    XmlSerializer xmlSerializer = new XmlSerializer(myParams.GetType());
                    myParams = (Parameters)xmlSerializer.Deserialize(s);

                    //Update GUI with object
                    UpdateGUIwithParameters(myParams);
                    VerifySequenceFiltersGroupBox();
                }
                catch
                {
                    //No default parameters exist
                    Console.WriteLine("User has not yet established default parameters.");
                }


            }
        }

        private void buttonSaveAsDefaultParams_Click(object sender, EventArgs e)
        {
            if (!radioButtonSHL.Checked && !radioButtonSLL.Checked && !radioButtonSUser.Checked)
            {
                MessageBox.Show("Please select a mode to save these parameters as defaults.");
                return;
            }

            Parameters myParams = GenerateParametersFromGUI();
            StringWriter sw = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(myParams.GetType());
            //Remmove namespaces
            System.Xml.Serialization.XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
            xs.Add("", "");
            xmlSerializer.Serialize(sw, myParams, xs);
            
            if (radioButtonSHL.Checked)
            {
                SEProcessor.Properties.Settings.Default.FilterSettingsHL = sw.ToString();
            }
            else if (radioButtonSLL.Checked)
            {
                SEProcessor.Properties.Settings.Default.FilterSettingsLL = sw.ToString();
            }
            else if (radioButtonSUser.Checked)
            {
                SEProcessor.Properties.Settings.Default.FilterSettings = sw.ToString();
            }
            
            SEProcessor.Properties.Settings.Default.Save();

            MessageBox.Show("Parameters serialized to application settings.");
        }

        private void checkBoxEnableSequenceFilters_CheckedChanged(object sender, EventArgs e)
        {
            VerifySequenceFiltersGroupBox();
        }

        private void VerifySequenceFiltersGroupBox()
        {
            if (checkBoxEnableSequenceFilters.Checked)
            {
                groupBoxSequenceFilters.Enabled = true;
            }
            else
            {
                groupBoxSequenceFilters.Enabled = false;
            }
        }

        private void checkBoxIncludeMS2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxIncludeMS2.Checked)
            {
                checkBoxEnableSequenceFilters.Checked = false;
                checkBoxEnableSequenceFilters.Enabled = false;
            }
            else
            {
                checkBoxEnableSequenceFilters.Enabled = true;
            }
        }


        private void toolStripMenuItemLoadSeproResult_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "SE Processor Result file (*.sepr)|*.sepr|All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                Console.WriteLine("Loading results...");
                LoadResults(openFileDialog1.FileName);
                

            }
        }

        private void toolStripMenuItemSaveSepro_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "sepr";

            if (Directory.Exists(textBoxSQTDirectory.Text))
            {
                saveFileDialog1.InitialDirectory = textBoxSQTDirectory.Text;

                DirectoryInfo di = new DirectoryInfo(textBoxSQTDirectory.Text);
                saveFileDialog1.FileName = di.Name;
            }

            saveFileDialog1.Filter = saveFileDialog1.Filter = "SE Processor Result file (*.sepr)|*.sepr|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                resultViewer1.ResultViewerResultPackage.Save(saveFileDialog1.FileName);
                MessageBox.Show("Project data saved.");
            }
        }

        private void toolStripMenuItemSaveResultFastaSequences_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Fasta (*.fasta)|*.fasta";

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach (MyProtein p in resultViewer1.ResultViewerResultPackage.MyProteins.MyProteinList)
                {
                    sw.WriteLine(">" + p.Locus + " " + p.Description);
                    sw.WriteLine(p.Sequence);
                }
                sw.Close();
                MessageBox.Show("File Saved!");
            }
        }

        private void toolStripMenuItemSaveResultByFileName_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                resultViewer1.SaveByFileName(saveFileDialog1.FileName);

                MessageBox.Show("Resport Saved.");
            }
        }

        private void toolStripMenuItemSaveAsDTASelectFiltered_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                resultViewer1.SaveDTASelectFilteredReport(saveFileDialog1.FileName);
                MessageBox.Show("Resport Saved.");
            }
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void textBoxSQTDirectory_Validated(object sender, EventArgs e)
        {
            string ProLuCIDParams = textBoxSQTDirectory.Text + "\\" + "search.xml";
            string cometParams = textBoxSQTDirectory.Text + "\\" + "comet.params";

            string databaseName = "";
            
            if (File.Exists(ProLuCIDParams))
            {

                XmlDocument xDocReader = new XmlDocument();
                xDocReader.Load(ProLuCIDParams);

                foreach (XmlNode xNode in xDocReader.DocumentElement)
                {
                    if (xNode.Name.Equals("database"))
                    {
                        foreach (XmlNode n2 in xNode.ChildNodes)
                        {
                            if (n2.Name.Equals("database_name"))
                            {

                                databaseName = n2.InnerText;
                                databaseName = databaseName.Replace("/", "\\");
                            }
                        }
                    }

                }

            }

            if (File.Exists(cometParams))
            {
                StreamReader sr = new StreamReader(cometParams);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("database_name ="))
                    {
                        string [] cols = Regex.Split(line, " = ");
                        databaseName = cols[1];
                        databaseName = databaseName.Replace("/", "\\");
                        break;
                    }
                }
                sr.Close();
            }


            if (databaseName.Length > 0 && !textBoxAlternativeProteinDB.Text.Equals(databaseName))
            {
                if (MessageBox.Show("Should I update the protein sequence database with the one found in your search parameter file?", "Update sequence database", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    textBoxAlternativeProteinDB.Text = databaseName;
                    Console.WriteLine("Protein satabase updated.");
                }
            }



        }

        private void pPMDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            PPMDistributionViewer pViewer = new PPMDistributionViewer();

            try
            {

                pViewer.TheScans = resultViewer1.ResultViewerResultPackage.MyProteins.AllSQTScans;
                pViewer.Plot();
                pViewer.ShowDialog();
            } catch (NullReferenceException nre) {
                Console.WriteLine("Please load a dataset. Exception:" + nre.Message);
            }



        }

        private void checkBoxGroupByNoEnzimaticTermini_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGroupByNoEnzimaticTermini.Checked)
            {
                if (MessageBox.Show("We recomend unchecking the Digestion Score when this box is selected.\nDo you wish to uncheck it now?", "Parameter tunning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    checkBoxDigestionScore.Checked = false;
                }
            }
        }

        private void chargeDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Result.ChargeStateDistribution cd = new Result.ChargeStateDistribution();
                cd.TheScans = resultViewer1.ResultViewerResultPackage.MyProteins.AllSQTScans;
                cd.Plot();
                cd.ShowDialog();
            } catch (NullReferenceException nre) {
                Console.WriteLine("Please load a dataset. Exception:" + nre.Message);
            }

            
        }


        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
            UpdateCheckInfo info;

            try
            {
                info = updateCheck.CheckForDetailedUpdate();

                if (info.UpdateAvailable)
                {
                    DialogResult dialogResult = MessageBox.Show("An update is available. Would you like to update the application now?", "Update available", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.OK)
                    {
                        updateCheck.Update();
                        MessageBox.Show("The application has been upgraded, and will now restart.");
                        Application.Restart();
                    }

                }
                else
                {
                    MessageBox.Show("No updates are available for the moment.");
                }
            }
            catch (DeploymentDownloadException dde)
            {
                MessageBox.Show(dde.Message + "\n" + dde.InnerException);
                return;
            }
            catch (InvalidDeploymentException ide)
            {
                MessageBox.Show(ide.Message + "\n" + ide.InnerException);
                return;
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message + "\n" + ioe.InnerException);
                return;
            }
            catch (Exception e10)
            {
                MessageBox.Show(e10.Message);
                return;
            }

        }

        private void pPMHeatMapOnlyAfterUsingMLLockMassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultViewer1.ResultViewerResultPackage.LockMass)
            {
                PPMHeatMap.FormPPMHeatMap f = new PPMHeatMap.FormPPMHeatMap();
                f.MyResultPackage = resultViewer1.ResultViewerResultPackage;
                f.LockMass = mllm;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("This feature is only used in data that were processed with lockmass feature");
                return;
            }
        }

        private void saveMaxParsimonyListOfIdentificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() !=  System.Windows.Forms.DialogResult.Cancel)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);


                //To save by groups
                //foreach (int groupNumber in resultViewer1.ResultViewerResultPackage.MyProteins.TheGroups)
                //{
                //    List<MyProtein> proteins = resultViewer1.ResultViewerResultPackage.MyProteins.MyProteinList.FindAll(a => a.GroupNo == groupNumber);
                //    sw.WriteLine(">" + proteins[0].Locus);
                //    sw.WriteLine(proteins[0].Sequence);
                //}

                List<MyProtein> maxParsimonyList = resultViewer1.ResultViewerResultPackage.MaxParsimonyList();

                foreach (MyProtein p in maxParsimonyList)
                {
                    sw.WriteLine(">" + p.Locus + " " + p.Description);
                    sw.WriteLine(p.Sequence);
                }
                sw.Close();
                MessageBox.Show("File Saved!");
            }
        }

        private void checkBoxFilterDeltaCN_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDeltaCN.Checked)
            {
                numericUpDownMinDeltaCN.Enabled = true;
            }
            else
            {
                numericUpDownMinDeltaCN.Enabled = false;
            }
        }

        private void saveMS2OfIdentifiedSpectraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveMS2 saveMS2Window = new SaveMS2();
                saveMS2Window.MyProteinManager = resultViewer1.ResultViewerResultPackage.MyProteins;
                saveMS2Window.ShowDialog();
                
            }
            catch
            {
                MessageBox.Show("Apparently there are no results loaded.");
            }
        }

        private void saveTabDelimitedTableOfProteinIdentificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    resultViewer1.SaveProteinTable(saveFileDialog1.FileName);
                    
                }

                MessageBox.Show("File saved.");
            }
            catch
            {
                MessageBox.Show("Please make sure that the results are loaded.");
            }
        }

        private void sEProUnifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SEProFusion.Fusion f = new SEProFusion.Fusion();
            f.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControlDistributionGraph_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void extractFastaForSpecificIDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultViewer1.ResultViewerResultPackage == null)
            {
                MessageBox.Show("Please load or filter a result first.");
                return;
            }

            ExtractFasta ef = new ExtractFasta();
            ef.MyRP = resultViewer1.ResultViewerResultPackage;
            ef.ShowDialog();


        }

        private void evaluationOfEnzymeSpecificityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnzymeEvaluator ev = new EnzymeEvaluator();

            //First, lets obtain a list of all identified peptides
            //List<string> peptideSequences = resultViewer1.ResultViewerResultPackage.MyProteins.MyPeptideList.Select(a => a.CleanedPeptideSequence).Distinct().ToList();
            List<string> peptideSequences = (from p in resultViewer1.ResultViewerResultPackage.MyProteins.MyPeptideList
                                             from s in p.MyScans
                                             select s.PeptideSequence).Distinct().ToList();
            

            //Now for each peptide lets check in the identified fasta their specificity level, we begin by assuming it is no specific

            int fullySpecific = 0;
            int semiSpecific = 0;
            int nonSpecific = 0;

            foreach (string peptide in peptideSequences)
            {
                int counter = 0;
                if (peptide[0].Equals('K') || peptide[0].Equals('R') || peptide[0].Equals('-'))
                {
                    counter++;
                }

                if (peptide.Last().Equals('K') || peptide.Last().Equals('R') || peptide.Last().Equals('-'))
                {
                    counter++;
                }

                if (counter == 0)
                {
                    nonSpecific++;
                } else if (counter == 1)
                {
                    semiSpecific++;
                } else if (counter == 2)
                {
                    fullySpecific++;
                }
            }

            MessageBox.Show("Fully specific : " + fullySpecific + "\nSemi-specific : " + semiSpecific + "\nNon-specific : " + nonSpecific);

            Console.WriteLine("Done");

            
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void checkBoxMoreThan50k_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonHL.Checked || radioButtonLL.Checked)
            {
                if (checkBoxMoreThan50k.Checked)
                {
                    checkBoxGroupByChargeState.Checked = true;
                    checkBoxGroupByNoEnzimaticTermini.Checked = true;
                }
                else
                {
                    checkBoxGroupByChargeState.Checked = false;
                    checkBoxGroupByNoEnzimaticTermini.Checked = true;
                }
            }
        }

        private void radioButtonHL_CheckedChanged(object sender, EventArgs e)
        {
            string inputDir = textBoxSQTDirectory.Text;
            string DBDir = textBoxAlternativeProteinDB.Text;

            Parameters myParams = new Parameters();
            StringReader s = new StringReader(SEProcessor.Properties.Settings.Default.FilterSettingsHL);

            XmlSerializer xmlSerializer = new XmlSerializer(myParams.GetType());
            myParams = (Parameters)xmlSerializer.Deserialize(s);

            //Update GUI with object
            UpdateGUIwithParameters(myParams);
            VerifySequenceFiltersGroupBox();

            textBoxSQTDirectory.Text = inputDir;
            textBoxAlternativeProteinDB.Text = DBDir;
        }

        private void radioButtonLL_CheckedChanged(object sender, EventArgs e)
        {

            string inputDir = textBoxSQTDirectory.Text;
            string DBDir = textBoxAlternativeProteinDB.Text;

            Parameters myParams = new Parameters();
            StringReader s = new StringReader(SEProcessor.Properties.Settings.Default.FilterSettingsLL);

            XmlSerializer xmlSerializer = new XmlSerializer(myParams.GetType());
            myParams = (Parameters)xmlSerializer.Deserialize(s);

            //Update GUI with object
            UpdateGUIwithParameters(myParams);
            VerifySequenceFiltersGroupBox();

            textBoxSQTDirectory.Text = inputDir;
            textBoxAlternativeProteinDB.Text = DBDir;
        }

        private void radioButtonAP_CheckedChanged(object sender, EventArgs e)
        {
            Parameters myParams = new Parameters();
            StringReader s = new StringReader(SEProcessor.Properties.Settings.Default.FilterSettings);

            string inputDir = textBoxSQTDirectory.Text;
            string DBDir = textBoxAlternativeProteinDB.Text;

            XmlSerializer xmlSerializer = new XmlSerializer(myParams.GetType());
            myParams = (Parameters)xmlSerializer.Deserialize(s);

            //Update GUI with object
            UpdateGUIwithParameters(myParams);
            VerifySequenceFiltersGroupBox();

            textBoxSQTDirectory.Text = inputDir;
            textBoxAlternativeProteinDB.Text = DBDir;

        }

        private void buttonLoadParameters_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Load SE Processor parameter file";
            openFileDialog1.Filter = "SEParams (*.seprms)|*.seprms";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }

            Parameters myParams = new Parameters();
            FileStream flStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);

            myParams = (Parameters)PatternTools.pTools.LoadSeializedXMLObject(openFileDialog1.FileName, myParams.GetType()); ;
            flStream.Close();

            //Update GUI with object
            UpdateGUIwithParameters(myParams);

            MessageBox.Show("Parameters loaded from a serialized xml file.");
        }

        private void buttonSerializeParameters_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "SE Processor parameter file";
            saveFileDialog1.Filter = "SEParams (*.seprms)|*.seprms";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {

                

                try
                {

                    Parameters myParams = GenerateParametersFromGUI();
                    FileStream flStream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);

                    XmlSerializer xmlSerializer = new XmlSerializer(myParams.GetType());
                    //Remmove namespaces
                    System.Xml.Serialization.XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
                    xs.Add("", "");
                    StreamWriter stWriter = new StreamWriter(flStream);
                    xmlSerializer.Serialize(stWriter, myParams, xs);
                    flStream.Close();
                    MessageBox.Show("Parameters serialized to a xml file.");
                }
                catch (Exception e5)
                {
                    MessageBox.Show(e5.Message);
                }
            }
        }

        private void cHPPNextProtAnalysisonlyForHumanSamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextProt.DisplayWindow dw = new NextProt.DisplayWindow();
            dw.MyChroBrowser.MySEProResultPackage = resultViewer1.ResultViewerResultPackage;
            dw.MyChroBrowser.PlotSEPro();
            dw.ShowDialog();
        }

        private void correlationPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Result.WindowLengthQuant w = new Result.WindowLengthQuant();
            w.MyLengthQuantCore.MyProteinList = resultViewer1.ResultViewerResultPackage.MyProteins.MyProteinList;
            w.MyLengthQuantCore.Plot();
            w.ShowDialog();

        }
    }
}
