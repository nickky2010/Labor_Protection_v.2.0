using AutoMapper;
using BLL.DTO.Employees;
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
    public class EmployeesController : AbstractCRUDController<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>, 
        IControllerServices<EmployeesController, ICRUDDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>> 
    {
        public EmployeesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            ICRUDDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorEmployeeController(Localizer);
        }
    }
}
