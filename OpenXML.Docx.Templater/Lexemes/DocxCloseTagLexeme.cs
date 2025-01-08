using OpenXML.Templater.Lexing;
using NPOI.XWPF.UserModel;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxCloseTagLexeme() : CloseTagLexeme(), IDocxLexem
    {
        /// <inheritdoc/>
        public required XWPFParagraph Element { get; set; }
    }
}
