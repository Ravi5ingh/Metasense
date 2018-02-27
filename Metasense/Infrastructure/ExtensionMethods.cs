using ExcelDna.Integration;

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

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
