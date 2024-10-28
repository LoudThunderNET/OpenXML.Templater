using ClosedXML.Excel;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    internal interface IXlsxLexem
    {
        IXLCell Cell { get; set; }
    }
}
