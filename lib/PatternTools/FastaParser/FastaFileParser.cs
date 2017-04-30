using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;

namespace PatternTools.FastaParser
{
    public enum DBTypes
    {
        NCBInr,
        IPI,
        Contaminant,
        UniProt,
        NeXtProt,
        IDSpaceDescription
    }

    public class FastaFileParser
    {
        List<FastaItem> myItems = new List<FastaItem>();
        double massOfLargestSequence = -1;
        double massOfSmallestSequence = -1;
        string dbName;
        AminoacidMasses aa = new AminoacidMasses();


        public string DBName
        {
            get
            {
                return dbName;
            }
            set { dbName = value; }
        }

        /// <summary>
        /// The number of AA's
        /// </summary>
        public double MonoisotopicMassOfLargestSequence
        {
            get
            {
                if (massOfLargestSequence == -1)
                {
                    massOfLargestSequence = myItems.Max(a => aa.CalculatePeptideMass(a.Sequence, false, true, false));
                }
                return massOfLargestSequence;
            }
        }

        public double MonoisotopicMassOfSmallestSequence
        {
            get
            {
                if (massOfSmallestSequence == -1 && myItems.Count > 0)
                {
                    massOfSmallestSequence = myItems.Min(a => aa.CalculatePeptideMass(a.Sequence, false, true, false));
                }
                return massOfSmallestSequence;
            }
        }

        public List<FastaItem> MyItems { 
            get  {return myItems;}
            set { myItems = value; }
        }

