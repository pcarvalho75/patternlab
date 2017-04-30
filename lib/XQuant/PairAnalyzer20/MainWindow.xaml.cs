using System;
using System.Collections.Generic;
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
using XQuant;

namespace PairAnalyzer20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Core35 core;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\paulo_000\Desktop\OngoingColaborations\brunoro\BreastCancerXIC2016.xic";

            core = XQuant.Core35.Deserialize(file);

            ButtonProcess.IsEnabled = true;

            Console.WriteLine("Done loading " + file);


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var protein in core.ProteinPeptideDictionary)
            {
                Console.WriteLine(protein.Key);

                foreach (var peptide in protein.Value)
                {
                    Console.WriteLine("\t" + peptide);
                }
            }

        }
    }
}
