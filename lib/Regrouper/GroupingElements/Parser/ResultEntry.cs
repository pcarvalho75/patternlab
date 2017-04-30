using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEPRPackage;
using System.IO;

namespace Regrouper.GroupingElements.Parser
{
    public class ResultEntry
    {
        // Can be either object from SEPro or PEX
        public ResultPackage MyResultPackage { get; set; }
        public FileInfo MyFileInfo { get; set; }
        public int ClassLabel { get; set; }

        public ResultEntry(ResultPackage myResultPackage, FileInfo fi, int classLabel)
        {
            MyResultPackage = myResultPackage;
            MyFileInfo = fi;
            ClassLabel = classLabel;
        }
    }
}
