//Create workbook
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OpenXML.Xlsx.Templater;
using System.Collections.Generic;

//IWorkbook wb = new XSSFWorkbook();
//ISheet ws = wb.CreateSheet("MySheet");
//
////Set the value of the cell
//ws.CreateRow(1).CreateCell(0).SetCellValue("FileFormat.com");
//
////Merge the cell
////CellRangeAddress region = new CellRangeAddress(0, 1, 0, 1);
////ws.AddMergedRegion(region);
//
////Save the file
//FileStream file = File.Create("CellsMerge.xlsx");
//wb.Write(file, false);
//file.Close();

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