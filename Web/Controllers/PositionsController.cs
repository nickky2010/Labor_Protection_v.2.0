using AutoMapper;
using BLL;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Web.Infrastructure;
using Web.Interfaces;
using Web.ViewModels.Positions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class PositionsController : ControllerExt<PositionDTO, PositionGetVModel>, 
        IControllerServices<PositionsController, IDataBaseService<PositionDTO>>, ICRUDController<PositionAddVModel, PositionUpdateVModel>
    {
        public IStringLocalizer<SharedResource> Localizer { get; private set; }
        public IMapper Mapper { get; private set; }
        public IDataBaseService<PositionDTO> Service { get; private set; }

        public PositionsController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<PositionDTO> service)
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
            return SetDataArrayResult(await Service.GetPageAsync(startItem, countItem), Mapper);
        }

        // GET api/<controller>/5
        [HttpGet("{guid}")]
        public async Task<IAppActionResult> Get(Guid guid)
        {
            return SetDataResult(await Service.GetAsync(guid), Mapper);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IAppActionResult> Post([FromBody] PositionAddVModel viewModel)
        {
            if (viewModel == null)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["NoData"] } });
            if (!ModelState.IsValid)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["DataIsNotValid"] } });
            return SetDataResult(await Service.AddAsync(Mapper.Map<PositionAddVModel, PositionDTO>(viewModel)), Mapper);
        }
        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IAppActionResult> Put([FromBody] PositionUpdateVModel viewModel)
        {
            if (viewModel == null)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["NoData"] } });
            if (!ModelState.IsValid)
                return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["DataIsNotValid"] } });
            return SetDataResult(await Service.UpdateAsync(Mapper.Map<PositionUpdateVModel, PositionDTO>(viewModel)), Mapper);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{guid}")]
        public async Task<IAppActionResult> Delete(Guid guid)
        {
            return SetResult(await Service.DeleteAsync(guid));
        }
    }
}
