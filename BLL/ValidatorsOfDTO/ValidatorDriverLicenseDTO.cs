using BLL.DTO.DriverLicenses;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO.Abstract;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorDriverLicenseDTO :
        AbstractCRUDValidatorDTO<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        protected override string EntityAlreadyExist { get => "DriverLicenseAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverLicenseNotFound"; }
        protected override string EntitiesNotFound { get => "DriverLicensesNotFound"; }

        public ValidatorDriverLicenseDTO(IUnitOfWork<LaborProtectionContext> unitOfWork)
            : base(unitOfWork) { }

        public override async Task<IAppActionResult> ValidateAdd(DriverLicenseAddDTO model)
        {
            var result = await base.ValidateAdd(model);
            if (result.IsSuccess)
                ValidateConnected(result, model.EmployeeId, model.DriverCategoriesId);
            return result;
        }

        public override async Task<IAppActionResult<DriverLicense>> ValidateUpdate(DriverLicenseUpdateDTO model)
        {
            var result = await base.ValidateUpdate(model);
            if (result.IsSuccess)
                ValidateConnected(result, model.EmployeeId, model.DriverCategoriesId);
            if (!result.IsSuccess)
                result.Data = default;
            return result;
        }

        protected override Task<DriverLicense> FindDataAsync(Guid id) =>
            UnitOfWork.DriverLicenses.FindAsync(x => x.Id == id);

        protected override Task<List<DriverLicense>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverLicenses.GetPageAsync(startItem, countItem);

        protected override Task<DriverLicense> FindDataAsync(DriverLicenseAddDTO modelDTO) =>
            UnitOfWork.DriverLicenses.FindAsync(x => x.SerialNumber == modelDTO.SerialNumber);
        protected override Task<int> GetCountElementAsync() => UnitOfWork.DriverLicenses.CountElementAsync();

        private async void ValidateConnected(IAppActionResult result, Guid employeeId, IList<Guid> driverCategoriesId)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(employeeId))
                result.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(driverCategoriesId))
                result.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
        }
    }
}
