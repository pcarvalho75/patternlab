using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using PatternTools.PTMMods;

namespace PatternTools
{
    public class AminoacidMasses 
    {
        Regex removeStartingHint = new Regex("^([A-Z]|-).", RegexOptions.Compiled);
        Regex removeEndingHint = new Regex(".([A-Z]|-)$", RegexOptions.Compiled);

        Dictionary<string, double> isotopicDictionary = new Dictionary<string, double>(20);
        Dictionary<string, double> averageMassDictionary = new Dictionary<string, double>(20);

        /// <summary>
        /// Dictionary:key is aminoacid letter, value is isotopic mass
        /// </summary>
        public Dictionary<string, double> IsotopicDic
        {
            get { return isotopicDictionary; }
            set { isotopicDictionary = value; }
        }

        public Dictionary<string, double> AverageDic
        {
            get { return averageMassDictionary; }
        }

        private void buildMonoisotopicDictionary()
        {
            isotopicDictionary.Clear();
            isotopicDictionary.Add("G", 57.0214637);  //Glycine
            isotopicDictionary.Add("A", 71.0371138);  //Alanine
            isotopicDictionary.Add("S", 87.0320284);  //Serine
            isotopicDictionary.Add("P", 97.0527638);  //Proline
            isotopicDictionary.Add("V", 99.0684139);  //Valine
            isotopicDictionary.Add("T", 101.047678); //Threonine
            isotopicDictionary.Add("C", 103.009185); //Cystein
            isotopicDictionary.Add("I", 113.084064); //Isoleucine
            isotopicDictionary.Add("L", 113.084064); //Leucine
            isotopicDictionary.Add("N", 114.042927); //Aspargine
            isotopicDictionary.Add("D", 115.026943); //Aspartic Acid
            isotopicDictionary.Add("Q", 128.058578); //Glutamine
            isotopicDictionary.Add("K", 128.094963); //Lysine
            isotopicDictionary.Add("E", 129.042593); //Glutamic Acid
            isotopicDictionary.Add("M", 131.040485); //Methionine
            isotopicDictionary.Add("H", 137.058912); //Histidine
            isotopicDictionary.Add("F", 147.068414); //Phenilanyne
            isotopicDictionary.Add("U", 150.95364); //Selenocysteine
            isotopicDictionary.Add("R", 156.101111); //Arginine
            isotopicDictionary.Add("X", 113.08406); //Undetermined AA, L or I.
            isotopicDictionary.Add("Y", 163.063329); //Tyrosine
            isotopicDictionary.Add("W", 186.079313); //Tryptophan


            //Special Features - molecular monoisotopic mass
            isotopicDictionary.Add("Hydrogen", 1.00782503214);
            isotopicDictionary.Add("Oxygen", 15.9949146221);
            isotopicDictionary.Add("Carbon", 12);
            isotopicDictionary.Add("Nitrogen", 14.00307400529); //
            isotopicDictionary.Add("NH3", 17.0265491);
            isotopicDictionary.Add("CO", 27.9949146221);

            isotopicDictionary.Add("H2O", 18.01056468638);

            isotopicDictionary.Add("B", 114.042927); //Aspargine os aspartic acid, not able to be determined by sequencing method, here we pick aspargine 
            isotopicDictionary.Add("Z", 128.058578); //glutamic acid or glutamine, we chose glutamine

        }


