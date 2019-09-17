using BLL.DTO.DriverLicenses;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverLicenseController: 
        AbstractValidatorOfControllers<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>
    {
        public ValidatorDriverLicenseController(IStringLocalizer<SharedResource> localizer):base(localizer) { }
    }
}
