using NUnit.Framework;
using OpenXML.Templater.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXML.Templater.Tests
{
    [TestFixture]
    public class StringSpanTest
    {
        [Test]
        public void WhenCreateFromNull()
        {
            StringSpan span;
            Assert.Catch(() => span = new StringSpan(null!));
        }

        [Test]
        public void WhenCreateFromEmpty()
        {
            StringSpan span = default;
            Assert.DoesNotThrow(() => span = new StringSpan(string.Empty));

            Assert.That(span.IsEmpty(), Is.True);
            Assert.Catch(()=>span.IsWhitespace(0));
            Assert.That(span.Length, Is.EqualTo(0));
            Assert.That(span.StartIndex, Is.EqualTo(0));
            Assert.That(span.EndIndex, Is.EqualTo(-1));
        }

        [Test]
        public void WhenCreateNotEmpty()
        {
            StringSpan span = default;
            Assert.DoesNotThrow(() => span = new StringSpan(" "));

            Assert.That(span.IsEmpty(), Is.False);
            Assert.DoesNotThrow(()=>span.IsWhitespace(0));
            Assert.That(span.IsWhitespace(0), Is.True);
            Assert.That(span.Length, Is.EqualTo(1));
            Assert.That(span.StartIndex, Is.EqualTo(0));
            Assert.That(span.EndIndex, Is.EqualTo(0));
        }

        [Test]
        public void WhenCreateFromNull0_1()
        {
            StringSpan span;
            Assert.Catch(() => span = new StringSpan(null!, 0, 1));
        }

        [Test]
        public void WhenCreateFromEmpty1_0()
        {
            StringSpan span = default;
            Assert.Catch(() => span = new StringSpan(string.Empty, 1, 0));
        }

        [Test]
        public void WhenCreateFromEmpty0_1()
        {
            StringSpan span = default;
            Assert.DoesNotThrow(() => span = new StringSpan(string.Empty, 0, 1));

            Assert.That(span.IsEmpty(), Is.True);
            Assert.Catch(()=>span.IsWhitespace(0));
            Assert.That(span.Length, Is.EqualTo(0));
            Assert.That(span.StartIndex, Is.EqualTo(0));
            Assert.That(span.EndIndex, Is.EqualTo(-1));
        }

        [Test]
        public void WhenCreateNotEmpty0_1()
        {
            StringSpan span = default;
            Assert.DoesNotThrow(() => span = new StringSpan(" ", 0, 1));

            Assert.That(span.IsEmpty(), Is.False);
            Assert.DoesNotThrow(()=>span.IsWhitespace(0));
            Assert.That(span.IsWhitespace(0), Is.True);
            Assert.That(span.Length, Is.EqualTo(1));
            Assert.That(span.StartIndex, Is.EqualTo(0));
            Assert.That(span.EndIndex, Is.EqualTo(0));
        }

        [Test]
        public void WhenCreateNotEmpty0_2()
        {
            StringSpan span = default;
            Assert.DoesNotThrow(() => span = new StringSpan(" ", 0, 2));

            Assert.That(span.IsEmpty(), Is.False);
            Assert.DoesNotThrow(()=>span.IsWhitespace(0));
            Assert.That(span.IsWhitespace(0), Is.True);
            Assert.That(span.Length, Is.EqualTo(1));
            Assert.That(span.StartIndex, Is.EqualTo(0));
            Assert.That(span.EndIndex, Is.EqualTo(0));
        }


        [Test]
        public void WhenSubstringFound0_7()
        {
            StringSpan span = default;
            Assert.DoesNotThrow(() => span = new StringSpan(" hell 0"));

            var index = span.IndexOf("he");

            Assert.That(span.IsEmpty(), Is.False);
            Assert.DoesNotThrow(()=>span.IsWhitespace(0));
            Assert.That(span.IsWhitespace(0), Is.True);
            Assert.That(index, Is.EqualTo(1));
            Assert.That(span.Length, Is.EqualTo(7));
            Assert.That(span.StartIndex, Is.EqualTo(0));
            Assert.That(span.EndIndex, Is.EqualTo(6));
        }

        [Test]
        public void WhenSubstringFound1_4()
        {
            StringSpan span = default;
            Assert.DoesNotThrow(() => span = new StringSpan(" hell 0", 1, 4));

            var index = span.IndexOf("he");

            Assert.That(span.IsEmpty(), Is.False);
            Assert.DoesNotThrow(()=>span.IsWhitespace(0));
            Assert.That(span.IsWhitespace(0), Is.False);
            Assert.That(index, Is.EqualTo(0));
            Assert.That(span.Length, Is.EqualTo(4));
            Assert.That(span.StartIndex, Is.EqualTo(1));
            Assert.That(span.EndIndex, Is.EqualTo(4));
        }

        [Test]
        public void WhenSubstringFound2_5()
        {
            StringSpan span = default;
            Assert.DoesNotThrow(() => span = new StringSpan(" hell 0", 2, 5));

            var index = span.IndexOf("he");

            Assert.That(span.IsEmpty(), Is.False);
            Assert.DoesNotThrow(()=>span.IsWhitespace(0));
            Assert.That(span.IsWhitespace(0), Is.False);
            Assert.That(index, Is.EqualTo(-1));
            Assert.That(span.Length, Is.EqualTo(4));
            Assert.That(span.StartIndex, Is.EqualTo(2));
            Assert.That(span.EndIndex, Is.EqualTo(5));
        }

        [Test]
        public void WhenEqualsFrom1()
        {
            StringSpan span = new StringSpan(" hell 0", 1, 4);

            var equal = span.Equals((StringSpan)"hell");

            Assert.That(span.IsEmpty(), Is.False);
            Assert.DoesNotThrow(()=>span.IsWhitespace(0));
            Assert.That(span.IsWhitespace(0), Is.False);
            Assert.That(equal, Is.True);
            Assert.That(span.Length, Is.EqualTo(4));
            Assert.That(span.StartIndex, Is.EqualTo(1));
            Assert.That(span.EndIndex, Is.EqualTo(4));
        }
    }
}
