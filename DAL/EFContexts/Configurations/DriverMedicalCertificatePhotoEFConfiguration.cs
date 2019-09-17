using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations
{
    class DriverMedicalCertificatePhotoEFConfiguration : IEntityTypeConfiguration<DriverMedicalCertificatePhoto>
    {
        public void Configure(EntityTypeBuilder<DriverMedicalCertificatePhoto> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.RowVersion).IsRowVersion();
            //builder.HasOne(b => b.DriverMedicalCertificate).WithMany(ba => ba.Photos).HasForeignKey(b => b.DriverMedicalCertificateId);
        }
    }
}
