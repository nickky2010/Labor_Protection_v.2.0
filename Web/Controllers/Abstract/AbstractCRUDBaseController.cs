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
    public abstract class AbstractCRUDBaseController<TGetDTO, TAddDTO, TUpdateDTO> : BaseController
        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        protected ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> Service { get; private set; }
        protected IValidatorCRUDController<TGetDTO, TAddDTO, TUpdateDTO> Validator { get; set; }

        public AbstractCRUDBaseController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> service) :  base(localizer, mapper)
        {
            Service = service;
            Service.Localizer = localizer;
        }

        [HttpGet]
        public virtual async Task<IAppActionResult<List<TGetDTO>>> Get([FromQuery] int startItem, [FromQuery] int countItem)
        {
            var result = Validator.ValidatePaging(startItem, countItem);
            SetResponseStatusCode(result);
            if (!result.IsSuccess)
                return result;
            result = await Service.GetPageAsync(startItem, countItem);
            SetResponseStatusCode(result);
            return result;
        }

        [HttpGet("{guid}")]
        public virtual async Task<IAppActionResult<TGetDTO>> Get(Guid guid)
        {
            var result = await Service.GetAsync(guid);
            SetResponseStatusCode(result);
            return result;
        }

        [HttpGet("GetCount")]
        public virtual async Task<IAppActionResult<int>> GetCount()
        {
            var result = await Service.GetCountElementAsync();
            SetResponseStatusCode(result);
            return result;
        }

        protected virtual async Task<IAppActionResult<TGetDTO>> PostBase(TAddDTO addDTO)
        {
            var result = Validator.ValidateAdd(addDTO, ModelState);
            SetResponseStatusCode(result);
            if (!result.IsSuccess)
                return result;
            result = await Service.AddAsync(addDTO);
            SetResponseStatusCode(result);
            return result;
        }

        protected virtual async Task<IAppActionResult<TGetDTO>> PutBase(TUpdateDTO updateDTO)
        {
            var result = Validator.ValidateUpdate(updateDTO, ModelState);
            SetResponseStatusCode(result);
            if (!result.IsSuccess)
                return result;
            result = await Service.UpdateAsync(updateDTO);
            SetResponseStatusCode(result);
            return result;
        }

        [HttpDelete("{guid}")]
        public virtual async Task<IAppActionResult> Delete(Guid guid)
        {
            var result = await Service.DeleteAsync(guid);
            SetResponseStatusCode(result);
            return result;
        }
    }
}
