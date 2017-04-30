using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MAligner.MAligner
{
    public class Aligner
    {
        /// <summary>
        /// The substitution matrix
        /// </summary>
        int[,] substitutionMatrix 
        {
            get;
            set;
        }

        /// <summary>
        /// A dictionary where the key points to the position of the character in the substitution matrix
        /// </summary>
        Dictionary<char, int> substitutionMatrixPos 
        {
            get;
            set;
        }

        public Aligner(KeyValuePair<int[,], Dictionary<char, int>> theMatrix)
        {
            substitutionMatrix = theMatrix.Key;
            substitutionMatrixPos = theMatrix.Value;
        }

        public Aligner()
        {
            
        }

        public string GetClosestPeptideInASequence(char[] peptide, char[] protein)
        {
            //Here we get an array having the size of sequence 2. Each value in the array in the alignment score.
            int [] res = Align(peptide, protein);

            //recovoer the position of best alignment;
            int maxScore = res.Max();
            int MaxScorePos = res.ToList().IndexOf(maxScore);

            ///-----------------
            int correction = 0;
            if (protein.Length - 1 < MaxScorePos + peptide.Length)
            {
                correction = MaxScorePos + peptide.Length - protein.Length;
            }
            string closestPeptideInProteinDB = new string(protein).Substring(MaxScorePos, peptide.Length - correction);
            
            ///

            return closestPeptideInProteinDB;
        }

        /// <summary>
        /// Returns a list of max scores, followed by the peptide in DB.
        /// </summary>
        /// <param name="peptide"></param>
        /// <param name="theDB"></param>
        /// <returns></returns>
        public KeyValuePair<List<int>, List<string>> GetClosestPeptideInDB(char[] peptide, List<FastaItem> theDB) 
        {
            List<int> Scores = new List<int>();
            List<string> Closest = new List<string>();

            for (int i = 0; i < theDB.Count; i++ )
            {
                FastaItem fi = theDB[i];

                List<int> res = Align(peptide, fi.Sequence.ToCharArray()).ToList();

                if (res.Count < peptide.Length)
                {
                    Scores.Add(-1);
                    Closest.Add("-1");
                    continue;
                }


                int maxScore = res.Max();
                int MaxScorePos = res.ToList().IndexOf(maxScore);

                int correction = 0;
                if (fi.Sequence.Length - 1 < MaxScorePos + peptide.Length)
                {
                    correction = MaxScorePos + peptide.Length - fi.Sequence.Length;
                }
                string closestPeptideInProteinDB = new string(fi.Sequence.ToArray()).Substring(MaxScorePos, peptide.Length - correction);

                Scores.Add(maxScore);
                Closest.Add(closestPeptideInProteinDB);
            }

            return new KeyValuePair<List<int>, List<string>>(Scores,Closest);
            
        }
       
        /// <summary>
        /// MC Alignment
        /// </summary>
        /// <param name="peptide">The first sequence, its length must be less than or equal to seq2</param>
        /// <param name="protein">The second sequence</param>
        /// <param name="gap">If a value greater than 0 is provided, up to 1 gap will be considered</param>
        /// <returns>Returns an array of score alignments for each position of seq2</returns>
        public int[] Align(char [] peptide, char[] protein)
        {

            //if (protein.Contains('X') || protein.Contains('U'))
            //{            
            //    //Console.WriteLine("Skipping a sequence that contains X or U"); 
            //    return new int [0];
            //}

            if (protein.Length < peptide.Length)
            {
                Console.WriteLine("Not optimized for peptide larger than proteins");
                return new int[] { 0 };
            }

            int[] alignmentScores = new int[protein.Length];

            for (int x = 0; x < protein.Length; x++)
            {
                
                int sum = 0;
                
                for (int y = 0; y < peptide.Length; y++)
                {

                    if (x + y >= protein.Length)
                    {
                        break;
                    }

                    char p1 = peptide[y];
                    char p2 = protein[x + y];
                    int pos1 = substitutionMatrixPos[p1];
                    int pos2 = substitutionMatrixPos[p2]; 
                    int add = substitutionMatrix[pos1, pos2];

                    sum += add;

                }

                alignmentScores[x] = sum;
            }

            return alignmentScores;

        }
        
        public List<int[]> SWAPAlign2(char[] seq1, char[] seq2)
        {
            List<char[]> seq1Swapped = Swapper.Swap(seq1);
            List<int[]> returnResult = new List<int[]>(seq1Swapped.Count);


            for (int y = 0; y < seq1Swapped.Count; y++)
            {
                Debug.Write(new string (seq1Swapped[y]) + "\n");
                int[] result = Align(seq1Swapped[y], seq2);
                returnResult.Add(result);
            }

            return returnResult; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seq1">The peptide to be queried</param>
        /// <param name="theDB">The sequence database</param>
        /// <returns></returns>
        public List<List<int[]>> SwapAlignBulk(char[] seq1, List<FastaItem> theDB)
        {
            List<char[]> seq1Swapped = Swapper.Swap(seq1);

            Console.WriteLine("------------------------------- Swap solutions ---------------------------------");
            for (int i = 0; i < seq1Swapped.Count; i++)
            {
                Console.WriteLine("{0}:{1}", i, new string(seq1Swapped[i]));
            }
            
            Console.WriteLine("\n-------------------------------------------------------------------------------\n");

            List<List<int[]>> returnResult = new List<List<int[]>>(seq1Swapped.Count);

            int counter = 0;
            foreach (FastaItem fi in theDB)
            {
                counter++;
                if (counter % 100 == 0)
                {
                    Console.WriteLine("DB item " + counter);
                }

                List<int[]> alignmentResult = SWAPAlign2(seq1, fi.Sequence.ToCharArray());

                if (alignmentResult != null)
                {
                    returnResult.Add(alignmentResult);
                }
            }
            return returnResult;
        }

        public int IDScore(byte[] peptide, byte[] protein, int speed = 1)
        {
            
            if (peptide.Length > protein.Length)
            {
                //Console.WriteLine("Shoudn´t receive a protein that is smaller than a peptide");
                return 0;
            }

            int[] identities = new int[protein.Length];

            
            for (int i = 0; i < protein.Length; i++)
            {
                int thisIdentity = 0;

                for (int j = 0; j < peptide.Length; j+= speed)
                {
                    if (i + j < protein.Length)
                    {
                        if (protein[i + j] == peptide[j])
                        {
                            thisIdentity++;
                        }
                    }
                }

                identities[i] = thisIdentity;
            }

            int maxIdentity = identities.ToList().Max();

            return maxIdentity;

        }

        public int[] IDScoreArray(byte[] peptide, byte[] protein, int speed = 1)
        {

            if (peptide.Length > protein.Length)
            {
                //Console.WriteLine("Shoudn´t receive a protein that is smaller than a peptide");
                return null;
            }

            int[] identities = new int[protein.Length];


            for (int i = 0; i < protein.Length; i++)
            {
                int thisIdentity = 0;

                for (int j = 0; j < peptide.Length; j += speed)
                {
                    if (i + j < protein.Length)
                    {
                        if (protein[i + j] == peptide[j])
                        {
                            thisIdentity++;
                        }
                    }
                }

                identities[i] = thisIdentity;
            }

            return identities;

        }

        public double SimilarityScore(KeyValuePair<int, List<List<int>>> PBSA, List<FastaItem> DB, List<List<int[]>> swapBulk, char [] seq1)
        {
            List<char[]> seq1Swapped = Swapper.Swap(seq1);

            double score = 0;
            int cont = 0;

            for (int i = 0; i < PBSA.Value[0].Count; i++)
            {
                for (int j = 0; j < swapBulk[0].Count; j++)
                {
                    for (int k = 0; k < seq1Swapped[0].Length; k++)
                    {
                        if (DB[PBSA.Value[0][i]].Sequence[k].ToString() != seq1Swapped[j][k].ToString())
                        {
                            score = swapBulk[i][j][k] + score;
                            cont++;
                        }
                    }
                }

            }

            score = score / cont;
            return score;
        }

        public int IdentitylScore(KeyValuePair<int, List<List<int>>> PBSA, List<FastaItem> DB, List<List<int[]>> swapBulk, char[] seq1)
        {
            List<char[]> seq1Swapped = Swapper.Swap(seq1);

            int score = 0;

            for (int i = 0; i < PBSA.Value[0].Count; i++)
            {
                for (int j = 0; j < swapBulk[0].Count; j++)
                {
                    for (int k = 0; k < seq1Swapped[0].Length; k++)
                    {
                        if (DB[PBSA.Value[0][i]].Sequence[k].ToString() == seq1Swapped[j][k].ToString())
                        {
                            score = swapBulk[i][j][k] + score;
                                                  
                        }
                    }
                }

            }

         return score;

        }

        /// <summary>
        /// Best Alignments 
        /// </summary>
        /// <param name="swapPossibilities"> Value swap in DB </param>
        /// <param name="DB"> Data Base if sequence </param>
        /// <returns>Key Value Pair contains maxvalue (i.e. best alignment), DBIndex, SwapIndex, SeqPosIndex </returns>
        public KeyValuePair<int, List<List<int>>> ProvideBestSwapBulkAlignments(List<List<int[]>> swapPossibilities, List<FastaItem> DB)
        {
            
            int maxValue = int.MinValue;


            List<int> DBIndex = new List<int>();
            List<int> SwapIndex = new List<int>();
            List<int> SeqPosIndex = new List<int>();


            int counter = 0;
            for (int i = 0; i < swapPossibilities.Count; i++) //
            {
                counter++;
                Console.WriteLine("Verifying swap " + counter + " of " + swapPossibilities.Count);
                List<int[]> swapOptions = swapPossibilities[i];

                for (int j = 0; j < swapOptions.Count; j++) //SwappedOptions
                {
                    for (int k = 0; k < swapOptions[j].Length; k++)
                    {
                        if (swapOptions[j][k] > maxValue)
                        {
                            maxValue = swapOptions[j][k];

                            DBIndex = new List<int>() { i };
                            SwapIndex = new List<int>() { j };
                            SeqPosIndex = new List<int>() { k };

                        }
                        else if (swapOptions[j][k] == maxValue)
                        {
                            DBIndex.Add(i);
                            SwapIndex.Add(j);
                            SeqPosIndex.Add(k);
                        }
                    }
                }

            }
            Console.WriteLine();
            return new KeyValuePair<int, List<List<int>>>(maxValue, new List<List<int>>() { DBIndex, SwapIndex, SeqPosIndex });
        }

        public void PrintSwap(KeyValuePair<int, List<List<int>>> PBSA, List<FastaItem> DB, char[] seq1)
        {
            Console.WriteLine("-------------------------------------------------------------------------------\n");
            Console.WriteLine("Query: " + new string(seq1));
            Console.WriteLine("Alignment score = " + PBSA.Key + "\n");

            Console.WriteLine("Protein Name: " + DB[PBSA.Value[0][0]].SequenceIdentifier + "\t" + DB[PBSA.Value[0][0]].Description);
            Console.WriteLine("\tSwap result: " + PBSA.Value[1][0]);
            Console.WriteLine("\tPosition: " + PBSA.Value[2][0]);
            string peptideInDB = DB[PBSA.Value[0][0]].Sequence.Substring(PBSA.Value[2][0], seq1.Length);
            Console.WriteLine("\tPeptide in DB: " + peptideInDB + "\n");
             
            for (int i = 1; i < PBSA.Value[0].Count; i++)
            {
               
                if (DB[PBSA.Value[0][i]].SequenceIdentifier != DB[PBSA.Value[0][i - 1]].SequenceIdentifier)
                {

                    Console.WriteLine("Protein Name: " + DB[PBSA.Value[0][i]].SequenceIdentifier + "\t" + DB[PBSA.Value[0][i]].Description);
                    Console.WriteLine("\tSwap result: " + PBSA.Value[1][i]);
                    Console.WriteLine("\tPosition: " + PBSA.Value[2][i]);
                    string peptideInD = DB[PBSA.Value[0][i]].Sequence.Substring(PBSA.Value[2][i], seq1.Length);
                    Console.WriteLine("\tPeptide in DB: " + peptideInD); 
                    
                }
                else 
                {
                   
                    Console.WriteLine("\tSwap result: " + PBSA.Value[1][i]);
                    Console.WriteLine("\tPosition: " + PBSA.Value[2][i]);
                    string peptideInD = DB[PBSA.Value[0][i]].Sequence.Substring(PBSA.Value[2][i], seq1.Length);
                    Console.WriteLine("\tPeptide in DB: " + peptideInD);    
                }

                Console.WriteLine("");
           
            }

            Console.WriteLine("-------------------------------------------------------------------------------\n");
                 
        }

    }
}
