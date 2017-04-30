using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPRPackage
{
    //Defines how many peptides the proteins share within their group
    public enum ProteinGroupType
    {
        All,
        Unique,
        Single,
        Some,
        Undetermined,
    }
}
