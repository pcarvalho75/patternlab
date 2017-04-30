using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepExplorer2.DataBase
{
    public class DataBaseRegistry
    {
        public string Identifier { get; set; }
        public string Description { get; set; }
        public string Sequence { get; set; }

        public DataBaseRegistry(string id, string desc, string seq)
        {
            Identifier = id;
            Description = desc;
            Sequence = seq;
        }

    }    
}
