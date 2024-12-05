using NPOI.SS.UserModel;
using OpenXML.Templater.Lexing;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    public record XlsxEmptyLexem() : EmptyLexem(), IXlsxLexem
    {
        public ICell? Cell { get; set; }
    }
}
