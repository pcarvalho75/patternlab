using PatternTools.FastaParser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XQuant;
using XQuant.Quants;

namespace XQuantPairWiseAnalyzer.PairAnalyzer
{
    [Serializable]
    public class Core20Paired
    {
        public Core35 core;

        public List<int> AcceptableClasses { get; private set; }
        public bool InvertClasses { get; private set; }
        public bool NormalizeTIC { get; private set; }
        public bool OnlyUniquePeptides { get; private set; }
        public int MinMS1Count { get; private set; }
        public double MinFold { get; private set; }


        public Core20Paired(string fileName, bool invertClasses)
        {
            core = Core35.Deserialize(fileName);
            InvertClasses = invertClasses;

            if (invertClasses)
            {
                

                List<int> labels = core.SEProFiles.Select(a => a.ClassLabel).Distinct().ToList();
                if (labels.Count > 2)
                {
                    throw new Exception("Switch labels can only be used in dataset with two classes.");
                }


                int l1 = labels[0];
                int l2 = labels[1];

                //Invert in SEPro
                List<SEProFileInfo> sepF1 = core.SEProFiles.FindAll(a => a.ClassLabel == l1);
                List<SEProFileInfo> sepF2 = core.SEProFiles.FindAll(a => a.ClassLabel == l2);

                foreach (var s in sepF1)
                {
                    s.ClassLabel = l2;
                }

                foreach (var s in sepF2)
                {
                    s.ClassLabel = l1;
                }

                //Invert in Associations
                List<AssociationItem> a1 = core.MyAssociationItems.FindAll(a => a.Label == l1);
                List<AssociationItem> a2 = core.MyAssociationItems.FindAll(a => a.Label == l2);

                foreach (var a in a1)
                {
                    a.Label = l2;
                }

                foreach (var a in a2)
                {
                    a.Label = l1;
                }

                //Invert in Quant Packages
                List<QuantPackage2> q1 = core.myQuantPkgs.FindAll(a => a.ClassLabel == l1);
                List<QuantPackage2> q2 = core.myQuantPkgs.FindAll(a => a.ClassLabel == l2);

                foreach (var q in q1)
                {
                    q.ClassLabel = l2;
                }

                foreach (var q in q2)
                {
                    q.ClassLabel = l1;
                }
            }

        }

        /// <summary>
        /// Used for serialization purposes.
        /// </summary>
        public Core20Paired()
        {

        }

        bool wasNormalized = false;
        public List<PeptideAnalysis> MyPeptideAnalyses { get; set; }
        public List<ProteinAnalysis> MyProteinAnalyses { get; set; }

        /// <summary>
        /// Binary Serialization
        /// </summary>
        /// <param name="fileName"></param>
        public void Serialize(string fileName)
        {
            System.IO.FileStream flStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            bf.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesAlways;
            bf.Serialize(flStream, this);
            flStream.Close();
        }

        public static Core20Paired Deserialize(string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            Core20Paired corePaired = (Core20Paired)bformatter.Deserialize(stream);
            stream.Close();
            return corePaired;
        }






