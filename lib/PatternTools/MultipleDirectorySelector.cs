using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace PatternTools
{
    public partial class MultipleDirectorySelector : UserControl
    {
        public string VerifyExtension { get; set; }
        public MultipleDirectorySelector()
        {
            InitializeComponent();
            VerifyExtension = null;
        }


        public List<DirectoryClassDescription> MyDirectoryDescriptionDictionary
        {
            get
            {
                List<DirectoryClassDescription> myStuff = new List<DirectoryClassDescription>();
                int counter = 0;

                foreach (DataGridViewRow row in dataGridViewInput.Rows)
                {
                    
                    if (row.Cells[0].Value != null)
                    {
                        counter++;
                        string description = "";
                        if (row.Cells[1].Value != null) {
                            description = row.Cells[1].Value.ToString();
                        }
                        myStuff.Add(new DirectoryClassDescription(row.Cells[0].Value.ToString(), counter, description));
                    }
                }
                return myStuff;
            }
        }

        private List<string> MyDirectories
        {
            get
            {
                List<string> directories = new List<string>();
                foreach (DataGridViewRow row in dataGridViewInput.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        directories.Add(row.Cells[0].Value.ToString());
                    }
                }
                return directories;

            }
        }

        private void buttonAddDirectory_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = false;

            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                //Add row to matrix
                int index = dataGridViewInput.Rows.Add();
                dataGridViewInput.Rows[index].Cells[0].Value = folderBrowserDialog1.SelectedPath;
                DirectoryInfo di = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                dataGridViewInput.Rows[index].Cells[1].Value = di.Name;
                VerifyCell(index);
            }
        }


        private void dataGridViewInput_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridViewInput.Rows[e.RowIndex].Cells[0].Value != null && dataGridViewInput.Rows[e.RowIndex].Cells[1].Value == null)
            {
                DirectoryInfo di = new DirectoryInfo(dataGridViewInput.Rows[e.RowIndex].Cells[0].Value.ToString());
                dataGridViewInput.Rows[e.RowIndex].Cells[1].Value = di.Name;
            }
            VerifyCell(e.RowIndex);
        }

        private void VerifyCell(int rowIndex)
        {
            //get the cell content
            try
            {
                if (rowIndex >= 0)
                {
                    //Check if directory is a number
                    DirectoryInfo di = new DirectoryInfo(dataGridViewInput.Rows[rowIndex].Cells[0].Value.ToString());

                    //Verify if we haven't already added this directory
                    if (MyDirectories.FindAll(a => a.Equals(di.FullName)).Count() > 1)
                    {
                        MessageBox.Show("This directory has already been included\n" + di.FullName);
                        dataGridViewInput.Rows.RemoveAt(rowIndex);
                        return;
                    }

                    if (VerifyExtension != null)
                    {
                        Console.WriteLine("Files having extension " + VerifyExtension + " found in directory: " + di.Name);
                        List<FileInfo> files = di.GetFiles(VerifyExtension, SearchOption.AllDirectories).ToList();

                        foreach (FileInfo fi in files)
                        {
                            Console.WriteLine(fi.FullName);
                        }

                        Console.WriteLine("");

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }



    }
}
