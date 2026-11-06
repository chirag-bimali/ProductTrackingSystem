using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.NameEn).IsRequired().HasMaxLength(100);
        builder.Property(r => r.NameNp).IsRequired().HasMaxLength(100);
        builder.HasIndex(r => r.NameEn).IsUnique();
        builder.HasIndex(r => r.NameNp).IsUnique();
    }
}
