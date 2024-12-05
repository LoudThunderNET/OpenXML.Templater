using DocumentFormat.OpenXml.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing.Nodes;
using OpenXML.Templater.Rederer;
using OpenXML.Xlsx.Templater.Exceptions;
using OpenXML.Xlsx.Templater.Lexemes;
using System.Diagnostics.CodeAnalysis;

namespace OpenXML.Xlsx.Templater.Renderer
{
    public class XlsxRenderer : IRenderVisitor, IDisposable
    {
        private bool _disposedValue;
        private readonly FileStream _fileStream;
        private readonly ISheet _templateSheet;
        private readonly XSSFWorkbook _targetWorkbook;
        private readonly ISheet _targetSheet;
        private readonly DataModel _dataModel;
        private readonly List<string> _warrnigs;
        private readonly Stack<SectionNode> _sectionStack;

        /// <summary>
        /// Смещение строки в целевом листе, относительно исходного листа.
        /// Показывает насколько ниже в целоевом листе необходимо выводить
        /// строку из исходного листа, после отрисовки вертикальной секции.
        /// </summary>
        private int _rowOffset = 0;

        public XlsxRenderer(ISheet templateSheet, DataModel dataModel, string outputFilename) 
        {
            _fileStream = new FileStream(outputFilename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _targetWorkbook = new XSSFWorkbook();
            _targetSheet = _targetWorkbook.CreateSheet(templateSheet.SheetName);
            _templateSheet = templateSheet;
            _dataModel = dataModel;
            _warrnigs = [];
            _sectionStack = new Stack<SectionNode>();
        }

        public void Visit(HorizSectionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(InlineNode node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (!ValidateInlineNode(node, out var inline, out var field))
                return;

            RenderCell(inline.Cell!, field.Value);
        }

        public void Visit(InvertedSectionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(RootNode node)
        {
            foreach (var child in node.Children)
            {
                child.Accept(this);
            }
            _targetWorkbook.Write(_fileStream, false);
            _fileStream.Close();
        }

        public void Visit(SectionNode node)
        {
            if(!ValidateNode<XlsxSectionLexeme>(node, out var sectionlexem, out var content))
                return;
            _sectionStack.Push(node);
            RenderSection(sectionlexem, content);
            _sectionStack.Pop();
        }

        private void RenderSection(XlsxSectionLexeme sectionlexem, string content)
        {
            var table = _dataModel.Tables.FirstOrDefault(t => t.Name == content);
        }

        public void Visit(TextNode node)
        {
            if (!ValidateNode<XlsxTextLexeme>(node, out var textLexem, out var content))
                return;

            RenderCell(textLexem.Cell!, content);
        }

        /// <inheritdoc/>
        public void Visit(EmptyNode node)
        {
            if (!ValidateType<XlsxEmptyLexem>(node, out var emptyLexem))
                return;

            RenderCell(emptyLexem.Cell!, string.Empty);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _fileStream.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private bool ValidateNode<TLexeme>(
            SyntaxNode node,
            [NotNullWhen(true)] out TLexeme lexem,
            [NotNullWhen(true)] out string content)
            where TLexeme : Lexem, IXlsxLexem
        {
            content = null!;
            lexem = null!;
            if (!ValidateType<TLexeme>(node, out lexem))
            {
                return false;
            }

            content = lexem.Content.ToString();
            if (lexem.Cell == null)
            {
                _warrnigs.Add($"Cell property of inlineNode with value {content} is null");
                content = null!;
                return false;
            }

            return true;
        }

        private bool ValidateType<TLexeme>(SyntaxNode node,
            [NotNullWhen(true)] out TLexeme lexem)
             where TLexeme : Lexem, IXlsxLexem
        {
            lexem = null!;
            if (node.Lexem is not TLexeme _lexem)
            {
                _warrnigs.Add($"Expected type of the node.Lexem is {typeof(TLexeme)} but meet {node.Lexem?.GetType().Name}");
                return false;
            }

            lexem = _lexem;
            return true;
        }

        private bool ValidateInlineNode(
            InlineNode node,
            [MaybeNullWhen(false)] out XlsxInlineLexeme lexem,
            [MaybeNullWhen(false)] out Field field)
        {
            field = null;
            if (!ValidateNode(node, out lexem, out var content))
            {
                return false;
            }


            field = _dataModel.SingleFileds.FirstOrDefault(f => f.Name == content);
            if (field == null)
            {
                _warrnigs.Add($"DataModel does not contains field of name '{content}' and will be skiped to render");
                return false;
            }

            return true;
        }

        private void RenderCell(ICell templateCell, string value)
        {
            var targetRow = _targetSheet.GetOrAddRow(TargetRowIndex(templateCell.RowIndex));
            var targetCell = targetRow.GetOrAddCell(templateCell.ColumnIndex);
            var targetCellStyle = _targetSheet.Workbook.CreateCellStyle();
            targetCellStyle.CloneStyleFrom(templateCell.CellStyle);

            if (templateCell.IsMergedCell && !_targetSheet.IsInMergedRegion(targetCell))
            {
                var mergeRange = templateCell.Sheet.MergedRegions
                    .First(mr => mr.IsInRange(templateCell.RowIndex, templateCell.ColumnIndex));
                for (var rowIndex = mergeRange.FirstRow; rowIndex <= mergeRange.LastRow; rowIndex++)
                {
                    var targetMergeRow = _targetSheet.GetOrAddRow(TargetRowIndex(rowIndex));
                    for (var colIndex = mergeRange.FirstColumn; colIndex <= mergeRange.LastColumn; colIndex++)
                    {
                        var cellMergeTarget = targetMergeRow.GetOrAddCell(colIndex);
                        cellMergeTarget.CellStyle = targetCellStyle;
                    }
                }
                var targetMergeRange = new CellRangeAddress(
                    TargetRowIndex(mergeRange.FirstRow),
                    TargetRowIndex(mergeRange.LastRow),
                    mergeRange.FirstColumn,
                    mergeRange.LastColumn);

                _targetSheet.AddMergedRegion(targetMergeRange);
                var leftTopeMergeCell = _targetSheet.GetRow(targetMergeRange.FirstRow)
                    .GetCell(targetMergeRange.FirstColumn);
                leftTopeMergeCell.SetCellValue(value);
                return;
            }
            
            targetCell.CellStyle = targetCellStyle;
            targetCell.SetCellType(templateCell.CellType);
            targetCell.SetCellValue(value);
        }

        /// <summary>
        /// Возвращает номер строки в целевом листе.
        /// </summary>
        /// <param name="templateRowIndex">Индекс строки в исходном листе.</param>
        private int TargetRowIndex(int templateRowIndex) =>
            _rowOffset + templateRowIndex;
    }
}
