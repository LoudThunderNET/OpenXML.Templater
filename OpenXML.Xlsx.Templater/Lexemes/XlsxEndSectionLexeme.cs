using NPOI.SS.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    public record XlsxEndSectionLexeme:
        EndSectionLexeme, IXlsxLexem
    {
        public XlsxEndSectionLexeme(StringSpan content) 
            : base(content)
        { 
        }

        public ICell? Cell { get; set; }
    }
}
