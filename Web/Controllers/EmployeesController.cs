using AutoMapper;
using BLL;
using BLL.DTO.Employees;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : AbstractCRUDDataController<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO> 
    {
        public EmployeesController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            ICRUDDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorEmployeeController(Localizer);
        }
    }
}
