using AutoFixture.Xunit2;
using BLL;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnitTests.Dependencies;
using Xunit;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public abstract class AbstractCRUDValidatorDTOWithConnectedEntitiesAndFileTest<TGetDTO, TAddDTO, TUpdateDTO, TData> :
        AbstractCRUDValidatorDTOWithConnectedEntitiesTest<TGetDTO, TAddDTO, TUpdateDTO, TData>
        where TGetDTO : IGetDTO, IGetPhotoDTO
        where TAddDTO : IAddDTO, IAddUpdatePhotoDTO
        where TUpdateDTO : IUpdateDTO, IAddUpdatePhotoDTO
        where TData : IData, IPhotoData
    {
        private string rootFolder => Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName).FullName;
        protected virtual string testFolder => "TestData";
        protected abstract string CorrectFile { get; }
        protected abstract string IncorrectFile { get; }

        [Theory, AutoMoqData]
        public override async Task<IAppActionResult> ValidateAdd_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TAddDTO dataDTO, LocalizedString localizedString)
        {
            dataDTO.Picture = CreateIFormFile(CorrectFile);
            return await base.ValidateAdd_PositiveTest(unitOfWork, localizer, dataDTO, localizedString);
        }

        public override async Task<IAppActionResult> ValidateAdd_ConnectedEntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, Mock<IStringLocalizer<SharedResource>> localizer, TAddDTO dataDTO, LocalizedString localizedString)
        {
            dataDTO.Picture = CreateIFormFile(CorrectFile);
            var result = await base.ValidateAdd_ConnectedEntitiesIsNotFound_NegativeTest(unitOfWork, localizer, dataDTO, localizedString);
            Assert.Single(result.ErrorMessages);
            return result;
        }

        [Theory, AutoMoqData]
        public async void ValidateAdd_FileIsIncorrect_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TAddDTO dataDTO, LocalizedString localizedString)
        {
            TData data = default;
            dataDTO.Picture = CreateIFormFile(IncorrectFile);
            SetMocksForConnectedEntities(unitOfWork, dataDTO, true);
            SetBaseMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateAdd(dataDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
            Assert.Single(result.ErrorMessages);
        }

        [Theory, AutoMoqData]
        public override async Task<IAppActionResult<TData>> ValidateUpdate_PositiveTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            updateDTO.Picture = CreateIFormFile(CorrectFile);
            return await base.ValidateUpdate_PositiveTest(unitOfWork, localizer, data, updateDTO, localizedString);
        }

        [Theory, AutoMoqData]
        public override async Task<IAppActionResult<TData>> ValidateUpdate_ConnectedEntitiesIsNotFound_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            updateDTO.Picture = CreateIFormFile(CorrectFile);
            var result = await base.ValidateUpdate_ConnectedEntitiesIsNotFound_NegativeTest(unitOfWork, localizer, data, updateDTO, localizedString);
            Assert.Single(result.ErrorMessages);
            return result;
        }

        [Theory, AutoMoqData]
        public async void ValidateUpdate_FileIsIncorrect_NegativeTest([Frozen] Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            Mock<IStringLocalizer<SharedResource>> localizer, TData data, TUpdateDTO updateDTO, LocalizedString localizedString)
        {
            updateDTO.Picture = CreateIFormFile(IncorrectFile);
            SetMocksForConnectedEntities(unitOfWork, updateDTO, true);
            SetBaseMocks(unitOfWork, localizer, localizedString, true, SetupFindExpression, data);
            var validatorDTO = CreateValidator(unitOfWork.Object, localizer.Object);
            var result = await validatorDTO.ValidateUpdate(updateDTO);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, unitOfWork, localizer);
            Assert.Null(result.Data);
            Assert.Single(result.ErrorMessages);
        }

        protected IFormFile CreateIFormFile(string fileName)
        {
            var path = $"{rootFolder}\\{testFolder}\\{fileName}";
            var fileMock = new Mock<IFormFile>();
            var physicalFile = new FileInfo(path);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fileMock.Setup(_ => _.FileName).Returns(physicalFile.Name);
            fileMock.Setup(_ => _.Length).Returns(fs.Length);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(fs);
            return fileMock.Object;
        }
    }
}
