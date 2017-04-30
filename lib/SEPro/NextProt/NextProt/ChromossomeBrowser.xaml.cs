using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SEPRPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NextProt
{
    /// <summary>
    /// Interaction logic for ChromossomeBrowser.xaml
    /// </summary>
    public partial class ChromossomeBrowser : UserControl
    {
        public ResultPackage MySEProResultPackage
        {
            set
            {
                pckg = value;
            }
        }

        public ResultPackage pckg = null;
        NextProtRecord[] nRecords;

        public ChromossomeBrowser()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifiedIDs">ID, Chr, AntiBody, Proteomics</param>
        private void Plot(List<NextProtRecord> identifiedRecords)
        {


            //Perform OxyPlot
            var plotModel1 = new PlotModel();
            plotModel1.LegendBorderThickness = 0;
            plotModel1.LegendOrientation = LegendOrientation.Horizontal;
            plotModel1.LegendPlacement = LegendPlacement.Outside;
            plotModel1.LegendPosition = LegendPosition.BottomCenter;
            plotModel1.Title = "NeXtProt Chromossome distribution analysis";

            var categoryAxis1 = new CategoryAxis();
            categoryAxis1.MinorStep = 1;

            for (int i = 1; i <= 22; i++)
            {
                categoryAxis1.Labels.Add(i.ToString());
            }
            categoryAxis1.Labels.Add("X");
            categoryAxis1.Labels.Add("Y");


            plotModel1.Axes.Add(categoryAxis1);
            var linearAxis1 = new LinearAxis();
            linearAxis1.AbsoluteMinimum = 0;
            linearAxis1.MaximumPadding = 0.06;
            linearAxis1.MinimumPadding = 0;
            plotModel1.Axes.Add(linearAxis1);

            var columnSeriesIdentified = new ColumnSeries();
            columnSeriesIdentified.LabelFormatString = "{0}";
            columnSeriesIdentified.StrokeThickness = 1;
            columnSeriesIdentified.Title = "Identified";

            var columnSeriesNoAntibody = new ColumnSeries();
            columnSeriesNoAntibody.LabelFormatString = "{0}";
            columnSeriesNoAntibody.StrokeThickness = 1;
            columnSeriesNoAntibody.Title = "No Antibody";

            var columnSeriesNoProteomics = new ColumnSeries();
            columnSeriesNoProteomics.LabelFormatString = "{0}";
            columnSeriesNoProteomics.StrokeThickness = 1;
            columnSeriesNoProteomics.Title = "No Proteomics";

            var columnSeriesNoEvidence = new ColumnSeries();
            columnSeriesNoEvidence.LabelFormatString = "{0}";
            columnSeriesNoEvidence.StrokeThickness = 1;
            columnSeriesNoEvidence.Title = "No Evidence";

            for (int i = 1; i <= 22; i++)
            {
                string ii = i.ToString();
                GenerateColumn(columnSeriesIdentified, columnSeriesNoAntibody, columnSeriesNoProteomics, columnSeriesNoEvidence, ii, identifiedRecords);

            }


            //Now for X
            GenerateColumn(columnSeriesIdentified, columnSeriesNoAntibody, columnSeriesNoProteomics, columnSeriesNoEvidence, "X", identifiedRecords);

            //And For Y
            GenerateColumn(columnSeriesIdentified, columnSeriesNoAntibody, columnSeriesNoProteomics, columnSeriesNoEvidence, "Y", identifiedRecords);

            //And For MT
            GenerateColumn(columnSeriesIdentified, columnSeriesNoAntibody, columnSeriesNoProteomics, columnSeriesNoEvidence, "MT", identifiedRecords);


            plotModel1.Series.Add(columnSeriesIdentified);
            plotModel1.Series.Add(columnSeriesNoAntibody);
            plotModel1.Series.Add(columnSeriesNoProteomics);
            plotModel1.Series.Add(columnSeriesNoEvidence);

            MyPlot.Model = plotModel1;


        }

        private void GenerateColumn(ColumnSeries columnSeriesIdentified, ColumnSeries columnSeriesNoAntibody, ColumnSeries columnSeriesNoProteomics, ColumnSeries columnSeriesNoEvidence, string chr, List<NextProtRecord> identifiedRecords)
        {
            //ID, Chr, AntiBody, Proteomics
            var forThisChr = identifiedRecords.FindAll(a => a.Chr.Equals(chr));
            columnSeriesIdentified.Items.Add(new ColumnItem(forThisChr.Count));
            columnSeriesNoAntibody.Items.Add(new ColumnItem( forThisChr.Count(a => !a.AntiBody )));
            columnSeriesNoProteomics.Items.Add(new ColumnItem(forThisChr.Count(a => !a.Proteomics )));
            columnSeriesNoEvidence.Items.Add(new ColumnItem(forThisChr.Count(a => !a.AntiBody && !a.Proteomics )));
        }

        private void LoadNRecords()
        {
            if (nRecords == null)
            {
                //Deserialize the NextProt
                try
                {
                    SoapFormatter formatter = new SoapFormatter();

                    // Deserialize the hashtable from the file and 
                    // assign the reference to the local variable.
                    nRecords = (NextProtRecord[])formatter.Deserialize(GenerateStreamFromString(NextProt.Properties.Resources.NextProtResource));
                }
                catch (SerializationException e)
                {
                    throw new Exception("Failed to deserialize. Reason: " + e.Message);
                }
            }
        }

        public void PlotSEPro()
        {

            LoadNRecords();
            List<string> identifiedProt = new List<string>();

            if ((bool)CheckBoxUnique.IsChecked)
            {
                identifiedProt = (from p in pckg.MyProteins.MyProteinList
                                  where p.PeptideResults.Exists(x => x.MyMapableProteins.Count == 1)
                                  //where p.PeptideResults.Exists(b => b.NoMyMapableProteins == 1)
                                  select p.Locus).ToList();


            }
            else
            {
                identifiedProt = pckg.MyProteins.MyProteinList.Select(a => a.Locus).ToList();
            }


            //clean possible np tags
            //for (int i = 0; i < identifiedProt.Count; i++)
            //{
            //    identifiedProt[i] = Regex.Replace(identifiedProt[i], "nxp:NX_", "");
            //    identifiedProt[i] = Regex.Replace(identifiedProt[i], "-[0-9]+", "");
            //}

            LabelProteins.Content = identifiedProt.Count;

            List<NextProtRecord> identifiedRecords = (from rec in nRecords
                                                      where identifiedProt.Contains(rec.ID)
                                                      select rec).ToList();

            //Get all the proteins that did not map------------------------

            List<string> unmapped = (from p in pckg.MyProteins.MyProteinList
                                     where !identifiedRecords.Exists(a => a.ID.Contains(p.Locus))
                                     select p.Locus).ToList();

            TextBoxLog.AppendText("Unmapped IDs\n");
            TextBoxLog.AppendText(string.Join(", ", unmapped));
            
            //--------------------------------------------------------------



            Plot(identifiedRecords);


            var result = from r in identifiedRecords
                         from p in pckg.MyProteins.MyProteinList
                         where r.ID.Contains(p.Locus)
                         select new
                         {
                             ID = p.Locus,
                             NoUniquePeptides = p.PeptideResults.Count(c => c.NoMyMapableProteins == 1),
                             SequenceCount = p.SequenceCount,
                             Chr = r.Chr,
                             Antibody = r.AntiBody,
                             Proteomics = r.Proteomics,
                             Description = p.Description
                         };

         //var result = from p in pckg.MyProteins.MyProteinList
         //                where identifiedRecords.Exists(b => p.Locus.Contains(b.ID))
         //                select new
         //                {
         //                    ID = p.Locus,
         //                    NoUniquePeptides = p.PeptideResults.Count(c => c.NoMyMapableProteins == 1),
         //                    SequenceCount = p.SequenceCount,
         //                    SpecCount = p.SpectrumCount,
         //                    Chr = identifiedIDs.Find(d => p.Locus.Contains(d.Item1)).Item2,
         //                    Antibody = identifiedIDs.Find(d => p.Locus.Contains(d.Item1)).Item3,
         //                    Proteomics = identifiedIDs.Find(d => p.Locus.Contains(d.Item1)).Item4,
         //                    Description = p.Description
         //                };

            DataGridInterestingProteins.ItemsSource = result;
        }

               

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public void LoadSepro(string fileName)
        {
            Console.WriteLine("Loading: " + fileName);
            pckg = ResultPackage.Load(fileName);
            Console.WriteLine("Done loading SEProFile");
        }

        public void LoadSepro(ResultPackage result)
        {
            pckg = result;
        }

        public void UpdateNextProtModel (DirectoryInfo di)
        {
            FileInfo[] files = di.GetFiles("*.txt");

            List<NextProtRecord> npRecordsTMP = new List<NextProtRecord>();
            foreach (FileInfo fi in files)
            {
                string[] fileParts = Regex.Split(fi.Name, "_");
                string[] part2 = Regex.Split(fileParts[2], Regex.Escape("."));

                string[] lines = File.ReadAllLines(fi.FullName);
                string chr = part2[0];

                foreach (string line in lines)
                {
                    string[] cols = Regex.Split(line, "\t");
                    if (cols.Length == 9)
                    {
                        string id = Regex.Matches(cols[0], "NX_[A-Z|0-9]+")[0].Value;
                        string description = cols.Last();

                        bool antibody = false;
                        if (cols[3].Contains("yes"))
                        {
                            antibody = true;
                        }

                        bool proteomics = false;
                        if (cols[4].Contains("yes"))
                        {
                            proteomics = true;
                        }

                        NextProtRecord npr = new NextProtRecord(id.Substring(3, id.Length-3), antibody, proteomics, chr);
                        npRecordsTMP.Add(npr);
                    }
                    
                }
            }
            Console.WriteLine("Missing according to antibody: " + npRecordsTMP.Count(a => a.AntiBody == false));
            Console.WriteLine("Missing according to proteomics: " + npRecordsTMP.Count(a => a.Proteomics == false));

            SoapFormatter soapFormat = new SoapFormatter();
            using (MemoryStream myStream = new MemoryStream())
            {
                soapFormat.Serialize(myStream, npRecordsTMP.ToArray());
                myStream.Position = 0;
                using (StreamReader sr = new StreamReader(myStream))
                {
                    string reqString = sr.ReadToEnd();
                    StreamWriter sw = new StreamWriter("NextProtResource.txt");
                    sw.Write(reqString);
                    sw.Close();
                }
            }

            //Now please update the resource manually
            
            Console.WriteLine("Done");
        }

        

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG files (.png)|*.png";

            if (dlg.ShowDialog() == true)
            {
                MyPlot.SaveBitmap(dlg.FileName);
                MessageBox.Show("Chart saved");
            }
        }

        //Fire it up through data in the input
        public void ParseTextInput()
        {
            List<string> lines = Regex.Split(TextBoxInput.Text, "\n").ToList();
            lines.RemoveAll(a => a.Length == 0);

            List<string> identifiedProt = new List<string>();

            foreach (string line in lines)
            {
                string[] cols = Regex.Split(line, "\t");
                string[] ids = Regex.Split(cols[0], ";");

                if ((bool)CheckBoxUnique.IsChecked)
                {
                    identifiedProt.AddRange(ids);
                    throw new Exception("need to make it consider only unique peptides.");
                } else
                {
                    identifiedProt.AddRange(ids);
                }


            }

            LabelProteins.Content = identifiedProt.Count;

            
            //identifiedRecords = (from rec in nRecords
            //                     where identifiedProt.Exists(a => a.Contains(rec.ID))
            //                     select rec).ToList();

            ////Generate the table of interest
            //identifiedIDs = (from ir in identifiedRecords
            //                 where !ir.Proteomics || !ir.AntiBody
            //                 select new Tuple<string, string, bool, bool>(ir.ID, ir.Chr, ir.AntiBody, ir.Proteomics)).ToList();


        }

        private void DataGridInterestingProteins_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
