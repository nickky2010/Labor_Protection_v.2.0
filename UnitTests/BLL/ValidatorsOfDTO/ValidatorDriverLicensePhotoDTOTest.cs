using BLL;
using BLL.DTO.DriverLicensePhotos;
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
    public class ValidatorDriverLicensePhotoDTOTest :
        AbstractCRUDValidatorDTOWithConnectedEntitiesAndFileTest<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        protected override string CorrectFile => "testPhoto_100x100.jpeg";
        protected override string IncorrectFile => "db.xlsx";

        protected override IValidatorDTO<DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            var validator = new ValidatorDriverLicensePhotoDTO(unitOfWork);
            validator.Localizer = localizer;
            return validator;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverLicensePhotos.CountElementAsync();
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverLicensePhoto>>> SetupFindExpression()
        {
            return a => a.DriverLicensePhotos.FindAsync(It.IsAny<Expression<Func<DriverLicensePhoto, bool>>>());
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<DriverLicensePhoto>>>> SetupGetPageExpression()
        {
            return a => a.DriverLicensePhotos.GetPageAsync(It.IsAny<int>(), It.IsAny<int>());
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverLicensePhotoAddDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.DriverLicenses.IsIdExistAsync(dataDTO.DriverLicenseId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverLicensePhotoUpdateDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.DriverLicenses.IsIdExistAsync(dataDTO.DriverLicenseId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }
    }
}
