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

namespace Histogram
{
    /// <summary>
    /// Interaction logic for HistogramControl.xaml
    /// </summary>
    public partial class HistogramControl : UserControl
    {
        public HistogramControl()
        {
            InitializeComponent();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxInput.Clear();
        }

        public TextBox MyInputTextBox
        {
            get { return TextBoxInput; }
            set { TextBoxInput = value; }
        }

        private void ButtonBinData_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, List<double>> columnValueDictionary = GetColumnValueDictionaryFromForm();


            if ((bool)CheckBoxLogData.IsChecked)
            {
                List<int> keys = columnValueDictionary.Keys.ToList();

                foreach (int k in keys)
                {
                    columnValueDictionary[k] = columnValueDictionary[k].Select(a => Math.Log(a)).ToList();
                }
            }

            if (DoubleUpDownXMin.Value >= DoubleUpDownXMax.Value && DoubleUpDownXMin.Value != -1)
            {
                MessageBox.Show("The minimum x value must be smaller than the maximum x-value");
                return;
            }

            List<int> theKeys = columnValueDictionary.Keys.ToList();

            foreach (int k in theKeys)
            {
                if ((double)DoubleUpDownXMin.Value != -1)
                {
                    columnValueDictionary[k].RemoveAll(a => a < (double)DoubleUpDownXMin.Value);
                }

                if ((double)DoubleUpDownXMax.Value != -1)
                {
                    columnValueDictionary[k].RemoveAll(a => a > (double)DoubleUpDownXMax.Value);
                }
            }

            //We need to use the same bin pace in case there are various columns so we need to find the global min and max
            double min = double.MaxValue;
            double max = double.MinValue;
            foreach (KeyValuePair<int, List<double>> kvp in columnValueDictionary)
            {
                if (kvp.Value.Min() < min) { min = kvp.Value.Min(); }
                if (kvp.Value.Max() > max) { max = kvp.Value.Max(); }
            }


            Dictionary<int, List<PatternTools.HistogramHelper.HistogramBin>> theBinsDict = new Dictionary<int, List<PatternTools.HistogramHelper.HistogramBin>>();


            foreach (KeyValuePair<int, List<double>> kvp in columnValueDictionary)
            {
                if (theKeys.Count > 1 && (double)DoubleUpDownXMin.Value == -1 && (double)DoubleUpDownXMax.Value == -1)
                {
                    theBinsDict.Add(kvp.Key, PatternTools.HistogramHelper.BinData(kvp.Value, min, max, (int)IntegerUpDownNoBins.Value, true));
                }
                else
                {
                    if ((double)DoubleUpDownXMin.Value == -1 && (double)DoubleUpDownXMax.Value == -1)
                    {
                        theBinsDict.Add(kvp.Key, PatternTools.HistogramHelper.BinData(kvp.Value, (int)IntegerUpDownNoBins.Value));
                    }
                    else
                    {
                        theBinsDict.Add(kvp.Key, PatternTools.HistogramHelper.BinData(kvp.Value, (double)DoubleUpDownXMin.Value, (double)DoubleUpDownXMax.Value, (int)IntegerUpDownNoBins.Value, true));
                    }
                }
            }

            MyHistogramPlot.Plot(theBinsDict, (double)DoubleUpDownYMax.Value, (double)DoubleUpDownXMin.Value, (double)DoubleUpDownXMax.Value, TextBoxTitle.Text, TextBoxXAxis.Text, TextBoxYAxis.Text);

        }

        private Dictionary<int, List<double>> GetColumnValueDictionaryFromForm()
        {
            List<string> theData = GetInboxData();

            Dictionary<int, List<double>> columnValueDictionary = new Dictionary<int, List<double>>();

            int noBins = (int)IntegerUpDownNoBins.Value;

            foreach (string s in theData)
            {
                List<string> cols = Regex.Split(s, "[\t| ]").ToList();
                cols.RemoveAll(a => a.Equals(""));

                for (int i = 0; i < cols.Count; i++)
                {
                    if (columnValueDictionary.ContainsKey(i))
                    {
                        try
                        {
                            columnValueDictionary[i].Add(double.Parse(cols[i]));
                        }
                        catch
                        {
                            Console.Write(".");
                        }
                    }
                    else
                    {
                        try
                        {
                            columnValueDictionary.Add(i, new List<double>() { double.Parse(cols[i]) });
                        }
                        catch
                        {
                            Console.Write(".");
                        }
                    }
                }
            }

            return columnValueDictionary;
        }

        private List<string> GetInboxData()
        {
            List<string> theData = Regex.Split(TextBoxInput.Text, "\n").ToList();
            theData.RemoveAll(a => a.Equals(""));
            return theData;
        }

    }
}
