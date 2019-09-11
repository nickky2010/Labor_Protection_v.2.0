using DAL.Interfaces;
using DAL.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.EF
{
    public class EFRoleRepository : IRepository<Role>
    {
        protected DbContext _context;
        private DbSet<Role> _dbSet;

        public EFRoleRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<Role>();
        }
        public void AddAsync(Role entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<Role> roles)
        {
            _dbSet.AddRangeAsync(roles);
        }

        public void Update(Role entity)
        {
            _context.Update(entity);
        }

        public void Delete(Role entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<Role> roles)
        {
            _dbSet.RemoveRange(roles);
        }

        public void Delete(Expression<Func<Role, bool>> where)
        {
            IEnumerable<Role> objects = _dbSet.Where(where).AsEnumerable();
            foreach (Role obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<Role> FindAsync(Expression<Func<Role, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Users)
                .FirstOrDefaultAsync(where);
        }

        public Task<Role> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<Role>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<Role>> GetAllAsync(Expression<Func<Role, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Users)
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<Role>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Users)
                .ToListAsync();
        }
    }
}