using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXML.Templater.Parsing
{
    public class Parser
    {
        private SyntaxNode _root;
        private SyntaxNode _currentNode;
        private Stack<SyntaxNode> _prevNode;
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

        public void Visit(OpenTagLexeme openTagLexeme)
        {

        }

        public void Visit(CloseTagLexeme closeTagLexeme)
        { 

        }

        public void Visit(EmptyContent  emptyContent)
        {
            _currentNode.Children.Add(new TextNode(string.Empty));
        }

        public void Visit(EndSectionLexeme endSectionLexeme)
        {
            _currentNode = _prevNode.Pop();
        }
        
        public void Visit(HorizSectionLexeme horizSectionLexeme)
        {
            _prevNode.Push(_currentNode);
            var horizSectionNode = new HorizSectionNode(horizSectionLexeme.Content);
            _currentNode.Children.Add(horizSectionNode);
            _currentNode = horizSectionNode;
        }

        public void Visit(InlineLexeme inlineLexeme)
        {
            _currentNode.Children.Add(new InlineNode(inlineLexeme.Content));
        }
        
        public void Visit(InvertedSectionLexeme invertedSectionLexeme)
        {
            _prevNode.Push(_currentNode);
            var invertedSectionNode = new InvertedSectionNode(invertedSectionLexeme.Content);
            _currentNode.Children.Add(invertedSectionNode);
            _currentNode = invertedSectionNode;
        }
        
        public void Visit(SectionLexeme sectionLexeme)
        {
            _prevNode.Push(_currentNode);
            var sectionNode = new SectionNode(sectionLexeme.Content);
            _currentNode.Children.Add(sectionNode);
            _currentNode = sectionNode;
        }

        public void Visit(TextLexeme textLexeme)
        { 
            _currentNode.Children.Add(new TextNode(textLexeme.Content));
        }
    }
}
