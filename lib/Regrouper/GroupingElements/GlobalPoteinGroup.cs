using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEPRPackage;

namespace Regrouper.GroupingElements
{
    public struct GlobalProteinGroup
    {
        string myLoci;
        int myGroupNo;
        ProteinGroupType myGroupType;

        public int GroupNo { get { return myGroupNo; } }
        public string Locci { get { return myLoci; } }
        public ProteinGroupType GroupType { get {return myGroupType;} }

        public GlobalProteinGroup (int groupNo, string locci, ProteinGroupType groupType) {
            myGroupNo = groupNo;
            myLoci = locci;
            myGroupType = groupType;
        }
    }
}
