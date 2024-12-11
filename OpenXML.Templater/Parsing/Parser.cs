using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing.Nodes;

namespace OpenXML.Templater.Parsing
{
    public class Parser
    {
        private readonly SyntaxNode _root;
        private readonly Stack<SyntaxNode> _prevNode;
        private SyntaxNode _currentNode;
        public Parser() 
        {
            _root = new RootNode();
            _currentNode = _root;
            _prevNode = new Stack<SyntaxNode>();
        }

        public SyntaxNode Root => _root;

        public void Parse(IEnumerable<Lexem> lexemes)
        {
            foreach (var lexem in lexemes)
            {
                lexem.Accept(this);
            }
        }

        public void Visit(EmptyLexem  emptyLexem)
        {
            _currentNode.Children.Add(new EmptyNode(emptyLexem));
        }

        public void Visit(EndSectionLexeme endSectionLexeme)
        {
            if (_currentNode is IHasEnd hasEndNode)
            {
                hasEndNode.End = endSectionLexeme;
            }
            _currentNode = _prevNode.Pop();
        }
        
        public void Visit(HorizSectionLexeme horizSectionLexeme)
        {
            _prevNode.Push(_currentNode);
            var horizSectionNode = new HorizSectionNode(horizSectionLexeme);
            _currentNode.Children.Add(horizSectionNode);
            _currentNode = horizSectionNode;
        }

        public void Visit(InlineLexeme inlineLexeme)
        {
            _currentNode.Children.Add(new InlineNode(inlineLexeme));
        }
        
        public void Visit(InvertedSectionLexeme invertedSectionLexeme)
        {
            _prevNode.Push(_currentNode);
            var invertedSectionNode = new InvertedSectionNode(invertedSectionLexeme);
            _currentNode.Children.Add(invertedSectionNode);
            _currentNode = invertedSectionNode;
        }
        
        public void Visit(SectionLexeme sectionLexeme)
        {
            _prevNode.Push(_currentNode);
            var sectionNode = new SectionNode(sectionLexeme);
            _currentNode.Children.Add(sectionNode);
            _currentNode = sectionNode;
        }

        public void Visit(TextLexeme textLexeme)
        { 
            _currentNode.Children.Add(new TextNode(textLexeme));
        }
    }
}
