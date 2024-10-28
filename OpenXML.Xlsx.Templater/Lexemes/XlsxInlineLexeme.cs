using ClosedXML.Excel;
using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record XlsxInlineLexeme: InlineLexeme
        
    {
        public XlsxInlineLexeme(StringSpan content) : base(content)
        { 
        }

        public IXLCell Cell { get; set; }
    }
}
