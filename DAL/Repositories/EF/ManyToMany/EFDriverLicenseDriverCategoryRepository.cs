using DAL.Interfaces;
using DAL.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.EF.ManyToMany
{
    public class EFDriverLicenseDriverCategoryRepository : IRepository<DriverLicenseDriverCategory>
    {
        protected DbContext _context;
        private DbSet<DriverLicenseDriverCategory> _dbSet;

        public EFDriverLicenseDriverCategoryRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<DriverLicenseDriverCategory>();
        }
        public void AddAsync(DriverLicenseDriverCategory entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<DriverLicenseDriverCategory> entities)
        {
            _dbSet.AddRangeAsync(entities);
        }

        public void Update(DriverLicenseDriverCategory entity)
        {
            _context.Update(entity);
        }

        public void Delete(DriverLicenseDriverCategory entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<DriverLicenseDriverCategory> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(Expression<Func<DriverLicenseDriverCategory, bool>> where)
        {
            IEnumerable<DriverLicenseDriverCategory> objects = _dbSet.Where(where).AsEnumerable();
            foreach (DriverLicenseDriverCategory obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<DriverLicenseDriverCategory> FindAsync(Expression<Func<DriverLicenseDriverCategory, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .FirstOrDefaultAsync(where);
        }

        public Task<DriverLicenseDriverCategory> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<DriverLicenseDriverCategory>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<DriverLicenseDriverCategory>> GetAllAsync(Expression<Func<DriverLicenseDriverCategory, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<DriverLicenseDriverCategory>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .ToListAsync();
        }
    }
}