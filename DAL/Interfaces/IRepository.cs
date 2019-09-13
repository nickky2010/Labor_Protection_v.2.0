using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TData>
        where TData : class
    {
        Task<EntityEntry<TData>> AddAsync(TData data);
        Task AddRangeAsync(IList<TData> datas);
        EntityEntry<TData> Update(TData data);
        EntityEntry<TData> Delete(TData data);
        Task<TData> FindAsync(Expression<Func<TData, bool>> where);
        Task<IList<TData>> GetPageAsync(int startItem, int countItem);
        Task<IList<TData>> GetAllAsync(Expression<Func<TData, bool>> where);
        Task<IList<TData>> GetAllAsync();
    }
}
