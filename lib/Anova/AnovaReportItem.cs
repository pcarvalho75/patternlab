using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anova
{
    public class AnovaReportItem
    {
        public string ID {get; set;}
        public string Description {get; set;}
        public int DegreeOfFreedomBetweenGroups {get; set;}
        public int DegreesOfFredomTotal {get; set;}
        public int DegreeOfFreedomWithinGroups {get; set;}
        public double MeanSquareVarianceBetweenGroups {get; set;}
        public double MeanSquareVarianceWithinGroups {get; set;}
        public double SumOfSquaresBetweenGroups {get; set;}
        public double SumOfSquaresTotal {get; set;}
        public double SumOfSquaresWithingGroups {get; set;}                
        public double FCritical {get; set;}
        public double FRatio {get; set;}
    }
}
