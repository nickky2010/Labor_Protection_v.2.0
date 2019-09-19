using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using System.Net;

namespace BLL.ValidatorsOfServices.Abstract
{
    internal abstract class AbstractBaseValidatorOfServices : IBaseValidatorService
    {
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        public IAppActionResult Result { get; set; }

        public AbstractBaseValidatorOfServices(IUnitOfWork<LaborProtectionContext> unitOfWork)
        {
            Result = new AppActionResult();
            UnitOfWork = unitOfWork;
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
