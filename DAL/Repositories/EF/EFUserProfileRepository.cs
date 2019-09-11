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
    public class EFUserProfileRepository : IRepository<UserProfile>
    {
        protected DbContext _context;
        private DbSet<UserProfile> _dbSet;

        public EFUserProfileRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<UserProfile>();
        }
        public void AddAsync(UserProfile entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<UserProfile> userProfiles)
        {
            _dbSet.AddRangeAsync(userProfiles);
        }

        public void Update(UserProfile entity)
        {
            _context.Update(entity);
        }

        public void Delete(UserProfile entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<UserProfile> userProfiles)
        {
            _dbSet.RemoveRange(userProfiles);
        }

        public void Delete(Expression<Func<UserProfile, bool>> where)
        {
            IEnumerable<UserProfile> objects = _dbSet.Where(where).AsEnumerable();
            foreach (UserProfile obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<UserProfile> FindAsync(Expression<Func<UserProfile, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.User)
                .FirstOrDefaultAsync(where);
        }

        public Task<UserProfile> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<UserProfile>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<UserProfile>> GetAllAsync(Expression<Func<UserProfile, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.User)
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<UserProfile>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.User)
                .ToListAsync();
        }
    }
}