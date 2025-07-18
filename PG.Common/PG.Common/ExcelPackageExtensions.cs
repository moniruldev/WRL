﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Common
{
    using OfficeOpenXml;
    using System.Data;
    using System.Linq;

    public static class ExcelPackageExtensions
    {
        public static DataTable ToDataTable(this ExcelPackage package,string sheetName)
        {
            if (sheetName.Equals(string.Empty))
                sheetName = "Sheet1";

            ExcelWorksheet workSheet = package.Workbook.Worksheets[sheetName];

            //ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            if (workSheet != null)
            {
                if (workSheet.Dimension != null)
                {
                    foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                    {
                        table.Columns.Add(firstRowCell.Text);
                    }
                    for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                    {
                        var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                        var newRow = table.NewRow();
                        foreach (var cell in row)
                        {
                            newRow[cell.Start.Column - 1] = cell.Text;
                        }
                        table.Rows.Add(newRow);
                    }
                }
            }
            return table;
        }
    }
}
