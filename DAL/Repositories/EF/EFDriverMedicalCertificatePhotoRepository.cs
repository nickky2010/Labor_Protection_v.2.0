using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EF
{
    public class EFDriverMedicalCertificatePhotoRepository : AbstractEFRepository<DriverMedicalCertificatePhoto>
    {
        public EFDriverMedicalCertificatePhotoRepository(DbContext context) : base(context) { }
    }
}