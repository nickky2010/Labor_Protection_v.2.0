using AutoFixture.Xunit2;
using BLL;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using UnitTests.BLL.Dependencies;
using Xunit;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public abstract class AbstractCRUDValidatorDTOTest<TGetDTO, TAddDTO, TUpdateDTO, TData>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        private delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<TData>>> SetupExpressionMethod();
        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<TData>>> SetupExpressionForUnitOfWorkFindForAdd();
        protected abstract IValidatorDTO<TAddDTO, TUpdateDTO, TData> CreateValidator(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer);

        [Theory, AutoMoqData]
        public virtual async void ValidateAdd_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TAddDTO dataDTO, LocalizedString localizedString)
        {
            TData data = default;
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupExpressionForUnitOfWorkFindForAdd, data, false);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.ErrorMessages);
            Assert.Equal(result.Status, (int)HttpStatusCode.OK);
            unitOfWork.Verify();
        }

        [Theory, AutoMoqData]
        public virtual async void ValidateAdd_EntityIsExistInDb_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TAddDTO dataDTO, LocalizedString localizedString)
        {
            var validatorDTO = CreateValidator(unitOfWork, localizer, localizedString, SetupExpressionForUnitOfWorkFindForAdd, data, true);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.ErrorMessages);
            Assert.Equal(result.Status, (int)HttpStatusCode.BadRequest);
            localizer.Verify();
            unitOfWork.Verify();
        }

        private IValidatorDTO<TAddDTO, TUpdateDTO, TData> CreateValidator(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString, SetupExpressionMethod setupExpressionMethod,
            TData data, bool IsVerifyLocalizer)
        {
            if (IsVerifyLocalizer)
                localizer.Setup(a => a[It.IsAny<string>()]).Returns(localizedString).Verifiable();
            else
                localizer.Setup(a => a[It.IsAny<string>()]).Returns(localizedString);
            unitOfWork.Setup(setupExpressionMethod()).ReturnsAsync(data).Verifiable();
            return CreateValidator(unitOfWork.Object, localizer.Object);
        }
    }
}
