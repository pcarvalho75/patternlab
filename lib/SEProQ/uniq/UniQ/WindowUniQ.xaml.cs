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
using System.Windows.Shapes;

namespace UniQ
{
    /// <summary>
    /// Interaction logic for WindowUniQ.xaml
    /// </summary>
    public partial class WindowUniQ : Window
    {
        public UniQC ThisUniQC {
            get { return MyUniQC; }
            set { MyUniQC = value; }
        }

        public WindowUniQ()
        {
            InitializeComponent();
        }

    }
}
