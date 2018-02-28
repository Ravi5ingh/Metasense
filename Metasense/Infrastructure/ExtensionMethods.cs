using ExcelDna.Integration;
using Microsoft.Office.Interop.Excel;

namespace Metasense.Infrastructure
{
    public static class ExtensionMethods
    {
        public static int GetNumRows(this ExcelReference callingRange)
        {
            return callingRange.RowLast - callingRange.RowFirst + 1;
        }

        public static int GetNumCols(this ExcelReference callingRange)
        {
            return callingRange.ColumnLast - callingRange.ColumnFirst + 1;
        }

        public static string GetFormula(this ExcelReference callingRange)
        {
            var formula =
            ((Range) ((Worksheet) ((Application) ExcelDnaUtil.Application).ActiveWorkbook.ActiveSheet).Cells[
                callingRange.RowFirst + 1, callingRange.ColumnFirst + 1]).Formula as string;
            return formula.Split('=', '(')[1];
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
