using BLL.DTO.DriverLicenses;
using BLL;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorDriverLicenseController: 
        AbstractValidatorOfControllers<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>
    {
        public ValidatorDriverLicenseController(IStringLocalizer<SharedResource> localizer):base(localizer) { }
    }
}
