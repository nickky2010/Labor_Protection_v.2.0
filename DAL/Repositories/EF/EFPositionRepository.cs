using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFPositionRepository : AbstractEFRepository<Position>
    {
        public EFPositionRepository(DbContext context) : base(context)
        {
            Query = Query
                .Include(b => b.Employees);
        }
    }
}