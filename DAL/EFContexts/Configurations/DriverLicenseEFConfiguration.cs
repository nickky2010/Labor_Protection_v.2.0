using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations
{
    class DriverLicenseEFConfiguration : IEntityTypeConfiguration<DriverLicense>
    {
        public void Configure(EntityTypeBuilder<DriverLicense> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasOne(b => b.Employee).WithOne(ba => ba.DriverLicense).HasForeignKey<Employee>(b => b.DriverLicenseId);
            builder.HasMany(b => b.DriverLicenseDriverCategories).WithOne(bg => bg.DriverLicense).HasForeignKey(b => b.DriverLicenseId);

        }
    }
}
