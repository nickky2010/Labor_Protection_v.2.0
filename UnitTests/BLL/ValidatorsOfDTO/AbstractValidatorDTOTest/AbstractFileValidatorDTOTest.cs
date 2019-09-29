using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.IO;
using System.Net;
using UnitTests.Dependencies;
using Xunit;

namespace UnitTests.BLL.ValidatorsOfDTO.AbstractValidatorDTOTest
{
    public abstract class AbstractFileValidatorDTOTest<FileType>
        where FileType : class
    {
        private string rootFolder => Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName).FullName;
        protected virtual string testFolder => "TestData";
        protected abstract string CorrectFile { get; }
        protected abstract string IncorrectFile { get; }
        protected abstract IValidatorOfUploadFile<FileType> CreateValidator(IStringLocalizer<SharedResource> localizer);

        [Theory, AutoMoqData]
        public virtual void ValidateFile_PositiveTest(Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString)
        {
            var file = CreateIFormFile(CorrectFile);
            SetMockLocalizer(localizer, localizedString, false);
            var validator = CreateValidator(localizer.Object);
            var result = validator.ValidateFile(file);
            CheckPositive(result, (int)HttpStatusCode.OK);
        }

        [Theory, AutoMoqData]
        public virtual void ValidateFile_FileIsIncorrectType_NegativeTest(Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString)
        {
            var file = CreateIFormFile(IncorrectFile);
            SetMockLocalizer(localizer, localizedString, true);
            var validator = CreateValidator(localizer.Object);
            var result = validator.ValidateFile(file);
            CheckNegative(result, (int)HttpStatusCode.BadRequest, localizer);
        }

        private IFormFile CreateIFormFile(string fileName)
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

        private void SetMockLocalizer(Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString, bool IsVerifyLocalizer)
        {
            var loc = localizer.Setup(a => a[It.IsAny<string>()]).Returns(localizedString);
            if (IsVerifyLocalizer)
                loc.Verifiable();
        }
        private void CheckPositive(IAppActionResult<FileType> result, int code)
        {
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.ErrorMessages);
            Assert.Equal(result.Status, code);
            Assert.NotNull(result.Data);
        }

        private void CheckNegative(IAppActionResult<FileType> result, int code, Mock<IStringLocalizer<SharedResource>> localizer)
        {
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.ErrorMessages);
            Assert.Equal(result.Status, code);
            localizer.Verify();
        }
    }
}
