using PatternTools.SQTParser2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CometWrapper
{
    public static class RemoteCall
    {
        public static List<SQTScan2> CallComet(cometParams cp, string searchFile)
        {
            FileInfo fi = new FileInfo(searchFile);

            string cpf = CometWrapper.GenerateTheSearchParamsString(cp);

            //Write the comet result to the directory
            StreamWriter sw = new StreamWriter(fi.DirectoryName + "/" + "comet.params");
            sw.WriteLine(cpf);
            sw.Close();

            string cometCall = CometWrapper.GetCometPath();
            CometWrapper.CallComet(fi, cometCall, cpf);

            string sqtFileTmp = fi.Name.Replace(fi.Extension, ".sqt");
            string sqtFile = fi.Directory.FullName + "/" + sqtFileTmp;

            PatternTools.SQTParser2.SQTParser2 sqtParser = new SQTParser2();
            
            List<SQTScan2> theResult = sqtParser.Parse(sqtFile);

            int removedNoMatch = theResult.RemoveAll(a => a.Matches.Count == 0);

            return theResult;
        }
    }
}
