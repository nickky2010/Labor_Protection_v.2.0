using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations
{
    class DriverLicensePhotoEFConfiguration : IEntityTypeConfiguration<DriverLicensePhoto>
    {
        public void Configure(EntityTypeBuilder<DriverLicensePhoto> builder)
        {
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasOne(b => b.DriverLicense).WithMany(ba => ba.Photos).HasForeignKey(b => b.DriverLicenseId);
        }
    }
}
