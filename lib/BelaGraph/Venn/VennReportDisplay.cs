using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using PatternTools;

namespace Venn
{
    public partial class ReportDisplay : Form
    {


        public ReportDisplay(List<int> myPopulation, SparseMatrix sm, SparseMatrixIndexParserV2 smip)
        {
            InitializeComponent();
            dataGridViewResults.Rows.Clear();
            int counter = 0;

            foreach (int i in myPopulation)
            {
                //Find out in how many input vectors of the sparse matrix this index appears
                int apperanceCounter = 0;
                double totalSpecCount = 0;
                string spectralCountsPerExperiment = "";

                int replicateCounter = 0;
                foreach (sparseMatrixRow smr in sm.theMatrixInRows)
                {
                    if (smr.Dims.Contains(i))
                    {
                        int itsIndex = smr.Dims.IndexOf(i);
                        totalSpecCount += smr.Values[itsIndex];
                        apperanceCounter++;
                        if (sm.ClassDescriptionDictionary.ContainsKey(smr.Lable)) {
                            spectralCountsPerExperiment += smr.FileName + "( " + sm.ClassDescriptionDictionary[smr.Lable] + ":" + smr.Values[itsIndex] + ") :: ";
                            replicateCounter++;
                        }
                    }
                }


                dataGridViewResults.Rows.Add();
                int index = dataGridViewResults.Rows.Count - 1;

                counter++;
                dataGridViewResults.Rows[index].HeaderCell.Value = counter.ToString();

                dataGridViewResults.Rows[index].Cells[0].Value = i;
                dataGridViewResults.Rows[index].Cells[1].Value = smip.GetName(i);
                dataGridViewResults.Rows[index].Cells[2].Value = spectralCountsPerExperiment;
                dataGridViewResults.Rows[index].Cells[3].Value = replicateCounter;
                dataGridViewResults.Rows[index].Cells[4].Value = totalSpecCount;
                dataGridViewResults.Rows[index].Cells[5].Value = smip.GetDescription(i);
            }

        }

        private void buttonSaveReport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "TXT file (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
             
                foreach (DataGridViewRow row in dataGridViewResults.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++ )
                    {
                        sw.Write(row.Cells[i].Value + "\t");
                    }
                    sw.WriteLine("");
                }
                sw.Close();
            }
        }


    }
}
