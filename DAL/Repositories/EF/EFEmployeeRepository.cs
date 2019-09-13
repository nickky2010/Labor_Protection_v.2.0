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
                .Include(b => b.DriverMedicalCertificate);
        }
    }
}