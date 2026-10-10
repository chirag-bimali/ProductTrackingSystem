using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.NameEn).IsRequired().HasMaxLength(100);
        builder.Property(r => r.NameNp).IsRequired().HasMaxLength(100);
        builder.HasIndex(r => r.NameEn).IsUnique();
    }
}