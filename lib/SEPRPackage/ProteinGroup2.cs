using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPRPackage
{
    public class ProteinGroup2
    {
        public List<MyProtein> MyProteins { get; set; }
        public List<string> MyPeptides { get; set; }
        public ProteinGroupType MyGroupType { get; set; }


        public ProteinGroup2(MyProtein prot)
        {
            MyProteins = new List<MyProtein>() { prot };
            MyPeptides = prot.DistinctPeptides;
        }

        public ProteinGroup2(List<MyProtein> myProteins)
        {
            MyProteins = myProteins;

            MyPeptides = (from prot in MyProteins
                          from pep in prot.DistinctPeptides
                          select pep).Distinct().ToList();
        }

        public void AssignGroupTypesToProteins ()
        {
            if (MyProteins.Count == 1)
            {
                if (MyProteins[0].PeptideResults.Exists(a => a.MyMapableProteins.Count == 1))
                {
                    MyGroupType = ProteinGroupType.Unique;
                }
                else
                {
                    MyGroupType = ProteinGroupType.Single;
                }
            }
            else
            {
                foreach (MyProtein p in MyProteins)
                {
                    if (p.DistinctPeptides.Count != MyPeptides.Count)
                    {
                        MyGroupType = ProteinGroupType.Some;
                        break;
                    }

                }

                if (MyGroupType != ProteinGroupType.Some)
                {
                    MyGroupType = ProteinGroupType.All;
                }
            }

            foreach (MyProtein p in MyProteins)
            {
                p.MyGroupType = MyGroupType;
            }
        }
    }
}
