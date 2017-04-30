using PatternTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQuant
{
    [Serializable]
    public class SEProFileInfo
    {
        public string MyDirectoryFullName { get; set; }
        public int ClassLabel { get; set; }
        public string ClassDescription { get; set; }
        public List<string> MyFilesFullPath { get; set; }

        public SEProFileInfo(string myFullNameDir, int classLabel, string description, List<string> myFiles)
        {
            MyDirectoryFullName = myFullNameDir;
            ClassLabel = classLabel;
            ClassDescription = description;
            MyFilesFullPath = myFiles;
        }

        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public SEProFileInfo() {}


    }
}
