using OpenXML.Templater.Parsing.Nodes;

namespace OpenXML.Templater.Rederer
{
    public interface IRenderVisitor
    {
        void Visit(HorizSectionNode node);
        void Visit(InlineNode node);
        void Visit(InvertedSectionNode node);
        void Visit(RootNode node);
        void Visit(SectionNode node);
        void Visit(TextNode node);
    }
}