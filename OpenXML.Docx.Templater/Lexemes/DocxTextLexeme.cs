using NPOI.XWPF.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxTextLexeme: 
        TextLexeme, IDocxLexem
    {
        public DocxTextLexeme(StringSpan content) : 
            base(content)
        { 
        }

        /// <inheritdoc/>
        public required XWPFParagraph Element { get; set; }
    }
}
