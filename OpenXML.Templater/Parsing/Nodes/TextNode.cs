using OpenXML.Templater.Lexing;
using OpenXML.Templater.Primitives;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class TextNode : SyntaxNode
    {
        public TextNode(Lexem content) : base(content) 
        { }

        public override void Accept(IRenderVisitor renderVisitor)
        {
            renderVisitor.Visit(this);
        }
    }
}
