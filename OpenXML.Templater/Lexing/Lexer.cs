using OpenXML.Templater.Extensions;
using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    /// <summary>
    /// Лексер.
    /// </summary>
    /// <param name="lexemeFactory">Фабрика лексем.</param>
    public class Lexer
    {
        private readonly static string OpenTag = "{{";
        private readonly static string CloseTag = "}}";
        private readonly ILexemeFactory _lexemeFactory;
        private readonly Dictionary<char, Func<StringSpan, Lexem>> _mustaсheBlocks;

        public Lexer(ILexemeFactory lexemeFactory)
        { 
            _lexemeFactory = lexemeFactory;
            _mustaсheBlocks = new Dictionary<char, Func<StringSpan, Lexem>>
            {
                ['#'] = (content) => _lexemeFactory.CreateSectionLexeme(content),
                ['^'] = (content) => _lexemeFactory.CreateInvertedSectionLexeme(content),
                ['>'] = (content) => _lexemeFactory.CreateHorizSectionLexeme(content),
                ['/'] = (content) => _lexemeFactory.CreateEndSectionLexeme(content),
            };
        }

        public readonly static HashSet<char> _specSmbs =
        [
            '=',',',':',';','"','\'','~','!','$','%','&','*','(',')','[',']','|','-','+','?','`','№'
        ];

        public readonly static HashSet<char> _digitSmbs =
        [
            '0','1','2','3','4','5','6','7','8','9'
        ];

        public readonly static HashSet<char> LiteralSmbs =
        [
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н','о','п','р','с','т','у','ф','х','ц','ч','ш',
            'щ','ъ','ы','ь','э','ю','я','А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С',
            'Т','У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я','_','@',

        ];

        public ICollection<Lexem> Analize(string template)
        {
            var lexemes = new List<Lexem>();
            if (template.IsNullOrEmpty())
            {
                lexemes.Add(_lexemeFactory.CreateEmptyLexeme());
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
                    lexemes.Add(_lexemeFactory.CreateTextLexeme(templateSpan));
                    break;
                }
                if (openTagIndex > 0)
                {
                    lexemes.Add(_lexemeFactory.CreateTextLexeme(templateSpan.Slice(0, openTagIndex - 1)));
                }
                index = openTagIndex + OpenTag.Length;

                lexemes.Add(_lexemeFactory.CreateOpenTagLexeme());

                var mustacheInner = templateSpan.Slice(index, closeTagIndex - 1);
                var trimmedMustacheInner = mustacheInner.Trim();
                if (!trimmedMustacheInner.IsEmpty())
                {
                    if (_mustaсheBlocks.TryGetValue(trimmedMustacheInner[0], out var lexemeFactory))
                    {
                        var mustacheInnerRemains = trimmedMustacheInner.Slice(1);
                        if (IsIdentifier(mustacheInnerRemains))
                        {
                            lexemes.Add(lexemeFactory(mustacheInnerRemains.Trim()));
                        }
                        else
                        {
                            lexemes.Add(_lexemeFactory.CreateTextLexeme(trimmedMustacheInner));
                        }
                    }
                    else
                    {
                        if (IsIdentifier(trimmedMustacheInner))
                        {
                            lexemes.Add(_lexemeFactory.CreateInlineLexeme(mustacheInner.Trim()));
                        }
                        else
                        {
                            lexemes.Add(_lexemeFactory.CreateTextLexeme(mustacheInner));
                        }
                    }
                }

                lexemes.Add(_lexemeFactory.CreateCloseTagLexeme());
                index = closeTagIndex + CloseTag.Length;

                templateSpan = templateSpan.Slice(index);
            }

            return lexemes;

            static bool AreTagsRightArranged(int openTagIndex, int closeTagIndex) =>
                openTagIndex >= 0 && closeTagIndex > 0 && openTagIndex < closeTagIndex;

        }

        private static bool IsIdentifier(StringSpan source)
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
