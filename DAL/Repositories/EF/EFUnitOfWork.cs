using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories.EF
{
    public class EFUnitOfWork<C> : IUnitOfWork<C>
        where C : DbContext
    {
        private readonly C _context;
        private bool disposed = false;

        public EFUnitOfWork(C context)
        {
            _context = context;
        }

        public IRepository<DriverCategory> DriverCategories => new Lazy<IRepository<DriverCategory>>(new EFDriverCategoryRepository(_context)).Value;
        public IRepository<DriverLicense> DriverLicenses => new Lazy<IRepository<DriverLicense>>(new EFDriverLicenseRepository(_context)).Value;
        public IRepository<DriverMedicalCertificate> DriverMedicalCertificates => new Lazy<IRepository<DriverMedicalCertificate>>(new EFDriverMedicalCertificateRepository(_context)).Value;
        public IRepository<Employee> Employees => new Lazy<IRepository<Employee>>(new EFEmployeeRepository(_context)).Value;
        public IRepository<Position> Positions => new Lazy<IRepository<Position>>(new EFPositionRepository(_context)).Value;
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
