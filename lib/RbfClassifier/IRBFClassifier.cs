using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RbfClassifier
{
    public interface IRBFClassifier
    {
        int ClassifyByLabel(List<double> x, out double gx);
        double Classify(List<double> x);
    }
}
