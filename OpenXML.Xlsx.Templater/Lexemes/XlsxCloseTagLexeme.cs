using OpenXML.Xlsx.Templater.Lexemes;
using NPOI.SS.UserModel;
using OpenXML.Templater.Lexing;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    public record XlsxCloseTagLexeme() : CloseTagLexeme(), IXlsxLexem
    {
        public ICell? Cell { get; set; }
    }
}
