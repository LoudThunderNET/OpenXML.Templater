//Create workbook
using MathNet.Numerics.Random;
using NPOI.XWPF.UserModel;
using OpenXML.Templater;
using OpenXML.Docx.Templater;
using OpenXML.Xlsx.Templater;
using System.Text.Json;
using Table = OpenXML.Templater.Table;

//IWorkbook wb = new XSSFWorkbook();
//ISheet ws = wb.CreateSheet("MySheet");
//
//
//var cellStyle = wb.CreateCellStyle();
//cellStyle.WrapText = true;
//cellStyle.BorderBottom = BorderStyle.Thin;
//cellStyle.BorderLeft = BorderStyle.Thin;
//cellStyle.BorderRight = BorderStyle.Thin;
//cellStyle.BorderLeft = BorderStyle.Thin;
//
////Set the value of the cell
//ICell cell = null;
//for (var rowIndex = 0; rowIndex <= 1; rowIndex++)
//{
//    var row = ws.CreateRow(rowIndex);
//    for (var col = 0; col <= 1; col++)
//    {
//        cell = row.CreateCell(col);
//        cell.CellStyle = cellStyle;
//    }
//}
//cell = ws.GetRow(0).GetCell(0);
//cell.SetCellValue("FileFormat.com");
//
////Merge the cell
//CellRangeAddress region = new CellRangeAddress(0, 1, 0, 1);
//ws.AddMergedRegion(region);
//
////Save the file
//FileStream file = File.Create("CellsMerge.xlsx");
//wb.Write(file, false);
//file.Close();
//
//return;

internal class Program
{
    private static void Main(string[] args)
    {
        /*
        var templater = new XlsxTemplater();
        templater.Render("XlsTemplates\\StaticTextOnly.xlsx", null!, "StaticTextOnly_Result.xlsx");
        var onlySingleFields = new DataModel
        {
            SingleFileds =
            [
                new Field("date",DateTime.Now.ToString("dd.MMMM.yyyy")),
        new Field("total_quantity", 45652.ToString()),
        new Field("totalsum",546546548.ToString())
            ]
        };

        templater.Render(
            "XlsTemplates\\StaticTextInline.xlsx",
            onlySingleFields,
            "StaticTextInline_Result.xlsx");

        var rnd = new Random();
        Table tables = new()
        {
            Name = "items"
        };
        for (var i = 1; i <= 10; i++)
        {
            var quantity = rnd.Next(1, 10);
            var price = rnd.NextDecimal() * 100;
            List<Field> cells =
            [
                new Field("id",i.ToString()),
        new Field("name","name"+i.ToString()),
        new Field("quantity",quantity.ToString()),
        new Field("price",price.ToString("N")),
        new Field("sum",Math.Round(price*quantity, 2).ToString("N")),
    ];
            tables.Rows.Add(new Row(cells));
        }
        var dataModel = new DataModel
        {
            SingleFileds =
            [
                new Field("date",DateTime.Now.ToString("dd.MMMM.yyyy")),
        new Field("total_quantity", 45652.ToString()),
        new Field("totalsum",546546548.ToString())
            ],
            Tables = [tables]
        };

        templater.Render(
            "XlsTemplates\\StaticTextInlineSection.xlsx",
            dataModel,
            "StaticTextInlineSection_Result.xlsx");
        */
        var dataModel = new DataModel
        {
            SingleFileds = 
            [ 
                new Field("date", DateTime.Now.ToString("dd.MM.yyyy"))
            ],
            Tables = 
            [
                new Table
                {
                    Name = "tab1",
                    Rows =
                [
                        new Row([new Field("number", "1"), new Field("name","Name1"), new Field("quantity", "2"), new Field("price","4564.45"), new Field("sum", "2342")]),
                        new Row([new Field("number", "2"), new Field("name","Name2"), new Field("quantity", "4"), new Field("price","74554.45"), new Field("sum", "4564652")])
                    ]
                }
            ]
        };
        var docxTemplater = new DocxTemplater();
        docxTemplater.Render("WordTemplates\\StaticTextOnly.docx", dataModel, "StaticTextOnly_Result.docx");
        Console.WriteLine("Done...");
    }
}