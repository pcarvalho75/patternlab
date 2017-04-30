using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PatternTools
{
    public partial class ClassSelection : Form
    {
        SparseMatrix sm;
        List<int> targetClasses;
        List<int> currentClasses;

        public ClassSelection(List<int> currentClasses, List<int> targetClasses, SparseMatrix theMatrix, bool enableChoosingNoClasses, int initialNoClasses)
        {
            InitializeComponent();

            if (enableChoosingNoClasses)
            {
                numericUpDownMaxClasses.Enabled = true;
                numericUpDownMaxClasses.Maximum = targetClasses.Count;
            }

            //Lets keep a reference to our original matrix
            sm = theMatrix;
            this.targetClasses = targetClasses;
            this.currentClasses = currentClasses;

            targetClasses.Sort((a, b) => a.CompareTo(b));
            currentClasses.Sort((a, b) => a.CompareTo(b));

            labelActualClass.Text = currentClasses.Count.ToString();
            numericUpDownMaxClasses.Value = targetClasses.Count;
            
            labelNewLabels.Text = "";

            foreach (int c in targetClasses)
            {
                labelNewLabels.Text += c + " ";
            }

            foreach (int c in currentClasses)
            {
                string toAdd = c.ToString();
                if (sm.ClassDescriptionDictionary.ContainsKey(c)) {
                    toAdd += " " + sm.ClassDescriptionDictionary[c];
                }
                checkedListBoxClasses.Items.Add(toAdd);

                //kryptonCheckedListBoxClasses.Items.Add(toAdd);
            }

            //Make sure we got the desired number of initial classes
             if ((int)numericUpDownMaxClasses.Value > initialNoClasses)
             {
                 numericUpDownMaxClasses.Value = (decimal)initialNoClasses;
                 Reduce();
             }

        }

        private void kryptonNumericUpDownMaxClasses_ValueChanged(object sender, EventArgs e)
        {
            Reduce();
        }

        private void Reduce()
        {
            //Reconstruct output label array
            labelNewLabels.Text = "";
            for (int c = 0; c < (int)numericUpDownMaxClasses.Value; c++)
            {
                labelNewLabels.Text += targetClasses[c] + " ";
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            //Verify if the required number of classes were selected

            if (checkedListBoxClasses.CheckedItems.Count != (int)numericUpDownMaxClasses.Value)
            {
                MessageBox.Show("Please select up to 3 classes. For a Venn Diagram or specify in the numeric control otherwise.");
                return;
            }


            //Find the selected classes
            List<int> selectedIndexes = new List<int>();
            //for (int i = 0; i < kryptonCheckedListBoxClasses.CheckedItems.Count; i++)
            //{
            //    string[] stuff = Regex.Split(kryptonCheckedListBoxClasses.CheckedItems[i].ToString(), " ");
            //    selectedIndexes.Add(int.Parse(stuff[0]));
            //}

            for (int i = 0; i < checkedListBoxClasses.CheckedItems.Count; i++)
            {
                string[] stuff = Regex.Split(checkedListBoxClasses.CheckedItems[i].ToString(), " ");
                selectedIndexes.Add(int.Parse(stuff[0]));
            }

            //Finaly, modify the matrix
            SparseMatrix newMatrix = new SparseMatrix();


            List<sparseMatrixRow> rowsToEliminate = new List<sparseMatrixRow>();
            Dictionary<int, string> newClassDescriptionDisctionary = new Dictionary<int, string>();

            for (int r = 0; r < sm.theMatrixInRows.Count; r++)
            {
                bool eliminateRow = true;
                for (int i = 0; i < selectedIndexes.Count; i++)
                {
                    if (sm.theMatrixInRows[r].Lable == selectedIndexes[i])
                    {
                        try
                        {
                            newClassDescriptionDisctionary.Add(targetClasses[i], sm.ClassDescriptionDictionary[sm.theMatrixInRows[r].Lable]);
                        }
                        catch { }

                        sm.theMatrixInRows[r].Lable = targetClasses[i];

                        eliminateRow = false;
                        break;
                    }
                }
                if (eliminateRow)
                {
                    rowsToEliminate.Add(sm.theMatrixInRows[r]);
                }
            }

            foreach (sparseMatrixRow r in rowsToEliminate)
            {
                sm.theMatrixInRows.Remove(r);
            }

            sm.ClassDescriptionDictionary = newClassDescriptionDisctionary;

            //Update the class descriptions

            this.Close();

        }


    }
}
