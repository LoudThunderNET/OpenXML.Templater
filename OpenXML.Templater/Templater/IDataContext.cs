using System.Collections;

namespace OpenXML.Templater
{
    public interface IDataContext<TDataModel>
        where TDataModel : class
    {
        void RestoreContext();
        string GetValue(string filedName);
        EnumerableState MoveNext();
    }

    public interface IContextSetter<TData>
    { 
        void SetContext(TData value);
    }

    public readonly struct EnumerableState(bool isFirst, int index, bool isLast)
    {
        public readonly bool IsFirst = isFirst;
        public readonly int Index = index;
        public readonly bool IsLast = isLast;
    }

    public class EnumeratorState(int index, int count, IEnumerator enumerator) : IEnumerator
    { 
        public int Index = index;
        public int Count = count;
        private readonly IEnumerator _enumerator = enumerator;

        public object Current => _enumerator.Current;

        public void Deconstruct(out int index, out int count,out IEnumerator enumerator)
        {

            index = Index;
            count = Count;
            enumerator = _enumerator;
        }

        public bool MoveNext() => _enumerator.MoveNext();
        public void Reset() => _enumerator.Reset();
    }

    public interface IDataModelContext : IDataContext<DataModel>,
        IContextSetter<Table>,
        IContextSetter<Row>
    { 
    }

    public class DataModelContext(DataModel dataModel) : IDataModelContext
    {
        private object? _current = dataModel;
        private readonly static EnumeratorState EmptyEnumerator = new(-1, 0, Array.Empty<string>().GetEnumerator());
        private EnumeratorState _currentEnumerator = EmptyEnumerator;
        private readonly Stack<object> _contextStack = new();
        private readonly Stack<EnumeratorState> _enumeratorStack = new();
        private int _enumerableIndex = -1;
        private int _enumerableCount = -1;

        public string GetValue(string fieldName) =>
            _current switch
            {
                DataModel dataModel => dataModel.SingleFileds
                                        .FirstOrDefault(f => EqualsInvariantCultureIgnoreCase(f.Name, fieldName))?.Value
                                        ?? string.Empty,
                Table table => string.Empty,
                Row row => row.Cells.FirstOrDefault(c => EqualsInvariantCultureIgnoreCase(c.Name, fieldName))?.Value
                                        ?? string.Empty,
                _ => string.Empty,
            };


        public EnumerableState MoveNext()
        {
            if (_currentEnumerator == null)
                return new EnumerableState(false, -1, false);

            if (_currentEnumerator.MoveNext())
            {
                _enumerableIndex++;
                _current = _currentEnumerator.Current;
                return new EnumerableState(_enumerableIndex == 0, _enumerableIndex, _enumerableIndex == _enumerableCount);
            }

            return new EnumerableState(false, -1, false);
        }

        public void RestoreContext()
        {
            if (_contextStack.Count > 0)
            {
                _current = _contextStack.Pop();
                if (_current is IEnumerable)
                {
                    _currentEnumerator = _enumeratorStack.Pop();
                }
                else
                {
                    _currentEnumerator = null;
                    _enumerableCount = -1;
                    _enumerableIndex = -1;
                }
            }
        }

        public void SetContext(DataModel dataModel)
        {
            if(_current != null)
                _contextStack.Push(_current);

            _current = dataModel;
        }

        public void SetContext(Table table)
        { 
            if(_current != null)
                _contextStack.Push(_current);
            _current = table;
        }

        public void SetContext(Row row)
        { 
            if(_current != null)
                _contextStack.Push(_current);
            _current = row;
        }

        private bool EqualsInvariantCultureIgnoreCase(string string1, string string2) =>
            string.Equals(string1, string2, StringComparison.InvariantCultureIgnoreCase);
    }
}
