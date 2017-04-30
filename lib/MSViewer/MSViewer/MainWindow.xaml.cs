using System.Threading.Tasks;
using System.Windows;

namespace MSViewer
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

        public MainWindow(string fileName)
        {
            InitializeComponent();
            MyViewer.LoadFile(fileName);
        }


    }
}
