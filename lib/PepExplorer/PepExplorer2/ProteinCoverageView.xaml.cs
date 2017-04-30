using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PatternTools.FastaParser;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PepExplorer2
{
    /// <summary>
    /// Interaction logic for ProteinCoverageView.xaml
    /// </summary>
    public partial class ProteinCoverageView : UserControl
    {
        public ProteinCoverageView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Will perform a coverage plot
        /// </summary>
        /// <param name="theProtein">The PatternTools Fasta Item</param>
        /// <param name="peptides">A List of Tuples were the first item is a list of positions were this peptide aligns with the protein, the second item is the peptide sequence</param>
        public void Plot (FastaItem theProtein, List<Tuple<List<int>, string>> peptides)
        {

            peptides.Sort((a, b) => a.Item1[0].CompareTo(b.Item1[0]));

            PlotModel MyModel = new PlotModel();
            MyModel.Title = theProtein.SequenceIdentifier;

            MyModel.IsLegendVisible = false;


            IntervalBarSeries intervalBarSeriesProtein = new IntervalBarSeries();
            intervalBarSeriesProtein.Title = theProtein.SequenceIdentifier;
            intervalBarSeriesProtein.Items.Add(new IntervalBarItem(0, theProtein.Sequence.Length));


            var categoryAxis1 = new CategoryAxis();
            categoryAxis1.Position = AxisPosition.Left;
            categoryAxis1.Labels.Add(theProtein.SequenceIdentifier);

            MyModel.Series.Add(intervalBarSeriesProtein);

            IntervalBarSeries intervalBarSeriesPeptide = new IntervalBarSeries();

            //Lets insert a blank entry so that the first peptide appears in the first row
            intervalBarSeriesPeptide.FillColor = OxyColors.AliceBlue;
            intervalBarSeriesPeptide.Items.Add(new IntervalBarItem(0, 0));

            foreach (Tuple<List<int>, string> peptide in peptides)
            { 
                foreach (int index in peptide.Item1)
                {
                    categoryAxis1.Labels.Add(peptide.Item2);
                    intervalBarSeriesPeptide.Title = peptide.Item2;
                    intervalBarSeriesPeptide.Items.Add(new IntervalBarItem(index, index + peptide.Item2.Length));

                    //And one series to show the coverage on the protein
                    IntervalBarSeries intervalBarSeriesPeptideProtein = new IntervalBarSeries();
                    intervalBarSeriesPeptideProtein.FillColor = OxyColors.AliceBlue;
                    intervalBarSeriesPeptideProtein.Items.Add(new IntervalBarItem(index, index + peptide.Item2.Length));
                    MyModel.Series.Add(intervalBarSeriesPeptideProtein);
                }
            }

            MyModel.Series.Add(intervalBarSeriesPeptide);

            MyModel.Axes.Add(categoryAxis1);

            MyPlot.Model = MyModel;


        }
    }
}
