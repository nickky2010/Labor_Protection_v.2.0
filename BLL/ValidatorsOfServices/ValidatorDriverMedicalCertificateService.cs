using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverMedicalCertificateService : AbstractValidatorOfServices<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>
    {
        public override string EntityAlreadyExist { get => "DriverMedicalCertificateAlreadyExist"; }
        public override string EntityNotFound { get => "DriverMedicalCertificateNotFound"; }
        public override string EntitiesNotFound { get => "DriverMedicalCertificatesNotFound"; }

        public ValidatorDriverMedicalCertificateService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        protected override async Task<IAppActionResult<DriverMedicalCertificateGetDTO>> ValidateConnectedEntities(IAppActionResult<DriverMedicalCertificateGetDTO> getResult, DriverMedicalCertificate data, DriverMedicalCertificateAddDTO model)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                getResult.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                getResult.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            return getResult;
        }
        protected override async Task<IAppActionResult<DriverMedicalCertificateUpdateDTO>> ValidateConnectedEntities(IAppActionResult<DriverMedicalCertificateUpdateDTO> updateResult, DriverMedicalCertificate data, DriverMedicalCertificateUpdateDTO model)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                updateResult.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                updateResult.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            return updateResult;
        }
    }
}
