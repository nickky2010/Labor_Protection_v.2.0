using AutoMapper;
using BLL;
using BLL.DTO.Employees;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : AbstractController<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>, 
        IControllerServices<EmployeesController, IDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>> 
    {
        public EmployeesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            IDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO> service, IHostingEnvironment environment)
            : base(localizer, mapper, service, environment)
        {
            Validator = new ValidatorEmployeeController(Localizer);
        }
    }
}
