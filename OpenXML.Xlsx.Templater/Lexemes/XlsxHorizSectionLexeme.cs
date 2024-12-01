using NPOI.SS.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    public record XlsxHorizSectionLexeme: 
        HorizSectionLexeme, IXlsxLexem
    {
        public XlsxHorizSectionLexeme(StringSpan content) : 
            base(content)
        { 
        }

        public ICell? Cell { get; set; }
    }
}
