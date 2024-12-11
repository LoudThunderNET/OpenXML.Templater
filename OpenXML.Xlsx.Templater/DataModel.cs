using OpenXML.Xlsx.Templater.Exceptions;

namespace OpenXML.Xlsx.Templater
{
    public class DataModel
    {
        public IReadOnlyCollection<Field> SingleFileds { get; set; } = [];
        public IReadOnlyCollection<Table> Tables { get; set; } = [];
    }

    public record Field
    {
        public Field()
        { 
            Name = Value = string.Empty;
        }

        public Field(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Table
    {
        private List<Row> _rows = [];

        public string Name { get; set; } = string.Empty;

        public List<Row> Rows
        {
            get => _rows;
            set
            {
                ArgumentNullException.ThrowIfNull(value);

                _rows = value;
            }
        }

        public string this[int rowIndex, string columnName]
        {
            get => GetCellValue(rowIndex, columnName);
            set => SetCellValue(rowIndex, columnName, value);
        }

        private string GetCellValue(int rowIndex, string columnName)
        {
            ValidateRowIndex(rowIndex);

            var cell = _rows[rowIndex].Cells.FirstOrDefault(f => f.Name == columnName);
            if (cell == null)
                XlsxTemplateException
                    .Throw($"Столбец {columnName} не найден.");

            return cell.Value;
        }

        private void ValidateRowIndex(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _rows.Count)
                XlsxTemplateException
                    .Throw($"Индекс {rowIndex} выходит за пределы диапазона [0..{_rows.Count - 1}].");
        }

        public void SetCellValue(int rowIndex, string columnName, string value)
        {
            ValidateRowIndex(rowIndex);
            var cell = _rows[rowIndex].Cells.FirstOrDefault(f => f.Name == columnName);
            if (cell == null)
                XlsxTemplateException.Throw($"Столбец {columnName} не найден.");

            cell.Value = value;
        }
    }

    public record Row
    {
        public Row()
        { 
            Cells = [];
        }
        public Row(IList<Field> cells)
        {
            Cells = cells;
        }

        public IList<Field> Cells { get; set; }
    }
}
