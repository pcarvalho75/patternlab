using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GO.UniProtAssociationParser
{
    public class UniProtOrGenePeptItem
    {
        public List<string> IDs { get; set; }
        public List<string> GOItems { get; set; }

        public UniProtOrGenePeptItem(List<string> goItems)
        {
            IDs = goItems;
            GOItems = new List<string>();
        }
    }
}