        public FastaFileParser() {
            myItems = new List<FastaItem>(1000000);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="removeReverse"></param>
        /// <param name="removeIPIVersionNumber"></param>
        /// <param name="considerOnlyFirstName"></param>
        /// <param name="useRegexForTitleOrDescription">If you wish to only consider items that have certain words in their descriptions or names.  You should also set the Regex parameter</param>
        /// <param name="theRegex"></param>
        public void ParseFile(
            StreamReader sr,
            bool removeReverse,
            DBTypes dbType,
            bool useRegexForTitleOrDescription = false,
            Regex theRegex = null
            )
        {
            //Prepare the stream Reader variables
            string read;
            Regex ipiVersion = new Regex(@"\.[0-9]+", RegexOptions.Compiled);
            Regex ipiRemove = new Regex(@">IPI:", RegexOptions.Compiled);
            Regex NextProtIDRegex = new Regex(@">nxp:([A-Z|0-9|\-|_]+) ", RegexOptions.Compiled);
            Regex correctIPI = new Regex(@"^\|>", RegexOptions.Compiled);
            Regex spaceSplitter = new Regex(@" ", RegexOptions.Compiled);

            Regex SequestFriendlyGI = new Regex(@">gi\|([0-9]*)", RegexOptions.Compiled);
            Regex SequestFriendlyGIDescription = new Regex(@">[^ ]* (.*)", RegexOptions.Compiled);
            Regex SequestFriendlyIPI = new Regex(@"IPI:(IPI[0-9|\.]+)\|", RegexOptions.Compiled);
            Regex SequestFriendlyIPIDescription = new Regex(@"Tax_Id=[0-9]+ Gene_Symbol=(.*)", RegexOptions.Compiled);
            Regex SequestFriendUniProt = new Regex(@"[a-z]+\|([A-Z|0-9|\-]+)\|", RegexOptions.Compiled);
            
            Regex SequestFriendlyUniProtDescription = new Regex(@" (.*)");

            Regex TitleSpaceDescriptionID = new Regex(@">(\S+)", RegexOptions.Compiled);
            Regex TitleSpaceDescriptionDescription = new Regex(@" (.*)", RegexOptions.Compiled);


            //Declare the PathCandidates we will use

            //Parse and load to Ram
            FastaItem item = new FastaItem();
            int SequenceCounter = 0;
            int lineCounter = 0;
            int problematicCounter = 0;
            while ((read = sr.ReadLine()) != null)
            {
                lineCounter++;
                if (read.StartsWith(">"))
                {
                    if (SequenceCounter > 0)
                    {
                        if (useRegexForTitleOrDescription)
                        {
                            if (theRegex.IsMatch(item.SequenceIdentifier) || theRegex.IsMatch(item.Description))
                            {
                                myItems.Add(item);
                            }
                        }
                        else
                        {
                            myItems.Add(item);
                        }
                    }
                    else
                    {
                        SequenceCounter++;
                    }


                    //---Parse search engine friendly
                    item = new FastaItem();

                    Match id;
                    Match description;

                    if (dbType.Equals(DBTypes.NCBInr))
                    {
                        id = SequestFriendlyGI.Match(read);
                        description = SequestFriendlyGIDescription.Match(read);
                        item.Description = description.Groups[1].Value;
                    }
                    else if (dbType.Equals(DBTypes.IPI))
                    {
                        id = SequestFriendlyIPI.Match(read);
                        description = SequestFriendlyIPIDescription.Match(read);
                        item.Description = description.Groups[1].Value;
                    }
                    else if (dbType.Equals(DBTypes.IDSpaceDescription))
                    {
                        id = TitleSpaceDescriptionID.Match(read);
                        description = TitleSpaceDescriptionDescription.Match(read);
                        item.Description = description.Groups[1].Value;
                    }
                    else if (dbType.Equals(DBTypes.UniProt))
                    {
                        id = SequestFriendUniProt.Match(read);
                        description = SequestFriendlyUniProtDescription.Match(read);
                        item.Description = description.Groups[1].Value;

                    } else if (dbType.Equals(DBTypes.NeXtProt))
                    {
                        id = NextProtIDRegex.Match(read);
                        List<string> cols = Regex.Split(read, Regex.Escape(@"\")).ToList();
                        string desc = cols.Find(a => a.StartsWith("Pname="));
                        item.Description = desc.Remove(0, 6);


                    }
                    else if (dbType.Equals(DBTypes.Contaminant))
                    {
                        //we are working with dbtypes = all
                        id = Regex.Match(read, @">(.*)");
                        //we do not want a match in the description
                        description = Regex.Match(read, @"cvbcvbfgz");
                        item.Description = description.Groups[1].Value;
                    }
                    else
                    {
                        string[] cols = Regex.Split(read, " ");
                        id = Regex.Match(cols[0], @">(.*)");
                        try
                        {
                            string desc = read.Substring(id.Length + 1);
                            description = Regex.Match(desc, @"(.*)");
                            item.Description = description.Groups[1].Value;

                        }
                        catch
                        {                
                            item.Description = "No Description";
                        }
                    }

                    if (id.Groups[1].Value.Length == 0)
                    {
                        item.SequenceIdentifier = "ProblematicID" + problematicCounter++;
                        Console.WriteLine("Problem parsing identifier: " + lineCounter + " problem:" + problematicCounter);
                    } else {

                        item.SequenceIdentifier = id.Groups[1].Value;
                    }
                    


                    //------------
                }
                else
                {
                    item.Sequence += read;
                }
            }
            Console.WriteLine(lineCounter + "lines parsed in the fasta DB.");

            //And now for the last item
            if (useRegexForTitleOrDescription)
            {
                if (theRegex.IsMatch(item.SequenceIdentifier) || theRegex.IsMatch(item.Description))
                {
                    myItems.Add(item);
                }
            }
            else
            {
                myItems.Add(item);
            }

            if (removeReverse)
            {
                myItems.RemoveAll(a => a.SequenceIdentifier.Contains("Reverse_"));
            }

            sr.Close();

        }

        public void IncludeByteSequences()
        {
            foreach (FastaItem fi in MyItems)
            {
                fi.SequenceInBytes = System.Text.Encoding.ASCII.GetBytes(fi.Sequence);
            }
        }

        public void GenerateAAFrequencyTable()
        {
            Dictionary<char, int> frequencyTable = new Dictionary<char, int>();
            frequencyTable.Add('A', 0); //1
            frequencyTable.Add('R', 0); //2
            frequencyTable.Add('N', 0); //3
            frequencyTable.Add('D', 0); //4
            frequencyTable.Add('C', 0); //5
            frequencyTable.Add('E', 0); //6
            frequencyTable.Add('Q', 0); //7
            frequencyTable.Add('G', 0); //8
            frequencyTable.Add('H', 0); //9
            frequencyTable.Add('I', 0); //10
            frequencyTable.Add('L', 0); //11
            frequencyTable.Add('K', 0); //12
            frequencyTable.Add('M', 0); //13
            frequencyTable.Add('F', 0); //14
            frequencyTable.Add('P', 0); //15
            frequencyTable.Add('S', 0); //16
            frequencyTable.Add('T', 0); //17
            frequencyTable.Add('W', 0); //18
            frequencyTable.Add('Y', 0); //19
            frequencyTable.Add('V', 0); //20

            foreach (FastaItem fi in MyItems)
            {
                for (int i = 0; i < fi.Sequence.Length; i++)
                {
                    if (frequencyTable.ContainsKey(fi.Sequence[i]))
                    {
                        frequencyTable[fi.Sequence[i]]++;
                    }
                }
            }

            double total = frequencyTable.Values.Sum();

            //now generate swap table
            List<char> toExclude = new List<char>(20);

            Dictionary<char, char> swap = new Dictionary<char, char>();

            List<char> theKeys = frequencyTable.Keys.ToList();
            List<int> theValues = frequencyTable.Values.ToList();

            theValues.Sort((a, b) => b.CompareTo(a));

            for (int i = 0; i <= 19; i += 2)
            {
                char theFirst = 'Z';
                char theSecond = 'Z';

                foreach (KeyValuePair<char, int> kvp in frequencyTable)
                {
                    if (kvp.Value == theValues[i])
                    {
                        theFirst = kvp.Key;
                    }

                    if (kvp.Value == theValues[i + 1])
                    {
                        theSecond = kvp.Key;
                    }
                }

                swap.Add(theFirst, theSecond);
                swap.Add(theSecond, theFirst);
            }

            Console.WriteLine("Done!");


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputFile"></param>
        /// <param name="reverse">To Add Reversed sequences this must be set to true</param>
        public void GenerateSearchDB(string outputFile, bool reverse, bool scrambled0, bool scrambled1, bool pairReversedMiddleReversed)
        {
            PatternTools.Enzyme thisEnzime = Enzyme.Trypsin;

            int problematicIndex =  myItems.FindIndex(a => a.SequenceIdentifier.Equals(""));
            if (problematicIndex > -1)
            {
                throw (new Exception("Problems parsing DB (blank index), check DB format or DB parser"));
            }

            myItems = myItems.Distinct().ToList();

            StreamWriter sw = new StreamWriter(outputFile);

            myItems.RemoveAll(a => a.SequenceIdentifier.Equals(""));

            PatternTools.EnzymeEvaluator enzE = new EnzymeEvaluator();

            int reverseMiddleReverseGap = 0;
            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i].SequenceIdentifier.Equals(""))
                {
                    Console.Write("");
                }
                sw.Write(">" + myItems[i].SequenceIdentifier);
                if (myItems[i].Description.Length > 0) {
                    sw.WriteLine (" " + myItems[i].Description);
                } else {
                    sw.WriteLine("");
                }
                sw.WriteLine(myItems[i].Sequence);


                if (reverse && !pairReversedMiddleReversed)
                {
                    sw.WriteLine(">" + "Reverse_" + myItems[i].SequenceIdentifier);
                    sw.WriteLine(ReverseString(myItems[i].Sequence));
                }

                if (scrambled0 && !pairReversedMiddleReversed)
                {
                    sw.WriteLine(">" + "Scrambled0_" + myItems[i].SequenceIdentifier);
                    sw.WriteLine(ScrambleString(myItems[i].Sequence));
                }
                if (scrambled1 && !pairReversedMiddleReversed)
                {
                    sw.WriteLine(">" + "Scrambled1_" + myItems[i].SequenceIdentifier);
                    sw.WriteLine(ScrambleString(myItems[i].Sequence));
                }


                if (pairReversedMiddleReversed)
                {
                    List<string> peptides = enzE.DigestSequence(myItems[i].Sequence, thisEnzime, 5);

                    sw.WriteLine(">" + "PairReversed_" + myItems[i].SequenceIdentifier);
                    string pairReversedSequence = PairReverse(peptides);
                    sw.WriteLine(pairReversedSequence);
                    reverseMiddleReverseGap += myItems[i].Sequence.Length - pairReversedSequence.Length;

                    sw.WriteLine(">" + "MiddleReversed_" + myItems[i].SequenceIdentifier);
                    sw.WriteLine(MiddleReversed(peptides));


                    Console.WriteLine(".");
                }
            }

            //As it is not possible to generate sequences of same size, we might need to add a compensation sequence just to balance things off
            if (pairReversedMiddleReversed && reverseMiddleReverseGap > 6)
            {
                Console.WriteLine("Adding compensation sequence");
                sw.WriteLine(">PairReversed_CompensationSequence");
                sw.WriteLine(GenerateRandomSequence(reverseMiddleReverseGap));
                sw.WriteLine(">MiddleReversed_CompensationSequence");
                sw.WriteLine(GenerateRandomSequence(reverseMiddleReverseGap));
            }

            sw.Close();
        }

        private string GenerateRandomSequence(int reverseMiddleReverseGap)
        {
            List<string> AA = new List<string>() {
                "G", "A", "S", "P", "V", "T", "C", "I", "L",
                "X", "N", "O", "B", "D", "Q", "K", "Z", "E",
                "M", "H", "F", "R", "Y", "W"};
            
            StringBuilder sequence = new StringBuilder();

            for (int i = 0; i < AA.Count; i++)
            {
                int r = PatternTools.pTools.getRandomNumber(AA.Count);
                sequence.Append(AA[r]);
            }

            return sequence.ToString();

        }

        private string MiddleReversed(List<string> peptides)
        {
            StringBuilder scrambledSequence = new StringBuilder();

            foreach (string peptide in peptides) {
                scrambledSequence.Append(MiddleReversePeptide(peptide));
            }
            
            return scrambledSequence.ToString();
        }

        private string PairReverse(List<string> peptides)
        {
           StringBuilder scrambledSequence = new StringBuilder();

            foreach (string peptide in peptides) {
                scrambledSequence.Append(PairReversePeptide(peptide));
            }
            
            return scrambledSequence.ToString();
        }

        /// <summary>
        /// We get the reverse from the middle, reverse from the end and concatenate them.  The termini are swapped
        /// </summary>
        /// <param name="theSequence"></param>
        /// <returns></returns>
        private static string MiddleReversePeptide(string theSequence)
        {
            StringBuilder sb2 = new StringBuilder();

            sb2.Append(theSequence.Substring(theSequence.Length - 1, 1));

            //Find the middle point
            int mp = (int)Math.Floor((double)(theSequence.Length / 2));
            string part1 = new string(theSequence.Substring(1, mp).Reverse().ToArray());
            string part2 = new string(theSequence.Substring(mp + 1, theSequence.Length - mp - 2).Reverse().ToArray());

            sb2.Append(part1);
            sb2.Append(part2);

            sb2.Append(theSequence.Substring(0, 1));


            return sb2.ToString();
        }


        /// <summary>
        /// We reverse the sequence, swap two by two excluding the termini, and then swap the termini
        /// </summary>
        /// <param name="theSequence"></param>
        /// <returns></returns>
        private static string PairReversePeptide(string theSequence)
        {
            //We reverse the sequence
            StringBuilder sb2 = new StringBuilder();
            sb2.Append(theSequence.Substring(theSequence.Length - 1, 1));
            for (int i = theSequence.Length - 3; i > 0; i = i - 2)
            {
                sb2.Append(theSequence.Substring(i, 2));
                if (i == 2)
                {
                    string s = theSequence.Substring(1, 1);
                    sb2.Append(s);
                }
            }

            sb2.Append(theSequence.Substring(0, 1));

            return sb2.ToString();
        }


        /// <summary>
        /// Receives string and returns the string with its letters reversed.
        /// </summary>
        private static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);

        }

        private static string ScrambleString(string s)
        {
            StringBuilder scrambledSequence = new StringBuilder();
            List<char> theSequence = s.ToCharArray().ToList();

            while (theSequence.Count > 0)
            {
                int RandomNumber = PatternTools.pTools.getRandomNumber(theSequence.Count - 1);
                scrambledSequence.Append(theSequence[RandomNumber]);
                theSequence.RemoveAt(RandomNumber);
            }

            return scrambledSequence.ToString();
        }

        public void IncludeScrambled()
        {
            List<FastaItem> myScrambled = new List<FastaItem>(myItems.Count);
            
            for (int i = 0; i < myItems.Count; i++)
            {
                FastaItem f = new FastaItem();
                f.SequenceIdentifier = "Scrambled_" + myItems[i].SequenceIdentifier;
                f.Sequence = ScrambleString(myItems[i].Sequence);
                f.Description = "";
                myScrambled.Add(f);
            }

            myItems.AddRange(myScrambled);
        }

        public void SaveDB(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            foreach (FastaItem fi in MyItems)
            {
                sw.WriteLine(">" + fi.SequenceIdentifier + " " + fi.Description);
                sw.WriteLine(fi.Sequence);
            }
            sw.Close();
        }
    }

}
