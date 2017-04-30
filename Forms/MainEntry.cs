using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Text.RegularExpressions;

namespace PatternLab
{
    public partial class MainEntry : Form
    {
        public MainEntry()
        {
            InitializeComponent();
        }

        private void setControlInPanel2(object controlObject)
        {

            //Get Rid of any controls
            for (int i = 0; i < splitContainer2.Panel1.Controls.Count; i++)
            {
                splitContainer2.Panel2.Controls[i].Dispose();
            }

            //We desperately need to make sure we clean up
            //lets call the garbage man
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            splitContainer2.Panel2.Controls.Clear();

            Type t = controlObject.GetType();

            splitContainer2.Panel2.Controls.Add((System.Windows.Forms.Control)controlObject);

        }

        private void aCFoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parserGUI.XFoldControl ACFoldCont = new parserGUI.XFoldControl(true);
            setControlInPanel2(ACFoldCont);
            labelBreadCrumb.Text = ".: Select :: ACFold";
            ACFoldCont.Dock = DockStyle.Fill;
        }


        private void tFoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parserGUI.XFoldControl TFold = new parserGUI.XFoldControl(false);
            setControlInPanel2(TFold);
            labelBreadCrumb.Text = ".: Select :: TFold";
            TFold.Dock = DockStyle.Fill;
        }


        private void gOExGeneOntologyExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GO.GOControl gc = new GO.GOControl();
            setControlInPanel2(gc);
            labelBreadCrumb.Text = ".: Analyze :: GOEx (Gene Ontology Explorer)";
            gc.Dock = DockStyle.Fill;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox2 ab = new AboutBox2();
            ab.ShowDialog();
        }

        private void onlineBulletinBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get Rid of any controls
            WebBrowser wb = new WebBrowser();
            System.Uri theURI = new Uri("http://pcarvalho.com/patternlab/intranet/manual.shtml");
            wb.Url = theURI;
            setControlInPanel2(wb);
            wb.Dock = DockStyle.Fill;
        }


        private void trendQuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrendQuest.TrendQuestControl tc = new TrendQuest.TrendQuestControl();
            setControlInPanel2(tc);
            labelBreadCrumb.Text = ".: Analyze :: TrendQuest";
            tc.Dock = DockStyle.Fill;
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
            UpdateCheckInfo info;

            try
            {
                info = updateCheck.CheckForDetailedUpdate();

                if (info.UpdateAvailable)
                {
                    DialogResult dialogResult = MessageBox.Show("An update is available. Would you like to update the application now?", "Update available", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.OK)
                    {
                        updateCheck.Update();
                        MessageBox.Show("The application has been upgraded, and will now restart.");
                        Application.Restart();
                    }

                }
                else
                {
                    MessageBox.Show("No updates are available for the moment.");
                }
            }
            catch (DeploymentDownloadException dde)
            {
                MessageBox.Show(dde.Message + "\n" + dde.InnerException);
                return;
            }
            catch (InvalidDeploymentException ide)
            {
                MessageBox.Show(ide.Message + "\n" + ide.InnerException);
                return;
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message + "\n" + ioe.InnerException);
                return;
            }
            catch (Exception e10)
            {
                MessageBox.Show(e10.Message);
                return;
            }

        }

        private void buziosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Integration.ElementHost eh = new System.Windows.Forms.Integration.ElementHost();
            Buzios.ClusterControlWPF buzios = new Buzios.ClusterControlWPF();
            eh.Child = buzios;
            setControlInPanel2(eh);
            labelBreadCrumb.Text = ".: Analyze :: Buzios";
            eh.Dock = DockStyle.Fill;

        }

        private void anovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anova.AnovaControl ac = new Anova.AnovaControl();
            setControlInPanel2(ac);
            labelBreadCrumb.Text = ".: Select :: Anova";
            ac.Dock = DockStyle.Fill;
        }

        private void sEProRegrouperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regrouper.RegrouperControl regrouper = new Regrouper.RegrouperControl();
            setControlInPanel2(regrouper);
            labelBreadCrumb.Text = ".: Project Organization :: SEPro or PepExplorer";
            regrouper.Dock = DockStyle.Fill;
            
        }

        private void MainEntry_Load(object sender, EventArgs e)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            try
            {
                string[] stuff = Regex.Split(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationContext.Identity.FullName, ",");
                this.Text += " " + stuff[1];
            }
            catch
            {
                Console.WriteLine("Deployed version not detected.");
            }
        }

        private void massSpectraFileBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Lets put him in
            MSViewer.MSFileViewerWPF viewer = new MSViewer.MSFileViewerWPF();

            System.Windows.Forms.Integration.ElementHost eh = new System.Windows.Forms.Integration.ElementHost();
            Histogram.HistogramControl hc = new Histogram.HistogramControl();
            eh.Child = viewer;
            setControlInPanel2(eh);
            labelBreadCrumb.Text = ".: Utils :: Mass Spectra File Browser";
            eh.Dock = DockStyle.Fill;
        }

        private void generateSearchDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NCBIExtractor.DBPrepareControl db = new NCBIExtractor.DBPrepareControl();
            setControlInPanel2(db);
            labelBreadCrumb.Text = ".: Generate Search DB";
            db.Dock = DockStyle.Fill;
        }

        private void parseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regrouper.RegrouperControl regrouper = new Regrouper.RegrouperControl();
            setControlInPanel2(regrouper);
            labelBreadCrumb.Text = ".: Project Organization :: SEPro or PepExplorer";
            regrouper.Dock = DockStyle.Fill;
        }

        private void searchEngineProcessorSEProForPSMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SEProcessor.MainControl sepro = new SEProcessor.MainControl();
            setControlInPanel2(sepro);
            labelBreadCrumb.Text = ".: Filter :: Search Engine Processor (SEPro - for PSMs)";
            sepro.Dock = DockStyle.Fill;

        }

        private void pepExplorerforDeNovoSequencingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PepExplorer2.Forms.ProgramMainControl pepexplorer = new PepExplorer2.Forms.ProgramMainControl();
            labelBreadCrumb.Text = ".: Filter :: PepExplorer (for de novo sequencing)";
            setControlInPanel2(pepexplorer);
        }

        private void searchCometPSMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CometWrapper.CometWrapper cw = new CometWrapper.CometWrapper();
            setControlInPanel2(cw);
            labelBreadCrumb.Text = ".: Search (Comet PSM)";
            cw.Dock = DockStyle.Fill;
        }


        private void rawReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawReader.MainForm rr = new RawReader.MainForm();
            setControlInPanel2(rr);
            labelBreadCrumb.Text = ".: Utils :: RawReader";
            rr.Dock = DockStyle.Fill;
        }

        private void whatIsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.WhatIsNew wis = new Forms.WhatIsNew();
            wis.ShowDialog();
        }

        private void xDScoringPhosphoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XDScore.XDScoreControl xdscore = new XDScore.XDScoreControl();
            setControlInPanel2(xdscore);
            labelBreadCrumb.Text = ".: Utils :: XD Scoring (Phosphosites)";
            xdscore.Dock = DockStyle.Fill;
        }

        private void indexSparseMatrixLegacyFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SparseMatrixIndexLegacy smi = new SparseMatrixIndexLegacy();
            setControlInPanel2(smi);
            labelBreadCrumb.Text = ".: Utils :: IndexSparseMatrixLegacyFormat";
            smi.Dock = DockStyle.Fill;
        }

        private void xICBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XQuant.XICExplorer xicExplorer = new XQuant.XICExplorer();
            setControlInPanel2(xicExplorer);
            labelBreadCrumb.Text = ".: Quant :: XIC Browser";
            xicExplorer.Dock = DockStyle.Fill;
        }

        private void isobaricAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SEProQ.ITRAQ.ITRAQControl ic = new SEProQ.ITRAQ.ITRAQControl();
            setControlInPanel2(ic);
            labelBreadCrumb.Text = ".: Quant :: Isobaric Analyzer";
            ic.Dock = DockStyle.Fill;
        }

        private void troubleshootingAndUserForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://groups.google.com/forum/#!forum/patternlab");
        }

        private void histogramToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Integration.ElementHost eh = new System.Windows.Forms.Integration.ElementHost();
            Histogram.HistogramControl hc = new Histogram.HistogramControl();
            eh.Child = hc;
            setControlInPanel2(eh);
            labelBreadCrumb.Text = ".: Utils :: Histogram Tool";
            eh.Dock = DockStyle.Fill;
        }

        private void approximatelyAreaProportionalVennDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Venn.VennControl vc = new Venn.VennControl();
            //setControlInPanel2(vc);
            //labelBreadCrumb.Text = ".: Analyze :: Approximately area-proportional Venn Diagrams";
            //vc.Dock = DockStyle.Fill;

            System.Windows.Forms.Integration.ElementHost eh = new System.Windows.Forms.Integration.ElementHost();
            VennWPF2.Venn venn = new VennWPF2.Venn();
            eh.Child = venn;
            setControlInPanel2(eh);
            labelBreadCrumb.Text = ".: Analyze :: Approximately area-proportional Venn Diagrams";
            eh.Dock = DockStyle.Fill;
        }
    }
}
