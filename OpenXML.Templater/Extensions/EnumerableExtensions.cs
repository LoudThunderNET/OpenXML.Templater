namespace OpenXML.Templater.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> collection) => 
            collection == null || !collection.Any();

        public static bool IsNotEmpty<T>(this IEnumerable<T> collection) => 
            !collection.IsEmpty();
    }
}
