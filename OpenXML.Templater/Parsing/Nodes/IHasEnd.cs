using OpenXML.Templater.Lexing;

namespace OpenXML.Templater.Parsing.Nodes
{
    public interface IHasEnd
    {
        public EndSectionLexeme End { get; set; }
    }
}
