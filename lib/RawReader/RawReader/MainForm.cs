using PatternTools.MSParser;
using PatternTools.MSParserLight;
using PatternTools.RawReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RawReader
{
    public partial class MainForm : UserControl
    {
        FileInfo studyFile;

        public MainForm()
        {
            InitializeComponent();
        }


        private void buttonGO_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxInputDirectory.Text))
            {
                MessageBox.Show("Please enter a valid directory");
                return;
            }

            if (!checkBoxMS1.Checked && !checkBoxMS2.Checked && !checkBoxMGF.Checked)
            {
                MessageBox.Show("Please enter at least one valid output format.");
                return;
            }

            buttonGO.Text = "Working";
            this.Update();

            DirectoryInfo di = new DirectoryInfo(textBoxInputDirectory.Text);
            List<FileInfo> rawFiles = di.GetFiles("*.raw").ToList();

            progressBar1.Value = 0;
            this.Update();

            RawReaderParams rawReaderParams = CaptureParamsFromScreen();

            double fileCounter = 0;
            foreach (FileInfo rawFile in rawFiles) {
                try
                {
                    Console.WriteLine("Processing file " + rawFile.Name);


                    Reader r = new Reader(rawReaderParams);
                    List<MSLight> theSpectra = r.GetSpectra(rawFile.FullName, new List<int>(), false);

                    if (theSpectra.Count == 0)
                    {
                        WriteMessageToLog(di, "No mass spectra found in file : " + rawFile.Name);
                    }
                    else
                    {
                        if (rawReaderParams.ExtractMS1)
                        {
                            List<MSLight> ms2Print = theSpectra.FindAll(a => a.ZLines.Count == 0);
                            SpectraPrinter.PrintFile(rawFile, ms2Print, ".ms1", rawReaderParams);
                        }

                        //MS2 and MS3 need to be revised to work with MS3.
                        if (rawReaderParams.ExtractMS2)
                        {
                            List<MSLight> ms2Print = theSpectra.FindAll(a => a.ZLines.Count > 0);
                            SpectraPrinter.PrintFile(rawFile, ms2Print, ".ms2", rawReaderParams);
                        }

                        if (rawReaderParams.ExtractMS3)
                        {
                            List<MSLight> ms3Print = theSpectra.FindAll(a => a.ZLines.Count > 0);
                            SpectraPrinter.PrintFile(rawFile, ms3Print, ".ms3", rawReaderParams);
                        }

                        if (checkBoxMGF.Checked)
                        {
                            List<MSLight> ms2Print = theSpectra.FindAll(a => a.ZLines.Count > 0);
                            List<MSUltraLight> ms22 = ms2Print.Select(a => new MSUltraLight(a, -1)).ToList();
                            
                            WriteAsMGF(ms22, rawFile.FullName + ".mgf");
                        }

                    }

                    fileCounter++;
                    progressBar1.Value = (int)((fileCounter / (double)rawFiles.Count()) * 100);
                    this.Update();
                }
                catch (Exception e2)
                {
                    Console.WriteLine("Problem on file : " + rawFile.Name + "\n" + e2.Message);
                    WriteMessageToLog(di, "Problem on file : " + rawFile.Name + "\n" + e2.Message);
                }

            }


            buttonGO.Text = "GO !";
            this.Update();
        }

         ///<summary>
        /// Converter to MGF file
        /// </summary>
        /// <param name="tmsList"></param>
        private static void WriteAsMGF(List<MSUltraLight> tmsList, string fileName)
        {
            if (tmsList == null || tmsList.Count == 0)
            {
                return;
            }

            Console.WriteLine(" Writing MGF File . . .");

            ///<summary>
            ///Get Program Version
            ///</summary>
            string version = "-";
            try
            {
                string[] stuff = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");
                version = Regex.Split(stuff[1], "=")[1];
            }
            catch (Exception e)
            {
                //Unable to retrieve version number
                Console.WriteLine("", e);
                version = "-";
            }

            StreamWriter sw = new StreamWriter(fileName);

            sw.Write("# Creation Date\t" + DateTime.Now.ToString() + "\n"
               + "# Extractor\tSpectrum Identification Machine\n"
               + "# Version\t" + version + "\n"
               + "# Comments\t This converter was written by Diogo Borges Lima and Paulo Costa Carvalho, 2013\n"
               + "# MGF Format File"
               + "#[Comments]\n"
               + "#[Query 1]\n"
               + "#[Query 2]\n"
               + "#...\n"
               + "#[Query N]\n"
               + "#\n"
               + "#   Format of each query:\n"
               + "#BEGIN IONS\n"
               + "#TITLE=[title of the entry]\n"
               + "#SCANS=[scan#]\n"
               + "#RTINSECONDS=[retention time in seconds]\n"
               + "#CHARGE=[charge state of the spectrum]\n"
               + "#PEPMASS=[precursor m/z value of the spectrum]\n"
               + "#[m/z intensity of product ion 1]\n"
               + "#[m/z intensity of product ion 2]\n"
               + "#...\n"
               + "#[m/z intensity of product ion m]\n"
               + "#END IONS\n\n"
               );

            foreach (MSUltraLight tms in tmsList)
            {
                double mass = ((tms.Precursors[0].Item1 / tms.Precursors[0].Item2) + (1.00794 / tms.Precursors[0].Item2) * (tms.Precursors[0].Item2 - 1.0));

                sw.WriteLine("BEGIN IONS");
                sw.WriteLine("TITLE=Scans:" + tms.ScanNumber + " RT:" + tms.CromatographyRetentionTime + " Charge:" + tms.Precursors[0].Item2 + "+ Fragmentation:" + tms.ActivationType);
                sw.WriteLine("SCANS=" + tms.ScanNumber);
                sw.WriteLine("RTINSECONDS=" + tms.CromatographyRetentionTime);
                sw.WriteLine("CHARGE=" + tms.Precursors[0].Item2 + "+");
                sw.WriteLine("PEPMASS=" + mass);

                foreach (Tuple<float, float> ion in tms.Ions)
                {
                    sw.WriteLine(ion.Item1 + "\t" + ion.Item2);
                }

                sw.WriteLine("END IONS");
            }

            Console.WriteLine(" Completed . . .");
            sw.Close();

        }

        private static void WriteMessageToLog(DirectoryInfo di, string message)
        {
            List<FileInfo> files = di.GetFiles("RawReaderLog.txt").ToList();
            StreamWriter sw = null;
            if (files.Count == 1)
            {
                sw = files[0].AppendText();
            }
            else if (files.Count == 0)
            {
                sw = new StreamWriter(di.FullName + "/" + "RawReaderLog.txt");
            }

            sw.WriteLine("[" + DateTime.Now + "] " + message);
            sw.Close();
        }

        private RawReaderParams CaptureParamsFromScreen()
        {
            RawReaderParams theParams = new RawReaderParams();
            theParams.ExtractMS1 = checkBoxMS1.Checked;
            theParams.ExtractMS2 = checkBoxMS2.Checked;
            theParams.ExtractMS2 = checkBoxMS3.Checked;

            theParams.UseThermoMonoIsotopicPrediction = checkBoxUseThermosMonoIsotopicInference.Checked;
            theParams.ActivateSpectraCleaner = checkBoxSpectraCleaner.Checked;
            theParams.SpectraCleanerWindowSize = (int)numericUpDownWindowSize.Value;
            theParams.SpectraCleanerMaxPeaksPerWindow = (int)numericUpDownMaxPeaksPerWindow.Value;
            theParams.SpectraCleanerMaxSpectraPerFile = (int)numericUpDownSpectraPerFile.Value;

            return theParams;

        }

        private void checkBoxSpectraCleaner_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSpectraCleaner.Checked)
            {
                groupBoxDeNovoSimplification.Enabled = true;
            }
            else
            {
                groupBoxDeNovoSimplification.Enabled = false;
            }
        }

        private void buttonExtract_Click(object sender, EventArgs e)
        {
            richTextBoxMS.Clear();
            RawReaderParams myParams = new RawReaderParams();
            myParams.LoadDefaults();

            Reader r = new Reader(myParams);

            List<MSLight> theMS = r.GetSpectra(studyFile.FullName, new List<int>() {(int)numericUpDownSpectrumNumber.Value}, false);
            
            List<Ion> theIons = new List<Ion>();

            for (int i = 0; i < theMS[0].MZ.Count; i++) 
            {
                theIons.Add(new Ion(theMS[0].MZ[i], theMS[0].Intensity[i], 0, 0));
            }
            

            foreach (Ion i in theIons)
            {
                richTextBoxMS.AppendText(i.MZ + " " + i.Intensity + "\n");
            }
        }

        private void buttonBrowseForFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                studyFile = new FileInfo(openFileDialog1.FileName);
                buttonBrowseForFile.Text = studyFile.Name;
                buttonExtract.Enabled = true;
            }
        }

        private void checkBoxMGF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMGF.Checked)
            {
                checkBoxMS1.Checked = false;
                checkBoxMS2.Checked = false;
                checkBoxMS3.Checked = false;
            }
        }

        private void checkBoxMS1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMS1.Checked)
            {
                checkBoxMGF.Checked = false;
            }
        }

        private void checkBoxMS2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMS2.Checked)
            {
                checkBoxMGF.Checked = false;
            }
        }

        private void checkBoxMS3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMS3.Checked)
            {
                checkBoxMGF.Checked = false;
            }
        }
    }
}
