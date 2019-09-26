using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFDriverCategoryRepository : AbstractEFRepository<DriverCategory>
    {
        public EFDriverCategoryRepository(DbContext context) : base(context)
        {
            Query = Query
                .Include(b => b.DriverLicenseDriverCategories)
                .Include(b => b.DriverMedicalCertificateDriverCategories);
        }
    }
}