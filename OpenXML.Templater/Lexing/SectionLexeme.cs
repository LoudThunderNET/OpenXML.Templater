using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record SectionLexeme(StringSpan content) : Lexem(content)
    {
    }
}
