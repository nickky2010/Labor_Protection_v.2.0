using AutoFixture.Xunit2;
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
using System.Net;
using System.Threading.Tasks;
using UnitTests.Dependencies;
using Xunit;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public class ValidatorDriverLicenseDTOTest :
        AbstractCRUDValidatorDTOTest<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        protected override IValidatorDTO<DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            return new ValidatorDriverLicenseDTO(unitOfWork, localizer);
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

        [Theory, AutoMoqData]
        public override void ValidateAdd_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, 
            Mock<IStringLocalizer<SharedResource>> localizer, DriverLicenseAddDTO dataDTO, LocalizedString localizedString)
        {
            SetMockUnitOfWorkForConnectedEntities(unitOfWork, dataDTO, true);
            base.ValidateAdd_PositiveTest(unitOfWork, localizer, dataDTO, localizedString);
        }

        [Theory, AutoMoqData]
        public async void ValidateAdd_ConnectedEntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, 
            Mock<IStringLocalizer<SharedResource>> localizer, DriverLicenseAddDTO dataDTO, LocalizedString localizedString)
        {
            DriverLicense data = default;
            SetMockUnitOfWorkForConnectedEntities(unitOfWork, dataDTO, false);
            SetMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
        }

        [Theory, AutoMoqData]
        public override void ValidateUpdate_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, 
            Mock<IStringLocalizer<SharedResource>> localizer, DriverLicense data, DriverLicenseUpdateDTO updateDTO, LocalizedString localizedString)
        {
            SetMockUnitOfWorkForConnectedEntities(unitOfWork, updateDTO, true);
            base.ValidateUpdate_PositiveTest(unitOfWork, localizer, data, updateDTO, localizedString);
        }

        [Theory, AutoMoqData]
        public async void ValidateUpdate_ConnectedEntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, DriverLicense data, DriverLicenseUpdateDTO updateDTO, LocalizedString localizedString)
        {
            SetMockUnitOfWorkForConnectedEntities(unitOfWork, updateDTO, false);
            SetMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateUpdate(updateDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
            Assert.Null(result.Data);
        }

        private void SetMockUnitOfWorkForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverLicenseAddDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x=>x.Employees.IsIdExistAsync(dataDTO.EmployeeId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
            unitOfWork.Setup(x => x.DriverCategories.IsAllIdExistAsync(dataDTO.DriverCategoriesId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }

        private void SetMockUnitOfWorkForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverLicenseUpdateDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.Employees.IsIdExistAsync(dataDTO.EmployeeId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
            unitOfWork.Setup(x => x.DriverCategories.IsAllIdExistAsync(dataDTO.DriverCategoriesId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }
    }
}
