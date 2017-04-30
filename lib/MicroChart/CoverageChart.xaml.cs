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

namespace MicroChart
{
    /// <summary>
    /// Interaction logic for CoverageChart.xaml
    /// </summary>
    public partial class CoverageChart : UserControl
    {


        #region nonQuantPlot
        public static readonly DependencyProperty SequenceAndPeptidesProperty =
            DependencyProperty.Register("SequenceAndPeptides", typeof(KeyValuePair<string, List<string>>), typeof(CoverageChart),
            new FrameworkPropertyMetadata(new KeyValuePair<string, List<string>>() { }, new PropertyChangedCallback(ValueChanged)));


        public static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CoverageChart cc = d as CoverageChart;
            KeyValuePair<string, List<string>> w = (KeyValuePair<string, List<string>>)e.NewValue;
            cc.Plot(w);

        }
  
        public KeyValuePair<string, List<string>> SequenceAndPeptides
        {
            get
            {
                KeyValuePair<string, List<string>> v = (KeyValuePair<string, List<string>>)GetValue(SequenceAndPeptidesProperty);
                return v;
            }

            set
            {
                SetValue(SequenceAndPeptidesProperty, value);
                KeyValuePair<string, List<string>> w = value;
                Plot(w);
            }
        }
        #endregion

        #region QuantPlot

        public static readonly DependencyProperty SequenceAndPeptidesQuantProperty =
        DependencyProperty.Register("SequenceAndPeptidesQuant", typeof(KeyValuePair<string, Dictionary<string, double>>), typeof(CoverageChart),
        new FrameworkPropertyMetadata(new KeyValuePair<string, Dictionary<string, double>>() { }, new PropertyChangedCallback(ValueChangedQuant)));


        public static void ValueChangedQuant(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CoverageChart cc = d as CoverageChart;
            KeyValuePair<string, Dictionary<string, double>> w = (KeyValuePair<string, Dictionary<string, double>>)e.NewValue;
            cc.PlotQuant(w);

        }

        public KeyValuePair<string, Dictionary<string, double>> SequenceAndPeptidesQuant
        {
            get
            {
                KeyValuePair<string, Dictionary<string, double>> v = (KeyValuePair<string, Dictionary<string, double>>)GetValue(SequenceAndPeptidesQuantProperty);
                return v;
            }

            set
            {
                SetValue(SequenceAndPeptidesQuantProperty, value);
                KeyValuePair<string, Dictionary<string, double>> w = value;
                PlotQuant(w);
            }
        }
        #endregion



        public CoverageChart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The Key is the protein fasta sequence, the dictionary, keys are cleaned peptides and double their corresponding quant value
        /// </summary>
        /// <param name=""></param>
        public void PlotQuant(KeyValuePair<string, Dictionary<string, double>> theData)
        {

            MyCanvas.Children.Clear();

            foreach (string pep in theData.Value.Keys)
            {
                MatchCollection mc = Regex.Matches(theData.Key, pep);
                List<int> pos = new List<int>();

                foreach (Match m in mc)
                {
                    pos.Add(m.Index);
                }

                pos.Distinct().ToList();

                foreach (int p in pos)
                {
                    double pepLength = ((double)pep.Length / (double)theData.Key.Length) * this.MaxWidth;
                    double LeftMargin = ((double)p / (double)theData.Key.Length) * this.MaxWidth;

                    Rectangle r = new Rectangle();
                    r.Height = this.MaxHeight;
                    r.Width = pepLength;
                    r.Opacity = 0.4;
                    r.StrokeThickness = 1;
                    r.Margin = new Thickness(-1.5, 1, 1, 1);
                    r.ToolTip = pep;


                    if (theData.Value[pep] == 0)
                    {
                        r.Stroke = Brushes.LightGray;
                        r.Fill = Brushes.DarkGray;
                    }
                    else if (theData.Value[pep] > 0)
                    {
                        //Find out how many pixels this peptide represents

                        r.Fill = Brushes.Green;
                        r.Stroke = Brushes.DarkGreen;
                    }
                    else
                    {
                        r.Fill = Brushes.Red;
                        r.Stroke = Brushes.DarkRed;

                    }

                    Canvas.SetTop(r, -3);
                    Canvas.SetLeft(r, LeftMargin);
                    MyCanvas.Children.Add(r);
                }
            }

        }


        public void Plot(KeyValuePair<string, List<string>> seqAndPeps)
        {
            MyCanvas.Children.Clear();

            foreach (string pep in seqAndPeps.Value)
            {
                MatchCollection mc = Regex.Matches(seqAndPeps.Key, pep);

                List<int> pos = new List<int>();

                foreach (Match m in mc)
                {
                    pos.Add(m.Index);
                }

                pos = pos.Distinct().ToList();

                foreach (int p in pos)
                {
                    //Find out how many pixels this peptide represents
                    double pepLength = ((double)pep.Length / (double)seqAndPeps.Key.Length) * this.MaxWidth;

                    double LeftMargin = ((double)p / (double)seqAndPeps.Key.Length) * this.MaxWidth;

                    Rectangle r = new Rectangle();
                    r.Height = this.MaxHeight;
                    r.Width = pepLength;
                    r.Opacity = 0.4;
                    r.Stroke = Brushes.DarkRed;
                    r.Fill = Brushes.Red;
                    r.StrokeThickness = 1;
                    r.Margin = new Thickness(-1.5, 1, 1, 1);
                    r.ToolTip = pep;

                    Canvas.SetTop(r, -3);
                    Canvas.SetLeft(r, LeftMargin);

                    MyCanvas.Children.Add(r);


                }
            }
        }


    }

}
