using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PatternTools;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using System.IO;
using PLP;
using System.Data;
using TrendQuest.ClusteringItems;

namespace TrendQuest
{
    public partial class TrendQuestControl : UserControl
    {
        PatternLabProject plp;

        DataTable plotDataTable;
        
        SparseMatrix myNeverNormalizedSparseMatrix;

        List<proteinScore> resultsLinearAnalysis = new List<proteinScore>();
        List<SpectralNode> resultsClusterAnalysis = new List<SpectralNode>();
        

        List<proteinScore> myProteinScores = new List<proteinScore>();

        public TrendQuestControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// This method works in conjuction with the prepare for cluster, or linear analysis
        /// </summary>
        private void matrixpreparation(int minDataPoints, double minAvgSignal)
        {

            myNeverNormalizedSparseMatrix = PatternTools.ObjectCopier.Clone(plp.MySparseMatrix);

            //Before normalizing, we need to eliminate any dims that did not achieve the minimum signal
            List<int> sparseMatrixDims = plp.MySparseMatrix.allDims();

            foreach (int dimension in sparseMatrixDims)
            {
                List<double> dimValues = plp.MySparseMatrix.ExtractDimValues(dimension, 0, true);



                if (dimValues.Count(a => a > 0) < minDataPoints)
                {
                    plp.MySparseMatrix.eliminateDim(dimension, 0, true);
                }

                double AvgValue = pTools.Average(dimValues);
                if (AvgValue < minAvgSignal)
                {
                    plp.MySparseMatrix.eliminateDim(dimension, 0, true);
                    continue;
                }


            }

        }


        private List<SpectralNode> ClusterMatrix(SparseMatrix myMatrix)
        {
            SparseMatrix transposedMatrix;

            transposedMatrix = TransposeMatrixForClustering(myMatrix);

            //Now lets get to clustering-----------------------------------------------

            SpectralAngleClustering sCluster = new SpectralAngleClustering(transposedMatrix);

            List<SpectralNode> clusterNodes = sCluster.kMeans((int)numericUpDownTargetClusterNumber.Value);
           
            //Eliminate nodes that do not attend minimum requirements
            List<SpectralNode> nodes2Remove = new List<SpectralNode>();
            foreach (SpectralNode sn in clusterNodes)
            {
                if (sn.MyInputVectors.Count < (int) numericUpDownMinNodesPerCluster.Value)
                {
                    nodes2Remove.Add(sn);
                }
            }

            foreach (var sn in nodes2Remove)
            {
                clusterNodes.Remove(sn);
            }

            return (clusterNodes);

        }

        private static SparseMatrix TransposeMatrixForClustering(SparseMatrix myMatrix)
        {

            //We need to shatter the matrix---------------------
            SparseMatrix myShatteredMatrix = new SparseMatrix();
            List<int> classes = myMatrix.ExtractLabels();
            List<int> dims = myMatrix.allDims();

            //We also need to now how many input vectors per class
            Dictionary<int, int> classNoInputVector = new Dictionary<int, int>();
            foreach (int c in classes)
            {
                classNoInputVector.Add(c, 0);
            }

            foreach (var row in myMatrix.theMatrixInRows)
            {
                classNoInputVector[row.Lable]++;
            }

            //Now we are ready to shater the matrix. Foreach class we need to construct the averaged input vector
            foreach (int c in classes)
            {
                sparseMatrixRow newRow = new sparseMatrixRow(c);

                foreach (int dim in dims)
                {
                    List<double> dimValues = myMatrix.ExtractDimValues(dim, c, false);
                    double sum = 0;
                    for (int i = 0; i < dimValues.Count; i++)
                    {
                        sum += dimValues[i];
                    }
                    newRow.Dims.Add(dim);
                    newRow.Values.Add(sum / classNoInputVector[c]);
                }

                myShatteredMatrix.addRow(newRow);
            }
            //Done Shattering-------------------------------------------------

            //Begin transposing the matrix

            SparseMatrix transposedMatrix = new SparseMatrix();

            for (int i = 0; i < dims.Count; i++)
            {
                List<double> values = myShatteredMatrix.ExtractDimValues(dims[i], 0, true);
                sparseMatrixRow r = new sparseMatrixRow(dims[i], classes, values);
                transposedMatrix.addRow(r);
            }


            //Done transposing the matrix.  The final transposed matrix will be such that the
            //lable of each row reflects the proteins index.
            //Before
            //0 1:11 2:2.6 3:11
            //1 1:6 2:12 3:0
            //
            //After transposing
            //1 0:11 1:6
            //2 0:2.6 1:12
            //3 0:11 1:0
            return transposedMatrix;
        }



