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
        ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> Service { get; set; }
        IValidatorCRUDController<TGetDTO, TAddDTO, TUpdateDTO> Validator { get; set; }

        Task<IAppActionResult<List<TGetDTO>>> Get([FromQuery]int startItem, [FromQuery]int countItem);
        Task<IAppActionResult<TGetDTO>> Get(Guid guid);
        Task<IAppActionResult<TGetDTO>> Post(TAddDTO addDTO);
        Task<IAppActionResult<TGetDTO>> Put([FromBody]TUpdateDTO updateDTO);
        Task<IAppActionResult> Delete(Guid guid);
    }
}
