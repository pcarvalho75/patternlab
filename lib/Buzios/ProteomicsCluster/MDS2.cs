using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PatternTools;

namespace Buzios
{
    class MDS2
    {
        public MatrixData matrix;
        List<sparseMatrixRow> centroids;
        SparseMatrix originalSparseMatrix = new SparseMatrix();

        public MatrixData Matrix
        {
            get { return matrix; }
        }

        public SparseMatrix Converge(int iterations, double outlierPenalty)
        {

            List<double> stressValues = new List<double>(iterations);

            double minStress = double.MaxValue;
            List<OrganismData> theResult = new List<OrganismData>();

            if (outlierPenalty != 0)
            {
                CalculateWheights();
            }
            else
            {
                ResetWheights();
            }

            for (int i = 0; i < iterations; i++)
            {
                double stress = SpringConverge(outlierPenalty);
                stressValues.Add(stress);

                if (stress < minStress)
                {
                    minStress = stress;
                    theResult = PatternTools.ObjectCopier.Clone(Matrix.Positions);
                }

                Randomize();
            }

            SparseMatrix resultMatrix = new SparseMatrix();
            resultMatrix.ClassDescriptionDictionary = originalSparseMatrix.ClassDescriptionDictionary;

            foreach (OrganismData o in theResult)
            {
                sparseMatrixRow smr = new sparseMatrixRow(o.Label, new List<int>() { 1, 2 }, new List<double>() { o.PosX, o.PosY });
                smr.FileName = o.Name;
                resultMatrix.addRow(smr);

            }


            return resultMatrix;
        }

        private double SpringConverge(double outlierPenalty)
        {
            int noIterations = 500;

            int counter0 = 0;
            double lastStress = double.MaxValue;
            while (true)
            {
                if (counter0++ > noIterations)
                {
                    break;
                }

                double stress = OneCycleSimple((decimal)outlierPenalty);

                if (stress == lastStress)
                {
                    break;
                }

                lastStress = stress;
            }

            Console.WriteLine("Finalized.  Interations: " + counter0);

            return lastStress;
        }


        public MDS2 (SparseMatrix sm)
        {
            //Construct a distance matrix
            StringBuilder distanceMatrix = new StringBuilder();
            foreach (sparseMatrixRow sr in sm.theMatrixInRows)
            {
                distanceMatrix.Append("," + sr.FileName);
            }
            distanceMatrix.Append("\n");

            Dictionary<string, int> fileLabelDictionary = new Dictionary<string, int>();
            for (int i = 0; i < sm.theMatrixInRows.Count; i++)
            {
                distanceMatrix.Append(sm.theMatrixInRows[i].FileName);

                if (fileLabelDictionary.ContainsKey(sm.theMatrixInRows[i].FileName))
                {
                    throw new Exception("The file " + sm.theMatrixInRows[i].FileName + " is found in more than one directory.");
                }
                fileLabelDictionary.Add(sm.theMatrixInRows[i].FileName, sm.theMatrixInRows[i].Lable);

                for (int j = 0; j < sm.theMatrixInRows.Count; j++)
                {
                    double p = PatternTools.pTools.DotProduct(sm.theMatrixInRows[i].Values, sm.theMatrixInRows[j].Values);
                    distanceMatrix.Append("," + Math.Round(p, 3) * 100);
                }

                distanceMatrix.Append("\n");

            }

            //----------------------------------------------------
            MatrixData md = new MatrixData(distanceMatrix.ToString(), fileLabelDictionary);


            matrix = md;
            Randomize();
            originalSparseMatrix = sm;

            //Calculate centroids
            List<int> labels = sm.ExtractLabels();
            centroids = new List<sparseMatrixRow>(labels.Count);
            foreach (int l in labels)
            {
                sparseMatrixRow centroid = new sparseMatrixRow(l);
                List<sparseMatrixRow> rows = sm.theMatrixInRows.FindAll(a => a.Lable == l);
                centroid.Dims = rows[0].Dims;
                double[] e5 = new double[rows[0].Dims.Count];
                centroid.Values = e5.ToList();

                double sum = 0;
                for (int i = 0; i < centroid.Dims.Count; i++)
                {
                    for (int j = 0; j < rows.Count; j++)
                    {
                        sum += rows[j].Values[i];
                    }
                    centroid.Values[i] = sum / (double)rows.Count;
                }
                centroids.Add(centroid);
            }
        }

        public double OneCycleSimple(decimal wheightValue)
        {
            double totalError = 0;
            Parallel.For(0, matrix.Positions.Count, i =>
            //for (int i = 0; i < matrix.Positions.Count; i++)
            {
                OrganismData od1 = matrix.Positions[i];
                double error = CalculateStress(od1, od1.PosX, od1.PosY, wheightValue);

                totalError += error;

                if (CalculateStress(od1, od1.PosX - 1, od1.PosY, wheightValue) < error)
                {
                    od1.PosX--;
                }
                else if (CalculateStress(od1, od1.PosX + 1, od1.PosY, wheightValue) < error)
                {
                    od1.PosX++;
                }

                if (CalculateStress(od1, od1.PosX, od1.PosY - 1, wheightValue) < error)
                {
                    od1.PosY--;
                }
                else if (CalculateStress(od1, od1.PosX, od1.PosY + 1, wheightValue) < error)
                {
                    od1.PosY++;
                }

            }
            );
        

            return totalError;
            
        }


        private double CalculateStress(OrganismData od, double x, double y, decimal weightValue)
        {
            double errorRMS = 0;

            int org = matrix.Positions.IndexOf(od);

            for (int i = 0; i < matrix.Positions.Count; i++)
            {
                OrganismData theOrg = matrix.Positions[i];

                List<double> v = new List<double>() {theOrg.PosX - x, theOrg.PosY - y};

                errorRMS += Math.Pow(PatternTools.pTools.Norm(v) - matrix.SpringSizeMatrix[org, i], 2) * Math.Pow(od.Weight * theOrg.Weight, (double)weightValue);
            }

            return errorRMS;

        }

        internal void Randomize()
        {
            //Add random positions           

            foreach (OrganismData o in matrix.Positions)
            {
                double rX = PatternTools.pTools.getRandomNumber(100); ;
                double rY = PatternTools.pTools.getRandomNumber(100); ;
                o.PosY = rY;
                o.PosX = rX;
            }
        }

        internal void ResetWheights()
        {
            foreach (OrganismData o in matrix.Positions)
            {
                o.Weight = 1;
            }
        }

        internal void CalculateWheights()
        {
            List<int> labels = originalSparseMatrix.ExtractLabels();

            foreach (int label in labels)
            {
                List<sparseMatrixRow> rows = originalSparseMatrix.theMatrixInRows.FindAll(a => a.Lable == label);
                sparseMatrixRow centroid = centroids.Find(a => a.Lable == label);

                rows.Sort((a, b) => pTools.DotProduct(b.Values, centroid.Values).CompareTo(pTools.DotProduct(a.Values, centroid.Values)));


                for (int i = 0; i < rows.Count; i++)
                {
                    OrganismData o = matrix.Positions.Find(a => a.Name.Equals(rows[i].FileName));
                    o.Weight = 1 / (double)(1 + i);
                }

            }
        }

    }
}
