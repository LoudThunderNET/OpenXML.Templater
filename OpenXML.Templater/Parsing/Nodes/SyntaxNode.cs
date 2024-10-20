using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Parsing.Nodes
{
    public abstract class SyntaxNode
    {
        public StringSpan Content { get; set; }
        protected SyntaxNode() : this(new List<SyntaxNode>()) 
        { 
        }

        protected SyntaxNode(StringSpan content) : this(content, new List<SyntaxNode>())
        { 
        }

        protected SyntaxNode(ICollection<SyntaxNode> children):this(StringSpan.Empty(), children)
        {
        }

        protected SyntaxNode(StringSpan content, ICollection<SyntaxNode> children)
        {
            Children = children;
            Content = content;
        }

        public ICollection<SyntaxNode> Children { get; }
    }
}
