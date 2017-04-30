using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.PTMMods
{
    [Serializable]
    public class DefaultModifications
    {
        public static List<ModificationItem> TheModifications = new List<ModificationItem>()
        {
            new ModificationItem("C", "Carbamidomethilation", 57.02146, false, false, false, true),
            new ModificationItem("M", "Mox", 15.9949, false,false,true, true)
        };

        public List<ModificationItem> MyModifications { get; set; }



    }
}
