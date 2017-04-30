using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PepExplorer2.DeNovo
{
    /// <summary>
    /// De Novo file Parser.
    /// </summary>
    public class DeNovoParser
    {
        /// <summary>
        /// The type of DeNovo file to be parsed.
        /// </summary>
        public DeNovoOption DeNovoType { get; set; }

        /// <summary>
        /// The directory where the files are saved.
        /// </summary>
        public string DirectoryPath { get; set; }

        /// <summary>
        /// The list of parsed registries.
        /// </summary>
        public List<DeNovoRegistry> DeNovoRegistryList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DeNovoParser(DeNovoOption dnvOpt, string path)
        {
            DeNovoType = dnvOpt;
            DirectoryPath = path;
            DeNovoRegistryList = new List<DeNovoRegistry>();
        }

        /// <summary>
        /// This method scans he given directory for all files available for the analysis, it not discriminates formats.
        /// </summary>
        /// <param name="path"></param>
        public void ScanDirectory()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);
            List<FileInfo> fileList = dirInfo.GetFiles("*.*").ToList();

			if (fileList.Count < 1)
			{
				throw new System.ArgumentException("\nNo file found in the directory\n");
			}

			for (int i = 0; i < fileList.Count; i++)
			{
				Console.WriteLine("Processing file " + fileList[i]);
                if (DeNovoType == DeNovoOption.Peaks70 || DeNovoType == DeNovoOption.Peaks75 || DeNovoType == DeNovoOption.NOVOR)
                {
                    fileList = fileList.FindAll(a => a.Extension.Equals(".csv"));
                }
                else if (DeNovoType == DeNovoOption.MSGFDB)
                {
                    fileList = fileList.FindAll(a => a.Extension.Equals(".tsv"));
                }
				ParseFile(fileList[i]);
			}
		}

        /// <summary>
        /// The parsing scan each file and each line, storing the desirable attributes.
        /// </summary>
        /// <param name="file">A list of DeNovo Registries</param>
        public void ParseFile(FileInfo file)
        {

            StreamReader sr = new StreamReader(file.FullName);
            string line;

            if (DeNovoType == DeNovoOption.PepNovo)
            {
                if (file.Extension != ".txt")
                {
                    return;
                }
                try
                {
                    string scanNumber = null;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, @">>"))
                        {
                            //Match match = Regex.Match(line, @"\.(\d{6})\.");
                            Match match = Regex.Match(line, "Scans:([0-9]+)");
                            scanNumber = match.Groups[0].ToString();
                            scanNumber = scanNumber.Remove(0, 6);
                            scanNumber = scanNumber.Replace(".", "");
                        }
                        else if (Regex.IsMatch(line, @"^\d"))
                        {
                            string[] cols = Regex.Split(line, "\t");
                            int rank = Convert.ToInt16(cols[0]);
                            double score = Double.Parse(cols[1]);
                            string ptmSeq = cols[7];
                            int charge = Convert.ToInt16(cols[6]);

                            DeNovoRegistry reg = new DeNovoRegistry(rank, int.Parse(scanNumber), score, ptmSeq, charge, file.FullName);
                            DeNovoRegistryList.Add(reg);
                        }
                        else if (Regex.IsMatch(line, @"^\s*$"))
                        {
                            if (scanNumber != null)
                            {
                                scanNumber = null;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n[ERROR 103] Parsing failed\n" + e);
                }
            }
            else if (DeNovoType == DeNovoOption.PNovo)
            {
                DeNovoRegistry dnr = new DeNovoRegistry();
                string fileName = null;
                int scanNumber = -1;
                int chargeState = -1;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] cols = Regex.Split(line, "\t");

                    if (line.StartsWith("S"))
                    {
                        //This file was generated using mgf files
                        if (line.Contains("Charge:"))
                        {
                            Match matchScan = Regex.Match(line, "Scans:([0-9]+)");
                            Match matchCharge = Regex.Match(line, "Charge:([0-9]+)");

                            string[] zState = matchCharge.Groups[0].ToString().Split(':');
                            string[] scnNo = matchScan.Groups[0].ToString().Split(':');



                            fileName = file.Name;
                            scanNumber = int.Parse(scnNo[1]);
                            chargeState = int.Parse(zState[1]);
                        }
                        else //this file was generated using ms2 files
                        {
                            string[] cols2 = Regex.Split(cols[1], @"\.");
                            fileName = cols2[0];
                            scanNumber = int.Parse(cols2[1]);
                            chargeState = int.Parse(cols2[3]);
                        }
                    }
                    else if (line.StartsWith("P"))
                    {
                        string r1 = cols[0].Substring(1, cols[0].Length - 1);
                        int rank = int.Parse(r1);
                        double dnovoScore = double.Parse(cols[2]);
                        DeNovoRegistryList.Add(new DeNovoRegistry(rank, scanNumber, dnovoScore, cols[1], chargeState, fileName));
                    }
                }

            }
            else if (DeNovoType == DeNovoOption.Peaks70)
            {
                //try
                //{
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith("Scan"))
                    {

                        string[] cols = Regex.Split(line, ",");
                        string[] scan = Regex.Split(cols[0], ":");

                        string ptmSeq = cols[1];
                        int charge = Convert.ToInt16(cols[5]);

                        // HACK only rank 1 is considered when using Peaks, for now this is hard-coded.
                        int rank = 1;

                        // HACK ALC will be used for now, maybe local score could be used as well.
                        double score = Convert.ToDouble(cols[3]);

                        DeNovoRegistry reg;
                        int n;

                        if (int.TryParse(scan[0], out n))
                        {
                            reg = new DeNovoRegistry(rank, int.Parse(scan[0]), score, ptmSeq, charge, file.FullName + "-" + scan[0]);
                        }
                        else
                        {
                            string[] tmp = Regex.Split(scan[1], " ");
                            reg = new DeNovoRegistry(rank, int.Parse(tmp[0]), score, ptmSeq, charge, file.FullName + "-" + scan[0]);
                        }

                        DeNovoRegistryList.Add(reg);
                    }
                }
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("\n[ERROR 103] Parsing failed\n" + e);
                //}

            }
            else if (DeNovoType == DeNovoOption.Peaks75)
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith("Scan"))
                    {

                        string[] cols = Regex.Split(line, ",");
                        string[] scan = Regex.Split(cols[0], ":");

                        string ptmSeq = cols[1];
                        int charge = Convert.ToInt16(cols[6]);

                        // HACK only rank 1 is considered when using Peaks, for now this is hard-coded.
                        int rank = 1;

                        // HACK ALC will be used for now, maybe local score could be used as well.
                        double score = Convert.ToDouble(cols[3]);

                        DeNovoRegistry reg;
                        int n;

                        if (int.TryParse(scan[0], out n))
                        {
                            reg = new DeNovoRegistry(rank, int.Parse(scan[0]), score, ptmSeq, charge, file.FullName + "-" + scan[0]);
                        }
                        else
                        {
                            string[] tmp = Regex.Split(scan[1], " ");
                            reg = new DeNovoRegistry(rank, int.Parse(tmp[0]), score, ptmSeq, charge, file.FullName + "-" + scan[0]);
                        }

                        DeNovoRegistryList.Add(reg);
                    }
                }
            }
            else if (DeNovoType == DeNovoOption.Peaks80)
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith("Fraction") && line.Length > 5)
                    {

                        string[] cols = Regex.Split(line, ",");
                        int scan = int.Parse(cols[1]);

                        string ptmSeq = cols[3];
                        int charge = Convert.ToInt16(cols[8]);

                        // HACK only rank 1 is considered when using Peaks, for now this is hard-coded.
                        int rank = 1;

                        // HACK ALC will be used for now, maybe local score could be used as well.
                        double score = Convert.ToDouble(cols[5]);

                        DeNovoRegistry reg;

                        reg = new DeNovoRegistry(rank, scan, score, ptmSeq, charge, cols[2]);

                        DeNovoRegistryList.Add(reg);
                    }
                }
            }
            else if (DeNovoType == DeNovoOption.NOVOR)
            {
                int counter = 0;
                string fileName = "-1";

                while ((line = sr.ReadLine()) != null)
                {

                    if (line.Length > 0)
                    {
                        if (line.StartsWith("# input file"))
                        {
                            string[] cols = Regex.Split(line, " = ");
                            fileName = cols[1];
                        }

                        if (Regex.IsMatch(line, "^[0-9]"))
                        {
                            //id, scanNum, RT, mz(data), z, pepMass(denovo), err(data-denovo), ppm(1e6*err/(mz*z)), score, peptide, aaScore,
                            string[] cols = Regex.Split(line, ",");


                            int scan = int.Parse(cols[1]);

                            double denovoScore = 0;
                            if (cols[8].Length > 0 && !cols[8].Equals(" "))
                            {
                                denovoScore = double.Parse(cols[8]);
                            }

                            string ptmSequence = Regex.Replace(cols[9], " ", "");
                            int z = int.Parse(cols[4]);

                            string cleanSequence = PatternTools.pTools.CleanPeptide(ptmSequence, true);


                            DeNovoRegistry dvr = new DeNovoRegistry(1, scan, denovoScore, ptmSequence, z, fileName);
                            DeNovoRegistryList.Add(dvr);

                        }
                    }
                    counter++;
                }
            }
            else if (DeNovoType == DeNovoOption.ListOfPeptides)
            {
                int counter = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {
                        continue;
                    }


                    string pepSequence = PatternTools.pTools.CleanPeptide(line, true);
                    counter++;
                    DeNovoRegistry reg = new DeNovoRegistry(0, counter, (double)1, pepSequence, 1, file.Name);
                }
            }
            else if (DeNovoType == DeNovoOption.PepNovoFull)
            {
                if (file.Extension != ".txt")
                {
                    return;
                }

                int counter = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    List<string> cols = Regex.Split(line, "\t").ToList();
                    List<string> peps = Regex.Split(cols.Last(), "-").ToList();

                    counter++;

                    for (int i = 0; i < peps.Count; i++)
                    {

                        DeNovoRegistry reg = new DeNovoRegistry(i, int.Parse(cols[1]), double.Parse(cols[4]), peps[i], 0, file.FullName + "-" + counter);
                        DeNovoRegistryList.Add(reg);
                    }
                }
            }
            else if (DeNovoType == DeNovoOption.MSGFDB)
            {
                if (file.Extension != ".tsv")
                {
                    return;
                }

                int counter = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {

                    }
                    else
                    {
                        //SpecFile	SpecIndex	Scan#	FragMethod	Precursor	PMError(ppm)	Charge	Peptide	Protein	DeNovoScore	MSGFScore	SpecProb	P-value	FDR	PepFDR	Position	internalFilename	ProteinID	Comment
                        List<string> cols = Regex.Split(line, "\t").ToList();

                        counter++;

                        int rank = 1;
                        int scan = int.Parse(cols[2]);
                        double dnvScore = double.Parse(cols[9]);
                        string ptmSeq = cols[7];
                        int charge = int.Parse(cols[6]);
                        string fName = cols[0];


                        DeNovoRegistry reg = new DeNovoRegistry(rank, scan, dnvScore, ptmSeq, charge, fName);
                        DeNovoRegistryList.Add(reg);
                    }
                }
            }

        }
    }
}