        public void RunPeptideAnalysis(bool normalizeTIC, List<int> acceptableClasses, bool onlyUniquePeptides, int minMS1Count, double minFold)
        {

            NormalizeTIC = normalizeTIC;
            AcceptableClasses = acceptableClasses;
            OnlyUniquePeptides = onlyUniquePeptides;
            MinMS1Count = minMS1Count;
            MinFold = minFold;
            
            if (AcceptableClasses.Count != 2)
            {
                throw new Exception("Please input 2 classes for peptide analysis");
            }

            if (normalizeTIC && !wasNormalized)
            {
                Console.WriteLine("Normalizing");
                wasNormalized = true;
                foreach (QuantPackage2 qp in core.myQuantPkgs)
                {

                    double tic = (from file in qp.MyQuants
                                   from q in file.Value
                                   select q.QuantArea).Sum();

                    foreach (List<Quant> peptide in qp.MyQuants.Values)
                    {
                        foreach (Quant quan in peptide)
                        {
                            quan.QuantArea /= tic;
                        }
                    }
                }

            }


            List<string> peptides = (from qp in core.myQuantPkgs
                                     where AcceptableClasses.Contains(qp.ClassLabel)
                                     from peptide in qp.MyQuants
                                     from quan in peptide.Value
                                     select peptide.Key + "::" + quan.Z).OrderBy(a => a).Distinct().ToList();

            

            //First we will get the scan numbers and then retrieve them from the sepro package
            

            List<AssociationItem> pairs = core.MyAssociationItems.FindAll(a => AcceptableClasses.Contains(a.Label));
            pairs.Sort((a, b) => a.Label.CompareTo(b.Label));

            //Build the matrix
            Console.WriteLine("Mounting peptide matrix");

            List<int> associations = pairs.Select(a => a.Assosication).Distinct().ToList();
            associations.Sort();

            ConcurrentBag<PeptideAnalysis> myPepAnalysis = new ConcurrentBag<PeptideAnalysis>();
            int counter = 0;

            int ZeroCounter = 0;
            int PlusCounter = 0;
            int MinusCounter = 0;
            int PositiveRatio = 0;
            int NegativeRatio = 0;

            //Parallel.For(0, peptides.Count, y =>
            for (int y = 0; y < peptides.Count; y++)
            {

                string[] cols = Regex.Split(peptides[y], "::");
                string pepSeq = cols[0];
                int z = int.Parse(cols[1]);

                string[] associationResults = new string[associations.Count];

                int noPtns = core.PeptideProteinDictionary[PatternTools.pTools.CleanPeptide(pepSeq, true)].Count;

                if (onlyUniquePeptides && noPtns == 1 || !onlyUniquePeptides)
                {

                    //var qs = (from qp in core.myQuantPkgs
                    //          where AcceptableClasses.Contains(qp.ClassLabel)
                    //          from peptide in qp.MyQuants.AsParallel()
                    //          from quan in peptide.Value
                    //          where peptide.Key.Equals(pepSeq) && quan.Z == z
                    //          select new { File = qp.FileName, Q = quan }).ToList();

                    for (int x = 0; x < associations.Count; x++)
                    {
                        List<AssociationItem> myAss = pairs.FindAll(a => a.Assosication == associations[x]);

                        //List<Quant> q1 = qs.FindAll(a => a.File.Equals(myAss[0].FileName)).Select(b => b.Q).ToList();
                        //List<Quant> q2 = qs.FindAll(a => a.File.Equals(myAss[1].FileName)).Select(b => b.Q).ToList();



                        List<Quant> q1 = (from qp in core.myQuantPkgs
                                          where qp.FileName.Equals(myAss[0].FileName) && AcceptableClasses.Contains(qp.ClassLabel)
                                          from peptide in qp.MyQuants
                                          from quan in peptide.Value
                                          where peptide.Key.Equals(pepSeq) && quan.Z == z
                                          select quan).ToList();

                        List<Quant> q2 = (from qp in core.myQuantPkgs
                                          where qp.FileName.Equals(myAss[1].FileName) && AcceptableClasses.Contains(qp.ClassLabel)
                                          from peptide in qp.MyQuants
                                          from quan in peptide.Value
                                          where peptide.Key.Equals(pepSeq) && quan.Z == z
                                          select quan).ToList();





                        double v1 = -1;
                        double v2 = -1;

                        if (q1.Count > 0)
                        {
                            if (q1[0].MyIons.GetLength(1) < minMS1Count)
                            {
                                v1 = 0;
                            }
                            else if (q1[0].QuantArea > 0)
                            {
                                v1 = q1[0].QuantArea;
                            }
                        }

                        if (q2.Count > 0)
                        {
                            if (q2[0].MyIons.GetLength(1) < minMS1Count)
                            {
                                v2 = 0;
                            }
                            else if (q2[0].QuantArea > 0)
                            {
                                v2 = q2[0].QuantArea;
                            }
                        }

                        if (q1.Count <= 0 && q2.Count <= 0)
                        {
                            associationResults[x] = "0";
                        }
                        else if (v1 > 0 && v2 > 0)
                        {
                            if (((v1 / v2) < minFold) && ((v2 / v1) < minFold))
                            {
                                associationResults[x] = "0";
                                ZeroCounter++;
                            }
                            else
                            {
                                double ratio = (v1 / v2);
                                associationResults[x] = ratio.ToString();

                                if (ratio >= 1)
                                {
                                    PositiveRatio++;
                                }
                                else
                                {
                                    NegativeRatio++;
                                }
                            }
                        }
                        else if (v1 > 0 && v2 <= 0)
                        {
                            associationResults[x] = "+";
                            PlusCounter++;
                        }
                        else if (v1 <= 0 && v2 > 0)
                        {
                            associationResults[x] = "-";
                            MinusCounter++;
                        } else if ( (v1 == -1 && v2 == 0) || (v2 == -1 && v1 == 0) || (v1 == 0 && v2 == 0))
                        {
                            associationResults[x] = "0";
                            ZeroCounter++;
                        }
                        else
                        {
                            Console.WriteLine("We Shouldn't be here");
                        }

                    }

                    PeptideAnalysis thisPep = new PeptideAnalysis(pepSeq, z, associationResults);

                    if (noPtns == 1)
                    {
                        thisPep.IsUnique = true;
                    } else
                    {
                        thisPep.IsUnique = false;
                    }


                    myPepAnalysis.Add(thisPep);
                    Console.Write("\r{0} / {1}      ", ++counter, peptides.Count);
                }
            }
            //);



            MyPeptideAnalyses = myPepAnalysis.ToList();
            MyPeptideAnalyses.Sort((a, b) => a.Sequence.CompareTo(b.Sequence));

            Console.WriteLine("Counter = " + counter);
            Console.WriteLine("Zero Counter = " + ZeroCounter);
            Console.WriteLine("Plus Counter = " + PlusCounter);
            Console.WriteLine("Minus Counter = " + MinusCounter);
            Console.WriteLine("Positive Ratio = " + PositiveRatio);
            Console.WriteLine("Negative Ratio = " + NegativeRatio);



        }

        public void RunProteinAnalysis()
        {
            List<ProteinAnalysis> myAnalysis = new List<ProteinAnalysis>();

            foreach (FastaItem fi in core.MyFastaItems)
            {

                //Get the related peptides
                List<PeptideAnalysis> myPeps = (from pa in MyPeptideAnalyses.AsParallel()
                                                where fi.Sequence.Contains(PatternTools.pTools.CleanPeptide(pa.Sequence, true))
                                                select pa).ToList();

                

                List<string> cleanedPeps = myPeps.Select(a => a.Sequence).Distinct().ToList();
                double cov = fi.Coverage(cleanedPeps, true);

                ProteinAnalysis pepAnalysis = new ProteinAnalysis(fi, cov, myPeps);
                myAnalysis.Add(pepAnalysis);        

            }

            MyProteinAnalyses = myAnalysis;
        }
    }
}
