using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools
{
        [Serializable]
        public class DirectoryClassDescription
        {
            public string MyDirectoryFullName { get; set; }
            public int ClassLabel { get; set; }
            public string Description { get; set; }

            public DirectoryClassDescription(string directoryFullName, int label, string desc)
            {
                MyDirectoryFullName = directoryFullName;
                ClassLabel = label;
                Description = desc;
            }
        }
}
