using NPOI.XWPF.UserModel;
using OpenXML.Templater;
using OpenXML.Templater.Parsing.Nodes;
using OpenXML.Templater.Rederer;

namespace OpenXML.Word.Templater
{
    /// <inheritdoc cref="IRenderVisitor"/>
    internal class DocxRenderer(
        XWPFDocument doc,
        DataModel dataModel,
        string outputFileName) : IRenderVisitor
    {
        private XWPFDocument _targetDoc = new XWPFDocument();

        /// <inheritdoc/>
        public void Visit(HorizSectionNode node)
        {
        }

        /// <inheritdoc/>
        public void Visit(InlineNode node)
        {
        }

        /// <inheritdoc/>
        public void Visit(InvertedSectionNode node)
        {
        }

        /// <inheritdoc/>
        public void Visit(RootNode node)
        {
        }

        /// <inheritdoc/>
        public void Visit(SectionNode node)
        {
        }

        /// <inheritdoc/>
        public void Visit(TextNode node)
        {
        }

        /// <inheritdoc/>
        public void Visit(EmptyNode node)
        {
        }
    }
}
