namespace OpenXML.Xlsx.Templater.Exceptions
{
    [Serializable]
    public class XlsxTemplateException : Exception
    {
        public XlsxTemplateException()
        {
        }

        public XlsxTemplateException(string? message) : base(message)
        {
        }

        public XlsxTemplateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void Throw(string message)
        {
            throw new XlsxTemplateException(message);
        }
    }
}
