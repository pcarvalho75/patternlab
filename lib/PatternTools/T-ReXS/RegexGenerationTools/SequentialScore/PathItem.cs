using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatternTools.TReXS.RegexGenerationTools;

namespace PatternTools.T_ReXS.RegexGenerationTools.SequentialScore
{
    public class PathItem
    {
        public List<GraphNode> MyDownPath { get; set; }
        public double DownIntensity { get; set; }

        public PathItem(List<GraphNode> theDownPath)
        {
            MyDownPath = theDownPath;
            DownIntensity = theDownPath.Sum(a => a.Intensity);
        }
    }
}
