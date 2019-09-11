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
    public class EFDriverMedicalCertificateDriverCategoryRepository : IRepository<DriverMedicalCertificateDriverCategory>
    {
        protected DbContext _context;
        private DbSet<DriverMedicalCertificateDriverCategory> _dbSet;

        public EFDriverMedicalCertificateDriverCategoryRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<DriverMedicalCertificateDriverCategory>();
        }
        public void AddAsync(DriverMedicalCertificateDriverCategory entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<DriverMedicalCertificateDriverCategory> entities)
        {
            _dbSet.AddRangeAsync(entities);
        }

        public void Update(DriverMedicalCertificateDriverCategory entity)
        {
            _context.Update(entity);
        }

        public void Delete(DriverMedicalCertificateDriverCategory entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<DriverMedicalCertificateDriverCategory> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(Expression<Func<DriverMedicalCertificateDriverCategory, bool>> where)
        {
            IEnumerable<DriverMedicalCertificateDriverCategory> objects = _dbSet.Where(where).AsEnumerable();
            foreach (DriverMedicalCertificateDriverCategory obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<DriverMedicalCertificateDriverCategory> FindAsync(Expression<Func<DriverMedicalCertificateDriverCategory, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .FirstOrDefaultAsync(where);
        }

        public Task<DriverMedicalCertificateDriverCategory> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<DriverMedicalCertificateDriverCategory>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<DriverMedicalCertificateDriverCategory>> GetAllAsync(Expression<Func<DriverMedicalCertificateDriverCategory, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<DriverMedicalCertificateDriverCategory>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .ToListAsync();
        }
    }
}