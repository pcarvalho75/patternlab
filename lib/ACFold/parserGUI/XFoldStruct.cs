using System;
using System.Collections.Generic;
using System.Text;

namespace parserGUI
{
    public class XFoldStruct
    {
        public double FoldChange { get; set; }
        public double pValue { get; set; }
        public double QuantitationPositiveClass { get; set; }
        public double QuantitationNegativeClass { get; set; }

        /// <summary>
        /// Our protein index from the index file
        /// </summary>
        public int GPI { get; set; }

        /// <summary>
        ///blue = 0, orange = 1, green = 2, red = 3
        /// </summary>
        public int Color { get; set; }

        /// <summary>
        /// Returns the ABS of the real fold change; a -0.5 will become -2
        /// </summary>
        /// <returns></returns>
        public double CorrectedFold()
        {
            if (FoldChange < 1)
            {
                return ((1 / FoldChange) * (-1));
            }
            else
            {
                return FoldChange;
            }

        }
        public double ABSCorrectedFold()
        {
            if (QuantitationNegativeClass < QuantitationPositiveClass && FoldChange < 1)
            {
                return (Math.Abs((1 / FoldChange)));
            }
            else
            {
                return Math.Abs(FoldChange);
            }

        }
    }
}
