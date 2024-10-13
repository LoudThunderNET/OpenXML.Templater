using NUnit.Framework;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Extensions;

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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(template));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(template));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(template));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(template));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(template));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(template));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("{{"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("hello "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(InlineLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(" world "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("hello "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(InlineLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(" beautiful "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(" word "));
        }

        [Test]
        public void WhenTextOpenTagWhitespaceSectionTagWhitespaceCloseTagText()
        {
            var template = "{{ #beautiful }}";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(3));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(SectionLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagSectionTagCloseTagText()
        {
            var template = "{{#beautiful}}";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(3));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(SectionLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagWhitespaceInvertedSectionTagWhitespaceCloseTagText()
        {
            var template = "{{ ^beautiful }}";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(3));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(InvertedSectionLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagWhitespaceHorizSectionTagWhitespaceCloseTagText()
        {
            var template = "{{ >beautiful }}";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(3));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(HorizSectionLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagWhitespaceEndSectionTagWhitespaceCloseTagText()
        {
            var template = "{{ /beautiful }}";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(3));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(EndSectionLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagSectionWhitespaceCloseTagTextWhitespaceOpenTagLiteralCloseTagWhitespaceOpenTagEndSectionCloseTag()
        {
            var template = "{{#beautiful }} {{soul}} {{/beautiful}}";
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(11));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(SectionLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(""));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(InlineLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("soul"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(""));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(""));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(EndSectionLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(""));
        }
    }
}
