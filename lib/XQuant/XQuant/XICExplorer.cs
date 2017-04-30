using PatternTools;
using PatternTools.FastaParser;
using PatternTools.MSParser;
using PatternTools.XIC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using XQuant.Quants;

namespace XQuant
{
    public partial class XICExplorer : UserControl
    {
        Core35 core;
        bool workIsOn = false;

        Dictionary<string, List<double>> quantDict = new Dictionary<string, List<double>>();


        public XICExplorer()
        {
            InitializeComponent();
        }

        private void XICExplorer_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns a list of XICs of all acceptable peptides</returns>
        public void FillPeptideXICTable3()
        {
            
            dataGridViewResult.Rows.Clear();
            dataGridViewResult.Columns.Clear();

            List<int> consideredCharges = core.MyClusterParams.AcceptableChargeStates;

            List<string> pepSequences = (from qp in core.myQuantPkgs
                                         from k in qp.MyQuants.Keys
                                         select k).Distinct().OrderBy(a => a).ToList();


            if (checkBoxOnlyUniquePeptides.Checked)
            {
                pepSequences.RemoveAll(a => core.PeptideProteinDictionary[PatternTools.pTools.CleanPeptide(a, true)].Count == 1);
            }


            core.myQuantPkgs.Sort((a, b) => a.ClassLabel.CompareTo(b.ClassLabel));

            //include the files
            var col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "Peptide::Z";
            col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewResult.Columns.Add(col1); 
            foreach (QuantPackage2 quant in core.myQuantPkgs)
            {
                var col = new DataGridViewTextBoxColumn();
                col.HeaderText = quant.FileName + "::" + quant.ClassLabel;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.HeaderCell.Style.BackColor = Utils.color[quant.ClassLabel + 1];
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewResult.Columns.Add(col);
            }

            pepSequences.Sort();

            List<string> validPepSeqs = new List<string>();

            int QuantCount = 0;
            foreach (string peptide in pepSequences)
            {

                foreach (int z in consideredCharges)
                {

                    int index = dataGridViewResult.Rows.Add();

                    string fill = peptide + "::" + z;

                    dataGridViewResult.Rows[index].Cells[0].Value = fill;

                    bool deleteRow = true;

                    for (int i = 0; i < core.myQuantPkgs.Count; i++)
                    {
                        QuantPackage2 thisQP = core.myQuantPkgs[i];

                        List<Quant> theQuants = new List<Quant>();

                        if (core.myQuantPkgs[i].MyQuants.ContainsKey(peptide))
                        {

                            theQuants = (from q in core.myQuantPkgs[i].MyQuants[peptide]
                                         where q.Z == z
                                         select q).OrderByDescending(a => a.QuantArea).ToList();
                        }



                        if (theQuants.Count > 0 && theQuants[0].MyIons.GetLength(1) >= numericUpDownMinMS1Count.Value)
                        {

                            validPepSeqs.Add(peptide);

                            double xic = theQuants[0].QuantArea;
                            dataGridViewResult.Rows[index].Cells[i+1].Value = xic;

                            if (xic > 0)
                            {
                                deleteRow = false;
                                QuantCount++;
                            }

                            if (theQuants.Exists(a => a.ScanNoMS2 > -1))
                            {
                                dataGridViewResult.Rows[index].Cells[i+1].Style.ForeColor = Color.Blue;
                            }
                            else
                            {
                                dataGridViewResult.Rows[index].Cells[i+1].Style.ForeColor = Color.DarkGreen;
                            }

                            dataGridViewResult.Rows[index].Cells[i+1].ToolTipText = "MS1 Counts = " + theQuants[0].MyIons.GetLength(1);
                        }
                        else
                        {
                            dataGridViewResult.Rows[index].Cells[i+1].Style.ForeColor = Color.Red;
                            dataGridViewResult.Rows[index].Cells[i+1].Value = -1;
                        }
                    }

                    if (deleteRow)
                    {
                        dataGridViewResult.Rows.RemoveAt(index);
                    }
                }
            }

            int totalPeptides = pepSequences.Count;
            labelQuantNo.Text = QuantCount.ToString();


            labelQuants.Text = "PepSequcnes: " + validPepSeqs.Distinct().Count() + " Rows: " + dataGridViewResult.Rows.Count;

        }


       

        //--Loading----------------------------------------------
        public void LoadFromDiskBin(string fileName)
        {
            Console.WriteLine("Loading " + fileName);
            core = Core35.Deserialize(fileName);

            UpdateUIWithUserParams();

            //Finish the rest of the stuff
            parameters1.UpdatePanel(core.MyClusterParams);
            labelFile.Text = fileName;

            Console.WriteLine("Setting up the GUI");
            FinishLoadingProcess();
        }

        private void UpdateUIWithUserParams()
        {
            numericUpDownMinMS1Count.Value = core.MyClusterParams.MinMS1Counts;
            checkBoxOnlyUniquePeptides.Checked = core.MyClusterParams.OnlyUniquePeptides;
            numericUpDownMinimumNumberOfPeptides.Value = core.MyClusterParams.MinNoPeptides;
            checkBoxMaximumParsimony.Checked = core.MyClusterParams.MaxParsimony;
        }

        public void LoadFromDiskJson(string fileName)
        {
            Console.WriteLine("Loading " + fileName);
            core = Core35.DeserializeJSON(fileName);
            UpdateUIWithUserParams();
            parameters1.UpdatePanel(core.MyClusterParams);

            Console.WriteLine("Setting up the GUI");
            labelFile.Text = fileName;
            FinishLoadingProcess();
        }
        public void LoadFromInstance(Core35 theNewCore)
        {
            core = theNewCore;
            FinishLoadingProcess();
        }

