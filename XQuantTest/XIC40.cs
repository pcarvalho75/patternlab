using CSMSL.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQuantTest
{
    public class XIC40
    {
        public XIC40 (string ms1File)
        {
            //Read the file
            using (MSDataFile dataFile = new AgilentDDirectory("somerawfile.d", true))
            {
                foreach (MSDataScan scan in dataFile.Where(scan => scan.MsnOrder > 1))
                {
                    Console.WriteLine("Scan #{0}", scan.SpectrumNumber);
                }
            }
        }
    }
}
