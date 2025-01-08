using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OpenXML.Templater;
using OpenXML.Templater.Exceptions;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing;
using OpenXML.Templater.Syntaxing;
using OpenXML.Templater.Templater;
using OpenXML.Xlsx.Templater.Lexemes;
using OpenXML.Xlsx.Templater.Renderer;
namespace OpenXML.Xlsx.Templater
{
    public class XlsxTemplater : ITemplater
    {
        public void Render(string templateFileName, DataModel dataModel, string outpurFileName)
        {
            using var fileStream = new FileStream(templateFileName, FileMode.Open, FileAccess.Read);

            var template = new XSSFWorkbook(fileStream);
            ISheet sheet = template.GetSheetAt(0);
            if (sheet == null)
            {
                TemplaterException.Throw("В файле шаблона нет книги");
                return;
            }

            var lexer = new Lexer(new XlsxLexemeFactory());

            var wrongLexemsExist = false;
            List<Lexem> lexemes = sheet
                .SelectMany(row => row.SelectMany(cell =>
                {
                    var lexems = lexer.Analize(cell.StringCellValue);
                    foreach (var l in lexems)
                        if (l is IXlsxLexem xlsxLexem)
                            xlsxLexem.Cell = cell;
                        else
                            wrongLexemsExist = true;
                    return lexems;
                }))
                .ToList();
            if (wrongLexemsExist)
                TemplaterException
                    .Throw("Ошибка разбора шаблона: есть лексемы тличные от типа "+typeof(IXlsxLexem).FullName);

            var syntax = new Syntax();
            var (isValid, lexem, syntaxError) = syntax.Verify(lexemes);
            if (!isValid && lexem != null)
            {
                TemplaterException
                    .Throw("Синтаксическая ошибка в ячейке " + ((IXlsxLexem)lexem).Cell!.Address + ": " + syntaxError);
            }
            var parser = new Parser();
            parser.Parse(lexemes);
            var renderer = new XlsxRenderer(sheet, dataModel, outpurFileName);
            parser.Root.Accept(renderer);
        }
    }
}
