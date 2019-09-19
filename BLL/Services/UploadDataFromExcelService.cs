using AutoMapper;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using System.Threading.Tasks;
using BLL.Services.Abstract;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using BLL.Infrastructure.Readers.ReadModels;
using BLL.Infrastructure.Readers;
using System.Net;
using System.Linq;

namespace BLL.Services
{
    internal class UploadDataFromExcelService : AbstractUploadDataFromFileService<XLWorkbook, ReadModelForExcel>
    {
        public UploadDataFromExcelService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorExcelFile(unitOfWorkService.UnitOfWorkLaborProtectionContext);
            Reader = new ReaderFromExcel(Localizer);
        }

        public override async Task<IAppActionResult> SynchronizeData(IFormFile file)
        {
            var result = Validator.ValidateFile(file, Localizer);
            if (!result.IsSuccess)
                return result;
            var readResult = Reader.Read(result.Data);
            Validator.SetStatus(readResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            if (!readResult.IsSuccess)
                return readResult;
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
            var empWhichNotExist = allEmp.Where(x=> !readResult.Data.Employees
                .Any(e=>e.Surname==x.Surname && e.FirstName == x.FirstName && e.Patronymic == x.Patronymic)).ToList();
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
            if(isSave)
                await UnitOfWork.SaveChangesAsync();
            readResult.Data = null;
            return readResult;
        }
    }
}
