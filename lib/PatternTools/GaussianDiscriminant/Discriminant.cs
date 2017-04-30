using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using PatternTools.CSML;

namespace PatternTools.GaussianDiscriminant
{
    [Serializable]
    public class Discriminant : IPLDiscriminator
    {
        Dictionary<int, ClassPackage> classLableClassPackageDic = new Dictionary<int, ClassPackage>();
        PatternTools.SparseMatrix mySparseMatrix = new SparseMatrix();

        //These variables will be cached for classification speed

        /// <summary>
        /// Provides access to the classes of thr classificator
        /// </summary>
        public Dictionary<int, ClassPackage> ClassLableClassPackageDic
        {
            get
            {
                return (classLableClassPackageDic);
            }
        }
        

        public PatternTools.SparseMatrix MySparseMatrix
        {
            get { return mySparseMatrix; }
            set { mySparseMatrix = value; }
        }
        
        public Discriminant() { }

        /// <summary>
        /// Saves a SOAP serialized model
        /// </summary>
        /// <param name="filename"></param>
        public void SaveModel(string fileName)
        {
            System.IO.FileStream flStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();

            foreach (KeyValuePair<int, ClassPackage> kvp in classLableClassPackageDic) {
                kvp.Value.ClearWorkingMatrix();
            }
            
            bf.Serialize(flStream, this.classLableClassPackageDic);
            flStream.Close();
        }

        /// <summary>
        /// Loads a bin serialized model
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadModel(string fileName)
        {
            FileStream flStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            this.classLableClassPackageDic = (Dictionary<int, ClassPackage>)bf.Deserialize(flStream);
            flStream.Close();
        }

        public void LoadModel(byte[] theByteArray)
        {
            Stream stream = new MemoryStream(theByteArray);
            BinaryFormatter bf = new BinaryFormatter();
            this.classLableClassPackageDic = (Dictionary<int, ClassPackage>)bf.Deserialize(stream);
            stream.Close();
        }

        

        //-------------------------------
        /// <summary>
        /// Loads the sarse matrix from a file
        /// </summary>
        /// <param name="fileName"></param>
        public void AppendMatrix(string fileName)
        {
            PatternTools.SparseMatrix sparseMatrix = new PatternTools.SparseMatrix(new StreamReader(fileName));
            mySparseMatrix.theMatrixInRows.AddRange(sparseMatrix.theMatrixInRows);
        }

        /// <summary>
        /// Loads the sparse matrix from an object instance
        /// </summary>
        /// <param name="sparsematrix"></param>
        public void AppendMatrix(SparseMatrix sparsematrix)
        {
            mySparseMatrix.theMatrixInRows.AddRange(sparsematrix.theMatrixInRows);
        }


        /// <summary>
        /// Modelling is required prior to classification
        /// Evoking this method will also delete the sparse matrix resident in this object
        /// For the MinNumber of Entries, the default used in the CPM was 50.
        /// Robust statistics uses Median and Median Absolute Deviation to replace mean and standard deviation
        /// Returns unstable dims (that result in singularity)
        /// </summary>
        public void Model (bool brainWash, List<int> classesToEliminate, int minNumberOfEntriesForAClass, bool useRobustStatistics, bool saveMatrices2DiskForDebug) {

            //mySparseMatrix.UnsparseTheMatrix();
            classLableClassPackageDic.Clear();

            //Feed our dictionary to all the classes
            foreach (PatternTools.sparseMatrixRow r in mySparseMatrix.theMatrixInRows)
            {
                if (classLableClassPackageDic.ContainsKey(r.Lable))
                {
                    classLableClassPackageDic[r.Lable].AddRow(r);
                }
                else
                {
                    classLableClassPackageDic.Add(r.Lable, new ClassPackage());
                    classLableClassPackageDic[r.Lable].AddRow(r);
                }
            }

            //Compute the models, and remove putative classes
            List<int> classesToRemove = new List<int>();
            double totalEntries = 0;

            foreach (KeyValuePair<int, ClassPackage> kvp in classLableClassPackageDic)
            {
                if (kvp.Value.NumberOfEntries < minNumberOfEntriesForAClass || classesToEliminate.Contains(kvp.Key))
                {
                    //Not enough trainning examples
                    Console.WriteLine("Class:" + kvp.Key + "(" + kvp.Value.NumberOfEntries + ") " + "removing class from model.");
                    classesToRemove.Add(kvp.Key);
                    continue;
                }
                totalEntries += kvp.Value.NumberOfEntries;
                kvp.Value.ComputeMatrices(useRobustStatistics, saveMatrices2DiskForDebug);
            }

            foreach (int i in classesToRemove)
            {
                classLableClassPackageDic.Remove(i);
            }

            //Save the prior information
            foreach (KeyValuePair<int, ClassPackage> kvp in classLableClassPackageDic)
            {
                kvp.Value.Prior = kvp.Value.NumberOfEntries / totalEntries;
            }

            //SaveAverageAndSTDMatrix(DateTime.Now.Ticks.ToString() + ".txt");


            //Lets free some RAM
            if (brainWash)
            {
                mySparseMatrix.clearMatrix();
            }

        }
        //--------------------------------------
  
