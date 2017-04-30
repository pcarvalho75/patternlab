using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PatternTools;

namespace Venn
{
    public partial class CellReportDisplay : Form
    {
        SparseMatrix sm;
        SparseMatrixIndexParserV2 smip;

        public CellReportDisplay(List<int> indexes, SparseMatrix sm, SparseMatrixIndexParserV2 smip)
        {
            InitializeComponent();
            this.smip = smip;
            this.sm = sm;
            richTextBoxResults.Clear();

            foreach (int i in indexes)
            {
                //Find out in how many input vectors of the sparse matrix this index appears
                int apperanceCounter = 0;
                double totalSpecCount = 0;
                foreach (sparseMatrixRow smr in sm.theMatrixInRows)
                {
                    if (smr.Dims.Contains(i))
                    {
                        int itsIndex = smr.Dims.IndexOf(i);
                        totalSpecCount += smr.Values[itsIndex];
                        apperanceCounter++;
                    }
                }
                richTextBoxResults.AppendText(i + "\t" + smip.GetName(i) + "\t" + smip.GetDescription(i) + "\n");
            }
        }


    }
}
