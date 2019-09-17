using BLL.DTO.Employees;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorEmployeeController: 
        AbstractValidatorOfControllers<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>
    {
        public ValidatorEmployeeController(IStringLocalizer<SharedResource> localizer):base(localizer) { }
    }
}
