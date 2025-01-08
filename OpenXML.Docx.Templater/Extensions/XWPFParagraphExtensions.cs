using NPOI.SS.Formula.Functions;
using NPOI.Util;
using NPOI.XWPF.Usermodel;
using NPOI.XWPF.UserModel;

namespace OpenXML.Docx.Templater.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="XWPFParagraph" />
    /// </summary>
    internal static class XWPFParagraphExtensions
    {
        /// <summary>
        /// Копирует параграф <paramref name="sourceParagraph"/> в <paramref name="targetParagraph"/>.
        /// </summary>
        /// <param name="sourceParagraph">Исходный параграф.</param>
        /// <param name="targetParagraph">Целевой параграф.</param>
        public static void CopyTo(this XWPFParagraph sourceParagraph, XWPFParagraph targetParagraph)
        {
            //targetParagraph.Alignment = sourceParagraph.Alignment;
            //targetParagraph.VerticalAlignment = sourceParagraph.VerticalAlignment;
            //targetParagraph.BorderTop = sourceParagraph.BorderTop;
            //targetParagraph.BorderBottom = sourceParagraph.BorderBottom;
            //targetParagraph.BorderLeft = sourceParagraph.BorderLeft;
            //targetParagraph.BorderRight = sourceParagraph.BorderRight;
            //targetParagraph.FillPattern = sourceParagraph.FillPattern;
            //targetParagraph.FillBackgroundColor = sourceParagraph.FillBackgroundColor;
            //targetParagraph.BorderBetween = sourceParagraph.BorderBetween;
            //targetParagraph.IsPageBreak = sourceParagraph.IsPageBreak;
            //targetParagraph.SpacingAfter = sourceParagraph.SpacingAfter;
            //targetParagraph.SpacingAfterLines = sourceParagraph.SpacingAfterLines;
            //targetParagraph.SpacingBefore = sourceParagraph.SpacingBefore;
            //targetParagraph.SpacingBeforeLines = sourceParagraph.SpacingBeforeLines;
            //targetParagraph.SpacingLineRule = sourceParagraph.SpacingLineRule;
            //targetParagraph.SpacingBetween = sourceParagraph.SpacingBetween;
            //targetParagraph.IndentationLeft = sourceParagraph.IndentationLeft;
            //targetParagraph.IndentationRight = sourceParagraph.IndentationRight;
            //targetParagraph.IndentationHanging = sourceParagraph.IndentationHanging;
            //targetParagraph.IndentationFirstLine = sourceParagraph.IndentationFirstLine;
            //targetParagraph.IndentFromLeft = sourceParagraph.IndentFromLeft;
            //targetParagraph.IndentFromRight = sourceParagraph.IndentFromRight;
            //targetParagraph.FirstLineIndent = sourceParagraph.FirstLineIndent;
            //targetParagraph.IsWordWrapped = sourceParagraph.IsWordWrapped;
            //targetParagraph.Style = sourceParagraph.Style;

            sourceParagraph.Runs.CopyRuns(targetParagraph);
            sourceParagraph.OMaths.CopyOMaths(targetParagraph);
        }

        private static void CopyRuns(this IEnumerable<XWPFRun> runsCollection, XWPFParagraph targetParagraph)
        {
            foreach (XWPFRun? sourceRun in runsCollection)
            {
                if (sourceRun is XWPFHyperlinkRun sourceHyperlink) 
                {
                    sourceHyperlink.CopyTo(targetParagraph);
                    continue;
                }

                XWPFRun targetRun = targetParagraph.CreateRun();
                sourceRun.CopyTo(targetRun);
            }
        }

        private static void CopyOMaths(this IEnumerable<XWPFOMath> omathCollection, XWPFParagraph targetParagraph)
        {
            foreach (XWPFOMath omath in omathCollection)
            {
                XWPFOMath targetOMath = targetParagraph.CreateOMath();
                foreach (XWPFSharedRun? r in omath.Runs)
                {
                    //r.
                }
                foreach (XWPFAcc acc in omath.Accs)
                {
                    XWPFAcc targetAcc = targetOMath.CreateAcc();
                    targetAcc.AccPr = acc.AccPr;
                }
                foreach (XWPFNary nary in omath.Naries)
                {
                }
                foreach (XWPFSSub ssub in omath.SSubs)
                {
                }
                foreach (XWPFF fs in omath.Fs)
                {
                }
                foreach (XWPFRad rad in omath.Rads)
                {
                }
            }
        }
    }
}
