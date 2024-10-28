using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class HorizSectionNode : SyntaxNode
    {
        public HorizSectionNode(HorizSectionLexeme content) : base(content)
        { }

        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