        /// <summary>
        /// Just returns the most probable class
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public int SimpleClassification(double[] point)
        {
            List<ClassScoreDictionary> classScoreDictionary = ClassificationEngine(point);
            classScoreDictionary.Sort((a, b) => b.Score.CompareTo(a.Score));
            return (classScoreDictionary[0].MyClass);

        }

        public int SimpleClassification(float[] point)
        {
            double[] pointFinal = point.Select(a => (double)a).ToArray();
            return (SimpleClassification(pointFinal));
        }



        /// <summary>
        /// Used for a single classification.  The class score dictionary is ordered by class number
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public List<ClassScoreDictionary> Classify(double[] point)
        {
            List<ClassScoreDictionary> classScoreDictionary = ClassificationEngine(point);

            classScoreDictionary.Sort((a, b) => b.MyClass.CompareTo(a.MyClass));
            return classScoreDictionary;
        }



        /// <summary>
        /// The class zero is used for the sole purpose of storing the total number of hypthesis.. 
        /// </summary>
        /// <param name="smTesting"></param>
        /// <param name="relaxationParameter"></param>
        /// <returns></returns>
        public Dictionary<int, ErrorComputation> EstimateErrorWithRelaxedBounds(SparseMatrix smTesting, double relaxationParameter, List<double> acceptableClasses)
        {

            //All the scoring is organized in this array
            List <ScoreOrganization> scoreOrganization = OrganizeScoreArray(relaxationParameter, smTesting, acceptableClasses);
            
            
            //-----------
            Dictionary<int, ErrorComputation> errorDictionary = new Dictionary<int, ErrorComputation>();

            ErrorComputation ecTotal = new ErrorComputation();
            ecTotal.Correct = 0;
            ecTotal.TotalInClass = 0;
            errorDictionary.Add(0, ecTotal);


            for (int j = 0; j < smTesting.theMatrixInRows.Count; j++)
            {

                List<int> predictedClasses = new List<int>();
                foreach (var v in scoreOrganization)
                {
                    if (v.RowIndex == j) { predictedClasses.Add(v.Class); }
                }

                errorDictionary[0].TotalInClass += predictedClasses.Count;

                foreach (int i in predictedClasses)
                {
                    if (!errorDictionary.ContainsKey(i))
                    {
                        ErrorComputation ec = new ErrorComputation();
                        ec.Correct = 0;
                        ec.TotalInClass = 0;
                        ec.FalsePositive = 0;
                        errorDictionary.Add(i, ec);
                    }

                }

                if (!errorDictionary.ContainsKey(smTesting.theMatrixInRows[j].Lable))
                {
                    ErrorComputation ec = new ErrorComputation();
                    ec.Correct = 0;
                    ec.FalsePositive = 0;
                    ec.TotalInClass = 0;
                    errorDictionary.Add(smTesting.theMatrixInRows[j].Lable, ec);
                }


                if (predictedClasses.Contains(smTesting.theMatrixInRows[j].Lable))
                {
                    errorDictionary[smTesting.theMatrixInRows[j].Lable].Correct++;

                    //keep track if this was not the first option of the classifier
                    int index = predictedClasses.IndexOf(smTesting.theMatrixInRows[j].Lable);
                    if (index > 0)
                    {
                        errorDictionary[0].Correct++;
                    }
                }

                //Now for the false positives
                foreach (var c in predictedClasses)
                {
                    if (c != smTesting.theMatrixInRows[j].Lable)
                    {
                        errorDictionary[c].FalsePositive++;
                    }
                }


                errorDictionary[smTesting.theMatrixInRows[j].Lable].TotalInClass++;

            }

            return (errorDictionary);
        }


        public class ErrorComputation
        {
            public double Correct { get; set; }
            public double TotalInClass { get; set; }
            public double FalsePositive { get; set; }


            public double Wrong
            {
                get
                {
                    return ((TotalInClass - Correct));
                }
            }

            public double ErrorPercentage
            {
                get { return ( (Wrong) / TotalInClass); }
            }

        }


        //----------------------------

        public int SimpleClassification (PatternTools.sparseMatrixRow r)
        {
            return (SimpleClassification(Unsparse(r)));
        }

        private double[] Unsparse(PatternTools.sparseMatrixRow r)
        {
            //Lets unsparse the point
            double[] point = new double[r.Dims.Count];

            for (int i = 0; i < point.Length; i++)
            {
                point[i] = r.Values[i];
            }

            return (point);
        }


        //--------------------------------

