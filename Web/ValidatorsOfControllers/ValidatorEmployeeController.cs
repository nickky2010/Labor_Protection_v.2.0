using BLL.DTO.Employees;
using BLL;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorEmployeeController: 
        AbstractValidatorOfControllers<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>
    {
        public ValidatorEmployeeController(IStringLocalizer<SharedResource> localizer):base(localizer) { }
    }
}
