using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFDriverCategoryRepository : AbstractEFRepository<DriverCategory>/*, IDriverCategoryRepository*/
    {
        public EFDriverCategoryRepository(DbContext context) : base(context)
        {
            Query = Query
                .Include(b => b.DriverLicenseDriverCategories)
                .Include(b => b.DriverMedicalCertificateDriverCategories);
        }

        //public Task<bool> AllContainNameAsync(IList<IDriverCategory> driverCategory)
        //{
        //    var guids = driverCategory.Select(g => g.DriverCategoryId).ToList();
        //    return _dbSet.AsNoTracking()
        //        .AsQueryable().
        //        .AllAsync(m => guids.Contains(m.Id));
        //}
    }
}