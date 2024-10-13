using OpenXML.Templater.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;


namespace OpenXML.Templater.Primitives
{
    public struct StringSpan
    {
        private string _source;

        public StringSpan(string source)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            _source = source;
            StartIndex = 0;
            EndIndex = source == null ? -1: source.Length - 1;
        }

        public StringSpan(string source, int startIndex, int endIndex) : this(source)
        {
            _source = source;
            if (startIndex > endIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), nameof(startIndex) + " should be less then " + nameof(endIndex));
            }
            StartIndex = startIndex;
            EndIndex = source == null ? -1 : TrimEndIndex(endIndex, source.Length);
        }

        private int TrimEndIndex(int trimmedValue, int length)
        {
            return length == 0 ? -1 : (trimmedValue > (length - 1) ? (length - 1) : trimmedValue);
        }

        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }

        public static StringSpan Empty() => new StringSpan(string.Empty);

        public int IndexOf(string searched)
        {
            var index = _source.IndexOf(searched, StartIndex, Length);

            return index == -1 ? index : index - StartIndex;
        }

        public StringSpan Trim()
        {
            if(IsEmpty())
                return Empty();

            int start;
            int endIndex = Length - 1;
            for (start = 0; start <= endIndex && IsWhitespace(start); start++) ;

            int end;
            for (end = endIndex; end > 0 && IsWhitespace(end); end--) ;

            if (start == 0 && end == endIndex)
                return this;

            return Slice(start, end);
        }

        public char this[int index] => 0 <= index &&  index <= (Length - 1)
            ? _source[StartIndex+index] 
            : throw new ArgumentOutOfRangeException(nameof(index), nameof(index)+" is great then " +nameof(EndIndex));

        public bool IsWhitespace(int index)
        {
            if (0 > index || index > (Length - 1))
                throw new IndexOutOfRangeException($"{nameof(index)} is out of [{StartIndex}:{EndIndex}]");

            return _source[StartIndex + index] == ' ';
        }

        public bool IsEmpty()
        {
            return (EndIndex - StartIndex + 1) == 0;
        }

        public StringSpan Slice(int startIndex, int endIndex)
        {
            if (startIndex > _source.Length - 1)
            {
                return Empty();
            }

            if (StartIndex + startIndex > StartIndex + endIndex)
                return Empty();

            return new StringSpan(_source, StartIndex + startIndex, StartIndex + endIndex);
        }

        public StringSpan Slice(int startIndex) => Slice(startIndex, Length - 1);


        public int Length => EndIndex - StartIndex + 1;

        public override string ToString()
        {
            if (_source == null)
                return string.Empty;

            unsafe
            {
                fixed (char* sourcePtr = _source )
                {
                    return new string(sourcePtr, StartIndex, EndIndex - StartIndex + 1);
                }
            }
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if(obj is StringSpan otherSpan)
            {
                if (Length != otherSpan.Length)
                    return false;

                bool equals = true;
                var otherSpanIndex = 0;
                for (var i = StartIndex; i <= EndIndex; i++)
                {
                    if (_source[i] == otherSpan[otherSpanIndex])
                    {
                        equals = true;
                        otherSpanIndex++;
                        if(otherSpanIndex > otherSpan.Length -1)
                            return true;
                    }
                    else
                    {
                        equals = false;
                        otherSpanIndex = 0;
                    }
                }

                return equals;
            }

            return false;
        }

        public static implicit operator StringSpan(string otherString) => new StringSpan(otherString);
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
