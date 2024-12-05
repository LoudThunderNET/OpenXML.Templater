using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;
using OpenXML.Xlsx.Templater.Lexemes;

namespace OpenXML.Xlsx.Templater
{
    public class XlsxLexemeFactory : ILexemeFactory
    {
        public CloseTagLexeme CreateCloseTagLexeme() => new XlsxCloseTagLexeme();

        public EmptyLexem CreateEmptyLexeme() => new XlsxEmptyLexem();

        public EndSectionLexeme CreateEndSectionLexeme(StringSpan content) => 
            new XlsxEndSectionLexeme(content);

        public HorizSectionLexeme CreateHorizSectionLexeme(StringSpan content) => 
            new XlsxHorizSectionLexeme(content);

        public InlineLexeme CreateInlineLexeme(StringSpan content) => 
            new XlsxInlineLexeme(content);

        public InvertedSectionLexeme CreateInvertedSectionLexeme(StringSpan content) =>
            new XlsxInvertedSectionLexeme(content);

        public OpenTagLexeme CreateOpenTagLexeme() => new XlsxOpenTagLexeme();

        public SectionLexeme CreateSectionLexeme(StringSpan content) => 
            new XlsxSectionLexeme(content);

        public TextLexeme CreateTextLexeme(StringSpan content) =>
            new XlsxTextLexeme(content);
    }
}
