﻿using DAL.EFContexts.Configurations;
using DAL.EFContexts.Configurations.ManyToMany;
using DAL.Models;
using DAL.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace DAL.EFContexts.Contexts
{
    public class LaborProtectionContext : DbContext
    {
        readonly string _connectionString;
        public LaborProtectionContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DriverLicenseDriverCategoryEFConfiguration());
            modelBuilder.ApplyConfiguration(new DriverMedicalCertificateDriverCategoryEFConfiguration());

            modelBuilder.ApplyConfiguration(new DriverCategoryEFConfiguration());
            modelBuilder.ApplyConfiguration(new DriverLicenseEFConfiguration());
            modelBuilder.ApplyConfiguration(new DriverLicensePhotoEFConfiguration());
            modelBuilder.ApplyConfiguration(new DriverMedicalCertificateEFConfiguration());
            modelBuilder.ApplyConfiguration(new DriverMedicalCertificatePhotoEFConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEFConfiguration());
            modelBuilder.ApplyConfiguration(new PositionEFConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DriverLicenseDriverCategory> DriverLicenseDriverCategories { get; set; }
        public DbSet<DriverMedicalCertificateDriverCategory> DriverMedicalCertificateDriverCategories { get; set; }
        public DbSet<DriverLicense> DriverLicenses { get; set; }
        public DbSet<DriverLicensePhoto> DriverLicensePhotos { get; set; }
        public DbSet<DriverMedicalCertificate> DriverMedicalCertificates { get; set; }
        public DbSet<DriverMedicalCertificatePhoto> DriverMedicalCertificatePhotos { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
