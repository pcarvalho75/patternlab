using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Deployment.Application;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using PatternTools.FastaParser;
using Microsoft.Win32;

namespace PatternTools
{
    public class pTools
    {
        public pTools(){	}

        /// <summary>
        /// Check whether MSFileReader (Thermo Program) is installed in pc, because the ParserRAW needs a msfilereader DLL
        /// </summary>
        /// <returns></returns>
        public static bool MSFileReaderInstalled()
        {
            #region Windows 7 or later
            //Windows RegistryKey
            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            if (regKey == null)
            {
                return false;
            }
            //Get key vector for each entry
            string[] keys = regKey.GetSubKeyNames();
            if (keys != null && keys.Length > 0)
            {
                //Interates key vector to try to get DisplayName
                for (int i = 0; i < keys.Length; i++)
                {
                    //Open current key
                    RegistryKey k = regKey.OpenSubKey(keys[i]);
                    try
                    {
                        //Get DisplayName
                        String appName = k.GetValue("DisplayName").ToString();

                        if (appName != null && appName.Length > 0 && appName.Contains("Thermo MSFileReader"))
                        {
                            return true;
                        }
                    }
                    catch (Exception) { }
                }
            }
            #endregion

            #region Windows Vista
            //Windows RegistryKey
            regKey = regKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            if (regKey == null)
            {
                return false;
            }
            //Get key vector for each entry
            keys = regKey.GetSubKeyNames();
            if (keys != null && keys.Length > 0)
            {
                //Interates key vector to try to get DisplayName
                for (int i = 0; i < keys.Length; i++)
                {
                    //Open current key
                    RegistryKey k = regKey.OpenSubKey(keys[i]);
                    try
                    {
                        //Get DisplayName
                        String appName = k.GetValue("DisplayName").ToString();

                        if (appName != null && appName.Length > 0 && appName.Contains("Thermo MSFileReader"))
                        {
                            return true;
                        }
                    }
                    catch (Exception) { }
                }
            }
            #endregion

            return false;
        }

        public static Dictionary<string, object> AnonymousToDictionary(object o)
        {
            return o.GetType().GetProperties().ToDictionary(x => x.Name.ToString(), x => x.GetValue(o, null));
        }

        public static Color[] color = new Color[]{
        Color.AliceBlue,
	    Color.AntiqueWhite,
	    Color.Aqua,
        Color.Beige,
	    Color.Aquamarine,
	    Color.Coral,
        Color.Azure,
	    Color.Bisque,
	    Color.Black,
	    Color.BlanchedAlmond,
	    Color.Blue,
	    Color.BlueViolet,
	    Color.Brown,
	    Color.BurlyWood,
	    Color.CadetBlue,
	    Color.Chartreuse,
	    Color.Chocolate,
	    Color.Cornsilk,
	    Color.Crimson,
	    Color.Cyan,
	    Color.DarkBlue,
	    Color.DarkCyan,
	    Color.DarkGoldenrod,
	    Color.DarkGray,
	    Color.DarkGreen,
	    Color.DarkKhaki,
	    Color.DarkMagenta,
	    Color.DarkOliveGreen,
	    Color.DarkOrange,
	    Color.DarkOrchid,
	    Color.DarkRed,
	    Color.DarkSalmon,
	    Color.DarkSeaGreen,
	    Color.DarkSlateBlue,
	    Color.DarkSlateGray,
	    Color.DarkTurquoise,
	    Color.DarkViolet,
	    Color.DeepPink,
	    Color.DeepSkyBlue,
	    Color.DimGray,
	    Color.DodgerBlue,
	    Color.Firebrick,
	    Color.FloralWhite,
	    Color.ForestGreen,
	    Color.Fuchsia,
	    Color.Gainsboro,
	    Color.GhostWhite,
	    Color.Gold,
	    Color.Goldenrod,
	    Color.Gray,
	    Color.Green,
	    Color.GreenYellow,
	    Color.Honeydew,
	    Color.HotPink,
	    Color.IndianRed,
	    Color.Indigo,
	    Color.Ivory,
	    Color.Khaki,
	    Color.Lavender,
	    Color.LavenderBlush,
	    Color.LawnGreen,
	    Color.LemonChiffon,
	    Color.LightBlue,
	    Color.LightCoral,
	    Color.LightCyan,
	    Color.LightGoldenrodYellow,
	    Color.LightGray,
	    Color.LightGreen,
	    Color.LightPink,
	    Color.LightSalmon,
	    Color.LightSeaGreen,
	    Color.LightSkyBlue,
	    Color.LightSlateGray,
	    Color.LightSteelBlue,
	    Color.LightYellow,
	    Color.Lime,
	    Color.LimeGreen,
	    Color.Linen,
	    Color.Magenta,
	    Color.Maroon,
	    Color.MediumAquamarine,
	    Color.MediumBlue,
	    Color.MediumOrchid,
	    Color.MediumPurple,
	    Color.MediumSeaGreen,
	    Color.MediumSlateBlue,
	    Color.MediumSpringGreen,
	    Color.MediumTurquoise,
	    Color.MediumVioletRed,
	    Color.MidnightBlue,
	    Color.MintCream,
	    Color.MistyRose,
	    Color.Moccasin,
	    Color.NavajoWhite,
	    Color.Navy,
	    Color.OldLace,
	    Color.Olive,
	    Color.OliveDrab,
	    Color.Orange,
	    Color.OrangeRed,
	    Color.Orchid,
	    Color.PaleGoldenrod,
	    Color.PaleGreen,
	    Color.PaleTurquoise,
	    Color.PaleVioletRed,
	    Color.PapayaWhip,
	    Color.PeachPuff,
	    Color.Peru,
	    Color.Pink,
	    Color.Plum,
	    Color.PowderBlue,
	    Color.Purple,
	    Color.Red,
	    Color.RosyBrown,
	    Color.RoyalBlue,
	    Color.SaddleBrown,
	    Color.Salmon,
	    Color.SandyBrown,
	    Color.SeaGreen,
	    Color.SeaShell,
	    Color.Sienna,
	    Color.Silver,
	    Color.SkyBlue,
	    Color.SlateBlue,
	    Color.SlateGray,
	    Color.Snow,
	    Color.SpringGreen,
	    Color.SteelBlue,
	    Color.Tan,
	    Color.Teal,
	    Color.Thistle,
	    Color.Tomato,
	    Color.Transparent,
	    Color.Turquoise,
	    Color.Violet,
	    Color.Wheat,
	    Color.White,
	    Color.WhiteSmoke,
	    Color.Yellow,
	    Color.YellowGreen
	    };

