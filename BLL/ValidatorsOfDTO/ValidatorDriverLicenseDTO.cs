using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using BLL.ValidatorsOfDTO.Abstract;
using System;
using System.Collections.Generic;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorDriverLicenseDTO : 
        AbstractValidatorDTO<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        protected override string EntityAlreadyExist { get => "DriverLicenseAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverLicenseNotFound"; }
        protected override string EntitiesNotFound { get => "DriverLicensesNotFound"; }

        public ValidatorDriverLicenseDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        protected override async Task<IAppActionResult<DriverLicense>> ValidateConnectedEntities(DriverLicense data, 
            DriverLicenseAddDTO model)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                DataResult.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                DataResult.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            return DataResult;
        }
        protected override async Task<IAppActionResult<DriverLicense>> ValidateConnectedEntities(DriverLicense data, 
            DriverLicenseUpdateDTO model)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                DataResult.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                DataResult.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            return DataResult;
        }

        protected override Task<DriverLicense> FindDataAsync(Guid id) =>
            UnitOfWork.DriverLicenses.FindAsync(x => x.Id == id);

        protected override Task<List<DriverLicense>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverLicenses.GetPageAsync(startItem, countItem);

        protected override Task<DriverLicense> FindDataIfAddAsync(DriverLicenseAddDTO modelDTO) =>
            UnitOfWork.DriverLicenses.FindAsync(x => x.SerialNumber == modelDTO.SerialNumber);
    }
}
