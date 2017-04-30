using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PatternTools;
using System.IO;
using SEPRPackage;
using PatternTools.SQTParser;
using Regrouper.GroupingElements;
using Regrouper.GroupingElements.Parser;
using System.Text.RegularExpressions;
using System.Threading;
using System.Deployment.Application;
using HMMVerifier;
using HMMVerifier.HMM;
using XQuant;

namespace Regrouper
{
    public partial class RegrouperControl : UserControl
    {

        IParser theParser; // The project parser used for generating sparce matrices

        static Dominomics.Dominomics dominomics;

        public RegrouperControl()
        {
            InitializeComponent();
        }

        public Parameters GetParametersFromGUI()
        {
            Parameters p = new Parameters();
            p.EliminateDecoys = checkBoxEliminateDecoys.Checked;
            p.UseMaximumParsimony = checkBoxUseMaxParsimony.Checked;
            p.DecoyTag = textBoxDecoyTag.Text;

            if (radioButtonFormatProtein.Checked)
            {
                p.MyProteinType = (ProteinOutputType)Enum.Parse(typeof(ProteinOutputType), comboBoxProteinContent.SelectedItem.ToString());
            }

            if (radioButtonFormatUniquePeptide.Checked)
            {
                p.PeptideParserOnlyUniquePeptides = true;
            }
            else
            {
                p.PeptideParserOnlyUniquePeptides = false;
            }

            return p;
        }



        private void buttonGo_Click(object sender, EventArgs e)
        {
            //Check if we have fileNames in the sparse matrix and index text box

            if (radioButtonFormatProtein.Checked && comboBoxProteinContent.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a protein quantitation type");
                return;
            }

            buttonGo.Text = "Processing!";
            this.Update();


            toolStripStatusLabel2.Text = "Working...";
            this.Update();
            DateTime beginTime = DateTime.Now;

            if (radioButtonSEPro.Checked)
            {

                if (radioButtonFormatPeptide.Checked || radioButtonFormatUniquePeptide.Checked)
                {
                    PeptideParser pp = new PeptideParser();
                    pp.MyParameters = GetParametersFromGUI();
                    theParser = pp;
                }
                else
                {
                    ProteinParser pp = new ProteinParser();
                    pp.UseNSAF = checkBoxNSAF.Checked;
                    pp.UseMaxParsimony = checkBoxUseMaxParsimony.Checked;
                    pp.MyParameters = GetParametersFromGUI();
                    theParser = pp;
                    
                }
            }
            else if (radioButtonPepExplorer.Checked)
            {
                PepExplorerParser pp = new PepExplorerParser();
                theParser = pp;
            }
            else
            {
                MessageBox.Show("Please select a software (SEPro / PepExplorer)");
                return;
            }

            theParser.ParseDirs(multipleDirectorySelector1.MyDirectoryDescriptionDictionary);
            theParser.ProcessParsedData();

            buttonGo.Text = "Go!";

            if (radioButtonFormatProtein.Checked)
            {
                GenerateBirdsEyeView();
                buttonSaveBirdsEyeView.Enabled = true;
            }

            buttonSavePatternLabProject.Enabled = true;

            toolStripStatusLabel2.Text = "Processing time: " + Math.Ceiling((DateTime.Now - beginTime).TotalMilliseconds).ToString() + " ms.";
        }
            

        private void radioButtonFormatProtein_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFormatProtein.Checked)
            {
                comboBoxProteinContent.Enabled = true;
            }
            else
            {
                comboBoxProteinContent.Enabled = false;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            multipleDirectorySelector1.VerifyExtension = "*.sepr";

        }

