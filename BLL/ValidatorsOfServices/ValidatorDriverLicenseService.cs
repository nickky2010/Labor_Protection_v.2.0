using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverLicenseService : AbstractValidatorOfServices<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        public override string EntityAlreadyExist { get => "DriverLicenseAlreadyExist"; }
        public override string EntityNotFound { get => "DriverLicenseNotFound"; }
        public override string EntitiesNotFound { get => "DriverLicensesNotFound"; }

        public ValidatorDriverLicenseService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        protected override async Task<IAppActionResult<DriverLicenseGetDTO>> ValidateConnectedEntities(IAppActionResult<DriverLicenseGetDTO> getResult, DriverLicense data, DriverLicenseAddDTO model)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                getResult.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                getResult.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            return getResult;
        }
        protected override async Task<IAppActionResult<DriverLicenseUpdateDTO>> ValidateConnectedEntities(IAppActionResult<DriverLicenseUpdateDTO> updateResult, DriverLicense data, DriverLicenseUpdateDTO model)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                updateResult.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                updateResult.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            return updateResult;
        }
    }
}
