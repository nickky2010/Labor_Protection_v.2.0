using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFDriverLicenseRepository: AbstractEFRepository<DriverLicense>
    {
        public EFDriverLicenseRepository(DbContext context) : base(context)
        {
            Query = Query
                .Include(b => b.DriverLicenseDriverCategories)
                .Include(b => b.Photos)
                .Include(b => b.Employee);
        }
    }
}