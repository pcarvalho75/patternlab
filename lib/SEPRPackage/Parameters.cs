using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using PatternTools.PTMMods;

namespace SEPRPackage
{
    [Serializable]
    public struct Parameters
    {

        public string LabeledDecoyTag { get; set; }
        public string UnlabeledDecoyTag { get; set; }
        
        //Simmilar ptns
        public bool EliminateInPtns { get; set; }
        public int GroupPtnsWithXCommonPeptides { get; set; }

        public bool GroupByNoEnzymaticTermini { get; set; }
        public bool GroupByChargeState { get; set; }
        public PatternTools.Enzyme MyEnzime { get; set; }

        public double SpectraFDR { get; set; }
        public double PeptideFDR { get; set; }
        public double ProteinFDR { get; set; }
        public bool CompositeScorePrimaryScore { get; set; }
        public bool CompositeScoreSecondaryScore { get; set; }
        public bool CompositeScoreDeltaCN { get; set; }
        public bool CompositeScorePeaksMatched { get; set; }
        public bool CompositeScoreDeltaMassPPM { get; set; }
        public bool CompositScoreDigestion { get; set; }
        public bool CompositeScorePresence { get; set; }
        public bool CompositeScoreSecondaryRank { get; set; }
        
        public string AlternativeProteinDB { get; set; }
        public bool ConsiderOnlyFirstDBLocus { get; set; }

        //Quality Filters
        public bool SequenceFiltersEnabled { get; set; }
        public double DeltaMassPPMPostProcessing { get; set; }
        public double MS2PPM { get; set; }
        public List<ModificationItem> PTMModifications  { get; set; }
        //public PatternTools.PTMMods.Modification SeqFilterStaticMod { get; set; }
        //public PatternTools.PTMMods.Modification SeqFilterVariableMod { get; set; }
        public int SeqQFilterACount { get; set; }
        public int SeqQFilterBCount { get; set; }
        public int SeqQFilterCCount { get; set; }
        public int SeqQFilterXCount { get; set; }
        public int SeqQFilterYCount { get; set; }
        public int SeqQFilterZCount { get; set; }
        public int SeqQFilterCAXCount { get; set; }
        public int SeqQFilterCBYCount { get; set; }
        public int SeqQFilterCCZCount { get; set; }
        public double SeqQFilterRelativeIntensityThreshold { get; set; }

        public int QFilterMinSequenceLength { get; set; }
        public bool QFilterDiscardChargeOneMS { get; set; }
        public bool QFilterDeltaMassPPM { get; set; }
        public double QFilterDeltaMassPPMValue { get; set; }
        public bool QFilterPrimaryScore { get; set; }
        public double QFilterPrimaryScoreValue { get; set; }
        public bool QFilterSecondaryScore { get; set; }
        public double QFilterSecondaryScoreValue { get; set; }
        public int QFilterMinNoEnzymaticTermini { get; set; }
        public bool QFilterDiscardProteinsWithNoUniquePeptides { get; set; }
        public int QFilterProteinsMinNoPeptides { get; set; }
        public bool QFilterMahalanobisDistance { get; set; }
        public double QFilterMahalanobisDistanceValue { get; set; }
        public int QFilterMinSpecCount { get; set; }
        public int QFilterMinNoStates { get; set; }
        public double QFilterMinPrimaryScoreForOneHitWonders { get; set; }
        public double QFilterMinPrimaryScoreForNonOneHitWonders { get; set; }

        public void LoadDefaults ( ) {


        }

        // Load or save to file
        /// <summary>
        /// Load a serialized instance of the param object
        /// </summary>
        /// <param name="file"></param>
        public void LoadFromFile(string file)
        {
            //Serialize it!
            System.IO.FileStream flStream = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();

            this = (Parameters)bf.Deserialize(flStream);
            flStream.Close();
        }

        /// <summary>
        /// Save a serialized instance of the object
        /// </summary>
        /// <param name="file"></param>
        public void saveToFile(string file)
        {
            //Serialize it!
            System.IO.FileStream flStream = new System.IO.FileStream(file, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(flStream, this);
            flStream.Close();
        }




        public string SeachResultDirectoy { get; set; }

        public bool FormsPriorForPeptides { get; set; }

        public double QFilterDeltaCNMin { get; set; }

        public bool QFilterDeltaCN { get; set; }
    }
}
