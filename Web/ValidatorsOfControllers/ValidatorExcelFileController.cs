using BLL;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorExcelFileController :  AbstractFileValidatorOfControllers
    {
        public ValidatorExcelFileController(IStringLocalizer<SharedResource> localizer):
            base(localizer) { }
    }
}
