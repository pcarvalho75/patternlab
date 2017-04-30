using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepExplorer2.Result2
{
    [Serializable]
    public class AlignmentResult
    {
        public List<DeNovoR> DeNovoRegistries { get; set; }
        public int SimilarityScore { get; set; }
        public List<string> ProtIDs { get; set; }

        public AlignmentResult() { }

        public AlignmentResult(List<DeNovoR> deNovoRegistries, int similarityScore, List<string> protIDs)
        {
            DeNovoRegistries = deNovoRegistries;
            SimilarityScore = similarityScore;
            ProtIDs = protIDs;
        }
    }
}