        public static List<FastaItem> MaxParsimonyList (List<FastaItem> fasta, Dictionary<string, List<string>> peptideProteinDictionary)
        {
            List<FastaItem> maxParsimonyList = new List<FastaItem>();

            List<string> peptides = peptideProteinDictionary.Keys.Select(a => a).ToList();

            //Make sure we only have the peptides of interest
            Dictionary<string, List<string>> peptideProteinDictionaryTMP = new Dictionary<string, List<string>>();
            foreach (string peptide in peptides)
            {
                peptideProteinDictionaryTMP.Add(peptide, peptideProteinDictionary[peptide]);
            }

            //Step 1, Construct the protein peptide dictionary
            List<KeyValuePair<string, List<string>>> proteinPeptideDict = new List<KeyValuePair<string, List<string>>>();

            foreach (KeyValuePair<string, List<string>> kvp in peptideProteinDictionaryTMP)
            {
                foreach (string protID in kvp.Value)
                {
                    int index = proteinPeptideDict.FindIndex(a => a.Key.Equals(protID));

                    if (index >=0)
                    {
                        proteinPeptideDict[index].Value.Add(kvp.Key);

                    }
                    else
                    {
                        KeyValuePair<string, List<string>> item = new KeyValuePair<string, List<string>>(protID, new List<string>() { kvp.Key });
                        proteinPeptideDict.Add(item);
                    }
                }
            }

            while (peptides.Count > 0)
            {

                //Find the protein that explains the most peptides

                proteinPeptideDict.Sort((a, b) => b.Value.Count.CompareTo(a.Value.Count));

                maxParsimonyList.Add(fasta.Find(a => a.SequenceIdentifier.Equals(proteinPeptideDict[0].Key)));

                peptides = peptides.Except(proteinPeptideDict[0].Value).ToList();
                KeyValuePair<string, List<string>> current = proteinPeptideDict[0];

                proteinPeptideDict.RemoveAt(0);



                List<KeyValuePair<string, List<string>>> proteinPeptideDictTMP = new List<KeyValuePair<string, List<string>>>();
                foreach (KeyValuePair<string, List<string>> kvp in proteinPeptideDict)
                {
                    List<string> peps = kvp.Value.Except(current.Value).ToList();

                    if (peps.Count > 0)
                    {
                        proteinPeptideDictTMP.Add(new KeyValuePair<string, List<string>>(kvp.Key, peps));
                    }
                }

                proteinPeptideDict = proteinPeptideDictTMP;

                Console.WriteLine("Peptides remaining: " + peptides.Count);
            }


            return maxParsimonyList;
        }

        public static bool HasWriteAccessToFolder(string folderPath)
        {
            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }


        /// <summary>
        /// Kullback–Leibler divergence
        /// </summary>
        /// <param name="x">Histogram values for distribution x</param>
        /// <param name="y">Histogram values for distribution y</param>
        /// <returns></returns>
        public static double KLDivergence(List<double> x, List<double> y)
        {

            if (x.Count != y.Count)
            {
                throw new Exception("Vectors must have same length for KL divergence");
            }

            double sumX = x.Sum();
            double sumY = y.Sum();
            List<double> v1 = x.Select(a => a / sumX).ToList();
            List<double> v2 = y.Select(a => a / sumY).ToList();

            double sum = 0;
            for (int i = 0; i < v1.Count; i++)
            {
                double a = Math.Log(v1[i] / v2[i]) * v1[i];

                if (double.IsNaN(a) || double.IsInfinity(a)) { 
                    continue;
                }

                sum += a;

            }

            return sum;
        }

