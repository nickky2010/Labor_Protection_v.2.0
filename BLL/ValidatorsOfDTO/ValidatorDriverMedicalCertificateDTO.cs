using BLL.DTO.DriverMedicalCertificates;
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
    internal class ValidatorDriverMedicalCertificateDTO : 
        AbstractValidatorDTO<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>
    {
        protected override string EntityAlreadyExist { get => "DriverMedicalCertificateAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverMedicalCertificateNotFound"; }
        protected override string EntitiesNotFound { get => "DriverMedicalCertificatesNotFound"; }

        public ValidatorDriverMedicalCertificateDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        protected override async Task<IAppActionResult<DriverMedicalCertificate>> ValidateConnectedEntities(DriverMedicalCertificate data, 
            DriverMedicalCertificateAddDTO model)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                DataResult.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                DataResult.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            return DataResult;
        }
        protected override async Task<IAppActionResult<DriverMedicalCertificate>> ValidateConnectedEntities(DriverMedicalCertificate data, 
            DriverMedicalCertificateUpdateDTO model)
        {
            if (!await UnitOfWork.Employees.IsIdExistAsync(model.EmployeeId))
                DataResult.ErrorMessages.Add(Localizer["EmployeeNotFound"]);
            if (!await UnitOfWork.DriverCategories.IsAllIdExistAsync(model.DriverCategoriesId))
                DataResult.ErrorMessages.Add(Localizer["DriverCategoriesNotFound"]);
            return DataResult;
        }

        protected override Task<DriverMedicalCertificate> FindDataAsync(Guid id) =>
            UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.Id == id);

        protected override Task<List<DriverMedicalCertificate>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverMedicalCertificates.GetPageAsync(startItem, countItem);

        protected override Task<DriverMedicalCertificate> FindDataIfAddAsync(DriverMedicalCertificateAddDTO modelDTO) =>
            UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.SerialNumber == modelDTO.SerialNumber);
    }
}
