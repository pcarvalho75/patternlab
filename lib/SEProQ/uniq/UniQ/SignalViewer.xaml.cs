using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UniQ
{
    /// <summary>
    /// Interaction logic for SignalViewer.xaml
    /// </summary>
    public partial class SignalViewer : Window
    {

        public DataGrid MyDataGridSpec
        {
            get { return DataGridSpec; }
            set { DataGridSpec = value; }
        }

        public PlotView MyBarChart
        {
            get { return MyPlot; }
            set { MyPlot = value; }
        }

        public SignalViewer()
        {
            InitializeComponent();
   
        }

        private void DataGridSpec_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "IsobaricAnalyzerChart";
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG files (.png)|*.png";

            if (dlg.ShowDialog() == true)
            {
                MyPlot.SaveBitmap(dlg.FileName);
                Console.WriteLine("Chart saved");

            }

        }
        
    }
}
