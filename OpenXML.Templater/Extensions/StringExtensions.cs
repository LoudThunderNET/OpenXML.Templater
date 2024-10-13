namespace OpenXML.Templater.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string src) => src == null || src.Length == 0;
        public static bool IsNotNullOrEmpty(this string src) => !src.IsNullOrEmpty();
    }
}
