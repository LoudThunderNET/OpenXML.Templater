using NUnit.Framework;
using OpenXML.Templater.Lexing;

namespace OpenXML.Templater.Tests
{
    [TestFixture]
    public class LexerTests
    {
        [Test]
        public void WhenEmptyTemplate()
        {
            var template = TemplateSamples.Empty;
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
            var template = TemplateSamples.Whitespace;
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.Whitespace));
        }

        [TestCase(TemplateSamples.One)]
        [TestCase(TemplateSamples.Two)]
        [TestCase(TemplateSamples.Three)]
        [TestCase(TemplateSamples.Four)]
        [TestCase(TemplateSamples.Five)]
        [TestCase(TemplateSamples.Six)]
        [TestCase(TemplateSamples.Seven)]
        [TestCase(TemplateSamples.Eigth)]
        [TestCase(TemplateSamples.Nine)]
        [TestCase(TemplateSamples.Ten)]
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
            var template = TemplateSamples.WhitespaceIdentifierWhitespace;
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.WhitespaceIdentifierWhitespace));
        }

        [Test]
        public void WhenWrongIndetifierTemplate()
        {
            var template = TemplateSamples.WhitespaceWroneIdentifierWhitespace;
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.WhitespaceWroneIdentifierWhitespace));
        }

        [Test]
        public void WhenOpenTagOnly()
        {
            var template = TemplateSamples.OpenTag;
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.OpenTag));
        }

        [Test]
        public void WhenCloseTagOnly()
        {
            var template = TemplateSamples.CloseTag;
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.CloseTag));
        }

        [Test]
        public void WhenCloseTagOpenTag()
        {
            var template = TemplateSamples.CloseTagOpenTag;
            var lexer = new Lexer();

            var lexems = lexer.Analize(template);

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems.Count, Is.EqualTo(1));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.CloseTagOpenTag));
        }

        [Test]
        public void WhenOpenTagOpenTagCloseTag()
        {
            var template = TemplateSamples.OpenTagOpenTagCloseTag;
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.OpenTag));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagTextCloseTag()
        {
            var template = TemplateSamples.TextSpaceOpenTagSpaceInlineSpaceCloseTag;
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("world"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenTextOpenTagTextCloseTagText()
        {
            var template = TemplateSamples.TextSpaceOpenTagSpaceInlineSpaceCloseTagSpaceTextSpace;
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
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
            var template = TemplateSamples.OpenTagSpaceSectionSpaceCloseTag;
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
            var template = TemplateSamples.OpenTagSectionCloseTag;
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
            var template =TemplateSamples.OpenTagSpaceInvertedSectionSpaceCloseTag;
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
            var template = TemplateSamples.OpenTagSpaceHorizSectionSpaceCloseTag;
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
            var template = TemplateSamples.OpenTagSpaceEndSectionSpaceCloseTag;
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
            var template = TemplateSamples.OpenTagSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag;
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

        [Test]
        public void WhenOpenTagDigitsCloseTag()
        {
            var template = TemplateSamples.OpenTagDigitsCloseTag;
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("435"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenOpenTagTextSpaceCloseTag()
        {
            var template = TemplateSamples.OpenTagTextSpaceCloseTag;
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(4));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("dsfsd132 {{#section"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(" }}"));
        }

        [Test]
        public void WhenOpenTagSectionCloseTagSpaceOpenTagInlineCloseTag()
        {
            var template = TemplateSamples.OpenTagSectionCloseTagSpaceOpenTagInlineCloseTag;
            var lexer = new Lexer();

            ICollection<Lexem>? lexems = null;
            Assert.DoesNotThrow(() => lexems = lexer.Analize(template));

            Assert.That(lexems, Is.Not.Null);
            Assert.That(lexems!.Count, Is.EqualTo(7));
            var lexemeEnumerator = lexems.GetEnumerator();
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(SectionLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("section"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(TextLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(OpenTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(InlineLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("fdfd"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void WhenOpenTagTextCloseTag()
        {
            var template = TemplateSamples.OpenTagTextCloseTag;
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
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo("#section dsf"));
            lexemeEnumerator.MoveNext();
            Assert.That(lexemeEnumerator.Current.GetType(), Is.EqualTo(typeof(CloseTagLexeme)));
            Assert.That(lexemeEnumerator.Current.Content.ToString(), Is.EqualTo(string.Empty));
        }
    }
}
