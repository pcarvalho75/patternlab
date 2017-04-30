using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using PatternTools.FastaParser;

namespace hmm3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class HMM : IHMM
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionaryIdSequence">A maximum of 100 sequences can be sent</param>
        /// <returns></returns>
        public HMMResult[] Scans(Dictionary<string, string> dictionaryIdSequence)
        {
            //Set maximum of 
            if (dictionaryIdSequence.Keys.Count > 1000)
            {
                return null;
            }
            Dictionary<string, HMMResult[]> result = new Dictionary<string, HMMResult[]>();

            string forSafety =  DateTime.Now.Millisecond.ToString();
            string inputFile = @"E:\WebServiceData\HMM\seqBatch" + forSafety + ".txt";
            string outputFile = @"E:\WebServiceData\HMM\domtblout" + forSafety + ".txt";

            StreamWriter sw = new StreamWriter(inputFile);
            foreach (KeyValuePair<string, string> fi in dictionaryIdSequence)
            {
                sw.WriteLine(">"+fi.Key);
                sw.WriteLine(fi.Value);
            }
            sw.Close();

            string theOutPut = FireUpHMM(inputFile, outputFile);

            //Parse the result
            HMMResult[] theResult = ParseHMM3Output(outputFile);

            File.Delete(inputFile);
            File.Delete(outputFile);

            return theResult;
        }

        public HMMResult [] Scan(string fastaSequence)
        {
            //Dump the fasta seqeuence to a file
            StreamWriter sw = new StreamWriter(@"E:\WebServiceData\HMM\seqRead.txt");
            sw.WriteLine(">SeqRead");
            sw.WriteLine(fastaSequence);
            sw.Close();

            string inputFile = @"E:\WebServiceData\HMM\seqRead.txt";
            string outputFile = @"E:\WebServiceData\HMM\domtblout.txt";

            string theOutPut = FireUpHMM(inputFile, outputFile);

            //Parse the result
            HMMResult[] theResult = ParseHMM3Output(outputFile);
            return theResult;
        }

        private static HMMResult[] ParseHMM3Output(string outputFile)
        {
            StreamReader sr = new StreamReader(outputFile);
            string line = "";
            List<HMMResult> theResults = new List<HMMResult>();
            while ((line = sr.ReadLine()) != null)
            {
                if (!line.StartsWith("#") && line.Length > 0)
                {
                    theResults.Add(new HMMResult(line));
                }

            }

            sr.Close();


            return theResults.ToArray();
        }

        private static string FireUpHMM(string inputFile, string outputFile)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"E:\academicSoftwares\hmmer3\hmmscan.exe";
            startInfo.Arguments = @"--cpu 12 --domtblout " + outputFile + @" E:\ProteinDatabases\PFam\Pfam-A.hmm " + inputFile;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            string theOutPut = "";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    using (StreamReader reader = exeProcess.StandardOutput)
                    {
                        theOutPut = reader.ReadToEnd();
                    }
                }

            }
            catch (Exception e)
            {
                StreamWriter logError = File.AppendText(@"E:\WebServiceData\HMM\errorLog");
                logError.WriteLine(DateTime.Now.ToString() + "\t" + e.Message);
                logError.Close();
                //return null;
            }
            return theOutPut;
        }

    }
}
