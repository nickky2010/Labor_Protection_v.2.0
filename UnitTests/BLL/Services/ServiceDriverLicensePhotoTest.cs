using AutoMapper;
using BLL;
using BLL.DTO.DriverLicensePhotos;
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
    public class ServiceDriverLicensePhotoTest :
        AbstractCRUDServiceTest<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        protected override ICRUDDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO> CreateService
            (IUnitOfWorkService unitOfWorkService, IMapper mapper, IStringLocalizer<SharedResource> localizer, IUnitOfWorkValidator unitOfWorkValidator)
        {
            var service = new DriverLicensePhotoService(unitOfWorkService, mapper, unitOfWorkValidator);
            service.Localizer = localizer;
            return service;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> SetupAddExpression(DriverLicensePhoto data)
        {
            return a => a.DriverLicensePhotos.AddAsync(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverLicensePhotos.CountElementAsync();
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupDeleteExpression(DriverLicensePhoto data)
        {
            return a => a.DriverLicensePhotos.Delete(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverLicensePhoto>>> SetupFindExpression()
        {
            return a => a.DriverLicensePhotos.FindAsync(It.IsAny<Expression<Func<DriverLicensePhoto, bool>>>());
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupUpdateExpression(DriverLicensePhoto data)
        {
            return a => a.DriverLicensePhotos.Update(data);
        }

        protected override Expression<Func<IUnitOfWorkValidator, IValidatorDTO<DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>>> SetupValidatorExpression()
        {
            return a => a.ValidatorDriverLicensePhotoDTO;
        }
    }
}
