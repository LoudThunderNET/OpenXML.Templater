using OpenXML.Templater.Lexing;
using OpenXML.Templater.Rederer;

namespace OpenXML.Templater.Parsing.Nodes
{
    public abstract class SyntaxNode
    {
        public Lexem? Lexem { get; set; }
        protected SyntaxNode() : this(new List<SyntaxNode>()) 
        { 
        }

        protected SyntaxNode(Lexem lexem) : this(lexem, new List<SyntaxNode>())
        { 
        }

        protected SyntaxNode(ICollection<SyntaxNode> children):this(null, children)
        {
        }

        protected SyntaxNode(Lexem? lexem, ICollection<SyntaxNode> children)
        {
            Children = children;
            Lexem = lexem;
        }

        public ICollection<SyntaxNode> Children { get; }

        public abstract void Accept(IRender renderVisitor);
    }
}
