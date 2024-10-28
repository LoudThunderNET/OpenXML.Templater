using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    /// <summary>
    /// Фабрика лексем.
    /// </summary>
    public interface ILexemeFactory
    {
        /// <summary>
        /// Возвращает лексему закрытого тэга "}}".
        /// </summary>
        CloseTagLexeme CreateCloseTagLexeme();

        /// <summary>
        /// Возвращает лексему пустого текста.
        /// </summary>
        EmptyContent CreateEmptyContent();

        /// <summary>
        /// Возвращает лексему окончания секции "\".
        /// </summary>
        EndSectionLexeme CreateEndSectionLexeme(StringSpan content);

        /// <summary>
        /// Возвращает лексему горизонтальной секции ">".
        /// </summary>
        HorizSectionLexeme CreateHorizSectionLexeme(StringSpan content);

        /// <summary>
        /// Возвращает лексему идентификатора.
        /// </summary>
        InlineLexeme CreateInlineLexeme(StringSpan content);

        /// <summary>
        /// Возвращает лексему обратной секции "^".
        /// </summary>
        InvertedSectionLexeme CreateInvertedSectionLexeme(StringSpan content);

        /// <summary>
        /// Возвращает лексему открытого тэга "{{".
        /// </summary>
        OpenTagLexeme CreateOpenTagLexeme();

        /// <summary>
        /// Возвращает лексему начала секции "#".
        /// </summary>
        SectionLexeme CreateSectionLexeme(StringSpan content);

        /// <summary>
        /// Возвращает лексему текста.
        /// </summary>
        TextLexeme CreateTextLexeme(StringSpan content);
    }
}
