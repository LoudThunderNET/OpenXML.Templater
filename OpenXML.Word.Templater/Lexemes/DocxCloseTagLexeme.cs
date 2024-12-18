using OpenXML.Word.Templater.Lexemes;
using OpenXML.Templater.Lexing;
using NPOI.XWPF.UserModel;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxCloseTagLexeme() : CloseTagLexeme(), IDocxLexem
    {
        /// <inheritdoc/>
        public IBodyElement? Element { get; set; }
    }
}
