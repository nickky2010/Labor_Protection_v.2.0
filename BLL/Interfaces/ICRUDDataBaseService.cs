﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> : ILocalService
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TGetDTO : IGetDTO
    {
        Task<IAppActionResult<TGetDTO>> AddAsync(TAddDTO entity);
        Task<IAppActionResult> DeleteAsync(Guid guid);
        Task<IAppActionResult<TGetDTO>> GetAsync(Guid guid);
        Task<IAppActionResult<List<TGetDTO>>> GetPageAsync(int startItem, int countItem);
        Task<IAppActionResult<TGetDTO>> UpdateAsync(TUpdateDTO entity);
        Task<IAppActionResult<int>> GetCountElementAsync();
    }
}
