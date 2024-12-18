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

    public interface ContextSetter<TData>
    { 
        void SetContext(TData value);
    }

    public readonly struct EnumerableState
    {
        public readonly bool IsFirst;
        public readonly int Index;
        public readonly bool IsLast;

        public EnumerableState(bool isFirst, int index, bool isLast)
        {
            IsFirst = isFirst;
            Index = index;
            IsLast = isLast;
        }
    }

    public class EnumeratorState : IEnumerator
    { 
        public int Index;
        public int Count;
        private IEnumerator _enumerator;

        public EnumeratorState(int index, int count, IEnumerator enumerator)
        {
            Index = index;
            Count = count;
            _enumerator = enumerator;
        }

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
        ContextSetter<Table>,
        ContextSetter<Row>
    { 
    }

    public class DataModelContext : IDataModelContext
    {
        private object? _current;
        private static EnumeratorState EmptyEnumerator = new EnumeratorState(-1, 0, Array.Empty<string>().GetEnumerator());
        private EnumeratorState _currentEnumerator;
        private Stack<object> _contextStack;
        private Stack<EnumeratorState> _enumeratorStack;
        private int _enumerableIndex = -1;
        private int _enumerableCount = -1;

        public DataModelContext(DataModel dataModel)
        {
            _current = dataModel;
            _contextStack = new Stack<object>();
            _enumeratorStack = new Stack<EnumeratorState>();
            _currentEnumerator = EmptyEnumerator;
        }

        public string GetValue(string fieldName)
        {
            switch (_current)
            {
                case DataModel dataModel:
                    return dataModel.SingleFileds
                        .FirstOrDefault(f => EqualsInvariantCultureIgnoreCase(f.Name, fieldName))?.Value 
                        ?? string.Empty;
                case Table table:
                    return string.Empty;

                case Row row:
                    return row.Cells.FirstOrDefault(c=> EqualsInvariantCultureIgnoreCase(c.Name, fieldName))?.Value 
                        ?? string.Empty;
                default:
                    return string.Empty;
            }
        }

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
                if (_current is IEnumerable enumerable)
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
