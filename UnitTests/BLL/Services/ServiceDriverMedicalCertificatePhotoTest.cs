using AutoMapper;
using BLL;
using BLL.DTO.DriverMedicalCertificatePhotos;
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
    public class ServiceDriverMedicalCertificatePhotoTest :
        AbstractCRUDServiceTest<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>
    {
        protected override ICRUDDataBaseService<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO> CreateService
            (IUnitOfWorkService unitOfWorkService, IMapper mapper, IStringLocalizer<SharedResource> localizer, IUnitOfWorkValidator unitOfWorkValidator)
        {
            var service = new DriverMedicalCertificatePhotoService(unitOfWorkService, mapper, unitOfWorkValidator);
            service.Localizer = localizer;
            return service;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> SetupAddExpression(DriverMedicalCertificatePhoto data)
        {
            return a => a.DriverMedicalCertificatePhotos.AddAsync(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverMedicalCertificatePhotos.CountElementAsync();
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupDeleteExpression(DriverMedicalCertificatePhoto data)
        {
            return a => a.DriverMedicalCertificatePhotos.Delete(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverMedicalCertificatePhoto>>> SetupFindExpression()
        {
            return a => a.DriverMedicalCertificatePhotos.FindAsync(It.IsAny<Expression<Func<DriverMedicalCertificatePhoto, bool>>>());
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupUpdateExpression(DriverMedicalCertificatePhoto data)
        {
            return a => a.DriverMedicalCertificatePhotos.Update(data);
        }

        protected override Expression<Func<IUnitOfWorkValidator, IValidatorDTO<DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>>> SetupValidatorExpression()
        {
            return a => a.ValidatorDriverMedicalCertificatePhotoDTO;
        }
    }
}
