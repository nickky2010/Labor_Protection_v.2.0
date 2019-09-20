using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System.Net;

namespace BLL.ValidatorsOfServices.Abstract
{
    internal abstract class AbstractBaseValidator : IBaseValidator
    {
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        public IAppActionResult Result { get; set; }
        public IStringLocalizer<SharedResource> Localizer { get; set; }

        public AbstractBaseValidator(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            Result = new AppActionResult();
            UnitOfWork = unitOfWork;
            Localizer = localizer;
        }
        public void SetStatus(IAppActionResult appActionResult, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (appActionResult.ErrorMessages.Count != 0)
                appActionResult.Status = (int)statusCodeIsError;
            else
                appActionResult.Status = (int)statusCodeIsSuccess;
        }
    }
}
