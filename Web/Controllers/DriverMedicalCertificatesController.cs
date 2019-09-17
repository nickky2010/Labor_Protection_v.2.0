using AutoMapper;
using BLL;
using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverMedicalCertificatesController : 
        AbstractController<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>,
        IControllerServices<DriverMedicalCertificatesController, IDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>>
    {
        public DriverMedicalCertificatesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverMedicalCertificateController(Localizer);
        }
    }
}
