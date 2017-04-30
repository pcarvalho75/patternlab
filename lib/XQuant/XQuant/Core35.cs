using PatternTools;
using PatternTools.Deisotoping;
using PatternTools.FastaParser;
using PatternTools.SQTParser;
using XQuant.Quants;
using SEPRPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using PatternTools.XIC;
using System.Xml.Serialization;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using PatternTools.MSParser;
using Newtonsoft.Json;
using System.Text;

namespace XQuant
{
    /// <summary>
    /// This is under construction
    /// </summary>
    [Serializable]
    public class Core35
    {


        //-------

        public List<SEProFileInfo> SEProFiles { get; set; }

        public List<QuantPackage2> myQuantPkgs { get; set; }

        public List<AssociationItem> MyAssociationItems { get; set; }

        public XQuantClusteringParameters MyClusterParams { get; set; }

        public Dictionary<string, List<string>> IdentifiedSequencesInFullDirDict { get; set; }

        public List<FastaItem> MyFastaItems { get; set; }

        public decimal ChromeAssociationTolerance { get; set; }

        Dictionary<string, List<string>> peptideProteinDictionary = new Dictionary<string, List<string>>();

        Dictionary<string, List<string>> proteinPeptideDictionary = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> PeptideProteinDictionary
        {
            get
            {
                if (object.Equals(peptideProteinDictionary, null))
                {
                    PreparePeptideProteinDictionary();
                }
                return peptideProteinDictionary;
            }
        }

        public Dictionary<string, List<string>> ProteinPeptideDictionary
        {
            get
            {
                if (object.Equals(proteinPeptideDictionary, null))
                {
                    PrepareProteinPeptideDictionary();
                }
                return proteinPeptideDictionary;
            }
        }


        /// <summary>
        /// To be used for serialization purposes onnly
        /// </summary>
        public Core35()
        {

        }


        public void RemoveDuplicateQuants()
        {
            Console.WriteLine("Remove duplicates activated");
            foreach (QuantPackage2 qp2 in myQuantPkgs)
            {
                Console.WriteLine("PackageName = " + qp2.FileName);
                Console.WriteLine("Quants before grouping = " + qp2.MyQuants.Sum(a => a.Value.Count));

                Dictionary<string, List<Quant>> tmpDict = new Dictionary<string, List<Quants.Quant>>();

                foreach (KeyValuePair<string, List<Quant>> kvp in qp2.MyQuants)
                {
                    var dict = from q in kvp.Value
                               group q by q.Z + "::"+q.PrecursorMZ+":" + q.AverageIntensity(3) into theGroup
                               select new { TheKey = theGroup.Key, TheValues = theGroup.ToList() };

                    tmpDict.Add(kvp.Key, dict.Select(a => a.TheValues[0]).ToList());

                }

                qp2.MyQuants = tmpDict;

                Console.WriteLine("Quants after groupint = " + qp2.MyQuants.Sum(a => a.Value.Count));
            }
            
        }

