using OpenXML.Docx.Templater.Lexemes;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Docx.Templater
{
    /// <inheritdoc cref="ILexemeFactory"/>
    internal class DocxLexemeFactory : ILexemeFactory
    {
        public CloseTagLexeme CreateCloseTagLexeme() =>
            new DocxCloseTagLexeme();

        public EmptyLexem CreateEmptyLexeme() =>
            new DocxEmptyLexem();

        public EndSectionLexeme CreateEndSectionLexeme(StringSpan content) =>

        public HorizSectionLexeme CreateHorizSectionLexeme(StringSpan content)
        {
            throw new NotImplementedException();
        }

        public InlineLexeme CreateInlineLexeme(StringSpan content)
        {
            throw new NotImplementedException();
        }

        public InvertedSectionLexeme CreateInvertedSectionLexeme(StringSpan content)
        {
            throw new NotImplementedException();
        }

        public OpenTagLexeme CreateOpenTagLexeme()
        {
            throw new NotImplementedException();
        }

        public SectionLexeme CreateSectionLexeme(StringSpan content)
        {
            throw new NotImplementedException();
        }

        public TextLexeme CreateTextLexeme(StringSpan content)
        {
            throw new NotImplementedException();
        }
    }
}
