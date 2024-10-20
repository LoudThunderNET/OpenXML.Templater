using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class InlineNode : SyntaxNode
    {
        public InlineNode(StringSpan content) : base(content)
        { 
        }
    }
}
