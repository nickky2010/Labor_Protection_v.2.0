using AutoMapper;
using BLL;
using BLL.DTO.Positions;
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
    public class PositionsController :
        AbstractController<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>,
        IControllerServices<PositionsController, IDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>>,
        ICRUDController<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>
    {
        public PositionsController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO> service)
            : base(localizer, mapper, service) { }

        // GET: api/<controller>?startItem=1&countItem=1
        [HttpGet]
        public async Task<IAppActionResult<IList<PositionGetUpdateDTO>>> Get([FromQuery] int startItem, [FromQuery] int countItem)
        {
            if (startItem < 1)
                return SendErrorForGetList((int)HttpStatusCode.BadRequest, "StartItemNotExist");
            if (countItem < 1)
                return SendErrorForGetList((int)HttpStatusCode.BadRequest, "CountItemsLeastOne");
            return SendResult(await Service.GetPageAsync(startItem, countItem));
        }

        // GET api/<controller>/5
        [HttpGet("{guid}")]
        public async Task<IAppActionResult<PositionGetUpdateDTO>> Get(Guid guid)
        {
            return SendGetResult(await Service.GetAsync(guid));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IAppActionResult<PositionGetUpdateDTO>> Post([FromBody] PositionAddDTO dtoAdd)
        {
            if (dtoAdd == null)
                return SendErrorForGet((int)HttpStatusCode.BadRequest, "NoData");
            if (!ModelState.IsValid)
                return SendErrorForGet((int)HttpStatusCode.BadRequest, "DataIsNotValid");
            return SendGetResult(await Service.AddAsync(dtoAdd));
        }
        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IAppActionResult<PositionGetUpdateDTO>> Put([FromBody] PositionGetUpdateDTO dtoUpdate)
        {
            if (dtoUpdate == null)
                return SendErrorForUpdate((int)HttpStatusCode.BadRequest, "NoData");
            if (!ModelState.IsValid)
                return SendErrorForUpdate((int)HttpStatusCode.BadRequest, "DataIsNotValid");
            return SendGetResult(await Service.UpdateAsync(dtoUpdate));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{guid}")]
        public async Task<IAppActionResult> Delete(Guid guid)
        {
            return SendResult(await Service.DeleteAsync(guid));
        }
    }
}
