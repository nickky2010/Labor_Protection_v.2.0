using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    internal interface ICRUDController<TGetDTO, TAddDTO, TUpdateDTO>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
    {
        Task<IAppActionResult<List<TGetDTO>>> Get([FromQuery]int startItem, [FromQuery]int countItem);
        Task<IAppActionResult<TGetDTO>> Get(Guid guid);
        Task<IAppActionResult<TGetDTO>> Post([FromBody]TAddDTO addDTO);
        Task<IAppActionResult<TUpdateDTO>> Put([FromBody]TUpdateDTO updateDTO);
        Task<IAppActionResult> Delete(Guid guid);
    }
}
