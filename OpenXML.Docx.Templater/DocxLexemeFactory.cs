using OpenXML.Docx.Templater.Lexemes;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Docx.Templater
{
    /// <summary>
    /// <inheritdoc cref="ILexemeFactory"/> для шаблонизатора docx.
    /// </summary>
    internal class DocxLexemeFactory : ILexemeFactory
    {
        /// <inheritdoc/>
        public CloseTagLexeme CreateCloseTagLexeme() =>
            new DocxCloseTagLexeme()
            { 
                Element  = null!
            };

        /// <inheritdoc/>
        public EmptyLexem CreateEmptyLexeme() =>
            new DocxEmptyLexem()
            { 
                Element = null!
            };

        /// <inheritdoc/>
        public EndSectionLexeme CreateEndSectionLexeme(StringSpan content) =>
            new DocxEndSectionLexeme(content) { Element = null! };

        /// <inheritdoc/>
        public HorizSectionLexeme CreateHorizSectionLexeme(StringSpan content) =>
            new DocxHorizSectionLexeme(content) { Element = null! };

        /// <inheritdoc/>
        public InlineLexeme CreateInlineLexeme(StringSpan content) =>
            new DocxInlineLexeme(content) { Element = null! };

        /// <inheritdoc/>
        public InvertedSectionLexeme CreateInvertedSectionLexeme(StringSpan content) =>
            new DocxInvertedSectionLexeme(content) { Element = null! };

        /// <inheritdoc/>
        public OpenTagLexeme CreateOpenTagLexeme() =>
            new DocxOpenTagLexeme() { Element = null! };

        /// <inheritdoc/>
        public SectionLexeme CreateSectionLexeme(StringSpan content) =>
            new DocxSectionLexeme(content) { Element = null! };

        /// <inheritdoc/>
        public TextLexeme CreateTextLexeme(StringSpan content) =>
            new DocxTextLexeme(content) { Element = null! };
    }
}
