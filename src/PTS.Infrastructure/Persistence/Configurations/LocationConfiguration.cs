using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.AddressLine).HasMaxLength(300);
        builder.Property(l => l.GpsCoordinates).HasMaxLength(100);

        builder.HasOne(l => l.Ward)
            .WithMany()
            .HasForeignKey(l => l.WardId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
