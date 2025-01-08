using NPOI.XWPF.UserModel;
using OpenXML.Templater.Lexing;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxEmptyLexem() : EmptyLexem(), IDocxLexem
    {
        /// <inheritdoc/>
        public required XWPFParagraph Element { get; set; }
    }
}
