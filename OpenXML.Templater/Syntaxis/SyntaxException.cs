using OpenXML.Templater.Lexing;
using System.Runtime.Serialization;


namespace OpenXML.Templater.Syntaxis
{
    [Serializable]
    public class SyntaxException : Exception
    {
        private readonly Lexem _lexem;

        public Lexem CauseLexem => _lexem;
        public SyntaxException(Lexem lexem)
        {
            _lexem = lexem;
        }

        public SyntaxException(Lexem lexem, string? message) : base(message)
        {
            _lexem = lexem;
        }

        public SyntaxException(Lexem lexem, string? message, Exception? innerException) : base(message, innerException)
        {
            _lexem = lexem;
        }

        public static void Throw(Lexem lexem) => throw new SyntaxException(lexem);
        public static void Throw(Lexem lexem, string? message) => throw new SyntaxException(lexem, message);
    }
}
