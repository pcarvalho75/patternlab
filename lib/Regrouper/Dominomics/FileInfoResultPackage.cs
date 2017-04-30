using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regrouper.Dominomics
{
    class FileInfoResultPackage
    {
        public FileInfo MyFileInfo { get; set; }
        public object MyResultPackage { get; set; }
        public int ClassLabel { get; set; }

        public FileInfoResultPackage(FileInfo fileInfo, object resultPackage, int classLabel)
        {
            MyFileInfo = fileInfo;
            MyResultPackage = resultPackage;
            ClassLabel = classLabel;
        }
    }
}
