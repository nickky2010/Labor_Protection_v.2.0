using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFEmployeeRepository : AbstractEFRepository<Employee>
    {
        public EFEmployeeRepository(DbContext context) : base(context)
        {
            Query = Query
                .Include(b => b.Position)
                .Include(b => b.DriverLicense)
                    .ThenInclude(b => b.DriverLicenseDriverCategories)
                    .ThenInclude(x => x.DriverCategory)
                .Include(b => b.DriverMedicalCertificate)
                    .ThenInclude(b => b.DriverMedicalCertificateDriverCategories)
                    .ThenInclude(x => x.DriverCategory);
        }
    }
}