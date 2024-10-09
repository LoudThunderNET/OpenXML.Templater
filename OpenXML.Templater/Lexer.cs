using OpenXML.Templater.Lexemes;

namespace OpenXML.Templater
{
    public class Lexer
    {
        private static string OpenTag = "{{";
        private static string CloseTag = "}}";
        private static Dictionary<char, Func<string, Lexem>> MustasheBlock = new Dictionary<char, Func<string, Lexem>>
        {
            ['#'] = (string content) => new SectionLexeme(content),
            ['^'] = (string content) => new InvertedSectionLexeme(content),
            ['>'] = (string content) => new HorizSectionLexeme(content),
            ['/'] = (string content) => new EndSectionLexeme(content),
        };

        public readonly static HashSet<char> _specSmbs = new HashSet<char>
        {
            '=',',',':',';','"','\'','~','!','$','%','&','*','(',')','[',']','|','-','+','?','`','№'
        };

        public readonly static HashSet<char> _digitSmbs = new HashSet<char>
        {
            '0','1','2','3','4','5','6','7','8','9'
        };

        public readonly static HashSet<char> _literalSmbs = new HashSet<char>
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н','о','п','р','с','т','у','ф','х','ц','ч','ш',
            'щ','ъ','ы','ь','э','ю','я','А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С',
            'Т','У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я','_','@',

        };

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
                var mustacheInner = templateClone.Substring(index, closeTagIndex - index).Trim();
                for (var innerIndex = 0; innerIndex < mustacheInner.Length - 1; innerIndex++)
                {
                    if (MustasheBlock.TryGetValue(mustacheInner[0], out var lexemeFactory))
                    {
                        var mustacheInnerRemains = mustacheInner.Substring(1);
                        if (IsIdentifier(mustacheInnerRemains))
                        {
                            lexemes.Add(lexemeFactory(mustacheInnerRemains));
                        }
                    }
                    else
                    {
                        lexemes.Add(new TextLexeme(mustacheInner));
                    }
                }

                index = closeTagIndex;

                lexemes.Add(new CloseTagLexeme());
                index = closeTagIndex + OpenTag.Length;

                templateClone = templateClone.Substring(index);
            }

            return lexemes;

            bool AreTagsRightArranged(int openTagIndex, int closeTagIndex) => 
                openTagIndex >= 0 && closeTagIndex > 0 && openTagIndex < closeTagIndex;

        }
            private bool IsIdentifier(string source)
            {
                if (string.IsNullOrEmpty(source))
                    return false;

                char firstChar = source[0];
                if (!_literalSmbs.Contains(firstChar))
                    return false;

                for (var i = 1; i < source.Length - 1; i++)
                    if (!_digitSmbs.Contains(source[i]) && !_literalSmbs.Contains(source[i]))
                        return false;

                return true;
            }

    }
}
