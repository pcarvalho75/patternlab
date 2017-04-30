using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SEPRPackage;
using PatternTools.MSParserLight;
using System.Text.RegularExpressions;
using PatternTools.MSParser;
using PatternTools.SQTParser;
using PatternTools;
using PLP;
using System.Windows.Forms.DataVisualization.Charting;
using UniQ;
using PatternTools.FastaParser;

namespace SEProQ.ITRAQ
{
    public partial class ITRAQControl : UserControl
    {
        Dictionary<string, double[]> signalIdentifiedNormalizationDictionary;
        Dictionary<string, double[]> signalAllNormalizationDictionary;
        List<SQTScan> theScansToAnalyze;

        public ITRAQControl()
        {
            InitializeComponent();
        }

        private void buttonLoadPurityDefaults_Click(object sender, EventArgs e)
        {

            dataGridViewPurity.Rows.Clear();
            dataGridViewPurity.RowHeadersVisible = true;
            
            //add 114;
            int index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "114";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0;
            dataGridViewPurity.Rows[index].Cells[1].Value = 1;
            dataGridViewPurity.Rows[index].Cells[2].Value = 5.9;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0.2;

            //add 115
            index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "115";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0;
            dataGridViewPurity.Rows[index].Cells[1].Value = 2;
            dataGridViewPurity.Rows[index].Cells[2].Value = 5.6;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0.1;

            //add 116
            index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "116";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0;
            dataGridViewPurity.Rows[index].Cells[1].Value = 3;
            dataGridViewPurity.Rows[index].Cells[2].Value = 4.5;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0.1;

            //add 117
            index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "117";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0.1;
            dataGridViewPurity.Rows[index].Cells[1].Value = 4;
            dataGridViewPurity.Rows[index].Cells[2].Value = 3.5;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0.1;

        }

