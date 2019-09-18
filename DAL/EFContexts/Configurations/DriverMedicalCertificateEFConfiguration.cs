using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations
{
    class DriverMedicalCertificateEFConfiguration : IEntityTypeConfiguration<DriverMedicalCertificate>
    {
        public void Configure(EntityTypeBuilder<DriverMedicalCertificate> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasOne(b => b.Employee).WithOne(ba => ba.DriverMedicalCertificate).HasForeignKey<Employee>(b => b.DriverMedicalCertificateId);
            builder.HasMany(b => b.DriverMedicalCertificateDriverCategories).WithOne(bg => bg.DriverMedicalCertificate).HasForeignKey(b => b.DriverMedicalCertificateId);
            builder.HasMany(b => b.Photos).WithOne(ba => ba.DriverMedicalCertificate).HasForeignKey(b => b.DriverMedicalCertificateId);
        }
    }
}
