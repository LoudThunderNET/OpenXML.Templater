using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record HorizSectionLexeme(StringSpan content) : Lexem(content)
    {
    }
}
