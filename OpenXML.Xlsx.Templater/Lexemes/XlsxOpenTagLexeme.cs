using ClosedXML.Excel;
using OpenXML.Xlsx.Templater.Lexemes;

namespace OpenXML.Templater.Lexing
{
    public record XlsxOpenTagLexeme() : OpenTagLexeme(), IXlsxLexem
    {
        public IXLCell Cell { get; set; }
    }
}
