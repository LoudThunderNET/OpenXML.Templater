using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    /// <inheritdoc cref="ILexemeFactory"/>
    public class DefaultLexemeFactory : ILexemeFactory
    {
        public CloseTagLexeme CreateCloseTagLexeme() => new();

        public EmptyContent CreateEmptyContent() => new();

        public EndSectionLexeme CreateEndSectionLexeme(StringSpan content) => 
            new(content);

        public HorizSectionLexeme CreateHorizSectionLexeme(StringSpan content) => 
            new(content);

        public InlineLexeme CreateInlineLexeme(StringSpan content) => 
            new(content);

        public InvertedSectionLexeme CreateInvertedSectionLexeme(StringSpan content) => 
            new(content);

        public OpenTagLexeme CreateOpenTagLexeme() => 
            new();

        public SectionLexeme CreateSectionLexeme(StringSpan content) => 
            new(content);

        public TextLexeme CreateTextLexeme(StringSpan content) => 
            new(content);
    }
}
