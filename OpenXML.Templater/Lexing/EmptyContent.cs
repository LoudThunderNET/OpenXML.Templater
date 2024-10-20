using OpenXML.Templater.Parsing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record EmptyContent() : Lexem(StringSpan.Empty())
    {
        public override void Accept(Parser parser)
        {
            parser.Visit(this);
        }
    }
}
