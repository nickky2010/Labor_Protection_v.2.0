using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Net;
using Web.Interfaces;

namespace Web.ValidatorsOfControllers.Abstract
{
    internal abstract class AbstractFileValidatorOfControllers : AbstractBaseValidatorOfControllers, IValidatorFileController
    {
        public AbstractFileValidatorOfControllers(IStringLocalizer<SharedResource> localizer) 
            : base(localizer) { }

        public virtual IAppActionResult ValidateFile(IFormFile file)
        {
            if (file==null || file.Length == 0)
                Result.ErrorMessages.Add(Localizer[NoData]);
            SetStatus(Result, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return Result;
        }
    }
}