        private void buildAverageMassDictionary()
        {
            averageMassDictionary.Clear();
            averageMassDictionary.Add("G", 57.0519);  //Glycine
            averageMassDictionary.Add("A", 71.0788);  //Alanine
            averageMassDictionary.Add("S", 87.0782);  //Serine
            averageMassDictionary.Add("P", 97.1167);  //Proline
            averageMassDictionary.Add("V", 99.1326);  //Valine
            averageMassDictionary.Add("T", 101.1051); //Threonine
            averageMassDictionary.Add("C", 103.1388); //Cystein
            averageMassDictionary.Add("I", 113.1594); //Isoleucine
            averageMassDictionary.Add("L", 113.1594); //Leucine
            averageMassDictionary.Add("X", 113.1594); //Undetermined AA, LorI.
            averageMassDictionary.Add("N", 114.1038); //Aspargine
            averageMassDictionary.Add("O", 114.1472); //Ornithine
            averageMassDictionary.Add("B", 114.5962); ////Aspargine or aspartic acid, not able to be determined by sequencing method, here we pick aspargine  : avg_NandD
            averageMassDictionary.Add("D", 115.0886); //Aspartic Acid
            averageMassDictionary.Add("Q", 128.1307); //Glutamine
            averageMassDictionary.Add("K", 128.1741); //Lysine
            averageMassDictionary.Add("Z", 128.1741); //avg_QandE
            averageMassDictionary.Add("E", 129.1155); //Glutamic Acid
            averageMassDictionary.Add("M", 131.1926); //Methionine
            averageMassDictionary.Add("H", 137.1411); //Histidine
            averageMassDictionary.Add("F", 147.1766); //Phenilanyne
            averageMassDictionary.Add("R", 156.1875); //Arginine
            averageMassDictionary.Add("Y", 163.1760); //Tyrosine
            averageMassDictionary.Add("W", 186.2132); //Tryptophan


            //Special Features - molecular monoisotopic mass
            averageMassDictionary.Add("Hydrogen", 1.008);
            averageMassDictionary.Add("Oxygen", 16.00);
            averageMassDictionary.Add("Carbon", 12.0107);
            averageMassDictionary.Add("Nitrogen", 14.0067); //
            averageMassDictionary.Add("H2O", 18.02);
            averageMassDictionary.Add("NH3", 17.0307);
            averageMassDictionary.Add("CO", 28.0107);



        }


        /// <summary>
        /// Returns the [M+H+] mass
        /// </summary>
        /// <param name="peptide"></param>
        /// <returns></returns>
        public double CalculatePeptideMass (string iPeptide, bool addAWaterAndAHydrogenMass, bool useIsotopicMass, bool removePeptideHints) {

            //We need to check id the peptide sequence has the aa hints, that is, aminoacids followed by a dot
            //If so, we need to remove these hints from the sequence
            try
            {
                string peptide;
                if (removePeptideHints)
                {
                    peptide = removeStartingHint.Replace(iPeptide, "");
                    peptide = removeEndingHint.Replace(peptide, "");
                }
                else
                {
                    peptide = iPeptide;
                }

                double mass = 0;
                for (int i = 0; i < peptide.Length; i++)
                {
                    string theAA = peptide.Substring(i, 1);

                    if (!isotopicDictionary.ContainsKey(theAA))
                    {
                        Console.WriteLine("AminoAcid : " + theAA + " not found in the dictionary");
                    }
                    else
                    {
                        if (useIsotopicMass)
                        {
                            mass += isotopicDictionary[theAA];
                        }
                        else
                        {
                            mass += averageMassDictionary[theAA];
                        }
                    }
                }

                if (addAWaterAndAHydrogenMass)
                {
                    mass += isotopicDictionary["H2O"];
                    mass += isotopicDictionary["Hydrogen"];
                }

                return (mass);
            }
            catch
            {
                Console.WriteLine("Amino acid not found in dictionary; unable to calculate mass");
                return -1;
            }
           
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AminoacidMasses()
        {
            buildMonoisotopicDictionary();
            buildAverageMassDictionary();
        }

        public List<GapItem> GenerateDeltaMassTable2(double ppmTolerance, List<ModificationItem> myMods)
        {
            List<GapItem> theGaps = new List<GapItem>();
            
            Dictionary<string, double> thisIsotopicDictionary = PatternTools.ObjectCopier.Clone(isotopicDictionary);
            
            thisIsotopicDictionary.Remove("H2O");
            thisIsotopicDictionary.Remove("Hydrogen");
            thisIsotopicDictionary.Remove("Nitrogen");
            thisIsotopicDictionary.Remove("Oxygen");
            thisIsotopicDictionary.Remove("Carbon");
            thisIsotopicDictionary.Remove("NH3");
            thisIsotopicDictionary.Remove("CO");
            thisIsotopicDictionary.Remove("Z");
            thisIsotopicDictionary.Remove("B");
            thisIsotopicDictionary.Remove("X");

            //Lets fire up the gap list
            foreach (KeyValuePair<string, double> kvp in thisIsotopicDictionary)
            {
                theGaps.Add(new GapItem(kvp.Value, 1, kvp.Key, kvp.Key));
            }

            //Now for the static mods
            foreach (GapItem g in theGaps)
            {
                int index = myMods.FindIndex(a => a.AminoAcid.Equals(g.AARegex) && !a.IsVariable && a.IsActive);

                if (index > -1)
                {
                    g.GapSize += myMods[index].DeltaMass;
                }
            }

            //Now lets add variable mods
            foreach (KeyValuePair<string, double> kvp in thisIsotopicDictionary)
            {
                List<ModificationItem> variableMods = myMods.FindAll(a => a.AminoAcid.Equals(kvp.Key) && a.IsVariable && a.IsActive);
                foreach (ModificationItem m in variableMods)
                {
                    theGaps.Add(new GapItem(kvp.Value + m.DeltaMass, 1, kvp.Key, kvp.Key+"(" + m.DeltaMass + ")"));
                }
            }

            //Now lets merge gap items that are within the ppm bounds
            ClusterGapItems(ppmTolerance, theGaps);

            return theGaps;


        }

        public static void ClusterGapItems(double ppmTolerance, List<GapItem> theGaps)
        {
            bool needsToMerge = true;

            while (needsToMerge)
            {
                needsToMerge = false;
                for (int i = 0; i < theGaps.Count; i++)
                {
                    for (int j = i + 1; j < theGaps.Count; j++)
                    {
                        double ppm = PatternTools.pTools.PPM(theGaps[i].GapSize, theGaps[j].GapSize);
                        if (Math.Abs(ppm) < ppmTolerance * 0.8)
                        {
                            MergeGaps(theGaps[i], theGaps[j], theGaps);
                            needsToMerge = true;
                        }
                    }

                }
            }
        }

        private static void MergeGaps(GapItem g1, GapItem g2, List<GapItem> gapTable)
        {
            string theRegex = "";
            string description = "(" + g1.Description + "|" + g2.Description + ")";

            if (g1.AARegex.Length == 1 && g2.AARegex.Length == 1)
            {
                theRegex = description;
            }
            else
            {
                theRegex = "(" + "(" + g1.AARegex + "|" + g2.AARegex + ")|(" + g2.AARegex + "|" + g1.AARegex + "))";
            }

            GapItem newGap = new GapItem((g1.GapSize + g2.GapSize) / 2, 1, theRegex, description);
            gapTable.Remove(g1);
            gapTable.Remove(g2);
            gapTable.Add(newGap);
        }


    }

