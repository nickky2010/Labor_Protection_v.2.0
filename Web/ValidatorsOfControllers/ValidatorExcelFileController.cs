using BLL;
using BLL.Infrastructure;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Net;
using Web.Interfaces;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorExcelFileController : AbstractBaseValidatorOfControllers, IValidatorFileController
    {
        public ValidatorExcelFileController(IStringLocalizer<SharedResource> localizer) :
            base(localizer)
        { }

        public IAppActionResult ValidateFile(IFormFile file)
        {
            var result = new AppActionResult();
            if (file == null || file.Length == 0)
                result.ErrorMessages.Add(Localizer[NoData]);
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return result;
        }
    }
}
