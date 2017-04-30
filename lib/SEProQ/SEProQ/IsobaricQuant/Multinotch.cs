using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatternTools.MSParserLight;
using SEPRPackage;
using PatternTools.SQTParser;
using PatternTools.MSParser;
using System.Text.RegularExpressions;
using System.IO;

namespace SEProQ.IsobaricQuant
{
    public partial class Multinotch : UserControl
    {
        public Multinotch()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {

            if (!File.Exists(textBoxSeproFile.Text))
            {
                MessageBox.Show("Please enter an input SEPro file to receive the multinotch patch.");
                return;
            }

            FileInfo fi = new FileInfo(textBoxSeproFile.Text);

            saveFileDialog1.InitialDirectory = fi.Directory.FullName;
            saveFileDialog1.FileName = "MN_" + fi.Name;

            saveFileDialog1.Filter = openFileDialog1.FileName = "SEPro file (*.sepr)|*.sepr";

            

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Console.WriteLine("Loading SEpro file.");
                ResultPackage sepro = ResultPackage.Load(textBoxSeproFile.Text);

                List<string> ms3FileNames = sepro.MyProteins.AllSQTScans.Select(a => Regex.Replace(a.FileName, ".sqt", ".ms3")).Distinct().ToList();

                Dictionary<string, List<MultiNotchMS3Item>> myMS3MNDict = new Dictionary<string, List<MultiNotchMS3Item>>();

                foreach (string ms3File in ms3FileNames)
                {
                    Console.WriteLine("Loading MS3 file :" + ms3File);
                    List<MSLight> theMS3 = PatternTools.MSParserLight.ParserLight.ParseLightMS2(textBoxMS3.Text + "\\" + ms3File);
                    List<MultiNotchMS3Item> myMNItems = theMS3.Select(a => new MultiNotchMS3Item(a)).ToList();
                    myMS3MNDict.Add(Regex.Replace(ms3File, ".ms3", ".sqt"), myMNItems);
                }


                foreach (SQTScan sqt in sepro.MyProteins.AllSQTScans)
                {
                    List<MultiNotchMS3Item> theItems = myMS3MNDict[sqt.FileName].FindAll(a => a.MS2PrecursorScan == sqt.ScanNumber);

                    if (theItems.Count == 1)
                    {
                        //check if the spectrum is null
                        if (object.ReferenceEquals(sqt.MSLight, null))
                        {
                            Console.WriteLine("Creating and providing quant data to spectrum: " + sqt.FileNameWithScanNumberAndChargeState);
                            sqt.MSLight = new MSLight();
                            sqt.MSLight.ScanNumber = sqt.ScanNumber;                     

                            foreach (Ion i in theItems[0].MyIons)
                            {
                                sqt.MSLight.MZ.Add(i.MZ);
                                sqt.MSLight.Intensity.Add(i.Intensity);
                            }


                        } else
                        {
                            Console.WriteLine("Providing uant data to spectrum: " + sqt.FileNameWithScanNumberAndChargeState);

                            List<Ion> newIons = sqt.MSLight.Ions.Concat(theItems[0].MyIons).ToList();
                            newIons.Sort((a, b) => a.MZ.CompareTo(b.MZ));

                            sqt.MSLight.MZ.Clear();
                            sqt.MSLight.Intensity.Clear();

                            sqt.MSLight.MZ = newIons.Select(a => a.MZ).ToList();
                            sqt.MSLight.Intensity = newIons.Select(a => a.Intensity).ToList();

                        }


                    }
                    else
                    {
                        Console.Write("Trouble finding MS3 for spectrum " + sqt.FileNameWithScanNumberAndChargeState);
                    }

                }

                Console.WriteLine("Saving the new result package");
                sepro.Save(saveFileDialog1.FileName);
            }

            Console.WriteLine("Done.");
        }

        private void buttonBrowseSEProFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "SEPro file (*.sepr)|*.sepr|All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBoxSeproFile.Text = openFileDialog1.FileName;
            }
        }

        private void buttonBrowseMS3Directory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBoxMS3.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
