using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
  public void Configure(EntityTypeBuilder<District> builder)
  {
    builder.ToTable("districts");
    builder.HasKey(d => d.Id);
    builder.Property(d => d.Id).ValueGeneratedNever();
    builder.Property(d => d.NameEn).IsRequired().HasMaxLength(100);
    builder.Property(d => d.NameNp).IsRequired().HasMaxLength(100);

    builder.HasOne(d => d.Province)
        .WithMany(p => p.Districts)
        .HasForeignKey(d => d.ProvinceId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}