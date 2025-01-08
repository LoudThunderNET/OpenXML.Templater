using NUnit.Framework;
using Table = OpenXML.Templater.Table;

namespace OpenXML.Xlsx.Templater.Tests
{
    [TestFixture]
    public class TableTests
    {
        [Test]
        public void WhenTableEmptyIndexerRow0()
        {
            var table = new Table();
            
            Assert.DoesNotThrow(() => table[0, "Column1"] = "Value1");
            Assert.That(table.Rows.Count, Is.EqualTo(1));
            Assert.That(table.Rows.First().Cells[0].Name, Is.EqualTo("Column1"));
            Assert.That(table.Rows.First().Cells[0], Is.EqualTo("Value1"));
            Assert.That(table[0, "Column1"], Is.EqualTo("Value1"));
        }

        [Test]
        public void WheTableEmptyIndexerRow2()
        {
            var table = new Table();

            Assert.DoesNotThrow(() => table.SetCellValue(2, "Column1", "Value2"));
            Assert.DoesNotThrow(() => table[1, "Column1"]= "Value1" );
            Assert.DoesNotThrow(() => table[0, "Column1"] = "Value0");
            Assert.That(table.Rows.Count, Is.EqualTo(1));
            Assert.That(table.Rows[0].Cells[0].Name, Is.EqualTo("Column1"));

            Assert.That(table.Rows.First().Cells[0], Is.EqualTo("Value0"));
            Assert.That(table[0, "Column1"], Is.EqualTo("Value0"));

            Assert.That(table.Rows.First().Cells[1], Is.EqualTo("Value1"));
            Assert.That(table[1, "Column1"], Is.EqualTo("Value1"));

            Assert.That(table.Rows.First().Cells[2], Is.EqualTo("Value2"));
            Assert.That(table[2, "Column1"], Is.EqualTo("Value2"));
        }
    }
}
