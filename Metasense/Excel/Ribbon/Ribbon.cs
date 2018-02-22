using System.Runtime.InteropServices;
using ExcelDna.Integration.CustomUI;
using Microsoft.Office.Interop.Excel;
using Metasense;
using Metasense.Tabular;

namespace Metasense.Excel.Ribbon
{
    [ComVisible(true)]
    public class Ribbon : ExcelRibbon
    {
        /// <summary>
        /// View the currently selected object
        /// </summary>
        /// <param name="control"></param>
        public void ViewObject(IRibbonControl control)
        {
            var app = (Application)ExcelDna.Integration.ExcelDnaUtil.Application;
            var activeCellText = (string)app.ActiveCell.Value2;

            if (activeCellText.ToUpper().Contains("TBL"))
            {
                //Get the table
                var table = (Table)ObjectStore.Get(activeCellText);

                //Show it
                var viewer = new TableViewerForm();
                viewer.ShowTable(table);
                viewer.Visible = true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Only the following types can be viewed : 'TBL'");
            }
        }
    }
}