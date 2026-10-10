using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Configurations;

public class FactoryConfiguration : IEntityTypeConfiguration<Factory>
{
    public void Configure(EntityTypeBuilder<Factory> builder)
    {
        builder.ToTable("factories");
        builder.HasKey(f => f.Id);

        builder.Property(f => f.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(f => f.NameNp).IsRequired().HasMaxLength(200);
        builder.Property(f => f.PanNumber).IsRequired().HasMaxLength(50);
        builder.Property(f => f.Email).IsRequired().HasMaxLength(256);
        builder.Property(f => f.ContactNum).IsRequired().HasMaxLength(20);

        builder.HasIndex(f => f.PanNumber).IsUnique();

        builder.HasOne(f => f.FactoryAdmin)
            .WithMany()
            .HasForeignKey(f => f.FactoryAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Location)
            .WithMany()
            .HasForeignKey(f => f.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}