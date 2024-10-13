using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record InvertedSectionLexeme(StringSpan content) : Lexem(content)
    {
    }
}
