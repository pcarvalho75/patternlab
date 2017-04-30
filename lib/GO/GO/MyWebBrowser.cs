using System;
using System.Windows.Forms;

namespace GO
{
    public partial class MyWebBrowser : Form
    {
        public MyWebBrowser()
        {
            InitializeComponent();
        }

        public void SetURL (string url) {
            Uri uri = new Uri(url);
            webBrowser1.Url = uri;
        }
    }
}
