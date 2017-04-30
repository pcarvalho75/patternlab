using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Buzios
{
    public class MatrixData
    {
        public double[,] SpringSizeMatrix { get; set; }
        public List<OrganismData> Positions { get; set; }
        public double[,] ElasticConstants { get; set; }

        double minDistance = double.MaxValue;
        double maxDistance = double.MinValue;

        public MatrixData(string theMatrix, Dictionary<string, int> fileLabelDictionary)
        {
            string[] lines = Regex.Split(theMatrix, "\n");

            string[] labels = new string[0];

            int lineCounter = 0;
            SpringSizeMatrix = new double[0, 0];
            ElasticConstants = new double[0, 0];

            for (int l = 0; l < lines.Length; l++)
            {
                string[] cols = Regex.Split(lines[l], ",");
                if (lineCounter == 0)
                {
                    labels = new string[cols.Length - 1];
                    for (int i = 1; i < cols.Length; i++)
                    {
                        labels[i - 1] = cols[i];
                    }
                    SpringSizeMatrix = new double[cols.Length - 1, cols.Length - 1];
                    ElasticConstants = new double[cols.Length - 1, cols.Length - 1];
                }
                else
                {
                    for (int i = 1; i < cols.Length; i++)
                    {
                        double size = Math.Abs(100 - double.Parse(cols[i]));
                        SpringSizeMatrix[lineCounter - 1, i - 1] = size;

                        if (size > maxDistance) { maxDistance = size; }
                        if (size < minDistance) { minDistance = size; }

                        ElasticConstants[lineCounter - 1, i - 1] = 1;
                    }
                }
                lineCounter++;
            }


            Positions = new List<OrganismData>(labels.Length);
            for (int i = 0; i < labels.Length; i++)
            {
                OrganismData o = new OrganismData(labels[i], i, fileLabelDictionary[labels[i]]);
                Positions.Add(o);
            }

        }

        //public MatrixData(StreamReader matrixFile)
        //{
        //    string line;
        //    string[] labels = new string[0];

        //    int lineCounter = 0;
        //    SpringSizeMatrix = new double[0, 0];
        //    ElasticConstants = new double[0, 0];

        //    while ((line = matrixFile.ReadLine()) != null)
        //    {
        //        string[] cols = Regex.Split(line, ",");
        //        if (lineCounter == 0)
        //        {
        //            labels = new string[cols.Length - 1];
        //            for (int i = 1; i < cols.Length; i++)
        //            {
        //                labels[i - 1] = cols[i];
        //            }
        //            SpringSizeMatrix = new double[cols.Length - 1, cols.Length - 1];
        //            ElasticConstants = new double[cols.Length - 1, cols.Length - 1];
        //        }
        //        else
        //        {
        //            for (int i = 1; i < cols.Length; i++)
        //            {
        //                SpringSizeMatrix[lineCounter - 1, i - 1] = Math.Abs(100 - double.Parse(cols[i]));
        //                ElasticConstants[lineCounter - 1, i - 1] = 1;
        //            }
        //        }
        //        lineCounter++;
        //    }

        //    matrixFile.Close();

        //    Positions = new List<OrganismData>(labels.Length);
        //    for (int i = 0; i < labels.Length; i++)
        //    {
        //        Positions.Add(new OrganismData(labels[i], i));
        //    }

        //}
    }
}
