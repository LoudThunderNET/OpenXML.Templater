using NPOI.XWPF.UserModel;
using OpenXML.Templater;
using OpenXML.Templater.Exceptions;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing;
using OpenXML.Templater.Syntaxing;
using OpenXML.Templater.Templater;
using OpenXML.Docx.Templater.Lexemes;
using DocumentFormat.OpenXml.EMMA;

namespace OpenXML.Docx.Templater
{
    /// <inheritdoc cref="ITemplater"/>
    public class DocxTemplater : ITemplater
    {
        /// <inheritdoc/>
        public void Render(string templateFileName, DataModel dataModel, string outputFileName)
        {
            using var fileStream = new FileStream(templateFileName, FileMode.Open, FileAccess.Read);
            var doc = new XWPFDocument(fileStream);

            var lexer = new Lexer(new DocxLexemeFactory());

            var wrongLexemsExist = false;
            var vDom = new VDomBuilder().Build(doc);
            List<Lexem> lexemes = doc.HeaderList.SelectMany(h => h.Paragraphs.Union(h.Tables.SelectMany(ExtractBodyElements)))
                .Union(doc.BodyElements.SelectMany(ExtractBodyElements))
                .Union(doc.FooterList.SelectMany(f => f.Paragraphs.Union(f.Tables.SelectMany(ExtractBodyElements))))
                .SelectMany(bodyElement =>
                {
                    var lexemes = lexer.Analize(bodyElement.Text);
                    foreach (var lexeme in lexemes)
                    {
                        if (lexeme is IDocxLexem xlsxLexem)
                            xlsxLexem.Element = bodyElement;
                        else
                            wrongLexemsExist = true;
                    }
                    return lexemes;
                })
                .ToList();

            if (wrongLexemsExist)
            {
                TemplaterException
                    .Throw($"Ошибка разбора шаблона: есть лексемы отличные от типа {typeof(IDocxLexem).FullName}");
            }

            var syntax = new Syntax();
            var (isValid, lexem, syntaxError) = syntax.Verify(lexemes);
            if (!isValid && lexem != null)
            {
                TemplaterException
                    .Throw($"Синтаксическая ошибка в элементе {((IDocxLexem)lexem).Element}: {syntaxError}");
            }
            var parser = new Parser();
            parser.Parse(lexemes);
            var docxRenderer = new DocxRenderer(dataModel, outputFileName, vDom);
            parser.Root.Accept(docxRenderer);

            static IEnumerable<XWPFParagraph> ExtractBodyElements(IBodyElement bodyElement)
            {
                return bodyElement switch
                {
                    XWPFParagraph paragraph => [paragraph],
                    XWPFTable table => table.Rows
                            .SelectMany(r => r.GetTableCells()
                                .SelectMany(c => c.Paragraphs
                                    .Union(c.Tables.SelectMany(ExtractBodyElements)))),
                    _ => UnknownBodyElement()
                };

                static IEnumerable<XWPFParagraph> UnknownBodyElement()
                {
                    TemplaterException.Throw("Неизвестный тип элемента");
                    return null;
                }
            }
        }
    }
}
