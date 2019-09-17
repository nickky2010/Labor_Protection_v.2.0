using AutoMapper;
using BLL;
using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverLicensessController : 
        AbstractController<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>,
        IControllerServices<DriverLicensessController, IDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>>
    {
        public DriverLicensessController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverLicenseController(Localizer);
        }
    }
}
