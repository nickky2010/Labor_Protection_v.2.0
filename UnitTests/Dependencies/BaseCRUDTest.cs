using BLL;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Dependencies
{
    public class BaseCRUDTest<TData>
        where TData : IData
    {
        protected delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<TData>>> SetupDataExpressionMethod();
        protected delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<TData>>>> SetupListDataExpressionMethod();
        protected delegate Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpressionMethod();

        protected Mock<IUnitOfWork<LaborProtectionContext>> SetMockUnitOfWork(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            SetupDataExpressionMethod method, TData data)
        {
            unitOfWork.Setup(method()).ReturnsAsync(data).Verifiable();
            return unitOfWork;
        }

        protected Mock<IUnitOfWork<LaborProtectionContext>> SetMockUnitOfWork(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            SetupListDataExpressionMethod method, List<TData> datas)
        {
            unitOfWork.Setup(method()).ReturnsAsync(datas).Verifiable();
            return unitOfWork;
        }

        protected Mock<IUnitOfWork<LaborProtectionContext>> SetMockUnitOfWork(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork,
            SetupCountExpressionMethod method, int count)
        {
            unitOfWork.Setup(method()).ReturnsAsync(count).Verifiable();
            return unitOfWork;
        }

        protected void SetMockLocalizer(Mock<IStringLocalizer<SharedResource>> localizer, LocalizedString localizedString, bool IsVerifyLocalizer)
        {
            var loc = localizer.Setup(a => a[It.IsAny<string>()]).Returns(localizedString);
            if (IsVerifyLocalizer)
                loc.Verifiable();
        }

        protected void SetMocks(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, Mock<IStringLocalizer<SharedResource>> localizer,
            LocalizedString localizedString, bool IsVerifyLocalizer, SetupListDataExpressionMethod method, List<TData> datas)
        {
            SetMockLocalizer(localizer, localizedString, IsVerifyLocalizer);
            SetMockUnitOfWork(unitOfWork, method, datas);
        }

        protected void SetMocks(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, Mock<IStringLocalizer<SharedResource>> localizer,
            LocalizedString localizedString, bool IsVerifyLocalizer, SetupCountExpressionMethod method, int count)
        {
            SetMockLocalizer(localizer, localizedString, IsVerifyLocalizer);
            SetMockUnitOfWork(unitOfWork, method, count);
        }

        protected void CheckBasePositive(IAppActionResult result, int code)
        {
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.ErrorMessages);
            Assert.Equal(result.Status, code);
        }

        protected void CheckBaseNegative(IAppActionResult result, int code)
        {
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.ErrorMessages);
            Assert.Equal(result.Status, code);
        }
    }
}
