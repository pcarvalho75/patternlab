using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEProQ.Acetylation
{
    class ReportItem
    {
        public string FileName { get; set; }
        public int ScanNumber {get; set;}
        public int ChargeState {get; set;}
        public double SignalMedium { get; set; }
        public double SignalHeavy {get; set;}
        public double Ratio { get; set; }
        public string Sequence { get; set; }

        public ReportItem(string fileName, int scanNumber, int chargeState, double signalMedium, double signalHeavy, double ratio, string sequence)
        {
            FileName = fileName;
            ScanNumber = scanNumber;
            ChargeState = chargeState;
            SignalMedium = signalMedium;
            SignalHeavy = signalHeavy;
            Ratio = ratio;
            Sequence = sequence;
        }
    }
}
