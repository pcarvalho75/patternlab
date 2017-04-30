using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using SEPRPackage;

namespace SEProcessor.Result
{
    /// <summary>
    /// Interaction logic for SequenceViewer.xaml
    /// </summary>
    public partial class SequenceViewer : UserControl
    {
        MyProtein myProtein;
        List<string> distinctPeptides = new List<string>();
        
        public SequenceViewer()
        {
            InitializeComponent();
        }

        public void DisplayProtein (MyProtein p) {

            myProtein = p;

            ListBoxFiles.Items.Clear();
            List<String> fileNames = (from pep in p.PeptideResults
                                      from scan in pep.MyScans
                                      select scan.FileName).Distinct().ToList();

            fileNames.Sort();

            CheckBox chk = new CheckBox();
            chk.IsChecked = true;
            chk.Content = "AllFiles";
            ListBoxFiles.Items.Add(chk);

            foreach (string fileName in fileNames)
            {
                CheckBox chk2 = new CheckBox();
                chk2.Content = fileName;
                ListBoxFiles.Items.Add(chk2);
            }

            Plot();

            
        }

        
        private void ButtonPlot_Click(object sender, RoutedEventArgs e)
        {
            Plot();
        }

        private void Plot()
        {
            distinctPeptides.Clear();
            foreach (var i in ListBoxFiles.Items)
            {
                CheckBox c = (CheckBox)i;
                if ((bool)c.IsChecked)
                {
                    if (c.Content.ToString().Equals("AllFiles"))
                    {
                        distinctPeptides.AddRange(myProtein.DistinctPeptides);
                    }
                    else
                    {
                        List<string> peptides = (from scan in myProtein.Scans
                                                 where scan.FileName.Equals(c.Content.ToString())
                                                 select scan.PeptideSequenceCleaned).ToList();

                        distinctPeptides.AddRange(peptides);
                    }
                }
            }

            distinctPeptides = distinctPeptides.Distinct().ToList();

            this.labelProteinID.Content = myProtein.Locus;

            List<int> matchLocations = new List<int>(new int[(int)myProtein.Length]);

            foreach (string peptide in distinctPeptides)
            {
                //Make sure the peptide is clean
                string cleanedPeptide = PatternTools.pTools.CleanPeptide(peptide, true);

                Match location = Regex.Match(myProtein.Sequence, cleanedPeptide);
                for (int i = location.Index; i < location.Index + location.Length; i++)
                {
                    matchLocations[i] = 1;
                }
            }

            int matchCounter = matchLocations.FindAll(a => a == 1).Count;

            labelCoverage.Content = matchCounter + " / " + myProtein.Sequence.Length + " = " + Math.Round((double)matchCounter / (double)myProtein.Sequence.Length, 3);

            FlowDocument myFlowDoc = new FlowDocument();
            Paragraph myParagraph = new Paragraph();


            for (int i = 0; i < myProtein.Length; i++)
            {

                double j = i + 1;
                double remainder10 = j % 10;
                double remainder60 = j % 60;

                if (i % 60 == 0)
                {
                    myParagraph.Inlines.Add(j.ToString("000") + " ");
                }

                if (matchLocations[i] == 1)
                {
                    Bold b = new Bold();
                    b.Foreground = Brushes.Blue;
                    b.Inlines.Add(myProtein.Sequence[i].ToString());
                    myParagraph.Inlines.Add(b);
                }
                else
                {
                    myParagraph.Inlines.Add(myProtein.Sequence[i].ToString());
                }

                if (remainder10 == 0)
                {
                    myParagraph.Inlines.Add(" ");
                }

                if (remainder60 == 0)
                {
                    myParagraph.Inlines.Add(j.ToString("000") + "\n");
                }

            }
            myFlowDoc.Blocks.Add(myParagraph);
            richTextBoxProteinSequence.Document = myFlowDoc;

        }

        private void ListBoxFiles_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Plot();
        }
    }
}
