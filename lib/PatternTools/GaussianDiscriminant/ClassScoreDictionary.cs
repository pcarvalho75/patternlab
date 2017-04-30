using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.GaussianDiscriminant
{
    /// <summary>
    /// This class is used as a result for the Gaussian Discriminant Function
    /// </summary>
    public struct ClassScoreDictionary
    {
        public int MyClass { get; set; }
        public double Score { get; set; }
    }
}
