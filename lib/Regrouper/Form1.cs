using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Regrouper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CheckForUpdate()
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

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckForUpdate();
        }
    }
}
