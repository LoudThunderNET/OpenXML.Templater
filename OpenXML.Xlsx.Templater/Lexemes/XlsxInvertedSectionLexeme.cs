using ClosedXML.Excel;
using OpenXML.Templater.Primitives;
using OpenXML.Xlsx.Templater.Lexemes;

namespace OpenXML.Templater.Lexing
{
    public record XlsxInvertedSectionLexeme(StringSpan content) : 
        InvertedSectionLexeme(content), IXlsxLexem
    {
        public IXLCell Cell { get; set; }
    }
}
