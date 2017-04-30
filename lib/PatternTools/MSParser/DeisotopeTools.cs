using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using PatternTools.MSParserLight;

namespace PatternTools.MSParser.Deisotoping
{
    public static class DeisotopeTools
    {

        public static void PrintTheSpectrum(StreamWriter outputFile, PatternTools.MSParser.MSFull ms, int minDechargedPeaks)
        {
            //Some basic checking before we print anything
            if (ms.MSData.Count < minDechargedPeaks)
            {
                Console.WriteLine("Skipping scan " + ms.ScanNumber + " for it has less than " + minDechargedPeaks + " peaks.");
                return;
            }

            outputFile.WriteLine("S\t" + ms.ScanNumber + "\t" + ms.ScanNumber + "\t" + ms.ChargedPrecursor);
            outputFile.WriteLine("I\tRetTime\t" + ms.CromatographyRetentionTime);
            outputFile.WriteLine("I\tIonInjectionTime\t" + ms.IonInjectionTime);
            outputFile.WriteLine("I\tActivationType\t" + ms.ActivationType);
            outputFile.WriteLine("I\tInstrumentType\t" + ms.InstrumentType);


            //We need to print the Z line only if it is an MS2
            if (ms.isMS2 && ms.ZLines.Count > 0)
            {
                foreach (string ZLine in ms.ZLines)
                {
                    outputFile.WriteLine(ZLine);
                }
            }

            foreach (Ion ion in ms.MSData)
            {
                outputFile.WriteLine(ion.MZ + " " + ion.Intensity);
            }
        }

        public static MSUltraLight RichTextBox2MS(string textBoxInput, double minimumIntensity)
        {
            MSUltraLight myMS = new MSUltraLight();

            string[] theLines = Regex.Split(textBoxInput, @"\n");

            List<Tuple<float, float>> myIons = new List<Tuple<float, float>>();

            foreach (string line in theLines)
            {
                if (Regex.IsMatch(line, "^[0-9]"))
                {
                    string[] entries = Regex.Split(line, @" |\t");
                    float intensity = float.Parse(entries[1]);

                    if (intensity >= minimumIntensity)
                    {
                        float thisMZ = float.Parse(entries[0]);
                        myIons.Add(new Tuple<float, float>(thisMZ, intensity));
                    }
                }
            }

            myMS.UpdateIons(myIons);

            Tuple<float, short> precursor = new Tuple<float, short>(myMS.Ions.Last().Item1, 2);
            myMS.Precursors = new List<Tuple<float, short>>() { precursor };
            return (myMS);

        }

        public static List<Tuple<float, float>> Centroiding(List<Tuple<float, float>> theInput, float tolerance)
        {

            List<Tuple<float, float>> simplifiedList = theInput.Select(a => a).ToList();

            float lastIntensity = 0f;

            List<Tuple<float,float>> toExclude = new List<Tuple<float, float>>();

            for (int i = 1; i< simplifiedList.Count; i++)
            {
                if (simplifiedList[i].Item2 >= lastIntensity)
                {
                    if ((simplifiedList[i].Item1 - simplifiedList[i-1].Item1) <= tolerance)
                    {
                        toExclude.Add(simplifiedList[i - 1]);
                    }
                }
                lastIntensity = simplifiedList[i].Item2;
            }

            simplifiedList = simplifiedList.Except(toExclude).ToList();

            toExclude.Clear();

            lastIntensity = 0f;

            for (int i = simplifiedList.Count - 2; i >= 0; i--)
            {
                if (simplifiedList[i].Item2 >= lastIntensity)
                {
                    if ((simplifiedList[i + 1].Item1 - simplifiedList[i].Item1) <= tolerance)
                    {
                        toExclude.Add(simplifiedList[i + 1]);
                    }
                }
                lastIntensity = simplifiedList[i].Item2;
            }

            simplifiedList = simplifiedList.Except(toExclude).ToList();

            return simplifiedList;

        }

        //public static void Centroiding (MSUltraLight theSpectrum, double ppm) {

