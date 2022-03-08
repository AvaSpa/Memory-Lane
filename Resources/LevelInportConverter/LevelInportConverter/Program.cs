using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LevelInportConverter
{
    class Program
    {
        const string excelFilePath = @"D:\GIT\Memory-Lane\Data\Levels.xlsx";
        const string exportFilePath = @"D:\GIT\Memory-Lane\Data\Levels.txt";

        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                var sheet = package.Workbook.Worksheets.First();
                var rows = sheet.Dimension.End.Row;
                var columns = sheet.Dimension.End.Column;

                var lines = new List<string>();

                for (var i = 3; i < rows; i++)
                {
                    var cell = sheet.Cells[i, 3];
                    var rawLevel = cell.GetValue<string>();
                    lines.Add(rawLevel);
                }

                File.WriteAllLines(exportFilePath, lines.ToArray());
            }

            Console.ReadKey();
        }
    }
}
