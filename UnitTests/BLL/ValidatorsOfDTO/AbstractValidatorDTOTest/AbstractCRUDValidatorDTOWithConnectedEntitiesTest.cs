using AutoFixture.Xunit2;
using BLL;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;
using System.Threading.Tasks;
using UnitTests.Dependencies;
using Xunit;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public abstract class AbstractCRUDValidatorDTOWithConnectedEntitiesTest<TGetDTO, TAddDTO, TUpdateDTO, TData> :
        AbstractCRUDValidatorDTOTest<TGetDTO, TAddDTO, TUpdateDTO, TData>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        protected abstract void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, TAddDTO addDataDTO, bool IsEntitiesExist);
        protected abstract void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, TUpdateDTO updateDataDTO, bool IsEntitiesExist);

        [Theory, AutoMoqData]
        public override async Task<IAppActionResult> ValidateAdd_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TAddDTO dataDTO, LocalizedString localizedString)
        {
            SetMocksForConnectedEntities(unitOfWork, dataDTO, true);
            return await base.ValidateAdd_PositiveTest(unitOfWork, localizer, dataDTO, localizedString);
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult> ValidateAdd_ConnectedEntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TAddDTO dataDTO, LocalizedString localizedString)
        {
            TData data = default;
            SetMocksForConnectedEntities(unitOfWork, dataDTO, false);
            SetBaseMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
            return result;
        }

        [Theory, AutoMoqData]
        public override async Task<IAppActionResult<TData>> ValidateUpdate_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            SetMocksForConnectedEntities(unitOfWork, updateDTO, true);
            return await base.ValidateUpdate_PositiveTest(unitOfWork, localizer, data, updateDTO, localizedString);
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TData>> ValidateUpdate_ConnectedEntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            SetMocksForConnectedEntities(unitOfWork, updateDTO, false);
            SetBaseMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateUpdate(updateDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
            Assert.Null(result.Data);
            return result;
        }
    }
}
