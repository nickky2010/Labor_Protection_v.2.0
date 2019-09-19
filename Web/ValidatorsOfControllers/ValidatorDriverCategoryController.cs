using BLL.DTO.DriverCategories;
using BLL;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorDriverCategoryController : 
        AbstractValidatorOfControllers<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO>
    {
        public ValidatorDriverCategoryController(IStringLocalizer<SharedResource> localizer):base(localizer) { }
    }
}
