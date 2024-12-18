using NPOI.XWPF.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;
using OpenXML.Word.Templater.Lexemes;

namespace OpenXML.Docx.Templater.Lexemes
{
    public record DocxInlineLexeme: InlineLexeme, IDocxLexem
    {
        public DocxInlineLexeme(StringSpan content) : base(content)
        { 
        }

        /// <inheritdoc/>
        public IBodyElement? Element { get; set; }
    }
}
