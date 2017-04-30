using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VennWPF2
{
    /// <summary>
    /// Interaction logic for ProteinReport.xaml
    /// </summary>
    public partial class ProteinReport : Window
    {
        public List<ReportItem> MyReportItems { get; set; }

        public ProteinReport()
        {
            InitializeComponent();
        }

        public void PlotReport()
        {
            if (MyReportItems.Count == 0)
            {
                throw new Exception("The MyReport Items array is empty");
            }

            DataGridReport.ItemsSource = MyReportItems;
        }

        void ctxExportCsv(object sender, MouseButtonEventArgs e)
        {
            DataGridReport.SelectAll();
            DataGridReport.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, DataGridReport);
            DataGridReport.UnselectAll();

            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            try
            {
                StreamWriter sw = new StreamWriter("export.csv");
                sw.WriteLine(result);
                sw.Close();
                Process.Start("export.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridReport_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
