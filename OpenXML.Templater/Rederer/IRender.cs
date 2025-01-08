using OpenXML.Templater.Parsing.Nodes;

namespace OpenXML.Templater.Rederer
{
    /// <summary>
    /// Визуализатор.
    /// </summary>
    public interface IRender
    {
        /// <summary>
        /// Визуализирует <see cref="HorizSectionNode"/>.
        /// </summary>
        /// <param name="node">Узел горизонтальной секции.</param>
        void Render(HorizSectionNode node);

        /// <summary>
        /// Визуализирует <see cref="InlineNode"/>.
        /// </summary>
        /// <param name="node">Узел встроенной секции.</param>
        void Render(InlineNode node);

        /// <summary>
        /// Визуализирует <see cref="InvertedSectionNode"/>.
        /// </summary>
        /// <param name="node">Узел инверсной секции.</param>
        void Render(InvertedSectionNode node);

        /// <summary>
        /// Визуализирует <see cref="RootNode"/>.
        /// </summary>
        /// <param name="node">Корневой узел.</param>
        void Render(RootNode node);

        /// <summary>
        /// Визуализирует <see cref="SectionNode"/>.
        /// </summary>
        /// <param name="node">Узел вертикальной секции.</param>
        void Render(SectionNode node);

        /// <summary>
        /// Визуализирует <see cref="TextNode"/>.
        /// </summary>
        /// <param name="node">Узел текстовой секции.</param>
        void Render(TextNode node);

        /// <summary>
        /// Визуализирует <see cref="EmptyNode"/>.
        /// </summary>
        /// <param name="node">Узел пустой секции.</param>
        void Render(EmptyNode node);
    }
}