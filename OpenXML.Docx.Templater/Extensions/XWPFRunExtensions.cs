using NPOI.XWPF.UserModel;

namespace OpenXML.Docx.Templater.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="XWPFRun"/>
    /// </summary>
    internal static class XWPFRunExtensions
    {
        /// <summary>
        /// Копирует парметры <paramref name="sourceRun"/> в <paramref name="targetRun"/>.
        /// </summary>
        /// <param name="sourceRun">Исходный пробег.</param>
        /// <param name="targetRun">Целевой пробег.</param>
        public static void CopyTo(this XWPFRun sourceRun, XWPFRun targetRun)
        {
            targetRun.SetText(sourceRun.Text);
            targetRun.IsBold = sourceRun.IsBold;
            targetRun.IsCapitalized = sourceRun.IsCapitalized;
            targetRun.IsShadowed = sourceRun.IsShadowed;
            targetRun.IsImprinted = sourceRun.IsImprinted;
            targetRun.IsEmbossed = sourceRun.IsEmbossed;
            targetRun.IsDoubleStrikeThrough = sourceRun.IsDoubleStrikeThrough;
            targetRun.IsSmallCaps = sourceRun.IsSmallCaps;
            targetRun.IsStrikeThrough = sourceRun.IsStrikeThrough;
            targetRun.FontSize = sourceRun.FontSize;
            targetRun.Underline = sourceRun.Underline;
            targetRun.Subscript = sourceRun.Subscript;
            targetRun.Kerning = sourceRun.Kerning;
            targetRun.CharacterSpacing = sourceRun.CharacterSpacing;
            targetRun.FontFamily = sourceRun.FontFamily;
            targetRun.FontSize = sourceRun.FontSize;
            targetRun.SetColor(sourceRun.GetColor());
            targetRun.SetFontFamily(sourceRun.FontFamily, FontCharRange.None);
        }
    }
}
