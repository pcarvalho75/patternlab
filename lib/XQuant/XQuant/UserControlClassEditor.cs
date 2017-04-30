using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XQuant.Quants;

namespace XQuant
{
    public partial class UserControlClassEditor : UserControl
    {
        public UserControlClassEditor()
        {
            InitializeComponent();
        }


        public void MyQuantPckg (List<QuantPackage2> myQuants) {
            
            //Setup datagrid
            dataGridViewQuantPckgs.Rows.Clear();
            dataGridViewQuantPckgs.Columns.Clear();

            var col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "DirName";
            col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewQuantPckgs.Columns.Add(col1);

            var col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "Class Label";
            col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewQuantPckgs.Columns.Add(col2);

            foreach (QuantPackage2 qp in myQuants) {

                int index = dataGridViewQuantPckgs.Rows.Add();
                dataGridViewQuantPckgs.Rows[index].Cells[0].Value = qp.FullDirPath;
                dataGridViewQuantPckgs.Rows[index].Cells[1].Value = qp.ClassLabel;

            }
            
        }

        public void UpdateCoreWithLabels(Core35 theCore)
        {

            for (int y = 0; y < dataGridViewQuantPckgs.Rows.Count; y++)
            {
                string dir = dataGridViewQuantPckgs.Rows[y].Cells[0].Value.ToString();
                int index = theCore.myQuantPkgs.FindIndex(a => a.FullDirPath.Equals(dir));

                if (index == -1)
                {
                    throw new Exception("Error in class assignment linkage.");
                }

                int lbl = int.Parse(dataGridViewQuantPckgs.Rows[y].Cells[1].Value.ToString());
                theCore.myQuantPkgs[index].ClassLabel = lbl;

                int index2 = theCore.MyAssociationItems.FindIndex(a => a.FileName.Equals(theCore.myQuantPkgs[index].FileName) && dir.Contains(a.Directory));
                theCore.MyAssociationItems[index2].Label = lbl;
            }
        }

        private void dataGridViewQuantPckgs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
