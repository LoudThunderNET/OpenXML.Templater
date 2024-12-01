using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class RootNode : SyntaxNode
    {
        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
