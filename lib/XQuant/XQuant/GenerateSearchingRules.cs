using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace XQuant
{
    public partial class GenerateSearchingRules : UserControl
    {
        public GenerateSearchingRules()
        {
            InitializeComponent();
        }

        public List<AssociationItem> MyAssociations
        {
            get;
            set;
        }

        public decimal ChromTolerance
        {
            get { return numericUpDownChromTolerance.Value; }
            set { numericUpDownChromTolerance.Value = value; }
        }

        public List<AssociationItem> GetAssociations ()
        {
              List<AssociationItem> myAssociations = new List<AssociationItem>();
                try
                {
                    //We allways need to remove the focus from the table so we can get all the updated values.
                    numericUpDownChromTolerance.Focus();
                    for (int i = 1; i < dataGridViewRules.Columns.Count; i += 2)
                    {
                        for (int j = 0; j < dataGridViewRules.Rows.Count; j++)
                        {
                            string[] cols = Regex.Split(dataGridViewRules.Columns[i].HeaderText, ":");
                            int label = int.Parse(cols[0]);
                            string directory = cols[1];
                            string fileName = dataGridViewRules.Rows[j].Cells[i - 1].Value.ToString();
                            int association = int.Parse(Convert.ToString(dataGridViewRules.Rows[j].Cells[i].FormattedValue.ToString()));

                            AssociationItem ai = new AssociationItem(label, directory, fileName, association);
                            myAssociations.Add(ai);
                        }
                    }
 

                } catch (Exception e)
                {
                    MessageBox.Show("Error generating associations: " + e.Message);
                }
                return myAssociations;
        }

        public void FillTable()
        {
            dataGridViewRules.Rows.Clear();
            dataGridViewRules.Columns.Clear();

            List<string> ColumnHeaders = (from ai in MyAssociations
                                          select ai.Label + ":" + ai.Directory).Distinct().ToList();

            int maxFiles = -1;
            ColumnHeaders.Sort();

            foreach (string rh in ColumnHeaders)
            {
                int fileCount = (from ai in MyAssociations
                                 where (ai.Label + ":" + ai.Directory).Equals(rh)
                                 select ai.FileName).Distinct().Count();

                if (fileCount > maxFiles)
                {
                    maxFiles = fileCount;
                }
            }

            List<int> associations = new List<int>() { -1 };
            for (int i = 1; i <= maxFiles; i++)
            {
                associations.Add(i);
            }

            dataGridViewRules.EnableHeadersVisualStyles = false;
            foreach (string rh in ColumnHeaders)
            {
                string[] cols = Regex.Split(rh, ":");
                var col = new DataGridViewTextBoxColumn();
                col.HeaderText = rh;

                col.HeaderCell.Style.BackColor = Utils.color[int.Parse(cols[0]) + 1];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewRules.Columns.Add(col);

                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderCell.Style.BackColor = Utils.color[int.Parse(cols[0]) + 1];
                col2.HeaderText = rh;
                col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewRules.Columns.Add(col2);
            }

            for (int i = 0; i < maxFiles; i++)
            {
                dataGridViewRules.Rows.Add();
            }

            for (int i = 0; i < ColumnHeaders.Count; i++)
            {
                List<AssociationItem> files = MyAssociations.FindAll(a =>(a.Label + ":" + a.Directory).Equals(ColumnHeaders[i])); 
                
                files.Sort((a, b) => (a.Label + ":" + a.Directory).CompareTo(b.Label + ":" + b.Directory));

                for (int j = 0; j < files.Count; j++)
                {
                    dataGridViewRules.Rows[j].Cells[i * 2].Value = files[j].FileName;
                    dataGridViewRules.Rows[j].Cells[(i * 2) + 1].Value = files[j].Assosication.ToString();
                }

            }
        }
    }
}