        //    List<Tuple<float, float>> simplifiedList = new List<Tuple<float, float>>();
        //    MonotonicalyIncreasingPeakFilter(theSpectrum, ppm, simplifiedList);

        //    //Now we need to reverse the algorithm since the isotopic peaks are simetrical
        //    theSpectrum.Ions.Sort((a,b) => b.Item2.CompareTo(a.Item1));
        //    MonotonicalyIncreasingPeakFilter(theSpectrum, ppm, simplifiedList);
        //    simplifiedList = simplifiedList.Distinct().ToList();
            
        //    //Now lets apply the percentage filter
        //    double percentage = 0.05; double distance = 0.3;
            
        //    for (int i = 0; i < simplifiedList.Count; i++)
        //    {
        //        double intensityRequired = simplifiedList[i].Item2 * percentage;
        //        simplifiedList.RemoveAll((a) => (Math.Abs(a.Item1 - simplifiedList[i].Item1) < distance && a.Item2 < intensityRequired));
        //    }
            

        //    //Return things to normal
        //    simplifiedList.Sort((a, b) => a.Item1.CompareTo(b.Item1));
        //    theSpectrum.UpdateIons(simplifiedList);
        //}

        //private static void MonotonicalyIncreasingPeakFilter(MSUltraLight theSpectrum, double ppm, List<Tuple<float, float>> simplifiedList)
        //{
        //    if (theSpectrum.Ions.Count == 0)
        //    {
        //        throw new Exception("There are no ions in this spectrum");
        //    }

        //    simplifiedList.Add(theSpectrum.Ions[0]);
        //    float lastIntensity = theSpectrum.Ions[0].Item2;

        //    for (int i = 1; i < theSpectrum.Ions.Count; i++)
        //    {
        //        Tuple<float, float> theIon = theSpectrum.Ions[i];

        //        if (theIon.Item2 > lastIntensity)
        //        {
        //            if (PatternTools.pTools.PPM(theIon.Item1, simplifiedList[simplifiedList.Count - 1].Item1) <= ppm)
        //            //if (theSpectrum.MSData[i].MZ - simplifiedList[simplifiedList.Count - 1].MZ <= 0.03)
        //            {
        //                simplifiedList[simplifiedList.Count - 1] = theIon;                        
        //            }
        //            else
        //            {
        //                simplifiedList.Add(theIon);
                     
        //            }
        //        }
        //        else if (PatternTools.pTools.PPM(theIon.Item1, simplifiedList[simplifiedList.Count - 1].Item1) > ppm)
        //        {
        //            simplifiedList.Add(theIon);
        //        }

        //        lastIntensity = theIon.Item2;
        //    }
        //}

        public static List<FilePairs> GetMSFilePairs (string directoryPath) {
            //Get all the equivalent MS1 and MS2 files
            DirectoryInfo di = new DirectoryInfo(directoryPath);

            FileInfo[] MS1 = di.GetFiles("*.ms1");
            FileInfo[] MS2 = di.GetFiles("*.ms2");

            List<FilePairs> filePairs = new List<FilePairs>();

            //Make sure avery ms1 has an ms2

            foreach (FileInfo f in MS1)
            {
                bool crash = true;
                string MS1Name = Regex.Replace(f.Name, @"\.ms1", "");
                FileInfo f2Pair = null;

                foreach (FileInfo f2 in MS2)
                {

                    //Remove the extensions
                    string MS2Name = Regex.Replace(f2.Name, @"\.ms2", "");

                    if (MS1Name.Equals(MS2Name))
                    {
                        crash = false;
                        f2Pair = f2;
                    }
                }

                if (crash)
                {
                    throw new Exception ("Missing an MS2 file in the directory for \n" + f.Name);
                }
                else
                {
                    filePairs.Add(new FilePairs(f, f2Pair));
                }
            }

            return (filePairs);

        }

        public class FilePairs
        {
            public FileInfo MS1 { get; set; }
            public FileInfo MS2 { get; set; }

            public FilePairs(FileInfo ms1, FileInfo ms2)
            {
                MS1 = ms1;
                MS2 = ms2;
            }

        }
    }


}
