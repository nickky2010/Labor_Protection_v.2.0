using DAL.EFContexts.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> :
    IService<LaborProtectionContext>
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TGetDTO : IGetDTO
    {
        Task<IAppActionResult<TGetDTO>> AddAsync(TAddDTO entity);
        Task<IAppActionResult> DeleteAsync(Guid guid);
        Task<IAppActionResult<TGetDTO>> GetAsync(Guid guid);
        Task<IAppActionResult<List<TGetDTO>>> GetPageAsync(int startItem, int countItem);
        Task<IAppActionResult<TGetDTO>> UpdateAsync(TUpdateDTO entity);
    }
}
