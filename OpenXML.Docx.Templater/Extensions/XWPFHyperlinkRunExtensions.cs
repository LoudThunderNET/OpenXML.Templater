using NPOI.XWPF.UserModel;

namespace OpenXML.Docx.Templater.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="XWPFHyperlinkRun"/>
    /// </summary>
    internal static class XWPFHyperlinkRunExtensions
    {
        /// <summary>
        /// Копирует парметры <paramref name="sourceHyperlink"/> в <paramref name="targetParagraph"/>.
        /// </summary>
        /// <param name="sourceHyperlink">Исходный пробег.</param>
        /// <param name="targetParagraph">Целевой пробег.</param>
        public static void CopyTo(this XWPFHyperlinkRun sourceHyperlink, XWPFParagraph targetParagraph)
        {
            var hyperlink = targetParagraph.CreateHyperlinkRun(sourceHyperlink.GetCTHyperlink().id);
            hyperlink.HyperlinkId = sourceHyperlink.HyperlinkId;
            sourceHyperlink.CopyTo(hyperlink);
        }
    }
}
