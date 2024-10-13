using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record EndSectionLexeme(StringSpan content) : Lexem(content)
    {
    }
}
