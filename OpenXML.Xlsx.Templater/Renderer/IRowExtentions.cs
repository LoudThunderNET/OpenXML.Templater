using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace OpenXML.Xlsx.Templater.Renderer
{
    internal static class IRowExtentions
    {
        internal static ICell GetOrAddCell(this IRow targetRow, int columnIndex)
        {
            return targetRow.GetCell(columnIndex) ?? targetRow.CreateCell(columnIndex);
        }
    }
}
