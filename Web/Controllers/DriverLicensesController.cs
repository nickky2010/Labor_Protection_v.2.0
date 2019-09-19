using AutoMapper;
using BLL.DTO.DriverLicenses;
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
    public class DriverLicensesController : 
        AbstractCRUDController<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>,
        IControllerServices<DriverLicensesController, ICRUDDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>>
    {
        public DriverLicensesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            ICRUDDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverLicenseController(Localizer);
        }
    }
}
