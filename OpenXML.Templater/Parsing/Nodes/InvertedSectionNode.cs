using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class InvertedSectionNode : SyntaxNode
    {
        public InvertedSectionNode(InvertedSectionLexeme content) :base(content)
        { 
        }

        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
