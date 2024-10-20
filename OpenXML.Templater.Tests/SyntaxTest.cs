using NUnit.Framework;
using OpenXML.Templater.Lexing;
using OpenXML.Templater.Syntaxing;

namespace OpenXML.Templater.Tests
{
    [TestFixture]
    public class SyntaxTest
    {
        [Test]
        public void WhenInlineTagContainsDigits()
        {
            var template = "{{435}}";
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var syntax = new Syntax();
            var (isValid, wrongLexem, syntaxError) = syntax.Verify(lexemes);

            Assert.That(isValid, Is.False);
            Assert.That(wrongLexem, Is.Not.Null);
            Assert.That(syntaxError, Is.EqualTo("Ожидался идентификатор"));
        }

        [Test]
        public void WhenInlineTagcontainsLiteralAndNotClosedSection()
        {
            var template = TemplateSamples.OpenTagTextSpaceCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var syntax = new Syntax();
            var(isValid, wrongLexem, syntaxError) = syntax.Verify(lexemes);

            Assert.That(isValid, Is.False);
            Assert.That(wrongLexem, Is.Not.Null);
            Assert.That(syntaxError, Is.EqualTo("Ожидался идентификатор"));
        }

        [Test]
        public void WhenSectionNotClosed()
        {
            var template = TemplateSamples.OpenTagSectionCloseTagSpaceOpenTagInlineCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var syntax = new Syntax();
            var(isValid, wrongLexem, syntaxError) = syntax.Verify(lexemes);

            Assert.That(isValid, Is.False);
            Assert.That(wrongLexem, Is.Not.Null);
            Assert.That(syntaxError, Is.EqualTo("Секция не имеет закрывающего тэга"));
        }

        [Test]
        public void WhenSectionContainsAdditionaLiteralAndNotClosed()
        {
            var template = TemplateSamples.OpenTagTextCloseTag;
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var syntax = new Syntax();
            var(isValid, wrongLexem, syntaxError) = syntax.Verify(lexemes);

            Assert.That(isValid, Is.False);
            Assert.That(wrongLexem, Is.Not.Null);
            Assert.That(syntaxError, Is.EqualTo("Ожидался идентификатор"));
        }
    }
}
