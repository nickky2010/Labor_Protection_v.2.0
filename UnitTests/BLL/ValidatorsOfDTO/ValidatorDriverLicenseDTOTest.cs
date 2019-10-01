using BLL;
using BLL.DTO.DriverLicenses;
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
    public class ValidatorDriverLicenseDTOTest :
        AbstractCRUDValidatorDTOWithConnectedEntitiesTest<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        protected override IValidatorDTO<DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            var validator = new ValidatorDriverLicenseDTO(unitOfWork);
            validator.Localizer = localizer;
            return validator;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverLicenses.CountElementAsync();
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverLicense>>> SetupFindExpression()
        {
            return a => a.DriverLicenses.FindAsync(It.IsAny<Expression<Func<DriverLicense, bool>>>());
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<DriverLicense>>>> SetupGetPageExpression()
        {
            return a => a.DriverLicenses.GetPageAsync(It.IsAny<int>(), It.IsAny<int>());
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverLicenseAddDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.Employees.IsIdExistAsync(dataDTO.EmployeeId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
            unitOfWork.Setup(x => x.DriverCategories.IsAllIdExistAsync(dataDTO.DriverCategoriesId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverLicenseUpdateDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.Employees.IsIdExistAsync(dataDTO.EmployeeId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
            unitOfWork.Setup(x => x.DriverCategories.IsAllIdExistAsync(dataDTO.DriverCategoriesId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }
    }
}
