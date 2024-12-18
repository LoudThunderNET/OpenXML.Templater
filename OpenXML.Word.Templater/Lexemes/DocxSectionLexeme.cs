using NPOI.XWPF.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;
using OpenXML.Word.Templater.Lexemes;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxSectionLexeme : 
        SectionLexeme, IDocxLexem
    {
        public DocxSectionLexeme(StringSpan content) : 
            base(content)
        { 
        }

        /// <inheritdoc/>
        public IBodyElement? Element { get; set; }
    }
}