        private void FinishLoadingProcess()
        {
            userControlClassEditor1.MyQuantPckg(core.myQuantPkgs);
            FillPeptideXICTable3();
            FillProteinView();

            //We need to present a table lining up all MSMS files so the user can create the searching rules
            generateSearchingRules2.MyAssociations = core.MyAssociationItems;
            generateSearchingRules2.ChromTolerance = core.ChromeAssociationTolerance;
            generateSearchingRules2.FillTable();

            checkedListBoxQuants.Items.Clear();
            List<string> qFileNames = core.myQuantPkgs.Select(a => a.FileName).ToList();
            qFileNames.Sort();
            for (int i = 0; i < qFileNames.Count; i++)
            {
                checkedListBoxQuants.Items.Add(qFileNames[i]);
                checkedListBoxQuants.SetItemChecked(i, true);
            }

            GenerateGraphicalAnalysis(checkBoxNormalizeHistogram.Checked, (int)numericUpDownNumberOfBins.Value);

            userControlClassEditor1.MyQuantPckg(core.myQuantPkgs);
            
        }



        private void FillProteinView()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Filling protein table");
            dataGridViewProteinView.Columns.Clear();

            List<DataGridViewColumn> theColumns = new List<DataGridViewColumn>();

            DataGridViewColumn dgvcID = new DataGridViewTextBoxColumn();
            dgvcID.Name = "ProteinID";
            dgvcID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcID.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            theColumns.Add(dgvcID);

            DataGridViewColumn dgvcNoAA = new DataGridViewTextBoxColumn();
            dgvcNoAA.Name = "NoAA";
            dgvcNoAA.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcNoAA.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            theColumns.Add(dgvcNoAA);


            foreach (QuantPackage2 quant in core.myQuantPkgs)
            {
                var colPep = new DataGridViewTextBoxColumn();
                colPep.HeaderText = quant.FileName + "_Peptides" + "::" + quant.ClassLabel;
                colPep.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colPep.HeaderCell.Style.BackColor = Utils.color[quant.ClassLabel + 1];
                colPep.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                theColumns.Add(colPep);

                var colUniquePep = new DataGridViewTextBoxColumn();
                colUniquePep.HeaderText = quant.FileName + "_UniquePeptides" + "::" + quant.ClassLabel;
                colUniquePep.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colUniquePep.HeaderCell.Style.BackColor = Utils.color[quant.ClassLabel + 1];
                colUniquePep.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                theColumns.Add(colUniquePep);

                var colCov = new DataGridViewTextBoxColumn();
                colCov.HeaderText = quant.FileName + "_Coverage" + "::" + quant.ClassLabel;
                colCov.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colCov.HeaderCell.Style.BackColor = Utils.color[quant.ClassLabel + 1];
                colCov.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                theColumns.Add(colCov);

                var colSignal = new DataGridViewTextBoxColumn();
                colSignal.HeaderText = quant.FileName + "_Signal" + "::" + quant.ClassLabel;
                colSignal.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colSignal.HeaderCell.Style.BackColor = Utils.color[quant.ClassLabel + 1];
                colSignal.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                theColumns.Add(colSignal);

                var colSignalAA = new DataGridViewTextBoxColumn();
                colSignalAA.HeaderText = quant.FileName + "_NormalizedIonAbundanceFactor" + "::" + quant.ClassLabel;
                colSignalAA.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colSignalAA.HeaderCell.Style.BackColor = Utils.color[quant.ClassLabel + 1];
                colSignalAA.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                theColumns.Add(colSignalAA);
            }

            DataGridViewColumn dgvcDescription = new DataGridViewTextBoxColumn();
            dgvcDescription.Name = "Description";
            dgvcDescription.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            theColumns.Add(dgvcDescription);

            foreach (DataGridViewColumn dc in theColumns)
            {
                dataGridViewProteinView.Columns.Add(dc);
            }

            int proteinsRecovedCounter = 0;
            double[] iafSum = new double[core.myQuantPkgs.Count];

            List<FastaItem> theFasta = core.MyFastaItems;



            if (checkBoxMaximumParsimony.Checked)
            {
                theFasta = PatternTools.pTools.MaxParsimonyList(core.MyFastaItems, core.PeptideProteinDictionary);
            }



