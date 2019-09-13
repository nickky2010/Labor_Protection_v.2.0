using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFDriverLicensePhotoRepository : AbstractEFRepository<DriverLicensePhoto>
    {
        public EFDriverLicensePhotoRepository(DbContext context) : base(context) { }
    }
}