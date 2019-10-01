using AutoMapper;
using BLL;
using BLL.DTO.DriverLicenses;
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
    public class ServiceDriverLicenseTest :
        AbstractCRUDServiceTest<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        protected override ICRUDDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO> CreateService
            (IUnitOfWorkService unitOfWorkService, IMapper mapper, IStringLocalizer<SharedResource> localizer, IUnitOfWorkValidator unitOfWorkValidator)
        {
            var service = new DriverLicenseService(unitOfWorkService, mapper, unitOfWorkValidator);
            service.Localizer = localizer;
            return service;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> SetupAddExpression(DriverLicense data)
        {
            return a => a.DriverLicenses.AddAsync(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverLicenses.CountElementAsync();
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupDeleteExpression(DriverLicense data)
        {
            return a => a.DriverLicenses.Delete(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverLicense>>> SetupFindExpression()
        {
            return a => a.DriverLicenses.FindAsync(It.IsAny<Expression<Func<DriverLicense, bool>>>());
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupUpdateExpression(DriverLicense data)
        {
            return a => a.DriverLicenses.Update(data);
        }

        protected override Expression<Func<IUnitOfWorkValidator, IValidatorDTO<DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>>> SetupValidatorExpression()
        {
            return a => a.ValidatorDriverLicenseDTO;
        }
    }
}
