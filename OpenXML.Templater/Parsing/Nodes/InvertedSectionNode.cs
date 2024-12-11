using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class InvertedSectionNode : SyntaxNode, IHasEnd
    {
        public InvertedSectionNode(InvertedSectionLexeme content) :base(content)
        { 
        }

        public EndSectionLexeme End { get; set; }

        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