        private List<ClassScoreDictionary> ClassificationEngine(sparseMatrixRow r)
        {
            return ClassificationEngine(Unsparse(r));
        }

        private List<ClassScoreDictionary> ClassificationEngine(double[] point)
        {
            List<ClassScoreDictionary> classScoreDict = new List<ClassScoreDictionary>(classLableClassPackageDic.Keys.Count);

            foreach (KeyValuePair<int, ClassPackage> kvp in classLableClassPackageDic)
            {
                ClassScoreDictionary csd = new ClassScoreDictionary();
                csd.MyClass = kvp.Key;
                csd.Score = kvp.Value.Evaluate(point);
                classScoreDict.Add(csd);

            }

            return (classScoreDict);
        }


        public List<ScoreOrganization> OrganizeScoreArray(double relaxationParameter, SparseMatrix sMatrix, List<double> acceptableClasses)
        {
            List<ScoreOrganization> scoreOrganization = new List<ScoreOrganization>();

            for (int i = 0; i < sMatrix.theMatrixInRows.Count; i++)
            {
                //just for bizarre safety reasons
                if (sMatrix.theMatrixInRows[i].Dims.Count == 0) { continue; }

                List<ClassScoreDictionary> predictedClasses = ClassificationEngine(sMatrix.theMatrixInRows[i]);

                predictedClasses.Sort((a, b) => b.Score.CompareTo(a.Score));

                for (int k = 0; k < predictedClasses.Count; k++)
                {
                    if (acceptableClasses.Contains(predictedClasses[k].MyClass))
                    {
                        double normalScore = predictedClasses[0].Score - predictedClasses[k].Score;

                        scoreOrganization.Add(new ScoreOrganization(i, predictedClasses[k].MyClass, predictedClasses[k].Score, normalScore));
                    }
                }

            }

            scoreOrganization.Sort((a, b) => a.RankingScore.CompareTo(b.RankingScore));

            double totalValidHyps = (double) scoreOrganization.Count / (double)acceptableClasses.Count;
            int indexCutoff = (int)Math.Round(totalValidHyps * relaxationParameter);

            //And then eliminate possibilities beyond the fuzzy limit
            int elementsToCutoff = scoreOrganization.Count - indexCutoff;
            if (elementsToCutoff < 0) { elementsToCutoff = 0; } //Make sure the user enter a large relaxation parameter, we would still be fine
            scoreOrganization.RemoveRange(indexCutoff, scoreOrganization.Count - indexCutoff);

            return (scoreOrganization);

        }

        //An internal Classy
        public class ScoreOrganization
        {
            public int RowIndex { get; set; }
            public int Class { get; set; }
            //This is the output, effectively calculated score
            public double Score { get; set; }

            //This score is obtained from the above score
            public double RankingScore { get; set; }

            public ScoreOrganization (int rowIndex, int myClass, double score, double rankingScore) {
                RowIndex = rowIndex;
                Class = myClass;
                Score = score;
                RankingScore = rankingScore;
            }
        }



        //--------------------------

        /// <summary>
        /// This is good to give an idea on how the trainning went
        /// </summary>
        /// <param name="p"></param>
        public void SaveAverageAndSTDMatrix(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);

            foreach (KeyValuePair<int, ClassPackage> kvp in classLableClassPackageDic)
            {
                sw.WriteLine("Class: " + kvp.Key + "Prior: " + kvp.Value.Prior);
                List<double> meanVector = kvp.Value.MeanVector;
                List<double> stdVector = kvp.Value.StdVector;
                for (int i = 0; i < meanVector.Count; i++)
                {
                    sw.Write(meanVector[i] + "+-" + stdVector[i] + "\t");
                }
                sw.WriteLine("");
            }

            sw.Close();
        }


        /// <summary>
        /// The input vector cannot be sparse. Method returns 0 if class does not exist
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public double MahalanobisDistance(PatternTools.sparseMatrixRow row)
        {
            //Term 1 (xi - mi)Transposed
            //Term 2 Inverse Cov Matrix
            //Term 3 (xi - mi)
            //M Squared = Math.Sqrt (t1 * t2 * t3)

            //Eliminate unstable dims

            if (classLableClassPackageDic.ContainsKey(row.Lable))
            {

                Matrix t1 = new CSML.Matrix(row.Values.ToArray()) - classLableClassPackageDic[row.Lable].MatrixMeanVector;
                t1 = t1.Transpose();

                Matrix t2 = classLableClassPackageDic[row.Lable].InverseCovarianceMatrix;

                Matrix t3 = new CSML.Matrix(row.Values.ToArray()) - classLableClassPackageDic[row.Lable].MatrixMeanVector;

                Matrix product = t1 * t2 * t3;

                double det = Math.Abs(product.Determinant().Re);

                return (Math.Pow(det, 0.5));
            }
            else
            {
                return 0;
            }

        }
    }


}
