using AutoMapper;
using BLL;
using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverMedicalCertificatesController : 
        AbstractCRUDDataController<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>
    {
        public DriverMedicalCertificatesController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            ICRUDDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverMedicalCertificateController(Localizer);
        }
    }
}
