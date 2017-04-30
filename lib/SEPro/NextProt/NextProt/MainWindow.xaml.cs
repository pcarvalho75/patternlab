using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NextProt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChromossomeBrowser1.UpdateNextProtModel(new DirectoryInfo(@"C:\Users\paulo_000\Documents\patternlab-for-proteomics\lib\SEPro\NextProt\NextProt\bin\Debug\nextprot") );
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SEPro (*.sepr)|*.sepr";

            if ((bool)ofd.ShowDialog())
            {
                ChromossomeBrowser1.LoadSepro(ofd.FileName);
                MessageBox.Show(ofd.FileName + " loaded");
                ChromossomeBrowser1.PlotSEPro();
            }
        }

        private void ButtonParseInput_Click(object sender, RoutedEventArgs e)
        {
            ChromossomeBrowser1.ParseTextInput();
            
        }
    }
}
