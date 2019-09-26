using BLL;
using BLL.DTO.DriverCategories;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorDriverCategoryController :
        AbstractValidatorOfControllers<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO>
    {
        public ValidatorDriverCategoryController(IStringLocalizer<SharedResource> localizer) : base(localizer) { }
    }
}
