using OpenXML.Templater.Parsing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    /// <summary>
    /// Лексема.
    /// </summary>
    /// <param name="Content"></param>
    public abstract record Lexem(StringSpan Content)
    {
        /// <summary>
        /// Принимает парсер абстрактного синтаксического дерева.
        /// </summary>
        /// <param name="parser"></param>
        public abstract void Accept(Parser parser);
    }
}
