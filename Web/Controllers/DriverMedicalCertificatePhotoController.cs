﻿using AutoMapper;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers;
using Web.Controllers.Abstract;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverMedicalCertificatePhotosController :
        AbstractCRUDPhotoController<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO>
    {
        public DriverMedicalCertificatePhotosController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            ICRUDDataBaseService<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverMedicalCertificatePhotoController(Localizer);  
        }
    }
}
