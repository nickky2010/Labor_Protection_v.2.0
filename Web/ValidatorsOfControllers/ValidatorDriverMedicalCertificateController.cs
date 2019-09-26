using BLL;
using BLL.DTO.DriverMedicalCertificates;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorDriverMedicalCertificateController :
        AbstractValidatorOfControllers<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>
    {
        public ValidatorDriverMedicalCertificateController(IStringLocalizer<SharedResource> localizer) : base(localizer) { }
    }
}
