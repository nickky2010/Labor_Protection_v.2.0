using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations
{
    class DriverCategoryEFConfiguration : IEntityTypeConfiguration<DriverCategory>
    {
        public void Configure(EntityTypeBuilder<DriverCategory> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasMany(b => b.DriverLicenseDriverCategories).WithOne(bg => bg.DriverCategory).HasForeignKey(b => b.DriverCategoryId);
        }
    }
}
