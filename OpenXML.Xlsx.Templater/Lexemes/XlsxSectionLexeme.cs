using ClosedXML.Excel;
using OpenXML.Templater.Primitives;
using OpenXML.Xlsx.Templater.Lexemes;

namespace OpenXML.Templater.Lexing
{
    public record XlsxSectionLexeme(StringSpan content) : 
        SectionLexeme(content), IXlsxLexem
    {
        public IXLCell Cell { get; set; }
    }
}
