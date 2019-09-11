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
    public class EFDriverMedicalCertificateRepository : IRepository<DriverMedicalCertificate>
    {
        protected DbContext _context;
        private DbSet<DriverMedicalCertificate> _dbSet;

        public EFDriverMedicalCertificateRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<DriverMedicalCertificate>();
        }
        public void AddAsync(DriverMedicalCertificate entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<DriverMedicalCertificate> driverMedicalCertificates)
        {
            _dbSet.AddRangeAsync(driverMedicalCertificates);
        }

        public void Update(DriverMedicalCertificate entity)
        {
            _context.Update(entity);
        }

        public void Delete(DriverMedicalCertificate entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<DriverMedicalCertificate> driverMedicalCertificates)
        {
            _dbSet.RemoveRange(driverMedicalCertificates);
        }

        public void Delete(Expression<Func<DriverMedicalCertificate, bool>> where)
        {
            IEnumerable<DriverMedicalCertificate> objects = _dbSet.Where(where).AsEnumerable();
            foreach (DriverMedicalCertificate obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<DriverMedicalCertificate> FindAsync(Expression<Func<DriverMedicalCertificate, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.DriverMedicalCertificateDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(where);
        }

        public Task<DriverMedicalCertificate> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<DriverMedicalCertificate>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.DriverMedicalCertificateDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<DriverMedicalCertificate>> GetAllAsync(Expression<Func<DriverMedicalCertificate, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.DriverMedicalCertificateDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<DriverMedicalCertificate>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.DriverMedicalCertificateDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .ToListAsync();
        }
    }
}