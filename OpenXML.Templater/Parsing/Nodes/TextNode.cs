using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class TextNode : SyntaxNode
    {
        public TextNode(StringSpan content) : base(content) 
        { }
    }
}
