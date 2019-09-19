using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using BLL.ValidatorsOfServices.Abstract;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverLicenseService : 
        AbstractValidatorOfCRUDDataBaseServices<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        protected override string EntityAlreadyExist { get => "DriverLicenseAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverLicenseNotFound"; }
        protected override string EntitiesNotFound { get => "DriverLicensesNotFound"; }

        public ValidatorDriverLicenseService(IUnitOfWork<LaborProtectionContext> unitOfWork)
            : base(unitOfWork) { }

        protected override async Task<IAppActionResult<DriverLicenseGetDTO>> ValidateConnectedAddEntities(DriverLicense data, 
            DriverLicenseAddDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                GetResult.ErrorMessages.Add(localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                GetResult.ErrorMessages.Add(localizer["DriverCategoriesNotFound"]);
            return GetResult;
        }
        protected override async Task<IAppActionResult<DriverLicenseGetDTO>> ValidateConnectedUpdateEntities(DriverLicense data, 
            DriverLicenseUpdateDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                GetResult.ErrorMessages.Add(localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                GetResult.ErrorMessages.Add(localizer["DriverCategoriesNotFound"]);
            return GetResult;
        }
    }
}
