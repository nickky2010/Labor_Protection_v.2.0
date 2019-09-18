using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverMedicalCertificateService : 
        AbstractValidatorOfServices<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>
    {
        protected override string EntityAlreadyExist { get => "DriverMedicalCertificateAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverMedicalCertificateNotFound"; }
        protected override string EntitiesNotFound { get => "DriverMedicalCertificatesNotFound"; }

        public ValidatorDriverMedicalCertificateService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        protected override async Task<IAppActionResult<DriverMedicalCertificateGetDTO>> ValidateConnectedAddEntities(DriverMedicalCertificate data, 
            DriverMedicalCertificateAddDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                GetResult.ErrorMessages.Add(localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                GetResult.ErrorMessages.Add(localizer["DriverCategoriesNotFound"]);
            return GetResult;
        }
        protected override async Task<IAppActionResult<DriverMedicalCertificateGetDTO>> ValidateConnectedUpdateEntities(DriverMedicalCertificate data, 
            DriverMedicalCertificateUpdateDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                GetResult.ErrorMessages.Add(localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                GetResult.ErrorMessages.Add(localizer["DriverCategoriesNotFound"]);
            return GetResult;
        }
    }
}
