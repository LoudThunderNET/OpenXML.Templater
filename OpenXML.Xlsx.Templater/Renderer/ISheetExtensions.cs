using NPOI.SS.UserModel;

namespace OpenXML.Xlsx.Templater.Renderer
{
    internal static class ISheetExtensions
    {
        internal static IRow GetOrAddRow(this ISheet sheet, int rowIndex)
        {
            IRow row = sheet.GetRow(rowIndex);
            if(row == null)
                row = sheet.CreateRow(rowIndex);

            return row!;
        }
    }
}
