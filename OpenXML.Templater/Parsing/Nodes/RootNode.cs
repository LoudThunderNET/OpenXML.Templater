using OpenXML.Templater.Rederer;
using System.Reflection;

namespace OpenXML.Templater.Parsing.Nodes
{
    public class RootNode : SyntaxNode
    {
        public override void Accept(IRender renderer)
        {
            renderer.Render(this);
        }
    }
}
