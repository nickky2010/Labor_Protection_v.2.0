﻿using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface ICRUDController<TGetDTO, TAddDTO, TUpdateDTO>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
    {
        Task<IAppActionResult<List<TGetDTO>>> Get([FromQuery]int startItem, [FromQuery]int countItem);
        Task<IAppActionResult<TGetDTO>> Get(Guid guid);
        Task<IAppActionResult<TGetDTO>> Post(TAddDTO addDTO);
        Task<IAppActionResult<TGetDTO>> Put(TUpdateDTO updateDTO);
        Task<IAppActionResult> Delete(Guid guid);
    }
}
