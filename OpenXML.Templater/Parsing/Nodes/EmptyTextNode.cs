using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class EmptyNode(Lexem lexem) : SyntaxNode(lexem)
    {
        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
