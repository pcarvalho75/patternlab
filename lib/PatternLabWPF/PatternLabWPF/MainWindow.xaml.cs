using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PatternLabWPF
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


        ////---------------------------------------------------------------

        //private void setControlInPanel2(object controlObject)
        //{

        //    //Get Rid of any controls
        //    for (int i = 0; i < splitContainer2.Panel1.Controls.Count; i++)
        //    {
        //        splitContainer2.Panel2.Controls[i].Dispose();
        //    }

        //    //We desperately need to make sure we clean up
        //    //lets call the garbage man
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    GC.Collect();

        //    splitContainer2.Panel2.Controls.Clear();

        //    Type t = controlObject.GetType();

        //    splitContainer2.Panel2.Controls.Add((System.Windows.Forms.Control)controlObject);

        //}


        private void aCFoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            parserGUI.XFoldControl ACFoldCont = new parserGUI.XFoldControl(true);
            host.Child = ACFoldCont;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Select :: ACFold";
        }


        private void tFoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            WindowsFormsHost host = new WindowsFormsHost();
            parserGUI.XFoldControl TFold = new parserGUI.XFoldControl(false);
            host.Child = TFold;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Select :: TFold";
 
        }


        private void proportionalVennDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            Venn.VennControl vc = new Venn.VennControl();
            host.Child = vc;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Analyze :: Approximately area-proportional Venn Diagrams";
            
        }

        private void gOExGeneOntologyExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            GO.GOControl gc = new GO.GOControl();
            host.Child = gc;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Analyze :: GOEx (Gene Ontology Explorer)";
           
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Wpf.AboutBox2 ab = new Wpf.AboutBox2();
            //ab.ShowDialog();
        }

        private void onlineBulletinBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////Get Rid of any controls
            //WebBrowser wb = new WebBrowser();
            //System.Uri theURI = new Uri("http://pcarvalho.com/patternlab/intranet/manual.shtml");
            //wb.Url = theURI;
            //setControlInPanel2(wb);
            //wb.Dock = DockStyle.Fill;
        }

        private void sendUsAMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////Get Rid of any controls
            //WebBrowser wb = new WebBrowser();
            //System.Uri theURI = new Uri("http://pcarvalho.com/patternlab/intranet/sendmessage.shtml");
            //wb.Url = theURI;
            //setControlInPanel2(wb);
            //wb.Dock = DockStyle.Fill;
        }


        private void trendQuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            TrendQuest.TrendQuestControl tc = new TrendQuest.TrendQuestControl();
            host.Child = tc;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Analyze :: TrendQuest";
       
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
            //UpdateCheckInfo info;

            //try
            //{
            //    info = updateCheck.CheckForDetailedUpdate();

            //    if (info.UpdateAvailable)
            //    {
            //        DialogResult dialogResult = MessageBox.Show("An update is available. Would you like to update the application now?", "Update available", MessageBoxButtons.OKCancel);

            //        if (dialogResult == DialogResult.OK)
            //        {
            //            updateCheck.Update();
            //            MessageBox.Show("The application has been upgraded, and will now restart.");
            //            Application.Restart();
            //        }

            //    }
            //    else
            //    {
            //        MessageBox.Show("No updates are available for the moment.");
            //    }
            //}
            //catch (DeploymentDownloadException dde)
            //{
            //    MessageBox.Show(dde.Message + "\n" + dde.InnerException);
            //    return;
            //}
            //catch (InvalidDeploymentException ide)
            //{
            //    MessageBox.Show(ide.Message + "\n" + ide.InnerException);
            //    return;
            //}
            //catch (InvalidOperationException ioe)
            //{
            //    MessageBox.Show(ioe.Message + "\n" + ioe.InnerException);
            //    return;
            //}
            //catch (Exception e10)
            //{
            //    MessageBox.Show(e10.Message);
            //    return;
            //}

        }

        private void buziosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //WindowsFormsHost host = new WindowsFormsHost();
            //Buzios.clusterControl buzios = new Buzios.clusterControl();
            //host.Child = buzios;
            //GridContent.Children.Clear();
            //GridContent.Children.Add(host);
            //labelBreadCrumb.Content = ".: Analyze :: Buzios";


        }

        private void anovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            Anova.AnovaControl ac = new Anova.AnovaControl();
            host.Child = ac;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Select :: Anova";
            
        }

        private void sEProRegrouperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            Regrouper.RegrouperControl regrouper = new Regrouper.RegrouperControl();
            host.Child = regrouper;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Project Organization :: SEPro or PepExplorer";
            regrouper.Dock = DockStyle.Fill;

        }

        private void MainEntry_Load(object sender, EventArgs e)
        {
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            //try
            //{
            //    string[] stuff = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");
            //    this.Text += " " + stuff[1];
            //}
            //catch
            //{
            //    Console.WriteLine("Deployed version not detected.");
            //}
        }

        private void massSpectraFileBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////Lets put him in
            //MSViewer.MSFileViewerWPF viewer = new MSViewer.MSFileViewerWPF();

            //System.Windows.Forms.Integration.ElementHost eh = new System.Windows.Forms.Integration.ElementHost();
            //Histogram.HistogramControl hc = new Histogram.HistogramControl();
            //eh.Child = viewer;
            //setControlInPanel2(eh);
            //labelBreadCrumb.Text = ".: Utils :: Mass Spectra File Browser";
            //eh.Dock = DockStyle.Fill;
        }

        private void generateSearchDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            NCBIExtractor.DBPrepareControl db = new NCBIExtractor.DBPrepareControl();
            host.Child = db;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Generate Search DB";
            db.Dock = DockStyle.Fill;
        }

        private void parseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            Regrouper.RegrouperControl regrouper = new Regrouper.RegrouperControl();
            host.Child = regrouper;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Project Organization :: SEPro or PepExplorer";
      
        }

        private void searchEngineProcessorSEProForPSMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            SEProcessor.MainControl sepro = new SEProcessor.MainControl();
            host.Child = sepro;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Filter :: Search Engine Processor (SEPro - for PSMs)";

        }

        private void pepExplorerforDeNovoSequencingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            PepExplorer2.Forms.ProgramMainControl pepexplorer = new PepExplorer2.Forms.ProgramMainControl();
            host.Child = pepexplorer;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Filter :: PepExplorer (for de novo sequencing)";
        }

        private void searchCometPSMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            CometWrapper.CometWrapper cw = new CometWrapper.CometWrapper();
            host.Child = cw;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Search (Comet PSM)";
            
        }


        private void rawReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            RawReader.MainForm rr = new RawReader.MainForm();
            host.Child = rr;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Utils :: RawReader";
            rr.Dock = DockStyle.Fill;
        }

        private void whatIsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Forms.WhatIsNew wis = new Forms.WhatIsNew();
            //wis.ShowDialog();
        }

        private void xDScoringPhosphoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            XDScore.XDScoreControl xdscore = new XDScore.XDScoreControl();
            host.Child = xdscore;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Utils :: XD Scoring (Phosphosites)";

        }

        private void indexSparseMatrixLegacyFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SparseMatrixIndexLegacy smi = new SparseMatrixIndexLegacy();
            //setControlInPanel2(smi);
            //labelBreadCrumb.Text = ".: Utils :: IndexSparseMatrixLegacyFormat";
            //smi.Dock = DockStyle.Fill;
        }

        private void xICBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            XQuant.XICExplorer xicExplorer = new XQuant.XICExplorer();
            host.Child = xicExplorer;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Quant :: XIC Browser";
        }

        private void isobaricAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            SEProQ.ITRAQ.ITRAQControl ic = new SEProQ.ITRAQ.ITRAQControl();
            host.Child = ic;
            GridContent.Children.Clear();
            GridContent.Children.Add(host);
            labelBreadCrumb.Content = ".: Quant :: Isobaric Analyzer";
           
        }

        private void troubleshootingAndUserForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://groups.google.com/forum/#!forum/patternlab");
        }

        private void histogramToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.Integration.ElementHost eh = new System.Windows.Forms.Integration.ElementHost();
            //Histogram.HistogramControl hc = new Histogram.HistogramControl();
            //eh.Child = hc;
            //setControlInPanel2(eh);
            //labelBreadCrumb.Content = ".: Utils :: Histogram Tool";
            //eh.Dock = DockStyle.Fill;
        }
    }
}
