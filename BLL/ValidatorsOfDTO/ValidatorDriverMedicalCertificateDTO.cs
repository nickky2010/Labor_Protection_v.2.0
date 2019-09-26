using BLL.DTO.DriverMedicalCertificates;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO.Abstract;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

        public override async Task<IAppActionResult> ValidateAdd(DriverMedicalCertificateAddDTO model)
        {
            var result = await base.ValidateAdd(model);
            ValidateConnected(result, model.EmployeeId, model.DriverCategoriesId);
            return result;
        }

        public override async Task<IAppActionResult<DriverMedicalCertificate>> ValidateUpdate(DriverMedicalCertificateUpdateDTO model)
        {
            var result = await base.ValidateUpdate(model);
            ValidateConnected(result, model.EmployeeId, model.DriverCategoriesId);
            return result;
        }

        protected override Task<DriverMedicalCertificate> FindDataAsync(Guid id) =>
            UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.Id == id);

        protected override Task<List<DriverMedicalCertificate>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverMedicalCertificates.GetPageAsync(startItem, countItem);

        protected override Task<DriverMedicalCertificate> FindDataAsync(DriverMedicalCertificateAddDTO modelDTO) =>
            UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.SerialNumber == modelDTO.SerialNumber);
        protected override Task<int> GetCountElementAsync() => UnitOfWork.DriverMedicalCertificates.CountElementAsync();

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
