using AutoMapper;
using BLL.DTO.DriverCategories;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Controllers.Abstract;
using Web.ValidatorsOfControllers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverCategoriesController : 
        AbstractCRUDDataController<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO>
    {
        public DriverCategoriesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            ICRUDDataBaseService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorDriverCategoryController(Localizer);
        }
    }
}
