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
    public class EFDriverMedicalCertificatePhotoRepository : IRepository<DriverMedicalCertificatePhoto>
    {
        protected DbContext _context;
        private DbSet<DriverMedicalCertificatePhoto> _dbSet;

        public EFDriverMedicalCertificatePhotoRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<DriverMedicalCertificatePhoto>();
        }
        public void AddAsync(DriverMedicalCertificatePhoto entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<DriverMedicalCertificatePhoto> entities)
        {
            _dbSet.AddRangeAsync(entities);
        }

        public void Update(DriverMedicalCertificatePhoto entity)
        {
            _context.Update(entity);
        }

        public void Delete(DriverMedicalCertificatePhoto entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<DriverMedicalCertificatePhoto> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(Expression<Func<DriverMedicalCertificatePhoto, bool>> where)
        {
            IEnumerable<DriverMedicalCertificatePhoto> objects = _dbSet.Where(where).AsEnumerable();
            foreach (DriverMedicalCertificatePhoto obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<DriverMedicalCertificatePhoto> FindAsync(Expression<Func<DriverMedicalCertificatePhoto, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .FirstOrDefaultAsync(where);
        }

        public Task<DriverMedicalCertificatePhoto> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<DriverMedicalCertificatePhoto>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<DriverMedicalCertificatePhoto>> GetAllAsync(Expression<Func<DriverMedicalCertificatePhoto, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<DriverMedicalCertificatePhoto>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .ToListAsync();
        }
    }
}