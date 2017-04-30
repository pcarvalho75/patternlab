using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.MSParser
{
    /// <summary>
    /// Used for the search Engine Class
    /// </summary>
    [Serializable]
    public class Identification
    {
        double score;
        int chargeState;
        object pepChop;

        public double Score { get { return score; } }
        public object PepChop { get { return pepChop; } }
        public int ChargeState {get {return chargeState;}}


        public Identification(double score, object pepChop, int chargeState)
        {
            this.score = score;
            this.pepChop = pepChop;
            this.chargeState = chargeState;
        }

    }
}
