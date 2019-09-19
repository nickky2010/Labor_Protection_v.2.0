using AutoMapper;
using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;
using Web.ValidatorsOfControllers;
using Web.Controllers.Abstract;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverMedicalCertificatesController : 
        AbstractCRUDController<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>,
        IControllerServices<DriverMedicalCertificatesController, ICRUDDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>>
    {
        public DriverMedicalCertificatesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            ICRUDDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverMedicalCertificateController(Localizer);
        }
    }
}
