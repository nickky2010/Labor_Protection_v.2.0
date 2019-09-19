﻿using AutoMapper;
using BLL.DTO.DriverLicensePhotos;
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
    public class DriverLicensePhotosController :
        AbstractPhotoController<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO>,
        IControllerServices<DriverLicensePhotosController, ICRUDDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO>>
    {
        public DriverLicensePhotosController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            ICRUDDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverLicensePhotoController(Localizer);  
        }
    }
}
