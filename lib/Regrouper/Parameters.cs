using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Regrouper.GroupingElements.Parser;

namespace Regrouper
{
    public class Parameters
    {
        public bool EliminateDecoys { get; set; }
        public string DecoyTag { get; set; }
        public bool EliminateIn { get; set; }
        public bool PeptideParserOnlyUniquePeptides { get; set; }


        //Parameters related with the group generation for sparse matrix
        public GroupContentType MyGroupContent { get; set; }
        public SEPRPackage.ProteinGroupType MyGroupType { get; set; }
        public SEPRPackage.ProteinOutputType MyProteinType { get; set; }

        public bool UseMaximumParsimony { get; set; }
    }
}
