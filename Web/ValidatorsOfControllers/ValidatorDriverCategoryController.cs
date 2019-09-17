using BLL.DTO.DriverCategories;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverCategoryController : 
        AbstractValidatorOfControllers<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO>
    {
        public ValidatorDriverCategoryController(IStringLocalizer<SharedResource> localizer):base(localizer) { }
    }
}
