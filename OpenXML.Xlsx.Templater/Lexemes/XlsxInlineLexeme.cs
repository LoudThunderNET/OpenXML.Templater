using NPOI.SS.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    public record XlsxInlineLexeme: InlineLexeme, IXlsxLexem
    {
        public XlsxInlineLexeme(StringSpan content) : base(content)
        { 
        }

        public ICell? Cell { get; set; }
    }
}
