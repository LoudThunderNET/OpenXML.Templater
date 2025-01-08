using NPOI;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;

namespace OpenXML.Docx.Templater
{
    internal static class Utils
    {
        public static ST_HdrFtr? GetSTHeaderType(XWPFHeader header)
        {
            var doc = header.GetXWPFDocument();
            var sectPr = GetSection(doc);

            for (int i = 0; i < sectPr.SizeOfHeaderReferenceArray(); i++)
            {
                // Get the header
                CT_HdrFtrRef ref1 = sectPr.GetHeaderReferenceArray(i);
                POIXMLDocumentPart relatedPart = doc.GetRelationById(ref1.id);
                if (relatedPart != null && relatedPart is XWPFHeader hdr)
                {
                    if (ReferenceEquals(hdr, header))
                    {
                        return ref1.type;
                    }
                }
            }

            return null;
        }

        public static ST_HdrFtr? GetCTFooterType(XWPFFooter footer)
        {
            var doc = footer.GetXWPFDocument();
            var sectPr = GetSection(doc);

            for (int i = 0; i < sectPr.SizeOfHeaderReferenceArray(); i++)
            {
                // Get the footer
                CT_HdrFtrRef ref1 = sectPr.GetHeaderReferenceArray(i);
                POIXMLDocumentPart relatedPart = doc.GetRelationById(ref1.id);
                if (relatedPart != null && relatedPart is XWPFFooter ftr)
                {
                    if (ReferenceEquals(ftr, footer))
                    {
                        return ref1.type;
                    }
                }
            }

            return null;
        }

        private static CT_SectPr GetSection(XWPFDocument doc)
        {
            CT_Body ctBody = doc.Document.body;
            return (ctBody.IsSetSectPr() ?
                    ctBody.sectPr :
                    ctBody.AddNewSectPr());
        }

        public static XWPFParagraph? CreateParagraph(object? templateElement) =>
            templateElement switch
            {
                XWPFDocument document => document.CreateParagraph(),
                XWPFHeader header => header.CreateParagraph(),
                XWPFFooter footer => footer.CreateParagraph(),
                _ => null
                //XWPFTableCell cell => 
                //{
                //    var paragraph = 
                //    cell.Cre
                //}
            };
    }
}
