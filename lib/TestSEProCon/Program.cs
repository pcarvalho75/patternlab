using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSEProCon
{
    class Program
    {
        static void Main(string[] args)
        {
            SEPRPackage.Parameters myParams = new SEPRPackage.Parameters();
            myParams.LoadFromFile(@"seproParams.seprms");
            myParams.SeachResultDirectoy = @"";
            myParams.AlternativeProteinDB = @"";

            PatternTools.SQTParser.SQTParser sqtParser = new PatternTools.SQTParser.SQTParser(@"sqtFileName");
            PatternTools.FastaParser.FastaFileParser fastaParser = new PatternTools.FastaParser.FastaFileParser();
            fastaParser.ParseFile(new System.IO.StreamReader(myParams.AlternativeProteinDB), false, PatternTools.FastaParser.DBTypes.IDSpaceDescription);
            SEProcessor.SEProConn seproConn = new SEProcessor.SEProConn(myParams, sqtParser.Scans, fastaParser, true, false);

            seproConn.myResults.Save("mySEProPckg.sepr");

        }
    }
}
