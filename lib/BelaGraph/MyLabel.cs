using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools;

namespace BelaGraph
{
    class MyLabel : System.Windows.Controls.Label
    {
        public List<int> MyExtraInfo { get; set; }
        public SparseMatrix mySparseMatrix {get; set;}
        public SparseMatrixIndexParserV2 mySparseMatrixIndexParser { get; set; }

        public MyLabel () : base () { }
    }
}
