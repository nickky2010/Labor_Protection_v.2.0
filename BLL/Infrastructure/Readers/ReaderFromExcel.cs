using BLL.Infrastructure.Readers.ReadModels;
using BLL.Interfaces;
using ClosedXML.Excel;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL.Infrastructure.Readers
{
    internal class ReaderFromExcel: IReader<XLWorkbook, ReadModelForExcel>
    {
        public IAppActionResult<ReadModelForExcel> Result { get; set; }
        private List<string>[] buffer;
        private IXLRange range;
        private int workSheetNumber = 1;
        private int RowCount { get; set; }
        private int ColCount { get; set; }
        private string SeparatorFIO { get; set; } = " ";
        public IStringLocalizer<SharedResource> Localizer { get; set; }
        public int DataModelColCount { get; set; }

        public ReaderFromExcel(IStringLocalizer<SharedResource> localizer)
        {
            Localizer = localizer;
            Result = new AppActionResult<ReadModelForExcel> { Data = new ReadModelForExcel()};
        }
        public IAppActionResult<ReadModelForExcel> Read(XLWorkbook workbook)
        {
            OpenFile(workbook);
            if (RowCount <= 1)
                Result.ErrorMessages.Add(Localizer["RowCountOneOrLess"]);
            DataModelColCount = Result.Data.GetType().GetProperties().Length;
            if (ColCount < DataModelColCount)
                Result.ErrorMessages.Add(Localizer["ColCountSmallerThanDataModels"]);
            if (Result.ErrorMessages.Count != 0)
                return Result;
            ReadFileInBufer();
            ParseLoadedData();
            return Result;
        }

        private void OpenFile(XLWorkbook workbook)
        {
            var worksheet = workbook.Worksheet(workSheetNumber);
            range = worksheet.RangeUsed();
            RowCount = range.RowsUsed().Count();
            ColCount = range.ColumnsUsed().Count();
        }
        private void ReadFileInBufer()
        {
            buffer = new List<string>[ColCount];
            for (int j = 0; j < ColCount; ++j)
            {
                buffer[j] = new List<string>();
            }
            for (int j = 0; j < ColCount; ++j)
            {
                for (int i = 0; i < RowCount; ++i)
                {
                    buffer[j].Add(range.Cell(i + 1, j + 1).Value.ToString());
                }
            }
        }

        private void ParseLoadedData()
        {
            Regex rSepForFIO = new Regex(SeparatorFIO);
            StringBuilder value = new StringBuilder();
            var position = "Position";
            var employee = "Employee";

            for (int i = 1; i < RowCount; ++i)
            {
                Result.Data.Employees.Add(new Employee());
                Result.Data.Employees[Result.Data.Employees.Count - 1].Id = Guid.NewGuid();
                for (int j = 0; j < ColCount; ++j)
                {
                    if (buffer[j][0] == position)
                    {
                        if (!Result.Data.Positions.Exists(d => d.Name == buffer[j][i]))
                        {
                            Result.Data.Positions.Add(new Position {  Id = Guid.NewGuid(), Name = buffer[j][i] });
                            Result.Data.Employees[Result.Data.Employees.Count - 1].PositionId = Result.Data.Positions[Result.Data.Positions.Count - 1].Id;
                            Result.Data.Employees[Result.Data.Employees.Count - 1].Position = new Position
                            {
                                Id = Result.Data.Positions[Result.Data.Positions.Count - 1].Id,
                                Name = buffer[j][i]
                            };
                        }
                        else
                        {
                            var pos = Result.Data.Positions.Find(d => d.Name == buffer[j][i]);
                            Result.Data.Employees[Result.Data.Employees.Count - 1].PositionId = pos.Id;
                            Result.Data.Employees[Result.Data.Employees.Count - 1].Position = new Position
                            {
                                Id = pos.Id,
                                Name = pos.Name
                            };
                        }
                    }
                    if (buffer[j][0] == employee)
                    {
                        string[] dataName = rSepForFIO.Split(buffer[j][i]);
                        Result.Data.Employees[Result.Data.Employees.Count - 1].Surname = dataName[0];
                        Result.Data.Employees[Result.Data.Employees.Count - 1].FirstName = dataName[1];
                        Result.Data.Employees[Result.Data.Employees.Count - 1].Patronymic = dataName[2];
                    }
                }
            }
        }
    }
}