            foreach (FastaItem fi in theFasta)
            {
                int index = dataGridViewProteinView.Rows.Add();
                dataGridViewProteinView.Rows[index].Cells[0].Value = fi.SequenceIdentifier;
                dataGridViewProteinView.Rows[index].Cells[1].Value = fi.Sequence.Length;

                int NoPeptidesMax = 0;

                double[] thisNIAF = new double[core.myQuantPkgs.Count];
                for (int i = 0; i < core.myQuantPkgs.Count; i++)
                {
                    fi.ResetCoverage(); //we need to reset the coverage
                    int colStep = 2 + (i * 5);

                    QuantPackage2 quantPkg = core.myQuantPkgs[i];


                    List<Tuple<string, List<Quant>>> relevantQuants = new List<Tuple<string, List<Quant>>>();

                    if (core.ProteinPeptideDictionary.ContainsKey(fi.SequenceIdentifier))
                    {
                        foreach (string peptide in core.ProteinPeptideDictionary[fi.SequenceIdentifier])
                        {
                            if (checkBoxOnlyUniquePeptides.Checked)
                            {
                                if (core.PeptideProteinDictionary[PatternTools.pTools.CleanPeptide(peptide, true)].Count > 1)
                                {
                                    continue;
                                }
                            }

                            if (quantPkg.MyQuants.ContainsKey(peptide))
                            {
                                if (quantPkg.MyQuants[peptide].Count > 0)
                                {
                                    List<Quant> validQuants = quantPkg.MyQuants[peptide].FindAll(a => a.MyIons.GetLength(1) >= numericUpDownMinMS1Count.Value);

                                    if (validQuants.Count > 0)
                                    {
                                        relevantQuants.Add(new Tuple<string, List<Quant>>(peptide, validQuants));
                                    }
                                } 
                            }
                        }
                    }


                    //Calculate Sequence Count
                    dataGridViewProteinView.Rows[index].Cells[colStep].Value = relevantQuants.Count();
                    dataGridViewProteinView.Rows[index].Cells[colStep].ToolTipText = string.Join("\n", relevantQuants.Select( a => a.Item1 + "::" + a.Item2[0].Z).ToList());
                    if (relevantQuants.Count > NoPeptidesMax)
                    {
                        NoPeptidesMax = relevantQuants.Count;
                    }

                    //Calculate Unique Peptides Count
                    int uniquePeptides = relevantQuants.Count(a => core.PeptideProteinDictionary[PatternTools.pTools.CleanPeptide(a.Item1, true)].Count == 1);
                    dataGridViewProteinView.Rows[index].Cells[colStep + 1].Value = uniquePeptides;

                    //Calculate Coverage
                    List<string> cleanPeptides = relevantQuants.Select(a => PatternTools.pTools.CleanPeptide(a.Item1, true)).Distinct().ToList();
                    double coverage = fi.Coverage(cleanPeptides);
                    dataGridViewProteinView.Rows[index].Cells[colStep + 2].Value = Math.Round(coverage, 4);

                    //Calculate Signal
                    double signalSum = relevantQuants.Sum(a => a.Item2.Sum(b=>b.QuantArea));
                    dataGridViewProteinView.Rows[index].Cells[colStep + 3].Value = signalSum;

                    //Calculate Normalized Signal
                    dataGridViewProteinView.Rows[index].Cells[colStep + 4].Value = signalSum / (double)fi.Sequence.Length;
                    thisNIAF[i] += signalSum / (double)fi.Sequence.Length; //Save the ion abundance factor

                }

                dataGridViewProteinView.Rows[index].Cells[dataGridViewProteinView.Rows[index].Cells.Count - 1].Value = fi.Description;


                //Clean proteins that do not have any peptides whatsoever because none of them achieved an acceptable XIC
                if (NoPeptidesMax < numericUpDownMinimumNumberOfPeptides.Value)
                {
                    dataGridViewProteinView.Rows.RemoveAt(index);
                    proteinsRecovedCounter++;
                } 
                else
                {
                    for (int i = 0; i < thisNIAF.Length; i++)
                    {
                        iafSum[i] += thisNIAF[i];
                    }
                }


            }

            //And now compute the NIAF for all rows
            for (int i = 0; i < dataGridViewProteinView.Rows.Count; i++)
            {
                for (int j = 0; j < core.myQuantPkgs.Count; j++)
                {
                    int cellNo = 2 + (j * 5);
                    string cellValue = dataGridViewProteinView.Rows[i].Cells[cellNo + 4].Value.ToString();
                    double currentValue = double.Parse(cellValue);
                    double newValue = currentValue /= iafSum[j];
                    dataGridViewProteinView.Rows[i].Cells[cellNo + 4].Value = newValue;
                }

            }


            //Lets take care of the keywords
            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            if (textBoxKeyWords.Text.Length > 0)
            {
                foreach (DataGridViewRow row in dataGridViewProteinView.Rows)
                {
                    string rID = row.Cells["ProteinID"].Value.ToString();
                    string rDesc = row.Cells["Description"].Value.ToString();

                    Console.WriteLine(rID + " " + rDesc);

                    if (!rID.Contains(textBoxKeyWords.Text) && !rDesc.Contains(textBoxKeyWords.Text))
                    {
                        rowsToRemove.Add(row);
                    }
                }
                
                foreach (DataGridViewRow r in rowsToRemove)
                {
                    dataGridViewProteinView.Rows.Remove(r);
                }
                dataGridViewProteinView.Refresh();
            }


            Console.WriteLine("Proteins removed for not having peptides with XICs: " + proteinsRecovedCounter);
            Console.WriteLine("Proteins removed for not matcing keywords: " + rowsToRemove.Count);

            labelProteinCount.Text = dataGridViewProteinView.Rows.Count + " / " + core.MyFastaItems.Count;

            sw.Stop();
            Console.WriteLine("Done filling protein table. " + Math.Round((double)sw.ElapsedMilliseconds / 1000, 1) + "s.");

        }

        //------------------------------------------------------


