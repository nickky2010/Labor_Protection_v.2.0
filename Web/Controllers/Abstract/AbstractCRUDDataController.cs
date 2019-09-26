using AutoMapper;
using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers.Abstract
{
    public abstract class AbstractCRUDDataController<TGetDTO, TAddDTO, TUpdateDTO> :
        AbstractCRUDBaseController<TGetDTO, TAddDTO, TUpdateDTO>,
        ICRUDController<TGetDTO, TAddDTO, TUpdateDTO>
        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {

        public AbstractCRUDDataController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> service)
            : base(localizer, mapper, service) { }

        [HttpPost]
        public virtual async Task<IAppActionResult<TGetDTO>> Post([FromBody] TAddDTO addDTO)
        {
            return await base.PostBase(addDTO);
        }

        [HttpPut]
        public virtual async Task<IAppActionResult<TGetDTO>> Put([FromBody] TUpdateDTO updateDTO)
        {
            return await base.PutBase(updateDTO);
        }
    }
}