        private void buttonProcessITRAQ_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "SEPro file (*.sepr)|*.sepr|PepExplorer (*.mpex)|*.mpex|All files (*.*)|*.*";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBoxitraqSEPro.Text = openFileDialog1.FileName;
            }
        }


        private void buttonGo_Click(object sender, EventArgs e)
        {

            //Verify write permission to directory

            if (!Directory.Exists(textBoxOutputDirectory.Text))
            {
                MessageBox.Show("Please specify a valid output directory");
                return;
            }

            if (!Regex.IsMatch(textBoxIsobaricMasses.Text, "[0-9]+ [0-9]+"))
            {
                MessageBox.Show("Please fill out the masses of the isobaric tags.");
                return;
            }


            if (!PatternTools.pTools.HasWriteAccessToFolder(textBoxOutputDirectory.Text))
            {
                MessageBox.Show("Please specify a valid output directory");
                return;
            }

            //Obtain class labels
            if (textBoxClassLabels.Text.Length == 0)
            {
                MessageBox.Show("Please input the class labels (eg., for iTRAQ 1,2,3,4");
                return;
            }
            List<int> labels = Regex.Split(textBoxClassLabels.Text, " ").Select(a => int.Parse(a)).ToList();

            //Obtain the isobaric masses
            string[] im = Regex.Split(textBoxIsobaricMasses.Text, " ");

            List<double> isobaricMasses = im.Select(a => double.Parse(a)).ToList();
            if (labels.Count != isobaricMasses.Count)
            {
                MessageBox.Show("Please make sure that the class labels and isobaric masses match");
                return;
            }

            buttonGo.Text = "Working...";
            this.Update();

            richTextBoxLog.Clear();


            //--------------------------------------------


            //Get signal from all
            signalAllNormalizationDictionary = new Dictionary<string,double[]>();
            //if (false)
            FileInfo fi = new FileInfo(textBoxitraqSEPro.Text);
            bool extractSignal = false;
            ResultPackage rp = null;

            if (checkBoxNormalizationChannelSignal.Checked) 
            {
                
                //We should get the MS infor and merge it the the sepro package
                if (fi.Extension.Equals(".sepr"))
                {
                    rp = ResultPackage.Load(textBoxitraqSEPro.Text);
                    extractSignal = true;
                }

                List<FileInfo> rawFiles = fi.Directory.GetFiles("*.RAW").ToList();

                foreach (FileInfo rawFile in rawFiles) 
                {
                    Console.WriteLine("Extracting data for " + rawFile.Name);
                    PatternTools.RawReader.RawReaderParams rParams = new PatternTools.RawReader.RawReaderParams();
                    rParams.ExtractMS1 = false;
                    rParams.ExtractMS2 = true;
                    rParams.ExtractMS3 = false;

                    PatternTools.RawReader.Reader reader = new PatternTools.RawReader.Reader(rParams);

                    List<MSLight> theMS2 = reader.GetSpectra(rawFile.FullName, new List<int>(), false);

                    theMS2.RemoveAll(a => a.Ions == null);

                    double [] totalSignal = new double[isobaricMasses.Count];

                    List<SQTScan> theScans = null;
                    //Update the sepro result package with the signal
                    if (extractSignal)
                    {
                        //Get all the scans from this file
                        string rawName = rawFile.Name.Substring(0, rawFile.Name.Length - 4);
                        theScans = rp.MyProteins.AllSQTScans.FindAll(a => a.FileName.Substring(0, a.FileName.Length - 4).Equals(rawName));
                    }


                    foreach (MSLight ms in theMS2) 
                    {
                        double[] thisQuantitation = GetIsobaricSignal(ms.Ions , isobaricMasses);

                        if (extractSignal)
                        {
                            SQTScan scn = theScans.Find(a => a.ScanNumber == ms.ScanNumber);
                            if (scn != null)
                            {
                                scn.MSLight = ms;
                                scn.MSLight.Ions.RemoveAll(a => a.MZ > 400);
                            }
                        }

                        for (int i = 0; i < thisQuantitation.Length; i++) 
                        {
                            totalSignal[i] += thisQuantitation[i];
                        }


                    }

                    string theName = rawFile.Name.Substring(0, rawFile.Name.Length-3);
                    theName += "sqt";

                    signalAllNormalizationDictionary.Add(theName, totalSignal);
                }
            
            }


            Console.WriteLine("Loading SEPro File");

            if (!File.Exists(textBoxitraqSEPro.Text))
            {
                MessageBox.Show("Unable to find SEPro file");
                return;
            }


            #region Load the spero or pepexplorer file

            theScansToAnalyze = new List<SQTScan>();
            List<FastaItem> theFastaItems = new List<FastaItem>();

            if (fi.Extension.Equals(".sepr"))
            {
                Console.WriteLine("Loading SEPro file");

                if (!extractSignal)
                {
                    rp = ResultPackage.Load(textBoxitraqSEPro.Text);
                }
                rp.MyProteins.AllSQTScans.RemoveAll(a => a.MSLight == null);
                theScansToAnalyze = rp.MyProteins.AllSQTScans;
                Console.WriteLine("Done reading SEPro result");
                theFastaItems = rp.MyProteins.MyProteinList.Select(a => new FastaItem(a.Locus, a.Sequence, a.Description)).ToList();
            } else if (fi.Extension.Equals(".mpex"))
            {
                Console.WriteLine("Loading PepExplorer file....");
                PepExplorer2.Result2.ResultPckg2 result = PepExplorer2.Result2.ResultPckg2.DeserializeResultPackage(textBoxitraqSEPro.Text);
                theFastaItems = result.MyFasta;

                theScansToAnalyze = new List<SQTScan>();

                foreach (PepExplorer2.Result2.AlignmentResult al in result.Alignments)
                {
                    foreach (var dnr in al.DeNovoRegistries)
                    {
                        SQTScan sqt = new SQTScan();
                        sqt.ScanNumber = dnr.ScanNumber;
                        sqt.FileName = dnr.FileName;
                        sqt.PeptideSequence = dnr.PtmSequence;
                        theScansToAnalyze.Add(sqt);
                    }
                }

                //And now we need to retrieve the mass spectra.  For this, the raw files should be inside the directory containing the mpex file
                List<string> rawFiles = theScansToAnalyze.Select(a => a.FileName).Distinct().ToList();

                for (int i = 0; i < rawFiles.Count; i++)
                {
                    rawFiles[i] = rawFiles[i].Remove(rawFiles[i].Length - 3, 3);
                    rawFiles[i] = rawFiles[i] += "raw";

                }

                foreach (string fn in rawFiles)
                {
                    
                    Console.WriteLine("Retrieving spectra for file: " + fn);
                    ParserUltraLightRAW parser = new ParserUltraLightRAW();

                    string tmpFile = fn.Substring(0, fn.Length - 3);

                    List<SQTScan> scansForThisFile = theScansToAnalyze.FindAll(a => Regex.IsMatch (tmpFile, a.FileName.Substring(0, a.FileName.Length -3), RegexOptions.IgnoreCase)).ToList();

                    List<int> scnNumbers = scansForThisFile.Select(a => a.ScanNumber).ToList();

                    FileInfo theInputFile = new FileInfo(textBoxitraqSEPro.Text);

                    List<MSUltraLight> theSpectra = parser.ParseFile(theInputFile.DirectoryName + "/" + fn, -1, 2, scnNumbers);

                    foreach (SQTScan sqt in scansForThisFile)
                    {
                        MSUltraLight spec = theSpectra.Find(a => a.ScanNumber == sqt.ScanNumber);
                        sqt.MSLight = new MSLight();
                        sqt.MSLight.MZ = spec.Ions.Select(a => (double)a.Item1).ToList();
                        sqt.MSLight.Intensity = spec.Ions.Select(a => (double)a.Item2).ToList();
                    }

                    Console.WriteLine("\tDone processing this file.");

                }
                

            } else
            {
                throw new Exception("This file format is not supported.");
            }

            #endregion


            //Obtaining multiplexed spectra         
            SEProQ.IsobaricQuant.YadaMultiplexCorrection.YMC ymc = null;
            if (textBoxCorrectedYadaDirectory.Text.Length > 0)
            {
                Console.WriteLine("Reading Yada results");
                ymc = new IsobaricQuant.YadaMultiplexCorrection.YMC(new DirectoryInfo(textBoxCorrectedYadaDirectory.Text));
                Console.WriteLine("Done loading Yada results");
            }
            
            //Remove multiplexed spectra from sepro results
            if (textBoxCorrectedYadaDirectory.Text.Length > 0)
            {
                int removedCounter = 0;
                
                foreach (KeyValuePair<string, List<int>> kvp in ymc.fileNameScanNumberMultiplexDictionary)
                {
                    Console.WriteLine("Removing multiplexed spectra for file :: " + kvp.Key);
                    richTextBoxLog.AppendText("Removing multiplexed spectra for file :: " + kvp.Key+ "\n");

                    string cleanName = kvp.Key.Substring(0, kvp.Key.Length - 4);
                    cleanName += ".sqt";
                    foreach (int scnNo in kvp.Value)
                    {
                        int index = theScansToAnalyze.FindIndex(a => a.ScanNumber == scnNo && a.FileName.Equals(cleanName));
                        if (index >= 0)
                        {
                            Console.Write(theScansToAnalyze[index].ScanNumber + " ");
                            richTextBoxLog.AppendText(theScansToAnalyze[index].ScanNumber + " ");
                            
                            removedCounter++;
                            theScansToAnalyze.RemoveAt(index);
                        }
                    }

                    Console.WriteLine("\n");
                    richTextBoxLog.AppendText("\n");
                }

                Console.WriteLine("Done removing multiplexed spectra :: " + removedCounter);
            }
            

            PatternTools.CSML.Matrix correctionMatrix = new PatternTools.CSML.Matrix();
            if (checkBoxApplyPurityCorrection.Checked)
            {
                List<List<double>> correctionData = GetPurityCorrectionsFromForm();
                correctionMatrix = IsobaricQuant.IsobaricImpurityCorrection.GenerateInverseCorrectionMatrix(correctionData);
            }


            //--------------------------------------------------------------------------------------------------------------------

            //Prepare normalization Dictionary
            signalIdentifiedNormalizationDictionary = new Dictionary<string, double[]>();

            List<string> fileNames = theScansToAnalyze.Select(a => a.FileName).Distinct().ToList();

            foreach (string fileName in fileNames)
            {
                signalIdentifiedNormalizationDictionary.Add(fileName, new double[isobaricMasses.Count]);
            }
            //-------------------------------------

            

            //If necessary, correct for impurity and feed global signal dictionary           
            foreach (SQTScan scn in theScansToAnalyze)
            {

                double[] thisQuantitation = GetIsobaricSignal(scn.MSLight.Ions, isobaricMasses);

                double maxSignal = thisQuantitation.Max();

                //We can only correct for signal for those that have quantitation values in all places    
                if (checkBoxApplyPurityCorrection.Checked && (thisQuantitation.Count(a => a > maxSignal * (double)numericUpDownIonCountThreshold.Value) == isobaricMasses.Count))
                {
                    thisQuantitation = IsobaricQuant.IsobaricImpurityCorrection.CorrectForSignal(correctionMatrix, thisQuantitation).ToArray();
                }

                if (checkBoxNormalizationChannelSignal.Checked)
                {
                    for (int k = 0; k < thisQuantitation.Length; k++)
                    {
                        signalIdentifiedNormalizationDictionary[scn.FileName][k] += thisQuantitation[k];
                    }
                }

                scn.Quantitation = new List<List<double>>() { thisQuantitation.ToList() };
            }

            //And now normalize -------------------

            if (checkBoxNormalizationChannelSignal.Checked) {

                Console.WriteLine("Performing channel signal normalization for " + theScansToAnalyze.Count + " scans.");

                foreach (SQTScan scn2 in theScansToAnalyze)
                {
                    for (int m = 0; m < isobaricMasses.Count; m++)
                    {
                        scn2.Quantitation[0][m] /= signalIdentifiedNormalizationDictionary[scn2.FileName][m];
                    }

                    if (scn2.Quantitation[0].Contains(double.NaN))
                    {
                        Console.WriteLine("Problems on signal of scan " + scn2.FileNameWithScanNumberAndChargeState);
                    }
                }
            }

            comboBoxSelectFileForGraphs.Items.Clear();
            foreach (string file in signalIdentifiedNormalizationDictionary.Keys.ToList())
            {
                comboBoxSelectFileForGraphs.Items.Add(file);
            }


            tabControlMain.SelectedIndex = 1;


            if (radioButtonAnalysisPeptideReport.Checked)
            {

                //Peptide Analysis

                //Write Peptide Analysis
                StreamWriter sw = new StreamWriter(textBoxOutputDirectory.Text + "/" + "PeptideQuantitationReport.txt");

                //Eliminate problematic quants
                int removed = theScansToAnalyze.RemoveAll(a => Object.ReferenceEquals(a.Quantitation, null));
                Console.WriteLine("Problematic scans removed: " + removed);

                var pepDic = from scn in theScansToAnalyze
                             group scn by scn.PeptideSequenceCleaned
                             
                             
                             into groupedSequences
                             select new { PeptideSequence = groupedSequences.Key, TheScans = groupedSequences.ToList() };

                foreach (var pep in pepDic)
                {
                    sw.WriteLine("Peptide:" + pep.PeptideSequence + "\tSpecCounts:" + pep.TheScans.Count);

                    foreach (SQTScan sqt in pep.TheScans)
                    {
                        sw.WriteLine(sqt.FileNameWithScanNumberAndChargeState + "\t" + string.Join("\t", sqt.Quantitation[0]));    
                    }
                }


                //And now write the Fasta
                sw.WriteLine("#Fasta Items");
                foreach (FastaItem fastaItem in theFastaItems)
                {
                    sw.WriteLine(">" + fastaItem.SequenceIdentifier + " " + fastaItem.Description);
                    sw.WriteLine(fastaItem.Sequence);
                }

                sw.Close();

            }
            else
            {

                rp = ResultPackage.Load(textBoxitraqSEPro.Text);

                //Peptide Level
                if (true)
                {
                    PatternTools.SparseMatrixIndexParserV2 ip = new SparseMatrixIndexParserV2();
                    List<int> allDims = new List<int>();
                    List<PeptideResult> peptides = rp.MyProteins.MyPeptideList;

                    if (checkBoxOnlyUniquePeptides.Checked)
                    {
                        int removedPeptides = peptides.RemoveAll(a => a.MyMapableProteins.Count > 1);
                        Console.WriteLine("Removing {0} peptides for not being unique.", removedPeptides);
                    }

                    for (int i = 0; i < peptides.Count; i++)
                    {
                        SparseMatrixIndexParserV2.Index index = new SparseMatrixIndexParserV2.Index();
                        index.Name = peptides[i].PeptideSequence;
                        index.Description = string.Join(" ", peptides[i].MyMapableProteins);
                        index.ID = i;

                        ip.Add(index, true);
                        allDims.Add(i);
                    }

                    SparseMatrix sm = new SparseMatrix();

                    List<int> dims = ip.allIDs();


                    for (int l = 0; l < labels.Count; l++)
                    {
                        if (labels[l] < 0) { continue; }

                        sparseMatrixRow smr = new sparseMatrixRow(labels[l]);
                        List<double> values = new List<double>(dims.Count);

                        List<int> dimsWithValues = new List<int>();

                        foreach (int d in dims)
                        {
                            List<SQTScan> scns = peptides[d].MyScans.FindAll(a => !object.ReferenceEquals(a.Quantitation, null));

                            if (scns.Count > 0)
                            {
                                double signalSum = scns.FindAll(a => !double.IsNaN(a.Quantitation[0][l])).Sum(a => a.Quantitation[0][l]);
                                values.Add(signalSum);
                                dimsWithValues.Add(d);
                            } 
                        }


                        smr.Dims = dimsWithValues;
                        smr.Values = values;
                        smr.FileName = isobaricMasses[l].ToString();

                        sm.addRow(smr);

                    }

                    PatternLabProject plp = new PatternLabProject(sm, ip, "IsobaricQuant");
                    plp.Save(textBoxOutputDirectory.Text + "/MyPatternLabProjectPeptides.plp");

                }

                //Protein Level
                if (true)
                {
                    //Generate Index
                    PatternTools.SparseMatrixIndexParserV2 ip = new SparseMatrixIndexParserV2();

                    List<MyProtein> theProteins = rp.MyProteins.MyProteinList;

                    if (checkBoxOnlyUniquePeptides.Checked)
                    {
                        int removedProteins = theProteins.RemoveAll(a => !a.PeptideResults.Exists(b => b.NoMyMapableProteins == 1));
                        Console.WriteLine("{0} removed proteins for not having unique peptides", removedProteins);
                    }

                    for (int i = 0; i < theProteins.Count; i++)
                    {
                        SparseMatrixIndexParserV2.Index index = new SparseMatrixIndexParserV2.Index();
                        index.ID = i;
                        index.Name = theProteins[i].Locus;
                        index.Description = theProteins[i].Description;

                        ip.Add(index, false);

                    }

                    //SparseMatrix
                    SparseMatrix sm = new SparseMatrix();

                    List<int> dims = ip.allIDs();

                    for (int l = 0; l < labels.Count; l++)
                    {
                        if (labels[l] < 0) { continue; }

                        if (!sm.ClassDescriptionDictionary.ContainsKey(labels[l]))
                        {
                            sm.ClassDescriptionDictionary.Add(labels[l], labels[l].ToString());
                        }
                        
                        sparseMatrixRow smr = new sparseMatrixRow(labels[l]);
                        List<double> values = new List<double>(dims.Count);

                        List<int> dimsToInclude = new List<int>();

                        foreach (int d in dims)
                        {
                                double signalSum = 0;

                                List<PeptideResult> thePeptides = theProteins[d].PeptideResults;

                                if (checkBoxOnlyUniquePeptides.Checked)
                                {
                                    thePeptides.RemoveAll(a => a.MyMapableProteins.Count > 1);
                                }

                                foreach (PeptideResult pr in thePeptides)
                                {
                                    List<SQTScan> scns = pr.MyScans.FindAll(a => !object.ReferenceEquals(a.Quantitation, null));

                                    foreach (SQTScan sqt in scns)
                                    {
                                        if (!double.IsNaN(sqt.Quantitation[0][l]) && !double.IsInfinity(sqt.Quantitation[0][l]))
                                        {
                                            signalSum += sqt.Quantitation[0][l];
                                        }
                                    } 
                                }

                                if (signalSum > 0)
                                {
                                    dimsToInclude.Add(d);
                                    values.Add(signalSum);
                                } else
                                {
                                    Console.WriteLine("No signal found for " + theProteins[d].Locus + " on marker " + l);
                                }
                            }

                            smr.Dims = dims;
                            smr.Values = values;
                            smr.FileName = isobaricMasses[l].ToString();

                            sm.addRow(smr);

                        }


                    PatternLabProject plp = new PatternLabProject(sm, ip, "IsobaricQuant");
                    plp.Save(textBoxOutputDirectory.Text + "/MyPatternLabProjectProteins.plp");
                }

            }

            comboBoxSelectFileForGraphs.Enabled = true;
            tabControlMain.SelectedIndex = 2;
            Console.WriteLine("Done");
            buttonGo.Text = "Generate Report";
            
        }

 
        private void buttonMassesItraq_Click(object sender, EventArgs e)
        {
            textBoxIsobaricMasses.Text = "114.1 115.1 116.1 117.1";
            textBoxClassLabels.Text = "1 2 3 4";
        }

        private void buttonMassesTMT_Click(object sender, EventArgs e)
        {
            textBoxIsobaricMasses.Text = "126.1277 127.1247 128.1344 129.1315 130.1411 131.1382";
            textBoxClassLabels.Text = "1 1 1 2 2 2";
        }

        private void buttonLoadTMTDefaults_Click(object sender, EventArgs e)
        {
            dataGridViewPurity.Rows.Clear();
            dataGridViewPurity.RowHeadersVisible = true;

            //add 126;
            int index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "126";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0;
            dataGridViewPurity.Rows[index].Cells[1].Value = 0.1;
            dataGridViewPurity.Rows[index].Cells[2].Value = 9.4;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0.6;

            //add 127
            index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "127";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0;
            dataGridViewPurity.Rows[index].Cells[1].Value = 0.5;
            dataGridViewPurity.Rows[index].Cells[2].Value = 6.7;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0;

            //add 128
            index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "128";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0.1;
            dataGridViewPurity.Rows[index].Cells[1].Value = 1.1;
            dataGridViewPurity.Rows[index].Cells[2].Value = 4.2;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0;

            //add 129
            index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "129";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0;
            dataGridViewPurity.Rows[index].Cells[1].Value = 1.7;
            dataGridViewPurity.Rows[index].Cells[2].Value = 4.1;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0;

            //add 130
            index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "130";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0;
            dataGridViewPurity.Rows[index].Cells[1].Value = 2.8;
            dataGridViewPurity.Rows[index].Cells[2].Value = 2.5;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0;

            //add 131
            index = dataGridViewPurity.Rows.Add();
            dataGridViewPurity.Rows[index].HeaderCell.Value = "131";
            dataGridViewPurity.Rows[index].Cells[0].Value = 0.1;
            dataGridViewPurity.Rows[index].Cells[1].Value = 4.1;
            dataGridViewPurity.Rows[index].Cells[2].Value = 4.7;
            dataGridViewPurity.Rows[index].Cells[3].Value = 0.1;
        }

        private List<List<double>> GetPurityCorrectionsFromForm()
        {
            List<List<double>> correction = new List<List<double>>();

            for (int i = 0; i < dataGridViewPurity.Rows.Count -1; i++)
            {
                List<double> row = new List<double>();
                for (int j = 0; j < dataGridViewPurity.Rows[i].Cells.Count; j++)
                {
                    if (j == 2)
                    {
                        //We need to add the monoisotopic mass as -1 a requirement
                        row.Add(-1);
                    }
                    double v = double.Parse(dataGridViewPurity.Rows[i].Cells[j].Value.ToString());
                    row.Add(v);
                }
                correction.Add(row);

            }

            return correction;
        }

        private void checkBoxApplyPurityCorrection_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxApplyPurityCorrection.Checked)
            {
                dataGridViewPurity.Enabled = true;
                buttonLoadPurityDefaultsITRAQ.Enabled = true;
                buttonLoadTMTDefaults.Enabled = true;
                
            }
            else
            {
                dataGridViewPurity.Enabled = false;
                buttonLoadPurityDefaultsITRAQ.Enabled = false;
                buttonLoadTMTDefaults.Enabled = false;
            }
        }

        private void textBoxitraqSEPro_TextChanged(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(textBoxitraqSEPro.Text);

            if (PatternTools.pTools.HasWriteAccessToFolder(fi.Directory.FullName))
            {
                textBoxOutputDirectory.Text = fi.Directory.FullName;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            UniQ.WindowUniQ uniq = new WindowUniQ();
            uniq.ThisUniQC.MyClassLabelsTextBoxText = textBoxClassLabels.Text;
            uniq.ThisUniQC.MyPeptideQuantitationReportTextBoxText = textBoxOutputDirectory.Text + "/" + "PeptideQuantitationReport.txt";
            uniq.ShowDialog();
        }


        private void buttonBrowseYada_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBoxCorrectedYadaDirectory.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonCombinePeptidePValues_Click(object sender, EventArgs e)
        {
            UniQ.RatioOfRatio rr = new RatioOfRatio();
            rr.ShowDialog();
            
        }



        private void buttonMassesItraq8_Click(object sender, EventArgs e)
        {
            textBoxIsobaricMasses.Text = "113.11 114.11 115.11 116.11 117.11 118.11 119.12 121.12";
            textBoxClassLabels.Text = "1 1 1 1 2 2 2 2";
        }

        private double[] GetIsobaricSignal(List<Ion> theIons, List<double> isoMasses)
        {
            double[] sig = new double[isoMasses.Count];

            for (int i = 0; i < sig.Length; i++)
            {
                List<Ion> acceptableIons = theIons.FindAll(a => Math.Abs(PatternTools.pTools.PPM(a.MZ, isoMasses[i])) < (double)numericUpDownTMSPPM.Value && a.Intensity > (double)numericUpDownIonCountThreshold.Value);

                double sum = 0;
                if (acceptableIons.Count > 0)
                {
                    sum = acceptableIons.Sum(a => a.Intensity);
                }
                sig[i] = sum;
            }

            return sig;
        }


        private void comboBoxSelectFileForGraphs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Lets update the graphs
            chartSignalPerChannel.Series[0].Points.Clear();
            chartSignalPerChannel.Series[1].Points.Clear();
            chartSignalPerChannel.Series[2].Points.Clear();

            if (comboBoxSelectFileForGraphs.Text.Equals("Select file"))
            {
                return;
            }

            string selectedFile = comboBoxSelectFileForGraphs.Text;


            //The unnormalized signal
            foreach (double d in signalIdentifiedNormalizationDictionary[selectedFile])
            {
                chartSignalPerChannel.Series[0].Points.Add(Math.Round(d, 0));
            }

            List<SQTScan> scansFromFile = theScansToAnalyze.FindAll(a => a.FileName.Equals(selectedFile));

            double[] points = new double[signalIdentifiedNormalizationDictionary[selectedFile].Length];

            for (int i = 0; i < points.Length; i++)
            {
                double sum = scansFromFile.Sum(a => a.Quantitation[0][i]);
                chartSignalPerChannel.Series[1].Points.Add(sum);
                points[i] = sum;
            }

            //And the signal from the raw files
            if (signalAllNormalizationDictionary.ContainsKey(selectedFile))
            {
                for (int i = 0; i < points.Length; i++)
                {
                    chartSignalPerChannel.Series[2].Points.Add(signalAllNormalizationDictionary[selectedFile][i]);
                }
            }
            
        }

        private void chartSignalPerChannel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();

                MenuItem mi1 = new MenuItem("Save as PNG");
                mi1.Click += mi1_Click;

                MenuItem mi2 = new MenuItem("Copy to clipboard");
                mi2.Click += (send, e2) => mi2_Click(send, e2, chartSignalPerChannel);
                
                cm.MenuItems.Add(mi1);

                chartSignalPerChannel.ContextMenu = cm;
                cm.Show(chartSignalPerChannel, new System.Drawing.Point(e.X, e.Y));


            }
        }

        void mi2_Click(object sender, EventArgs e, Chart c)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                c.SaveImage(ms, ChartImageFormat.Bmp);
                Bitmap bm = new Bitmap(ms);
                Clipboard.SetImage(bm);
            }
        }

        void mi1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG File (.png)|*.png";

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                //Save here
                chartSignalPerChannel.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
                MessageBox.Show("Image saved.");
            }
        }

        private void radioButtonSparseMatrix_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSparseMatrix.Checked)
            {
                checkBoxOnlyUniquePeptides.Enabled = true;
            } else
            {
                checkBoxOnlyUniquePeptides.Enabled = false;
            }
        }

        private void buttonTMT10_Click(object sender, EventArgs e)
        {
            textBoxIsobaricMasses.Text = "126.127726 127.124761 127.131081 128.128116 128.134436 129.131471 129.137790 130.134825 130.141145 131.138180";
            textBoxClassLabels.Text = "1 1 1 1 1 2 2 2 2 2";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }

}
