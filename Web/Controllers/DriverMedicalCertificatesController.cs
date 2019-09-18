using AutoMapper;
using BLL;
using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Hosting;
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
        public DriverMedicalCertificatesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            IDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO> service, IHostingEnvironment environment)
            : base(localizer, mapper, service, environment)
        {
            Validator = new ValidatorDriverMedicalCertificateController(Localizer);
        }
    }
}
