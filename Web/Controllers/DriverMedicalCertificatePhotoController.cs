using AutoMapper;
using BLL;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverMedicalCertificatePhotosController :
        AbstractPhotoController<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO>,
        IControllerServices<DriverMedicalCertificatePhotosController, IDataBaseService<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO>>
    {
        public DriverMedicalCertificatePhotosController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            IDataBaseService<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO> service, IHostingEnvironment environment)
            : base(localizer, mapper, service, environment)
        {
            Validator = new ValidatorDriverMedicalCertificatePhotoController(Localizer);  
        }
    }
}