        public static void CheckForUpdates()
        {
            ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
            UpdateCheckInfo info;

            try
            {
                info = updateCheck.CheckForDetailedUpdate();

                if (info.UpdateAvailable)
                {
                    DialogResult dialogResult = MessageBox.Show("An update is available. Would you like to update the application now?", "Update available", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.OK)
                    {
                        updateCheck.Update();
                        MessageBox.Show("The application has been upgraded, and will now restart.");
                        Application.Restart();
                    }

                }
                else
                {
                    MessageBox.Show("No updates are available for the moment.");
                }
            }
            catch (DeploymentDownloadException dde)
            {
                MessageBox.Show(dde.Message + "\n" + dde.InnerException);
                return;
            }
            catch (InvalidDeploymentException ide)
            {
                MessageBox.Show(ide.Message + "\n" + ide.InnerException);
                return;
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message + "\n" + ioe.InnerException);
                return;
            }
            catch (Exception e10)
            {
                MessageBox.Show(e10.Message);
                return;
            }

        }


        /// <summary>
        /// Benjamini-Hochberg, FDR
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="pvalues"></param>
        /// <returns></returns>
        public static double BenjaminiHochbergFDR (double alpha, List<double> pvalues, bool useDistinct)
        {
            pvalues.Sort();
            if (useDistinct)
            {
                pvalues = pvalues.Distinct().ToList();
            }

            double cuttoff = double.MaxValue;

            double q = 0;

            for (int i = 0; i < pvalues.Count; i++)
            {

                double p = pvalues[i];
                q = ( ((double)i + 1) / (double) pvalues.Count) * alpha;

                if (p < q)
                {
                    cuttoff = q;
                }
                else
                {
                    break;
                }
            }

            return cuttoff;
        }


        /// <summary>
        /// Deserialize a XML object to an object
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object LoadSeializedXMLObject(string filePath, Type type)
        {
            XmlSerializer xmlSerializer;
            FileStream fileStream = null;
            try
            {
                xmlSerializer = new XmlSerializer(type);
                fileStream = new FileStream(filePath,
                            FileMode.Open, FileAccess.Read);
                object objectFromXml =
                     xmlSerializer.Deserialize(fileStream);
                return objectFromXml;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
        }

        public static double[,] ConvertListofListsToDoubleArray (List<List<double>> input) {
            double[,] result = new double[input.Count, input[0].Count];

            for (int i = 0; i < input.Count; i++) {
                for (int j = 0; j < input[0].Count; j++) {
                    result[i, j] = input[i][j];
                }
            }

            return result;
        }

        public static List<double> SubtractVectors(List<double> x, List<double> y)
        {
            List<double> result = new List<double>(x.Count);

            for (int i = 0; i < x.Count; i++)
            {
                result.Add(x[i] - y[i]);
            }

            return result;
        }

        /// <summary>
        /// Removes the enzime cut specifications from the peptide
        /// </summary>
        /// <param name="peptide"></param>
        /// <returns></returns>
        public static string CleanPeptide(string peptide, bool removeParenthesis)
        {
            string cleanedPeptide = Regex.Replace(peptide, @"^[A-Z|\-|\*]+\.", "");
            cleanedPeptide = Regex.Replace(cleanedPeptide, @"\.[A-Z|\-|\*]+$", "");
            cleanedPeptide = cleanedPeptide.Replace("*", "");

            if (removeParenthesis)
            {
                cleanedPeptide = Regex.Replace(cleanedPeptide, @"\([0-9|\.|\+|\-| |a-z|A-Z]*\)", "");
            }

            return cleanedPeptide;
        }

        /// <summary>
        /// Merges the two vectors using a wheighted average
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static sparseMatrixRow MergeTwoInputVectors(sparseMatrixRow v1O, double weightV1, sparseMatrixRow v2O, double weightV2, bool makeBothUnitVector)
        {
            sparseMatrixRow resultingVector = new sparseMatrixRow(0);

            sparseMatrixRow v1 = v1O.Clone();
            sparseMatrixRow v2 = v2O.Clone();

            if (makeBothUnitVector) {
                v1.ConvertToUnitVector();
                v2.ConvertToUnitVector();
            }

            //Get all the dims
            List<int> allDims = v1.Dims;

            foreach (int d in v2.Dims)
            {
                if (!v2.Dims.Contains(d))
                {
                    allDims.Add(d);
                }
            }

            allDims.Sort();

            foreach (int dim in allDims)
            {
                resultingVector.Dims.Add(dim);
                int v1Index = v1.Dims.IndexOf(dim);
                int v2Index = v2.Dims.IndexOf(dim);

                double value = 0;

                if (v1Index >=0) {
                    value += v1.Values[v1Index] * weightV1;
                }

                if (v2Index >=0) {
                    value += v2.Values[v2Index] * weightV2;
                }

                resultingVector.Values.Add(value / (weightV1 + weightV2));

            }


            return(resultingVector);
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);

        }

        public static int HammingDistance(double[] v1, double[] v2)
        {
            int distance = 0;

            //Assert the vectors are the same size
            if (v1.Length != v2.Length)
            {
                throw new Exception("Hamming distance calculation error: Both vectors must be the same length");
            }

            for (int i = 0; i < v1.Length; i++)
            {
                if (v1[i] != v2[i])
                {
                    distance++;
                }
            }

            return (distance);

        }

