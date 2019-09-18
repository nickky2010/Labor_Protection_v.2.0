﻿using AutoMapper;
using BLL;
using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverLicensesController : 
        AbstractController<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>,
        IControllerServices<DriverLicensesController, IDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>>
    {
        public DriverLicensesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            IDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO> service, IHostingEnvironment environment)
            : base(localizer, mapper, service, environment)
        {
            Validator = new ValidatorDriverLicenseController(Localizer);
        }
    }
}
