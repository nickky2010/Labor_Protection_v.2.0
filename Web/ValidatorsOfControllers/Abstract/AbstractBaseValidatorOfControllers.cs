using BLL.Infrastructure;
using BLL.Interfaces;
using BLL;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Web.ValidatorsOfControllers.Abstract
{
    internal abstract class AbstractBaseValidatorOfControllers
    {
        public IStringLocalizer<SharedResource> Localizer { get; set; }
        public IAppActionResult Result { get; set; }
        public virtual string NoData { get => "NoData"; }

        public AbstractBaseValidatorOfControllers(IStringLocalizer<SharedResource> localizer)
        {
            Result = new AppActionResult();
            Localizer = localizer;
        }

        protected void SetStatus(IAppActionResult appActionResult, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (appActionResult.ErrorMessages.Count != 0)
                appActionResult.Status = (int)statusCodeIsError;
            else
                appActionResult.Status = (int)statusCodeIsSuccess;
        }
    }
}
