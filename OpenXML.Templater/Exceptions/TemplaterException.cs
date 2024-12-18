using System.Diagnostics.CodeAnalysis;

namespace OpenXML.Templater.Exceptions
{
    [Serializable]
    public class TemplaterException : Exception
    {
        public TemplaterException()
        {
        }

        public TemplaterException(string? message) : base(message)
        {
        }

        public TemplaterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        [DoesNotReturn]
        public static void Throw(string message)
        {
            throw new TemplaterException(message);
        }
    }
}
