using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.EF
{
    public class EFDriverLicenseRepository: IRepository<DriverLicense>
    {
        protected DbContext _context;
        private DbSet<DriverLicense> _dbSet;

        public EFDriverLicenseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<DriverLicense>();
        }
        public void AddAsync(DriverLicense entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<DriverLicense> driverLicenses)
        {
            _dbSet.AddRangeAsync(driverLicenses);
        }

        public void Update(DriverLicense entity)
        {
            _context.Update(entity);
        }

        public void Delete(DriverLicense entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<DriverLicense> driverLicenses)
        {
            _dbSet.RemoveRange(driverLicenses);
        }

        public void Delete(Expression<Func<DriverLicense, bool>> where)
        {
            IEnumerable<DriverLicense> objects = _dbSet.Where(where).AsEnumerable();
            foreach (DriverLicense obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<DriverLicense> FindAsync(Expression<Func<DriverLicense, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.DriverLicenseDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(where);
        }

        public Task<DriverLicense> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<DriverLicense>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.DriverLicenseDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<DriverLicense>> GetAllAsync(Expression<Func<DriverLicense, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.DriverLicenseDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<DriverLicense>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.DriverLicenseDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .ToListAsync();
        }
    }
}