using DAL.EFContexts.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDataBaseService<TDTO>: IService<LaborProtectionContext>
        where TDTO : IDataDTO
    {
        Task<IAppActionResult> AddAsync(TDTO entity);
        Task<IAppActionResult> UpdateAsync(TDTO entity);
        Task<IAppActionResult> DeleteAsync(Guid guid);
        Task<IAppActionResult> GetAsync(Guid guid);
        Task<IAppActionResult> GetPageAsync(int startItem, int countItem);
    }
}
