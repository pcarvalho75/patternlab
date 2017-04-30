using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using PatternTools.PTMMods;
using PatternTools.MSParserLight;

namespace PatternTools.SpectraPrediction
{
    public class SpectraPredictor
    {
        AminoacidMasses aam;
        Regex removeStartingHint = new Regex("^([A-Z]|-)" + Regex.Escape("."), RegexOptions.Compiled);
        Regex removeEndingHint = new Regex(Regex.Escape(".") + "([A-Z]|-)$", RegexOptions.Compiled);
        Regex neutralLossForNH3 = new Regex("[N|Q|R|K]", RegexOptions.Compiled);
        Regex neutralLossForH20 = new Regex("[S|T]");
        Dictionary<string, double> aaDict;
        SpectralPredictionParameters myParams;

        public SpectraPredictor(SpectralPredictionParameters myparams)
        {
            myParams = myparams;

            aam = new AminoacidMasses();
            
            if (myParams.IsMonoisotopic)
            {
                aaDict = PatternTools.ObjectCopier.Clone(aam.IsotopicDic);
            }
            else
            {
                aaDict = PatternTools.ObjectCopier.Clone(aam.AverageDic);
            }

            //Handle the modifications
            foreach (ModificationItem m in myparams.MyModifications)
            {
                if (m.IsVariable)
                {
                    string key = m.AminoAcid + "(" + m.DeltaMass + ")";
                    double mass = m.DeltaMass + aaDict[m.AminoAcid];
                    aaDict.Add(key, mass);
                }
                else
                {
                    aaDict[m.AminoAcid] += m.DeltaMass;
                }
            }

        }

        public static List<ModificationItem> GetVMods(string peptide)
        {
            List<string> broken = SpectraPredictor.SplitPeptide(peptide);
            List<string> withPTMs = broken.FindAll(a => a.Contains("("));
            List<ModificationItem> mods = new List<ModificationItem>();

            foreach (string s in withPTMs)
            {
                string cleanAA = PatternTools.pTools.CleanPeptide(s, true);
                Match delta = Regex.Match(s, @"-?[\d|\.]+");
                double no = double.Parse(delta.Value.ToString());
                mods.Add(new ModificationItem(cleanAA, "", no, false, false, true, true));
            }

            return mods;
        }

        public List<PredictedIon> PredictPeaks(string iPeptide, double precursorCharge, double theoreticalMassMH, MSLight ms, double ppm)
        {
            List<PredictedIon> result = PredictPeaks(iPeptide, precursorCharge, theoreticalMassMH);

            foreach (PredictedIon pi in result)
            {
                if (ms.MZ.Exists(a => Math.Abs( PatternTools.pTools.PPM(a, pi.MZ)) <= ppm))
                {
                    pi.Matched = true;
                }
            }

            return result;
        }

