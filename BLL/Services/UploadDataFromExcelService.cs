using AutoMapper;
using BLL.Infrastructure;
using BLL.Infrastructure.Extentions;
using BLL.Infrastructure.Readers;
using BLL.Infrastructure.Readers.ReadModels;
using BLL.Interfaces;
using BLL.Services.Abstract;
using BLL.ValidatorsOfDTO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class UploadDataFromExcelService : AbstractBaseService,
        IUploadDataFromFileService<XLWorkbook, ReadModelForExcel>
    {
        public UploadDataFromExcelService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        { }

        public async Task<IAppActionResult> SynchronizeData(IFormFile file)
        {
            IValidatorOfUploadFile<XLWorkbook> validator = new ValidatorExcelFile(UnitOfWork, Localizer);
            IReader<XLWorkbook, ReadModelForExcel> reader = new ReaderFromExcel(Localizer);
            var result = new AppActionResult();
            var resultData = validator.ValidateFile(file);
            result.SetResult(resultData);
            if (!result.IsSuccess)
                return result;
            var readResult = reader.Read(resultData.Data);
            readResult.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
            result.SetResult(readResult);
            if (!result.IsSuccess)
                return result;
            bool isSave = false;
            var allPos = await UnitOfWork.Positions.GetAllAsync();
            var positionNames = readResult.Data.Positions.Select(x => x.Name).ToList();
            var posWhichNotExist = allPos.Where(x => !positionNames.Contains(x.Name)).ToList();
            if (posWhichNotExist.Count != 0)
            {
                UnitOfWork.Positions.DeleteRange(posWhichNotExist);
                await UnitOfWork.SaveChangesAsync();
            }
            var allEmp = await UnitOfWork.Employees.GetAllAsync();
            var empWhichNotExist = allEmp.Where(x => !readResult.Data.Employees
                .Any(e => e.Surname == x.Surname && e.FirstName == x.FirstName && e.Patronymic == x.Patronymic)).ToList();
            if (empWhichNotExist.Count != 0)
            {
                UnitOfWork.Employees.DeleteRange(empWhichNotExist);
                isSave = true;
            }
            var empWhichExist = readResult.Data.Employees.Where(x => !allEmp
                .Any(e => e.Surname == x.Surname && e.FirstName == x.FirstName && e.Patronymic == x.Patronymic)).ToList();
            var empWhichExistByPos = allEmp.Where(x => positionNames.Contains(x.Position.Name)).ToList();
            if (empWhichExistByPos.Count != 0)
            {
                empWhichExist.ForEach(x =>
                {
                    var emp = empWhichExistByPos.Find(y => y.Position.Name == x.Position.Name);
                    if (emp != null)
                    {
                        x.PositionId = emp.PositionId;
                        x.Position = null;
                    }
                });
            }
            if (empWhichExist.Count != 0)
            {
                await UnitOfWork.Employees.AddRangeAsync(empWhichExist);
                isSave = true;
            }
            if (isSave)
                await UnitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
