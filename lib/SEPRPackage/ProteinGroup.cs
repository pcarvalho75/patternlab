using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPRPackage
{
    /// <summary>
    /// Used for clustering proteins with common peptides
    /// </summary>
    public class ProteinGroup
    {

        public List<MyProtein> MyProteins { get; set; }
        public ProteinGroupType MyGroupType { get; set; }

        public List<string> DistinctPeptides
        {
            get
            {
                List<string> dPeptides = (from p in MyProteins
                                          from pep in p.DistinctPeptides
                                          select pep).Distinct().ToList();

                return dPeptides;
            }
        }

        public ProteinGroup(List<MyProtein> theProteins)
        {
            MyProteins = theProteins;
        }

        public void AssignGroupTypesToProteins()
        {
            //Determine what type of group this is
            foreach (MyProtein p in MyProteins)
            {
                p.MyGroupType = MyGroupType;
            }
        }

        internal bool HasCommonProtein(List<MyProtein> list, int minNoPeptides)
        {
            foreach (MyProtein externalProtein in list)
            {
                int index = MyProteins.FindIndex(a => a.DistinctPeptides.Intersect(externalProtein.DistinctPeptides).Count() > minNoPeptides);
                if (index > -1)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