        public List<PredictedIon> PredictPeaks(string iPeptide, double precursorCharge, double theoreticalMassMH)
        {

            //Perform static modifications to dictionary


            //To implement
            //NH3 loss only occurs when a peptide has one of the following: Aspargine (N), Glutamine (Q), Lysine (K), Arginine (R)
            //H20 loss only occurs when a peptide has one of the following: Serine (S), Threonine (T). 


            //Make sure we have a peptide clean of hints
            string peptide = removeStartingHint.Replace(iPeptide, "");
            peptide = removeEndingHint.Replace(peptide, "");
            
            List<PredictedIon> peakList = new List<PredictedIon>();


            List<PredictedIon> aPeaks = new List<PredictedIon>();
            List<PredictedIon> bPeaks = new List<PredictedIon>();
            List<PredictedIon> cPeaks = new List<PredictedIon>();
            List<PredictedIon> xPeaks = new List<PredictedIon>();
            List<PredictedIon> yPeaks = new List<PredictedIon>();
            List<PredictedIon> zPeaks = new List<PredictedIon>();
            List<PredictedIon> precursor = new List<PredictedIon>();

            string reversePeptide = ReverseString(peptide);

            //Lets begin
            ////We first construct b and y, and then do the rest
            
            List<string> bEndAAs = SplitPeptide(peptide);
            List<string> yEndAAs = new List<string>(bEndAAs.Count);
            for (int i = bEndAAs.Count - 1; i >= 0; i--)
            {
                yEndAAs.Add(bEndAAs[i]);
            }

            List<double> bFragmentMasses = CalculateLadderOfMasses(bEndAAs);
            List<double> yFragmentMasses = CalculateLadderOfMasses(yEndAAs);

            List<string> bLadder = GenerateLadder(bEndAAs);
            List<string> yLadder = GenerateLadder(yEndAAs);



            //Predict the precursor neutral losses
            for (int i = 1; i <= precursorCharge; i++)
            {
                precursor.Add(new PredictedIon(i, ((theoreticalMassMH + ((i - 1) * aam.IsotopicDic["Hydrogen"])) / (double)i) - (aam.IsotopicDic["H2O"] / (double) i), i, "Precursor H2O neutral loss for charge " + i, IonSeries.Precursor));
                precursor.Add(new PredictedIon(i, ((theoreticalMassMH + ((i - 1) * aam.IsotopicDic["Hydrogen"])) / (double)i) - (aam.IsotopicDic["NH3"] / (double)i), i, "Precursor NH3 neutral loss for charge " + i, IonSeries.Precursor));
                precursor.Add(new PredictedIon(i, ((theoreticalMassMH + ((i - 1) * aam.IsotopicDic["Hydrogen"])) / (double)i), i, "Charge reduced precursor for charge " + i, IonSeries.Precursor));
            }

            // Generate B and Y ions, these will serve as basis for calculating other series
            for (int j = 0; j < bEndAAs.Count; j++)
            {
                double mb = bFragmentMasses[j];
                string finalAAb = bEndAAs[j];

                double my = yFragmentMasses[j] + aaDict["H2O"];
                string finalAAy = yEndAAs[j];

                bPeaks.Add(new PredictedIon(1, mb, j+1, finalAAb, IonSeries.B));
                yPeaks.Add(new PredictedIon(1, my, j+1, finalAAy, IonSeries.Y));

                if (myParams.H20) {
                    //Add H20 neutral Losses to ABC

                    if (neutralLossForH20.IsMatch(bLadder[j]))
                    {
                        bPeaks.Add(new PredictedIon(1, mb - aaDict["H2O"], -2, finalAAb + "-H2O", IonSeries.B));
                    }

                    if (neutralLossForH20.IsMatch(yLadder[j]))
                    {
                        yPeaks.Add(new PredictedIon(1, my - aaDict["H2O"], -2, finalAAy + "-H2O", IonSeries.Y));
                    }
                }

                if (myParams.NH3) {
                    //Add NH3 neutral Losses to ABC
                    if (neutralLossForNH3.IsMatch(bLadder[j]))
                    {
                        bPeaks.Add(new PredictedIon(1, mb - aaDict["NH3"], -1, finalAAb + "-NH3", IonSeries.B));
                    }

                    if (neutralLossForNH3.IsMatch(yLadder[j]))
                    {
                        yPeaks.Add(new PredictedIon(1, my - aaDict["NH3"], -1, finalAAy + "-NH3", IonSeries.Y));
                    }
                }
            }

            //Generate the Z peaks by transform all Y ions into Z ions by subtracting the mass of an NH3
            //-NH - 2H.
            for (int i = 0; i < yPeaks.Count; i++)
            {
                zPeaks.Add(new PredictedIon(1, yPeaks[i].MZ - aaDict["NH3"] + aaDict["Hydrogen"], yPeaks[i].Number, yPeaks[i].FinalAA, IonSeries.Z));
            }       


            //Generate C ions by adding NH to B ions
            for (int i = 0; i < bPeaks.Count; i++)
            {
                cPeaks.Add(new PredictedIon(1, bPeaks[i].MZ + aaDict["NH3"], bPeaks[i].Number, bPeaks[i].FinalAA, IonSeries.C));
            }

            //Generate A ions by subtracting CO from B
            for (int i = 0; i < bPeaks.Count; i++)
            {
                aPeaks.Add(new PredictedIon(1, bPeaks[i].MZ - aaDict["CO"], bPeaks[i].Number, bPeaks[i].FinalAA, IonSeries.A));
            }

            //Generate X ions by adding CO from Y
            for (int i = 0; i < yPeaks.Count; i++)
            {
                // I subtracted the mass of 2 H's to correction but dont know why, but matches with http://db.systemsbiology.net:8080/proteomicsToolkit/FragIonServlet.html
                double newIonsMass = yPeaks[i].MZ + aaDict["CO"] - 2 * aaDict["Hydrogen"];
                PredictedIon pion = new PredictedIon(1, newIonsMass , yPeaks[i].Number, yPeaks[i].FinalAA, IonSeries.X);
                xPeaks.Add(pion); 
            }

            //Now lets go for the neutral losses
            //I think water losses can only occur in xyz and NH3 losses only in abc, must check

            //Generate multiple charged ions
            if (myParams.DaughterIonChargesMax > 1)
            {
                List<PredictedIon> newAIons = new List<PredictedIon>(10);
                List<PredictedIon> newBIons = new List<PredictedIon>(10);
                List<PredictedIon> newCIons = new List<PredictedIon>(10);
                List<PredictedIon> newXIons = new List<PredictedIon>(10);
                List<PredictedIon> newYIons = new List<PredictedIon>(10);
                List<PredictedIon> newZIons = new List<PredictedIon>(10);

                for (int j = 0; j < bEndAAs.Count; j++)
                {
                    for (double k = 2; k <= myParams.DaughterIonChargesMax; k++)
                    {
                        newAIons.Add(new PredictedIon((int)k, (aPeaks[j].MZ + (k - 1) * aaDict["Hydrogen"]) / k, aPeaks[j].Number, aPeaks[j].FinalAA, IonSeries.A));
                        newBIons.Add(new PredictedIon((int)k, (bPeaks[j].MZ + (k - 1) * aaDict["Hydrogen"]) / k, bPeaks[j].Number, bPeaks[j].FinalAA, IonSeries.B));
                        newCIons.Add(new PredictedIon((int)k, (cPeaks[j].MZ + (k - 1) * aaDict["Hydrogen"]) / k, cPeaks[j].Number, cPeaks[j].FinalAA, IonSeries.C));
                        newXIons.Add(new PredictedIon((int)k, (xPeaks[j].MZ + (k - 1) * aaDict["Hydrogen"]) / k, xPeaks[j].Number, xPeaks[j].FinalAA, IonSeries.X));
                        newYIons.Add(new PredictedIon((int)k, (yPeaks[j].MZ + (k - 1) * aaDict["Hydrogen"]) / k, yPeaks[j].Number, yPeaks[j].FinalAA, IonSeries.Y));
                        newZIons.Add(new PredictedIon((int)k, (zPeaks[j].MZ + (k - 1) * aaDict["Hydrogen"]) / k, zPeaks[j].Number, zPeaks[j].FinalAA, IonSeries.Z));
                    }
                }

                aPeaks.AddRange(newAIons);
                bPeaks.AddRange(newBIons);
                cPeaks.AddRange(newCIons);
                xPeaks.AddRange(newXIons);
                yPeaks.AddRange(newYIons);
                zPeaks.AddRange(newZIons);
            }

            if (myParams.A)
            {
                peakList.AddRange(aPeaks);
            }

            if (myParams.B)
            {
                peakList.AddRange(bPeaks);
            }

            if (myParams.C)
            {
                peakList.AddRange(cPeaks);
            }

            if (myParams.X)
            {
                peakList.AddRange(xPeaks);
            }

            if (myParams.Y)
            {
                peakList.AddRange(yPeaks);
            }

            if (myParams.Z)
            {
                peakList.AddRange(zPeaks);
            }

            peakList.AddRange(precursor);
            peakList.Sort((a,b) => a.MZ.CompareTo(b.MZ));

            //Lets round to decimal 5
            foreach (PredictedIon p in peakList)
            {
                p.MZ = Math.Round(p.MZ, 5);
            }

            peakList.RemoveAll(a => a.MZ <= 0.1);

            return (peakList);
        }

