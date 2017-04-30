using PatternTools;
using System;
using System.Collections.Generic;
using System.Text;


namespace TrendQuest.ClusteringItems
{
    public class SpectralNode
    {
 
        //Creates a new cluster node from two input vectors
        List<PatternTools.sparseMatrixRow> myInputVectors = new List<sparseMatrixRow>(30);

        sparseMatrixRow myClusterRepresentation;
        public int IndexOfFormerMatrix { get; set; }

        public List<PatternTools.sparseMatrixRow> MyInputVectors
        {
            get { return myInputVectors; }
        }

        public sparseMatrixRow MyClusterRepresentationInputVector
        {
            get { return myClusterRepresentation; }
            set { myClusterRepresentation = value; }
        }
        

        //Constructors -----------------------------------

        public SpectralNode(PatternTools.sparseMatrixRow v1, PatternTools.sparseMatrixRow v2)
        {
            myInputVectors.Add(v1);
            myInputVectors.Add(v2);
            myClusterRepresentation = PatternTools.pTools.MergeTwoInputVectors(v1, 1, v2, 1, true);
        }

        public SpectralNode(PatternTools.sparseMatrixRow v1)
        {
            myClusterRepresentation = PatternTools.pTools.MergeTwoInputVectors(v1, 1, v1, 1, false); ;
            MyInputVectors.Add(v1);
        }

        public SpectralNode()
        {

        }

        //-------------------------------------------------

        public double [] Consensus()
        {
            List<int> aDimVector = MyInputVectors[0].Dims;

            double[] consensus = new double[aDimVector.Count];
            foreach (sparseMatrixRow smr in MyInputVectors)
            {
                //We should make a clone
                sparseMatrixRow cv = new sparseMatrixRow(0);

                foreach (double v in smr.Values)
                {
                    cv.Values.Add(v);
                }

                cv.ConvertToUnitVector();

                for (int i = 0; i < cv.Values.Count; i++)
                {
                    consensus[i] += cv.Values[i];
                }

            }

            for (int i = 0; i < consensus.Length; i++)
            {
                consensus[i] /= (double)MyInputVectors.Count;
            }

            return consensus;
        }


      
    }
}
