using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class InlineNode : SyntaxNode
    {
        public InlineNode(InlineLexeme content) : base(content)
        { 
        }

        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
