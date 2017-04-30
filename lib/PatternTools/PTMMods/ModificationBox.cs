using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace PatternTools.PTMMods
{
    public partial class ModificationBox : UserControl
    {
        public ModificationBox()
        {
            InitializeComponent();
        }

        List<ModificationItem> myModifications = new List<ModificationItem>();

        public List<ModificationItem> MyModifications
        {
            get
            {
                UpdateModificationArray();
                return myModifications;
            }

            set
            {
                myModifications = value;
            }
        }

        private void UpdateModificationArray()
        {
            try
            {
                List<ModificationItem> tmp = new List<ModificationItem>();
                for (int i = 0; i < dataGridViewPTMs.Rows.Count - 1; i++)
                {
                    string aminoAcid = dataGridViewPTMs.Rows[i].Cells[2].Value.ToString();
                    string description = dataGridViewPTMs.Rows[i].Cells[1].Value.ToString();
                    double deltaMass = double.Parse(dataGridViewPTMs.Rows[i].Cells[3].Value.ToString());

                    bool isVariable = false;
                    if (dataGridViewPTMs.Rows[i].Cells[4].Value != null)
                    {
                        if ((bool)dataGridViewPTMs.Rows[i].Cells[4].Value != false)
                        {
                            isVariable = true;
                        }
                    }
                    else
                    {
                        isVariable = false;
                    }

                    bool onlyCTerminus = false;
                    if (dataGridViewPTMs.Rows[i].Cells[6].Value != null)
                    {
                        if ((bool)dataGridViewPTMs.Rows[i].Cells[6].Value != false)
                        {
                            onlyCTerminus = true;
                        }
                    }
                    else
                    {
                        onlyCTerminus = false;
                    }

                    bool onlyNTerminus = false;
                    if (dataGridViewPTMs.Rows[i].Cells[5].Value != null)
                    {
                        if ((bool)dataGridViewPTMs.Rows[i].Cells[5].Value != false)
                        {
                            onlyNTerminus = true;
                        }
                    }
                    else
                    {
                        onlyNTerminus = false;
                    }

                    bool active = false;
                    if (dataGridViewPTMs.Rows[i].Cells[0].Value != null)
                    {
                        if ((bool)dataGridViewPTMs.Rows[i].Cells[0].Value != false)
                        {
                            active = true;
                        }
                    }
                    else
                    {
                        active = false;
                    }

                    ModificationItem m = new ModificationItem(aminoAcid, description, deltaMass, onlyCTerminus, onlyNTerminus, isVariable);
                    m.IsActive = active;

                    tmp.Add(m);
                }
                myModifications = tmp;
            }
            catch
            {
                Console.WriteLine("Problems updating modificationn array");
            }
        }

        private void SaveTableAsDefault()
        {
            try
            {
                //update the array
                UpdateModificationArray();

                StringWriter sw = new StringWriter();
                XmlSerializer xmlSerializer = new XmlSerializer(myModifications.GetType());
                //Remmove namespaces
                System.Xml.Serialization.XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
                xs.Add("", "");
                xmlSerializer.Serialize(sw, myModifications, xs);

                string serializedObject = sw.ToString();
                PatternTools.Properties.Settings.Default.ModificationTable = serializedObject;
                PatternTools.Properties.Settings.Default.Save();

                Console.WriteLine("Your lib has been updated.");
            }
            catch (Exception e2)
            {
                Console.WriteLine("Error updating modification box: " + e2.Message);
            }
        }

        public void LoadDefaults()
        {
            try
            {

                StringReader s = new StringReader(PatternTools.Properties.Settings.Default.ModificationTable);

                XmlSerializer xmlSerializer = new XmlSerializer(myModifications.GetType());
                myModifications = (List<ModificationItem>)xmlSerializer.Deserialize(s);

                FillTableWith(myModifications);
                Console.WriteLine("Modification table successfuly deserialized");

            }
            catch
            {
                Console.WriteLine("Problems loading serialized defaults.  Loading software defaults");
                FillTableWith(PatternTools.PTMMods.DefaultModifications.TheModifications);
            }

        }

        private void FillTableWith(List<ModificationItem> list)
        {
            dataGridViewPTMs.Rows.Clear();
            myModifications = list;

            foreach (ModificationItem m in list)
            {
                int r = dataGridViewPTMs.Rows.Add();
                dataGridViewPTMs.Rows[r].Cells[0].Value = m.IsActive;
                dataGridViewPTMs.Rows[r].Cells[1].Value = m.Description;
                dataGridViewPTMs.Rows[r].Cells[2].Value = m.AminoAcid;
                dataGridViewPTMs.Rows[r].Cells[3].Value = m.DeltaMass;
                dataGridViewPTMs.Rows[r].Cells[4].Value = m.IsVariable;
                dataGridViewPTMs.Rows[r].Cells[5].Value = m.OnlyNTerminus;
                dataGridViewPTMs.Rows[r].Cells[6].Value = m.OnlyCTerminus;

            }

        }

        private void buttonUpdateLib_Click(object sender, EventArgs e)
        {
            SaveTableAsDefault();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillTableWith(PatternTools.PTMMods.DefaultModifications.TheModifications);
        }

        private void dataGridViewPTMs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
