using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace CometWrapper
{
    public partial class ModificationLibGUI : Form
    {

        ModificationLib ml = new ModificationLib();

        public List<Modification> MySelectedModification
        {
            get
            {
                List<Modification> modifications = new List<Modification>();

                for (int i = 0; i < dataGridViewModifications.Rows.Count; i++) {
                    if (dataGridViewModifications.Rows[i].Selected) {
                        Modification m = GetModFTable(i);
                        modifications.Add(m);
                    }
                }

                return modifications;
            }
        }

        private Modification GetModFTable(int i)
        {
            Modification m = new Modification(
                dataGridViewModifications.Rows[i].Cells[0].Value.ToString(),
                decimal.Parse(dataGridViewModifications.Rows[i].Cells[1].Value.ToString()),
                dataGridViewModifications.Rows[i].Cells[2].Value.ToString());
            return m;
        }


        public ModificationLibGUI()
        {
            InitializeComponent();

            try
            {
                StringReader s = new StringReader(Properties.Settings.Default.ModificationLib);

                XmlSerializer xmlSerializer = new XmlSerializer(ml.GetType());
                ml = (ModificationLib)xmlSerializer.Deserialize(s);

            }
            catch
            {
                //No default parameters exist
                Console.WriteLine("User has not yet established default parameters.");

                Modification mod1 = new Modification("Carbamidomethylation of Cysteine", 57.02146M, "C");
                Modification mod2 = new Modification("Oxidation of Methionine", 15.9949M, "M");
                Modification mod3 = new Modification("Phosphorilation", 79.9663M, "YST");
                Modification mod4 = new Modification("iTRAQ - 8", 304.2022M, "KY");
                Modification mod5 = new Modification("iTRAQ - 4", 144.1M, "KY");
                Modification mod6 = new Modification("TMT - 6", 229.1629M, "K");
                ml.MyModifications.AddRange(new List<Modification>() { mod1, mod2, mod3, mod4, mod5, mod6 });
            }

            UpdateTableWithModifications();
        }

        private void UpdateModificationsWithTable()
        {
            ml.MyModifications.Clear();

            for (int i = 0; i < dataGridViewModifications.Rows.Count -1 ; i++)
            {
                Modification m = GetModFTable(i);
                ml.MyModifications.Add(m);
            }
        }

        private void UpdateTableWithModifications()
        {

            dataGridViewModifications.Rows.Clear();

            foreach (Modification m in ml.MyModifications)
            {
                int row = dataGridViewModifications.Rows.Add();
                dataGridViewModifications.Rows[row].Cells[0].Value = m.Name;
                dataGridViewModifications.Rows[row].Cells[1].Value = m.MassShift.ToString();
                dataGridViewModifications.Rows[row].Cells[2].Value = m.Residues.ToString();
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateModificationsWithTable();

            try
            {
                StringWriter sw = new StringWriter();
                XmlSerializer xmlSerializer = new XmlSerializer(ml.GetType());
                //Remmove namespaces
                System.Xml.Serialization.XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
                xs.Add("", "");
                xmlSerializer.Serialize(sw, ml, xs);

                Properties.Settings.Default.ModificationLib = sw.ToString();
                Properties.Settings.Default.Save();


                MessageBox.Show("Your lib has been updated.");
            }
            catch (Exception e2)
            {
                MessageBox.Show("Error updating: " + e2.Message);
            }
        }

    }
}
