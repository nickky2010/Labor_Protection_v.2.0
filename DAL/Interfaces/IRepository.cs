using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TData>
        where TData : Data
    {
        Task AddAsync(TData data);
        Task AddRangeAsync(IList<TData> datas);
        void Update(TData data);
        void Delete(TData data);
        void DeleteRange(IList<TData> datas);
        Task<TData> FindAsync(Expression<Func<TData, bool>> where);
        Task<List<TData>> GetPageAsync(int startItem, int countItem);
        Task<List<TData>> GetAllAsync(Expression<Func<TData, bool>> where);
        Task<List<TData>> GetAllAsync();
        Task<bool> IsIdExistAsync(Guid id);
        Task<bool> IsAllIdExistAsync(IList<Guid> idList);
        Task<int> CountElementAsync();
    }
}
