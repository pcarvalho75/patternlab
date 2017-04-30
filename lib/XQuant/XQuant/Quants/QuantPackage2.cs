using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQuant.Quants
{
    [Serializable]
    public class QuantPackage2
    {
        public string FileName { get; set; }
        public string FullDirPath { get; set; }
        public int ClassLabel { get; set; }
        public Dictionary<string, List<Quant>> MyQuants { get; set; }


        /// <summary>
        /// Will include a list of quants to the dictionary
        /// </summary>
        /// <param name="theQuants"></param>


        public QuantPackage2(string fileName, string fullDirPath, int classLabel)
        {
            FileName = fileName;
            FullDirPath = fullDirPath;
            ClassLabel = classLabel;
            MyQuants = new Dictionary<string, List<Quant>>();
        }

        /// <summary>
        /// Used for serialization purposes
        /// </summary>
        public QuantPackage2() { }
    }

}
