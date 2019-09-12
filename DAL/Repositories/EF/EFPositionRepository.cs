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
    public class EFPositionRepository : IRepository<Position>
    {
        protected DbContext _context;
        private DbSet<Position> _dbSet;

        public EFPositionRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<Position>();
        }
        public void AddAsync(Position entity)
        {
            _dbSet.AddAsync(entity);
        }
        public void AddRangeAsync(IList<Position> positions)
        {
            _dbSet.AddRangeAsync(positions);
        }

        public void Update(Position entity)
        {
            _context.Update(entity);
        }

        public void Delete(Position entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<Position> positions)
        {
            _dbSet.RemoveRange(positions);
        }

        public void Delete(Expression<Func<Position, bool>> where)
        {
            IEnumerable<Position> objects = _dbSet.Where(where).AsEnumerable();
            foreach (Position obj in objects)
                _dbSet.Remove(obj);
        }

        public Task<Position> FindAsync(Expression<Func<Position, bool>> where)
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Employees)
                .FirstOrDefaultAsync(where);
        }

        public Task<Position> GetLastItemAsync()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable()
                .LastOrDefaultAsync();
        }
        public async Task<IList<Position>> GetPageAsync(int startItem, int countItem)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Employees)
                .Skip(startItem - 1)
                .Take(countItem)
                .ToListAsync();
        }

        public async Task<IList<Position>> GetAllAsync(Expression<Func<Position, bool>> where)
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Employees)
                .Where(where)
                .ToListAsync();
        }
        public async Task<IList<Position>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .AsQueryable()
                .Include(b => b.Employees)
                .ToListAsync();
        }
    }
}