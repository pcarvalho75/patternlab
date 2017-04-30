using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MAligner
{
    public static class Utils
    {

        public static string PAM30MS 
        {
            get
            {
                return Properties.Resources.PAM30MS;
            }
        }

        /// <summary>
        /// Loads a substituion matrix
        /// </summary>
        /// <param name="pathToSubstitutionMatrix">Path to the text file of the substitution matrix</param>
        /// <returns>A keyvaluePair were the key is the substitution matrix and the value is a dictionary relating each aminoacid / nucleotid with its position in the matrix</returns>
        /// 


        public static KeyValuePair<int[,], Dictionary<char, int>> LoadSubstitutionMatrix(string pathToSubstitutionMatrix)
        {

            StreamReader file = new StreamReader(pathToSubstitutionMatrix);

            string mText = file.ReadToEnd();

            file.Close();

            return LoadSubstitutionMatrixFromString(mText);

            
        }

        public static KeyValuePair<int[,], Dictionary<char, int>> LoadSubstitutionMatrixFromString(string matrixInText)
        {
            matrixInText = Regex.Replace(matrixInText, "\r", "");
            
            int[,] substitutionMatrix = null;
            string[] lines = Regex.Split(matrixInText, "\n");
            int counter = 0;
            Dictionary<char, int> subsMatrixScores = new Dictionary<char, int>();

            foreach (string line in lines)
            {
                counter++;

                List<string> cols = Regex.Split(line, " ").ToList();
               

                if (counter == 1)
                {
                    List<char> index = cols.Select(a => a[0]).ToList();

                    for (int i = 0; i < index.Count; i++)
                    {
                        subsMatrixScores.Add(index[i], i);
                    }

                    substitutionMatrix = new int[index.Count + 1, index.Count + 1];
                }

                else
                {
                    cols.RemoveAt(0);
                    int[] row = cols.Select(a => int.Parse(a)).ToArray();

                    for (int i = 0; i < row.Length; i++)
                    {
                        substitutionMatrix[i, counter - 2] = row[i];
                    }

                }
            }

            return new KeyValuePair<int[,], Dictionary<char, int>>(substitutionMatrix, subsMatrixScores);
        }
    }
}