        private void GenerateGraphicalAnalysis(bool normalize, int numberOfBins)
        {
            List<string> checkedFiles =  checkedListBoxQuants.CheckedItems.Cast<string>().ToList();
            List<string> allFiles = checkedListBoxQuants.Items.Cast<string>().ToList();

            List<QuantPackage2> myQuants = core.myQuantPkgs.FindAll(a => checkedFiles.Contains(a.FileName));


            // Set series visual attributes
            chartQuantitationHistogram.ResetAutoValues();
            chartQuantitationHistogram.ChartAreas[0].AxisX.Title = "(-1)Log(quant)";
            chartQuantitationHistogram.ChartAreas[0].AxisY.Title = "# quants";          
            chartQuantitationHistogram.Series.Clear();

            if (checkedFiles.Count == 0) { return; }

            int counter = 0;

            //Find the minimum and max values

            List<double> allQuants = new List<double>();
            foreach (QuantPackage2 qp in myQuants)
            {

                //I need to replicate this logic down below!!!!!!!!!!!!!!!!!
                var quantsT = (from q in qp.MyQuants
                               from qv in q.Value
                               where qv.QuantArea > 1 && qv.MyIons.GetLength(1) >= numericUpDownMinMS1Count.Value
                               select new { CSeq = PatternTools.pTools.CleanPeptide(q.Key, true), XIC = qv.QuantArea });

                List<double> quants = new List<double>();

                if (checkBoxOnlyUniquePeptides.Checked)
                {
                    quants = (from q in quantsT
                              where core.PeptideProteinDictionary[q.CSeq].Count == 1
                              select q.XIC).ToList();
                } else
                {
                    quants = (from q in quantsT
                              select q.XIC).ToList();
                }

                //-----------------------------------------------------------

                if (normalize)
                {
                    double total = quants.Sum();
                    quants.Sort();
                    quants = quants.Select(a => Math.Log(a / total) * -1).ToList();
                }
                else
                {
                    quants = quants.Select(a => Math.Log(a)).ToList();
                    chartQuantitationHistogram.ChartAreas[0].AxisX.Title = "Log(quant)";
                }

                allQuants.AddRange(quants);
                

            }

            double allQuantsMin = allQuants.Min();
            double allQuantsMax = allQuants.Max();

            foreach (QuantPackage2 qp in myQuants)
            {
                counter++;

                var quantsT = (from q in qp.MyQuants
                               from qv in q.Value
                               where qv.QuantArea > 1 && qv.MyIons.GetLength(1) >= numericUpDownMinMS1Count.Value
                               select new { CSeq = PatternTools.pTools.CleanPeptide(q.Key, true), XIC = qv.QuantArea });

                List<double> quants = new List<double>();

                if (checkBoxOnlyUniquePeptides.Checked)
                {
                    quants = (from q in quantsT
                              where core.PeptideProteinDictionary[q.CSeq].Count == 1
                              select q.XIC).ToList();
                }
                else
                {
                    quants = (from q in quantsT
                              select q.XIC).ToList();
                }

                if (normalize)
                {
                    double total = quants.Sum();
                    quants.Sort();
                    quants = quants.Select(a => Math.Log(a / total) * -1).ToList();
                }
                else
                {
                    quants = quants.Select(a => Math.Log(a)).ToList();
                    chartQuantitationHistogram.ChartAreas[0].AxisX.Title = "Log(quant)";
                }

                List<PatternTools.HistogramHelper.HistogramBin> theBins;
                theBins = PatternTools.HistogramHelper.BinData(quants, allQuantsMin, allQuantsMax, numberOfBins, true);
                List<PatternTools.Point> graphPoints = new List<PatternTools.Point>(theBins.Count);

                chartQuantitationHistogram.Series.Add(qp.FileName + "-" + counter);

                Console.WriteLine(qp.FileName);
                foreach (PatternTools.HistogramHelper.HistogramBin bin in theBins)
                {
                    double x = Math.Round(bin.IntervalFloor, 2);
                    double y = bin.TheData.Count;
                    chartQuantitationHistogram.Series[qp.FileName + "-" + counter].Points.AddXY(x, y);
                    graphPoints.Add(new PatternTools.Point(x, y));
                    Console.WriteLine("Interval: " + bin.IntervalFloor + "\tCount: " + bin.TheData.Count);

                }


            }

            Console.WriteLine("Average of all quants: " + allQuants.Average());
            Console.WriteLine("Std of All Quants: " + PatternTools.pTools.Stdev(allQuants, true));

           

        }

        private string GetComparisonData(string quantFileName1, string quantFileName2, bool normalize, int noBins)
        {
            List<QuantPackage2> myQuants = core.myQuantPkgs.FindAll(a => quantFileName1.Equals(a.FileName) || quantFileName2.Equals(a.FileName));

            if (myQuants.Count >2 || myQuants.Count <1)
            {
                throw new Exception("Files were not appropriately named.  The file names should be distinct");
            }

            List<double>  qv1 = (from qp in core.myQuantPkgs
                                where qp.FileName.Equals(quantFileName1)
                                from q in qp.MyQuants
                                from qv in q.Value
                                where qv.QuantArea > 0
                                select qv.QuantArea).ToList();

            List<double> qv2 = (from qp in core.myQuantPkgs
                                where qp.FileName.Equals(quantFileName2)
                                from q in qp.MyQuants
                                from qv in q.Value
                                where qv.QuantArea > 0
                                select qv.QuantArea).ToList();

            if (normalize)
            {
                double s1 = qv1.Sum();
                qv1 = qv1.Select(a => a / s1).ToList();
                qv1 = qv1.Select(a => Math.Log(a / s1) * -1).ToList();

                double s2 = qv2.Sum();
                qv2 = qv2.Select(a => a / s2).ToList();
                qv2 = qv2.Select(a => Math.Log(a / s2) * -1).ToList();
            }
            else
            {
                qv1 = qv1.Select(a => Math.Log(a)).ToList();
                qv2 = qv2.Select(a => Math.Log(a)).ToList();
            }

            List<double> allQuants = qv1.Concat(qv2).ToList();

            double min = allQuants.Min();
            double max = allQuants.Max();

            List<PatternTools.HistogramHelper.HistogramBin> b1 = PatternTools.HistogramHelper.BinData(qv1, min, max, noBins, true);
            List<PatternTools.HistogramHelper.HistogramBin> b2 = PatternTools.HistogramHelper.BinData(qv2, min, max, noBins, true);

            List<double> folds = new List<double>();

            List<double> klV1 = new List<double>();
            List<double> klV2 = new  List<double>();

            for (int i = 0; i < b1.Count; i++)
            {
                if (b1[i].TheData.Count == 0 || b2[i].TheData.Count == 0)
                {
                    continue;
                }

                klV1.Add(b1[i].TheData.Count);
                klV2.Add(b2[i].TheData.Count);
                folds.Add(Math.Log( (double) b1[i].TheData.Count / (double) b2[i].TheData.Count ));
            }


            double leftTail;
            double rightTail;
            double bothTails;

            alglib.studentttest1(folds.ToArray(), folds.Count, 0, out bothTails, out leftTail, out rightTail);
            double klDivergence = PatternTools.pTools.KLDivergence(klV1, klV2);

            return "p = " + Math.Round(bothTails, 3) + " KL = " + Math.Round(klDivergence, 3);
            
        }

