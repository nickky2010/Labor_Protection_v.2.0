using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        void AddAsync(T entity);
        void AddRangeAsync(IList<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IList<T> entities);
        void Delete(Expression<Func<T, bool>> where);
        Task<T> FindAsync(Expression<Func<T, bool>> where);
        Task<T> GetLastItemAsync();
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where);
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetPageAsync(int startItem, int countItem);
    }
}
