using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQuant
{
    [Serializable]
    public class AssociationItem
    {
        public int Label { get; set; }
        public string Directory { get; set; }
        public string FileName { get; set; }
        public int Assosication { get; set; }

        /// <summary>
        /// Used only for serialization purposes
        /// </summary>
        public AssociationItem() { }

        public AssociationItem(int label, string directory, string fileName, int association)
        {
            Label = label;
            Directory = directory;
            FileName = fileName;
            Assosication = association;
        }
    }
}
