using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools
{
    public interface IPLDiscriminator
    {
        int SimpleClassification(double[] point);

        int SimpleClassification(float[] point);
    }
}
