using NPOI.XWPF.UserModel;

namespace OpenXML.Docx.Templater
{
    internal class VDomBuilder
    {
        public VDomNode Build(XWPFDocument doc)
        {
            var root = new VDomNode
            { 
                Template = doc
            };

            BuildHeaders(root, doc.HeaderList);
            BuildBodyElements(root, doc.BodyElements);
            BuildFooters(root, doc.FooterList);

            return root;
        }

        private static void BuildHeaders(VDomNode node, IEnumerable<XWPFHeader> headerList)
        {
            foreach (var header in headerList)
            {
                BuildHeader(node, header);
            }
        }

        private static void BuildHeader(VDomNode node, XWPFHeader header)
        {
            var headerNode = new VDomNode
            {
                Template = header,
                Parent = node
            };
            node.Children.Add(headerNode);

            BuildHeaderFooterChildren(headerNode, header);
        }

        private static void BuildHeaderFooterChildren(VDomNode headerNode, XWPFHeaderFooter header)
        {
            BuildParagraphsAsChildren(headerNode, header.Paragraphs);
            foreach (var tab in header.Tables)
            {
                VDomNode tableNode = new VDomNode()
                {
                    Template = tab,
                    Parent = headerNode
                };
                headerNode.Children.Add(tableNode);
                BuildTableChildren(tableNode, tab);
            }
        }

        private static void BuildTableChildren(VDomNode tableNode, XWPFTable tab)
        {
            foreach (var row in tab.Rows)
            {
                var rowNode = new VDomNode
                {
                    Template = row,
                    Parent = tableNode
                };
                tableNode.Children.Add(rowNode);
                BuildRowChildren(rowNode, row);
            }
        }

        private static void BuildRowChildren(VDomNode rowNode, XWPFTableRow row)
        {
            foreach (var cell in row.GetTableCells())
            {
                var cellNode = new VDomNode
                {
                    Template = cell,
                    Parent = rowNode
                };
                rowNode.Children.Add(cellNode);
                BuildParagraphsAsChildren(cellNode, cell.Paragraphs);
            }
        }

        private static void BuildParagraphsAsChildren(VDomNode node, IEnumerable<XWPFParagraph> paragraphs)
        {
            foreach (var para in paragraphs)
            {
                BuildParagraphAsChild(node, para);
            }
        }

        private static void BuildParagraphAsChild(VDomNode node, XWPFParagraph para)
        {
            node.Children.Add(new VDomNode
            {
                Template = para,
                Parent = node
            });
        }

        private static void BuildBodyElements(VDomNode node, IEnumerable<IBodyElement> bodyElements)
        {
            foreach (var bodyElement in bodyElements)
            {
                switch (bodyElement)
                {
                    case XWPFParagraph paragraph:
                        BuildParagraphAsChild(node, paragraph);
                        break;
                    case XWPFTable table:
                        BuildTableChildren(node, table);
                        break;
                }
            }
        }

        private static void BuildFooters(VDomNode node, IEnumerable<XWPFFooter> footerList)
        {
            foreach (var footer in footerList)
            {
                BuildFooter(node, footer);
            }
        }

        private static void BuildFooter(VDomNode node, XWPFFooter footer)
        {
            var footerNode = new VDomNode
            {
                Template = footer,
                Parent = node
            };
            node.Children.Add(footerNode);

            BuildHeaderFooterChildren(footerNode, footer);
        }
    }
}
