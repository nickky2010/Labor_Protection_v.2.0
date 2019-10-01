using AutoMapper;
using BLL;
using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using BLL.Services;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTests.BLL.Services.AbstractServicesTest;

namespace UnitTests.BLL.Services
{
    public class ServiceDriverMedicalCertificateTest :
        AbstractCRUDServiceTest<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>
    {
        protected override ICRUDDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO> CreateService
            (IUnitOfWorkService unitOfWorkService, IMapper mapper, IStringLocalizer<SharedResource> localizer, IUnitOfWorkValidator unitOfWorkValidator)
        {
            var service = new DriverMedicalCertificateService(unitOfWorkService, mapper, unitOfWorkValidator);
            service.Localizer = localizer;
            return service;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> SetupAddExpression(DriverMedicalCertificate data)
        {
            return a => a.DriverMedicalCertificates.AddAsync(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverMedicalCertificates.CountElementAsync();
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupDeleteExpression(DriverMedicalCertificate data)
        {
            return a => a.DriverMedicalCertificates.Delete(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverMedicalCertificate>>> SetupFindExpression()
        {
            return a => a.DriverMedicalCertificates.FindAsync(It.IsAny<Expression<Func<DriverMedicalCertificate, bool>>>());
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupUpdateExpression(DriverMedicalCertificate data)
        {
            return a => a.DriverMedicalCertificates.Update(data);
        }

        protected override Expression<Func<IUnitOfWorkValidator, IValidatorDTO<DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>>> SetupValidatorExpression()
        {
            return a => a.ValidatorDriverMedicalCertificateDTO;
        }
    }
}
