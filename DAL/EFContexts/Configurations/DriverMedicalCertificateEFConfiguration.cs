using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations
{
    class DriverMedicalCertificateEFConfiguration : IEntityTypeConfiguration<DriverMedicalCertificate>
    {
        public void Configure(EntityTypeBuilder<DriverMedicalCertificate> builder)
        {
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasOne(b => b.Employee).WithOne(ba => ba.DriverMedicalCertificate).HasForeignKey<Employee>(b => b.DriverMedicalCertificateId);
        }
    }
}
