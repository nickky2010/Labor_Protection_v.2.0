using DAL.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations.ManyToMany
{
    class DriverMedicalCertificateDriverCategoryEFConfiguration : IEntityTypeConfiguration<DriverMedicalCertificateDriverCategory>
    {
        public void Configure(EntityTypeBuilder<DriverMedicalCertificateDriverCategory> builder)
        {
            builder.HasKey(k => new { k.DriverCategoryId, k.DriverMedicalCertificateId });
            builder.HasOne(b => b.DriverCategory).WithMany(bg => bg.DriverMedicalCertificateDriverCategories).HasForeignKey(b => b.DriverCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(g => g.DriverMedicalCertificate).WithMany(bg => bg.DriverMedicalCertificateDriverCategories).HasForeignKey(g => g.DriverMedicalCertificateId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(b => b.DriverCategoryId).ValueGeneratedNever();
            builder.Property(g => g.DriverMedicalCertificateId).ValueGeneratedNever();
        }
    }
}
