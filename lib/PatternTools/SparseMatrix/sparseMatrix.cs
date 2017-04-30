/*
 * Created by SharpDevelop.
 * User: paulo
 * Date: 1/14/2007
 * Time: 12:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace PatternTools
{
	/// <summary>
	/// The SparseMatrix.
	/// </summary>
    [Serializable]
	public class SparseMatrix
	{
        private List<double> medianVector;
		
		List<sparseMatrixRow> matrix = new List<sparseMatrixRow>();

        /// <summary>
        /// CSML is a powerful linear algebra lib
        /// </summary>
        /// <returns></returns>
        public PatternTools.CSML.Matrix ToCSMLMatrix ()
        {
            this.UnsparseTheMatrix();

            double [,] newMatrix = new double[this.theMatrixInRows[0].Dims.Count, this.theMatrixInRows.Count];
            
            for (int y = 0; y < theMatrixInRows.Count; y++) {
                for (int x = 0; x < theMatrixInRows[0].Dims.Count; x++) {
                    newMatrix[x,y] = theMatrixInRows[y].Values[x];
                }
            }


            return(new CSML.Matrix(newMatrix));
        }


        /// <summary>
        /// Uses SVD to reduce the dimension
        /// </summary>
        /// <param name="newDim"></param>
        /// <returns></returns>
        private double[,] SVDDimensionalReduction(int newDim)
        {
            if (newDim >= this.allDims().Count)
            {
                throw new Exception("The new number of dimensions must be less than the current number of dims");
            }
            double[,] mySimplifiedM = this.ToDoubleArrayMatrix();
            //PatternTools.CSML.Matrix originalM = new PatternTools.CSML.Matrix(myMatrix);

            double[] sigma = null;
            double[,] u = null;
            double[,] vt = null;


            alglib.svd.rmatrixsvd(
                mySimplifiedM,                   //Input
                mySimplifiedM.GetLength(0),      //# rows
                mySimplifiedM.GetLength(1),      //# columns
                2,                          //compute whole U
                1,                          //Compute whole v_transpose
                2,                          //Use max available RAM for speed
                ref sigma,
                ref u,
                ref vt
                );

            //Bring it down to 2 domensions
            double[,] sigmaM = new double[mySimplifiedM.GetLength(0), mySimplifiedM.GetLength(1)];

            for (int i = 0; i < mySimplifiedM.GetLength(1); i++)
            {
                for (int j = 0; j < mySimplifiedM.GetLength(0); j++)
                {
                    if (i == j && i < newDim)
                    {
                        sigmaM[i, j] = sigma[i];
                    }
                }
            }


            //Generate the final positions
            PatternTools.CSML.Matrix uM = new PatternTools.CSML.Matrix(u);
            PatternTools.CSML.Matrix sM = new PatternTools.CSML.Matrix(sigmaM);
            PatternTools.CSML.Matrix vtM = new PatternTools.CSML.Matrix(vt);

            PatternTools.CSML.Matrix step1M = uM * sM;
            PatternTools.CSML.Matrix step2 = step1M * vtM.Transpose();


            double[,] resultMatrix = new double[this.theMatrixInRows.Count, newDim];

            for (int i = 1; i <= this.theMatrixInRows.Count; i++)
            {
                for (int j = 1; j <= newDim; j++)
                {
                    double r = step1M[i, j].Re;
                    resultMatrix[i - 1, j - 1] = r;
                }
            }

            return resultMatrix;


        }


        public double[][] ToDoubleDoubleMatrix()
        {
            this.UnsparseTheMatrix();

            double[][] data = new double[theMatrixInRows.Count][];
            int[] labels = new int[theMatrixInRows.Count];

            for (int i = 0; i < theMatrixInRows.Count; i++)
            {
                data[i] = theMatrixInRows[i].Values.ToArray();
            }

            return data;
        }

        public double[,] ToDoubleArrayMatrix()
        {
            this.UnsparseTheMatrix();

            double[,] newMatrix = new double[this.theMatrixInRows.Count, this.theMatrixInRows[0].Dims.Count];

            for (int y = 0; y < theMatrixInRows.Count; y++)
            {

                for (int x = 0; x < theMatrixInRows[0].Dims.Count; x++)
                {
                    newMatrix[y, x] = theMatrixInRows[y].Values[x];
                }
            }

            return (newMatrix);

        }

        public double[][] ToDoubleDoubleArrayMatrix()
        {
            this.UnsparseTheMatrix();

            double[][] newMatrix = new double[this.theMatrixInRows.Count][];

            for (int i = 0; i < theMatrixInRows.Count; i++)
            {
                newMatrix[i] = theMatrixInRows[i].Values.ToArray();
            }

            return (newMatrix);

        }


        /// <summary>
        /// Used to store description for each of the present classes
        /// The class descriptions will be included in the begining of the sparse matrix file following the scheme:
        /// #ClassDescription\tClassNo\tDescription
        /// </summary>
        public Dictionary<int, string> ClassDescriptionDictionary { get; set; }
		
		public object Matrix {
			get { return matrix; }
		}

        public List<sparseMatrixRow> theMatrixInRows
        {
            get { return matrix; }
            set { matrix = value; }
        }
		
		//Constructors
		public SparseMatrix()
		{
            ClassDescriptionDictionary = new Dictionary<int, string>();
		}


        /// <summary>
        /// This method is used to generate, for example, a decoy sparse matrix, by shuffling the original sparse matrix
        /// This method automatically unsparses the matrix
        /// </summary>
        /// <param name="noOfShuffles"></param>
        public void Shuffle(int noOfShuffles)
        {
            int shuffles = 0;
            List<int> allDims = this.allDims();

            //Lets unsparse the matrix
            this.UnsparseTheMatrix();

            while (shuffles <= noOfShuffles)
            {
                shuffles++;

                //pick two random input vectors
                //we need to make sure that we can pick zero
                int ivA = pTools.getRandomNumber(matrix.Count - 1);
                int ivB = pTools.getRandomNumber(matrix.Count - 1);

                //get the indexes
                int dimA = pTools.getRandomNumber( allDims.Count -1 );
                int dimB = pTools.getRandomNumber( allDims.Count -1 );

                //get the values
                double valueInA = matrix[ivA].Values[dimA];
                double valueInB = matrix[ivB].Values[dimB];

                //swap the values
                matrix[ivA].Values[dimA] = valueInB;
                matrix[ivB].Values[dimB] = valueInA;

            }

        }

        /// <summary>
        /// Usualy, negative dims are dims that were previously flagged, to be eliminated in a latter stage.
        /// Here, we eliminate these dims.
        /// </summary>
        public void EliminateNegativeDims()
        {
            List<int> allDims = this.allDims();
            allDims.Sort();

            foreach (int dim in allDims)
            {
                if (dim < 0)
                {
                    eliminateDim(dim, 0, true);
                }
            }
        }

        /// <summary>
        /// Will make every input vector contain all the dimensions, even if it has to include zeroes
        /// </summary>
        public void UnsparseTheMatrix()
        {
            List<int> allDims = this.allDims();
            allDims.Sort();

            foreach (sparseMatrixRow r in theMatrixInRows)
            {
                List<double> newValues = new List<double>(allDims.Count);

                foreach (int dim in allDims)
                {
                    if (r.Dims.Contains(dim))
                    {
                        newValues.Add(r.Values[r.Dims.IndexOf(dim)]);
                    }
                    else
                    {
                        newValues.Add(0);
                    }
                }

                r.Dims = allDims;
                r.Values = newValues;
            }
        }

        public void ConvertAllVectorsToUnitVectors()
        {
            foreach (sparseMatrixRow r in theMatrixInRows)
            {
                r.ConvertToUnitVector();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Minimum number of replicate experiments per class the id should be present in</param>
        public void EliminateIDsThatAreNotPresentAtLeastInXReplicates(int x, bool allClassesMode)
        {

            List<int> allClasses = this.ExtractLabels();
            List<int> allDims = this.allDims();

            if (allClassesMode)
            {

                foreach (int dim in allDims)
                {
                    //It needs to satisfy the condition of being present at least x times in one condition
                    bool satisfiesCondition = false;

                    foreach (int lbl in allClasses)
                    {
                        List<sparseMatrixRow> theRows = matrix.FindAll(a => a.Lable == lbl && a.Dims.Contains(dim));

                        if (theRows.Count >= x)
                        {
                            satisfiesCondition = true;
                        }
                    }

                    if (!satisfiesCondition)
                    {
                        eliminateDim(dim, 0, true);
                    }

                }
            }
            else
            {
                //Per class mode
                foreach (int dim in allDims)
                {
                    //It needs to satisfy the condition of being present at least x times in one condition
                    
                    foreach (int lbl in allClasses)
                    {
                        bool satisfiesCondition = true;

                        List<sparseMatrixRow> theRows = matrix.FindAll(a => a.Lable == lbl && a.Dims.Contains(dim));

                        if (theRows.Count < x)
                        {
                            satisfiesCondition = false;
                        }

                        if (dim == 5 && lbl == 2)
                        {
                            Console.WriteLine(".");
                        }

                        if (!satisfiesCondition)
                        {
                            eliminateDim(dim, lbl, false);
                        }
                    }



                }


            }
        }

        /// <summary>
        /// Used as a replacement for the mean vector for robust outlier statistics
        /// </summary>
        /// <returns></returns>
        public List<double> GetMedianVector()
        {
            List<int> allTheDims = allDims();

            //Obtain the mean and covariances for each dim
            List<double> median = new List<double>(allTheDims.Count);

            foreach (int dim in allTheDims)
            {
                List<double> values = this.ExtractDimValues(dim, 0, true);
                values.Sort();
                double v = (double)values.Count / 2;
                int meanIndex = (int)Math.Round(v, 0);
                median.Add(values[meanIndex]);
            }

            medianVector = median;

            return median;
        }

        /// <summary>
        /// This method can only be applied to Unsparse matrices
        /// This vector will show the mean value for each dimension, respectively
        /// </summary>
        /// <returns></returns>
        public List<double> GetMeanVector()
        {
            List<int> allTheDims = allDims();

            //Obtain the mean and covariances for each dim
            List<double> mean = new List<double>(allTheDims.Count);

            foreach (int dim in allTheDims)
            {
                List<double> values = this.ExtractDimValues(dim, 0, true);
                mean.Add(values.Average());
            }

            return mean;
        }

        /// <summary>
        /// This vector will provide the standard deviation for each dimension, respectively
        /// </summary>
        public List<double> GetStandardDevVector()
        {
            List<int> allTheDims = allDims();

            //Obtain the stdev for each dim
            List<double> stdev = new List<double>(allTheDims.Count);

            foreach (int dim in allTheDims)
            {
                List<double> values = this.ExtractDimValues(dim, 0, true);
                double stdevRow = PatternTools.pTools.Stdev(values, true);
                if (double.IsNaN(stdevRow))
                {
                    throw new Exception("Problems computing standard deviation of matix row");
                }
                stdev.Add(stdevRow);
            }


            return stdev;
        }


        /// <summary>
        /// This method can only be applied to Unsparse matrice
        /// </summary>

        public static double[,] CovarianceMatrix(double[,] matrix, double[] mean)
        {
            double divide = matrix.GetLength(0) - 1;
            double dimension = 1;

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            double[,] cov;

            if (dimension == 1)
            {
                cov = new double[cols, cols];
                for (int i = 0; i < cols; i++)
                {
                    for (int j = i; j < cols; j++)
                    {
                        double s = 0.0;
                        for (int k = 0; k < rows; k++)
                        {
                            s += (matrix[k, j] - mean[j]) * (matrix[k, i] - mean[i]);
                        }
                        s /= divide;
                        cov[i, j] = s;
                        cov[j, i] = s;

                        if (double.IsInfinity(s))
                        {
                            throw new Exception("Infinity obtained when generating sparse matrix");
                        }
                    }
                }
            }
            else
            {
                cov = new double[rows, rows];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = i; j < rows; j++)
                    {
                        double s = 0.0;
                        for (int k = 0; k < cols; k++)
                            s += (matrix[j, k] - mean[j]) * (matrix[i, k] - mean[i]);
                        s /= divide;
                        cov[i, j] = s;
                        cov[j, i] = s;
                    }
                }
            }

            return cov;
        }

        public SparseMatrix ShatterMatrixSum ()
        {
            List<int> labels = ExtractLabels();

            List<sparseMatrixRow> newRows = new List<sparseMatrixRow>(labels.Count);
            List<int> dims = allDims();

            foreach (int l in labels)
            {
                List<sparseMatrixRow> rowsWithSameLabel = theMatrixInRows.FindAll(a => a.Lable == l);
                List<double> values = new List<double>(dims.Count);

                for (int i = 0; i < dims.Count; i++)
                {
                    values.Add(ExtractDimValues(i, l, false).Sum());
                }

                newRows.Add(new sparseMatrixRow(l, dims, values));
            }
            SparseMatrix sm = new SparseMatrix(newRows);
            sm.ClassDescriptionDictionary = ClassDescriptionDictionary;

            return sm;
        }


        /// <summary>
        /// This method compresses the matrix only to two vectors, one from the positive class
        /// and another from the negative.  This is achieved by averaging the values for each dimension
        /// </summary>
        /// 
        public void shatterMatrix()
        {

            SortedDictionary<int, double> pos = new SortedDictionary<int, double>();
            SortedDictionary<int, double> neg = new SortedDictionary<int, double>();

            int noPosVectors = 0;
            int noNegVectors = 0;


            //Sum EveryBody
            foreach (sparseMatrixRow r in matrix)
            {
                if (r.Lable > 0)
                {
                    //Positive Class
                    noPosVectors++;
                    for (int i = 0; i < r.Dims.Count; i++)
                    {
                        if (pos.ContainsKey(r.Dims[i]))
                        {
                            pos[r.Dims[i]] += r.Values[i];
                        }
                        else
                        {
                            pos.Add(r.Dims[i], r.Values[i]);
                        }
                    }
                }
                else
                {
                    //Negative Class
                    noNegVectors++;
                    for (int i = 0; i < r.Dims.Count; i++)
                    {
                        if (neg.ContainsKey(r.Dims[i]))
                        {
                            neg[r.Dims[i]] += r.Values[i];
                        }
                        else
                        {
                            neg.Add(r.Dims[i], r.Values[i]);
                        }
                    }
                }

            }

            List<int> posDims = new List<int>();
            List<double> posValues = new List<double>();
            List<int> negDims = new List<int>();
            List<double> negValues = new List<double>();


            foreach (KeyValuePair<int, double> kvp in pos)
            {
                posDims.Add(kvp.Key);
                posValues.Add(kvp.Value / noPosVectors);
            }

            foreach (KeyValuePair<int, double> kvp in neg)
            {
                negDims.Add(kvp.Key);
                negValues.Add(kvp.Value / noNegVectors);
            }

            this.matrix.Clear();

            sparseMatrixRow posRow = new sparseMatrixRow(1, posDims, posValues);
            sparseMatrixRow negRow = new sparseMatrixRow(-1, negDims, negValues);

            this.matrix.Add(posRow);
            this.matrix.Add(negRow);

        }

        //----

        public SparseMatrix(List<sparseMatrixRow> matrix)
        {
            this.matrix = matrix;
        }

        //----

        public SparseMatrix(StreamReader fileName)
        {
            getMatrixFromString(fileName.ReadToEnd());
            fileName.Close();
        }

        public SparseMatrix(string matrixAsAString, bool blank)
        {
            getMatrixFromString(matrixAsAString);
        }

        //------

        public void addRow(sparseMatrixRow theRow)
        {
            this.matrix.Add(theRow);
        }

        public void clearMatrix()
        {
            this.matrix.Clear();
        }

        //------------------

        public void dispose()
        {
            this.matrix.Clear();
        }

        public bool getMatrixFromString (string theMatrixInAString) 
        {
            matrix.Clear();

            string[] matrixInRows = Regex.Split(theMatrixInAString, "[\n|\r]");

            ClassDescriptionDictionary = new Dictionary<int, string>();

            string lastFileName = "";

            //Declare the PathCandidates we will use
            Regex spaceSplitter = new Regex(@"[\s]+");
            Regex doubleDotSplitter = new Regex(@":");

            //Parse and load to Ram
            foreach (string read in matrixInRows)
            {

                //Keep us safe from comments
                if (read.StartsWith("#ClassDescription"))
                {
                    string[] cols = Regex.Split(read, "\t");
                    ClassDescriptionDictionary.Add(int.Parse(cols[1]), cols[2]);
                    continue;

                }
                else if (read.StartsWith("#"))
                {
                    lastFileName = read.Substring(1, read.Length - 1);
                    continue;
                }


                if (read.Length == 0) { continue; }

                List<int> dims = new List<int>();
                List<double> values = new List<double>();

                string[] line = spaceSplitter.Split(read);
                int lable = int.Parse(line[0]);


                for (int i = 1; i < line.Length; i++)
                {
                    string[] info = doubleDotSplitter.Split(line[i]);

                    try
                    {
                        dims.Add(int.Parse(info[0]));
                        values.Add(double.Parse(info[1]));
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message + "\n" + info[0].ToString() + " " + info[1].ToString() + "\n" + "Line " + i.ToString() + "\n" + line[i].ToString());
                    }

                }

                sparseMatrixRow row = new sparseMatrixRow(lable, dims, values);
                row.FileName = lastFileName;
                matrix.Add(row);

            }

            return true;
        }            


        //------------------

        public StringBuilder MatrixAsString()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                //First lets write some class descriptios; if we have some
                if (ClassDescriptionDictionary != null)
                {
                    foreach (KeyValuePair<int, string> kvp in ClassDescriptionDictionary)
                    {
                        sb.AppendLine("#ClassDescription\t" + kvp.Key + "\t" + kvp.Value);
                    }
                }

                foreach (sparseMatrixRow r in matrix)
                {
                    sb.AppendLine("#" + r.FileName);

                    sb.Append(r.Lable);

                    for (int i = 0; i < r.Values.Count; i++)
                    {
                        sb.Append(" " +  r.Dims[i] + ":" + r.Values[i]);
                    }
                    sb.AppendLine();
                }
            }
            catch (Exception e)
            {
                //throw(e);
                MessageBox.Show("Error generating sparse matrix.\n" + e.GetBaseException().ToString());
            }

            return sb;
        }
		
		public void saveMatrix (string fileName) 
        {
            StreamWriter WRITE = new StreamWriter(fileName);
            WRITE.Write(MatrixAsString());
            WRITE.Close();
		}

        /// <summary>
        /// This method adds the number specified to every value of the matrix,
        /// generaly, a pseudocount of 1 is specified
        /// </summary>
        /// <param name="counts"></param>

        public void addPseudoCounts (double count) {
            
            List<int> allTheDims = allDims();
            
            foreach (sparseMatrixRow r in matrix)
            {
                //make a dictionary for that row
                SortedDictionary<int, double> rowDic = new SortedDictionary<int, double>();
  
                //Note that we are already adding a value of 1 to existing counts
                for (int i = 0; i < r.Dims.Count; i++)
                {
                    rowDic.Add(r.Dims[i], r.Values[i]+count);
                }

                //Now lets add the counts, to the zero value indexes
                for (int i = 0; i < allTheDims.Count; i++)
                {
                    if (rowDic.ContainsKey(allTheDims[i])) {
                        //we already added the count
                    } else {
                        rowDic.Add(allTheDims[i], count);
                    }
                }

                List<int> newDims = new List<int>();
                List<double> newValues = new List<double>();

                foreach (KeyValuePair<int,double> kvp in rowDic) {
                    newDims.Add(kvp.Key);
                    newValues.Add(kvp.Value);
                }

                //Finally, return the row
                r.Dims = newDims;
                r.Values = newValues;
            }
        }
        
        /// <summary>
        /// Returns a sorted list of all dimensions in the sparse matrix
        /// </summary>
        /// <returns></returns>
        public List<int> allDims () {

            List<int> allDims = (from row in matrix
                                 from d in row.Dims
                                 select d).Distinct().ToList();
            allDims.Sort();
        	return (allDims);        	
        }

        public void eliminateDim(int dim, int classLable, bool useZeroAsClassLableJoker)
        {
            foreach (sparseMatrixRow r in this.matrix)
            {
                if (r.Lable == classLable || (classLable == 0 && useZeroAsClassLableJoker))
                {

                    //Find the index
                    int dimIndex2Eliminate = r.Dims.IndexOf(dim);

                    if (dimIndex2Eliminate != -1)
                    {
                        //Eliminate
                        try
                        {
                            r.Dims.RemoveAt(dimIndex2Eliminate);
                            r.Values.RemoveAt(dimIndex2Eliminate);
                        }
                        catch
                        {
                            MessageBox.Show("Problems Eliminating Dim " + dimIndex2Eliminate.ToString() + "\n");
                        }

                    }
                }


            }
        }

        /// <summary>
        /// Converts the Dim ID value to negative.. This is usualy used to "tag" some dimensions, since many patternlab software will only consider positive dimensions.
        /// </summary>
        /// <param name="dim"></param>
        public void TransformDimIdToNegative(int dim)
        {
            foreach (sparseMatrixRow r in this.matrix)
            {

                //Find the index
                int dimIndex2Transform = r.Dims.IndexOf(dim);

                if (dimIndex2Transform != -1)
                {
                    //Eliminate
                    try
                    {
                        r.Dims[dimIndex2Transform] *= -1;
                    }
                    catch
                    {
                        throw (new Exception("Problems Eliminating Dim " + dimIndex2Transform+ "\n"));
                    }

                }


            }
        }


        public List <double> ExtractDimValues (int dim, int lable, bool useClassLabel0AsJoker) {
            
            List<double> theValues = new List<double>();


            foreach (sparseMatrixRow row in matrix)
            {
                //MessageBox.Show("The row Lable = "+row.Lable+ " the sent lable = "+lable);
                if (row.Lable == lable || (lable == 0 && useClassLabel0AsJoker))
                {
                    int theIndex = row.Dims.IndexOf(dim);
                    if (theIndex != -1)
                    {
                        theValues.Add(row.Values[theIndex]);
                    }
                }
            }

            return (theValues);
        }

        public List<double> ExtractDimValues(int dim)
        {

            List<double> theValues = new List<double>();


            foreach (sparseMatrixRow row in matrix)
            {
                int theIndex = row.Dims.IndexOf(dim);
                if (theIndex != -1)
                {
                    theValues.Add(row.Values[theIndex]);
                }
            }

            return (theValues);
        }

        //Normalization Methods
        /// <summary>
        /// This method receives a list of indexes coresponding to spiked markers.  It sums the signal from these markers
        /// and then normalizes all items according to the sum of the signal from the corresponding markers
        /// 
        /// </summary>
        /// <param name="standards"></param>
        public void InternalStandardNormalization(List<int> standards)
        {
            foreach (sparseMatrixRow r in matrix)
            {
                //Collect the rows standardSignal
                double totalSpikedStandardSignal = 0;

                for (int i = 0; i < r.Dims.Count; i++)
                {
                    foreach (int standard in standards)
                    {
                        if (r.Dims[i] == standard)
                        {
                            totalSpikedStandardSignal += r.Values[i];
                        }
                    }
                }

                //Now normalize all values by the sum of the internal standard signal
                for (int i = 0; i < r.Dims.Count; i++)
                {
                    r.Values[i] /= totalSpikedStandardSignal;
                }

            }
        }


        public void zNormalization()
        {
            List<int> allDims = this.allDims();
            PatternTools.pTools ptool = new PatternTools.pTools();

            for (int dimIndex = 0; dimIndex < allDims.Count; dimIndex++)
            {
                //Calculate mean and standard deviation
                List<double> theNumbers = this.ExtractDimValues(allDims[dimIndex], 0, true);
                
                double mean = PatternTools.pTools.Average(theNumbers);
                double stdDev = Math.Sqrt(PatternTools.pTools.variance(theNumbers, true));

                
                //Finaly Manipulate the matrix

                foreach (sparseMatrixRow row in matrix)
                {
                    //find in what index our value is located
                    int index = getDimIndexFromValue(row, allDims[dimIndex]);
                    //MessageBox.Show("Working on row " + matrix.IndexOf(row) + " the index is " + index );

                    //modify the value
                    if (index >= 0)
                    {
                        double value = (row.Values[index] - mean) / stdDev;
                        if (value.Equals(double.NaN))
                        {
                            row.Values[index] = 0;
                        }
                        else
                        {
                            row.Values[index] = value;
                        }
                    }
                }

            }
        }

        //--------------------------------------------------

        public void lnNormalization () {

            foreach (sparseMatrixRow r in matrix)
            {
                for (int d = 0; d < r.Values.Count; d++)
                {
                    r.Values[d] = Math.Log(r.Values[d], Math.E);
                }
            }            
        }

        //------------------------------------------------

        public void totalSignalNormalization()
        {

            foreach (sparseMatrixRow row in matrix)
            {
                //Obtain the total count for the row
                double totalCount = 0;
                for (int dimIndex = 0; dimIndex < row.Values.Count; dimIndex++)
                {
                    totalCount += row.Values[dimIndex];
                }

                //divide each item in the row
                for (int dimIndex = 0; dimIndex < row.Values.Count; dimIndex++)
                {
                    row.Values[dimIndex] /= totalCount;
                }

            }
        }

        //
        public void maximumSignalNormalization()
        {
            foreach (sparseMatrixRow row in matrix)
            {
                //Obtain the maximum value
                double maximumValue = 0;
                for (int dimIndex = 0; dimIndex < row.Values.Count; dimIndex++)
                {
                    if (row.Values[dimIndex] > maximumValue)
                    {
                        maximumValue = row.Values[dimIndex];
                    }
                }

                //divide each item in the row
                for (int dimIndex = 0; dimIndex < row.Values.Count; dimIndex++)
                {
                    row.Values[dimIndex] /= maximumValue;
                }

            }
        }

        public void rowSigmaNormalization (int sigmaMultiplier) {

            PatternTools.pTools ptool = new PatternTools.pTools();

            foreach (sparseMatrixRow row in matrix)
            {
                //Calculate the row sigma

                List<double> rowValues = new List<double>();
                foreach (double v in row.Values)
                {
                    rowValues.Add(v);
                }

                double rowSigma = Math.Sqrt(PatternTools.pTools.variance(rowValues, true));
                double rowAverage = PatternTools.pTools.Average(rowValues);
                double sigmaFactor = rowSigma * sigmaMultiplier;

                //divide each item in the row
                for (int dimIndex = 0; dimIndex < row.Values.Count; dimIndex++)
                {
                    row.Values[dimIndex] /= (sigmaFactor + rowAverage);
                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void NormalizeAllColumnsToRangeFrom0To1New()
        {
            List<int> allDims = (from r in this.theMatrixInRows
                                 from d in r.Dims
                                 select d).Distinct().ToList();

            for (int dimIndex = 0; dimIndex < allDims.Count; dimIndex++)
            {
                //Find the minimum and maximum values;
                double maxValue = theMatrixInRows.Max(a => a.Values[dimIndex]);
                double minValue = theMatrixInRows.Min(a => a.Values[dimIndex]);

                //Finaly Manipulate the matrix
                double diference = maxValue - minValue;
                foreach (sparseMatrixRow r in matrix)
                {
                    r.Values[dimIndex] = (r.Values[dimIndex] - minValue) / diference;
                }

            }

        }

            //----------------------------------------------

        //Private Methods
        /// <summary>
        /// If the index is not found, -1 will be returned
        /// </summary>
        /// <param name="row"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int getDimIndexFromValue (sparseMatrixRow row, int value) {
            return row.Dims.IndexOf(value);
        }


        /// <summary>
        /// Return a sorted list of the lables
        /// </summary>
        /// <returns></returns>
        public List<int> ExtractLabels()
        {
            List<int> labels = matrix.Select(a => a.Lable).Distinct().ToList();
            labels.Sort();

            return (labels);
        }

    }


    //---------------------------------------------------------------

}
