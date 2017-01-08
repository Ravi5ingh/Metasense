using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metasense.ObjectSpace
{
    public partial class LoadObjectForm : Form
    {
        public LoadObjectForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            switch(objectTypeComboBox.Text)
            {
                case "Table (#TBL)":
                    var table = LoadTable(new FileInfo(filePathTextBox.Text));
                    break;
                case "Neural Network (#NNT)":
                    MessageBox.Show("No Load Implementation for Neural Networks yet");
                    break;
                default:
                    MessageBox.Show("Internal Error : No code path available for " + objectTypeComboBox.Text + ". Please report to ravisingh1203@hotmail.com");
                    break;
            }
        }

        /// <summary>
        /// Load a table from a file
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private static Table LoadTable(FileInfo fileInfo)
        {
            Table table;
            switch (fileInfo.Extension.ToUpper())
            {
                case ".CSV":
                    table = Table.LoadFromCSV(fileInfo);
                    break;
                default:
                    throw new Exception("File extension : " + fileInfo.Extension + " not recognized");
            }

            return table;
        }
    }
}
