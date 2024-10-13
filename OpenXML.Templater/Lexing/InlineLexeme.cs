using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record InlineLexeme(StringSpan content) : Lexem(content)
    {
    }
}
