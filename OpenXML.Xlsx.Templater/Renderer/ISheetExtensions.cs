using NPOI.SS.UserModel;

namespace OpenXML.Xlsx.Templater.Renderer
{
    internal static class ISheetExtensions
    {
        internal static IRow GetOrAddRow(this ISheet sheet, int rowIndex)
        {
            IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);

            return row!;
        }

        internal static bool IsInMergedRegion(this ISheet sheet, ICell cell) =>
            sheet.MergedRegions.Any(mr => mr.IsInRange(cell.RowIndex, cell.ColumnIndex));
    }
}
