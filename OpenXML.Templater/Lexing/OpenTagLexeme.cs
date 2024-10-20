using OpenXML.Templater.Parsing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record OpenTagLexeme() : Lexem(StringSpan.Empty())
    {
        public override void Accept(Parser parser)
        {
            parser.Visit(this);
        }
    }
}