        public Core35(List<DirectoryClassDescription> dcds, XQuantClusteringParameters myClusteringParameters)
        {
            MyClusterParams = myClusteringParameters;
            IdentifiedSequencesInFullDirDict = new Dictionary<string, List<string>>();
            MyFastaItems = new List<FastaItem>();
            SEProFiles = new List<SEProFileInfo>();

            //First we will need to load all SEPros and generate a list of peptides with their respectve quantitations

            foreach (DirectoryClassDescription cdc in dcds)
            {
                FileInfo[] files = new DirectoryInfo(cdc.MyDirectoryFullName).GetFiles("*.sepr", SearchOption.AllDirectories);

                SEProFiles.Add(new SEProFileInfo(cdc.MyDirectoryFullName, cdc.ClassLabel, cdc.Description, files.Select(a => a.FullName).ToList()));

                foreach (FileInfo fi in files)
                {
                    //Make sure we only have 1 sepro file per directory
                    if (fi.Directory.GetFiles("*.sepr").Count() != 1)
                    {
                        throw new Exception("There can be only one SEPro file per directory; error in directory:\n" + fi.DirectoryName);
                    }

                    Console.WriteLine("Loading " + fi.FullName);
                    ResultPackage rp = ResultPackage.Load(fi.FullName);

                    //Verify if all equivalent ms1 or raw files are in directory

                    List<string> filesInSEPro = RemoveExtensions((from sqt in rp.MyProteins.AllSQTScans
                                                                  select sqt.FileName).Distinct().ToList());


                    List<string> ms1OrRawOrmzMLFiles = fi.Directory.GetFiles("*.ms1").Select(a => a.Name).Concat(fi.Directory.GetFiles("*.RAW").Select(a => a.Name)).Concat(fi.Directory.GetFiles("*.mzML").Select(a => a.Name)).ToList();
                    ms1OrRawOrmzMLFiles = RemoveExtensions(ms1OrRawOrmzMLFiles);

                    //Lets store the fasta items
                    List<MyProtein> proteinsToAnalyze = new List<MyProtein>();

                    if (myClusteringParameters.MaxParsimony)
                    {
                        proteinsToAnalyze = rp.MaxParsimonyList();
                    }
                    else
                    {
                        proteinsToAnalyze = rp.MyProteins.MyProteinList;
                    }


                    IdentifiedSequencesInFullDirDict.Add(fi.Directory.FullName, proteinsToAnalyze.Select(a => a.Locus).ToList());

                    foreach (MyProtein p in proteinsToAnalyze)
                    {
                        if (!MyFastaItems.Exists(a => a.SequenceIdentifier.Equals(p.Locus)))
                        {
                            MyFastaItems.Add(new FastaItem(p.Locus, p.Sequence, p.Description));
                        }
                    }

                    //End storing fasta stuff


                    foreach (string f in filesInSEPro)
                    {
                        if (!ms1OrRawOrmzMLFiles.Contains(f))
                        {
                            throw new Exception("All .ms1, .mzML, or Thermo .RAW files must be placed in each corresponding SEPro directory.  Error in directory:\n" + fi.DirectoryName + "\nfor file:" + f);
                        }
                    }

                }

            }

            Process();

            PreparePeptideProteinDictionary();
            if (myClusteringParameters.RetainOptimal)
            {
                RetainOptimalSignal();
            }

            Console.WriteLine("Done");

        }

        private void PreparePeptideProteinDictionary()
        {
            Console.WriteLine("Generating peptide protein dictionary");

            if (peptideProteinDictionary.Keys.Count > 0)
            {
                Console.WriteLine("\tNo need for this.");
                return;
            }
            //Fill protein peptide dictionary
            

            peptideProteinDictionary = new Dictionary<string, List<string>>();

            List<string> allPeptides = (from qp in myQuantPkgs
                                        from p in qp.MyQuants
                                        select PatternTools.pTools.CleanPeptide(p.Key, true)).Distinct().ToList();


            foreach (string peptide in allPeptides)
            {
                List<FastaItem> theItems = MyFastaItems.FindAll(a => a.Sequence.Contains(peptide));
                peptideProteinDictionary.Add(peptide, theItems.Select(a => a.SequenceIdentifier).ToList());
            }

            Console.WriteLine("Done Generating dictionary");
        }

        private void PrepareProteinPeptideDictionary()
        {
            //Fill protein peptide dictionary
            Console.WriteLine("Generating protein peptide dictionary");

            if (proteinPeptideDictionary.Keys.Count > 0)
            {
                Console.WriteLine("\tNo need for this.");
            }

            proteinPeptideDictionary = new Dictionary<string, List<string>>();

            List<string> allPeptides = (from qp in myQuantPkgs
                                        from p in qp.MyQuants
                                        select p.Key).Distinct().ToList();


            foreach (string peptide in allPeptides)
            {
                List<FastaItem> theItems = MyFastaItems.FindAll(a => a.Sequence.Contains(PatternTools.pTools.CleanPeptide(peptide, true)));

                foreach (FastaItem fi in theItems)
                {
                    if (proteinPeptideDictionary.ContainsKey(fi.SequenceIdentifier))
                    {
                        proteinPeptideDictionary[fi.SequenceIdentifier].Add(peptide);
                    } else
                    {
                        proteinPeptideDictionary.Add(fi.SequenceIdentifier, new List<string>() { peptide });
                    }
                }

            }

            Console.WriteLine("Done Generating dictionary");
        }


        private static List<string> RemoveExtensions(List<string> files)
        {
            return files.Select(a => RemoveExtension(a)).ToList();
        }

        private static string RemoveExtension(string fileName)
        {
            FileInfo fii = new FileInfo(fileName);
            string ext = fii.Extension;
            return fileName.Substring(0, fileName.Length - ext.Length);
        }


