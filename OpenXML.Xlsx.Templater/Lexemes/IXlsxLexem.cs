using NPOI.SS.UserModel;

namespace OpenXML.Xlsx.Templater.Lexemes
{
    internal interface IXlsxLexem
    {
        ICell? Cell { get; set; }
    }
}
