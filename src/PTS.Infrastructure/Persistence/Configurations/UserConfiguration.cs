using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(u => u.NameNp).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Contact).IsRequired().HasMaxLength(20);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.ImageUrl).HasMaxLength(500);
        builder.Property(u => u.VerificationStatus).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();

        // Login works with email or contact, so both must be unique
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.Contact).IsUnique();
    }
}