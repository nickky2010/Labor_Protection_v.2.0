using AutoMapper;
using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
    public abstract class AbstractController<TGetDTO, TAddDTO, TUpdateDTO> : Controller, 
        ICRUDController<TGetDTO, TAddDTO, TUpdateDTO>

        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        public IStringLocalizer<SharedResource> Localizer { get; protected set; }
        public IMapper Mapper { get; private set; }
        public IDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> Service { get; protected set; }
        public IValidatorController<TGetDTO, TAddDTO, TUpdateDTO> Validator { get; protected set; }

        public AbstractController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> service)
        {
            Service = service;
            Localizer = localizer;
            Service.Localizer = Localizer;
            Mapper = mapper;
        }
        // GET: api/<controller>?startItem=1&countItem=1
        [HttpGet]
        public async Task<IAppActionResult<List<TGetDTO>>> Get([FromQuery] int startItem, [FromQuery] int countItem)
        {
            var result = Validator.ValidatePaging(startItem, countItem);
            if (!result.IsSuccess)
                return result;
            return SendResult(await Service.GetPageAsync(startItem, countItem));
        }

        // GET api/<controller>/5
        [HttpGet("{guid}")]
        public async Task<IAppActionResult<TGetDTO>> Get(Guid guid)
        {
            return SendGetResult(await Service.GetAsync(guid));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IAppActionResult<TGetDTO>> Post([FromBody] TAddDTO addDTO)
        {
            var result = Validator.ValidateAdd(addDTO, ModelState);
            if (!result.IsSuccess)
                return result;
            return SendGetResult(await Service.AddAsync(addDTO));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IAppActionResult<TUpdateDTO>> Put([FromBody] TUpdateDTO updateDTO)
        {
            var result = Validator.ValidateUpdate(updateDTO, ModelState);
            if (!result.IsSuccess)
                return result;
            return SendUpdateResult(await Service.UpdateAsync(updateDTO));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{guid}")]
        public async Task<IAppActionResult> Delete(Guid guid)
        {
            return SendResult(await Service.DeleteAsync(guid));
        }

        protected void SetResult(int status)
        {
            ControllerContext.HttpContext.Response.StatusCode = status;
        }

        protected IAppActionResult<List<TGetDTO>> SendResult(IAppActionResult<List<TGetDTO>> result)
        {
            SetResult(result.Status);
            return result;
        }

        protected IAppActionResult<TGetDTO> SendGetResult(IAppActionResult<TGetDTO> result)
        {
            SetResult(result.Status);
            return result;
        }
        protected IAppActionResult<TUpdateDTO> SendUpdateResult(IAppActionResult<TUpdateDTO> result)
        {
            SetResult(result.Status);
            return result;
        }
        protected IAppActionResult SendResult(IAppActionResult result)
        {
            SetResult(result.Status);
            return result;
        }
    }
}
