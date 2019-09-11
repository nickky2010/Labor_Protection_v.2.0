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
    public class EFDriverLicensePhotoRepository : IRepository<DriverLicensePhoto>
    {
        protected DbContext _context;
        private DbSet<DriverLicensePhoto> _dbSet;

        public EFDriverLicensePhotoRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<DriverLicensePhoto>();
        }
        public void AddAsync(DriverLicensePhoto entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<DriverLicensePhoto> entities)
        {
            _dbSet.AddRangeAsync(entities);
        }

        public void Update(DriverLicensePhoto entity)
        {
            _context.Update(entity);
        }

        public void Delete(DriverLicensePhoto entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<DriverLicensePhoto> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(Expression<Func<DriverLicensePhoto, bool>> where)
        {
            IEnumerable<DriverLicensePhoto> objects = _dbSet.Where(where).AsEnumerable();
            foreach (DriverLicensePhoto obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<DriverLicensePhoto> FindAsync(Expression<Func<DriverLicensePhoto, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .FirstOrDefaultAsync(where);
        }

        public Task<DriverLicensePhoto> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<DriverLicensePhoto>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<DriverLicensePhoto>> GetAllAsync(Expression<Func<DriverLicensePhoto, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<DriverLicensePhoto>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .ToListAsync();
        }
    }
}