using AutoMapper;
using BLL;
using BLL.DTO.DriverCategories;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DriverCategoriesController : 
        AbstractController<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO>,
        IControllerServices<DriverCategoriesController, IDataBaseService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO>>
    {
        public DriverCategoriesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            IDataBaseService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO> service, IHostingEnvironment environment)
            : base(localizer, mapper, service, environment)
        {
            Validator = new ValidatorDriverCategoryController(Localizer);
        }
    }
}
