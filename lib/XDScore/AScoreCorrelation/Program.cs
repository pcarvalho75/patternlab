using PatternTools;
using PatternTools.SQTParser2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AScoreCorrelation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //Parse AScore result
            string inputAscore = @"C:\Users\pcarvalho\Desktop\SyntheticLib\10\phosphoRS_HCD_10.csv";
            string inputXDScore = @"C:\Users\pcarvalho\Desktop\SyntheticLib\10\10.sqt";

            List<PhosphoAnalysis> phosfo = new List<PhosphoAnalysis>();

            Console.WriteLine("Parsing PhosphoScores");
            string line;
            StreamReader sr = new StreamReader(inputAscore);
            Regex pSplitter = new Regex(Regex.Escape("."));
            int counter = 0;
            while ((line = sr.ReadLine()) != null)
            {
                counter++;

                if (counter <= 1)
                {
                    continue;
                }
                string[] cols = Regex.Split(line, ",");
                string confidence = cols[0];
                string sequence = cols[1];
                int scanNo = int.Parse(cols[25]);

                PhosphoAnalysis p = new PhosphoAnalysis(sequence, scanNo, confidence, cols[12]);
                phosfo.Add(p);

            }

            //Remove all non-fosfo hits

            int removed = phosfo.RemoveAll(a => !Regex.IsMatch(a.Sequence, "[a-z]"));

            sr.Close();

            Console.WriteLine("Parsing sqt file");
            SQTParser2 sqtParser2 = new SQTParser2();
            List<SQTScan2> sqts = sqtParser2.Parse(inputXDScore, 50);

            List<double> goodScores = new List<double>();
            List<double> badScores = new List<double>();
            foreach (PhosphoAnalysis p in phosfo)
            {
                int index = sqts.FindIndex(a => a.ScanNumber == p.ScanNo);

                if (index > -1)
                {
                    double xdScore = XDScore.XDScore.GetDeltaScore(sqts[index], true);

                    if (!double.IsNegativeInfinity(xdScore)) 
                    {
                        if (p.Confidence.Equals("High") || p.Confidence.Equals("Medium"))
                        {
                            int noSites = Regex.Matches(p.Sequence, "[a-z]").Count;
                            int NoConfidenceAssignments = Regex.Matches(p.FosfoScores, "100").Count;

                            if (NoConfidenceAssignments == noSites)
                            {
                                goodScores.Add(xdScore);
                            }
                            else
                            {
                                badScores.Add(xdScore);
                            }
                            
                        }
                        
                    }

                }
            }

            double avgGood = goodScores.Average();
            double avgBad = badScores.Average();

            double goodSTD = PatternTools.pTools.Stdev(goodScores, true);
            double badSTD = PatternTools.pTools.Stdev(badScores, true);

            double bothTails;
            double leftTail;
            double rightTail;

            alglib.studentttest2(goodScores.ToArray(), goodScores.Count, badScores.ToArray(), badScores.Count, out bothTails, out leftTail, out rightTail);


            Console.WriteLine("Done!");
            
            //Application.Run(new Form1());
        }
    }
}
