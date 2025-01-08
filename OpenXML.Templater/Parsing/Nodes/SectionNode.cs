using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;
using System.Reflection;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class SectionNode(SectionLexeme content) : SyntaxNode(content), IHasEnd
    {
        public EndSectionLexeme End { get; set; }
        public override void Accept(IRender renderer)
        {
            renderer.Render(this);
        }
    }
}
