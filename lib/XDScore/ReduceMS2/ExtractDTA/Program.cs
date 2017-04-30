using PatternTools.MSParser;
using PatternTools.MSParserLight;
using PatternTools.SQTParser2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractDTA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate DTA

            string dtaDir = @"C:\Users\pcarvalho\Desktop\Juli_fosfo\DTA\out";
            string inputSQT = @"C:\Users\pcarvalho\Desktop\Colaborations\Juli_fosfo\DTA\20140410_JF_HAP50mmmS2.sqt";
            string inputMS2 = @"C:\Users\pcarvalho\Desktop\Colaborations\Juli_fosfo\DTA\20140410_jf_hap50mmms2.ms2";
            


            if (true) //Remove MSMS from a file
            {
                int targetNoSpectra = 9917;
                List<MSLight> theMS = PatternTools.MSParserLight.ParserLight.ParseLightMS2(inputMS2);
                string outputMS2 = @"C:\Users\pcarvalho\Desktop\Colaborations\Juli_fosfo\DTA\Removed3_20140410_jf_hap50mmms2.ms2";

                while (theMS.Count > targetNoSpectra)
                {
                    int scnRemove = PatternTools.pTools.getRandomNumber(theMS.Count - 1);
                    theMS.RemoveAt(scnRemove);
                }

                PatternTools.MSParser.MSPrinter.PrintMSListToFile(theMS, outputMS2, "", MSFileFormat.ms2);

                Console.WriteLine("Done!");

            }

            //Generate a side by side table of XDScores and AScores
            if (false) //Generate a side by side table of XDScores and AScores
            {

                XDScore.XDScore xdScore = new XDScore.XDScore(new List<string>() { inputSQT }, true);

                SQTParser2 sqtParser = new SQTParser2();
                List<SQTScan2> theSQT = sqtParser.Parse(inputSQT);
                
                //Parse the aScore File
                StreamReader sr = new StreamReader(@"C:\Users\pcarvalho\Desktop\Juli_fosfo\DTA\AllResults.txt");
                string line;
                StreamWriter sw = new StreamWriter(@"C:\Users\pcarvalho\Desktop\Juli_fosfo\DTA\AScoreXDScoreGoodScores.txt");
                while ((line = sr.ReadLine()) != null)
                {
                    string[] cols = Regex.Split(line, "[-|\t]");
                    double aScore = double.Parse(cols[2]);
                    int scanNo = int.Parse(cols[0]);
                    SQTScan2 theScan = theSQT.Find(a => a.ScanNumber == scanNo);

                    if (theScan.Matches[0].PrimaryScore > 2 && theScan.ChargeState == 2)
                    {
                        double xd = xdScore.GetDeltaScore(theScan, false);

                        sw.WriteLine(scanNo + "\t" + aScore + "\t" + xd);
                    }
                }
                sr.Close();
                sw.Close();
            }

            if (false)
            {
                

                SQTParser2 sqtParser = new SQTParser2();
                List<SQTScan2> myScans = sqtParser.Parse(inputSQT);
                List<MSLight> theMS2 = PatternTools.MSParserLight.ParserLight.ParseLightMS2(inputMS2);

                myScans.RemoveAll(a => a.Matches.Count == 0);
                myScans.RemoveAll(a => !a.Matches[0].PeptideSequence.Contains("79"));

                foreach (SQTScan2 scn in myScans)
                {
                    try
                    {
                        Console.Write(".");
                        MSLight thems = theMS2.Find(a => a.ScanNumber == (int)scn.ScanNumber);

                        string pepSeq = scn.Matches[0].PeptideSequence;
                        pepSeq = Regex.Replace(pepSeq, @"\(\+79\.966300\)", "Z");
                        StreamWriter sw = new StreamWriter(dtaDir + "/" + scn.ScanNumber + "-" + pepSeq + ".dta");

                        sw.WriteLine(thems.MHPrecursor[0] + " " + thems.Zs[0].ToString());
                        foreach (Ion i in thems.Ions)
                        {
                            sw.WriteLine(i.MZ + " " + i.Intensity);
                        }
                        sw.Close();
                    } catch
                    {
                        Console.Write("P");
                    }

                }
            }

            if (false)
            {
                DirectoryInfo di = new DirectoryInfo(dtaDir);
                FileInfo[] theFiles = di.GetFiles("*.dta");

                StreamWriter sw = new StreamWriter(@"C:\Users\pcarvalho\Desktop\Juli_fosfo\DTA\result.txt");

                foreach (FileInfo fi in theFiles)
                {
                    // Read file data
                    FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                    fs.Close();

                    // Generate post objects
                    Dictionary<string, object> postParameters = new Dictionary<string, object>();
                    
                    postParameters.Add("fileformat", "txt");
                    postParameters.Add("userfile", new FormUpload.FileParameter(data, fi.Name, "application/msword"));

                    postParameters.Add("mods", "1"); //Cystein
                    postParameters.Add("MAX_FILE_SIZE", "40000");

                    string [] cols = Regex.Split(fi.Name, "[-|.]");
                    string pepS = Regex.Replace(cols[1], "Z", "*");
                    postParameters.Add("sequence", pepS);
                    postParameters.Add("submit", "Calculate Ascore");

                    HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(@"http://ascore.med.harvard.edu/trim/trim-single.php", "PhosphoPeptide", postParameters);

                    // Process response
                    StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                    string fullResponse = responseReader.ReadToEnd();
                    webResponse.Close();

                    string[] cols2 = Regex.Split(fullResponse, "\n");

                    Regex getNo = new Regex("[0-9]+.[0-9]+");

                    try
                    {

                        foreach (var m in getNo.Matches(cols2[39]))
                        {
                            sw.WriteLine(fi.Name + "\t" + m.ToString());
                            Console.WriteLine(m.ToString());
                            sw.Flush();
                        }
                    }
                    catch { Console.Write("f"); }

                    fi.Delete();
                    
                }

                sw.Close();
            }

            if (false)
            {
                // Read file data
                string file = @"C:\Users\pcarvalho\Desktop\Juli_fosfo\ToMSAorNotToMSA\test.txt";
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                fs.Close();

                // Generate post objects
                Dictionary<string, object> postParameters = new Dictionary<string, object>();

                //postParameters.Add("fileformat", "txt");
                postParameters.Add("userfile", new FormUpload.FileParameter(data, file, "application/msword"));

                postParameters.Add("mods", "1"); //Cystein
                postParameters.Add("MAX_FILE_SIZE", "40000");
                postParameters.Add("sequence", "IEDVGS*DEEDDSGK");
                postParameters.Add("submit", "Calculate Ascore");

                HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(@"http://ascore.med.harvard.edu/trim/trim-single.php", "PhosphoPeptide", postParameters);

                // Process response
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string fullResponse = responseReader.ReadToEnd();
                webResponse.Close();

                string[] cols = Regex.Split(fullResponse, "\n");

                Regex getNo = new Regex("[0-9]+.[0-9]+");
                
                foreach (var m in getNo.Matches(cols[39])) 
                {
                    Console.WriteLine( m.ToString());
                }
               
                Console.WriteLine("Done");
            }

        }
    }
}
