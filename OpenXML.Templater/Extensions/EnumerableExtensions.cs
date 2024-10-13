using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenXML.Templater.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();
        public static bool IsNotEmpty<T>(this IEnumerable<T> collection) => !collection.IsEmpty();
    }
}
