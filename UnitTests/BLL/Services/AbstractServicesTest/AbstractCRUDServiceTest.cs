using AutoFixture.Xunit2;
using AutoMapper;
using BLL;
using BLL.Infrastructure;
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

namespace UnitTests.BLL.Services.AbstractServicesTest
{
    public abstract class AbstractCRUDServiceTest<TGetDTO, TAddDTO, TUpdateDTO, TData> :
        BaseCRUDTest<TData>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        protected delegate Expression<Action<IUnitOfWork<LaborProtectionContext>>> UpdateDeleteExpression(TData data);
        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> SetupAddExpression(TData data);
        protected abstract Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupUpdateExpression(TData data);
        protected abstract Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupDeleteExpression(TData data);
        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<TData>>> SetupFindExpression();
        protected abstract Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression();
        protected abstract Expression<Func<IUnitOfWorkValidator, IValidatorDTO<TAddDTO, TUpdateDTO, TData>>> SetupValidatorExpression();
        protected abstract ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> CreateService(IUnitOfWorkService unitOfWorkService, IMapper mapper, IStringLocalizer<SharedResource> localizer, IUnitOfWorkValidator unitOfWorkValidator);

        #region ServiceAdd
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TGetDTO>> ServiceAdd_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper, Mock<IStringLocalizer<SharedResource>> localizer,
            Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO, TAddDTO dataDTO, TData dataFromDb, TGetDTO getDTO)
        {
            SetAddMockMapper(mapper, dataFromDb, getDTO);
            SetMockUnitOfWork(unitOfWork, SetupAddExpression(dataFromDb));
            unitOfWork.Setup(SetupFindExpression()).ReturnsAsync(dataFromDb).Verifiable();
            validatorDTO.Setup(x => x.ValidateAdd(It.IsAny<TAddDTO>())).ReturnsAsync(new AppActionResult { Status = (int)HttpStatusCode.OK });
            SetMocks(unitOfWorkValidator, unitOfWorkService, unitOfWork.Object, validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.AddAsync(dataDTO);
            CheckPositive(result, (int)HttpStatusCode.OK, mapper);
            unitOfWork.Verify();
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult> ServiceAdd_EntityIsExistInDb_NegativeTest(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper, Mock<IStringLocalizer<SharedResource>> localizer,
            Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO, TAddDTO dataDTO)
        {
            validatorDTO.Setup(x => x.ValidateAdd(It.IsAny<TAddDTO>())).ReturnsAsync(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { "error" } });
            SetMocks(unitOfWorkValidator, unitOfWorkService, unitOfWork.Object, validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.AddAsync(dataDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest);
            return result;
        }
        #endregion

        #region ServiceGetById
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TGetDTO>> ServiceGetById_PositiveTest(Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper,
            Mock<IStringLocalizer<SharedResource>> localizer, Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO,
            TData dataFromDb, TGetDTO getDTO)
        {
            SetGetMockMapper(mapper, getDTO);
            validatorDTO.Setup(a => a.ValidateGetData(It.IsAny<Guid>())).ReturnsAsync(new AppActionResult<TData> { Status = (int)HttpStatusCode.OK, Data = dataFromDb });
            unitOfWorkValidator.Setup(SetupValidatorExpression()).Returns(validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.GetAsync(getDTO.Id);
            CheckPositive(result, (int)HttpStatusCode.OK, mapper);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TGetDTO>> ServiceGetById_EntityIsNotFound_NegativeTest(Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper,
            Mock<IStringLocalizer<SharedResource>> localizer, Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO,
            Guid id)
        {
            validatorDTO.Setup(a => a.ValidateGetData(It.IsAny<Guid>()))
                .ReturnsAsync(new AppActionResult<TData> { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { "error" } });
            unitOfWorkValidator.Setup(SetupValidatorExpression()).Returns(validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.GetAsync(id);
            CheckNegative(result, (int)HttpStatusCode.NotFound);
            return result;
        }
        #endregion

        #region ServiceGetAsPage
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<List<TGetDTO>>> ServiceGetAsPage_PositiveTest(Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper,
            Mock<IStringLocalizer<SharedResource>> localizer, Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO,
            List<TData> datasFromDb, List<TGetDTO> getDTOs)
        {
            int startItem = 0;
            int countItem = 3;
            SetGetListMockMapper(mapper, getDTOs);
            validatorDTO.Setup(a => a.ValidateGetData(startItem, countItem))
                .ReturnsAsync(new AppActionResult<List<TData>> { Status = (int)HttpStatusCode.OK, Data = datasFromDb });
            unitOfWorkValidator.Setup(SetupValidatorExpression())
                .Returns(validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.GetPageAsync(startItem, countItem);
            CheckPositive(result, (int)HttpStatusCode.OK, mapper);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<List<TGetDTO>>> ServiceGetAsPage_EntitiesIsNotFound_NegativeTest(Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper,
            Mock<IStringLocalizer<SharedResource>> localizer, Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO)
        {
            int startItem = 0;
            int countItem = 3;
            validatorDTO.Setup(a => a.ValidateGetData(startItem, countItem))
                .ReturnsAsync(new AppActionResult<List<TData>> { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { "error" } });
            unitOfWorkValidator.Setup(SetupValidatorExpression()).Returns(validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.GetPageAsync(startItem, countItem);
            CheckNegative(result, (int)HttpStatusCode.NotFound);
            return result;
        }
        #endregion

        #region ServiceUpdate
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TGetDTO>> ServiceUpdate_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper, Mock<IStringLocalizer<SharedResource>> localizer,
            Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO, TUpdateDTO updateDTO, TData dataFromDb, TGetDTO getDTO)
        {
            SetUpdateDTOMockMapper(mapper, dataFromDb, getDTO);
            SetMockUpdateUnitOfWork(unitOfWork, SetupUpdateExpression, dataFromDb);
            validatorDTO.Setup(x => x.ValidateUpdate(updateDTO))
                .ReturnsAsync(new AppActionResult<TData> { Status = (int)HttpStatusCode.OK, Data = dataFromDb });
            SetMocks(unitOfWorkValidator, unitOfWorkService, unitOfWork.Object, validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.UpdateAsync(updateDTO);
            CheckPositive(result, (int)HttpStatusCode.OK, mapper);
            unitOfWork.Verify();
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<TGetDTO>> ServiceUpdate_EntityIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper, Mock<IStringLocalizer<SharedResource>> localizer,
            Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO, TUpdateDTO updateDTO)
        {
            validatorDTO.Setup(x => x.ValidateUpdate(updateDTO))
                .ReturnsAsync(new AppActionResult<TData> { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { "error" } });
            SetMocks(unitOfWorkValidator, unitOfWorkService, unitOfWork.Object, validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.UpdateAsync(updateDTO);
            CheckNegative(result, (int)HttpStatusCode.NotFound);
            return result;
        }
        #endregion

        #region ServiceDelete
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult> ServiceDelete_PositiveTest(Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper,
            Mock<IStringLocalizer<SharedResource>> localizer, Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO,
            TData dataFromDb)
        {
            validatorDTO.Setup(a => a.ValidateDelete(It.IsAny<Guid>())).ReturnsAsync(new AppActionResult<TData> { Status = (int)HttpStatusCode.OK, Data = dataFromDb });
            unitOfWorkValidator.Setup(SetupValidatorExpression()).Returns(validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.DeleteAsync(dataFromDb.Id);
            CheckBasePositive(result, (int)HttpStatusCode.OK);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult> ServiceDelete_EntityIsNotFound_NegativeTest(Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper,
            Mock<IStringLocalizer<SharedResource>> localizer, Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO,
            TData dataFromDb)
        {
            validatorDTO.Setup(a => a.ValidateDelete(It.IsAny<Guid>())).ReturnsAsync(new AppActionResult<TData> { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { "error" } });
            unitOfWorkValidator.Setup(SetupValidatorExpression()).Returns(validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.DeleteAsync(dataFromDb.Id);
            CheckBaseNegative(result, (int)HttpStatusCode.NotFound);
            return result;
        }
        #endregion

        #region ServiceCount
        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<int>> ServiceCount_PositiveTest(Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper,
            Mock<IStringLocalizer<SharedResource>> localizer, Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO,
            int count)
        {
            validatorDTO.Setup(a => a.ValidateCount()).ReturnsAsync(new AppActionResult<int> { Status = (int)HttpStatusCode.OK, Data = count });
            unitOfWorkValidator.Setup(SetupValidatorExpression()).Returns(validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.GetCountElementAsync();
            CheckBasePositive(result, (int)HttpStatusCode.OK);
            Assert.Equal(result.Data, count);
            return result;
        }

        [Theory, AutoMoqData]
        public virtual async Task<IAppActionResult<int>> ServiceCount_EntityIsNotFound_NegativeTest(Mock<IUnitOfWorkService> unitOfWorkService, Mock<IMapper> mapper,
            Mock<IStringLocalizer<SharedResource>> localizer, Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IValidatorDTO<TAddDTO, TUpdateDTO, TData>> validatorDTO)
        {
            validatorDTO.Setup(a => a.ValidateCount()).ReturnsAsync(new AppActionResult<int> { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { "error" } });
            unitOfWorkValidator.Setup(SetupValidatorExpression()).Returns(validatorDTO.Object);
            var service = CreateService(unitOfWorkService.Object, mapper.Object, localizer.Object, unitOfWorkValidator.Object);
            var result = await service.GetCountElementAsync();
            CheckBaseNegative(result, (int)HttpStatusCode.NotFound);
            Assert.Equal(0, result.Data);
            return result;
        }
        #endregion

        #region utility methods
        protected Mock<IUnitOfWork<LaborProtectionContext>> SetMockUnitOfWork(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> method)
        {
            unitOfWork.Setup(method).Verifiable();
            unitOfWork.Setup(x => x.SaveChangesAsync()).Verifiable();
            return unitOfWork;
        }

        protected Mock<IUnitOfWork<LaborProtectionContext>> SetMockUpdateUnitOfWork(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            UpdateDeleteExpression method, TData data)
        {
            unitOfWork.Setup(method(data)).Verifiable();
            unitOfWork.Setup(x => x.SaveChangesAsync()).Verifiable();
            return unitOfWork;
        }

        protected void CheckPositive(IAppActionResult<TGetDTO> result, int code, Mock<IMapper> mapper)
        {
            CheckBasePositive(result, code);
            Assert.NotNull(result.Data);
            mapper.Verify();
        }

        protected void CheckPositive(IAppActionResult<List<TGetDTO>> result, int code, Mock<IMapper> mapper)
        {
            CheckBasePositive(result, code);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
            mapper.Verify();
        }

        protected void CheckNegative(IAppActionResult<TGetDTO> result, int code)
        {
            CheckBaseNegative(result, code);
            Assert.Null(result.Data);
        }

        protected void CheckNegative(IAppActionResult<List<TGetDTO>> result, int code)
        {
            CheckBaseNegative(result, code);
            Assert.Null(result.Data);
        }

        protected void SetAddMockMapper(Mock<IMapper> mapper, TData data, TGetDTO getDTO)
        {
            mapper.Setup(x => x.Map<TAddDTO, TData>(It.IsAny<TAddDTO>())).Returns(data).Verifiable();
            mapper.Setup(x => x.Map<TData, TGetDTO>(It.IsAny<TData>())).Returns(getDTO).Verifiable();
        }

        protected void SetUpdateDTOMockMapper(Mock<IMapper> mapper, TData data, TGetDTO getDTO)
        {
            mapper.Setup(x => x.Map(It.IsAny<TUpdateDTO>(), It.IsAny<TData>())).Returns(data).Verifiable();
            mapper.Setup(x => x.Map<TData, TGetDTO>(It.IsAny<TData>())).Returns(getDTO).Verifiable();
        }

        protected void SetGetMockMapper(Mock<IMapper> mapper, TGetDTO getDTO)
        {
            mapper.Setup(x => x.Map<TData, TGetDTO>(It.IsAny<TData>())).Returns(getDTO).Verifiable();
        }
        protected void SetGetListMockMapper(Mock<IMapper> mapper, List<TGetDTO> getDTOs)
        {
            mapper.Setup(x => x.Map<List<TData>, List<TGetDTO>>(It.IsAny<List<TData>>())).Returns(getDTOs).Verifiable();
        }

        protected void SetMocks(Mock<IUnitOfWorkValidator> unitOfWorkValidator, Mock<IUnitOfWorkService> unitOfWorkService,
            IUnitOfWork<LaborProtectionContext> unitOfWork, IValidatorDTO<TAddDTO, TUpdateDTO, TData> validatorDTO)
        {
            unitOfWorkService.Setup(x => x.UnitOfWorkLaborProtectionContext).Returns(unitOfWork);
            unitOfWorkValidator.Setup(SetupValidatorExpression()).Returns(validatorDTO);
        }
        #endregion
    }
}
