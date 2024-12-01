using NPOI.SS.UserModel;
using OpenXML.Templater.Lexing;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    public record XlsxOpenTagLexeme() : OpenTagLexeme(), IXlsxLexem
    {
        public ICell? Cell { get; set; }
    }
}
