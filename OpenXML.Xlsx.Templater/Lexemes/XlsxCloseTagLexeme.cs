using ClosedXML.Excel;
using OpenXML.Templater.Parsing;
using OpenXML.Xlsx.Templater.Lexemes;

namespace OpenXML.Templater.Lexing
{
    public record XlsxCloseTagLexeme() : CloseTagLexeme(), IXlsxLexem
    {
        public IXLCell Cell { get; set; }
    }
}
