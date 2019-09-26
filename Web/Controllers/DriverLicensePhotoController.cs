using AutoMapper;
using BLL;
using BLL.DTO.DriverLicensePhotos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Controllers.Abstract;
using Web.ValidatorsOfControllers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverLicensePhotosController :
        AbstractCRUDPhotoController<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO>
    {
        public DriverLicensePhotosController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            ICRUDDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverLicensePhotoController(Localizer);
        }
    }
}
