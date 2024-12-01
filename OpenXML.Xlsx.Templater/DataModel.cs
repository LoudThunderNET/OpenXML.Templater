using OpenXML.Xlsx.Templater.Exceptions;

namespace OpenXML.Xlsx.Templater
{
    public class DataModel
    {
        public IReadOnlyCollection<Field> SingleFileds { get; set; } = [];
        public IReadOnlyCollection<Table> Tables { get; set; } = [];
    }

    public record Field(string Name, string Value);

    public class Table
    {
        private int _rowsCount = 0;
        public string Name { get; set; } = string.Empty;
        private List<Column> _cells = new();

        public IReadOnlyCollection<Column> Columns
        {
            get => _cells.ToArray();
            set
            {
                ArgumentNullException.ThrowIfNull(value);

                _cells = [.. value];
                _rowsCount = _cells.FirstOrDefault()?.Rows.Count ?? 0;
            }
        }

        public string this[string columnName, int rowIndex]
        {
            get => GetCellValue(columnName, rowIndex);
            set => SetCellValue(columnName, rowIndex, value);
        }

        private string GetCellValue(string columnName, int rowIndex)
        {
            var column = _cells.FirstOrDefault(k => k.Name == columnName);
            if (column == null)
            {
                XlsxTemplateException.Throw("Столбец "+columnName+" не найден.");
            }
            if (rowIndex >= column!.Rows.Count)
            {
                XlsxTemplateException.Throw("Индекс " + rowIndex + " выходит за пределы строк");
            }

            return column.Rows[rowIndex];
        }

        public void SetCellValue(string columnName, string columnHeader, int rowIndex, string value)
        {
            var column = _cells.FirstOrDefault(c => c.Name == columnName);
            if (column == null)
            {
                var rows = Enumerable
                    .Range(0, _rowsCount)
                    .Select(r => string.Empty)
                    .ToList();
                column = new Column(columnHeader, columnName, rows);
                _cells.Add(column);
            }

            if (rowIndex >= _rowsCount)
            {
                foreach (var cellColumn in _cells)
                {
                    for (var i = _rowsCount; i <= rowIndex; i++)
                        cellColumn.Rows.Add(string.Empty);
                }
                _rowsCount = rowIndex + 1;
            }

            column.Rows[rowIndex] = value;
        }

        private void SetCellValue(string columnName, int rowIndex, string value)
        {
            SetCellValue(columnName, string.Empty, rowIndex, value);
        }
    }

    public record Column(string Header, string Name, IList<string> Rows);

}
