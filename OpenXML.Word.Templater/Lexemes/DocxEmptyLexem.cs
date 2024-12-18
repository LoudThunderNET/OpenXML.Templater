using NPOI.XWPF.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Word.Templater.Lexemes;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxEmptyLexem() : EmptyLexem(), IDocxLexem
    {
        /// <inheritdoc/>
        public IBodyElement? Element { get; set; }
    }
}