        private void buttonSEProQProcessing_Click(object sender, EventArgs e)
        {
            ////Make sure directory exists
            //if (!Directory.Exists(textBoxSEProQ.Text))
            //{
            //    if (MessageBox.Show("This directory does not exist.  Do you want me to create it?", "Directory does not exist", MessageBoxButtons.YesNoCancel) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        try
            //        {
            //            Directory.CreateDirectory(textBoxSEProQ.Text);
            //        }
            //        catch (Exception e2)
            //        {
            //            MessageBox.Show(e2.Message);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
                
            //}

            //buttonSEProQProcessing.Text = "Working...";

            //ProteinParser pp = new ProteinParser();
            //pp.MyParameters = GetParametersFromGUI();
            //Console.WriteLine("Loading files.");
            //pp.ParseDirs(multipleDirectorySelector1.MyDirectoryDescriptionDictionary);

            //if (checkBoxDNIAFOnlyPtnWithUniquePeptide.Checked)
            //{
            //    foreach (ResultEntry re in pp.MyResultPackages)
            //    {
            //        re.MyResultPackage.MyProteins.MyProteinList.RemoveAll(a => a.ContainsUniquePeptide == 0);
            //    }
            //}

            ////This will build the index
            //Console.WriteLine("Building protein index");
            //pp.ProcessParsedData();

            //SparseMatrix sm = new SparseMatrix();


            ////For each experiment we need to do the dNSAF and build the sparse matrix
            //foreach (ResultEntry r in pp.MyResultPackages)
            //{
            //    Console.WriteLine("Analyzing file " + r.MyFileInfo.Name);
            //    this.Update();

            //    //Check if it was SEProQued
            //    if (!r.MyResultPackage.MyProteins.MyPeptideList.Exists(a => a.SeproQ > 1))
            //    {
            //        throw new Exception("File " + r.MyFileInfo.Name + " was not analyzed by SEProQ");
            //    }

            //    List<PeptideResult> quantitatedPeptides = r.MyResultPackage.MyProteins.MyPeptideList.FindAll(a => a.SeproQ > 0);
                
            //    List<string> allProteinLocci = (from prot in r.MyResultPackage.MyProteins.MyProteinList
            //                                   select prot.Locus).ToList();

            //    Console.WriteLine("Quantitated peptides: " + quantitatedPeptides.Count + " / " + r.MyResultPackage.MyProteins.MyPeptideList.Count);
            //    Console.WriteLine("Unique peptides quantitated: " + quantitatedPeptides.Count(a => a.NoMyMapableProteins == 1));

            //    List<DNSAFGroupingItem> dnsafs = new List<DNSAFGroupingItem>(r.MyResultPackage.MyProteins.MyProteinList.Count);

            //    //As a first step we need to add the unique signal to each protein
            //    Console.WriteLine("Computing unique signal for each protein");
            //    foreach (MyProtein thisProtein in r.MyResultPackage.MyProteins.MyProteinList)
            //    {
            //        //Console.WriteLine("Analyzing protein " + thisProtein.Locus);
            //        //Calculate unique signal for this protein
            //        double uSpCi = 0;
            //        List<PeptideResult> uniquePeptides = quantitatedPeptides.FindAll(a => a.MyMapableProteins.Contains(thisProtein.Locus) && a.MyMapableProteins.Count == 1).Distinct().ToList();                    
                    
            //        if (uniquePeptides.Count > 0) {
            //            uSpCi += uniquePeptides.Sum(a => a.SeproQ);
            //            //Console.WriteLine(" Unique peptides: " + uniquePeptides.Count + " " + uSpCi);
            //        }

            //        dnsafs.Add(new DNSAFGroupingItem(thisProtein.Locus, pp.TheIndex.FindIndex(a => a.Locus.Equals(thisProtein.Locus)) + 1, uSpCi));
 
            //    }

            //    //Now lets find the signal for the shared peptides
            //    foreach (MyProtein thisProtein in r.MyResultPackage.MyProteins.MyProteinList)
            //    {
            //        DNSAFGroupingItem thisDnsaf = dnsafs.Find(a => a.Locus.Equals(thisProtein.Locus));

            //        Console.WriteLine("Analyzing protein " + thisProtein.Locus);
            //        List<PeptideResult> distributedPeptides = quantitatedPeptides.FindAll(a => a.MyMapableProteins.Contains(thisProtein.Locus) && a.MyMapableProteins.Count > 1).Distinct().ToList();
            //        Console.WriteLine(" Shared peptides: " + distributedPeptides.Count);
                                        

            //        double sspc = 0;
            //        foreach (PeptideResult peptide in distributedPeptides)
            //        {
            //            //Find all proteins that share this peptide
            //            List<MyProtein> proteinsSharingThisPeptide = r.MyResultPackage.MyProteins.MyProteinList.FindAll(a => a.DistinctPeptides.Contains(peptide.CleanedPeptideSequence));
            //            Console.WriteLine(" Simmilar proteins (including this one) sharing this {0} peptide: {1}", peptide.CleanedPeptideSequence, proteinsSharingThisPeptide.Count);

            //            //And get there dnsadgroupingitems
            //            List<string> locciOfProteinsSharingOnePeptide = proteinsSharingThisPeptide.Select(a => a.Locus).ToList();
            //            List<DNSAFGroupingItem> dnsafOfSimmilarProteins = dnsafs.FindAll(a => locciOfProteinsSharingOnePeptide.Contains(a.Locus));

            //            double sumOfUniqueCurrentOfSimmilarProteins = dnsafOfSimmilarProteins.Sum(a => a.USpC);
            //            double distributionFactor = thisDnsaf.USpC / sumOfUniqueCurrentOfSimmilarProteins;

            //            if (sumOfUniqueCurrentOfSimmilarProteins < 1)
            //            {
            //                distributionFactor = 1 / (double)proteinsSharingThisPeptide.Count;
            //            }

            //            sspc += distributionFactor * peptide.SeproQ;
            //        }
                     
                   

            //        thisDnsaf.DSAF = (thisDnsaf.USpC + sspc) / (double)thisProtein.Sequence.Length;

            //        Console.WriteLine(" NSAF: " + thisDnsaf.DSAF);

            //    }

            //    //Finally, compute the dNSAF
            //    double totalDSAF = dnsafs.Sum(a => a.DSAF);

            //    foreach (DNSAFGroupingItem d in dnsafs)
            //    {
            //        d.DNSAF = d.DSAF / totalDSAF;
            //        Console.WriteLine(" " + d.Locus + " dNSAF = " + d.DNSAF);
            //    }

            //    //We can now compose a sparse matrix row for this guy
            //    sparseMatrixRow sr = new sparseMatrixRow(r.ClassLabel);
            //    sr.FileName = r.MyFileInfo.FullName;
            //    sr.Dims = new List<int>();
            //    sr.Values = new List<double>();

            //    for (int i = 0; i < pp.TheIndex.Count; i++)
            //    {
            //        int index = dnsafs.FindIndex(a => a.Locus.Equals(pp.TheIndex[i].Locus));

            //        if (index > -1)
            //        {
            //            if (dnsafs[index].DSAF > 1)
            //            {
            //                sr.Dims.Add(dnsafs[index].Index);
            //                sr.Values.Add(Math.Round(dnsafs[index].DNSAF, 10));

            //            }
            //        }
            //    }

            //    sm.addRow(sr);

            //}

            //sm.ClassDescriptionDictionary = new Dictionary<int, string>();
            //foreach (var v in pp.MyDirectoryDescriptionDictionary)
            //{
            //    sm.ClassDescriptionDictionary.Add(v.ClassLabel, v.MyDirectoryFullName);
            //}

            ////Now we need to do a final correction as there are index items that will not have any quantitation, so we need to remove these guys
            //pp.VerifyIntegrity(sm);


            //sm.saveMatrix(textBoxSEProQ.Text + "/SparseMatrix.txt");
            //pp.SaveIndex(textBoxSEProQ.Text + "/Index.txt");

            //buttonSEProQProcessing.Text = "Process";

            //Console.WriteLine("Done!");



        }

