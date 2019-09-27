using AutoFixture.Xunit2;
using BLL;
using BLL.DTO.DriverMedicalCertificates;
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
    public class ValidatorDriverMedicalCertificateDTOTest :
        AbstractCRUDValidatorDTOTest<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>
    {
        protected override IValidatorDTO<DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            return new ValidatorDriverMedicalCertificateDTO(unitOfWork, localizer);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverMedicalCertificates.CountElementAsync();
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverMedicalCertificate>>> SetupFindExpression()
        {
            return a => a.DriverMedicalCertificates.FindAsync(It.IsAny<Expression<Func<DriverMedicalCertificate, bool>>>());
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<DriverMedicalCertificate>>>> SetupGetPageExpression()
        {
            return a => a.DriverMedicalCertificates.GetPageAsync(It.IsAny<int>(), It.IsAny<int>());
        }

        [Theory, AutoMoqData]
        public override void ValidateAdd_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, 
            Mock<IStringLocalizer<SharedResource>> localizer, DriverMedicalCertificateAddDTO dataDTO, LocalizedString localizedString)
        {
            SetMockUnitOfWorkForConnectedEntities(unitOfWork, dataDTO, true);
            base.ValidateAdd_PositiveTest(unitOfWork, localizer, dataDTO, localizedString);
        }

        [Theory, AutoMoqData]
        public async void ValidateAdd_ConnectedEntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, 
            Mock<IStringLocalizer<SharedResource>> localizer, DriverMedicalCertificateAddDTO dataDTO, LocalizedString localizedString)
        {
            DriverMedicalCertificate data = default;
            SetMockUnitOfWorkForConnectedEntities(unitOfWork, dataDTO, false);
            SetMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
        }

        [Theory, AutoMoqData]
        public override void ValidateUpdate_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, 
            Mock<IStringLocalizer<SharedResource>> localizer, DriverMedicalCertificate data, DriverMedicalCertificateUpdateDTO updateDTO, LocalizedString localizedString)
        {
            SetMockUnitOfWorkForConnectedEntities(unitOfWork, updateDTO, true);
            base.ValidateUpdate_PositiveTest(unitOfWork, localizer, data, updateDTO, localizedString);
        }

        [Theory, AutoMoqData]
        public async void ValidateUpdate_ConnectedEntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, DriverMedicalCertificate data, DriverMedicalCertificateUpdateDTO updateDTO, LocalizedString localizedString)
        {
            SetMockUnitOfWorkForConnectedEntities(unitOfWork, updateDTO, false);
            SetMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateUpdate(updateDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
            Assert.Null(result.Data);
        }

        private void SetMockUnitOfWorkForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverMedicalCertificateAddDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x=>x.Employees.IsIdExistAsync(dataDTO.EmployeeId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
            unitOfWork.Setup(x => x.DriverCategories.IsAllIdExistAsync(dataDTO.DriverCategoriesId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }

        private void SetMockUnitOfWorkForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverMedicalCertificateUpdateDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.Employees.IsIdExistAsync(dataDTO.EmployeeId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
            unitOfWork.Setup(x => x.DriverCategories.IsAllIdExistAsync(dataDTO.DriverCategoriesId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }
    }
}
