using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PepExplorer2.Result2
{
    public partial class MAlginerrForm : Form
    {
        public MAlignerViewer MyViewer
        {
            get { return mAlignerViewer1; }
            set { mAlignerViewer1 = value; }
        }

        public MAlginerrForm()
        {
            InitializeComponent();
        }

        public MAlginerrForm(string resultFile)
        {
            InitializeComponent();

            mAlignerViewer1.LoadResult(resultFile);
            mAlignerViewer1.UpdateLayout();
        }
    }
}
