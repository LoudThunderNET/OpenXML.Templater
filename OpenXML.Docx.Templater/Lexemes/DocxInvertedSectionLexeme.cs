using NPOI.XWPF.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxInvertedSectionLexeme : 
        InvertedSectionLexeme, IDocxLexem
    {
        public DocxInvertedSectionLexeme(StringSpan content) : 
            base(content)
        { 
        }

        /// <inheritdoc/>
        public required XWPFParagraph Element { get; set; }
    }
}
