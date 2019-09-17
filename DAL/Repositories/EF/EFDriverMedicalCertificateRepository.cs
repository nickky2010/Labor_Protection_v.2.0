using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFDriverMedicalCertificateRepository : AbstractEFRepository<DriverMedicalCertificate>
    {
        public EFDriverMedicalCertificateRepository(DbContext context) : base(context)
        {
            Query = Query
                .Include(b => b.Photos)
                .Include(b => b.Employee)
                .Include(b => b.DriverMedicalCertificateDriverCategories)
                    .ThenInclude(x => x.DriverCategory);
        }
    }
}