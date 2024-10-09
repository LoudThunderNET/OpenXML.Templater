using NUnit.Framework;
using OpenXML.Templater.Lexemes;

namespace OpenXML.Templater.Tests
{
    [TestFixture]
    public class LexerTests
    {
        [Test]
        public void WhenEmptyTemplate()
        {
            var template = string.Empty;
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(EmptyContent)));
        }

        [Test]
        public void WhenOneWhitespaceTemplate()
        {
            var template = " ";
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(" "));
        }

        [TestCase("0")]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        public void WhenOneDigitTemplate(string template)
        {
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(template));
        }

        [Test]
        public void WhenIndetifierTemplate()
        {
            var template = " hello123 ";
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(template));
        }

        [Test]
        public void WhenWrongIndetifierTemplate()
        {
            var template = " 21hello123 ";
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(template));
        }

        [Test]
        public void WhenOpenTagOnly()
        {
            var template = "{{";
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(template));
        }

        [Test]
        public void WhenCloseTagOnly()
        {
            var template = "}}";
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(template));
        }

        [Test]
        public void WhenCloseTagOpenTag()
        {
            var template = "}}{{";
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(template));
        }

        [Test]
        public void WhenOpenTagOpenTagCloseTag()
        {
            var template = "{{{{}}";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(3));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo("{{"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagTextCloseTag()
        {
            var template = "hello {{ world }}";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(4));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo("hello "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(" world "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagTextCloseTagText()
        {
            var template = "hello {{ beautiful }} word ";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(5));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo("hello "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(" beautiful "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content, Is.EqualTo(" word "));
        }

        [Test]
        public void WhenWhitespaceText()
        {
            var template = " hello";
            bool isIdentifier = true;
            Assert.DoesNotThrow(() => isIdentifier = IsIdentifier(template));

            Assert.That(isIdentifier, Is.False);
        }

        [Test]
        public void WhenEmpty()
        {
            var template = "";
            bool isIdentifier = true;
            Assert.DoesNotThrow(() => isIdentifier = IsIdentifier(template));

            Assert.That(isIdentifier, Is.False);
        }

        [TestCase("=")]
        [TestCase(",")]
        [TestCase(":")]
        [TestCase(";")]
        [TestCase("\"")]
        [TestCase("'")]
        [TestCase("~")]
        [TestCase("!")]
        [TestCase("$")]
        [TestCase("%")]
        [TestCase("&")]
        [TestCase("*")]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase("[")]
        [TestCase("]")]
        [TestCase("|")]
        [TestCase("-")]
        [TestCase("+")]
        [TestCase("?")]
        [TestCase("`")]
        [TestCase("№")]
        public void WhenNonLiteralText(string nonLiteral)
        {
            var template = nonLiteral+"hello";
            bool isIdentifier = true;
            Assert.DoesNotThrow(() => isIdentifier = IsIdentifier(template));

            Assert.That(isIdentifier, Is.False);
        }

        [Test]
        public void WhenLiteralWhiteSpaceLiteral()
        {
            var template = "hello world";
            bool isIdentifier = true;
            Assert.DoesNotThrow(() => isIdentifier = IsIdentifier(template));

            Assert.That(isIdentifier, Is.False);
        }

        [Test]
        public void WhenLiteralDigit()
        {
            var template = "hello_world1";
            bool isIdentifier = true;
            Assert.DoesNotThrow(() => isIdentifier = IsIdentifier(template));

            Assert.That(isIdentifier, Is.True);
        }

        private bool IsIdentifier(string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            char firstChar = source[0];
            if (!Lexer._literalSmbs.Contains(firstChar))
                return false;

            for (var i = 1; i < source.Length - 1; i++)
                if (!Lexer._digitSmbs.Contains(source[i]) && !Lexer._literalSmbs.Contains(source[i]))
                    return false;

            return true;
        }
    }
}
