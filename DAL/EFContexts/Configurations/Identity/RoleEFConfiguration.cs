using DAL.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations.Identity
{
    class RoleEFConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.RowVersion).IsRowVersion();

            builder.HasData(new Role[]
            {
                new Role { Id = "admin", Name = "admin" },
                new Role { Id = "user", Name = "user" }
            });
        }
    }
}
