using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.Model;
using NPOI.XWPF.UserModel;
using OpenXML.Docx.Templater.Extensions;
using OpenXML.Docx.Templater.Lexemes;
using OpenXML.Templater;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing.Nodes;
using OpenXML.Templater.Rederer;
using System.Diagnostics.CodeAnalysis;

namespace OpenXML.Docx.Templater
{
    /// <inheritdoc cref="IRender"/>
    /// <param name="dataModel">Данные.</param>
    /// <param name="outputFileName">Имя выходного файла.</param>
    internal class DocxRenderer(
        DataModel dataModel,
        string outputFileName,
        VDomNode vDom) : IRender, IDisposable
    {
        private XWPFDocument _targetDoc = new();
        private bool disposedValue;
        private readonly FileStream _fileStream = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        private readonly IDataModelContext _dataModelContext = new DataModelContext(dataModel);
        private readonly List<string> _warrnigs = new List<string>();
        private XWPFHeaderFooterPolicy? _targetHeaderFooterPolicy;

        /// <inheritdoc/>
        public void Render(HorizSectionNode node)
        {
        }

        /// <inheritdoc/>
        public void Render(InlineNode node)
        {
        }

        /// <inheritdoc/>
        public void Render(InvertedSectionNode node)
        {
        }

        /// <inheritdoc/>
        public void Render(RootNode node)
        {
            foreach (SyntaxNode child in node.Children)
            {
                child.Accept(this);
            }
            _targetDoc.Write(_fileStream);
            _fileStream.Close();
        }

        /// <inheritdoc/>
        public void Render(SectionNode node)
        {
        }

        /// <inheritdoc/>
        public void Render(TextNode node)
        {
            if (!ValidateLexemeType<DocxTextLexeme>(node, out DocxTextLexeme? textLexem))
                return;

            RenderParagraph(textLexem.Element, textLexem.Content.ToString());
        }

        /// <inheritdoc/>
        public void Render(EmptyNode node)
        {
            if (!ValidateLexemeType<DocxEmptyLexem>(node, out DocxEmptyLexem? emptyLexem))
                return;

            RenderParagraph(emptyLexem.Element, string.Empty);
        }

        private void RenderParagraph(XWPFParagraph element, string value)
        {
            List<VDomNode> parents = GetParentList(element);
            parents.Reverse();
            VDomNode? parent = parents.FirstOrDefault();
            if (parent == null)
            {
                _warrnigs.Add("Список родительских узлов пуст");
                return;
            }
            foreach (VDomNode parentNode in parents)
            {
                if(parentNode.Target == null)
                    RenderElement(parentNode, parent);

                parent = parentNode;
            }
        }

        private void RenderElement(VDomNode nodeToRender, VDomNode parentNode)
        {
            object templateElement = nodeToRender.Template;
            switch (templateElement)
            {
                case XWPFHeader header:
                    RenderHeader(nodeToRender, parentNode);
                    break;
                case XWPFTable table:
                    RenderTable(nodeToRender, parentNode);
                    break;
                case XWPFTableRow row:
                    RenderTableRow(nodeToRender, parentNode);
                    break;
                case XWPFTableCell cell:
                    RenderTableCell(nodeToRender, parentNode);
                    break;
                case XWPFParagraph paragraph:
                    RenderParagraph(nodeToRender, parentNode);
                    break;
                case XWPFFooter footer:
                    RenderFooter(nodeToRender, parentNode);
                    break;
                case XWPFDocument document:
                    RenderDocument(nodeToRender);
                    break;
            }
        }

        private void RenderDocument(VDomNode nodeToRender)
        {
            if (nodeToRender.Template is not XWPFDocument doc)
            {
                _warrnigs.Add($"Отрисовка документа: Ожидался тип {typeof(XWPFDocument).Name}," +
                    $" элемент шаблона имеет тип {nodeToRender.Template?.GetType().Name}");

                return;
            }
            CT_Styles templateCTStyles = doc.GetCTStyle();
            XWPFStyles targetStyles = _targetDoc.GetStyles();
            targetStyles.SetStyles(templateCTStyles);

            //XWPFStyles templateStyles = doc.GetStyles();
            //foreach (var templateStyle in templateStyles.listOfStyles)
            //{
            //    if (!targetStyles.StyleExist(templateStyle.StyleId))
            //    {
            //        var targetStyle = templateStyles.GetStyle(templateStyle.StyleId);
            //        targetStyles.AddStyle(targetStyle);
            //    }
            //}
            nodeToRender.Target = _targetDoc;
        }

        private void RenderHeader(VDomNode headerNode, VDomNode parentNode)
        {
            XWPFHeader templateHeader = (headerNode.Template as XWPFHeader)!;
            ST_HdrFtr? headerType = Utils.GetSTHeaderType(templateHeader);
            if (headerType == null)
            {
                _warrnigs.Add("Не удалось определить тип заголовка");
                return;
            }

            XWPFHeader targetHeader = GetHeaderFooterPolicy()
                .CreateHeader(headerType.Value);

            headerNode.Target = targetHeader;
        }

        private XWPFHeaderFooterPolicy GetHeaderFooterPolicy()
        {
            return _targetHeaderFooterPolicy ??= _targetDoc.CreateHeaderFooterPolicy();
        }

        private void RenderTable(VDomNode tableNode, VDomNode parentNode)
        { 
        }

        private void RenderTableRow(VDomNode tableRowNode, VDomNode parentNode)
        { 
        }

        private void RenderTableCell(VDomNode tableCellNode, VDomNode parentNode)
        { 
        }

        private void RenderParagraph(VDomNode paragraphNode, VDomNode parentNode)
        {
            if (paragraphNode.Template is not XWPFParagraph templateParagraph)
            {
                _warrnigs.Add($"Ожидается шаблонный элемент типа {typeof(XWPFParagraph)}");
                return;
            }

            XWPFParagraph? targetParagraph = Utils.CreateParagraph(parentNode.Target);
            if (targetParagraph == null)
            {
                _warrnigs.Add("Неизвестный тип элемента");
                return;
            }
            paragraphNode.Target = targetParagraph;
            if (templateParagraph.IsEmpty)
                return;

            templateParagraph.CopyTo(targetParagraph);
        }

        private void RenderFooter(VDomNode footerNode, VDomNode parentNode)
        {
        }

        private List<VDomNode> GetParentList(IBodyElement bodyElement)
        {
            var parentTree = new List<VDomNode>();
            if(!BuildParentList(vDom, bodyElement, parentTree))
                _warrnigs.Add($"Элемент {bodyElement} не найден в виртуальном дереве документа");

            return parentTree;

            bool BuildParentList(VDomNode currentNode, IBodyElement bodyElement, IList<VDomNode> parentTree)
            {
                if (ReferenceEquals(currentNode.Template, bodyElement))
                {
                    parentTree.Add(currentNode);

                    return true;
                }

                foreach(var child in currentNode.Children)
                    if (BuildParentList(child, bodyElement, parentTree))
                    {
                        parentTree.Add(currentNode);
                        return true;
                    }

                return false;
            }
        }

        object? GetParent(object part)
        {
            return part switch
            {
                XWPFHeaderFooter headerFooter => headerFooter.Part, //(POIXMLDocumentPart, IBody)
                XWPFPictureData picture => picture.GetParent(), //POIXMLDocumentPart
                XWPFTable table => table.Part,// IBodyElement
                XWPFTableRow row => row.GetTable(),
                XWPFTableCell cell => cell.GetTableRow(),
                XWPFDocument document => null,// IBody
                _ => null
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _fileStream.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private bool ValidateLexemeType<TLexeme>(SyntaxNode node,
            [NotNullWhen(true)] out TLexeme lexem)
             where TLexeme : Lexem, IDocxLexem
        {
            lexem = null!;
            if (node.Lexem is not TLexeme _lexem)
            {
                _warrnigs.Add($"Ожидаемый тип node.Lexem {typeof(TLexeme)}, однако является {node.Lexem?.GetType().Name}");
                return false;
            }
            lexem = _lexem;

            return true;
        }

    }
}