        //----------------------------------------------------------------------

        private void buttonPredictDomainsWithFioCloud_Click(object sender, EventArgs e)
        {

            if (textBoxSavePLP.Text.Equals("") || textBoxUnmappedDomains.Text.Equals(""))
            {
                MessageBox.Show("Please specify sparse matrix and index file before launching process");
                return;
            }

            Dominomics.InputFormat inputF;

            if (radioButtonProjectTypeSEPro.Checked)
            {
                inputF = Dominomics.InputFormat.SEPro;
            } 
            else if (radioButtonProjectTypePepExplorer.Checked)
            {
                inputF = Dominomics.InputFormat.MPex;
            } 
            else
            {
                throw new Exception ("Result file format is invalid.  Should be either .sepr or .pex");
            }

            dominomics = new Dominomics.Dominomics(multipleDirectorySelector1, inputF);

            //Read all SEPro Files to obtain a dictionary of ID (key) and fasta (value) and peptide (key) SeproFiles (values)
            progressBar1.Value = 0;
            

            //Fetch domains
            labelDominomicsStatus.Text = "Fetching domains";
            this.Update();

            groupBoxDominomics.Enabled = false;

            backgroundWorkerDominomics.RunWorkerAsync();
            timer1.Start();           

        }

        //--------------------------------------------------


