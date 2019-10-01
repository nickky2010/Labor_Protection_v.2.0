using BLL;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public class ValidatorDriverMedicalCertificatePhotoDTOTest :
        AbstractCRUDValidatorDTOWithConnectedEntitiesAndFileTest<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>
    {
        protected override string CorrectFile => "testPhoto_100x100.jpeg";
        protected override string IncorrectFile => "db.xlsx";

        protected override IValidatorDTO<DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            var validator = new ValidatorDriverMedicalCertificatePhotoDTO(unitOfWork);
            validator.Localizer = localizer;
            return validator;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverMedicalCertificatePhotos.CountElementAsync();
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverMedicalCertificatePhoto>>> SetupFindExpression()
        {
            return a => a.DriverMedicalCertificatePhotos.FindAsync(It.IsAny<Expression<Func<DriverMedicalCertificatePhoto, bool>>>());
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<DriverMedicalCertificatePhoto>>>> SetupGetPageExpression()
        {
            return a => a.DriverMedicalCertificatePhotos.GetPageAsync(It.IsAny<int>(), It.IsAny<int>());
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverMedicalCertificatePhotoAddDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.DriverMedicalCertificates.IsIdExistAsync(dataDTO.DriverMedicalCertificateId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverMedicalCertificatePhotoUpdateDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.DriverMedicalCertificates.IsIdExistAsync(dataDTO.DriverMedicalCertificateId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }
    }
}
