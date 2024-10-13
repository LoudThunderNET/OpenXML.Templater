using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record TextLexeme(StringSpan content) : Lexem(content)
    {
    }
}
