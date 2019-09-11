using AutoMapper;
using BLL;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Web.Infrastructure;
using Web.Interfaces;
using Web.ViewModels.Employees;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : ControllerExt<EmployeeDTO, EmployeeViewModel>, 
        IControllerServices<EmployeesController, IDataBaseService<EmployeeDTO>>, ICRUDController<EmployeeViewModel>
    {
        public IStringLocalizer<SharedResource> Localizer { get; private set; }
        public IMapper Mapper { get; private set; }
        public IDataBaseService<EmployeeDTO> Service { get; private set; }

        public EmployeesController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<EmployeeDTO> service)
        {
            Service = service;
            Localizer = localizer;
            Service.Localizer = Localizer;
            Mapper = mapper;
        }

        // GET: api/<controller>?startItem=1&countItem=1
        [HttpGet]
        public async Task<IAppActionResult> Get([FromQuery] int startItem, [FromQuery] int countItem)
        {
            if (startItem < 1)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["StartItemNotExist"] } });
            if (countItem < 1)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["CountItemsLeastOne"] } });
            return SetResult(await Service.GetPageAsync(startItem, countItem));
        }

        // GET api/<controller>/5
        [HttpGet("{guid}")]
        public async Task<IAppActionResult> Get(Guid guid)
        {
            return SetResult(await Service.GetAsync(guid));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IAppActionResult> Post([FromBody] EmployeeViewModel viewModel)
        {
            if (viewModel == null)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["NoData"] } });
            if (!ModelState.IsValid)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["DataIsNotValid"] } });
            return SetDataResult(await Service.AddAsync(Mapper.Map<EmployeeViewModel, EmployeeDTO>(viewModel)), Mapper);
        }
        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IAppActionResult> Put([FromBody] EmployeeViewModel viewModel)
        {
            if (viewModel == null)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["NoData"] } });
            if (!ModelState.IsValid)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["DataIsNotValid"] } });
            return SetDataResult(await Service.UpdateAsync(Mapper.Map<EmployeeViewModel, EmployeeDTO>(viewModel)), Mapper);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{guid}")]
        public async Task<IAppActionResult> Delete(Guid guid)
        {
            return SetResult(await Service.DeleteAsync(guid));
        }
    }
}
