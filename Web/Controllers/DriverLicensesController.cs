using AutoMapper;
using BLL;
using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Controllers.Abstract;
using Web.ValidatorsOfControllers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverLicensesController :
        AbstractCRUDDataController<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>
    {
        public DriverLicensesController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            ICRUDDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverLicenseController(Localizer);
        }
    }
}
