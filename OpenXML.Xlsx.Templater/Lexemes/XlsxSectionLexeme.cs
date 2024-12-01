using NPOI.SS.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    public record XlsxSectionLexeme : 
        SectionLexeme, IXlsxLexem
    {
        public XlsxSectionLexeme(StringSpan content) : 
            base(content)
        { 
        }

        public ICell? Cell { get; set; }
    }
}
