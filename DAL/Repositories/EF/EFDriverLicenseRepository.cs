using DAL.Models;

using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFDriverLicenseRepository: AbstractEFRepository<DriverLicense>
    {
        public EFDriverLicenseRepository(DbContext context) : base(context)
        {
            Query = Query

                //.Include(b => b.Photos)
                .Include(b => b.Employee)
                .Include(b => b.DriverLicenseDriverCategories)
                    .ThenInclude(x => x.DriverCategory);
        }
    }
}