using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SEPRPackage;

namespace Regrouper.GroupingElements.Parser
{
    public class ResultEntryForGrouper
    {
        public FileInfo MyFileInfo { get; set; }
        public ProteinManager MyProteinManager { get; set; }

        public ResultEntryForGrouper(FileInfo fi, ProteinManager pm)
        {
            MyFileInfo = fi;
            MyProteinManager = pm;
        }
    }
}
