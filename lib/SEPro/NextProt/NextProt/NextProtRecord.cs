using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextProt
{
    [Serializable]
    public class NextProtRecord
    {
        public string ID { get; private set; }
        public bool AntiBody { get; private set; }
        public bool Proteomics { get; private set; }
        public string Chr { get; private set; }

        public NextProtRecord()
        {

        }

        public NextProtRecord(string id, bool antibody, bool proteomics, string chr)
        {
            ID = id;
            AntiBody = antibody;
            Proteomics = proteomics;
            Chr = chr;
        }
    }
}
