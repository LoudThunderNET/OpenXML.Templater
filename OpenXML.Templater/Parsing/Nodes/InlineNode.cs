using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class InlineNode(InlineLexeme content) : SyntaxNode(content)
    {
        public override void Accept(IRender renderer)
        {
            renderer.Render(this);
        }
    }
}
