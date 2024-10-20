using OpenXML.Templater.Parsing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public abstract record Lexem(StringSpan Content)
    {
        public abstract void Accept(Parser parser);
    }
}