    //--------------------------------------------


    [Serializable]
    public class GapItem
    {
        public double GapSize { get; set; }
        public int NoAA { get; set; }
        public string AARegex { get; set; }
        public string Description { get; set; }
        public List<string> theAAs
        {
            get
            {
                Regex theReg = new Regex(@"([A-Z])");
                MatchCollection theMatches = theReg.Matches(AARegex);
                List<string> aas = new List<string>();

                foreach (Match m in theMatches)
                {
                    aas.Add(m.ToString());
                }

                return aas;
            }
        }

        public double DeltaMass (double mass)
        {
            return (Math.Abs(GapSize - mass));
        }

        public GapItem (double gapSize, int noAA, string aARegex, string description = "") {
            this.GapSize = gapSize;
            this.NoAA = noAA;
            this.AARegex = aARegex;
            this.Description = description;
        }
    }

//    Monosaccharide Type
// Average Mass (Da)
// Monoisotopic Mass (Da)
 
//Hexose 162.1424 162.0528 
//HexNAc 203.1950 203.0794 
//Deoxyhexose 146.1430 146.0579 
//NeuAc 291.2579 291.0954  
//NeuGc 307.2573 307.0903 
//Pentose 132.1161 132.0423 
//Sulfate 80.0642 79.9568 
//Phosphate 79.9799 79.9663 
//KDN 250.2053 250.0689 
//KDO 220.1791 220.0583 
//HexA 176.1259 176.0321 
//Methyl 14.0269 14.0157 
//Acetyl 42.0373 42.0106 
//H2O (for reducing terminus) 18.0153 18.0106 
//Other Manually entered at time of data entry Manually entered at time of data entry 


}
