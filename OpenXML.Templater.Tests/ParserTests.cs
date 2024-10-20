using NUnit.Framework;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Parsing;
using OpenXML.Templater.Parsing.Nodes;

namespace OpenXML.Templater.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void WhenEmpty()
        {
            var template = TemplateSamples.Empty;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenWhitespace()
        {
            var template = TemplateSamples.Whitespace;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(" "));
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
        public void WhenDigit(string template)
        {
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(template));
        }

        [Test]
        public void WhenWhitespaceIdentifierWhitespace()
        {
            var template = TemplateSamples.WhitespaceIdentifierWhitespace;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.WhitespaceIdentifierWhitespace));
        }

        [Test]
        public void WhenWhitespaceWroneIdentifierWhitespace()
        {
            var template = TemplateSamples.WhitespaceWroneIdentifierWhitespace;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.WhitespaceWroneIdentifierWhitespace));
        }

        [Test]
        public void WhenOpenTag()
        {
            var template = TemplateSamples.OpenTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.OpenTag));
        }

        [Test]
        public void WhenCloseTag()
        {
            var template = TemplateSamples.CloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.CloseTag));
        }

        [Test]
        public void WhenCloseTagOpenTag()
        {
            var template = TemplateSamples.CloseTagOpenTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(TemplateSamples.CloseTagOpenTag));
        }

        [Test]
        public void WhenOpenTagOpenTagCloseTag()
        {
            var template = TemplateSamples.OpenTagOpenTagCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("{{"));
        }

        [Test]
        public void WhenTextSpaceOpenTagSpaceInlineSpaceCloseTag()
        {
            var template = TemplateSamples.TextSpaceOpenTagSpaceInlineSpaceCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(2));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("hello "));
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(InlineNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("world"));
        }

        [Test]
        public void WhenTextSpaceOpenTagSpaceInlineSpaceCloseTagSpaceTextSpace()
        {
            var template = TemplateSamples.TextSpaceOpenTagSpaceInlineSpaceCloseTagSpaceTextSpace;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(3));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("hello "));
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(InlineNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(" word "));
        }

        [Test]
        public void WhenOpenTagSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag()
        {
            var template = TemplateSamples.OpenTagSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(SectionNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            Assert.That(enumerator.Current.Children.Count, Is.EqualTo(3));
            var sectionEnumerator = enumerator.Current.Children.GetEnumerator();
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(InlineNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo("soul"));
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
        }

        [Test]
        public void WhenOpenTagTextSpaceCloseTag()
        {
            var template = TemplateSamples.OpenTagTextSpaceCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(2));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("dsfsd132 {{#section"));
            Assert.That(enumerator.Current.Children.Count, Is.EqualTo(0));
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo(" }}"));
        }

        [Test]
        public void WhenOpenTagInvertedSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag()
        {
            var template = TemplateSamples.OpenTagInvertedSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(InvertedSectionNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            Assert.That(enumerator.Current.Children.Count, Is.EqualTo(3));
            var sectionEnumerator = enumerator.Current.Children.GetEnumerator();
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(InlineNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo("soul"));
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
        }

        [Test]
        public void WhenOpenTagHorizSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag()
        {
            var template = TemplateSamples.OpenTagHorizSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var parser = new Parser();
            parser.Parse(lexemes);
            var ast = parser.Root;

            Assert.That(ast, Is.Not.Null);
            Assert.That(ast.Content.Length, Is.EqualTo(0));
            Assert.That(ast.Children, Is.Not.Null);
            Assert.That(ast.Children.Count, Is.EqualTo(1));
            var enumerator = ast.Children.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current.GetType(), Is.EqualTo(typeof(HorizSectionNode)));
            Assert.That(enumerator.Current.Content.ToString(), Is.EqualTo("beautiful"));
            Assert.That(enumerator.Current.Children.Count, Is.EqualTo(3));
            var sectionEnumerator = enumerator.Current.Children.GetEnumerator();
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(InlineNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo("soul"));
            sectionEnumerator.MoveNext();
            Assert.That(sectionEnumerator.Current.GetType(), Is.EqualTo(typeof(TextNode)));
            Assert.That(sectionEnumerator.Current.Content.ToString(), Is.EqualTo(" "));
        }
    }
}
