﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EFContexts.Configurations
{
    class PositionEFConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.RowVersion).IsRowVersion();
        }
    }
}
