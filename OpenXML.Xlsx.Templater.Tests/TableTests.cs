using NUnit.Framework;

namespace OpenXML.Xlsx.Templater.Tests
{
    [TestFixture]
    public class TableTests
    {
        [Test]
        public void WhenTableEmptyIndexerRow0()
        {
            var table = new Table();

            Assert.DoesNotThrow(() => table["Column1", 0] = "Value1");
            Assert.That(table.Columns.Count, Is.EqualTo(1));
            Assert.That(table.Columns.First().Name, Is.EqualTo("Column1"));
            Assert.That(table.Columns.First().Rows[0], Is.EqualTo("Value1"));
            Assert.That(table["Column1",0], Is.EqualTo("Value1"));
        }

        [Test]
        public void WheTableEmptyIndexerRow2()
        {
            var table = new Table();

            Assert.DoesNotThrow(() => table.SetCellValue("Column1", "Header1", 2, "Value2") );
            Assert.DoesNotThrow(() => table["Column1", 1]= "Value1" );
            Assert.DoesNotThrow(() => table["Column1", 0] = "Value0");
            Assert.That(table.Columns.Count, Is.EqualTo(1));
            Assert.That(table.Columns.First().Name, Is.EqualTo("Column1"));
            Assert.That(table.Columns.First().Header, Is.EqualTo("Header1"));

            Assert.That(table.Columns.First().Rows[0], Is.EqualTo("Value0"));
            Assert.That(table["Column1",0], Is.EqualTo("Value0"));

            Assert.That(table.Columns.First().Rows[1], Is.EqualTo("Value1"));
            Assert.That(table["Column1",1], Is.EqualTo("Value1"));

            Assert.That(table.Columns.First().Rows[2], Is.EqualTo("Value2"));
            Assert.That(table["Column1",2], Is.EqualTo("Value2"));
        }
    }
}
