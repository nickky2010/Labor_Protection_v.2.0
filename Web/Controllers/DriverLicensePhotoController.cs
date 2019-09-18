using AutoMapper;
using BLL;
using BLL.DTO.DriverLicensePhotos;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverLicensePhotosController :
        AbstractPhotoController<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO>,
        IControllerServices<DriverLicensePhotosController, IDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO>>
    {
        public DriverLicensePhotosController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            IDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO> service, IHostingEnvironment environment)
            : base(localizer, mapper, service, environment)
        {
            Validator = new ValidatorDriverLicensePhotoController(Localizer);  
        }
    }
}
