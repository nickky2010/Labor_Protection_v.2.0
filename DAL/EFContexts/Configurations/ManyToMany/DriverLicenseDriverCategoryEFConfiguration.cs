using DAL.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations.ManyToMany
{
    class DriverLicenseDriverCategoryEFConfiguration : IEntityTypeConfiguration<DriverLicenseDriverCategory>
    {
        public void Configure(EntityTypeBuilder<DriverLicenseDriverCategory> builder)
        {
            builder.HasKey(k => new { k.DriverCategoryId, k.DriverLicenseId });
            builder.HasOne(b => b.DriverCategory).WithMany(bg => bg.DriverLicenseDriverCategories).HasForeignKey(b => b.DriverCategoryId);
            builder.HasOne(g => g.DriverLicense).WithMany(bg => bg.DriverLicenseDriverCategories).HasForeignKey(g => g.DriverLicenseId);
            builder.Property(b => b.DriverCategoryId).ValueGeneratedNever();
            builder.Property(g => g.DriverLicenseId).ValueGeneratedNever();
        }
    }
}
