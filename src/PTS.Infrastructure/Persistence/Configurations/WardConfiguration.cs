using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Configurations;

public class WardConfiguration : IEntityTypeConfiguration<Ward>
{
  public void Configure(EntityTypeBuilder<Ward> builder)
  {
    builder.ToTable("wards");
    builder.HasKey(w => w.Id);
    builder.Property(w => w.Id).ValueGeneratedNever();

    // A municipality cannot have two wards with the same number
    builder.HasIndex(w => new { w.MunicipalityId, w.Number }).IsUnique();

    builder.HasOne(w => w.Municipality)
        .WithMany(m => m.Wards)
        .HasForeignKey(w => w.MunicipalityId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}