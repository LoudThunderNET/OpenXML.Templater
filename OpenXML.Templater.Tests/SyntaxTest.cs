using NUnit.Framework;
using OpenXML.Templater.Syntaxing;
using OpenXML.Templater.Syntaxis;

namespace OpenXML.Templater.Tests
{
    [TestFixture]
    public class SyntaxTest
    {
        [Test]
        public void WhenIndetifierExpected()
        {
            var template = "{{435}}";
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var syntax = new Syntax();
            Assert.Catch<SyntaxException>(()=>syntax.Verify(lexemes));
        }

        [Test]
        public void WhenIndetifierExpected1()
        {
            var template = "{{dsfsd132 {{#section}} }}";
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var syntax = new Syntax();
            Assert.Catch<SyntaxException>(()=>syntax.Verify(lexemes));
        }

        [Test]
        public void WhenSectionNotClosed()
        {
            var template = "{{#section}} {{fdfd}}";
            var lexer = new Lexer();
            var lexemes = lexer.Analize(template);
            var syntax = new Syntax();
            Assert.Catch<SyntaxException>(()=>syntax.Verify(lexemes));
        }
    }
}