        //-----------------------------------------------------

        private void backgroundWorkerDominomics_DoWork(object sender, DoWorkEventArgs e)
        {
            dominomics.DoWork((int)numericUpDownDomainIEValue.Value, (int)numericUpDownDomainEvalue.Value);
        }


        private void buttonUnmappedDomains_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog1.Title = "Save Unmapped Domains file";
            saveFileDialog1.FileName = "UnmappedDomains";
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxUnmappedDomains.Text = saveFileDialog1.FileName;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = BatchDomainFetcher.Percent;
            this.Update();
        }

        private void backgroundWorkerDominomics_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Stop();
            //When worker finished
            progressBar1.Value = 100;

            labelDominomicsStatus.Text = "Mapping peptides to Domains";
            Console.WriteLine("Mapping peptides to Domains");
            this.Update();

            //Filter step
            //Depricated, this is now done at the server side to reduce internet traffic.

            dominomics.PrepareIndexAndSparseMatrix();

            labelDominomicsStatus.Text = "Saving peptides not mapped to domains";
            this.Update();
            dominomics.SaveIndexWithNoMappedDomains(textBoxUnmappedDomains.Text);


            labelDominomicsStatus.Text = "Saving PatternLab project file";
            this.Update();

            dominomics.SavePatternLabProject(textBoxSavePLP.Text, "Dominomics");
            

            Console.WriteLine("Done");
            labelDominomicsStatus.Text = "Idle";
            groupBoxDominomics.Enabled = true;
        }


        private void buttonLoad2_Click(object sender, EventArgs e)
        {
            //Check if all directories exist
            foreach (DirectoryClassDescription myDir in multipleDirectorySelector1.MyDirectoryDescriptionDictionary)
            {
                if (!Directory.Exists(myDir.MyDirectoryFullName))
                {
                    MessageBox.Show("I cant find directory: " + myDir.MyDirectoryFullName);
                    return;
                }

                //and make sure we have at least one Sepro file in each directory
                List<FileInfo> fileInfo = new DirectoryInfo(myDir.MyDirectoryFullName).GetFiles("*.sepr", SearchOption.AllDirectories).ToList();
                fileInfo.AddRange(new DirectoryInfo(myDir.MyDirectoryFullName).GetFiles("*.mpex", SearchOption.AllDirectories).ToList());

                if (fileInfo.Count == 0)
                {
                    MessageBox.Show("There are no SEPro or mpex files in the directory : " + myDir.MyDirectoryFullName);
                    return;
                }

            }

            //Test if there are cells with no descriptions
            bool notVerified = true;
            foreach (DirectoryClassDescription myDir in multipleDirectorySelector1.MyDirectoryDescriptionDictionary)
            {
                if (myDir.Description.Equals(""))
                {
                    if (notVerified)
                    {
                        notVerified = false;
                        if (MessageBox.Show("There are still classes with no description.  Do you wish to proceed anyway?", "Class description", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                }
            }
            splitContainerSpecCount.Enabled = true;
            parametersXQuant.Enabled = true;
            groupBoxDominomics.Enabled = true;
            buttonProcessXIC.Enabled = true;

        }

        private void buttonGenerateSummary_Click(object sender, EventArgs e)
        {
            SEProSummary ss = new SEProSummary();

            ss.MyRichTextBox.AppendText("\tFileName\t\tMass Spectra\t(unique/decoy/total)Peptides\tProteins\tMax Parsimony Proteins\n\n");
            foreach (DirectoryClassDescription cdd in multipleDirectorySelector1.MyDirectoryDescriptionDictionary)
            {
                ss.MyRichTextBox.AppendText(new DirectoryInfo(cdd.MyDirectoryFullName).Name + "\t" + cdd.Description + "\n");

                Console.WriteLine("SEPro files found in directory: " + new DirectoryInfo(cdd.MyDirectoryFullName).Name);
                List<FileInfo> files = new DirectoryInfo(cdd.MyDirectoryFullName).GetFiles("*.sepr", SearchOption.AllDirectories).ToList();
   
                foreach (FileInfo fi in files)
                {
                    Console.WriteLine("\tLoading " + fi.Name);
                    ResultPackage sepro =  ResultPackage.Load(fi.FullName);
                    
                    ss.MyRichTextBox.AppendText("\t" + fi.Name + "\t\t" + sepro.MyFDRResult.SpectraFDRLabel + "\t" +  sepro.MyProteins.MyPeptideList.Count(a => a.MyMapableProteins.Count== 1) + "/" + sepro.MyFDRResult.PeptideFDRLabel + "\t" + sepro.MyFDRResult.ProteinFDRLabel + "\t" + sepro.MaxParsimonyList().Count + "\n");
                }
                
            }

            ss.ShowDialog();
            
        }

        private void buttonSEProQ2_Click(object sender, EventArgs e)
        {  

            saveFileDialog1.Filter = "XIC Files (xic)|*.xic";
        

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                buttonProcessXIC.Text = "Working";
                this.Update();
                XQuant.XQuantClusteringParameters cp = parametersXQuant.MyXQuantClusteringParameters;
                cp.MinMS1Counts = 1;
                cp.RetainOptimal = true;
                cp.OnlyUniquePeptides = false;
                cp.MaxParsimony = false;

                Core35 XICAnalyzer = new Core35(multipleDirectorySelector1.MyDirectoryDescriptionDictionary, cp);

                XICAnalyzer.Serialize(saveFileDialog1.FileName);

                MessageBox.Show("XIC file saved successfuly", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                buttonProcessXIC.Text = "Process XIC";
            }
        }

        private void GenerateBirdsEyeView()
        {
            if (radioButtonPepExplorer.Checked)
            {
                buttonSaveBirdsEyeView.Enabled = false;
                return;
            }
            else
            {
                buttonSaveBirdsEyeView.Enabled = true;
            }



            ProteinParser pp = (ProteinParser)theParser;

            double[,] results = new double[pp.MyResultPackages.Count * 5, pp.TheIndex.Count];

            for (int x1 = 0; x1 < pp.MyResultPackages.Count; x1++)
            {
                ResultEntry re = pp.MyResultPackages[x1];
                
                //Calculate NSAF Factor
                double nsafDenominator = 0;
                for (int y = 0; y < pp.TheIndex.Count; y++)
                {
                    ProteinIndexStruct i = pp.TheIndex[y];
                    int index = re.MyResultPackage.MyProteins.MyProteinList.FindIndex(a => a.Locus.Equals(i.Locus));

                    if (index > -1)
                    {
                        nsafDenominator += (double)re.MyResultPackage.MyProteins.MyProteinList[index].Scans.Count / (double)re.MyResultPackage.MyProteins.MyProteinList[index].Sequence.Length;
                    }

                }
                

                for (int y = 0; y < pp.TheIndex.Count; y++)
                {
                    ProteinIndexStruct i = pp.TheIndex[y];
                    int index = re.MyResultPackage.MyProteins.MyProteinList.FindIndex(a => a.Locus.Equals(i.Locus));
                    int xPos = x1 * 5;
                    if (index > -1)
                    {
                        double nsaf = ((double)re.MyResultPackage.MyProteins.MyProteinList[index].Scans.Count / (double)re.MyResultPackage.MyProteins.MyProteinList[index].Length) / nsafDenominator;

                        results[xPos, y] = re.MyResultPackage.MyProteins.MyProteinList[index].ContainsUniquePeptide;
                        results[xPos+1, y] = re.MyResultPackage.MyProteins.MyProteinList[index].PeptideResults.Count();
                        results[xPos+2, y] = re.MyResultPackage.MyProteins.MyProteinList[index].Scans.Count;
                        results[xPos + 3, y] = nsaf;
                        results[xPos + 4, y] = re.MyResultPackage.MyProteins.MyProteinList[index].Coverage;

                        
                    }
                    else
                    {
                        results[xPos, y] = -1.0;
                        results[xPos + 1, y] = -1.0;
                        results[xPos + 2, y] = -1.0;
                        results[xPos + 3, y] = -1.0;
                        results[xPos + 4, y] = -1.0;
                    }


                }
            }

            dataGridViewBirdsEyeView.Columns.Clear();
            dataGridViewBirdsEyeView.Rows.Clear();

            foreach (ResultEntry re in pp.MyResultPackages)
            {
                var col = new DataGridViewTextBoxColumn();
                col.HeaderText = re.ClassLabel + "::" + re.MyFileInfo.Name + "::" + "UniquePeptideCount";
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.HeaderCell.Style.BackColor = PatternTools.pTools.color[re.ClassLabel + 1];
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewBirdsEyeView.Columns.Add(col);

                var col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = re.ClassLabel + "::" + re.MyFileInfo.Name + "::" + "PeptideCount";
                col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col2.HeaderCell.Style.BackColor = PatternTools.pTools.color[re.ClassLabel + 1];
                col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewBirdsEyeView.Columns.Add(col2);

                var col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = re.ClassLabel + "::" + re.MyFileInfo.Name + "::" + "SpecCount";
                col3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col3.HeaderCell.Style.BackColor = PatternTools.pTools.color[re.ClassLabel + 1];
                col3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewBirdsEyeView.Columns.Add(col3);

                var col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = re.ClassLabel + "::" + re.MyFileInfo.Name + "::" + "NSAF";
                col4.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col4.HeaderCell.Style.BackColor = PatternTools.pTools.color[re.ClassLabel + 1];
                col4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewBirdsEyeView.Columns.Add(col4);

                var col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = re.ClassLabel + "::" + re.MyFileInfo.Name + "::" + "Coverage";
                col5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col5.HeaderCell.Style.BackColor = PatternTools.pTools.color[re.ClassLabel + 1];
                col5.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewBirdsEyeView.Columns.Add(col5);
            }

            for (int y = 0; y < results.GetLength(1); y++)
            {
                int rowI = dataGridViewBirdsEyeView.Rows.Add();
                dataGridViewBirdsEyeView.Rows[rowI].HeaderCell.Value = pp.TheIndex[y].Locus + "\t" + pp.TheIndex[y].Description;

                for (int x = 0; x < results.GetLength(0); x++)
                {
                    dataGridViewBirdsEyeView.Rows[y].Cells[x].Value = results[x, y];
                }
            }
            this.Update();
            
        }


        private void dataGridViewBirdsEyeView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;

            Console.WriteLine("Done");
        }

        private void buttonSaveBirdsEyeView_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog1.Title = "Bird´s Eye View";
            saveFileDialog1.FileName = "BirdsEyeView";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                for (int i = 0; i < dataGridViewBirdsEyeView.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        sw.Write("\t");
                    }
                    sw.Write("\t" + dataGridViewBirdsEyeView.Columns[i].HeaderText);
                }
                sw.Write("\n");

                for (int y = 0; y < dataGridViewBirdsEyeView.Rows.Count; y++)
                {
                    for (int x = 0; x < dataGridViewBirdsEyeView.Columns.Count; x++)
                    {
                        if (x == 0)
                        {
                            sw.Write(dataGridViewBirdsEyeView.Rows[y].HeaderCell.Value.ToString());
                        }

                        sw.Write("\t" + dataGridViewBirdsEyeView.Rows[y].Cells[x].Value.ToString());
                    }

                    sw.Write("\n");
                }

                sw.Close();
            }
        }

        private void buttonSavePatternLabProject_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PatternLab Project (*.plp)|*.plp";
            saveFileDialog1.Title = "Save PatternLab Project file";
            saveFileDialog1.FileName = "MyPatternLabProject";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                buttonSavePatternLabProject.Text = "Saving";
                this.Update();

                theParser.MyParameters = GetParametersFromGUI();
                theParser.SavePatternLabProjectFile(saveFileDialog1.FileName, textBoxProjectDescription.Text);
                buttonSavePatternLabProject.Text = "Save PatternLab Project file";
                MessageBox.Show("File Saved.");
            }
        }


        private void buttonDominomicsSavePatternLabProject_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PatternLab project file (*.plp)|*.plp";
            saveFileDialog1.Title = "Save the PatternLab project file";
            saveFileDialog1.FileName = "MyProjectFile";
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBoxSavePLP.Text = saveFileDialog1.FileName;
            }
        }

        //------------------------------------------------------------------------

    }


}