        private void kryptonButtonPrepareforClustering_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButtonUncheck2_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButtonPlotCluster_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButtonExport_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButtonExpAll_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButtonSavePlot_Click(object sender, EventArgs e)
        {


        }

        private void kryptonButtonVerifyClusterValidity_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButtonPlotDensity_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButtonEstimatePValue_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButtonBrowseIndex_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonSaveGraphData_Click(object sender, EventArgs e)
        {
            try {
            saveFileDialog1.Filter = "Text Files (txt)|*.txt";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                sw.WriteLine("#X value followed by Y values");
                foreach (Series s in chartTrend.Series)
                {
                    foreach (DataPoint p in s.Points)
                    {
                        string yJoined = string.Join("\t", p.YValues);
                        sw.WriteLine(p.XValue + "\t" + yJoined);
                    }
                }
                sw.Close();
                MessageBox.Show("File saved");
            }

            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }



        private void buttonSavePlot_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG (*.png)|*.png";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                chartTrend.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
                MessageBox.Show("File saved");
            }
        }



        private void buttonPrepareForClustering_Click(object sender, EventArgs e)
        {
            string previousButtonText = buttonPrepareForClustering.Text;
            this.Update();

            try
            {
                matrixpreparation((int)numericUpDownMinDatapointsClustering.Value, double.Parse(textBoxMinAvgSignalCluster.Text));
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "Error");
                return;
            }


            //Update the label
            int noDims = plp.MySparseMatrix.allDims().Count;
            labelNoElements.Text = noDims.ToString();

            groupBoxClusteringStep3.Enabled = true;
            buttonCluster.Enabled = true;
            buttonPrepareForClustering.Text = previousButtonText;

            //Make sure we allow the cluster validity for later on
            groupBoxClusteringStep3.Enabled = true;
            
        }

        private void buttonUncheck2_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < dataGridViewCluster.Rows.Count; r++)
            {
                dataGridViewCluster.Rows[r].Cells[0].Value = false;
            }
        }

        private void buttonExpAll_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) { return; }


            //We will gather all the protein IPIs so they can be analyzed by GOEx
            for (int r = 0; r < dataGridViewCluster.Rows.Count; r++)
            {

                if ((bool)dataGridViewCluster.Rows[r].Cells[0].Value == true)
                {

                    string fileName = folderBrowserDialog1.SelectedPath + "/cluster" + r + ".txt";
                    System.IO.StreamWriter SW = new System.IO.StreamWriter(fileName);
                    SW.WriteLine("#TrendQuest");
                    SW.WriteLine("#Minimum Data points required per protein: {0}\t", numericUpDownMinDatapointsClustering.Value);
                    SW.WriteLine("#Minimum Average Signal: {0}\t", textBoxMinAvgSignalCluster.Text);

                    foreach (var iv in resultsClusterAnalysis[(int)dataGridViewCluster.Rows[r].Cells[1].Value].MyInputVectors)
                    {
                        SW.WriteLine(iv.Lable + "\t" + plp.MyIndex.GetName(iv.Lable) + "\t" + plp.MyIndex.GetDescription(iv.Lable) + "\t");
                    }
                    SW.Close();
                }
            }

            MessageBox.Show("Done!");

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (plp.MyIndex.TheIndexes.Count == 0)
            {
                MessageBox.Show("please load the index file.", "Error");
                return;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }

            //We will gather all the protein IPIs so they can be analyzed by GOEx
            List<int> indexes = new List<int>();
            for (int r = 0; r < dataGridViewCluster.Rows.Count; r++)
            {
                if ((bool)dataGridViewCluster.Rows[r].Cells[0].Value == true)
                {
                    //We have come across a selected cluster so we should keep its members
                    foreach (var iv in resultsClusterAnalysis[(int)dataGridViewCluster.Rows[r].Cells[1].Value].MyInputVectors)
                    {
                        indexes.Add(iv.Lable);
                    }

                }
            }


            System.IO.StreamWriter SW = new System.IO.StreamWriter(saveFileDialog1.FileName);
            SW.WriteLine("#TrendQuest");
            SW.WriteLine("#Minimum Data points required per protein: {0}\t", numericUpDownMinDatapointsClustering.Value);
            SW.WriteLine("#Minimum Average Signal: {0}\t", textBoxMinAvgSignalCluster.Text);

            foreach (int index in indexes)
            {
                SW.WriteLine(index + "\t" + plp.MyIndex.GetName(index) + "\t" + plp.MyIndex.GetDescription(index));
            }

            SW.Close();

            MessageBox.Show("Done!");
        }

        private void buttonPlotCluster_Click(object sender, EventArgs e)
        {
            
            List<int> aDimVector = resultsClusterAnalysis[(int)dataGridViewCluster.Rows[0].Cells[1].Value].MyInputVectors[0].Dims;

            double[] consensus = new double[aDimVector.Count];
            double conCounter = 0;


            plotDataTable = new DataTable();
            plotDataTable.Columns.Add("ID");
            plotDataTable.Columns.Add("Description");

            DataColumn dc = new DataColumn("Euclidian");
            //dc.DataType = System.Type.GetType("System.Double");
           
            plotDataTable.Columns.Add(dc);

            for (int i = 0; i < aDimVector.Count; i++)
            {
                DataColumn column = new DataColumn();
                column.DataType = typeof(double);
                plotDataTable.Columns.Add(aDimVector[i].ToString());
            }

            dataGridViewPlotData.AutoGenerateColumns = true;
            dataGridViewPlotData.DataSource = plotDataTable;

            for (int r = 0; r < dataGridViewCluster.Rows.Count; r++)
            {
                if ((bool)dataGridViewCluster.Rows[r].Cells[0].Value)
                {
                    //Lets Construct a line for each element in the node

                    foreach (sparseMatrixRow inputVector in resultsClusterAnalysis[(int)dataGridViewCluster.Rows[r].Cells[1].Value].MyInputVectors)
                    {
                        conCounter++;

                        //We should make a clone
                        sparseMatrixRow cv = new sparseMatrixRow(0);

                        foreach (double v in inputVector.Values)
                        {
                            cv.Values.Add(v);
                        }

                        cv.ConvertToUnitVector();

                        for (int i = 0; i < cv.Values.Count; i++)
                        {
                            consensus[i] += cv.Values[i];
                        }


                        DataRow row = plotDataTable.NewRow();
                        row["ID"] = plp.MyIndex.GetName(inputVector.Lable);
                        row["Description"] = plp.MyIndex.GetDescription(inputVector.Lable);

                        for (int i = 0; i < inputVector.Dims.Count; i++)
                        {
                            row[inputVector.Dims[i].ToString()] = cv.Values[i];
                        }

                        plotDataTable.Rows.Add(row);

                    }

                }
            }

            UpdateEuclidian();

            Plot();
        }

        private void UpdateEuclidian()
        {

            DataRow[] cR = plotDataTable.Select("ID = 'Consensus'");

            if (cR.Length > 0)
            {
                cR[0].Delete();
            }

            double [] updatedConsensus = new double[plotDataTable.Columns.Count - 3];
            //Calculate Consensus
            foreach (DataRow r in plotDataTable.Rows)
            {
                for (int i = 3; i < plotDataTable.Columns.Count; i++)
                {
                    updatedConsensus[i - 3] += double.Parse(r[plotDataTable.Columns[i].ColumnName].ToString());
                }
            }

            for (int i = 0; i < updatedConsensus.Length; i++)
            {
                updatedConsensus[i] /= (double)plotDataTable.Rows.Count;
            }

            //Add consensus to dataset
            DataRow row = plotDataTable.NewRow();
            row["ID"] = "Consensus";
            row["Description"] = "Consensus";

            for (int i = 0; i < updatedConsensus.Length; i++)
            {
                row[i + 3] = updatedConsensus[i];
            }

            plotDataTable.Rows.Add(row);
            //


            //Update Euclidian
            foreach (DataRow r in plotDataTable.Rows)
            {
                List<double> thisRow = new List<double>(plotDataTable.Columns.Count - 3);

                for (int i = 3; i < plotDataTable.Columns.Count; i++)
                {
                    thisRow.Add(double.Parse(r[plotDataTable.Columns[i].ColumnName].ToString()));
                }

                double distance = PatternTools.pTools.EuclidianDistance(updatedConsensus.ToList(), thisRow);

                r["Euclidian"] = distance;
            }

        }

        /// <summary>
        /// Returns the sum of the euclidian stress
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public double EStress(SpectralNode sn)
        {
            double eSumm = 0;

            List<double> consensus = sn.Consensus().ToList();

            foreach (sparseMatrixRow r in sn.MyInputVectors)
            {
                sparseMatrixRow cv = new sparseMatrixRow(0);

                foreach (double v in r.Values)
                {
                    cv.Values.Add(v);
                }

                cv.ConvertToUnitVector();

                eSumm += PatternTools.pTools.EuclidianDistance(consensus, cv.Values);
            }

            return eSumm;
            
        }

        private void Plot()
        {
            chartTrend.Series.Clear();

            foreach (DataRow dr in plotDataTable.Rows)
            {
                string label = dr["ID"] + "\t" + dr["Description"];
                Series s = new Series(label);
                s.ToolTip = label;

                if (dr["ID"].Equals("Consensus"))
                {
                    s.BorderWidth = 7;

                    if (!checkBoxPlotCOnsensus.Checked)
                    {
                        continue;
                    }
                }

                for (int i = 3; i < plotDataTable.Columns.Count; i++)
                {
                    double x = double.Parse(plotDataTable.Columns[i].ColumnName);
                    double y = double.Parse(dr[i].ToString());
                    DataPoint dp = new DataPoint(x, y);
                    s.Points.Add(dp);
                }
                s.ChartType = SeriesChartType.Spline;
                s.IsVisibleInLegend = false;



                chartTrend.Series.Add(s);
            }

            List<int> dimsVector = resultsClusterAnalysis[(int)dataGridViewCluster.Rows[0].Cells[1].Value].MyInputVectors[0].Dims;

            double minAxisVal = dimsVector[0];
            double maxAxisVal = dimsVector.Last();
            chartTrend.ChartAreas[0].Axes[0].Minimum = minAxisVal;
            chartTrend.ChartAreas[0].Axes[0].Maximum = maxAxisVal;

            chartTrend.ChartAreas[0].Axes[1].Maximum = 1;

            chartTrend.ChartAreas[0].AxisX.Title = "Time";
            chartTrend.ChartAreas[0].AxisY.Title = "Signal intensity";
        }

        private void buttonCluster_Click(object sender, EventArgs e)
        {

            string bText = buttonCluster.Text;
            buttonCluster.Text = "Working";
            this.Update();

            resultsClusterAnalysis = ClusterMatrix(plp.MySparseMatrix);

            //Fill up the table
            dataGridViewCluster.Rows.Clear();
            for (int i = 0; i < resultsClusterAnalysis.Count; i++)
            {
                dataGridViewCluster.Rows.Add();
                dataGridViewCluster.Rows[i].Cells[0].Value = false;
                dataGridViewCluster.Rows[i].Cells[1].Value = i;
                dataGridViewCluster.Rows[i].Cells[2].Value = resultsClusterAnalysis[i].MyInputVectors.Count;
                dataGridViewCluster.Rows[i].Cells[3].Value = Math.Round(EStress(resultsClusterAnalysis[i]),4);
            }


            //-Exiting......
            this.buttonCluster.Text = bText;

            buttonPlotCluster.Enabled = true;
            buttonExport.Enabled = true;
            buttonExpAll.Enabled = true;
            buttonUncheck2.Enabled = true;

        }


        private void buttonLoadPLP_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "PatternLab Project (*.plp)|*.plp";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                plp = new PatternLabProject(openFileDialog1.FileName);
                groupBoxStep2.Enabled = true;
                labelNoElements.Text = plp.MyIndex.AllNames.Count.ToString();
            }
        }

        private void buttonRefreshPlot_Click(object sender, EventArgs e)
        {
            UpdateEuclidian();
            Plot();
        }

        private void groupBoxStep1_Enter(object sender, EventArgs e)
        {

        }
    }
}
