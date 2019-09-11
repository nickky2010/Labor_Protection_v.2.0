using DAL.Models;
using DAL.Models.Identity;
using DAL.Models.ManyToMany;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork<C> : IDisposable
            where C : IdentityDbContext<User>
    {
        IRepository<DriverLicense> DriverLicenses { get; }
        IRepository<DriverMedicalCertificate> DriverMedicalCertificates { get; }
        IRepository<Employee> Employees { get; }
        IRepository<Position> Positions { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<DriverLicenseDriverCategory> DriverLicenseDriverCategories { get; }
        IRepository<DriverMedicalCertificateDriverCategory> DriverMedicalCertificateDriverCategories { get; }
        IRepository<DriverLicensePhoto> DriverLicensePhotos { get; }
        IRepository<DriverMedicalCertificatePhoto> DriverMedicalCertificatePhotos { get; }
        Task<int> SaveChangesAsync();
    }
}
