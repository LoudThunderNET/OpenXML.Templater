using OpenXML.Templater.Lexemes;

namespace OpenXML.Templater
{
    public class Lexer
    {
        private static string OpenTag = "{{";
        private static string CloseTag = "}}";

        public ICollection<Lexem> Analize(string template)
        {
            var lexemes = new List<Lexem>();
            if (string.IsNullOrEmpty(template))
            {
                lexemes.Add(new EmptyContent());
            }
            string templateClone = new(template);
            int endIndex = template.Length - 1;
            int index = 0;
            while (index <= endIndex)
            {
                var openTagIndex = templateClone.IndexOf(OpenTag);
                var closeTagIndex = templateClone.IndexOf(CloseTag);
                if (!AreTagsRightArranged(openTagIndex, closeTagIndex))
                {
                    lexemes.Add(new TextLexeme(templateClone));
                    break;
                }
                if (openTagIndex > 0)
                {
                    lexemes.Add(new TextLexeme(templateClone.Substring(0, openTagIndex)));
                }
                index = openTagIndex + OpenTag.Length;

                lexemes.Add(new OpenTagLexeme());

                // lexical analize of inner content instead TextLexeme
                lexemes.Add(new TextLexeme(templateClone.Substring(index, closeTagIndex - index)));
                index = closeTagIndex;

                lexemes.Add(new CloseTagLexeme());
                index = closeTagIndex + OpenTag.Length;

                templateClone = templateClone.Substring(index);
            }

            return lexemes;

            bool AreTagsRightArranged(int openTagIndex, int closeTagIndex) => 
                openTagIndex >= 0 && closeTagIndex > 0 && openTagIndex < closeTagIndex;
        }

    }
}
