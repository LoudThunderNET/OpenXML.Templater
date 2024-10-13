using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record EmptyContent() : Lexem(StringSpan.Empty())
    {
    }
}
