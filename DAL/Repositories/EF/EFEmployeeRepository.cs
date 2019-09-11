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
    public class EFEmployeeRepository : IRepository<Employee>
    {
        protected DbContext _context;
        private DbSet<Employee> _dbSet;

        public EFEmployeeRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<Employee>();
        }
        public void AddAsync(Employee entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<Employee> employees)
        {
            _dbSet.AddRangeAsync(employees);
        }

        public void Update(Employee entity)
        {
            _context.Update(entity);
        }

        public void Delete(Employee entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<Employee> employees)
        {
            _dbSet.RemoveRange(employees);
        }

        public void Delete(Expression<Func<Employee, bool>> where)
        {
            IEnumerable<Employee> objects = _dbSet.Where(where).AsEnumerable();
            foreach (Employee obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<Employee> FindAsync(Expression<Func<Employee, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Position)
                .Include(b => b.DriverLicense)
                .Include(b => b.DriverMedicalCertificate)
                .FirstOrDefaultAsync(where);
        }

        public Task<Employee> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<Employee>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Position)
                .Include(b => b.DriverLicense)
                .Include(b => b.DriverMedicalCertificate)
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<Employee>> GetAllAsync(Expression<Func<Employee, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Position)
                .Include(b => b.DriverLicense)
                .Include(b => b.DriverMedicalCertificate)
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<Employee>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Position)
                .Include(b => b.DriverLicense)
                .Include(b => b.DriverMedicalCertificate)
                .ToListAsync();
        }
    }
}