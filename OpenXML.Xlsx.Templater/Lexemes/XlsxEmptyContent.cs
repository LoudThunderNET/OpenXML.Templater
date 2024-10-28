using ClosedXML.Excel;
using OpenXML.Templater.Parsing;
using OpenXML.Xlsx.Templater.Lexemes;

namespace OpenXML.Templater.Lexing
{
    public record XlsxEmptyContent() : EmptyContent(), IXlsxLexem
    {
        public IXLCell Cell { get; set; }
    }
}
