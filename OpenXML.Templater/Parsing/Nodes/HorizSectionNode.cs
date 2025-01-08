using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class HorizSectionNode(HorizSectionLexeme content) : 
        SyntaxNode(content), IHasEnd
    {
        public EndSectionLexeme End { get; set; }

        public override void Accept(IRender renderVisitor)
        {
            renderVisitor.Render(this);
        }
    }
}
