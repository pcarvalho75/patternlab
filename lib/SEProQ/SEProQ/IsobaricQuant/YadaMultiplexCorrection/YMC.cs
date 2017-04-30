using PatternTools.MSParserLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProQ.IsobaricQuant.YadaMultiplexCorrection
{
    public class YMC
    {
        public Dictionary<string, List<int>> fileNameScanNumberMultiplexDictionary { get; set; }

        public YMC(DirectoryInfo yadaDir)
        {
            fileNameScanNumberMultiplexDictionary = new Dictionary<string,List<int>>();

            List<FileInfo> ms2Files = yadaDir.GetFiles("*.ms2").ToList();

            foreach (FileInfo fi in ms2Files)
            {
                

                List<MSLight> theMS = PatternTools.MSParserLight.ParserLight.ParseLightMS2(fi.FullName);

                List<MSLight> multiplexed = theMS.FindAll(a => a.ZLines.Count > 1);
                
                fileNameScanNumberMultiplexDictionary.Add(fi.Name, multiplexed.Select(a => a.ScanNumber).ToList());

                Console.WriteLine("File {0} has {1} of {2} are multiplexed spectra", fi.Name, multiplexed.Count, theMS.Count);

            }
        }
    }
}
