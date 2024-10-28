using ClosedXML.Excel;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing;
using OpenXML.Templater.Syntaxing;
using OpenXML.Xlsx.Templater.Exceptions;
using OpenXML.Xlsx.Templater.Lexemes;
using OpenXML.Xlsx.Templater.Renderer;
namespace OpenXML.Xlsx.Templater
{
    public class XlsxTemplater
    {
        public void Render(string templateFileName, DataModel dataModel, string outpurFileName)
        {
            using (var fileStream = new FileStream(templateFileName, FileMode.Open, FileAccess.Read))
            {
                var template = new XLWorkbook(fileStream);
                var worksheet = template.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {
                    XlsxTemplateException.Throw("В файле шаблона нет книги");
                    return;
                }

                var lexer = new Lexer(new XlsxLexemeFactory());
                var lexemes = worksheet.Cells()
                    .SelectMany(cell => lexer.Analize(cell.Value.ToString()))
                    .ToList();
                var syntax = new Syntax();
                var(isValid, lexem, syntaxError) = syntax.Verify(lexemes);
                if (!isValid && lexem != null)
                {
                    XlsxTemplateException
                        .Throw("Синтаксическая ошибка в ячейке " + ((IXlsxLexem)lexem).Cell.Address + ": " + syntaxError);
                }
                var parser = new Parser();
                parser.Parse(lexemes);
                var ast = parser.Root;
                var renderer = new XlsxRenderer(worksheet, dataModel, outpurFileName);
                ast.Accept(renderer);
            }
        }
    }
}
