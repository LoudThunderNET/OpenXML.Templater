using OpenXML.Templater.Extensions;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Syntaxis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OpenXML.Templater.Syntaxing
{
    public class Syntax
    {
        public (bool Valid, Lexem? wrongLexem, string SyntaxError) Verify(IEnumerable<Lexem> lexemes)
        {
            if (lexemes.IsEmpty())
                return (true, null, string.Empty);

            var enumerator = lexemes.GetEnumerator();
            Lexem? current, prev = current = null;
            var sectionsStack = new Stack<Lexem>(lexemes.Count());
            var mustacheTagsStack = new Stack<Lexem>(lexemes.Count());
            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                switch (current)
                {
                    case TextLexeme text:
                        if (prev is OpenTagLexeme)
                            return (false, text, "Ожидался идентификатор");
                        if (prev is SectionLexeme)
                            return (false, text, "Ожидался идентификатор");
                        break;
                    case OpenTagLexeme openTag:
                        if (mustacheTagsStack.Count() != 0)
                            return (false, openTag, "Открывающий тэг не может быть вложен");
                        mustacheTagsStack.Push(openTag);
                        break;
                    case CloseTagLexeme closeTag:
                        if (mustacheTagsStack.Count() == 0)
                            return (false, closeTag, "Закрывающий тэг должен следовать после открывающего тэга");
                        mustacheTagsStack.Pop();
                        break;
                    case SectionLexeme section:
                        if (sectionsStack.Count != 0)
                            return (false, current, "Вложенная секция не поддерживается");
                        sectionsStack.Push(current);
                        break;
                    case InvertedSectionLexeme invertedSection:
                        if (sectionsStack.Count != 0)
                            return (false, current, "Вложенная секция не поддерживается");
                        sectionsStack.Push(current);
                        break;
                    case HorizSectionLexeme horizSection:
                        if (sectionsStack.Peek() is not SectionLexeme)
                            return (false, current, "Горизонтиальная секция может быть вложеная только в секцию");
                        sectionsStack.Push(current);
                        break;
                    case EndSectionLexeme endSection:
                        if (sectionsStack.Count() == 0)
                            return (false, current, "Горизонтиальная секция может быть вложеная только в секцию");
                        var openedSection = sectionsStack.Pop();
                        if (!endSection.Content.Equals(openedSection.Content))
                            return (false, current, "Секция '"+endSection.Content+"' относиться к другой секции");
                        break;
                }
                prev = enumerator.Current;
            }

            if (sectionsStack.Count() != 0)
            {
                return (false, sectionsStack.Peek(), "Секция не имеет закрывающего тэга");
            }
            if (mustacheTagsStack.Count() != 0)
            { 
                return (false, mustacheTagsStack.Peek(), "Ожидается закрывающий тэг");
            }

            return (true, null, string.Empty);
        }
    }
}
