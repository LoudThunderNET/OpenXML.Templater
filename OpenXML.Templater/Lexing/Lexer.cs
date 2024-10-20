using OpenXML.Templater.Extensions;
using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public class Lexer
    {
        private static string OpenTag = "{{";
        private static string CloseTag = "}}";
        private static Dictionary<char, Func<StringSpan, Lexem>> MustasheBlocks = new Dictionary<char, Func<StringSpan, Lexem>>
        {
            ['#'] = (content) => new SectionLexeme(content),
            ['^'] = (content) => new InvertedSectionLexeme(content),
            ['>'] = (content) => new HorizSectionLexeme(content),
            ['/'] = (content) => new EndSectionLexeme(content),
        };

        public readonly static HashSet<char> _specSmbs = new HashSet<char>
        {
            '=',',',':',';','"','\'','~','!','$','%','&','*','(',')','[',']','|','-','+','?','`','№'
        };

        public readonly static HashSet<char> _digitSmbs = new HashSet<char>
        {
            '0','1','2','3','4','5','6','7','8','9'
        };

        public readonly static HashSet<char> LiteralSmbs = new HashSet<char>
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
            if (template.IsNullOrEmpty())
            {
                lexemes.Add(new EmptyContent());
            }
            var templateSpan = new StringSpan(template);
            int endIndex = template.Length - 1;
            int index = 0;
            while (index <= endIndex && !templateSpan.IsEmpty())
            {
                var openTagIndex = templateSpan.IndexOf(OpenTag);
                var closeTagIndex = templateSpan.IndexOf(CloseTag);
                if (!AreTagsRightArranged(openTagIndex, closeTagIndex))
                {
                    lexemes.Add(new TextLexeme(templateSpan));
                    break;
                }
                if (openTagIndex > 0)
                {
                    lexemes.Add(new TextLexeme(templateSpan.Slice(0, openTagIndex - 1)));
                }
                index = openTagIndex + OpenTag.Length;

                lexemes.Add(new OpenTagLexeme());

                // lexical analize of inner content instead TextLexeme
                var mustacheInner = templateSpan.Slice(index, closeTagIndex - 1);
                var trimmedMustacheInner = mustacheInner.Trim();
                if (!trimmedMustacheInner.IsEmpty())
                {
                    if (MustasheBlocks.TryGetValue(trimmedMustacheInner[0], out var lexemeFactory))
                    {
                        var mustacheInnerRemains = trimmedMustacheInner.Slice(1);
                        if (IsIdentifier(mustacheInnerRemains))
                        {
                            lexemes.Add(lexemeFactory(mustacheInnerRemains.Trim()));
                        }
                        else
                        {
                            lexemes.Add(new TextLexeme(trimmedMustacheInner));
                        }
                    }
                    else
                    {
                        if (IsIdentifier(trimmedMustacheInner))
                        {
                            lexemes.Add(new InlineLexeme(mustacheInner.Trim()));
                        }
                        else
                        {
                            lexemes.Add(new TextLexeme(mustacheInner));
                        }
                    }
                }

                index = closeTagIndex;

                lexemes.Add(new CloseTagLexeme());
                index = closeTagIndex + CloseTag.Length;

                templateSpan = templateSpan.Slice(index);
            }

            return lexemes;

            bool AreTagsRightArranged(int openTagIndex, int closeTagIndex) =>
                openTagIndex >= 0 && closeTagIndex > 0 && openTagIndex < closeTagIndex;

        }

        private bool IsIdentifier(StringSpan source)
        {
            if (source.IsEmpty())
                return false;

            char firstChar = source[0];
            if (!LiteralSmbs.Contains(firstChar))
                return false;

            for (var i = 1; i < source.Length - 1; i++)
                if (!_digitSmbs.Contains(source[i]) && !LiteralSmbs.Contains(source[i]))
                    return false;

            return true;
        }
    }
}
