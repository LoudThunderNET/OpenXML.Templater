using NPOI.XWPF.UserModel;

namespace OpenXML.Word.Templater.Lexemes
{
    internal interface IDocxLexem
    {
        IBodyElement? Element { get; set; }
    }
}
