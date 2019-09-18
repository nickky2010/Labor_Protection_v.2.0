using AutoMapper;
using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
    public abstract class AbstractPhotoController<TGetDTO, TAddDTO, TUpdateDTO> : 
        AbstractBaseController<TGetDTO, TAddDTO, TUpdateDTO>, 
        ICRUDController<TGetDTO, TAddDTO, TUpdateDTO>

        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        public AbstractPhotoController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            IDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> service, IHostingEnvironment environment) :
            base(localizer, mapper, service, environment) { }

        // GET: api/<controller>?startItem=1&countItem=1
        [HttpGet]
        public virtual async Task<IAppActionResult<List<TGetDTO>>> Get([FromQuery] int startItem, [FromQuery] int countItem)
        {
            var result = Validator.ValidatePaging(startItem, countItem);
            if (!result.IsSuccess)
                return result;
            return SendResult(await Service.GetPageAsync(startItem, countItem));
        }

        // GET api/<controller>/5
        [HttpGet("{guid}")]
        public virtual async Task<IAppActionResult<TGetDTO>> Get(Guid guid)
        {
            return SendGetResult(await Service.GetAsync(guid));
        }

        // POST api/<controller>
        [HttpPost]
        public virtual async Task<IAppActionResult<TGetDTO>> Post(TAddDTO addDTO)
        {
            var result = Validator.ValidateAdd(addDTO, ModelState);
            if (!result.IsSuccess)
                return result;
            return SendGetResult(await Service.AddAsync(addDTO));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public virtual async Task<IAppActionResult<TGetDTO>> Put(TUpdateDTO updateDTO)
        {
            var result = Validator.ValidateUpdate(updateDTO, ModelState);
            if (!result.IsSuccess)
                return result;
            return SendGetResult(await Service.UpdateAsync(updateDTO));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{guid}")]
        public virtual async Task<IAppActionResult> Delete(Guid guid)
        {
            return SendResult(await Service.DeleteAsync(guid));
        }
    }
}
