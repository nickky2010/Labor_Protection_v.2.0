using AutoMapper;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers.Abstract
{
    public abstract class AbstractCRUDController<TGetDTO, TAddDTO, TUpdateDTO> : 
        AbstractBaseController<TGetDTO>, 
        ICRUDController<TGetDTO, TAddDTO, TUpdateDTO>

        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        public ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> Service { get; set; }
        public IValidatorCRUDController<TGetDTO, TAddDTO, TUpdateDTO> Validator { get; set; }

        public AbstractCRUDController(IStringLocalizer<BLL.SharedResource> localizer, IMapper mapper,
            ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> service) :  base(localizer, mapper)
        {
            Service = service;
            Service.Localizer = localizer;
        }

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
        public virtual async Task<IAppActionResult<TGetDTO>> Post([FromBody] TAddDTO addDTO)
        {
            var result = Validator.ValidateAdd(addDTO, ModelState);
            if (!result.IsSuccess)
                return result;
            return SendGetResult(await Service.AddAsync(addDTO));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public virtual async Task<IAppActionResult<TGetDTO>> Put([FromBody] TUpdateDTO updateDTO)
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
