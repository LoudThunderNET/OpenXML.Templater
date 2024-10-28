using ClosedXML.Excel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing.Nodes;
using OpenXML.Templater.Rederer;
using OpenXML.Xlsx.Templater.Exceptions;

namespace OpenXML.Xlsx.Templater.Renderer
{
    public class XlsxRenderer : IRenderVisitor, IDisposable
    {
        private bool _disposedValue;
        private FileStream _fileStream;
        private readonly IXLWorksheet _template;
        private readonly XLWorkbook _targetWorkbook;
        private readonly IXLWorksheet _target;
        private readonly DataModel _dataModel;

        public XlsxRenderer(IXLWorksheet tempate, DataModel dataModel, string outputFilename) 
        {
            _fileStream = new FileStream(outputFilename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _targetWorkbook = new XLWorkbook();
            //_targetWork
            _template = tempate;
            _dataModel = dataModel;
        }

        public void Visit(HorizSectionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(InlineNode node)
        {
            ArgumentNullException.ThrowIfNull(node);

            var inline = node.Lexem as XlsxInlineLexeme;
            if (inline == null)
                return;
                //XlsxTemplateException.Throw("Некорректная структура синтаксического дерева. Ожидалась лексема "+typeof(XlsxInlineLexeme).FullName+", но встретилась лексема "+node.Lexem!.GetType().FullName);

            var field = _dataModel.SingleFileds.FirstOrDefault(f => f.Name == inline!.Content.ToString());
            if (field == null)
                return;
            //field.Value;
            //inline.Cell.Address;
            //templ
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
        }

        public void Visit(SectionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(TextNode node)
        {
            throw new NotImplementedException();
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
    }
}
