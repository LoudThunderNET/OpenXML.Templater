using NPOI.XWPF.UserModel;
using OpenXML.Templater.Lexing;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxOpenTagLexeme() : OpenTagLexeme(), IDocxLexem
    {
        /// <inheritdoc/>
        public required XWPFParagraph Element { get; set; }
    }
}