        /// <summary>
        /// Spaces are not allowed.  Accepts Insertions, deletions, substitutions
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int LevenshteinDistance(string source, string target)
        {
            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target)) return 0;
                return target.Length;
            }
            if (String.IsNullOrEmpty(target)) return source.Length;

            if (source.Length > target.Length)
            {
                var temp = target;
                target = source;
                source = temp;
            }

            var m = target.Length;
            var n = source.Length;
            var distance = new int[2, m + 1];
            // Initialize the distance 'matrix'
            for (var j = 1; j <= m; j++) distance[0, j] = j;

            var currentRow = 0;
            for (var i = 1; i <= n; ++i)
            {
                currentRow = i & 1;
                distance[currentRow, 0] = i;
                var previousRow = currentRow ^ 1;
                for (var j = 1; j <= m; j++)
                {
                    var cost = (target[j - 1] == source[i - 1] ? 0 : 1);
                    distance[currentRow, j] = Math.Min(Math.Min(
                                distance[previousRow, j] + 1,
                                distance[currentRow, j - 1] + 1),
                                distance[previousRow, j - 1] + cost);
                }
            }
            return distance[currentRow, m];
        }


        
        /// <summary>
        /// SPACES ARE NOT ALLOWED
        /// Computes the Damerau-Levenshtein Distance between two strings, represented as arrays of
        /// integers, where each integer represents the code point of a character in the source string.
        /// Includes an optional threshhold which can be used to indicate the maximum allowable distance.
        /// Accepts insertions, deletions substitutions and swaps
        /// </summary>
        /// <param name="source">An array of the code points of the first string</param>
        /// <param name="target">An array of the code points of the second string</param>
        /// <param name="threshold">Maximum allowable distance</param>
        /// <returns>Int.MaxValue if threshhold exceeded; otherwise the Damerau-Leveshteim distance between the strings</returns>
        public static int DamerauLevenshteinDistance(int[] source, int[] target, int threshold)
        {

            int length1 = source.Length;
            int length2 = target.Length;

            // Return trivial case - difference in string lengths exceeds threshhold
            if (Math.Abs(length1 - length2) > threshold) { return int.MaxValue; }

            // Ensure arrays [i] / length1 use shorter length 
            if (length1 > length2)
            {
                Swap(ref target, ref source);
                Swap(ref length1, ref length2);
            }

            int maxi = length1;
            int maxj = length2;

            int[] dCurrent = new int[maxi + 1];
            int[] dMinus1 = new int[maxi + 1];
            int[] dMinus2 = new int[maxi + 1];
            int[] dSwap;

            for (int i = 0; i <= maxi; i++) { dCurrent[i] = i; }

            int jm1 = 0, im1 = 0, im2 = -1;

            for (int j = 1; j <= maxj; j++)
            {

                // Rotate
                dSwap = dMinus2;
                dMinus2 = dMinus1;
                dMinus1 = dCurrent;
                dCurrent = dSwap;

                // Initialize
                int minDistance = int.MaxValue;
                dCurrent[0] = j;
                im1 = 0;
                im2 = -1;

                for (int i = 1; i <= maxi; i++)
                {

                    int cost = source[im1] == target[jm1] ? 0 : 1;

                    int del = dCurrent[im1] + 1;
                    int ins = dMinus1[i] + 1;
                    int sub = dMinus1[im1] + cost;

                    //Fastest execution for min value of 3 integers
                    int min = (del > ins) ? (ins > sub ? sub : ins) : (del > sub ? sub : del);

                    if (i > 1 && j > 1 && source[im2] == target[jm1] && source[im1] == target[j - 2])
                        min = Math.Min(min, dMinus2[im2] + cost);

                    dCurrent[i] = min;
                    if (min < minDistance) { minDistance = min; }
                    im1++;
                    im2++;
                }
                jm1++;
                if (minDistance > threshold) { return int.MaxValue; }
            }

            int result = dCurrent[maxi];
            return (result > threshold) ? int.MaxValue : result;
        }

        private static void Swap<T>(ref T arg1, ref T arg2)
        {
            T temp = arg1;
            arg1 = arg2;
            arg2 = temp;
        }


        /// <summary>
        /// Returns the correlation coeficient, both arrays must have the same size
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="standardize">You should always submit standardized data, if you select true, the method will automaticaly standardize the data</param>
        /// <returns></returns>
        public double PearsonsCorrelationCoeficient(List<double> l1, List<double> l2, bool standardize)
        {
            double r = 0;

            if (l1.Count != l2.Count) {
                throw new Exception("Arrays must be the same size");
            }

            if (standardize)
            {
                l1 = Standardize(l1);
                l2 = Standardize(l2);
            }

            for (int i = 0; i < l1.Count; i++)
            {
                r += l1[i] * l2[i];
            }


            return (r / (double)(l1.Count -1));
        }

        /// <summary>
        /// Standardizes a list by subracting the mean and dividing by the standard deviation for each number
        /// </summary>
        /// <param name="theNumbers"></param>
        /// <returns></returns>
        public List<double> Standardize (List<double> theNumbers) 
        {
            //List<double> results = new List<double>();
            
            double mean = Average(theNumbers);
            double stdDev = Math.Sqrt(variance(theNumbers, true));

            return theNumbers.Select(a => (a - mean) / stdDev).ToList();
        }


        /// <summary>
        /// The correct implementation of the Pearson Cross Moment correlation
        /// Correlation score
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static double PearsonsCorrelationCorrelation (sparseMatrixRow v1, sparseMatrixRow v2)
        {
            double correlation = 0;

            /// number of elements(0), sum of product(1), sumv1squared(2), sumv2squared(3), sumv1(4), sumv2(5)
            List<double> workVariables = WorkForPearsonOrLinearRegression(v1, v2);

            //Finally, calculate the correlation
            double cNum = workVariables[0] * workVariables[1] - workVariables[4] * workVariables[5];
            
            double cDenPartA = workVariables[0] * workVariables[2] - Math.Pow(workVariables[4], 2);
            double cDenPartB = (workVariables[0] * workVariables[3] - Math.Pow(workVariables[5], 2));
            double cDen = Math.Sqrt(cDenPartA * cDenPartB);

            correlation = cNum / cDen;

            if (double.IsNaN(correlation))
            {
                correlation = 0;
            }

            return (correlation);

        }

        



        /// <summary>
        /// Input is an array of the x values, and a corresponding array of the y values
        ///  m, b, r, meanSqrError, t
        /// Returns a list were the elements are the slope, y intercept, r, mean squared error, studest t (p value)
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static List<double> LinearRegression2D(List<double> x, List<double> y)
        {
            //Just to make sure
            if (x.Count != y.Count) { throw new Exception("For linear regression, the x1 and x2 vectors should have same length"); }

            List<int> dims = new List<int>(x.Count);
            for (int i = 0; i < x.Count; i++) {
                dims.Add(i);
            }

            sparseMatrixRow vx = new sparseMatrixRow(0, dims, x);
            sparseMatrixRow vy = new sparseMatrixRow(0, dims, y);


            /// number of elements(0), sum of product(1), sumv1squared(2), sumv2squared(3), sumv1(4), sumv2(5)
            List<double> workVariables = WorkForPearsonOrLinearRegression(vx, vy);

            double sxx = workVariables[2] - (Math.Pow(workVariables[4], 2) / workVariables[0]);
            double sxy = workVariables[1] - ((workVariables[4] * workVariables[5]) / workVariables[0]);

            double m = sxy / sxx;
            double b = workVariables[5] / workVariables[0] - m * (workVariables[4] / workVariables[0]);

            double rNum = (workVariables[0] * workVariables[1] - workVariables[4] * workVariables[5]);
            double rDen = Math.Sqrt((workVariables[0] * workVariables[2] - Math.Pow(workVariables[4], 2)) * (workVariables[0] * workVariables[3] - Math.Pow(workVariables[5],2)));

            double r = rNum / rDen;

            double tValue = r / Math.Sqrt((1 - Math.Pow(r, 2)) / (workVariables[0] - 2));
            //double t = alglib.studenttdistr.studenttdistribution((int)workVariables[0], tValue);

            //Now for the squared error
            double meanSqrError = 0;

            for (int i = 0; i < y.Count; i++)
            {
                double regressorValue = m * x[i] + b;
                meanSqrError += Math.Pow(regressorValue - y[i], 2);
            }

            meanSqrError /= (double)y.Count;


            return (new List<double> { m, b, r, meanSqrError, tValue});
        }

        /// <summary>
        /// Provides necessary computation for the Pearson Correlation and Linear Regression Methods
        /// A list with the following elements in order:
        /// number of elements(0), sum of product(1), sumv1squared(2), sumv2squared(3), sumv1(4), sumv2(5)
        /// </summary>
        private static List<double> WorkForPearsonOrLinearRegression(sparseMatrixRow v1, sparseMatrixRow v2)
        {
            double numberOfElements = 0;
            double sumOfProduct = 0;
            double sumV1Squared = 0;
            double sumV2Squared = 0;
            double sumV1 = 0;
            double sumV2 = 0;

            List<double> results = new List<double>();

            int jCache = 0;

            //Find number of elements
            List<int> uniqueDims = v1.Dims;
            foreach (int dim in v2.Dims)
            {
                if (!uniqueDims.Contains(dim)) { uniqueDims.Add(dim); }
            }
            numberOfElements = (double)uniqueDims.Count;

            //Find sumof product & sum of v1, sum v1 squared
            for (int i = 0; i < v1.Dims.Count; i++)
            {
                sumV1Squared += Math.Pow(v1.Values[i], 2);
                sumV1 += v1.Values[i];


                for (int j = jCache; j < v2.Dims.Count; j++)
                {
                    if (v1.Dims[i] == v2.Dims[j])
                    {
                        jCache = j;
                        sumOfProduct += v1.Values[i] * v2.Values[j];
                        break;
                    }

                }

            }

            //Find SumV2, sum v2 squared
            for (int i = 0; i < v2.Dims.Count; i++)
            {
                sumV2Squared += Math.Pow(v2.Values[i], 2);
                sumV2 += v2.Values[i];
            }

            results.Add(numberOfElements);
            results.Add(sumOfProduct);
            results.Add(sumV1Squared);
            results.Add(sumV2Squared);
            results.Add(sumV1);
            results.Add(sumV2);

            return (results);
            
        }

        //--------------------------------------------------------------

        /// <summary>
        /// Returns the spectral angle  Values close to 1, means the vectors are unlike.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double SpectralAngle(sparseMatrixRow v1, sparseMatrixRow v2)
        {
            double result = 0;
            double SumV1Squared = 0;
            double SumV2Squared = 0;

            int jCache = 0;

            for (int i = 0; i < v1.Dims.Count; i++)
            {
                SumV1Squared += Math.Pow(v1.Values[i], 2);

                for (int j = jCache; j < v2.Dims.Count; j++)
                {
                    if (v1.Dims[i] == v2.Dims[j])
                    {
                        jCache = j;
                        result += v1.Values[i] * v2.Values[j];                     
                        break;
                    }

                }

            }

            for (int k = 0; k < v2.Dims.Count; k++)
            {
                SumV2Squared += Math.Pow(v2.Values[k], 2);
            }

            return (result / Math.Sqrt(SumV1Squared * SumV2Squared));

        }

        public static double DechargeMSPeakToPlus1(double mh, double charge)
        {
            return ((mh * charge) - (((charge - 1) * 1.007276466)));
        }

        public static double ChargeAnMHPeak(double mz, double charge)
        {
            return (mz * charge) + ((charge - 1) * 1.007276466);
        }



        /// <summary>
        /// Returns the parts per million error of two mass spectral numbers (eg theoretical and measured, and the program will return the 
        /// )
        /// </summary>
        /// <param name="n1">measurement 1</param>
        /// <param name="n2">measurement 2</param>
        /// <returns></returns>
        public static double PPM(double n1, double n2)
        {
            return ((Math.Abs(n1 - n2) * 1000000) / n2);
        }

        public static decimal PPM(decimal n1, decimal n2)
        {
            return ((Math.Abs(n1 - n2) * 1000000) / n2);
        }

        public static decimal PPM(double n1, decimal n2)
        {
            return ((Math.Abs((decimal)n1 - n2) * 1000000) / n2);
        }


        /// <summary>
        /// Only to be used with high resolution data
        /// </summary>
        /// <param name="measured"></param>
        /// <param name="theoretical"></param>
        /// <returns></returns>
        public static double OrbitrapPPM(double measured, double theoretical)
        {
            double closestInt = Math.Round(Math.Abs(measured - theoretical), 0);
            double increase = closestInt * 1.00335;

            double diff = measured - (theoretical + increase);
            double ppm = (diff * 1000000) / measured;
            return ppm;

        }


        /// <summary>
        /// Uses Cryptography to generate really random numbers
        /// </summary>
        /// <param name="maxNo"></param>
        /// <returns></returns>
        public static int getRandomNumber(int maxNo)
        {
            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];

            // Create a new instance of the RNGCryptoServiceProvider. 
            System.Security.Cryptography.RNGCryptoServiceProvider Gen = new System.Security.Cryptography.RNGCryptoServiceProvider();

            // Fill the array with a random value.
            Gen.GetBytes(randomNumber);

            // Convert the byte to an integer value to make the modulus operation easier.
            int rand = Convert.ToInt32(randomNumber[0]);

            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            if (maxNo == 0) { return 0; }
            else
            {
                return rand % maxNo;
            }
        }
        

        //----------------

        public static double TrapezoidalIntegration(List<Point> points)
        {
            if (points.Count < 2)
            {
                throw new Exception("At least two points must be provided to use the trapezoidal integration.  Point provided = " + points.Count);
            }

            points.Sort((a, b) => a.X.CompareTo(b.X));

            double result = 0;

            for (int i = 0; i < points.Count-1; i++)
            {
                double y2 = points[i+1].Y;
                double y1 = points[i].Y;
                double x2Mx1 = points[i + 1].X - points[i].X;

                double rectangle = x2Mx1 * y1;
                double triangle = x2Mx1 * ((y2 - y1) / 2);

                result += rectangle + triangle;
            }


            return result;
        }


        /// <summary>
        /// Point must be ordered in increasing order.  only works with 2D
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static double TrapezoidalIntegration(double[,] points, int dimX, int dimY)
        {
            if (points.GetLength(1) < 2)
            {
                throw new Exception("At least two points must be provided to use the trapezoidal integration.  Point provided = " + points.GetLength(1));
            }

            double result = 0;

            for (int i = 0; i < points.GetLength(1) -1; i++)
            {
                double y2 = points[dimY, i + 1];
                double y1 = points[dimY, i];
                double x2 = points[dimX, i + 1];
                double x1 = points[dimX,i];
                double x2Mx1 = x2 - x1;

                double rectangle = x2Mx1 * y1;
                double triangle = x2Mx1 * ((y2 - y1) / 2);

                result += rectangle + triangle;
            }


            return result;
        }

        /// <summary>
        /// Uses the trapezoid method to calculate the area under the normal curve
        /// a suggested delta is 0.01
        /// </summary>
        /// <param name="zValue"></param>
        /// <param name="delta"></param>
        /// <returns></returns>

        public static double areaUnderNormalTrap(double zValue, double delta)
        {

            double resultA = 1;
            double resultB = 0;
            double finalResult = 0;


            for (double i = delta; i <= zValue; i += delta)
            {
                resultB = Math.Pow(Math.E, (i * i) / -2);

                finalResult += (delta / 2) * (resultA + resultB);
                resultA = resultB;
            }

            return (Math.Round(finalResult / (Math.Sqrt(2 * Math.PI)),4));

        }

        //----------------

        /// <summary>
        /// This method also sorts the list
        /// </summary>
        /// <param name="myPoints"></param>
        /// <returns></returns>
        public static double AverageDistanceBetweenPoints(List<double> myPoints)
        {
            myPoints.Sort();
            return  (myPoints[myPoints.Count - 1] - myPoints[0]) / (double)myPoints.Count;
        }

        public static double Stdev (List<double> theNumbers, bool unbiased) {

            double vari = variance(theNumbers, unbiased);
            double stdev = Math.Sqrt(vari);
            return (stdev);
        }


        public static double variance(List<double> theNumbers, bool unbiased)
        {
            double variance = 0;

            double avg = Average(theNumbers);
            
            //sum(xBar - x)^2;
            double numerator = theNumbers.Sum(a => Math.Pow(a - avg, 2));

            if (unbiased)
            {
                variance = numerator / ((double)theNumbers.Count - 1);
            }
            else
            {
                variance = numerator / ((double)theNumbers.Count);
            }

            return (variance);
        }

        public static double Average(List<double> theNumbers)
        {
            theNumbers.RemoveAll(a => a == double.NegativeInfinity || a == double.PositiveInfinity);
            if (theNumbers.Count > 0)
            {
                return theNumbers.Average();
            } else
            {
                return 0;
            }
        }

        /// <summary>
        /// SQRT (x1^2 + x2^2 ...)
        /// </summary>
        /// <param name="theNumbers"></param>
        /// <returns></returns>
        public static double Norm(List<double> theNumbers)
        {
            return Math.Sqrt(theNumbers.Sum(a => Math.Pow(a, 2)));
        }

        public static float Norm(List<float> theNumbers)
        {
            return (float)Math.Sqrt(theNumbers.Sum(a => Math.Pow(a, 2)));
        }


        public static double EuclidianDistance(List<double> l1, List<double> l2)
        {
            double result = 0;
            if (l1.Count != l2.Count)
            {
                throw (new Exception("Array count should match for euclidian distance calculation"));
            }

            for (int i = 0; i < l1.Count; i++)
            {
                result += Math.Pow(l1[i]-l2[i],2);
            }

            return (Math.Sqrt(result));

        }

        public static List<double> TotalSignalNormalize(List<double> x)
        {
            double soma = x.Sum();
            return x.Select(a => a / soma).ToList();
        }


        /// <summary>
        /// Divides each arg by the vector norm
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static List<double> UnitVector(List<double> v)
        {
            double norm = Norm(v);
            return v.Select(a => a / norm).ToList();
        }

        public static List<float> UnitVector(List<float> v)
        {
            float norm = Norm(v);
            return v.Select(a => (float)(a / norm)).ToList();
        }

        /// <summary>
        /// If one vector is larger than the other, it will allign both vectors at position 0
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double DotProduct(List<double> v1, List<double> v2)
        {
            double result = 0;
            int theLimit = v1.Count;
            if (theLimit > v2.Count) { theLimit = v2.Count; }

            for (int i = 0; i < theLimit; i++)
            {
                result += v1[i] * v2[i];
            }
            
            return (result);
        }

        public static float DotProduct(List<float> v1, List<float> v2)
        {
            float result = 0;
            int theLimit = v1.Count;
            if (theLimit > v2.Count) { theLimit = v2.Count; }

            for (int i = 0; i < theLimit; i++)
            {
                result += v1[i] * v2[i];
            }

            return ((float)result);
        }


        static double Factorial(double val)
        {
            double result = 1;
            for (int i = 2; i <= val; i++)
            {
                result *= i;
            }
            return (result);
        }

        public double ACTest(double n1, double n2, double x1, double x2)
        {
            double answer = 0;

            if ( (x2/n2) < (x1/n1)) {
                for (int i = 0; i <= x2; i++)
                {
                    answer += ACTestStep(n1, n2, x1, i);
                }

            } else {
                for (int i = 0; i <= x1; i++)
                {
                    //Console.WriteLine("Iteration {0}, Add = {1}, cum = {2}", pn.ToString(), ACTestStep(n1, n2, pn, x2).ToString(), (answer + ACTestStep(n1, n2, pn, x2)).ToString());
                    answer += ACTestStep(n1, n2, i, x2);
                }

            }

            //For extremely low numbers
            if (double.IsNaN(answer))
            {
                answer = 0.0000000001;
            }

            if (answer < 0.00000001)
            {
                answer = 0.00000001;
            }

            return (answer);
        }

        public double ACTestStep(double n1, double n2, double x1, double x2)
        {
            double answer = 0;

            //Method 2
            answer = (Math.Pow((n2 / n1), x2)) * ACTerm(x1, x2) / Math.Pow((1 + (n2 / n1)), (x1 + x2 + 1));

            return (answer);
        }

        public double ACTerm(double x1, double x2)
        {
            double answer = 0;
            //(x1+x2)! / (x1!*x2!)
            //We will simplify by eliminating x1

            //Calculate numerator
            double numerator = 1;
            for (int i = ((int)x1 + 1); i <= (x1 + x2); i++)
            {
                numerator *= i;
            }

            answer = numerator / Factorial(x2);

            return (answer);
        }

        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }



        /// <summary>
        /// The angles must have the same number of dims; ideally, they must be unit vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double AngleCosBetweenVectors(List<double> a, List<double> b)
        {
            double dot = 0;
            for (int i = 0; i < a.Count; i++)
            {
                dot += a[i] * b[i];
            }

            double ma = Norm(a);
            double mb = Norm(b);

            double cos = dot / (ma * mb);

            if (cos > 1) { cos = 1; }
            if (cos < 0) { cos = 0; }

            return cos;
        }

        //-----------------------------------------

        //Generate all possible combinations
        public static IEnumerable<string> Combinations(List<string> characters, int length)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                // only want 1 character, just return this one
                if (length == 1)
                    yield return characters[i];

                // want more than one character, return this one plus all combinations one shorter
                // only use characters after the current one for the rest of the combinations
                else
                    foreach (string next in Combinations(characters.GetRange(i + 1, characters.Count - (i + 1)), length - 1))
                    {
                        yield return characters[i] + next;
                    }
            }
        }

        public static List<List<int>> GetClassCombinations(int n)
        {
            List<List<int>> theResults = new List<List<int>>();

            //Generate all combination of classes
            List<string> combinations = new List<string>();
            List<string> combinationResult = new List<string>();

            for (int i = 0; i < n; i++)
            {
                combinations.Add(i.ToString());
            }
            combinationResult.AddRange(combinations);


            for (int i = 2; i <= n; i++)
            {
                combinationResult.AddRange(Combinations(combinations, i).ToList());
            }

            foreach (string s in combinationResult)
            {
                List<int> partialResult = new List<int>();
                for (int i = 0; i < s.Length; i++)
                {
                    int ti = int.Parse(s[i].ToString());
                    partialResult.Add(ti);
                }
                theResults.Add(partialResult);
            }

            return theResults;

        }

        //-----------------------------------------

        /// <summary>
        /// This method immitated a blanket, getting only the top peaks.  It was created for SEProQ
        /// </summary>
        /// <param name="myPoints"></param>
        /// <param name="maxNoCycles"></param>
        /// <param name="minPointsInSmoothedArray"></param>
        /// <returns></returns>
        public static List<PatternTools.Point> SmoothList(List<PatternTools.Point> myPoints, int maxNoCycles, int minPointsInSmoothedArray)
        {
            List<PatternTools.Point> smoothedPoints = PatternTools.ObjectCopier.Clone(myPoints);

            int lastint = smoothedPoints.Count;
            int maxSmoothCycles = maxNoCycles;
            int smoothCycleCounter = 0;
            while (true)
            {
                smoothedPoints = Smooth(smoothedPoints);
                if (smoothedPoints.Count < minPointsInSmoothedArray || smoothedPoints.Count == lastint || smoothCycleCounter == maxSmoothCycles)
                {
                    break;
                }
                lastint = smoothedPoints.Count;
                smoothCycleCounter++;
            }
            return smoothedPoints;
        }

        private static List<PatternTools.Point> Smooth(List<PatternTools.Point> thePoints)
        {
            List<PatternTools.Point> smoothed = new List<PatternTools.Point>(thePoints.Count);
            smoothed.Add(thePoints[0]);

            for (int i = 1; i < thePoints.Count - 1; i++)
            {
                if (thePoints[i].Y < thePoints[i - 1].Y && thePoints[i].Y < thePoints[i + 1].Y)
                {
                    //we should not consider this point for quantitation
                }
                else
                {
                    smoothed.Add(thePoints[i]);
                }
            }

            smoothed.Add(thePoints.Last());

            return smoothed;
        }

        /// <summary>
        /// Mata-analysis
        /// http://en.wikipedia.org/wiki/Fisher%27s_method
        /// </summary>
        /// <param name="pValues"></param>
        /// <returns></returns>
        public static double FischersMethod(List<double> pValues)
        {
            if (pValues.Count == 1)
            {
                return pValues[0];
            }
            else
            {
                double chi = -2 * pValues.Sum(a => Math.Log(a));
                double p = alglib.chisquaredistribution(chi, pValues.Count * 2);
                return p;
            }
        }

        /// <summary>
        /// Mata-analysis
        /// http://en.wikipedia.org/wiki/Fisher%27s_method
        /// </summary>
        /// <param name="p">the p values</param>
        /// <param name="w">the weights</param>
        /// <param name="distribution"> enter 0 for normal or 1 for t</param>
        /// <returns></returns>
        public static double StouffersMethod(List<double> p, List<double> w, int distribution = 0)
        {
            //We shouldnt be using this method in first place if there is only one p-value
            if (p.Count == 1)
            {
                return p[0];
            }

            if (w == null)
            {
                w = p.Select(a => a = 1).ToList();
            }
            if (w.Count != p.Count)
            {
                throw new Exception("p-vector and w-vector must have the same length");
            }

            double numerator = 0;
            for (int i = 0; i < p.Count; i++)
            {
                if (distribution == 0)
                {
                    numerator += alglib.invnormaldistribution(1 - p[i]) * w[i];
                } else
                {
                    numerator += alglib.invstudenttdistribution(p.Count, (1 - p[i])) * w[i];
                }
            }

            double denominator = Math.Sqrt(w.Sum(a => Math.Pow(a, 2)));
            double z = numerator / denominator;

            double pValue = -1;

            if (distribution == 0)
            {
                pValue = 1 - alglib.normaldistribution(z);
            } else
            {
                pValue = 1 - alglib.studenttdistr.studenttdistribution(p.Count, z);
            }

            return pValue;
        }

        public static double StouffersMethod(List<double> p)
        {
            List<double> w = new List<double>();
            foreach (double pvalue in p)
            {
                w.Add(pvalue);
            }
            return StouffersMethod(p, w);
        }
    }
}