        private List<string> GenerateLadder(List<string> theAAs)
        {
            List<string> ladder = new List<string>(theAAs.Count);

            ladder.Add(theAAs[0]);
            for (int i = 1; i < theAAs.Count; i++)
            {
                ladder.Add(ladder[i - 1] + theAAs[i]);
            }

            return ladder;
            
        }
        

        private string ReverseString(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private double peptideMass (string peptide)
        {
            double result = 0;

            for (int i = 0; i < peptide.Length; i++)
            {
                result += aaDict[peptide.Substring(i,1)];
            }

            return result + aaDict["Hydrogen"];
        }

        private List<double> CalculateLadderOfMasses(List<string> brokenPeptide)
        {
            List<string> theAAs = brokenPeptide;
            List<double> y = new List<double>(theAAs.Count);

            y.Add(aaDict[theAAs[0]]);

            for (int i = 1; i < theAAs.Count; i++)
            {
                y.Add(y[i-1] + aaDict[theAAs[i]]);
            }

            for (int i = 0; i < y.Count; i++)
            {
                y[i] += aaDict["Hydrogen"];
            }

            return y;
        }

        public static List<string> SplitPeptide(string peptide)
        {
            List<string> cols = new List<string>();

            string s = ""; bool ptm = false; string nextChar = "";
            for (int i = 0; i < peptide.Length; i++)
            {
                string thechar = peptide.Substring(i, 1);
                if (i < peptide.Length - 1)
                {
                    nextChar = peptide.Substring(i + 1, 1);
                }

                if (nextChar.Equals("("))
                {
                    s += thechar;
                    ptm = true;
                }
                else if (thechar.Equals(")"))
                {
                    s += ")";
                    ptm = false;
                    cols.Add(s);
                    s = "";
                    continue;
                }
                else if (ptm)
                {
                    s += thechar;
                }

                if (ptm == false)
                {
                    cols.Add(thechar);
                }
            }

            return cols;


        }

    }

}
