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
        private delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<TData>>> SetupDataExpressionMethod();
        private delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<TData>>>> SetupListDataExpressionMethod();
        private delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpressionMethod();

        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<TData>>> SetupFindExpression();
        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<TData>>>> SetupGetPageExpression();
        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression();

        protected abstract IValidatorDTO<TAddDTO, TUpdateDTO, TData> CreateValidator(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer);

        #region ValidateAdd
        [Theory, AutoMoqData]
        public virtual async void ValidateAdd_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TAddDTO dataDTO, LocalizedString localizedString)
        {
            TData data = default;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupFindExpression, data, false);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
        }

        [Theory, AutoMoqData]
        public virtual async void ValidateAdd_EntityIsExistInDb_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TAddDTO dataDTO, LocalizedString localizedString)
        {
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupFindExpression, data, true);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
        }
        #endregion

        #region ValidateGetById
        [Theory, AutoMoqData]
        public virtual async void ValidateGetById_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, Guid id, LocalizedString localizedString)
        {
            data.Id = id;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupFindExpression, data, false);
            var result = await validatorDTO.ValidateGetData(id);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            Assert.Equal(result.Data.Id, id);
        }

        [Theory, AutoMoqData]
        public virtual async void ValidateGetById_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, Guid id, LocalizedString localizedString)
        {
            TData data = default;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupFindExpression, data, true);
            var result = await validatorDTO.ValidateGetData(id);
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
        }
        #endregion

        #region ValidateGetAsPage
        [Theory, AutoMoqData]
        public virtual async void ValidateGetAsPage_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, List<TData> datas, LocalizedString localizedString)
        {
            int startItem = 0;
            int countItem = 3;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupGetPageExpression, datas, false);
            var result = await validatorDTO.ValidateGetData(startItem, countItem);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            Assert.NotEmpty(result.Data);
        }

        [Theory, AutoMoqData]
        public virtual async void ValidateGetAsPage_EntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString)
        {
            int startItem = 0;
            int countItem = 3;
            List<TData> datas = default;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupGetPageExpression, datas, false);
            var result = await validatorDTO.ValidateGetData(startItem, countItem);
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
            Assert.Null(result.Data);
        }
        #endregion

        #region ValidateUpdate
        [Theory, AutoMoqData]
        public virtual async void ValidateUpdate_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            data.Id = updateDTO.Id;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupFindExpression, data, false);
            var result = await validatorDTO.ValidateUpdate(updateDTO);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            Assert.Equal(result.Data.Id, data.Id);
        }

        [Theory, AutoMoqData]
        public virtual async void ValidateUpdate_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            TData data = default;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupFindExpression, data, true);
            var result = await validatorDTO.ValidateUpdate(updateDTO);
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
            Assert.Null(result.Data);
        }
        #endregion

        #region ValidateDelete
        [Theory, AutoMoqData]
        public virtual async void ValidateDelete_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, Guid id, LocalizedString localizedString)
        {
            data.Id = id;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupFindExpression, data, false);
            var result = await validatorDTO.ValidateDelete(id);
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
        }

        [Theory, AutoMoqData]
        public virtual async void ValidateDelete_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, Guid id, LocalizedString localizedString)
        {
            TData data = default;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupFindExpression, data, true);
            var result = await validatorDTO.ValidateDelete(id);
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
        }
        #endregion

        #region ValidateCount
        [Theory, AutoMoqData]
        public virtual async void ValidateCount_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString)
        {
            int count = 3;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupCountExpression, count, false);
            var result = await validatorDTO.ValidateCount();
            CheckPositive(result, (int)HttpStatusCode.OK, unitOfWork);
            Assert.Equal(result.Data, count);
        }

        [Theory, AutoMoqData]
        public virtual async void ValidateCount_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString)
        {
            int count = 0;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupCountExpression, count, true);
            var result = await validatorDTO.ValidateCount();
            CheckNegative(result, (int)HttpStatusCode.NotFound, unitOfWork, localizer);
            Assert.Equal(result.Data, count);
        }
        #endregion

        #region utility methods
        private IValidatorDTO<TAddDTO, TUpdateDTO, TData> CreateValidator(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString, SetupDataExpressionMethod method,
            TData data, bool IsVerifyLocalizer)
        {
            SetLocalizer(localizer, localizedString, IsVerifyLocalizer);
            unitOfWork.Setup(method()).ReturnsAsync(data).Verifiable();
            return CreateValidator(unitOfWork.Object, localizer.Object);
        }

        private IValidatorDTO<TAddDTO, TUpdateDTO, TData> CreateValidator(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString, SetupListDataExpressionMethod method,
            List<TData> datas, bool IsVerifyLocalizer)
        {
            SetLocalizer(localizer, localizedString, IsVerifyLocalizer);
            unitOfWork.Setup(method()).ReturnsAsync(datas).Verifiable();
            return CreateValidator(unitOfWork.Object, localizer.Object);
        }

        private IValidatorDTO<TAddDTO, TUpdateDTO, TData> CreateValidator(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString, SetupCountExpressionMethod method,
            int count, bool IsVerifyLocalizer)
        {
            SetLocalizer(localizer, localizedString, IsVerifyLocalizer);
            unitOfWork.Setup(method()).ReturnsAsync(count).Verifiable();
            return CreateValidator(unitOfWork.Object, localizer.Object);
        }

        private void SetLocalizer(Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString, bool IsVerifyLocalizer)
        {
            var loc = localizer.Setup(a => a[It.IsAny<string>()]).Returns(localizedString);
            if (IsVerifyLocalizer)
                loc.Verifiable();
        }

        private void CheckPositive(IAppActionResult result, int code, Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork)
        {
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.ErrorMessages);
            Assert.Equal(result.Status, code);
            unitOfWork.Verify();
        }

        private void CheckNegative(IAppActionResult result, int code, Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
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
