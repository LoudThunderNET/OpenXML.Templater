using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class EmptyNode : SyntaxNode
    {
        public EmptyNode(Lexem lexem) : base(lexem) 
        { }

        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
