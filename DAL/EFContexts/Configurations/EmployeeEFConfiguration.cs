using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations
{
    class EmployeeEFConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasOne(b => b.DriverLicense).WithOne(ba => ba.Employee).HasForeignKey<DriverLicense>(b => b.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.DriverMedicalCertificate).WithOne(ba => ba.Employee).HasForeignKey<DriverMedicalCertificate>(b => b.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Position).WithMany(ba => ba.Employees).HasForeignKey(b => b.PositionId);
        }
    }
}
