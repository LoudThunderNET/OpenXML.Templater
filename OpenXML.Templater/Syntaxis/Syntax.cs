using OpenXML.Templater.Extensions;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Syntaxis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXML.Templater.Syntaxing
{
    public class Syntax
    {
        public void Verify(IEnumerable<Lexem> lexemes)
        {
            if (lexemes.IsEmpty())
                return;

            var enumerator = lexemes.GetEnumerator();
            enumerator.MoveNext();
            Lexem current, prev = current = enumerator.Current;
            var sectionsStack = new Stack<Lexem>(lexemes.Count());
            var mustacheTagsStack = new Stack<Lexem>(lexemes.Count());
            while (enumerator.MoveNext())
            {
                switch (enumerator.Current)
                {
                    case TextLexeme text:
                        if (prev is OpenTagLexeme)
                            SyntaxException.Throw(text, "Ожидлася идентификатор");
                        if (prev is SectionLexeme)
                            SyntaxException.Throw(text, "Ожидлася идентификатор");
                        break;
                    case OpenTagLexeme openTag:
                        if (mustacheTagsStack.Count() != 0)
                            SyntaxException.Throw(openTag, "Открывающий тэг не может быть вложен");
                        mustacheTagsStack.Push(openTag);
                        break;
                    case CloseTagLexeme closeTag:
                        if (mustacheTagsStack.Count() == 0)
                            SyntaxException.Throw(closeTag, "Закрывающий тэг должен следовать после открывающего тэга");
                        mustacheTagsStack.Pop();
                        break;
                    case SectionLexeme section: 
                        if (sectionsStack.Count != 0)
                            SyntaxException.Throw(current, "Вложенная секция не поддерживается");
                        sectionsStack.Push(current);
                        break;
                    case InvertedSectionLexeme invertedSection:
                        if (sectionsStack.Count != 0)
                            SyntaxException.Throw(current, "Вложенная секция не поддерживается");
                        sectionsStack.Push(current);
                        break;
                    case HorizSectionLexeme horizSection:
                        if (sectionsStack.Peek() is not SectionLexeme)
                            SyntaxException.Throw(current, "Горизонтиальная секция может быть вложеная только в секцию");
                        sectionsStack.Push(current);
                        break;
                    case EndSectionLexeme endSection:
                        if (sectionsStack.Count() == 0)
                            SyntaxException.Throw(current, "Горизонтиальная секция может быть вложеная только в секцию");
                        var openedSection = sectionsStack.Pop();
                        if (!endSection.Content.Equals(openedSection.Content))
                            SyntaxException.Throw(current, "Секция '"+endSection.Content+"' относиться к другой секции");
                        break;
                }
                prev = enumerator.Current;
            }
        }
    }
}
