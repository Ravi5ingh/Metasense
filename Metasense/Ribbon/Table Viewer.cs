using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metasense.Ribbon
{
    public partial class TableViewerForm : Form
    {
        private int tableRows = 10;

        public TableViewerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Render the given table
        /// </summary>
        /// <param name="table"></param>
        public void ShowTable(Table table)
        {
            var data = table.Data;
            var numCols = data.GetLength(1);
            for(var j = 0; j < numCols; j++)
            {
                var column = new DataGridViewTextBoxColumn();
                column.HeaderText = data[0, j].ToString();
                column.Name = "Column [" + data[0, j].ToString() + "]";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                viewGrid.Columns.Add(column);
            }

            var numRows = System.Math.Min(tableRows, data.GetLength(0));
            for(var i = 1; i < numRows; i++)
            {
                var row = new Object[numCols];
                for (var j = 0; j < numCols; j++)
                {
                    row[j] = data[i, j];
                }
                viewGrid.Rows.Add(row);
            }
            if(numRows == tableRows)
            {
                viewGrid.Rows.Add("...");
            }
        }
    }
}
