using BLL.DTO.DriverMedicalCertificates;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverMedicalCertificateController: 
        AbstractValidatorOfControllers<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>
    {
        public ValidatorDriverMedicalCertificateController(IStringLocalizer<SharedResource> localizer):base(localizer) { }
    }
}
