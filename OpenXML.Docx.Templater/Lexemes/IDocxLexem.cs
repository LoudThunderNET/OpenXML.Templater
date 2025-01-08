using NPOI.XWPF.UserModel;

namespace OpenXML.Docx.Templater.Lexemes
{
    internal interface IDocxLexem
    {
        XWPFParagraph Element { get; set; }
    }
}
