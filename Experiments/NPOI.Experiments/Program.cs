//Create workbook
using MathNet.Numerics.Random;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OpenXML.Xlsx.Templater;
using System.Collections.Generic;
using Table = OpenXML.Xlsx.Templater.Table;

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

var templater = new XlsxTemplater();
templater.Render("XlsTemplates\\StaticTextOnly.xlsx", null!, "StaticTextOnly_Result.xlsx");
templater.Render("XlsTemplates\\StaticTextInline.xlsx", new DataModel 
{
    SingleFileds = 
    [
        new Field("date",DateTime.Now.ToString("dd.MMMM.yyyy")),
        new Field("total_quantity", 45652.ToString()),
        new Field("totalsum",546546548.ToString())
    ]
}, "StaticTextInline_Result.xlsx");

var rnd = new Random();
Table items = new Table();
items.Name = "items";
for (var i = 1; i <= 10; i++)
{
    items["id", i] = i.ToString();
    items["name", i] = "name"+i.ToString();
    var quantity = rnd.Next(1, 10);
    var price = rnd.NextDecimal() * 100;
    items["quantity", i] = quantity.ToString();
    items["price", i] = price.ToString("N");
    items["sum", i] = Math.Round(price*quantity, 2).ToString("N");
}
templater.Render("XlsTemplates\\StaticTextInlineSection.xlsx", new DataModel 
{
    SingleFileds = 
    [
        new Field("date",DateTime.Now.ToString("dd.MMMM.yyyy")),
        new Field("total_quantity", 45652.ToString()),
        new Field("totalsum",546546548.ToString())
    ],
    Tables = [items]
}, "StaticTextInlineSection_Result.xlsx");