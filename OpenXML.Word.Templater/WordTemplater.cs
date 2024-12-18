using NPOI.XWPF.UserModel;
using OpenXML.Templater;
using OpenXML.Templater.Exceptions;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing;
using OpenXML.Templater.Syntaxing;
using OpenXML.Templater.Templater;
using OpenXML.Word.Templater.Lexemes;

namespace OpenXML.Word.Templater
{
    /// <inheritdoc cref="ITemplater"/>
    public class WordTemplater : ITemplater
    {
        /// <inheritdoc/>
        public void Render(string templateFileName, DataModel dataModel, string outpurFileName)
        {
            using var fileStream = new FileStream(templateFileName, FileMode.Open, FileAccess.Read);
            var doc = new XWPFDocument(fileStream);

            var lexer = new Lexer(new DocxLexemeFactory());

            var wrongLexemsExist = false;
            List<Lexem> lexemes = doc.BodyElements
            .SelectMany(bodyElement =>
            {
                var lexemes = lexer.Analize(string.Empty);
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
            var ast = parser.Root;
            var renderer = new DocxRenderer(doc, dataModel, outpurFileName);
            ast.Accept(renderer);
        }
    }
}
