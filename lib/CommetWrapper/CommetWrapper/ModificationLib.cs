using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CometWrapper
{
    [Serializable]
    public class ModificationLib
    {
        public List<Modification> MyModifications { get; set; }

        public ModificationLib()
        {
            MyModifications = new List<Modification>();
        }
    }


}
