using ClosedXML.Excel;
using OpenXML.Templater.Parsing;
using OpenXML.Templater.Primitives;
using OpenXML.Xlsx.Templater.Lexemes;

namespace OpenXML.Templater.Lexing
{
    public record XlsxEndSectionLexeme(StringSpan content) : 
        EndSectionLexeme(content), IXlsxLexem
    {
        public IXLCell Cell { get; set; }
    }
}
