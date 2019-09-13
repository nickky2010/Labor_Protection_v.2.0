using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork<C> : IDisposable
            where C : DbContext
    {
        IRepository<DriverCategory> DriverCategories { get; }
        IRepository<DriverLicense> DriverLicenses { get; }
        IRepository<DriverMedicalCertificate> DriverMedicalCertificates { get; }
        IRepository<Employee> Employees { get; }
        IRepository<Position> Positions { get; }
        IRepository<DriverLicensePhoto> DriverLicensePhotos { get; }
        IRepository<DriverMedicalCertificatePhoto> DriverMedicalCertificatePhotos { get; }
        Task<int> SaveChangesAsync();
    }
}
