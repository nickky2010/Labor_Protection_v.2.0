using AutoMapper;
using BLL;
using BLL.DTO.Employees;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : AbstractController<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>, 
        IControllerServices<EmployeesController, IDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>>, 
        ICRUDController<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>
    {
        public EmployeesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO> service)
            : base(localizer, mapper, service) { }

        // GET: api/<controller>?startItem=1&countItem=1
        [HttpGet]
        public async Task<IAppActionResult<IList<EmployeeGetDTO>>> Get([FromQuery] int startItem, [FromQuery] int countItem)
        {
            if (startItem < 1)                
                return SendErrorForGetList((int)HttpStatusCode.BadRequest, "StartItemNotExist");
            if (countItem < 1)
                return SendErrorForGetList((int)HttpStatusCode.BadRequest, "CountItemsLeastOne");
            return SendResult(await Service.GetPageAsync(startItem, countItem));
        }

        // GET api/<controller>/5
        [HttpGet("{guid}")]
        public async Task<IAppActionResult<EmployeeGetDTO>> Get(Guid guid)
        {
            return SendGetResult(await Service.GetAsync(guid));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IAppActionResult<EmployeeGetDTO>> Post([FromBody] EmployeeAddDTO addDTO)
        {
            if (addDTO == null)
                return SendErrorForGet((int)HttpStatusCode.BadRequest, "NoData");
            if (!ModelState.IsValid)
                return SendErrorForGet((int)HttpStatusCode.BadRequest, "DataIsNotValid");
            return SendGetResult(await Service.AddAsync(addDTO));
        }
        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IAppActionResult<EmployeeUpdateDTO>> Put([FromBody]EmployeeUpdateDTO updateDTO)
        {
            if (updateDTO == null)
                return SendErrorForUpdate((int)HttpStatusCode.BadRequest, "NoData");
            if (!ModelState.IsValid)
                return SendErrorForUpdate((int)HttpStatusCode.BadRequest, "DataIsNotValid");
            return SendUpdateResult(await Service.UpdateAsync(updateDTO));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{guid}")]
        public async Task<IAppActionResult> Delete(Guid guid)
        {
            return SendResult(await Service.DeleteAsync(guid));
        }
    }
}
