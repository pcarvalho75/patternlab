/*
 * Program:     XAligner 
 * Author:      Marlon Dias Mariano dos Santos and Paulo Costa Carvalho
 * Update:      19/12/2012
 * Update by:   Marlon Dias Mariano dos Santos
 * Description: Aligner
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MAligner
{
    public class Aligners
    {
        //int ScoreTraceBack = 0;
        int gap = 0;
        int gapextension = 0;

        //int[,] positionMatrix;
        

        //List<int> tracePositionYSL = new List<int>();
        //List<int> tracePositionXSL = new List<int>();

        //List<int> tracePositionYID = new List<int>();
        //List<int> tracePositionXID = new List<int>();
        int[,] substitutionMatrix;

        Dictionary<char, int> subsMatrixScores;

  

        public Aligners(int Gap, int GapExtension, string substitutionMatrixPath)
        {
            gap = Gap;
            gapextension = GapExtension;
             KeyValuePair<int[,], Dictionary<char, int>> kvp =  Utils.LoadSubstitutionMatrix(substitutionMatrixPath);
             substitutionMatrix = kvp.Key;
             subsMatrixScores = kvp.Value;
        }


        //public int[,] AlignmentNeedlemanWush(List<char> seq1, List<char> seq2)
        //{

        //    //BuildMatrix
        //    int[,] alignmentMatrix = new int[seq1.Count + 1, seq2.Count + 1];

        //    for (int xn = 1; xn <= seq1.Count; xn++)
        //    {
        //        alignmentMatrix[xn, 0] = alignmentMatrix[xn - 1, 0] + gap;
        //    }

        //    for (int yn = 1; yn <= seq2.Count; yn++)
        //    {
        //        alignmentMatrix[0, yn] = alignmentMatrix[0, yn - 1] + gap;
        //    }

        //    for (int y = 1; y < seq2.Count + 1; y++)
        //    {
        //        for (int x = 1; x < seq1.Count + 1; x++)
        //        {
        //            List<int> score = new List<int>();
        //            int siti = 0;

        //            siti = substitutionMatrix[subsMatrixScores[seq1[x - 1]], subsMatrixScores[seq2[y - 1]]];

        //            if (seq1[x - 1] == seq2[y - 1])
        //            {
        //                score.Add(alignmentMatrix[x - 1, y - 1] + siti);
        //            }
        //            else
        //            {
        //                score.Add(alignmentMatrix[x - 1, y - 1] + siti);
        //                score.Add(alignmentMatrix[x - 1, y] + siti);
        //                score.Add(alignmentMatrix[x, y - 1] + siti);
        //            }

        //            alignmentMatrix[x, y] = score.Max();
        //        }
        //    }

        //    return alignmentMatrix;

        //}

        //public List<int> TraceBackNeedlemanWush(int[,] matrix)
        //{
            
        //    int num = int.MinValue;
        //    int xpos = matrix.GetLength(0) -1;
        //    int ypos = matrix.GetLength(1) -1;

        //    double currentValue = num;
        //    int currentX = xpos;
        //    int currentY = ypos;

        //    positionMatrix = new int[currentX, currentY];

        //    List<int> tracebackY = new List<int>(ypos);
        //    List<int> tracebackX = new List<int>(xpos);
        //    List<int> tracebackValues = new List<int>();

        //    tracebackX.Add(currentX);
        //    tracebackY.Add(currentY);
            
        //    tracebackValues.Add(matrix[currentX, currentY]);

        //    int maxValue = int.MinValue;

        //    while ((currentX > 0) && (currentY > 0))
        //    {
        //        int vD = matrix[currentX - 1, currentY - 1];// Diagonal  
        //        int vU = matrix[currentX, currentY - 1];// Up exclusion
        //        int vL = matrix[currentX - 1, currentY];// Left insertion
            
        //        int newPosX = -1;
        //        int newPosY = -1;

        //        int currentVal = matrix[currentX, currentY];

        //        maxValue = int.MinValue;

        //        if (vU > maxValue)
        //        {
        //            maxValue = vU;
        //            newPosX = currentX;
        //            newPosY = currentY - 1;
        //        }

        //        if (vL > maxValue)
        //        {
        //            maxValue = vL;
        //            newPosX = currentX - 1;
        //            newPosY = currentY;
        //        }

        //        if (vD >= maxValue)
        //        {
        //            maxValue = vD;
        //            newPosX = currentX - 1;
        //            newPosY = currentY - 1;
        //        }

        //        ScoreTraceBack = matrix[newPosX, newPosY] + ScoreTraceBack;
        //        tracePositionXSL.Add(currentX);
        //        tracePositionYSL.Add(currentY);

        //        tracePositionXID.Add(currentX);
        //        tracePositionYID.Add(currentY);

        //        tracebackX.Add(newPosX);
        //        tracebackY.Add(newPosY);
        //        tracebackValues.Add(matrix[newPosX, newPosY]);


        //        currentX = newPosX;
        //        currentY = newPosY;

        //        if (xpos == 0 && ypos == 0)
        //        {
        //            break;
        //        }

        //    }

        //    Console.WriteLine("TraceBack Score: " + ScoreTraceBack);
        //    return tracebackValues;

        //}

        //public int SimilarityScore(List<char> seq1, List<char> seq2, List<int> traceBack)
        //{
        //    List<int> SLtracePositionY = new List<int>();
        //    List<int> SLtracePositionX = new List<int>();
        //    List<int> SLtraceBack = new List<int>();

        //    int SLscore = 0;

        //    SLtracePositionX = tracePositionXSL;
        //    SLtracePositionX.Reverse();

        //    SLtracePositionY = tracePositionYSL;
        //    SLtracePositionY.Reverse();

        //    SLtraceBack = traceBack;
        //    SLtraceBack.Reverse();
        //    SLtraceBack.RemoveAt(0);

        //    char[] SLseq1 = new char[SLtracePositionX.Count];
        //    char[] SLseq2 = new char[SLtracePositionY.Count];

        //    for (int i = 0; i < SLtracePositionX.Count - 1; i++)
        //    {
        //        SLseq1[i] = seq1[SLtracePositionX[i]];
        //    }

        //    for (int i = 0; i < SLtracePositionY.Count - 1; i++)
        //    {
        //        SLseq2[i] = seq2[SLtracePositionY[i] - 1];
        //    }
    
        //    for (int i = 0; i < SLtraceBack.Count; i++)
        //    {
        //        if (SLseq1[i] != SLseq2[i])
        //        {
        //            SLscore = SLtraceBack[i] + SLscore;
        //        }
        //    }

        //    Console.WriteLine("Slscore: " + SLscore);
        //    return SLscore;
        //}

        //public int IdentitylScore(List<char> seq1, List<char> seq2, List<int> traceBack)
        //{
        //    List<int> IDtracePositionY = new List<int>();
        //    List<int> IDtracePositionX = new List<int>();
        //    List<int> IDtraceBack = new List<int>();

        //    int IDscore = 0;

        //    IDtracePositionX = tracePositionXID;
        //    IDtracePositionX.Reverse();

        //    IDtracePositionY = tracePositionYID;
        //    IDtracePositionY.Reverse();

        //    IDtraceBack = traceBack;
        //    IDtraceBack.Reverse();
        //    IDtraceBack.RemoveAt(0);

        //    char[] IDseq1 = new char[IDtracePositionX.Count];
        //    char[] IDseq2 = new char[IDtracePositionY.Count];

        //    for (int i = 0; i < IDtracePositionX.Count - 1; i++)
        //    {
        //        IDseq1[i] = seq1[IDtracePositionX[i]];
        //    }

        //    for (int i = 0; i < IDtracePositionY.Count - 1; i++)
        //    {
        //        IDseq2[i] = seq2[IDtracePositionY[i] - 1];
        //    }
    
        //   for (int i = 0; i < IDtraceBack.Count; i++)
        //   {
        //       if (IDseq1[i] == IDseq2[i])
        //       {
        //           IDscore = IDtraceBack[i] + IDscore;
        //       }
        //       //Console.WriteLine(IDseq1[i] + "-" + IDseq2[i]);
        //   }
           
        //   Console.WriteLine("IDscore: " + IDscore);
        //   return IDscore;
        //}

        //-----------------------------------------------------------------------SmithWaterman--------------------------------------------------------------------------------------//

        //public int[,] AlignmenSmithWaterman(List<char> seq1, List<char> seq2)
        //{
        //    //BuildMatrix
        //    int[,] alignmentMatrix = new int[seq1.Count + 1, seq2.Count + 1];

        //    for (int y = 1; y < seq2.Count + 1; y++)
        //    {
        //        for (int x = 1; x < seq1.Count + 1; x++)
        //        {

        //            List<int> score = new List<int>();
        //            int siti = 0;

        //            if (seq1[x - 1] == seq2[y - 1])
        //            {
        //                siti = gap;
        //                score.Add(alignmentMatrix[x - 1, y - 1] + siti);
        //            }
        //            score.Add(alignmentMatrix[x - 1, y - 1] + gap);
        //            score.Add(alignmentMatrix[x - 1, y] + gap);
        //            score.Add(alignmentMatrix[x, y - 1] + gap);

        //            alignmentMatrix[x, y] = score.Max();
        //        }
        //    }
        //        return alignmentMatrix;
        //}

        //public List<int> TraceBackSmithWaterman(int[,] matrix)
        //{

        //    int num = int.MinValue;
        //    int xpos = 0;
        //    int ypos = 0;

        //    for (int x = 0; x < matrix.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < matrix.GetLength(1); y++)
        //        {

        //            if (matrix[x, y] > num)
        //            {
        //                num = matrix[x, y];
        //                xpos = x;
        //                ypos = y;
        //            }

        //        }

        //    }

        //    double currentValue = num;
        //    int currentX = xpos;
        //    int currentY = ypos;
        //    List<int> tracebackY = new List<int>();
        //    List<int> tracebackX = new List<int>();
        //    List<int> tracebackValues = new List<int>();

        //    tracebackX.Add(currentX);
        //    tracebackY.Add(currentY);
        //    tracebackValues.Add(matrix[currentX, currentY]);

        //    int maxValue = -1;
        //    while (maxValue != 0)
        //    {

        //        int vD = matrix[currentX - 1, currentY - 1];
        //        int vU = matrix[currentX, currentY - 1];
        //        int vL = matrix[currentX - 1, currentY];

        //        int newPosX = -1;
        //        int newPosY = -1;

        //        int currentVal = matrix[currentX, currentY];

        //        maxValue = 0;

        //        if (vL > maxValue)
        //        {
        //            maxValue = vL;
        //            newPosX = currentX - 1;
        //            newPosY = currentY;
        //        }

        //        if (vU > maxValue)
        //        {
        //            maxValue = vU;
        //            newPosX = currentX;
        //            newPosY = currentY - 1;
        //        }

        //        if (vD >= maxValue)
        //        {
        //            newPosX = currentX - 1;
        //            newPosY = currentY - 1;
        //        }

        //        tracebackX.Add(newPosX);
        //        tracebackY.Add(newPosY);
        //        tracebackValues.Add(matrix[newPosX, newPosY]);

        //        currentX = newPosX;
        //        currentY = newPosY;

        //    }

        //    return tracebackValues;

        //}

        ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static void PrintAlingmentMatrix(int[,] matrix, List<char> seq1, List<char> seq2, string fileName = null)
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                if (x == 0)
                {
                    sb.Append("\t\t\tX(0)=-");
                }
                else
                {
                    sb.Append("\tX(" + x + ")=" + seq1[x - 1]);
                }
            }
            sb.AppendLine();

            int rowCounter = 0;
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    if (x == 0)
                    {
                        if (y == 0)
                        {
                            sb.Append("\tY(0)=-");
                        }
                        else
                        {
                            sb.Append("\tY(" + rowCounter + ")=" + seq2[y - 1]);
                        }
                        rowCounter++;
                    }

                    sb.Append("\t" + matrix[x, y] + "\t");
                }

                sb.Append("\n");
            }

            if (fileName == null)
            {
                Console.Write(sb.ToString());
            }
            else
            {
                StreamWriter sw = new StreamWriter(fileName);
                sw.Write(sb.ToString());
                sw.Close();
            }
        }
    }
}