        private void retainOptimumSignalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (core.MyClusterParams.RetainOptimal)
            {
                MessageBox.Show("This filter was already applied.");
            }
            else
            {
                core.RetainOptimalSignal();
                core.MyClusterParams.RetainOptimal = true;
                FillPeptideXICTable3();
                MessageBox.Show("Done");
            }
        }

        private void fillInTheGapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "XIC Files (xic)|*.xic";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Console.WriteLine("Fill in the gaps - start");
                
                core.MyAssociationItems = generateSearchingRules2.GetAssociations();
                core.ChromeAssociationTolerance = generateSearchingRules2.ChromTolerance;

                List<string> peptideWithCharges = new List<string>();

                for (int i = 0; i < dataGridViewResult.Rows.Count; i++)
                {
                    peptideWithCharges.Add(dataGridViewResult.Rows[i].HeaderCell.Value.ToString());
                }

                core.FillInTheGaps(peptideWithCharges);
                FinishLoadingProcess();

                SaveProcedure(saveFileDialog1.FileName);

                Console.WriteLine("Done");
            }
        }

        private void SaveProcedure(string fileName)
        {
            core.MyAssociationItems = generateSearchingRules2.GetAssociations();
            core.ChromeAssociationTolerance = generateSearchingRules2.ChromTolerance;
            userControlClassEditor1.UpdateCoreWithLabels(core);

            core.MyClusterParams.MinMS1Counts = (int)numericUpDownMinMS1Count.Value;
            core.MyClusterParams.OnlyUniquePeptides = checkBoxOnlyUniquePeptides.Checked;
            core.MyClusterParams.MinNoPeptides = (int)numericUpDownMinimumNumberOfPeptides.Value;
            core.MyClusterParams.MaxParsimony = checkBoxMaximumParsimony.Checked;

            core.Serialize(saveFileDialog1.FileName);
        }

        private void checkBoxNormalizeHistogram_CheckedChanged(object sender, EventArgs e)
        {
            GenerateGraphicalAnalysis(checkBoxNormalizeHistogram.Checked, (int)numericUpDownNumberOfBins.Value);
        }

        private void numericUpDownNumberOfBins_ValueChanged(object sender, EventArgs e)
        {
            GenerateGraphicalAnalysis(checkBoxNormalizeHistogram.Checked, (int)numericUpDownNumberOfBins.Value);
        }

        private void checkedListBoxQuants_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateGraphicalAnalysis(checkBoxNormalizeHistogram.Checked, (int)numericUpDownNumberOfBins.Value);
        }



        private void buttonGeneratePairWiseTable_Click(object sender, EventArgs e)
        {
            #region pairwise table

            //Prepare pairwise table
            //Set up the table
            dataGridViewPairWiseComparisonGrid.Rows.Clear();
            dataGridViewPairWiseComparisonGrid.Columns.Clear();

            List<string> allFiles = core.myQuantPkgs.Select(a => a.FileName).ToList();
            bool normalize = checkBoxNormalizeHistogram.Checked;
            int numberOfBins = (int)numericUpDownNumberOfBins.Value;

            foreach (string s in allFiles)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = s;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


                int cIndex = dataGridViewPairWiseComparisonGrid.Columns.Add(col);
                dataGridViewPairWiseComparisonGrid.Columns[cIndex].HeaderText = s;

                int rIndex = dataGridViewPairWiseComparisonGrid.Rows.Add();
                dataGridViewPairWiseComparisonGrid.Rows[rIndex].HeaderCell.Value = s;

            }

            for (int x = 0; x < allFiles.Count; x++)
            {
                for (int y = 0; y < allFiles.Count; y++)
                {
                    string comp = GetComparisonData(allFiles[x], allFiles[y], normalize, numberOfBins);
                    dataGridViewPairWiseComparisonGrid.Rows[y].Cells[x].Value = comp;
                }
            }

            tabControlGraphical.SelectedTab = tabPagePairWiseComparison;

            #endregion
        }

        private void dataGridViewPairWiseComparisonGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int y = e.RowIndex;
            int x = e.ColumnIndex;

            List<string> allFiles = core.myQuantPkgs.Select(a => a.FileName).ToList();

            List<string> fileNames = new List<string>() {allFiles[x], allFiles[y]};

            for (int i = 0; i< checkedListBoxQuants.Items.Count; i++) 
            {

                if (fileNames.Contains(checkedListBoxQuants.Items[i].ToString())) 
                {
                    checkedListBoxQuants.SetItemCheckState(i, CheckState.Checked);
                }
                else
                {
                    checkedListBoxQuants.SetItemCheckState(i, CheckState.Unchecked);
                }
            }

            GenerateGraphicalAnalysis(checkBoxNormalizeHistogram.Checked, (int)numericUpDownNumberOfBins.Value);
            tabControlGraphical.SelectedTab = tabPageQuantitationHistogram;
        }

        private void saveBinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "XIC Files (xic)|*.xic";

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                SaveProcedure(saveFileDialog1.FileName);
            }

            MessageBox.Show("File saved.");
        }

 
        private void loadJSonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XIC JSON Files (json)|*.json";

            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                LoadFromDiskJson(openFileDialog1.FileName);
            }
        }

        private void saveJSonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "XIC JSON Files (json)|*.json";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                core.MyAssociationItems = generateSearchingRules2.GetAssociations();
                core.ChromeAssociationTolerance = generateSearchingRules2.ChromTolerance;
                userControlClassEditor1.UpdateCoreWithLabels(core);
                core.SerializeJSON(saveFileDialog1.FileName);
                MessageBox.Show("XIC file updated.");
            }
        }

        private void dataGridViewResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PlotXICFromView(dataGridViewResult, e.ColumnIndex, e.RowIndex);
            
        }



        private void dataGridViewProteinView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            DisplayPeptidesForThisProtein2(rowIndex, columnIndex);

        }

        private void dataGridViewProteinView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!workIsOn)
            {
                int rowIndex = e.RowIndex;
                int columnIndex = e.ColumnIndex;
                DisplayPeptidesForThisProtein2(rowIndex, columnIndex);
            }
        }

        

        private void dataGridViewPeptidesFromSelectedProtein_CellDoubleClick_old(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                MessageBox.Show("Please dont use the header cells as a reference.");
                return;
            }


            List<string> fNames = Regex.Split(dataGridViewPeptidesFromSelectedProtein.Columns[e.ColumnIndex].HeaderText, ":").ToList();
            string fileName = fNames[0];
            string[] cols = Regex.Split(dataGridViewPeptidesFromSelectedProtein.Rows[e.RowIndex].Cells[0].Value.ToString(), "::");
            int z = int.Parse(cols[1]);
            string pepSequence = cols[0];

            //Now lets get the cluster and group them


            List<Quant> quants = (from qp in core.myQuantPkgs
                                  where qp.FileName.Equals(fileName)
                                  from q in qp.MyQuants
                                  where q.Key.Equals(pepSequence)
                                  from qv in q.Value
                                  where qv.Z == z
                                  select qv).OrderByDescending(a => a.QuantArea).ToList();

            if (quants.Count == 0)
            {
                return;
            }

            List<int> scnanNumbers = quants.Select(a => a.ScanNoMS2).ToList();

            XICViewerWPF.ViewerWindow vw = new XICViewerWPF.ViewerWindow();

            string seriesName = "Peptide";
            List<IonLight> theIons = quants[0].GetIonsLight();
            List<int> ms2ScanNo = scnanNumbers;
            Tuple<int, string, List<IonLight>, List<int>> pd = new Tuple<int, string, List<IonLight>, List<int>>(1, seriesName, theIons, ms2ScanNo);

            vw.MyViewer.ReferenceMass = quants[0].PrecursorMZ;
            vw.MyViewer.MyData = new List<Tuple<int, string, List<IonLight>, List<int>>>() { pd };
            vw.ShowDialog();


            Console.WriteLine("Plot");
        }


        private void DisplayPeptidesForThisProtein2(int rowIndex, int columnIndex)
        {
            if (columnIndex == -1)
            {
                MessageBox.Show("Please dont use the header cells as a reference.");
                return;
            }

            if (rowIndex == -1)
            {
                return;
            }

            if (object.Equals(dataGridViewProteinView.Rows[rowIndex].Cells[0].Value, null))
            {
                return;
            }


            string protID = dataGridViewProteinView.Rows[rowIndex].Cells[0].Value.ToString();
            Console.WriteLine("Display peptides for protein :" + protID);


            dataGridViewPeptidesFromSelectedProtein.Rows.Clear();
            dataGridViewPeptidesFromSelectedProtein.Columns.Clear();

            List<int> consideredCharges = core.MyClusterParams.AcceptableChargeStates;

            
            List<string> pepSequences = (from pepseq in core.ProteinPeptideDictionary[protID]
                                            from qp in core.myQuantPkgs
                                            where qp.MyQuants.ContainsKey(pepseq)
                                                select pepseq).Distinct().OrderBy(a => a).ToList();


            if (checkBoxOnlyUniquePeptides.Checked)
            {
                pepSequences.RemoveAll(a => core.PeptideProteinDictionary[PatternTools.pTools.CleanPeptide(a, true)].Count == 1);
            }


            core.myQuantPkgs.Sort((a, b) => a.ClassLabel.CompareTo(b.ClassLabel));

            var col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "Sequence :: Z";
            col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewPeptidesFromSelectedProtein.Columns.Add(col1);


            //include the files
            foreach (QuantPackage2 quant in core.myQuantPkgs)
            {
                var col = new DataGridViewTextBoxColumn();
                col.HeaderText = quant.FileName + "::" + quant.ClassLabel;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.HeaderCell.Style.BackColor = Utils.color[quant.ClassLabel + 1];
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewPeptidesFromSelectedProtein.Columns.Add(col);
            }

            pepSequences.Sort();

            List<string> validPepSeqs = new List<string>();

            int QuantCount = 0;



            foreach (string peptide in pepSequences)
            {

                foreach (int z in consideredCharges)
                {

                    int index = dataGridViewPeptidesFromSelectedProtein.Rows.Add();

                    string fill = peptide + "::" + z;

                    dataGridViewPeptidesFromSelectedProtein.Rows[index].Cells[0].Value =  fill;

                    bool deleteRow = true;

                    for (int i = 0; i < core.myQuantPkgs.Count; i++)
                    {
                        QuantPackage2 thisQP = core.myQuantPkgs[i];

                        List<Quant> theQuants = new List<Quant>();

                        if (core.myQuantPkgs[i].MyQuants.ContainsKey(peptide))
                        {

                            theQuants = (from q in core.myQuantPkgs[i].MyQuants[peptide]
                                         where q.Z == z
                                         select q).OrderByDescending(a => a.QuantArea).ToList();
                        }



                        if (theQuants.Count > 0 && theQuants[0].MyIons.GetLength(1) >= numericUpDownMinMS1Count.Value)
                        {

                            validPepSeqs.Add(peptide);

                            double xic = theQuants[0].QuantArea;
                            dataGridViewPeptidesFromSelectedProtein.Rows[index].Cells[i + 1].Value = xic;

                            if (xic > 0)
                            {
                                deleteRow = false;
                                QuantCount++;
                            }

                            if (theQuants.Exists(a => a.ScanNoMS2 > -1))
                            {
                                dataGridViewPeptidesFromSelectedProtein.Rows[index].Cells[i+1].Style.ForeColor = Color.Blue;
                            }
                            else
                            {
                                dataGridViewPeptidesFromSelectedProtein.Rows[index].Cells[i+1].Style.ForeColor = Color.DarkGreen;
                            }

                            dataGridViewPeptidesFromSelectedProtein.Rows[index].Cells[i+1].ToolTipText = "MS1 Counts = " + theQuants[0].MyIons.GetLength(1);
                        }
                        else
                        {
                            dataGridViewPeptidesFromSelectedProtein.Rows[index].Cells[i+1].Style.ForeColor = Color.Red;
                            dataGridViewPeptidesFromSelectedProtein.Rows[index].Cells[i+1].Value = -1;
                        }
                    }

                    if (deleteRow)
                    {
                        dataGridViewPeptidesFromSelectedProtein.Rows.RemoveAt(index);
                    }
                }
            }


            return;


        }

        private void PlotXICFromView(DataGridView myDGView, int columnIndex, int rowIndex)
        {
            if (columnIndex == -1 || rowIndex == -1)
            {
                MessageBox.Show("Please dont use the header cells as a reference.");
                return;
            }

            string quantValue = myDGView.Rows[rowIndex].Cells[columnIndex].Value.ToString();
            if (quantValue.Equals("-1"))
            {
                Console.WriteLine("Unable to generate plot for -1 value");
                return;
            }

            double precursorMZ = -1;
            List<Tuple<int, string, List<IonLight>, List<int>>> toPlot = new List<Tuple<int, string, List<IonLight>, List<int>>>();


            for (int i = 1; i < myDGView.Columns.Count; i++)
            {
                if (i != columnIndex && rowIndex != 0)
                {
                    continue;
                }

                List<string> fNames = Regex.Split(myDGView.Columns[i].HeaderText, ":").ToList();
                string fileName = fNames[0];
                string[] cols = Regex.Split(myDGView.Rows[rowIndex].Cells[0].Value.ToString(), "::");
                int z = int.Parse(cols[1]);
                string pepSequence = cols[0];

                //Now lets get the cluster and group them


                List<Quant> quants = (from qp in core.myQuantPkgs
                                      where qp.FileName.Equals(fileName)
                                      from q in qp.MyQuants
                                      where q.Key.Equals(pepSequence)
                                      from qv in q.Value
                                      where qv.Z == z
                                      select qv).OrderByDescending(a => a.QuantArea).ToList();

                if (quants.Count > 0)
                {

                    precursorMZ = quants[0].PrecursorMZ;
                    List<int> scnanNumbers = quants.Select(a => a.ScanNoMS2).ToList();



                    string seriesName = fileName;
                    List<IonLight> theIons = quants[0].GetIonsLight();
                    List<int> ms2ScanNo = scnanNumbers;
                    toPlot.Add(new Tuple<int, string, List<IonLight>, List<int>>(1, seriesName, theIons, ms2ScanNo));

                }

            }

            XICViewerWPF.ViewerWindow vw = new XICViewerWPF.ViewerWindow();

            vw.MyViewer.ReferenceMass = precursorMZ;
            vw.MyViewer.MyData = toPlot;
            vw.MyViewer.Plot();
            vw.ShowDialog();
        }

        private void dataGridViewPeptidesFromSelectedProtein_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PlotXICFromView(dataGridViewPeptidesFromSelectedProtein, e.ColumnIndex, e.RowIndex);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            workIsOn = true;
            buttonUpdate.Text = "working";
            this.Update();
            FillProteinView();
            FillPeptideXICTable3();
            GenerateGraphicalAnalysis(checkBoxNormalizeHistogram.Checked, (int)numericUpDownNumberOfBins.Value);
            buttonUpdate.Text = "Update";
            workIsOn = false;
        }

        private void xICToyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XICToy x = new XICToy();
            x.ShowDialog();
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "XIC-Analysis";
            dlg.DefaultExt = ".xls";
            dlg.Filter = "Excel (.xls)|*.xls";

            if (dlg.ShowDialog() == true)
            {
                dataGridViewProteinView.SelectAll();
                dataGridViewProteinView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

                //ApplicationCommands.Copy.Execute(null, dataGridViewProteinView);

                DataObject dataObj = dataGridViewProteinView.GetClipboardContent();
                Clipboard.SetDataObject(dataObj, true);

                String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                String result = (string)Clipboard.GetData(DataFormats.Text);
                dataGridViewProteinView.ClearSelection();
                System.IO.StreamWriter file = new System.IO.StreamWriter(dlg.FileName);
                file.WriteLine(result.Replace(',', ' '));
                file.Close();

                MessageBox.Show("Exporting DataGrid data to Excel file created");
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "XIC files (*.xic)|*.xic";

            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                LoadFromDiskBin(openFileDialog1.FileName);
                labelFile.Text = openFileDialog1.FileName;
            }
        }

        private void independentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Update();

            saveFileDialog1.Filter = "PatternLab Project (*.plp)|*.plp";
            saveFileDialog1.Title = "Save PatternLab Project file - Independent mode";
            saveFileDialog1.FileName = "MyPatternLabProject-Independent";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                userControlClassEditor1.UpdateCoreWithLabels(core);
                SavePLP(saveFileDialog1.FileName, SaveMode.Independent);
                MessageBox.Show("File saved");
            }

            this.Enabled = true;
        }

        private enum SaveMode
        {
            Independent,
            TechnicalReplicate,
            MudPIT
        }

        private void technicalReplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Update();

            saveFileDialog1.Filter = "PatternLab Project (*.plp)|*.plp";
            saveFileDialog1.Title = "Save PatternLab Project file - Technical replicate mode";
            saveFileDialog1.FileName = "MyPatternLabProject-TechnicalReplicate";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                userControlClassEditor1.UpdateCoreWithLabels(core);
                SavePLP(saveFileDialog1.FileName, SaveMode.TechnicalReplicate);
                MessageBox.Show("File saved");
            }

            this.Enabled = true;
        }

        private void mudPITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Update();

            saveFileDialog1.Filter = "PatternLab Project (*.plp)|*.plp";
            saveFileDialog1.Title = "Save PatternLab Project file - MudPIT mode";
            saveFileDialog1.FileName = "MyPatternLabProject-MudPITt";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                userControlClassEditor1.UpdateCoreWithLabels(core);
                SavePLP(saveFileDialog1.FileName, SaveMode.MudPIT);
                MessageBox.Show("File saved");
            }

            this.Enabled = true;
        }

        private void SavePLP(string fileName, SaveMode theSaveMode)
        {

            List<SparseMatrixIndexParserV2.Index> indexes = new List<SparseMatrixIndexParserV2.Index>();
            int noCells = dataGridViewProteinView.Columns.Count;
            for (int i = 0; i < dataGridViewProteinView.Rows.Count; i++)
            {
                SparseMatrixIndexParserV2.Index ind = new SparseMatrixIndexParserV2.Index();
                ind.ID = i;
                ind.Name = dataGridViewProteinView.Rows[i].Cells[0].Value.ToString();
                ind.Description = dataGridViewProteinView.Rows[i].Cells[noCells - 1].Value.ToString();
                indexes.Add(ind);

            }

            SparseMatrixIndexParserV2 smip2 = new SparseMatrixIndexParserV2(indexes);

            SparseMatrix sm = new SparseMatrix();
            core.SEProFiles.Sort((a, b) => a.ClassLabel.CompareTo(b.ClassLabel));

            foreach (SEProFileInfo sfi in core.SEProFiles)
            {
                sm.ClassDescriptionDictionary.Add(sfi.ClassLabel, sfi.ClassDescription);
            }


            for (int i = 6; i < dataGridViewProteinView.Columns.Count; i += 5)
            {
                List<int> dims = new List<int>();
                List<double> theValues = new List<double>();


                for (int j = 0; j < dataGridViewProteinView.Rows.Count; j++)
                {
                    double theV;
                    if (double.TryParse(dataGridViewProteinView.Rows[j].Cells[i].Value.ToString(), out theV))
                    {
                        if (theV > 0)
                        {
                            dims.Add(j);
                            theValues.Add(theV);
                        }

                    }
                }

                List<string> cols = Regex.Split(dataGridViewProteinView.Columns[i].HeaderCell.Value.ToString(), "::").ToList();

                //We need to retrieve the full directory data in case we need to do any merging later.

                int classLabel = int.Parse(cols.Last());
                sparseMatrixRow smr = new sparseMatrixRow(classLabel, dims, theValues);
                smr.FileName = cols[0];
                sm.addRow(smr);

            }
            /// 

            PLP.PatternLabProject plp = new PLP.PatternLabProject(sm, smip2, "XIC Quant Project");

            //This method will compress the sparse matrix assuming there are tehnical replicates in the SPero directories.  These technical replicates will be joined by averaging their quant value of the non-zero items.
            if (theSaveMode != SaveMode.Independent)
            {
                SparseMatrix smJoined = new SparseMatrix();
                smJoined.ClassDescriptionDictionary = sm.ClassDescriptionDictionary;

                List<string> LabelDirectories = core.MyAssociationItems.Select(a => a.Label + "--0--" + a.Directory).Distinct().ToList();

                foreach (string dir in LabelDirectories)
                {
                    //Find files in that directory
                    string[] labelDir = Regex.Split(dir, "--0--");
                    Console.WriteLine("Working on " + labelDir[1]);
                    List<AssociationItem> inThisDir = core.MyAssociationItems.FindAll(a => a.Directory.Equals(labelDir[1]) && a.Label == int.Parse(labelDir[0]));
                    string[] validFileNames = inThisDir.Select(a => a.FileName).ToArray();

                    sparseMatrixRow theNewRow = new sparseMatrixRow(inThisDir[0].Label);
                    theNewRow.FileName = inThisDir[0].Directory + " (" + string.Join(",", validFileNames) + ")";

                    List<sparseMatrixRow> theOnesToJoin = new List<sparseMatrixRow>();

                    foreach (string validN in validFileNames)
                    {
                        foreach (sparseMatrixRow smr in sm.theMatrixInRows)
                        {
                            if (smr.FileName.Contains(validN))
                            {
                                theOnesToJoin.Add(smr);
                            }
                        }
                    }


                    foreach (var index in smip2.TheIndexes)
                    {
                        List<double> values = new List<double>();
                        foreach (sparseMatrixRow sss in theOnesToJoin)
                        {
                            values.Add(sss.getValueForDim(index.ID));
                        }
                        values.RemoveAll(a => a <= 0);

                        if (values.Count > 0)
                        {
                            theNewRow.Dims.Add(index.ID);

                            if (theSaveMode == SaveMode.TechnicalReplicate)
                            {
                                theNewRow.Values.Add(values.Average()); //Technical replicate mode
                            }
                            else if (theSaveMode == SaveMode.MudPIT)
                            {
                                theNewRow.Values.Add(values.Sum()); //MudPit mode
                            } else
                            {
                                throw new Exception("No save mode detected");
                            }
                        }
                    }

                    smJoined.addRow(theNewRow);

                }

                plp.MySparseMatrix = smJoined;
                Console.WriteLine("Sparse matrix updated.");
            }

            plp.Save(fileName);
        }



    }
}
