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
using System.Windows.Shapes;

namespace MicroChart
{
    /// <summary>
    /// Interaction logic for TestForm.xaml
    /// </summary>
    public partial class TestForm : Window
    {

        public TestForm()
        {
            InitializeComponent();
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            KeyValuePair<string,List<string>> peptide = new KeyValuePair<string, List<string>>(TBSequence.Text, Regex.Split(TBPeptides.Text, " ").ToList());

            List<string> peptides = Regex.Split(TBPeptides.Text, " ").ToList();
            List<double> quants = Regex.Split(textBoxQuants.Text, " ").ToList().Select(a => double.Parse(a)).ToList();

            Dictionary<string, double> dic = new Dictionary<string, double>();

            for(int i = 0; i< peptides.Count; i++)
            {
                dic.Add(peptides[i], quants[i]);    
            }

            KeyValuePair<string, Dictionary<string, double>> theDatum = new KeyValuePair<string, Dictionary<string, double>>(TBSequence.Text, dic);


            List<object> myStuff = new List<object>();
            myStuff.Add(new { MyName = "SequenceName", SequenceAndPeptidesQuant = theDatum });

            MyDataGrid.ItemsSource = myStuff;
            
        }


        private void MyDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.PropertyName.Equals("SequenceAndPeptides"))
            {
                //Create a new teamplate column.
                DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                templateColumn.Header = e.Column.Header;
                templateColumn.MinWidth = 160;

                DataTemplate dt = new DataTemplate();
                FrameworkElementFactory fef = new FrameworkElementFactory(typeof(CoverageChart));

                fef.SetBinding(CoverageChart.SequenceAndPeptidesProperty, new Binding("SequenceAndPeptides"));

                dt.VisualTree = fef;
                templateColumn.CellTemplate = dt;
                e.Column = templateColumn;
            }

            if (e.PropertyName.Equals("SequenceAndPeptidesQuant"))
            {
                //Create a new teamplate column.
                DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                templateColumn.Header = e.Column.Header;
                templateColumn.MinWidth = 160;

                DataTemplate dt = new DataTemplate();
                FrameworkElementFactory fef = new FrameworkElementFactory(typeof(CoverageChart));

                fef.SetBinding(CoverageChart.SequenceAndPeptidesQuantProperty, new Binding("SequenceAndPeptidesQuant"));

                dt.VisualTree = fef;
                templateColumn.CellTemplate = dt;
                e.Column = templateColumn;
            }
        }    
    }
}
