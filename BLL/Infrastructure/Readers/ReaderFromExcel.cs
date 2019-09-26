using BLL.Infrastructure.Readers.ReadModels;
using BLL.Interfaces;
using ClosedXML.Excel;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL.Infrastructure.Readers
{
    internal class ReaderFromExcel : IReader<XLWorkbook, ReadModelForExcel>
    {
        private readonly int workSheetNumber = 1;
        private string SeparatorFIO { get; set; } = " ";
        private IStringLocalizer<SharedResource> Localizer { get; set; }

        public ReaderFromExcel(IStringLocalizer<SharedResource> localizer)
        {
            Localizer = localizer;
        }
        public IAppActionResult<ReadModelForExcel> Read(XLWorkbook workbook)
        {
            IAppActionResult<ReadModelForExcel> result = new AppActionResult<ReadModelForExcel> { Data = new ReadModelForExcel() };
            var worksheet = workbook.Worksheet(workSheetNumber);
            var range = worksheet.RangeUsed();
            var rowCount = range.RowsUsed().Count();
            var colCount = range.ColumnsUsed().Count();
            if (rowCount <= 1)
                result.ErrorMessages.Add(Localizer["RowCountOneOrLess"]);
            var dataModelColCount = result.Data.GetType().GetProperties().Length;
            if (colCount < dataModelColCount)
                result.ErrorMessages.Add(Localizer["ColCountSmallerThanDataModels"]);
            if (result.ErrorMessages.Count != 0)
                return result;
            return ParseLoadedData(result, range, rowCount, colCount);
        }

        private IAppActionResult<ReadModelForExcel> ParseLoadedData(IAppActionResult<ReadModelForExcel> result, IXLRange range, int rowCount, int colCount)
        {
            Regex rSepForFIO = new Regex(SeparatorFIO);
            StringBuilder value = new StringBuilder();
            var position = "Position";
            var employee = "Employee";

            for (int i = 1; i < rowCount; ++i)
            {
                result.Data.Employees.Add(new Employee());
                result.Data.Employees[result.Data.Employees.Count - 1].Id = Guid.NewGuid();
                for (int j = 0; j < colCount; ++j)
                {
                    var rowData = range.Cell(i + 1, j + 1).Value.ToString();
                    var columnName = range.Cell(1, j + 1).Value.ToString();
                    if (columnName == position)
                    {
                        if (!result.Data.Positions.Exists(d => d.Name == rowData))
                        {
                            result.Data.Positions.Add(new Position { Id = Guid.NewGuid(), Name = rowData });
                            result.Data.Employees[result.Data.Employees.Count - 1].PositionId = result.Data.Positions[result.Data.Positions.Count - 1].Id;
                            result.Data.Employees[result.Data.Employees.Count - 1].Position = new Position
                            {
                                Id = result.Data.Positions[result.Data.Positions.Count - 1].Id,
                                Name = rowData
                            };
                        }
                        else
                        {
                            var pos = result.Data.Positions.Find(d => d.Name == rowData);
                            result.Data.Employees[result.Data.Employees.Count - 1].PositionId = pos.Id;
                            result.Data.Employees[result.Data.Employees.Count - 1].Position = new Position
                            {
                                Id = pos.Id,
                                Name = pos.Name
                            };
                        }
                    }
                    if (columnName == employee)
                    {
                        string[] dataName = rSepForFIO.Split(rowData);
                        result.Data.Employees[result.Data.Employees.Count - 1].Surname = dataName[0];
                        result.Data.Employees[result.Data.Employees.Count - 1].FirstName = dataName[1];
                        result.Data.Employees[result.Data.Employees.Count - 1].Patronymic = dataName[2];
                    }
                }
            }
            return result;
        }
    }
}
