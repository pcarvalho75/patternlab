using System;
using System.Collections.Generic;
using System.Text;

namespace parserGUI
{
    class MultiClassItem
    {
        public MultiClassItem(int GPI, double [] blueDotArray)
        {
            this.GPI = GPI;
            this.blueDotArray = blueDotArray;
            this.ClusterNumber = -1;
            this.ClusterNumber = -1;
        }

        public int ClusterNumber { get; set; }
        public double ClusterDistance { get; set; }

        public int Score
        {
            get {
                int score = 0;
                for (int i = 0; i < blueDotArray.Length; i++) {
                    if (blueDotArray[i] != 0)
                    {
                        score++;
                    }
                }
                return (score);
                }
        }

        public int GPI { get; set; }
        public double[] blueDotArray { get; set; }
    }
}
