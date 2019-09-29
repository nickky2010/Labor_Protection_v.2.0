using AutoFixture.Xunit2;
using BLL;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
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
    public abstract class AbstractCRUDValidatorDTOTest<TGetDTO, TAddDTO, TUpdateDTO, TData>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        protected delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<TData>>> SetupDataExpressionMethod();
        protected delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<TData>>>> SetupListDataExpressionMethod();
        protected delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpressionMethod();

        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<TData>>> SetupFindExpression();
        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<TData>>>> SetupGetPageExpression();
        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression();
        protected abstract IValidatorDTO<TAddDTO, TUpdateDTO, TData> CreateValidator(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer);

        #region ValidateAdd
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult> ValidateAdd_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TAddDTO dataDTO, LocalizedString localizedString)
        {
            TData data = default;
            SetBaseMocks(unitOfWork, localizer, localizedString, false, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult> ValidateAdd_EntityIsExistInDb_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TAddDTO dataDTO, LocalizedString localizedString)
        {
            SetBaseMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
            return result;
        }
        #endregion

        #region ValidateGetById
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TData>> ValidateGetById_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, Guid id, LocalizedString localizedString)
        {
            data.Id = id;
            SetBaseMocks(unitOfWork, localizer, localizedString, false, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateGetData(id);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            Assert.Equal(result.Data.Id, id);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TData>> ValidateGetById_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, Guid id, LocalizedString localizedString)
        {
            TData data = default;
            SetBaseMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateGetData(id);
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
            return result;
        }
        #endregion

        #region ValidateGetAsPage
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<List<TData>>> ValidateGetAsPage_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, List<TData> datas, LocalizedString localizedString)
        {
            int startItem = 0;
            int countItem = 3;
            SetMocks(unitOfWork, localizer, localizedString, false, SetupGetPageExpression, datas);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateGetData(startItem, countItem);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            Assert.NotEmpty(result.Data);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<List<TData>>> ValidateGetAsPage_EntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString)
        {
            int startItem = 0;
            int countItem = 3;
            List<TData> datas = default;
            SetMocks(unitOfWork, localizer, localizedString, true, SetupGetPageExpression, datas);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateGetData(startItem, countItem);
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
            Assert.Null(result.Data);
            return result;
        }
        #endregion

        #region ValidateUpdate
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TData>> ValidateUpdate_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            data.Id = updateDTO.Id;
            SetBaseMocks(unitOfWork, localizer, localizedString, false, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateUpdate(updateDTO);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            Assert.Equal(result.Data.Id, data.Id);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TData>> ValidateUpdate_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            TData data = default;
            SetBaseMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateUpdate(updateDTO);
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
            Assert.Null(result.Data);
            return result;
        }
        #endregion

        #region ValidateDelete
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TData>> ValidateDelete_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, Guid id, LocalizedString localizedString)
        {
            data.Id = id;
            SetBaseMocks(unitOfWork, localizer, localizedString, false, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateDelete(id);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TData>> ValidateDelete_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, Guid id, LocalizedString localizedString)
        {
            TData data = default;
            SetBaseMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateDelete(id);
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
            return result;
        }
        #endregion

        #region ValidateCount
        [Theory, AutoMoqData]
        public virtual async void ValidateCount_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString)
        {
            int count = 3;
            SetMocks(unitOfWork, localizer, localizedString, false, SetupCountExpression, count);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateCount();
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            Assert.Equal(result.Data, count);
        }

        [Theory, AutoMoqData]
        public virtual async void ValidateCount_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString)
        {
            int count = 0;
            SetMocks(unitOfWork, localizer, localizedString, true, SetupCountExpression, count);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateCount();
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
            Assert.Equal(result.Data, count);
        }
        #endregion

        #region utility methods
        private Mock<IUnitOfWork<LaborProtectionContext>> SetMockUnitOfWork(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            SetupDataExpressionMethod method, TData data)
        {
            unitOfWork.Setup(method()).ReturnsAsync(data).Verifiable();
            return unitOfWork;
        }

        private Mock<IUnitOfWork<LaborProtectionContext>> SetMockUnitOfWork(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            SetupListDataExpressionMethod method, List<TData> datas)
        {
            unitOfWork.Setup(method()).ReturnsAsync(datas).Verifiable();
            return unitOfWork;
        }

        private Mock<IUnitOfWork<LaborProtectionContext>> SetMockUnitOfWork(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            SetupCountExpressionMethod method, int count)
        {
            unitOfWork.Setup(method()).ReturnsAsync(count).Verifiable();
            return unitOfWork;
        }

        private void SetMockLocalizer(Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString, bool IsVerifyLocalizer)
        {
            var loc = localizer.Setup(a => a[It.IsAny<string>()]).Returns(localizedString);
            if (IsVerifyLocalizer)
                loc.Verifiable();
        }

        protected void SetBaseMocks(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, Mock<IStringLocalizer<SharedResource>> localizer,
            LocalizedString localizedString, bool IsVerifyLocalizer, SetupDataExpressionMethod method, TData data)
        {
            SetMockLocalizer(localizer, localizedString, IsVerifyLocalizer);
            SetMockUnitOfWork(unitOfWork, method, data);
        }

        private void SetMocks(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, Mock<IStringLocalizer<SharedResource>> localizer,
            LocalizedString localizedString, bool IsVerifyLocalizer, SetupListDataExpressionMethod method, List<TData> datas)
        {
            SetMockLocalizer(localizer, localizedString, IsVerifyLocalizer);
            SetMockUnitOfWork(unitOfWork, method, datas);
        }

        private void SetMocks(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, Mock<IStringLocalizer<SharedResource>> localizer,
            LocalizedString localizedString, bool IsVerifyLocalizer, SetupCountExpressionMethod method, int count)
        {
            SetMockLocalizer(localizer, localizedString, IsVerifyLocalizer);
            SetMockUnitOfWork(unitOfWork, method, count);
        }

        private void CheckPositive(IAppActionResult result, int code, Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork)
        {
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.ErrorMessages);
            Assert.Equal(result.Status, code);
            unitOfWork.Verify();
        }

        protected void CheckNegative(IAppActionResult result, int code, Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer)
        {
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.ErrorMessages);
            Assert.Equal(result.Status, code);
            localizer.Verify();
            unitOfWork.Verify();
        }
        #endregion
    }
}
