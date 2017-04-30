using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SEPRPackage;
using PatternTools.SQTParser;
using PatternTools.MSParserLight;
using System.Text.RegularExpressions;
using PatternTools.MSParser;
using PatternTools;

namespace SEProQ.Acetylation
{
    public partial class AcetylationQuant : UserControl
    {
        public AcetylationQuant()
        {
            InitializeComponent();
        }



        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                if (Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.ms1").Count() == 0)
                {
                    MessageBox.Show("No MS1's in the provided directory");
                    return;
                }

                if (Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.sepr").Count() == 0)
                {
                    MessageBox.Show("No SEPro files in the provided directory");
                    return;
                }

                textBoxMS1SEProDirectory.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonGO_Click(object sender, EventArgs e)
        {
            double ppmTolerance = 65;            

            AcetylationParams myParams = GetParamsFromGUI();

            string [] seproFiles = Directory.GetFiles(myParams.MS1SEProDirectory, "*.sepr");

            foreach (string seproFile in seproFiles)
            {

                Console.WriteLine("Loading SEPro File: " + seproFile);
                ResultPackage sepro = ResultPackage.Load(seproFile);
                Console.WriteLine("Done loading SEPro File");

                //Lets sort sqt scans by their file name and then load an ms1 file at a time
                List<string> files = Directory.GetFiles(myParams.MS1SEProDirectory, "*.ms1").ToList();

                foreach (string file in files)
                {
                    Console.WriteLine("Loading " + file);
                    List<MSLight> theMS1Spectra = PatternTools.MSParserLight.ParserLight.ParseLightMS2(file);

                    List<SQTScan> scans = sepro.MyProteins.AllSQTScans.FindAll(a => file.Contains(Regex.Replace(a.FileName, "sqt", "ms1")));


                    foreach (SQTScan scan in scans)
                    {
                        //theMS1Spectra.Sort((a, b) => Math.Abs(scan.ScanNumber - a.ScanNumber).CompareTo(Math.Abs(scan.ScanNumber - b.ScanNumber)));

                        List<MSLight> mstmp = theMS1Spectra.FindAll(a => a.ScanNumber < scan.ScanNumber);
                        mstmp.Sort((a, b) => Math.Abs(a.ScanNumber - scan.ScanNumber).CompareTo(Math.Abs(scan.ScanNumber - b.ScanNumber)));
                        
                        
                        //theMS1Spectra.Sort((a, b) => Math.Abs(a.ScanNumber - scan.ScanNumber).CompareTo(Math.Abs(scan.ScanNumber - b.ScanNumber)));

                        //Check if we are dealing with a medium or heavy peptide

                        bool isMedium = false;
                        if (scan.PeptideSequenceCleaned.StartsWith("(" + textBoxMediumMarker.Text + ")"))
                        {
                            isMedium = true;
                        }



                        //Find the delta
                        int noMods = Regex.Matches(scan.PeptideSequenceCleaned, Regex.Escape("(")).Count;
                        double delta = ((double)noMods * myParams.DeltaHighMedium) / (double)scan.ChargeState;
                        //scan.PeptideSequenceCleaned

                        double chargedPrecursor = (scan.MeasuredMH + ((double)(scan.ChargeState - 1) * 1.0078)) / (double)(scan.ChargeState);

                        scan.Quantitation = new List<List<double>>();
                        Console.WriteLine("Scan: " + scan.ScanNumber + " +" + scan.ChargeState + " " +scan.PeptideSequence);
                        for (int i = 0; i < myParams.SearchSpaceSize; i++)
                        {
                            //Obtain ratios from MS1
                            double mediumIntensity = -1;
                            double heavyIntensity = -1;

                            if (isMedium)
                            {
                                mediumIntensity = mstmp[i].Ions.FindAll(a => Math.Abs(pTools.PPM(a.MZ, chargedPrecursor)) < ppmTolerance).Sum(a => a.Intensity);
                                heavyIntensity = mstmp[i].Ions.FindAll(a => Math.Abs(pTools.PPM(a.MZ, chargedPrecursor + delta)) < ppmTolerance).Sum(a => a.Intensity);

                            }
                            else
                            {
                                mediumIntensity = mstmp[i].Ions.FindAll(a => Math.Abs(pTools.PPM(a.MZ, chargedPrecursor - delta)) < ppmTolerance).Sum(a => a.Intensity);
                                heavyIntensity = mstmp[i].Ions.FindAll(a => Math.Abs(pTools.PPM(a.MZ, chargedPrecursor)) < ppmTolerance).Sum(a => a.Intensity);
                            }

                            double summed = mediumIntensity + heavyIntensity;

                            scan.Quantitation.Add(new List<double>() { mediumIntensity, heavyIntensity });
                            Console.WriteLine("\tRatio: " + scan.ScanNumber + ": " + Math.Round(mediumIntensity / heavyIntensity,2));
                            Console.WriteLine("\tIntensities: " + mediumIntensity + " " + heavyIntensity);

                        }
                        Console.WriteLine("");
                    }

                }

                Console.WriteLine("Patching Sepro File, " + seproFile + " with quantitative data");
                sepro.Save(seproFile);
            }
            Console.WriteLine("Done!");
        }


        private AcetylationParams GetParamsFromGUI()
        {
            double mediumMarker = double.Parse(textBoxMediumMarker.Text);
            double heavyMarker = double.Parse(textBoxHeavyMarker.Text);
            
            if (mediumMarker > heavyMarker)
            {
                throw new Exception("Heavy marker should be heavier than medium marker");
            }

            if (!Directory.Exists(textBoxMS1SEProDirectory.Text))
            {
                throw new Exception("MS1 directory does not exist");
            }



            AcetylationParams myParams = new AcetylationParams(
                textBoxMS1SEProDirectory.Text,
                mediumMarker,
                heavyMarker,
                (int)numericUpDownSearchSpace.Value
                );

            return myParams;
        }
    }
}
