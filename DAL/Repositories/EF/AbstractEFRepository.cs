using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class AbstractEFRepository<TData> : IRepository<TData>
        where TData : Data
    {
        protected DbContext Context { get; set; }
        protected DbSet<TData> DbSet { get; set; }
        protected IQueryable<TData> Query { get; set; }

        public AbstractEFRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TData>();
            Query = DbSet.AsNoTracking().AsQueryable();
        }
        public virtual Task AddAsync(TData data)
        {
            return DbSet.AddAsync(data);
        }
        public virtual Task AddRangeAsync(IList<TData> datas)
        {
            return DbSet.AddRangeAsync(datas);
        }

        public virtual void Update(TData data)
        {
            Context.Update(data);
        }

        public virtual void Delete(TData data)
        {
            DbSet.Remove(data);
        }
        public virtual void DeleteRange(IList<TData> datas)
        {
            DbSet.RemoveRange(datas);
        }

        public virtual Task<TData> FindAsync(Expression<Func<TData, bool>> where)
        {
            return Query.FirstOrDefaultAsync(where);
        }

        public virtual Task<List<TData>> GetPageAsync(int startItem, int countItem)
        {
            return Query.Skip(startItem).Take(countItem).ToListAsync();
        }

        public virtual Task<List<TData>> GetAllAsync(Expression<Func<TData, bool>> where)
        {
            return Query.Where(where).ToListAsync();
        }

        public virtual Task<int> CountElementAsync()
        {
            return Query.CountAsync();
        }

        public virtual Task<List<TData>> GetAllAsync()
        {
            return Query.ToListAsync();
        }

        public virtual Task<bool> IsIdExistAsync(Guid id)
        {
            return Query.AnyAsync(m => m.Id == id);
        }

        public virtual Task<bool> IsAllIdExistAsync(IList<Guid> idList)
        {
            return Query.AnyAsync(m => idList.Contains(m.Id));
        }
    }
}
