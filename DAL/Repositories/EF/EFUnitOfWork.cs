using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Identity;
using DAL.Models.ManyToMany;
using DAL.Repositories.EF.ManyToMany;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories.EF
{
    public class EFUnitOfWork<C> : IUnitOfWork<C>
        where C : IdentityDbContext<User>
    {
        private readonly C _context;
        private bool disposed = false;

        public EFUnitOfWork(C context)
        {
            _context = context;
        }

        public IRepository<DriverLicense> DriverLicenses => new Lazy<IRepository<DriverLicense>>(new EFDriverLicenseRepository(_context)).Value;
        public IRepository<DriverMedicalCertificate> DriverMedicalCertificates => new Lazy<IRepository<DriverMedicalCertificate>>(new EFDriverMedicalCertificateRepository(_context)).Value;
        public IRepository<Employee> Employees => new Lazy<IRepository<Employee>>(new EFEmployeeRepository(_context)).Value;
        public IRepository<Position> Positions => new Lazy<IRepository<Position>>(new EFPositionRepository(_context)).Value;
        public IRepository<User> Users => new Lazy<IRepository<User>>(new EFUserRepository(_context)).Value;
        public IRepository<Role> Roles => new Lazy<IRepository<Role>>(new EFRoleRepository(_context)).Value;
        public IRepository<UserProfile> UserProfiles => new Lazy<IRepository<UserProfile>>(new EFUserProfileRepository(_context)).Value;

        public IRepository<DriverLicenseDriverCategory> DriverLicenseDriverCategories => 
            new Lazy<IRepository<DriverLicenseDriverCategory>>(new EFDriverLicenseDriverCategoryRepository(_context)).Value;

        public IRepository<DriverMedicalCertificateDriverCategory> DriverMedicalCertificateDriverCategories => 
            new Lazy<IRepository<DriverMedicalCertificateDriverCategory>>(new EFDriverMedicalCertificateDriverCategoryRepository(_context)).Value;

        public IRepository<DriverLicensePhoto> DriverLicensePhotos => 
            new Lazy<IRepository<DriverLicensePhoto>>(new EFDriverLicensePhotoRepository(_context)).Value;

        public IRepository<DriverMedicalCertificatePhoto> DriverMedicalCertificatePhotos => 
            new Lazy<IRepository<DriverMedicalCertificatePhoto>>(new EFDriverMedicalCertificatePhotoRepository(_context)).Value;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
