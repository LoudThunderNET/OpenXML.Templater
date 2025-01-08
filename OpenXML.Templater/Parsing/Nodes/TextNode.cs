using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;
using System.Reflection;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class TextNode(Lexem content) : 
        SyntaxNode(content)
    {
        public override void Accept(IRender renderer)
        {
            renderer.Render(this);
        }
    }
}
