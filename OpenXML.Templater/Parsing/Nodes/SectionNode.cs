using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class SectionNode : SyntaxNode, IHasEnd
    {
        public SectionNode(SectionLexeme content) : base(content)
        {
        }

        public EndSectionLexeme End { get; set; }
        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