        /// <summary>
        /// We recomend runing this after retaining optimal signal.  This will revisit other MS1s to try and find a XIC to reuce the undersampling problem
        /// </summary>
        public void FillInTheGaps(List<string> peptideWithCharges)
        {
            //We need to know the acceptable comaprisons.
            List<int> groups = MyAssociationItems.Select(a => a.Assosication).Distinct().ToList();


            foreach (int group in groups)
            {
                //Get the stuff from this group
                List<AssociationItem> items = MyAssociationItems.FindAll(a => a.Assosication == group);
                List<string> seproFiles = items.Select(a => a.Directory + "\\" + a.Directory + ".sepr").ToList();

                //Now get all peptides from  the group
                List<QuantPackage2> relevantQuantPackages = (from qpackg in myQuantPkgs
                                                            where items.Exists(a => qpackg.FullDirPath.Contains(a.Directory) && a.FileName.Equals(qpackg.FileName))
                                                            select qpackg).ToList();

                //Load all MS1s to RAM
                List<XICGet5> myGetters = new List<XICGet5>(items.Count);
                foreach (AssociationItem ai in items)
                {
                    //Find out the full directory
                    string dtmp = @"\" + ai.Directory;


                    List<string> seprof = (from s in SEProFiles
                                           from d in s.MyFilesFullPath
                                           where d.Contains(dtmp + "\\")
                                           select d).ToList();

                    foreach (string s in seprof)
                    {
                        FileInfo f = new FileInfo(s);

                        //We need to find out if we are dealing with .ms2, .RAW or .mzML
                        string theExtension = GetExtension(f);

                        string fileName = f.Directory.FullName + @"\" + Regex.Replace(ai.FileName, ".sqt", theExtension);
                        myGetters.Add(new XICGet5(fileName));
                    }

                }


                peptideWithCharges.Sort();
                foreach (string peptideWithZ in peptideWithCharges)
                {
                    string[] cols = Regex.Split(peptideWithZ, "::");

                    string peptide = cols[0];
                    int z = int.Parse(cols[1]);

                    //Find out which one has the greatest XIC


                    List<QuantPackage2> qpSearch = relevantQuantPackages.FindAll(a => a.MyQuants.ContainsKey(peptide));

                    List<Quant> refQuants = (from qpckg in qpSearch
                                             from quant in qpckg.MyQuants[peptide]
                                             where quant.Z == z && quant.QuantArea > 0
                                             select quant).OrderByDescending(a => a.QuantArea).ToList();

                    if (refQuants.Count == 0)
                    {
                        continue;
                    }

                    List<IonLight> MyIonsLight = refQuants[0].GetIonsLight();

                    List<string> fileNames = (from qpckg in qpSearch
                                              from quant in qpckg.MyQuants[peptide]
                                              where quant.Z == z && quant.QuantArea > 0
                                              select qpckg.FullDirPath + @"\" + qpckg.FileName).Distinct().ToList();


                    //Find out the tolerance i.e., the XIC above +- the tolerance

                    double lowerBound = MyIonsLight.Min(a => a.RetentionTime) - (double)ChromeAssociationTolerance;
                    double upperBound = MyIonsLight.Max(a => a.RetentionTime) + (double)ChromeAssociationTolerance;


                    //Extract the XICs of this peptide in the not found ones and add them
                    //Find the missing getters
                    List<string> fileGetters = myGetters.Select(a => a.MyFile.FullName).ToList();

                    List<string> fileQuants = new List<string>();

                    string theExtension = null;
                    foreach (string fName in fileNames)
                    {
                        theExtension = GetExtension(new FileInfo(fName));
                        fileQuants.Add(Regex.Replace(fName, ".sqt", theExtension));
                    }




                    List<string> missing = fileGetters.FindAll(a => !fileQuants.Contains(a)).ToList();

                    List<XICGet5> gettersToUse = myGetters.FindAll(a => missing.Contains(a.MyFile.FullName));


                    foreach (XICGet5 xg in gettersToUse)
                    {
                        //The problem is it is getting the reference mass of onw charge when it is supposed to be the other
                        double mz = refQuants[0].PrecursorMZ;
                        double ppmTol = MyClusterParams.ClusteringPPM;
                        double minCurrent = MyClusterParams.MinMaxSignal;
                        double minGapForItemsInCluster = MyClusterParams.MinTimeGapFromItemsInCluster;

                        //Find out a good scan number
                        double maxInt = MyIonsLight.Max(a => a.Intensity);
                        IonLight maxIntIon = MyIonsLight.Find(a => a.Intensity == maxInt);

                        double Ctolerance = (double)ChromeAssociationTolerance;

                        double[,] cluster = xg.GetXIC3(mz, ppmTol, maxIntIon.RetentionTime);

                        if (cluster != null)
                        {
                            string tmpf = xg.MyFile.Name;
                            tmpf = Regex.Replace(tmpf, theExtension, ".sqt");
                            tmpf = Regex.Replace(tmpf, theExtension, ".sqt");
                            QuantPackage2 qp = myQuantPkgs.Find(a => a.FullDirPath.Equals(xg.MyFile.Directory.FullName) && a.FileName.Equals(tmpf));

                            Quant q = new Quants.Quant(cluster, -1, z, mz);

                            if (qp.MyQuants.ContainsKey(peptide))
                            {
                                qp.MyQuants[peptide].Add(q);
                            } else
                            {
                                qp.MyQuants.Add(peptide, new List<Quant>() { q });
                            }
                            
                        }
                    }
                    Console.WriteLine(peptide);
                }
            }
        }

        private static string GetExtension(FileInfo f)
        {
            List<int> theExtCount = new List<int>() { f.Directory.GetFiles("*.RAW").Count(), f.Directory.GetFiles("*.ms1").Count(), f.Directory.GetFiles("*.mzML").Count() };


            string theExtension = null;
            if (theExtCount[0] > 0)
            {
                theExtension = ".RAW";
            }
            else if (theExtCount[1] > 0)
            {
                theExtension = ".ms1";
            }
            else
            {
                theExtension = ".mzML";
            }

            return theExtension;
        }

        public void Process()
        {
            myQuantPkgs = new List<QuantPackage2>();
            SignalGenerator isotopicSignal = new SignalGenerator();

            foreach (SEProFileInfo sfi in SEProFiles)
            {

                foreach (string file in sfi.MyFilesFullPath)
                {

                    Console.WriteLine("Processing for " + file);
                    ResultPackage rp = ResultPackage.Load(file);

                    List<string> filesInSEPro = (from sqt in rp.MyProteins.AllSQTScans
                                                 select sqt.FileName).Distinct().ToList();

                    FileInfo fi = new FileInfo(file);

                    foreach (string msFile in filesInSEPro)
                    {
                        QuantPackage2 qp = new QuantPackage2(msFile, fi.Directory.FullName, sfi.ClassLabel);

                        Console.WriteLine("\t" + msFile);
                        List<string> ms1OrRawOrmzMLFiles = fi.Directory.GetFiles("*.ms1").ToList().Concat(fi.Directory.GetFiles("*.RAW")).Concat(fi.Directory.GetFiles("*.mzML")).ToList().Select(a => a.Name).ToList();


                        int fileToRead = ms1OrRawOrmzMLFiles.FindIndex(a => RemoveExtension(a).Equals(RemoveExtension(msFile)));

                        XICGet5 xic = new XICGet5(fi.DirectoryName + "/" + ms1OrRawOrmzMLFiles[fileToRead]);

                        List<SQTScan> scansTMP = rp.MyProteins.AllSQTScans.FindAll(a => a.FileName.Equals(msFile));

                        if (MyClusterParams.OnlyUniquePeptides)
                        {
                            int removedForNotUnique = scansTMP.RemoveAll(a => a.IsUnique);
                            Console.WriteLine("Scans removed for not dealing with unique peptides: " + removedForNotUnique);
                        }

                        List<SQTLight> scans = scansTMP.Select(a => new SQTLight(a)).ToList();

                        int counter = 0;
                        for (int i = 0; i < scans.Count; i++)
                        {
                            Dictionary<string, List<Quant>> theseQuants = Core35.Quant(xic, scans[i], scans[i].TheoreticalMH, isotopicSignal, scans[i].ScanNumber, MyClusterParams);

                            foreach (var kvp in theseQuants)
                            {
                                kvp.Value.RemoveAll(a => a.MyIons.GetLength(1) < MyClusterParams.MinMS1Counts);

                                if (kvp.Value.Count > 0)
                                {
                                    if (qp.MyQuants.ContainsKey(kvp.Key))
                                    {
                                        qp.MyQuants[kvp.Key].AddRange(kvp.Value);
                                    }
                                    else
                                    {
                                        qp.MyQuants.Add(kvp.Key, kvp.Value);
                                    }
                                }
                            }

                            counter++;
                            Console.Write("\rScans Processed: " + counter + "/" + scans.Count);

                        }

                        //Store them
                        myQuantPkgs.Add(qp);
                        Console.WriteLine("\nTotal quants stored so far = " + myQuantPkgs.Sum(a => a.MyQuants.Count));
                        Console.WriteLine("Total files analyzed so far = " + myQuantPkgs.Count);

                        Console.WriteLine("Done procesing :" + msFile);
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        System.GC.Collect();

                    }

                }

            }


            GenerateAssociationItems();
        }

        /// <summary>
        /// Binary Serialization
        /// </summary>
        /// <param name="fileName"></param>
        public void Serialize(string fileName)
        {
            //Make sure we are saving something clean
            RemoveDuplicateQuants();

            PreparePeptideProteinDictionary();
            PrepareProteinPeptideDictionary();

            ////Now lets save it.
            System.IO.FileStream flStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            bf.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesAlways;
            bf.Serialize(flStream, this);
            flStream.Close();
        }

        public static Core35 Deserialize(string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            Core35 core = (Core35)bformatter.Deserialize(stream);
            stream.Close();

            //Make sure we are loading something clean
            core.RemoveDuplicateQuants();

            return core;
        }

        public void SerializeJSON(string fileName)
        {

            PreparePeptideProteinDictionary();
            PrepareProteinPeptideDictionary();

            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(JsonConvert.SerializeObject(this,
                Formatting.Indented)
                );
            sw.Close();
        }

        public static Core35 DeserializeJSON(string fileName)
        {

            StreamReader sr = new StreamReader(fileName);
            Core35 c = JsonConvert.DeserializeObject<Core35>(
                sr.ReadToEnd());
            sr.Close();

            return c;

        }



        public static Dictionary<string, List<Quant>> Quant(XICGet5 xic, SQTLight theScn, double theoreticalMH, SignalGenerator isotopicSignal, int scnNo, XQuantClusteringParameters myParams)
        {

            Dictionary<string, List<Quant>> theResults = new Dictionary<string, List<Quants.Quant>>();

            //We will obtain the isotopic signal and obtain the XIC according to the most intense isotope
            List<double> isoSignal = isotopicSignal.GetSignal(5, theoreticalMH, 0);
            int maximumSignalIsotope = isoSignal.IndexOf(isoSignal.Max());

           
            foreach (int z in myParams.AcceptableChargeStates)
            {
                double pMass = ((double)z - 1.0) * 1.007276466;
                double iMass = (maximumSignalIsotope * 1.00335);
                double mz = (theoreticalMH + pMass + iMass) / (double)z;

                double[,] ic = xic.GetXIC3(Math.Round(mz, 4), myParams.ClusteringPPM, scnNo);

                if (ic != null)
                {
                    Quant q = new Quants.Quant(ic, theScn.ScanNumber, z, Math.Round(mz, 4));

                    if (theResults.ContainsKey(theScn.PeptideSequenceCleaned))
                    {
                        theResults[theScn.PeptideSequenceCleaned].Add(q);
                    } else
                    {
                        theResults.Add(theScn.PeptideSequenceCleaned, new List<Quants.Quant>() { q });
                    }
                }
            }

            return theResults;

        }

        /// <summary>
        /// If we have a peptide identified with charge 2 and charge 3, it will delete the quantitation of the identification having the least signal.
        /// </summary>
        public void RetainOptimalSignal()
        {
            Console.WriteLine("Retain optimal signal activated");
            //We will search between the different charge states for all peptides and retain only the one with max signal

            List<string> allPeptides = peptideProteinDictionary.Keys.ToList();

            allPeptides.Sort();

            foreach (string peptide in allPeptides)
            {

                //Extract maximum quant
                List<QuantPackage2> searchQP = myQuantPkgs.FindAll(a => a.MyQuants.ContainsKey(peptide)).ToList();

                List<Quant> quants = (from qp in searchQP
                                      from q in qp.MyQuants[peptide]
                                      orderby q.AverageIntensity(10) descending
                                      select q).ToList();

                if (quants.Count == 0) { continue; }

                int optimalZ = quants[0].Z;

                foreach (QuantPackage2 qp in myQuantPkgs)
                {
                    if (qp.MyQuants.ContainsKey(peptide))
                    {
                        int qr = qp.MyQuants[peptide].RemoveAll(a => a.Z != optimalZ);
                    }
                }
            }

        }

        private void GenerateAssociationItems()
        {
            MyAssociationItems = new List<AssociationItem>();

            List<int> classes = (from q in myQuantPkgs
                                 select q.ClassLabel).Distinct().ToList();

            foreach (int c in classes)
            {
                List<string> directories = (from q in myQuantPkgs
                                            where q.ClassLabel == c
                                            select new DirectoryInfo(q.FullDirPath).Name).Distinct().ToList();

                foreach (string dir in directories)
                {
                    List<string> files = (from q in myQuantPkgs
                                          where q.ClassLabel == c && new DirectoryInfo(q.FullDirPath).Name.Equals(dir)
                                          select q.FileName).Distinct().ToList();

                    foreach (string file in files)
                    {
                        AssociationItem ai = new AssociationItem(c, dir, file, -1);
                        MyAssociationItems.Add(ai);
                    }
                }
            }


        }


    }
}



